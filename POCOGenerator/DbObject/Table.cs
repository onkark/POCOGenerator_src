using System;
using System.Collections.Generic;

namespace POCOGenerator.DbObject
{
    public class Table : IDbObject
    {
        public string table_schema { get; set; }
        public string table_name { get; set; }

        public Database Database { get; set; }
        public List<Column> Columns { get; set; }
        public Exception Error { get; set; }
        public string ClassName { get; set; }

        public override string ToString()
        {
            return table_schema + "." + table_name;
        }
    }
}
