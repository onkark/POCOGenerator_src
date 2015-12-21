using System;
using System.Collections.Generic;
using System.Data;

namespace POCOGenerator.DbObject
{
    public class TVP : IDbObject
    {
        public string tvp_schema { get; set; }
        public string tvp_name { get; set; }
        public int type_table_object_id { get; set; }

        public Database Database { get; set; }
        public List<TVPColumn> TVPColumns { get; set; }
        public Exception Error { get; set; }
        public string ClassName { get; set; }

        public DataTable TVPDataTable { get; set; }

        public override string ToString()
        {
            return tvp_schema + "." + tvp_name;
        }
    }
}
