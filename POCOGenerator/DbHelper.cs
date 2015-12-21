using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using POCOGenerator.DbObject;

namespace POCOGenerator
{
    public static class DbHelper
    {
        #region Connection

        public static string ConnectionString { get; set; }

        public static bool TestConnection()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion

        #region Server

        public static List<Server> GetServers()
        {
            // http://msdn.microsoft.com/en-us/library/a6t1z9x2.aspx
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable dataSources = instance.GetDataSources();
            List<Server> servers = dataSources.ToList<Server>();
            return servers;
        }

        public delegate void BuildingDbObjectHandler(IDbObject dbObject);
        public delegate void BuiltDbObjectHandler(IDbObject dbObject);

        public static void BuildServerSchema(Server server, string initialCatalog, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            server.Databases = GetDatabases(initialCatalog);

            foreach (var database in server.Databases.OrderBy<Database, string>(d => d.ToString()))
            {
                database.Server = server;
                BuildDatabaseSchema(database, buildingDbObject, builtDbObject);
            }
        }

        #endregion

        #region Databases

        public static List<Database> GetDatabases(string initialCatalog = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    List<Database> databases =
                        connection.GetSchema("Databases").ToList<Database>().
                        Where(d => d.database_name != "master" && d.database_name != "model" && d.database_name != "msdb" && d.database_name != "tempdb").
                        Where(d => string.IsNullOrEmpty(initialCatalog) || string.Compare(d.database_name, initialCatalog, true) == 0).ToList<Database>();
                    return databases;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get databases.", ex);
            }
        }

        private static void BuildDatabaseSchema(Database database, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            buildingDbObject(database);

            database.Errors = new List<Exception>();

            try
            {
                database.SystemObjects = GetSystemObjects(database);
                if (database.SystemObjects != null)
                {
                    database.SystemObjects.ForEach(so =>
                    {
                        so.Database = database;
                        so.type = so.type.Trim();
                    });
                }
            }
            catch (Exception ex)
            {
                database.Errors.Add(ex);
            }

            try
            {
                database.PrimaryKeys = GetPrimaryKeys(database);
                if (database.PrimaryKeys != null)
                    database.PrimaryKeys.ForEach(pk => pk.Database = database);
            }
            catch (Exception ex)
            {
                database.Errors.Add(ex);
            }

            try
            {
                database.ForeignKeys = GetForeignKeys(database);
                if (database.ForeignKeys != null)
                    database.ForeignKeys.ForEach(fk => fk.Database = database);
            }
            catch (Exception ex)
            {
                database.Errors.Add(ex);
            }

            try
            {
                BuildSchemaTVPs(database, buildingDbObject, builtDbObject);
            }
            catch (Exception ex)
            {
                database.Errors.Add(ex);
            }

            try
            {
                BuildSchemaTables(database, buildingDbObject, builtDbObject);
            }
            catch (Exception ex)
            {
                database.Errors.Add(ex);
            }

            try
            {
                BuildSchemaViews(database, buildingDbObject, builtDbObject);
            }
            catch (Exception ex)
            {
                database.Errors.Add(ex);
            }

            try
            {
                BuildSchemaProcedures(database, buildingDbObject, builtDbObject);
            }
            catch (Exception ex)
            {
                database.Errors.Add(ex);
            }

            try
            {
                BuildSchemaFunctions(database, buildingDbObject, builtDbObject);
            }
            catch (Exception ex)
            {
                database.Errors.Add(ex);
            }

            builtDbObject(database);
        }

        #endregion

        #region System Objects

        private static List<SystemObject> GetSystemObjects(Database database)
        {
            try
            {
                string connectionString = database.GetConnectionString(ConnectionString);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            select 
                                al.type,
                                object_schema = ss.name,
                                object_name = al.name
                            from sys.all_objects al
                            inner join sys.schemas ss on al.schema_id = ss.schema_id
                            where al.is_ms_shipped = 1
                        ";
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 5;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable systemObjectsDT = new DataTable();
                            systemObjectsDT.Load(reader);
                            return systemObjectsDT.ToList<SystemObject>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get system objects.", ex);
            }
        }

