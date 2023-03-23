using DatabaseUtils.Utils;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DatabaseUtils.Model.Database
{
    public class DbConnectionString
    {
        public DbConnectionString()
        {
            ConnectionTimeout = 5;
        }

        public DbConnectionString(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public int ConnectionTimeout { get; set; }

        public string ConnectionString { get; set; }



        public string GetConnectionString()
        {
            return !string.IsNullOrEmpty(ConnectionString) ? ConnectionString : string.Format(
                "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Connection Timeout={4}",
                DataSource, InitialCatalog, UserId, Password, ConnectionTimeout);
        }

        public string GetConnectionStringNoCatalog()
        {
            return !string.IsNullOrEmpty(ConnectionString) ? ConnectionString : string.Format(
                "Data Source={0};Persist Security Info=True;User ID={1};Password={2};Connection Timeout={3}",
                DataSource, UserId, Password, ConnectionTimeout);
        }

       
        public string GetConnectionStringWithCatalog()
        {
            var newValue = ConnectionString.ReplaceBetweenTwoStrings("Initial Catalog=", ";integrated security", InitialCatalog);

            //;integrated security
            if (newValue == ConnectionString)
                return ConnectionString.ReplaceBetweenTwoStrings("Initial Catalog=", ";Persist Security", InitialCatalog);

            return newValue;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(ConnectionString) || (!string.IsNullOrEmpty(DataSource) && !string.IsNullOrEmpty(InitialCatalog) &&
                   !string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(Password));
        }

        public bool IsValidNoCatalog()
        {
            return !string.IsNullOrEmpty(ConnectionString) || (!string.IsNullOrEmpty(DataSource) &&
                   !string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(Password));
        }


    }
}
