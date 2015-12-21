using System;
using System.Collections.Generic;

namespace POCOGenerator.DbObject
{
    public class Procedure : IDbObject
    {
        public string routine_schema { get; set; }
        public string routine_name { get; set; }

        public Database Database { get; set; }
        public List<ProcedureParameter> ProcedureParameters { get; set; }
        public List<ProcedureColumn> ProcedureColumns { get; set; }
        public Exception Error { get; set; }
        public string ClassName { get; set; }

        public override string ToString()
        {
            return routine_schema + "." + routine_name;
        }
    }
}
