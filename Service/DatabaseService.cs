using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DatabaseUtils.Model.Database;
using DatabaseUtils.Model.General;

namespace DatabaseUtils.Service
{
    public class DatabaseService : BaseService
    {

        public static OperationResult<List<Database>> GetDataBases(Query<Database> query)
        {
            OperationResult<List<Database>> result = new OperationResult<List<Database>>();

            try
            {

                using (SqlConnection conn = new SqlConnection(query.Conn.GetConnectionStringNoCatalog()))
                {
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = @"SELECT dbid AS Id, name AS Name 
	                                            FROM master.dbo.sysdatabases";

                        conn.Open();

                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            result.Response = new List<Database>();

                            while (reader.Read())
                            {
                                Database database = new Database();
                                SetReaderValues(reader, database);
                                result.Response.Add(database);
                            }

                            result.Result = true;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                HandleError(e);
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        public static OperationResult<List<DatabaseTable>> GetDataBaseTables(Query<Database> query)
        {
            OperationResult<List<DatabaseTable>> result = new OperationResult<List<DatabaseTable>>();

            try
            {

                using (SqlConnection conn = new SqlConnection(query.Conn.GetConnectionString()))
                {
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = @"SELECT 1 AS Id, TABLE_NAME AS Name
                                                FROM " + (string.IsNullOrEmpty(query.Conn.InitialCatalog) ? "" : ("[" + query.Conn.InitialCatalog + "].")) + @"INFORMATION_SCHEMA.TABLES
                                                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME <> 'sysdiagrams' ";


                        //if (!string.IsNullOrEmpty(query.Conn.InitialCatalog))
                        //    comm.Parameters.AddWithValue("@pDatabase", query.Conn.InitialCatalog);


                        conn.Open();

                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            result.Response = new List<DatabaseTable>();

                            while (reader.Read())
                            {
                                DatabaseTable databaseTable = new DatabaseTable();
                                SetReaderValues(reader, databaseTable);
                                result.Response.Add(databaseTable);
                            }

                            result.Result = true;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                HandleError(e);
                result.ErrorMessage = e.Message;
            }

            return result;
        }



        public static OperationResult<List<DatabaseColumn>> GetDataBaseTableColumns(Query<DatabaseTable> query)
        {
            OperationResult<List<DatabaseColumn>> result = new OperationResult<List<DatabaseColumn>>();

            try
            {

                using (SqlConnection conn = new SqlConnection(query.Conn.GetConnectionString()))
                {
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = @"SELECT ORDINAL_POSITION AS Id, COLUMN_NAME AS Name, DATA_TYPE AS DataType, ISNULL(CHARACTER_MAXIMUM_LENGTH, 0) AS Length
                                                FROM  " + (string.IsNullOrEmpty(query.Conn.InitialCatalog) ? "" : ("[" + query.Conn.InitialCatalog + "].")) + @"INFORMATION_SCHEMA.COLUMNS
                                                WHERE TABLE_NAME = @TableName ";

                        comm.Parameters.AddWithValue("@TableName", query.Parameter.Name);
                        conn.Open();

                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            result.Response = new List<DatabaseColumn>();

                            while (reader.Read())
                            {
                                DatabaseColumn databaseColumn = new DatabaseColumn();
                                SetReaderValues(reader, databaseColumn);
                                result.Response.Add(databaseColumn);
                            }

                            result.Result = true;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                HandleError(e);
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        public static OperationResult<Database> CreateDatabase(Query<Database> query)
        {
            OperationResult<Database> result = new OperationResult<Database>();

            try
            {

                using (SqlConnection conn = new SqlConnection(query.Conn.GetConnectionStringNoCatalog()))
                {
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = $"CREATE DATABASE {query.Parameter.Name}"; 
                        conn.Open();
                        comm.ExecuteNonQuery();
                        result.Result = true;
                    }
                }

            }
            catch (Exception e)
            {
                HandleError(e);
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        public static OperationResult<bool> ExecuteSql(Query<DatabaseCommand> query)
        {
            OperationResult<bool> result = new OperationResult<bool>();

            try
            {

                using (SqlConnection conn = new SqlConnection(query.Conn.GetConnectionStringWithCatalog()))
                {
                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = query.Parameter.Command;
                        conn.Open();
                        comm.ExecuteNonQuery();
                        result.Result = true;
                    }
                }

            }
            catch (Exception e)
            {
                HandleError(e);
                result.ErrorMessage = e.Message;
            }

            return result;
        }

    }
}
