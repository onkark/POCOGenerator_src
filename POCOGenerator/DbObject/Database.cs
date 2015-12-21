using System;
using System.Collections.Generic;

namespace POCOGenerator.DbObject
{
    public class Database : IDbObject
    {
        public string database_name { get; set; }

        public Server Server { get; set; }
        public List<Table> Tables { get; set; }
        public List<View> Views { get; set; }
        public List<Procedure> Procedures { get; set; }
        public List<Function> Functions { get; set; }
        public List<TVP> TVPs { get; set; }
        public List<PrimaryKey> PrimaryKeys { get; set; }
        public List<ForeignKey> ForeignKeys { get; set; }
        public List<SystemObject> SystemObjects { get; set; }
        public List<Exception> Errors { get; set; }

        public override string ToString()
        {
            return database_name;
        }

        public string GetConnectionString(string connectionString)
        {
            return connectionString + (connectionString.EndsWith(";") ? string.Empty : ";") + string.Format("Initial Catalog={0};", database_name);
        }
    }
}
