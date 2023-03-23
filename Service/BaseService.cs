using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DatabaseUtils.Service
{
    public class BaseService
    {
        protected static void HandleError(Exception exception)
        {
            
        }

        public static void SetReaderValues(SqlDataReader reader, object parent)
        {
            var props = parent.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                // Ignore indexers
                .Where(prop => prop.GetIndexParameters().Length == 0)
                // Must be both readable and writable
                .Where(prop => prop.CanWrite && prop.CanRead);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string fieldName = reader.GetName(i);
                try
                {
                    var prop = props.FirstOrDefault(x => x.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));

                    if (prop == null)
                        continue;

                    object value = null;

                    if (prop.PropertyType == typeof(string))
                    {
                        value = reader[prop.Name] == DBNull.Value ? String.Empty : reader[prop.Name].ToString();
                    }
                    else if (prop.PropertyType == typeof(byte))
                    {
                        value = reader[prop.Name] == DBNull.Value ? (byte)0 : Convert.ToByte(reader[prop.Name]);
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        value = reader[prop.Name] == DBNull.Value ? 0 : Convert.ToInt32(reader[prop.Name]);
                    }
                    else if (prop.PropertyType == typeof(long))
                    {
                        value = reader[prop.Name] == DBNull.Value ? 0 : Convert.ToInt64(reader[prop.Name]);
                    }
                    else if (prop.PropertyType == typeof(decimal))
                    {
                        value = reader[prop.Name] == DBNull.Value ? 0 : Convert.ToDecimal(reader[prop.Name]);
                    }
                    else if (prop.PropertyType == typeof(double))
                    {
                        value = reader[prop.Name] == DBNull.Value ? 0 : Convert.ToDouble(reader[prop.Name]);
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        value = reader[prop.Name] != DBNull.Value && Convert.ToBoolean(reader[prop.Name]);
                    }
                    else if (prop.PropertyType == typeof(DateTime))
                    {
                        value = reader[prop.Name] == DBNull.Value
                            ? new DateTime(1900, 1, 1)
                            : Convert.ToDateTime(reader[prop.Name]);
                    }

                    prop.SetValue(parent, value, null);
                }
                catch (Exception ex)
                {
                    //RollbarLog.WriteRollBarLog(ex, null, "BaseSErvice->SetReaderValues");
                }
            }
        }

        protected static void PrepareSqlCommand(SqlCommand sqlCommand)
        {
            sqlCommand.CommandTimeout = 0;
            sqlCommand.CommandType = CommandType.StoredProcedure;
        }
    }
}
