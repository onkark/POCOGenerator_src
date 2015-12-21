using System;

namespace POCOGenerator.DbObject
{
    public class ProcedureColumn : IDbObject
    {
        public string ColumnName { get; set; }
        public int? ColumnOrdinal { get; set; }
        public int? ColumnSize { get; set; }
        public short? NumericPrecision { get; set; }
        public short? NumericScale { get; set; }
        public bool? IsUnique { get; set; }
        public bool? IsKey { get; set; }
        public string BaseServerName { get; set; }
        public string BaseCatalogName { get; set; }
        public string BaseColumnName { get; set; }
        public string BaseSchemaName { get; set; }
        public string BaseTableName { get; set; }
        public Type DataType { get; set; }
        public bool? AllowDBNull { get; set; }
        public int? ProviderType { get; set; }
        public bool? IsAliased { get; set; }
        public bool? IsExpression { get; set; }
        public bool? IsIdentity { get; set; }
        public bool? IsAutoIncrement { get; set; }
        public bool? IsRowVersion { get; set; }
        public bool? IsHidden { get; set; }
        public bool? IsLong { get; set; }
        public bool? IsReadOnly { get; set; }
        public Type ProviderSpecificDataType { get; set; }
        public string DataTypeName { get; set; }
        public string XmlSchemaCollectionDatabase { get; set; }
        public string XmlSchemaCollectionOwningSchema { get; set; }
        public string XmlSchemaCollectionName { get; set; }
        public string UdtAssemblyQualifiedName { get; set; }
        public int? NonVersionedProviderType { get; set; }
        public bool? IsColumnSet { get; set; }

        public Procedure Procedure { get; set; }

        public string DataTypeDisplay
        {
            get
            {
                if (DataTypeName == "xml")
                    return "XML";
                // sys.geography, sys.geometry, sys.hierarchyid
                if (DataTypeName.Contains("sys."))
                    return DataTypeName.Substring(DataTypeName.IndexOf("sys.") + 4);
                return DataTypeName;
            }
        }

        public string PrecisionDisplay
        {
            get
            {
                string precision = null;

                string DataTypeName = this.DataTypeName.ToLower();

                if (DataTypeName == "binary" || DataTypeName == "char" || DataTypeName == "nchar" || DataTypeName == "nvarchar" || DataTypeName == "varbinary" || DataTypeName == "varchar")
                {
                    if (IsLong == true)
                        precision = "(max)";
                    else if (ColumnSize > 0)
                        precision = "(" + ColumnSize + ")";
                }
                else if (DataTypeName == "decimal" || DataTypeName == "numeric")
                {
                    precision = "(" + NumericPrecision + "," + NumericScale + ")";
                }
                else if (DataTypeName == "datetime2" || DataTypeName == "datetimeoffset" || DataTypeName == "time")
                {
                    precision = "(" + NumericScale + ")";
                }
                else if (DataTypeName == "xml")
                {
                    precision = "(.)";
                }

                return precision;
            }
        }

        public bool IsNullable
        {
            get { return (AllowDBNull ?? false); }
        }

        public override string ToString()
        {
            return ColumnName + " (" + DataTypeDisplay + PrecisionDisplay + ", " + (IsNullable ? "null" : "not null") + ")";
        }
    }
}
