using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Baseline;
using Marten.Codegen;
using Marten.Schema.Hierarchies;
using Marten.Util;
using Npgsql;
using NpgsqlTypes;
using Remotion.Linq;

namespace Marten.Schema
{
    public static class DocumentStorageBuilder
    {
        public static IDocumentStorage Build(IDocumentSchema schema, Type documentType)
        {
            return Build(schema, new DocumentMapping(documentType, schema?.StoreOptions ?? new StoreOptions()));
        }

        public static IDocumentStorage Build(IDocumentSchema schema, DocumentMapping mapping)
        {
            return Build(schema, new[] {mapping}).Single();
        }

        public static IEnumerable<IDocumentStorage> Build(IDocumentSchema schema, DocumentMapping[] mappings)
        {
            // Generate the actual source code
            var code = GenerateDocumentStorageCode(mappings);

            var generator = new AssemblyGenerator();

            // Tell the generator which other assemblies that it should be referencing 
            // for the compilation
            generator.ReferenceAssembly(Assembly.GetExecutingAssembly());
            generator.ReferenceAssemblyContainingType<NpgsqlConnection>();
            generator.ReferenceAssemblyContainingType<QueryModel>();
            generator.ReferenceAssemblyContainingType<DbCommand>();
            generator.ReferenceAssemblyContainingType<Component>();
            generator.ReferenceAssemblyContainingType<DbDataReader>();

            mappings.Select(x => x.DocumentType.Assembly).Distinct().Each(assem => generator.ReferenceAssembly(assem));

            // build the new assembly -- this will blow up if there are any
            // compilation errors with the list of errors and the actual code

            var assembly = generator.Generate(code);

            return assembly
                .GetExportedTypes()
                .Where(x => x.IsConcreteTypeOf<IDocumentStorage>())
                .Select(x => BuildStorageObject(schema, mappings, x));
        }

        public static Type DocumentTypeForStorage(this Type storageType)
        {
            return storageType.FindInterfaceThatCloses(typeof(IdAssignment<>)).GetGenericArguments().Single();
        }

        public static IDocumentStorage BuildStorageObject(IDocumentSchema schema, DocumentMapping[] mappings, Type storageType)
        {
            var docType = storageType.DocumentTypeForStorage();

            var mapping = mappings.Single(m => m.DocumentType == docType);

            return BuildStorageObject(schema, storageType, mapping);
        }

        public static IDocumentStorage BuildStorageObject(IDocumentSchema schema, Type storageType, DocumentMapping mapping)
        {
            var arguments = mapping.ToArguments().Select(arg => arg.GetValue(schema)).ToArray();

            var ctor = storageType.GetConstructors().Single();

            return ctor.Invoke(arguments).As<IDocumentStorage>();
        }

        private static readonly Regex _storenameSanitizer = new Regex("<|>", RegexOptions.Compiled);

        public static string GenerateDocumentStorageCode(DocumentMapping[] mappings)
        {
            var writer = new SourceWriter();

            // TODO -- get rid of the magic strings
            var namespaces = new List<string>
            {
                "System",
                "Marten",
                "Marten.Schema",
                "Marten.Services",
                "Marten.Linq",
                "Marten.Util",
                "Npgsql",
                "Remotion.Linq",
                typeof (NpgsqlDbType).Namespace,
                typeof (IEnumerable<>).Namespace,
                typeof(DbDataReader).Namespace,
                typeof(CancellationToken).Namespace,
                typeof(Task).Namespace
            };
            namespaces.AddRange(mappings.Select(x => x.DocumentType.Namespace));

            namespaces.Distinct().OrderBy(x => x).Each(x => writer.WriteLine($"using {x};"));
            writer.BlankLine();

            writer.StartNamespace("Marten.GeneratedCode");

            mappings.Each(x =>
            {
                GenerateDocumentStorage(x, writer);
                writer.BlankLine();
                writer.BlankLine();
            });

            writer.FinishBlock();
            return writer.Code();
        }

        public static void GenerateDocumentStorage(DocumentMapping mapping, SourceWriter writer)
        {
            var upsertFunction = mapping.ToUpsertFunction();

            var id_NpgsqlDbType = TypeMappings.ToDbType(mapping.IdMember.GetMemberType());
            
            var typeName = mapping.DocumentType.GetTypeName();
            var storeName = _storenameSanitizer.Replace(mapping.DocumentType.GetPrettyName(), string.Empty);

            var storageArguments = mapping.ToArguments().ToArray();
            var ctorArgs = storageArguments.Select(x => x.ToCtorArgument()).Join(", ");
            var ctorLines = storageArguments.Select(x => x.ToCtorLine()).Join("\n");
            var fields = storageArguments.Select(x => x.ToFieldDeclaration()).Join("\n");

            var baseType = mapping.IsHierarchy() ? "HierarchicalResolver" : "Resolver";

            var callBaseCtor = mapping.IsHierarchy() ? $": base({HierarchyArgument.Hierarchy})" : string.Empty;

            writer.Write(
                $@"
BLOCK:public class {storeName}Storage : {baseType}<{typeName}>, IDocumentStorage, IBulkLoader<{typeName}>, IdAssignment<{typeName}>, IResolver<{typeName}>

{fields}

BLOCK:public {storeName}Storage({ctorArgs}) {callBaseCtor}
{ctorLines}
END

public Type DocumentType => typeof ({typeName});

BLOCK:public NpgsqlCommand UpsertCommand(object document, string json)
return UpsertCommand(({typeName})document, json);
END

BLOCK:public NpgsqlCommand LoaderCommand(object id)
return new NpgsqlCommand(`select {mapping.SelectFields().Join(", ")} from {mapping.Table.QualifiedName} as d where id = :id`).With(`id`, id);
END

BLOCK:public NpgsqlCommand DeleteCommandForId(object id)
return new NpgsqlCommand(`delete from {mapping.Table.QualifiedName} where id = :id`).With(`id`, id);
END

BLOCK:public NpgsqlCommand DeleteCommandForEntity(object entity)
return DeleteCommandForId((({typeName})entity).{mapping.IdMember.Name});
END

BLOCK:public NpgsqlCommand LoadByArrayCommand<T>(T[] ids)
return new NpgsqlCommand(`select {mapping.SelectFields().Join(", ")} from {mapping.Table.QualifiedName} as d where id = ANY(:ids)`).With(`ids`, ids);
END

BLOCK:public void Remove(IIdentityMap map, object entity)
var id = Identity(entity);
map.Remove<{typeName}>(id);
END

BLOCK:public void Delete(IIdentityMap map, object id)
map.Remove<{typeName}>(id);
END

BLOCK:public void Store(IIdentityMap map, object id, object entity)
map.Store<{typeName}>(id, ({typeName})entity);
END

BLOCK:public object Assign({typeName} document, out bool assigned)
{mapping.IdStrategy.AssignmentBodyCode(mapping.IdMember)}
return document.{mapping.IdMember.Name};
END

BLOCK:public object Retrieve({typeName} document)
return document.{mapping.IdMember.Name};
END


public NpgsqlDbType IdType => NpgsqlDbType.{id_NpgsqlDbType};

BLOCK:public object Identity(object document)
return (({typeName})document).{mapping.IdMember.Name};
END

{upsertFunction.ToUpdateBatchMethod(typeName)}

{upsertFunction.ToBulkInsertMethod(typeName)}


END

");
        }

    }
}