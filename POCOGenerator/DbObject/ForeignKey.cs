using System;

namespace POCOGenerator.DbObject
{
    public class ForeignKey : IDbObject
    {
        public string Foreign_Key { get; set; }
        public string Foreign_Schema { get; set; }
        public string Foreign_Table { get; set; }
        public string Foreign_Column { get; set; }
        public string Primary_Schema { get; set; }
        public string Primary_Table { get; set; }
        public string Primary_Column { get; set; }

        public Database Database { get; set; }

        public override string ToString()
        {
            return Foreign_Key;
        }
    }
}
