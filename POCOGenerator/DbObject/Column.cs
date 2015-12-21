using System;

namespace POCOGenerator.DbObject
{
    public class Column : IDbObject
    {
        public string table_catalog { get; set; }
        public string table_schema { get; set; }
        public string table_name { get; set; }
        public string column_name { get; set; }
        public int? ordinal_position { get; set; }
        public string column_default { get; set; }
        public string is_nullable { get; set; }
        public string data_type { get; set; }
        public int? character_maximum_length { get; set; }
        public int? character_octet_length { get; set; }
        public byte? numeric_precision { get; set; }
        public short? numeric_precision_radix { get; set; }
        public int? numeric_scale { get; set; }
        public short? datetime_precision { get; set; }
        public string character_set_catalog { get; set; }
        public string character_set_schema { get; set; }
        public string character_set_name { get; set; }
        public string collation_catalog { get; set; }
        public bool? is_sparse { get; set; }
        public bool? is_column_set { get; set; }
        public bool? is_filestream { get; set; }

        public Table  Table  { get; set; }

        public PrimaryKey PrimaryKey { get; set; }
        public bool IsPrimaryKey { get { return PrimaryKey != null; } }

        public ForeignKey ForeignKey { get; set; }
        public bool IsForeignKey { get { return ForeignKey != null; } }

        public string DataTypeDisplay
        {
            get
            {
                if (data_type == "xml")
                    return "XML";
                return data_type;
            }
        }

        public string PrecisionDisplay
        {
            get
            {
                string precision = null;

                string data_type = this.data_type.ToLower();

                if (data_type == "binary" || data_type == "char" || data_type == "nchar" || data_type == "nvarchar" || data_type == "varbinary" || data_type == "varchar")
                {
                    if (character_maximum_length == -1)
                        precision = "(max)";
                    else if (character_maximum_length > 0)
                        precision = "(" + character_maximum_length + ")";
                }
                else if (data_type == "decimal" || data_type == "numeric")
                {
                    precision = "(" + numeric_precision + "," + numeric_scale + ")";
                }
                else if (data_type == "datetime2" || data_type == "datetimeoffset" || data_type == "time")
                {
                    precision = "(" + datetime_precision + ")";
                }
                else if (data_type == "xml")
                {
                    precision = "(.)";
                }

                return precision;
            }
        }

        public bool IsNullable
        {
            get { return (is_nullable == "YES"); }
        }

        public override string ToString()
        {
            return column_name + " (" + DataTypeDisplay + PrecisionDisplay + ", " + (IsNullable ? "null" : "not null") + ")";
        }

        public string ToStringWithPKFK()
        {
            return column_name + " (" + (IsPrimaryKey ? "PK, " : string.Empty) + (IsForeignKey ? "FK, " : string.Empty) + DataTypeDisplay + PrecisionDisplay + ", " + (IsNullable ? "null" : "not null") + ")";
        }
    }
}
