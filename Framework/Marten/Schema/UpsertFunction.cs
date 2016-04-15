using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Baseline;
using Marten.Util;
using NpgsqlTypes;

namespace Marten.Schema
{
    public class UpsertFunction
    {
        private readonly string _primaryKeyConstraintName;
        private readonly FunctionName _functionName;
        private readonly TableName _tableName;

        public readonly IList<UpsertArgument> Arguments = new List<UpsertArgument>();

        public UpsertFunction(DocumentMapping mapping)
        {
            if (mapping == null) throw new ArgumentNullException(nameof(mapping));

            _functionName = mapping.UpsertFunction;
            _tableName = mapping.Table;
            _primaryKeyConstraintName = "pk_" + mapping.Table.Name;
                
            var idType = mapping.IdMember.GetMemberType();
            var pgIdType = TypeMappings.GetPgType(idType);

            Arguments.Add(new UpsertArgument
            {
                Arg = "docId",
                PostgresType = pgIdType,
                Column = "id",
                Members = new[] {mapping.IdMember}
            });
            Arguments.Add(new UpsertArgument
            {
                Arg = "doc",
                PostgresType = "JSONB",
                DbType = NpgsqlDbType.Jsonb,
                Column = "data",
                BulkInsertPattern = "writer.Write(serializer.ToJson(x), NpgsqlDbType.Jsonb);",
                BatchUpdatePattern = "*"
            });
        }

        public void WriteFunctionSql(PostgresUpsertType upsertType, StringWriter writer)
        {
            var ordered = OrderedArguments();

            var argList = ordered.Select(x => x.ArgumentDeclaration()).Join(", ");

            var updates = ordered.Where(x => x.Column != "id")
                .Select(x => $"\"{x.Column}\" = {x.Arg}").Join(", ");

            var inserts = ordered.Select(x => $"\"{x.Column}\"").Join(", ");
            var valueList = ordered.Select(x => x.Arg).Join(", ");

            if (upsertType == PostgresUpsertType.Legacy)
            {
                writer.WriteLine($"CREATE OR REPLACE FUNCTION {_functionName.QualifiedName}({argList}) RETURNS VOID AS");
                writer.WriteLine("$$");
                writer.WriteLine("BEGIN");
                writer.WriteLine($"LOCK TABLE {_tableName.QualifiedName} IN SHARE ROW EXCLUSIVE MODE;");
                writer.WriteLine($"  WITH upsert AS (UPDATE {_tableName.QualifiedName} SET {updates} WHERE id=docId RETURNING *) ");
                writer.WriteLine($"  INSERT INTO {_tableName.QualifiedName} ({inserts})");
                writer.WriteLine($"  SELECT {valueList} WHERE NOT EXISTS (SELECT * FROM upsert);");
                writer.WriteLine("END;");
                writer.WriteLine("$$ LANGUAGE plpgsql;");
            }
            else
            {
                writer.WriteLine($"CREATE OR REPLACE FUNCTION {_functionName.QualifiedName}({argList}) RETURNS VOID AS");
                writer.WriteLine("$$");
                writer.WriteLine("BEGIN");
                writer.WriteLine($"INSERT INTO {_tableName.QualifiedName} ({inserts}) VALUES ({valueList})");
                writer.WriteLine($"  ON CONFLICT ON CONSTRAINT {_primaryKeyConstraintName}");
                writer.WriteLine($"  DO UPDATE SET {updates};");
                writer.WriteLine("END;");
                writer.WriteLine("$$ LANGUAGE plpgsql;");
            }
        }

        private UpsertArgument[] OrderedArguments()
        {
            return Arguments.OrderBy(x => x.Arg).ToArray();
        }

        public string ToUpdateBatchMethod(string typeName)
        {
            var parameters = OrderedArguments().Select(x => x.ToUpdateBatchParameter()).Join("");

            return $@"
BLOCK:public void RegisterUpdate(UpdateBatch batch, object entity)
var document = ({typeName})entity;
var function = new FunctionName(`{_functionName.Schema}`, `{_functionName.Name}`);
batch.Sproc(function){parameters.Replace("*", ".JsonEntity(`doc`, document)")};
END

BLOCK:public void RegisterUpdate(UpdateBatch batch, object entity, string json)
var document = ({typeName})entity;
var function = new FunctionName(`{_functionName.Schema}`, `{_functionName.Name}`);
batch.Sproc(function){parameters.Replace("*", ".JsonBody(`doc`, json)")};
END
";
        }

        public string ToBulkInsertMethod(string typeName)
        {
            var columns = OrderedArguments().Select(x => $"\\\"{x.Column}\\\"").Join(", ");

            var writerStatements = OrderedArguments()
                .Select(x => x.ToBulkInsertWriterStatement())
                .Join("\n");

            return $@"
BLOCK:public void Load(ISerializer serializer, NpgsqlConnection conn, IEnumerable<{typeName}> documents)
BLOCK:using (var writer = conn.BeginBinaryImport(`COPY {_tableName}({columns}) FROM STDIN BINARY`))
BLOCK:foreach (var x in documents)
bool assigned = false;
Assign(x, out assigned);
writer.StartRow();
{writerStatements}
END
END
END
";
        }
    }
}