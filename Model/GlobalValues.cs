using DatabaseUtils.Model.Database;
using System.Net.Configuration;

namespace DatabaseUtils.Model
{
    public static class GlobalValues
    {
        public static string ConnectionString { get; set; }
        public static string Action { get; set; } 
        public static DbConnectionString DbConnectionString { get; set; }

    }
}
