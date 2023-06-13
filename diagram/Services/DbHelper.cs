using System;
using System.Data;
using Npgsql;

namespace diagram.Services
{
    internal class DbHelper
    {
        private string Schema;
        private NpgsqlConnection Connection;
        private static DbHelper? Instance;

        private DbHelper(string host, int port, string username, string password, string database, string schema)
        {
            Schema = schema;
            Connection = new NpgsqlConnection()
            {
                ConnectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database};"
            };
        }

        public static DbHelper Initialize(string host, int port, string username, string password, string database, string schema)
        {
            if (Instance == null)
            {
                Instance = new DbHelper(host, port, username, password, database, schema);
            }
            return Instance;
        }

        public static DbHelper GetInstance()
        {
            if (Instance == null)
            {
                throw new InvalidOperationException("DbHelper has not been initialized.");
            }
            return Instance;
        }

        public int ExecuteNonQuery(string sql)
        {
            int affectedRows;
            try
            {
                Connection.Open();
                affectedRows = new NpgsqlCommand()
                {
                    Connection = Connection,
                    CommandText = sql
                }.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
            return affectedRows;
        }

        public DataTable ExecuteQuery(string sql)
        {
            DataTable table = new DataTable();
            try
            {
                Connection.Open();
                new NpgsqlDataAdapter(sql, Connection).Fill(table);
                Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
            return table;
        }

        public int Create(string table, Dictionary<string, object> data)
        {
            string column = "";
            string value = "";

            foreach (String key in data.Keys)
            {
                column += key;
                value += $"'{data[key]}'";

                if (key != data.Keys.Last())
                {
                    column += ", ";
                    value += ", ";
                }
            }

            int affectedRows;
            try
            {
                affectedRows = ExecuteNonQuery($"INSERT INTO {Schema}.{table} ({column}) VALUES ({value})");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
            return affectedRows;
        }

        public DataTable Read(string table, string? orderBy)
        {
            // String options = "";


            return ExecuteQuery($"SELECT * FROM {Schema}.{table} {(orderBy != null ? $"ORDER BY {orderBy}" : "")}");
        }

        public int Update(string table, Dictionary<string, object> data, string whereColumn, int? whereValue)
        {
            string set = "";
            foreach (string key in data.Keys)
            {
                set += $"{key} = '{data[key]}'";
                if (key != data.Keys.Last())
                {
                    set += ", ";
                }
            }
            int affectedRows;
            try
            {
                affectedRows = ExecuteNonQuery($"UPDATE {Schema}.{table} SET {set} WHERE {whereColumn} = {whereValue}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;

            }
            return affectedRows;
        }

        public int Delete(string table, string whereColumn, int? whereValue)
        {
            int affectedRows;
            try
            {
                affectedRows = ExecuteNonQuery($"DELETE FROM {Schema}.{table} WHERE {whereColumn} = {whereValue}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
            return affectedRows;
        }
    }
}