        #endregion

        #region Primary Keys and Foreign Keys

        private static List<PrimaryKey> GetPrimaryKeys(Database database)
        {
            try
            {
                string connectionString = database.GetConnectionString(ConnectionString);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            select 
	                            Schema_Name = ss.name,
	                            Table_Name = object_name(kc.parent_object_id),
	                            Ordinal = ic.key_ordinal,
	                            Column_Name = c.name,
	                            Is_Descending = ic.is_descending_key,
	                            Is_Identity = c.is_identity
                            from sys.key_constraints kc
                            inner join sys.index_columns ic on kc.parent_object_id = ic.object_id and kc.unique_index_id = ic.index_id
                            inner join sys.columns c on ic.object_id = c.object_id and ic.column_id = c.column_id
                            inner join sys.schemas ss on kc.schema_id = ss.schema_id
                            order by Schema_Name, Table_Name, Ordinal
                        ";
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 5;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable primaryKeysDT = new DataTable();
                            primaryKeysDT.Load(reader);
                            return primaryKeysDT.ToList<PrimaryKey>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get primary keys.", ex);
            }
        }

        private static List<ForeignKey> GetForeignKeys(Database database)
        {
            try
            {
                string connectionString = database.GetConnectionString(ConnectionString);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            select 
	                            Foreign_Key = f.name,
	                            Foreign_Schema = ssf.name,
	                            Foreign_Table = object_name(f.parent_object_id),
	                            Foreign_Column = col_name(fc.parent_object_id, fc.parent_column_id),
	                            Primary_Schema = ssp.name,
	                            Primary_Table = object_name (f.referenced_object_id),
	                            Primary_Column = col_name(fc.referenced_object_id, fc.referenced_column_id)
                            from sys.foreign_keys f
                            inner join sys.foreign_key_columns fc on f.object_id = fc.constraint_object_id
                            inner join sys.schemas ssf on f.schema_id = ssf.schema_id
                            inner join sys.tables st on f.referenced_object_id = st.object_id
                            inner join sys.schemas ssp on st.schema_id = ssp.schema_id
                            order by Foreign_Schema, Foreign_Table, Foreign_Column
                            ";
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 5;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable foreignKeysDT = new DataTable();
                            foreignKeysDT.Load(reader);
                            return foreignKeysDT.ToList<ForeignKey>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get foreign keys.", ex);
            }
        }

        #endregion

        #region TVPs

        private static void BuildSchemaTVPs(Database database, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            database.TVPs = GetTVPs(database);

            if (database.SystemObjects != null)
                database.TVPs = database.TVPs.Where(tvp => database.SystemObjects.Count(so => so.object_schema == tvp.tvp_schema && so.object_name == tvp.tvp_name && so.type == "TT") == 0).ToList();

            foreach (var tvp in database.TVPs.OrderBy<TVP, string>(t => t.ToString()))
            {
                tvp.Database = database;
                GetTVPSchema(tvp, buildingDbObject, builtDbObject);
                if (tvp.TVPColumns != null)
                    tvp.TVPColumns.ForEach(c => c.TVP = tvp);
            }
        }

        private static List<TVP> GetTVPs(Database database)
        {
            try
            {
                string connectionString = database.GetConnectionString(ConnectionString);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            select 
	                            tvp_schema = ss.name, 
	                            tvp_name = stt.name, 
	                            stt.type_table_object_id 
                            from sys.table_types stt 
                            inner join sys.schemas ss on stt.schema_id = ss.schema_id
                        ";
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 5;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable tvpsDT = new DataTable();
                            tvpsDT.Load(reader);
                            return tvpsDT.ToList<TVP>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get TVPs.", ex);
            }
        }

