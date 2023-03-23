using DatabaseUtils.Model.Database;

namespace DatabaseUtils.Model.General
{
    public class Query<T>
    { 
        public T Parameter { get; set; }

        public DbConnectionString Conn { get; set; } 
    }
}
