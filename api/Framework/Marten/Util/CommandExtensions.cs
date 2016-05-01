﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Baseline;
using Marten.Schema;
using Npgsql;
using NpgsqlTypes;

namespace Marten.Util
{
    public static class CommandExtensions
    {
        public static IEnumerable<T> Fetch<T>(this NpgsqlCommand cmd, string sql, Func<DbDataReader, T> transform, params object[] parameters)
        {
            cmd.WithText(sql);
            parameters.Each(x =>
            {
                var param = cmd.AddParameter(x);
                cmd.CommandText = cmd.CommandText.UseParameter(param);
            });

             

            var list = new List<T>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(transform(reader));
                }
            }

            return list;
        }

        public static NpgsqlCommand AppendQuery(this NpgsqlCommand command, string sql)
        {
            if (command.CommandText.IsEmpty())
            {
                command.CommandText = sql;
            }
            else
            {
                command.CommandText += ";" + sql;
            }

            return command;
        }

        public static NpgsqlParameter AddParameter(this NpgsqlCommand command, object value)
        {
            var name = "arg" + command.Parameters.Count;

            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);

            return parameter;
        }

        public static NpgsqlParameter AddParameter(this NpgsqlCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);

            return parameter;
        }

        public static NpgsqlCommand With(this NpgsqlCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);

            return command;
        }

        public static NpgsqlCommand With(this NpgsqlCommand command, string name, object value, NpgsqlDbType dbType)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            parameter.NpgsqlDbType = dbType;
            command.Parameters.Add(parameter);

            return command;
        }

        public static NpgsqlCommand AsSproc(this NpgsqlCommand command)
        {
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }

        public static NpgsqlCommand WithJsonParameter(this NpgsqlCommand command, string name, string json)
        {
            command.Parameters.Add(name, NpgsqlDbType.Jsonb).Value = json;

            return command;
        }

        public static NpgsqlCommand CallsSproc(this NpgsqlCommand cmd, FunctionName function)
        {
            if (cmd == null) throw new ArgumentNullException(nameof(cmd));
            if (function == null) throw new ArgumentNullException(nameof(function));

            cmd.CommandText = function.QualifiedName;
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd;
        }

        public static NpgsqlCommand Returns(this NpgsqlCommand command, string name, NpgsqlDbType type)
        {
            var parameter = command.AddParameter(name);
            parameter.NpgsqlDbType = type;
            parameter.Direction = ParameterDirection.ReturnValue;
            return command;
        }

        public static NpgsqlCommand WithText(this NpgsqlCommand command, string sql)
        {
            command.CommandText = sql;
            return command;
        }

        public static NpgsqlCommand CreateCommand(this NpgsqlConnection conn, string command)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = command;

            return cmd;
        }
    }
}