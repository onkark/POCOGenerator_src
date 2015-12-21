using System;

namespace POCOGenerator.DbObject
{
    public class PrimaryKey : IDbObject
    {
        public string Schema_Name { get; set; }
        public string Table_Name { get; set; }
        public byte Ordinal { get; set; }
        public string Column_Name { get; set; }
        public bool? Is_Descending { get; set; }
        public bool Is_Identity { get; set; }

        public Database Database { get; set; }

        public override string ToString()
        {
            return Column_Name;
        }
    }
}