        public static void GetTVPSchema(TVP tvp, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            buildingDbObject(tvp);

            string connectionString = tvp.Database.GetConnectionString(ConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            select 
	                            sc.*, 
	                            data_type = st.name 
                            from sys.columns sc 
                            inner join sys.types st on sc.system_type_id = st.system_type_id and sc.user_type_id = st.user_type_id
                            where sc.object_id = @tvp_id
                        ";
                        command.Parameters.Add("@tvp_id", SqlDbType.Int).Value = tvp.type_table_object_id;
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 5;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable tvpColumnsDT = new DataTable();
                            tvpColumnsDT.Load(reader);
                            tvp.TVPColumns = tvpColumnsDT.ToList<TVPColumn>();
                        }
                    }
                }
                catch (Exception ex)
                {
                    tvp.Error = ex;
                }
            }

            builtDbObject(tvp);
        }

        private static DataTable GetTVPDataTable(TVP tvp)
        {
            if (tvp.TVPDataTable != null)
                return tvp.TVPDataTable;

            DataTable tvpDataTable = new DataTable();

            if (tvp.TVPColumns != null)
            {
                foreach (TVPColumn column in tvp.TVPColumns)
                {
                    switch ((column.data_type ?? string.Empty).ToLower())
                    {
                        case "bigint": tvpDataTable.Columns.Add(column.name, typeof(long)); break;
                        case "binary": tvpDataTable.Columns.Add(column.name, typeof(byte[])); break;
                        case "bit": tvpDataTable.Columns.Add(column.name, typeof(bool)); break;
                        case "char": tvpDataTable.Columns.Add(column.name, typeof(string)); break;
                        case "date": tvpDataTable.Columns.Add(column.name, typeof(DateTime)); break;
                        case "datetime": tvpDataTable.Columns.Add(column.name, typeof(DateTime)); break;
                        case "datetime2": tvpDataTable.Columns.Add(column.name, typeof(DateTime)); break;
                        case "datetimeoffset": tvpDataTable.Columns.Add(column.name, typeof(DateTimeOffset)); break;
                        case "decimal": tvpDataTable.Columns.Add(column.name, typeof(decimal)); break;
                        case "filestream": tvpDataTable.Columns.Add(column.name, typeof(byte[])); break;
                        case "float": tvpDataTable.Columns.Add(column.name, typeof(double)); break;
                        case "geography": tvpDataTable.Columns.Add(column.name, typeof(Microsoft.SqlServer.Types.SqlGeography)); break;
                        case "geometry": tvpDataTable.Columns.Add(column.name, typeof(Microsoft.SqlServer.Types.SqlGeometry)); break;
                        case "hierarchyid": tvpDataTable.Columns.Add(column.name, typeof(Microsoft.SqlServer.Types.SqlHierarchyId)); break;
                        case "image": tvpDataTable.Columns.Add(column.name, typeof(byte[])); break;
                        case "int": tvpDataTable.Columns.Add(column.name, typeof(int)); break;
                        case "money": tvpDataTable.Columns.Add(column.name, typeof(decimal)); break;
                        case "nchar": tvpDataTable.Columns.Add(column.name, typeof(string)); break;
                        case "ntext": tvpDataTable.Columns.Add(column.name, typeof(string)); break;
                        case "numeric": tvpDataTable.Columns.Add(column.name, typeof(decimal)); break;
                        case "nvarchar": tvpDataTable.Columns.Add(column.name, typeof(string)); break;
                        case "real": tvpDataTable.Columns.Add(column.name, typeof(Single)); break;
                        case "rowversion": tvpDataTable.Columns.Add(column.name, typeof(byte[])); break;
                        case "smalldatetime": tvpDataTable.Columns.Add(column.name, typeof(DateTime)); break;
                        case "smallint": tvpDataTable.Columns.Add(column.name, typeof(short)); break;
                        case "smallmoney": tvpDataTable.Columns.Add(column.name, typeof(decimal)); break;
                        case "sql_variant": tvpDataTable.Columns.Add(column.name, typeof(object)); break;
                        case "text": tvpDataTable.Columns.Add(column.name, typeof(string)); break;
                        case "time": tvpDataTable.Columns.Add(column.name, typeof(TimeSpan)); break;
                        case "timestamp": tvpDataTable.Columns.Add(column.name, typeof(byte[])); break;
                        case "tinyint": tvpDataTable.Columns.Add(column.name, typeof(byte)); break;
                        case "uniqueidentifier": tvpDataTable.Columns.Add(column.name, typeof(Guid)); break;
                        case "varbinary": tvpDataTable.Columns.Add(column.name, typeof(byte[])); break;
                        case "varchar": tvpDataTable.Columns.Add(column.name, typeof(string)); break;
                        case "xml": tvpDataTable.Columns.Add(column.name, typeof(string)); break;
                        default: tvpDataTable.Columns.Add(column.name, typeof(object)); break;
                    }
                }
            }

            tvp.TVPDataTable = tvpDataTable;

            return tvpDataTable;
        }

        #endregion

        #region Tables

        private static void BuildSchemaTables(Database database, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            string connectionString = database.GetConnectionString(ConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    database.Tables = connection.GetSchema("Tables", new string[] { database.database_name, null, null, "BASE TABLE" }).ToList<Table>();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to get tables.", ex);
                }
            }

            if (database.SystemObjects != null)
                database.Tables = database.Tables.Where(t => database.SystemObjects.Count(so => so.object_schema == t.table_schema && so.object_name == t.table_name && (so.type == "IT" || so.type == "S" || so.type == "U")) == 0).ToList();

            foreach (var table in database.Tables.OrderBy<Table, string>(t => t.ToString()))
            {
                table.Database = database;
                GetTableSchema(table, buildingDbObject, builtDbObject);
                if (table.Columns != null)
                    table.Columns.ForEach(c => c.Table = table);
            }
        }

        public static void GetTableSchema(Table table, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            buildingDbObject(table);

            string connectionString = table.Database.GetConnectionString(ConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    table.Columns = connection.GetSchema("Columns", new string[] { table.Database.database_name, table.table_schema, table.table_name, null }).ToList<Column>();

                    foreach (Column column in table.Columns)
                    {
                        if (table.Database.PrimaryKeys != null)
                            column.PrimaryKey = table.Database.PrimaryKeys.Where(pk => pk.Schema_Name == table.table_schema && pk.Table_Name == table.table_name && pk.Column_Name == column.column_name).FirstOrDefault();
                        if (table.Database.ForeignKeys != null)
                            column.ForeignKey = table.Database.ForeignKeys.Where(fk => fk.Foreign_Schema == table.table_schema && fk.Foreign_Table == table.table_name && fk.Foreign_Column == column.column_name).FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    table.Error = ex;
                }
            }

            builtDbObject(table);
        }

        #endregion

        #region Views

        private static void BuildSchemaViews(Database database, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            string connectionString = database.GetConnectionString(ConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    database.Views = connection.GetSchema("Tables", new string[] { database.database_name, null, null, "VIEW" }).ToList<View>();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to get views.", ex);
                }
            }

            if (database.SystemObjects != null)
                database.Views = database.Views.Where(v => database.SystemObjects.Count(so => so.object_schema == v.table_schema && so.object_name == v.table_name && so.type == "V") == 0).ToList();

            foreach (var view in database.Views.OrderBy<View, string>(v => v.ToString()))
            {
                view.Database = database;
                GetViewSchema(view, buildingDbObject, builtDbObject);
                if (view.Columns != null)
                    view.Columns.ForEach(c => c.Table = view);
            }
        }

        public static void GetViewSchema(View view, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            buildingDbObject(view);

            string connectionString = view.Database.GetConnectionString(ConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    view.Columns = connection.GetSchema("Columns", new string[] { view.Database.database_name, view.table_schema, view.table_name, null }).ToList<Column>();
                }
                catch (Exception ex)
                {
                    view.Error = ex;
                }
            }

            builtDbObject(view);
        }

        #endregion

        #region Procedures

        private static void BuildSchemaProcedures(Database database, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            string connectionString = database.GetConnectionString(ConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    database.Procedures = connection.GetSchema("Procedures", new string[] { database.database_name, null, null, "PROCEDURE" }).ToList<Procedure>();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to get procedures.", ex);
                }
            }

            if (database.SystemObjects != null)
                database.Procedures = database.Procedures.Where(p => database.SystemObjects.Count(so => so.object_schema == p.routine_schema && so.object_name == p.routine_name && (so.type == "P" || so.type == "PC" || so.type == "RF" || so.type == "X")) == 0).ToList();

            foreach (var procedure in database.Procedures.OrderBy<Procedure, string>(p => p.ToString()))
            {
                procedure.Database = database;
                GetProcedureSchema(procedure, buildingDbObject, builtDbObject);
                if (procedure.ProcedureParameters != null)
                    procedure.ProcedureParameters.ForEach(p => p.Procedure = procedure);
                if (procedure.ProcedureColumns != null)
                    procedure.ProcedureColumns.ForEach(c => c.Procedure = procedure);
            }
        }

        public static void GetProcedureSchema(Procedure procedure, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            buildingDbObject(procedure);

            string connectionString = procedure.Database.GetConnectionString(ConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    procedure.ProcedureParameters = connection.GetSchema("ProcedureParameters", new string[] { procedure.Database.database_name, procedure.routine_schema, procedure.routine_name, null }).ToList<ProcedureParameter>();
                    GetProcedureSchema(procedure, connection);
                }
                catch (Exception ex)
                {
                    if (ex.Message.StartsWith("Invalid object name '#"))
                    {
                        try
                        {
                            GetProcedureWithTemporaryTablesSchema(procedure, connection);
                        }
                        catch
                        {
                            procedure.Error = new Exception(ex.Message, new Exception("Temporary tables in stored procedure.\nYou may want to add this code to the stored procedure to retrieve the schema:\nIF 1=0\nBEGIN\n    SET FMTONLY OFF\nEND"));
                        }
                    }
                    else
                    {
                        procedure.Error = ex;
                    }
                }
            }

            builtDbObject(procedure);
        }

        private static void GetProcedureSchema(Procedure procedure, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = string.Format("[{0}].[{1}]", procedure.routine_schema, procedure.routine_name);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 5;

                foreach (ProcedureParameter parameter in procedure.ProcedureParameters.OrderBy<ProcedureParameter, int>(c => c.ordinal_position ?? 0))
                {
                    if (parameter.is_result == "NO")
                    {
                        command.Parameters.Add(GetSqlParameter(parameter, procedure.Database));
                    }
                }

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly))
                {
                    DataTable schemaTable = reader.GetSchemaTable();
                    if (schemaTable != null)
                        procedure.ProcedureColumns = schemaTable.ToList<ProcedureColumn>();
                }
            }
        }

        private static void GetProcedureWithTemporaryTablesSchema(Procedure procedure, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 5;

                string commandText = @"
                    IF 1=0
                    BEGIN
                        SET FMTONLY OFF;
                    END
                    exec [{0}].[{1}] ";
                commandText = string.Format(commandText, procedure.routine_schema, procedure.routine_name);
                foreach (ProcedureParameter parameter in procedure.ProcedureParameters.OrderBy<ProcedureParameter, int>(c => c.ordinal_position ?? 0))
                {
                    if (parameter.is_result == "NO")
                    {
                        commandText += parameter.parameter_name + ",";
                        command.Parameters.Add(GetSqlParameter(parameter, procedure.Database));
                    }
                }
                commandText = commandText.TrimEnd(',');
                command.CommandText = commandText;

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly))
                {
                    DataTable schemaTable = reader.GetSchemaTable();
                    if (schemaTable != null)
                        procedure.ProcedureColumns = schemaTable.ToList<ProcedureColumn>();
                }
            }
        }

        #endregion

        #region Functions

        private static void BuildSchemaFunctions(Database database, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            string connectionString = database.GetConnectionString(ConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    database.Functions = connection.GetSchema("Procedures", new string[] { database.database_name, null, null, "FUNCTION" }).ToList<Function>();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to get functions.", ex);
                }
            }

            if (database.SystemObjects != null)
                database.Functions = database.Functions.Where(f => database.SystemObjects.Count(so => so.object_schema == f.routine_schema && so.object_name == f.routine_name && (so.type == "AF" || so.type == "FN" || so.type == "FS" || so.type == "FT" || so.type == "IF" || so.type == "TF")) == 0).ToList();

            List<Function> scalarFunctions = new List<Function>();

            foreach (var function in database.Functions.OrderBy<Function, string>(f => f.ToString()))
            {
                function.Database = database;
                bool isScalarFunction = GetFunctionSchema(function, buildingDbObject, builtDbObject);
                if (isScalarFunction)
                {
                    scalarFunctions.Add(function);
                }
                else
                {
                    if (function.ProcedureParameters != null)
                        function.ProcedureParameters.ForEach(p => p.Procedure = function);
                    if (function.ProcedureColumns != null)
                        function.ProcedureColumns.ForEach(c => c.Procedure = function);
                }
            }

            database.Functions = database.Functions.Except(scalarFunctions).ToList();
        }

        public static bool GetFunctionSchema(Function function, BuildingDbObjectHandler buildingDbObject, BuiltDbObjectHandler builtDbObject)
        {
            buildingDbObject(function);

            bool isScalarFunction = true;

            string connectionString = function.Database.GetConnectionString(ConnectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    function.ProcedureParameters = connection.GetSchema("ProcedureParameters", new string[] { function.Database.database_name, function.routine_schema, function.routine_name, null }).ToList<ProcedureParameter>();

                    isScalarFunction = function.ProcedureParameters.Where(param => param.IsResult).Count() == 1;
                    if (isScalarFunction == false)
                        GetFunctionSchema(function, connection);
                }
                catch (Exception ex)
                {
                    function.Error = ex;
                }
            }

            if (isScalarFunction == false)
                builtDbObject(function);

            return isScalarFunction;
        }

        private static void GetFunctionSchema(Function function, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 5;

                string commandText = string.Format("select * from [{0}].[{1}](", function.routine_schema, function.routine_name);
                foreach (ProcedureParameter parameter in function.ProcedureParameters.OrderBy<ProcedureParameter, int>(c => c.ordinal_position ?? 0))
                {
                    if (parameter.is_result == "NO")
                    {
                        commandText += parameter.parameter_name + ",";
                        command.Parameters.Add(GetSqlParameter(parameter, function.Database));
                    }
                }
                commandText = commandText.TrimEnd(',');
                commandText += ")";
                command.CommandText = commandText;

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly))
                {
                    DataTable schemaTable = reader.GetSchemaTable();
                    if (schemaTable != null)
                        function.ProcedureColumns = schemaTable.ToList<ProcedureColumn>();
                }
            }
        }

        #endregion

        #region SqlParameter

        private static SqlParameter GetSqlParameter(ProcedureParameter parameter, Database database)
        {
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = parameter.parameter_name;
            sqlParameter.Value = DBNull.Value;

            string data_type = (parameter.data_type ?? string.Empty).ToLower();

            // http://msdn.microsoft.com/en-us/library/cc716729.aspx
            switch (data_type)
            {
                case "bigint": sqlParameter.SqlDbType = SqlDbType.BigInt; break;
                case "binary": sqlParameter.SqlDbType = SqlDbType.VarBinary; break;
                case "bit": sqlParameter.SqlDbType = SqlDbType.Bit; break;
                case "char": sqlParameter.SqlDbType = SqlDbType.Char; break;
                case "date": sqlParameter.SqlDbType = SqlDbType.Date; break;
                case "datetime": sqlParameter.SqlDbType = SqlDbType.DateTime; break;
                case "datetime2": sqlParameter.SqlDbType = SqlDbType.DateTime2; break;
                case "datetimeoffset": sqlParameter.SqlDbType = SqlDbType.DateTimeOffset; break;
                case "decimal": sqlParameter.SqlDbType = SqlDbType.Decimal; break;
                case "filestream": sqlParameter.SqlDbType = SqlDbType.VarBinary; break;
                case "float": sqlParameter.SqlDbType = SqlDbType.Float; break;
                case "geography":
                    sqlParameter.SqlDbType = SqlDbType.Udt;
                    sqlParameter.UdtTypeName = "Geography";
                    break;
                case "geometry":
                    sqlParameter.SqlDbType = SqlDbType.Udt;
                    sqlParameter.UdtTypeName = "Geometry";
                    break;
                case "hierarchyid":
                    sqlParameter.SqlDbType = SqlDbType.Udt;
                    sqlParameter.UdtTypeName = "HierarchyId";
                    break;
                case "image": sqlParameter.SqlDbType = SqlDbType.Image; break;
                case "int": sqlParameter.SqlDbType = SqlDbType.Int; break;
                case "money": sqlParameter.SqlDbType = SqlDbType.Money; break;
                case "nchar": sqlParameter.SqlDbType = SqlDbType.NChar; break;
                case "ntext": sqlParameter.SqlDbType = SqlDbType.NText; break;
                case "numeric": sqlParameter.SqlDbType = SqlDbType.Decimal; break;
                case "nvarchar": sqlParameter.SqlDbType = SqlDbType.NVarChar; break;
                case "real": sqlParameter.SqlDbType = SqlDbType.Real; break;
                case "rowversion": sqlParameter.SqlDbType = SqlDbType.Timestamp; break;
                case "smalldatetime": sqlParameter.SqlDbType = SqlDbType.SmallDateTime; break;
                case "smallint": sqlParameter.SqlDbType = SqlDbType.SmallInt; break;
                case "smallmoney": sqlParameter.SqlDbType = SqlDbType.SmallMoney; break;
                case "sql_variant": sqlParameter.SqlDbType = SqlDbType.Variant; break;
                case "text": sqlParameter.SqlDbType = SqlDbType.Text; break;
                case "time": sqlParameter.SqlDbType = SqlDbType.Time; break;
                case "timestamp": sqlParameter.SqlDbType = SqlDbType.Timestamp; break;
                case "tinyint": sqlParameter.SqlDbType = SqlDbType.TinyInt; break;
                case "uniqueidentifier": sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier; break;
                case "varbinary": sqlParameter.SqlDbType = SqlDbType.VarBinary; break;
                case "varchar": sqlParameter.SqlDbType = SqlDbType.VarChar; break;
                case "xml": sqlParameter.SqlDbType = SqlDbType.Xml; break;
                default:
                    if (database.TVPs != null)
                    {
                        // could be more than one tvp with the same name but with different schema
                        // there's no way to differentiate between them
                        // beacuse the data type from the procedure parameter doesn't come with the schema name
                        TVP tvp = database.TVPs.Where(t => string.Compare(t.tvp_name, parameter.data_type, true) == 0).FirstOrDefault();
                        if (tvp != null)
                        {
                            sqlParameter.TypeName = parameter.data_type;
                            sqlParameter.SqlDbType = SqlDbType.Structured;
                            sqlParameter.Value = GetTVPDataTable(tvp);
                        }
                    }
                    break;
            }

            if (data_type == "binary" || data_type == "char" || data_type == "nchar" || data_type == "nvarchar" || data_type == "varbinary" || data_type == "varchar")
            {
                if (parameter.character_maximum_length == -1 || parameter.character_maximum_length > 0)
                    sqlParameter.Size = parameter.character_maximum_length.Value;
            }

            if (parameter.parameter_mode == "IN")
                sqlParameter.Direction = ParameterDirection.Input;
            else if (parameter.parameter_mode == "INOUT")
                sqlParameter.Direction = ParameterDirection.InputOutput;
            else if (parameter.parameter_mode == "OUT")
                sqlParameter.Direction = ParameterDirection.Output;

            return sqlParameter;
        }

        #endregion
    }
}
