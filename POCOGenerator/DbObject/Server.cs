using System;
using System.Collections.Generic;

namespace POCOGenerator.DbObject
{
    public class Server : IDbObject
    {
        public string ServerName { get; set; }
        public string InstanceName { get; set; }
        public string IsClustered { get; set; }
        public string Version { get; set; }

        public List<Database> Databases { get; set; }

        public override string ToString()
        {
            return ServerName + (string.IsNullOrEmpty(InstanceName) ? string.Empty : "\\" + InstanceName);
        }
    }
}
