using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using POCOGenerator.DbObject;

namespace POCOGenerator
{
    public partial class POCOGeneratorForm : Form
    {
        #region Form

        public POCOGeneratorForm(Server server, string initialCatalog, ConnectionForm connectionForm)
        {
            InitializeComponent();

            this.Server = server;
            this.InitialCatalog = initialCatalog;
            this.connectionForm = connectionForm;
        }

        private ConnectionForm connectionForm;

        private void POCOGeneratorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connectionForm.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            BuildServerTree();
        }

        #endregion

        #region Server Tree

        public Server Server { get; set; }
        public string InitialCatalog { get; set; }

        public enum NodeType
        {
            None,
            Server,
            Database,
            Tables,
            Table,
            Views,
            View,
            Columns,
            Column,
            Procedures,
            Procedure,
            Functions,
            Function,
            ProcedureParameters,
            ProcedureParameter,
            ProcedureColumns,
            ProcedureColumn,
            TVPs,
            TVP,
            TVPColumns,
            TVPColumn
        }

        public class NodeTag
        {
            public NodeType NodeType { get; set; }
            public IDbObject DbObject { get; set; }
        }

        public enum ImageType
        {
            Server,
            Database,
            Folder,
            Table,
            View,
            Procedure,
            Function,
            Column,
            PrimaryKey,
            ForeignKey
        }

        private void BuildServerTree()
        {
            try
            {
                TreeNode serverNode = BuildServerNode();
                trvServer.Nodes.Add(serverNode);
                Application.DoEvents();

                Database databaseCurrent = null;
                TreeNode databaseNodeCurrent = null;
                TreeNode tablesNode = null;
                TreeNode viewsNode = null;
                TreeNode proceduresNode = null;
                TreeNode functionsNode = null;
                TreeNode tvpsNode = null;

                DbHelper.BuildingDbObjectHandler buildingDbObject = delegate(IDbObject dbObject)
                {
                    if (dbObject is Database)
                    {
                        Database database = dbObject as Database;
                        TreeNode databaseNode = AddDatabaseNode(serverNode, database);

                        databaseCurrent = database;
                        databaseNodeCurrent = databaseNode;
                        tablesNode = null;
                        viewsNode = null;
                        proceduresNode = null;
                        functionsNode = null;
                        tvpsNode = null;
                    }

                    ShowBuildingStatus(dbObject);
                };

                DbHelper.BuiltDbObjectHandler builtDbObject = delegate(IDbObject dbObject)
                {
                    if (dbObject is Database)
                    {
                        Database database = dbObject as Database;
                        if (database.Errors.Count > 0)
                            databaseNodeCurrent.ForeColor = Color.Red;
                        toolStripStatusLabel.Text = string.Empty;
                        Application.DoEvents();
                    }
                    else if (dbObject is Table && (dbObject is POCOGenerator.DbObject.View) == false)
                    {
                        Table table = dbObject as Table;
                        tablesNode = AddTablesNode(tablesNode, databaseCurrent, databaseNodeCurrent);
                        AddTableNode(tablesNode, table);
                    }
                    else if (dbObject is POCOGenerator.DbObject.View)
                    {
                        POCOGenerator.DbObject.View view = dbObject as POCOGenerator.DbObject.View;
                        viewsNode = AddViewsNode(viewsNode, databaseCurrent, databaseNodeCurrent);
                        AddViewNode(viewsNode, view);
                    }
                    else if (dbObject is Procedure && (dbObject is Function) == false)
                    {
                        Procedure procedure = dbObject as Procedure;
                        proceduresNode = AddProceduresNode(proceduresNode, databaseCurrent, databaseNodeCurrent);
                        AddProcedureNode(proceduresNode, procedure);
                    }
                    else if (dbObject is Function)
                    {
                        Function function = dbObject as Function;
                        functionsNode = AddFunctionsNode(functionsNode, databaseCurrent, databaseNodeCurrent);
                        AddFunctionNode(functionsNode, function);
                    }
                    else if (dbObject is TVP)
                    {
                        TVP tvp = dbObject as TVP;
                        tvpsNode = AddTVPsNode(tvpsNode, databaseCurrent, databaseNodeCurrent);
                        AddTVPNode(tvpsNode, tvp);
                    }
                };

                DbHelper.BuildServerSchema(Server, InitialCatalog, buildingDbObject, builtDbObject);

                trvServer.SelectedNode = serverNode;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = "Error. " + ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : string.Empty);
                toolStripStatusLabel.ForeColor = Color.Red;
            }
        }

        private void ShowBuildingStatus(IDbObject dbObject)
        {
            if (dbObject is Table)
            {
                Table table = dbObject as Table;
                toolStripStatusLabel.Text = string.Format("{0}.{1}", table.Database.ToString(), dbObject.ToString());
            }
            else if (dbObject is Procedure)
            {
                Procedure procedure = dbObject as Procedure;
                toolStripStatusLabel.Text = string.Format("{0}.{1}", procedure.Database.ToString(), dbObject.ToString());
            }
            else if (dbObject is TVP)
            {
                TVP tvp = dbObject as TVP;
                toolStripStatusLabel.Text = string.Format("{0}.{1}", tvp.Database.ToString(), dbObject.ToString());
            }
            else
            {
                toolStripStatusLabel.Text = string.Format("{0}", dbObject.ToString());
            }
            toolStripStatusLabel.ForeColor = Color.Black;
            Application.DoEvents();
        }

        private TreeNode AddDatabaseNode(TreeNode serverNode, Database database)
        {
            TreeNode databaseNode = BuildDatabaseNode(database);
            serverNode.Nodes.AddSorted(databaseNode);
            serverNode.Expand();
            Application.DoEvents();
            return databaseNode;
        }

        private TreeNode AddTablesNode(TreeNode tablesNode, Database databaseCurrent, TreeNode databaseNodeCurrent)
        {
            if (tablesNode == null)
            {
                tablesNode = BuildTablesNode(databaseCurrent);
                databaseNodeCurrent.Nodes.Insert(0, tablesNode);
                Application.DoEvents();
            }
            return tablesNode;
        }

        private void AddTableNode(TreeNode tablesNode, Table table)
        {
            TreeNode tableNode = BuildTableNode(table);
            tablesNode.Nodes.AddSorted(tableNode);
            Application.DoEvents();
        }

        private TreeNode AddViewsNode(TreeNode viewsNode, Database databaseCurrent, TreeNode databaseNodeCurrent)
        {
            if (viewsNode == null)
            {
                viewsNode = BuildViewsNode(databaseCurrent);
                databaseNodeCurrent.Nodes.Insert(1, viewsNode);
                Application.DoEvents();
            }
            return viewsNode;
        }

        private void AddViewNode(TreeNode viewsNode, POCOGenerator.DbObject.View view)
        {
            TreeNode viewNode = BuildViewNode(view);
            viewsNode.Nodes.AddSorted(viewNode);
            Application.DoEvents();
        }

        private TreeNode AddProceduresNode(TreeNode proceduresNode, Database databaseCurrent, TreeNode databaseNodeCurrent)
        {
            if (proceduresNode == null)
            {
                proceduresNode = BuildProceduresNode(databaseCurrent);
                databaseNodeCurrent.Nodes.Insert(2, proceduresNode);
                Application.DoEvents();
            }
            return proceduresNode;
        }

        private void AddProcedureNode(TreeNode proceduresNode, Procedure procedure)
        {
            TreeNode procedureNode = BuildProcedureNode(procedure);
            proceduresNode.Nodes.AddSorted(procedureNode);
            Application.DoEvents();
        }

        private TreeNode AddFunctionsNode(TreeNode functionsNode, Database databaseCurrent, TreeNode databaseNodeCurrent)
        {
            if (functionsNode == null)
            {
                functionsNode = BuildFunctionsNode(databaseCurrent);
                databaseNodeCurrent.Nodes.Insert(3, functionsNode);
                Application.DoEvents();
            }
            return functionsNode;
        }

        private void AddFunctionNode(TreeNode functionsNode, Function function)
        {
            TreeNode functionNode = BuildFunctionNode(function);
            functionsNode.Nodes.AddSorted(functionNode);
            Application.DoEvents();
        }

        private TreeNode AddTVPsNode(TreeNode tvpsNode, Database databaseCurrent, TreeNode databaseNodeCurrent)
        {
            if (tvpsNode == null)
            {
                tvpsNode = BuildTVPsNode(databaseCurrent);
                databaseNodeCurrent.Nodes.Add(tvpsNode); // first one to be inserted
                Application.DoEvents();
            }
            return tvpsNode;
        }

        private void AddTVPNode(TreeNode tvpsNode, TVP tvp)
        {
            TreeNode tvpNode = BuildTVPNode(tvp);
            tvpsNode.Nodes.AddSorted(tvpNode);
            Application.DoEvents();
        }

        private TreeNode BuildServerNode()
        {
            string serverName = Server.ToString();
            if (string.IsNullOrEmpty(Server.Version) == false)
                serverName += string.Format(" (SQL Server {0})", Server.Version);
            TreeNode serverNode = new TreeNode(serverName);
            serverNode.Tag = new NodeTag() { NodeType = NodeType.Server, DbObject = Server };
            serverNode.ImageIndex = (int)ImageType.Server;
            serverNode.SelectedImageIndex = (int)ImageType.Server;
            return serverNode;
        }

        private TreeNode BuildDatabaseNode(Database database)
        {
            TreeNode databaseNode = new TreeNode(database.ToString());
            databaseNode.Tag = new NodeTag() { NodeType = NodeType.Database, DbObject = database };
            databaseNode.ImageIndex = (int)ImageType.Database;
            databaseNode.SelectedImageIndex = (int)ImageType.Database;
            return databaseNode;
        }

        private TreeNode BuildTablesNode(Database database)
        {
            TreeNode tablesNode = new TreeNode("Tables");
            tablesNode.Tag = new NodeTag() { NodeType = NodeType.Tables, DbObject = database };
            tablesNode.ImageIndex = (int)ImageType.Folder;
            tablesNode.SelectedImageIndex = (int)ImageType.Folder;
            return tablesNode;
        }

        private TreeNode BuildTableNode(Table table)
        {
            TreeNode tableNode = new TreeNode(table.ToString());
            tableNode.Tag = new NodeTag() { NodeType = NodeType.Table, DbObject = table };
            tableNode.ImageIndex = (int)ImageType.Table;
            tableNode.SelectedImageIndex = (int)ImageType.Table;

            TreeNode columnsNode = new TreeNode("Columns");
            columnsNode.Tag = new NodeTag() { NodeType = NodeType.Columns, DbObject = table };
            columnsNode.ImageIndex = (int)ImageType.Folder;
            columnsNode.SelectedImageIndex = (int)ImageType.Folder;
            tableNode.Nodes.Add(columnsNode);

            if (table.Columns != null)
            {
                foreach (Column column in table.Columns.OrderBy<Column, int>(c => c.ordinal_position ?? 0))
                {
                    TreeNode columnNode = new TreeNode(column.ToStringWithPKFK());
                    columnNode.Tag = new NodeTag() { NodeType = NodeType.Column, DbObject = column };
                    columnNode.ImageIndex = (int)(column.IsPrimaryKey ? ImageType.PrimaryKey : (column.IsForeignKey ? ImageType.ForeignKey : ImageType.Column));
                    columnNode.SelectedImageIndex = (int)(column.IsPrimaryKey ? ImageType.PrimaryKey : (column.IsForeignKey ? ImageType.ForeignKey : ImageType.Column));
                    columnsNode.Nodes.Add(columnNode);
                }
            }
            else if (table.Error != null)
            {
                tableNode.ForeColor = Color.Red;
            }

            return tableNode;
        }

        private TreeNode BuildViewsNode(Database database)
        {
            TreeNode viewsNode = new TreeNode("Views");
            viewsNode.Tag = new NodeTag() { NodeType = NodeType.Views, DbObject = database };
            viewsNode.ImageIndex = (int)ImageType.Folder;
            viewsNode.SelectedImageIndex = (int)ImageType.Folder;
            return viewsNode;
        }

        private TreeNode BuildViewNode(POCOGenerator.DbObject.View view)
        {
            TreeNode viewNode = new TreeNode(view.ToString());
            viewNode.Tag = new NodeTag() { NodeType = NodeType.View, DbObject = view };
            viewNode.ImageIndex = (int)ImageType.View;
            viewNode.SelectedImageIndex = (int)ImageType.View;

            TreeNode columnsNode = new TreeNode("Columns");
            columnsNode.Tag = new NodeTag() { NodeType = NodeType.Columns, DbObject = view };
            columnsNode.ImageIndex = (int)ImageType.Folder;
            columnsNode.SelectedImageIndex = (int)ImageType.Folder;
            viewNode.Nodes.Add(columnsNode);

            if (view.Columns != null)
            {
                foreach (Column column in view.Columns.OrderBy<Column, int>(c => c.ordinal_position ?? 0))
                {
                    TreeNode columnNode = new TreeNode(column.ToString());
                    columnNode.Tag = new NodeTag() { NodeType = NodeType.Column, DbObject = column };
                    columnNode.ImageIndex = (int)ImageType.Column;
                    columnNode.SelectedImageIndex = (int)ImageType.Column;
                    columnsNode.Nodes.Add(columnNode);
                }
            }
            else if (view.Error != null)
            {
                viewNode.ForeColor = Color.Red;
            }

            return viewNode;
        }

        private TreeNode BuildProceduresNode(Database database)
        {
            TreeNode proceduresNode = new TreeNode("Stored Procedures");
            proceduresNode.Tag = new NodeTag() { NodeType = NodeType.Procedures, DbObject = database };
            proceduresNode.ImageIndex = (int)ImageType.Folder;
            proceduresNode.SelectedImageIndex = (int)ImageType.Folder;
            return proceduresNode;
        }

        private TreeNode BuildProcedureNode(Procedure procedure)
        {
            TreeNode procedureNode = new TreeNode(procedure.ToString());
            procedureNode.Tag = new NodeTag() { NodeType = NodeType.Procedure, DbObject = procedure };
            procedureNode.ImageIndex = (int)ImageType.Procedure;
            procedureNode.SelectedImageIndex = (int)ImageType.Procedure;

            TreeNode parametersNode = new TreeNode("Parameters");
            parametersNode.Tag = new NodeTag() { NodeType = NodeType.ProcedureParameters, DbObject = procedure };
            parametersNode.ImageIndex = (int)ImageType.Folder;
            parametersNode.SelectedImageIndex = (int)ImageType.Folder;
            procedureNode.Nodes.Add(parametersNode);

            if (procedure.ProcedureParameters != null && procedure.ProcedureParameters.Count > 0)
            {
                foreach (ProcedureParameter parameter in procedure.ProcedureParameters.OrderBy<ProcedureParameter, int>(c => c.ordinal_position ?? 0))
                {
                    TreeNode parameterNode = new TreeNode(parameter.ToString());
                    parameterNode.Tag = new NodeTag() { NodeType = NodeType.ProcedureParameter, DbObject = parameter };
                    parameterNode.ImageIndex = (int)ImageType.Column;
                    parameterNode.SelectedImageIndex = (int)ImageType.Column;
                    parametersNode.Nodes.Add(parameterNode);
                }
            }

            TreeNode columnsNode = new TreeNode("Columns");
            columnsNode.Tag = new NodeTag() { NodeType = NodeType.ProcedureColumns, DbObject = procedure };
            columnsNode.ImageIndex = (int)ImageType.Folder;
            columnsNode.SelectedImageIndex = (int)ImageType.Folder;

            if (procedure.ProcedureColumns != null && procedure.ProcedureColumns.Count > 0)
            {
                procedureNode.Nodes.Add(columnsNode);

                foreach (ProcedureColumn column in procedure.ProcedureColumns.OrderBy<ProcedureColumn, int>(c => c.ColumnOrdinal ?? 0))
                {
                    TreeNode columnNode = new TreeNode(column.ToString());
                    columnNode.Tag = new NodeTag() { NodeType = NodeType.ProcedureColumn, DbObject = column };
                    columnNode.ImageIndex = (int)ImageType.Column;
                    columnNode.SelectedImageIndex = (int)ImageType.Column;
                    columnsNode.Nodes.Add(columnNode);
                }
            }
            else if (procedure.Error != null)
            {
                procedureNode.ForeColor = Color.Red;
            }

            return procedureNode;
        }

        private TreeNode BuildFunctionsNode(Database database)
        {
            TreeNode functionsNode = new TreeNode("Table-valued Functions");
            functionsNode.Tag = new NodeTag() { NodeType = NodeType.Functions, DbObject = database };
            functionsNode.ImageIndex = (int)ImageType.Folder;
            functionsNode.SelectedImageIndex = (int)ImageType.Folder;
            return functionsNode;
        }

        private TreeNode BuildFunctionNode(Function function)
        {
            TreeNode functionNode = new TreeNode(function.ToString());
            functionNode.Tag = new NodeTag() { NodeType = NodeType.Function, DbObject = function };
            functionNode.ImageIndex = (int)ImageType.Function;
            functionNode.SelectedImageIndex = (int)ImageType.Function;

            TreeNode parametersNode = new TreeNode("Parameters");
            parametersNode.Tag = new NodeTag() { NodeType = NodeType.ProcedureParameters, DbObject = function };
            parametersNode.ImageIndex = (int)ImageType.Folder;
            parametersNode.SelectedImageIndex = (int)ImageType.Folder;
            functionNode.Nodes.Add(parametersNode);

            if (function.ProcedureParameters != null && function.ProcedureParameters.Count > 0)
            {
                foreach (ProcedureParameter parameter in function.ProcedureParameters.OrderBy<ProcedureParameter, int>(c => c.ordinal_position ?? 0))
                {
                    TreeNode parameterNode = new TreeNode(parameter.ToString());
                    parameterNode.Tag = new NodeTag() { NodeType = NodeType.ProcedureParameter, DbObject = parameter };
                    parameterNode.ImageIndex = (int)ImageType.Column;
                    parameterNode.SelectedImageIndex = (int)ImageType.Column;
                    parametersNode.Nodes.Add(parameterNode);
                }
            }

            TreeNode columnsNode = new TreeNode("Columns");
            columnsNode.Tag = new NodeTag() { NodeType = NodeType.ProcedureColumns, DbObject = function };
            columnsNode.ImageIndex = (int)ImageType.Folder;
            columnsNode.SelectedImageIndex = (int)ImageType.Folder;

            if (function.ProcedureColumns != null && function.ProcedureColumns.Count > 0)
            {
                functionNode.Nodes.Add(columnsNode);

                foreach (ProcedureColumn column in function.ProcedureColumns.OrderBy<ProcedureColumn, int>(c => c.ColumnOrdinal ?? 0))
                {
                    TreeNode columnNode = new TreeNode(column.ToString());
                    columnNode.Tag = new NodeTag() { NodeType = NodeType.ProcedureColumn, DbObject = column };
                    columnNode.ImageIndex = (int)ImageType.Column;
                    columnNode.SelectedImageIndex = (int)ImageType.Column;
                    columnsNode.Nodes.Add(columnNode);
                }
            }
            else if (function.Error != null)
            {
                functionNode.ForeColor = Color.Red;
            }

            return functionNode;
        }

        private TreeNode BuildTVPsNode(Database database)
        {
            TreeNode tvpsNode = new TreeNode("User-Defined Table Types");
            tvpsNode.Tag = new NodeTag() { NodeType = NodeType.TVPs, DbObject = database };
            tvpsNode.ImageIndex = (int)ImageType.Folder;
            tvpsNode.SelectedImageIndex = (int)ImageType.Folder;
            return tvpsNode;
        }

        private TreeNode BuildTVPNode(TVP tvp)
        {
            TreeNode tvpNode = new TreeNode(tvp.ToString());
            tvpNode.Tag = new NodeTag() { NodeType = NodeType.TVP, DbObject = tvp };
            tvpNode.ImageIndex = (int)ImageType.Table;
            tvpNode.SelectedImageIndex = (int)ImageType.Table;

            TreeNode columnsNode = new TreeNode("Columns");
            columnsNode.Tag = new NodeTag() { NodeType = NodeType.TVPColumns, DbObject = tvp };
            columnsNode.ImageIndex = (int)ImageType.Folder;
            columnsNode.SelectedImageIndex = (int)ImageType.Folder;
            tvpNode.Nodes.Add(columnsNode);

            if (tvp.TVPColumns != null)
            {
                foreach (TVPColumn column in tvp.TVPColumns.OrderBy<TVPColumn, int>(c => c.column_id))
                {
                    TreeNode columnNode = new TreeNode(column.ToString());
                    columnNode.Tag = new NodeTag() { NodeType = NodeType.TVPColumn, DbObject = column };
                    columnNode.ImageIndex = (int)ImageType.Column;
                    columnNode.SelectedImageIndex = (int)ImageType.Column;
                    columnsNode.Nodes.Add(columnNode);
                }
            }
            else if (tvp.Error != null)
            {
                tvpNode.ForeColor = Color.Red;
            }

            return tvpNode;
        }

        #endregion

        #region Server Tree CheckBoxes

        private void trvServer_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            NodeType nodeType = ((NodeTag)e.Node.Tag).NodeType;

            bool isDrawCheckBox =
                nodeType == NodeType.Database ||
                nodeType == NodeType.Tables ||
                nodeType == NodeType.Views ||
                nodeType == NodeType.Procedures ||
                nodeType == NodeType.Functions ||
                nodeType == NodeType.TVPs ||
                nodeType == NodeType.Table ||
                nodeType == NodeType.View ||
                nodeType == NodeType.Procedure ||
                nodeType == NodeType.Function ||
                nodeType == NodeType.TVP;

            if (isDrawCheckBox == false)
                trvServer.HideCheckBox(e.Node);
            e.DrawDefault = true;
        }

        private void trvServer_AfterCheck(object sender, TreeViewEventArgs e)
        {
            trvServer.AfterCheck -= trvServer_AfterCheck;

            SetChildrenCheckBoxes(e.Node);

            TreeNode root = e.Node;
            while (root != null)
            {
                root.Checked = IsAllChildrenChecked(root);
                root = root.Parent;
            }

            trvServer.AfterCheck += trvServer_AfterCheck;
        }

        private void SetChildrenCheckBoxes(TreeNode root)
        {
            if (root != null)
            {
                bool isChecked = root.Checked;
                foreach (TreeNode node in root.Nodes)
                {
                    node.Checked = isChecked;
                    SetChildrenCheckBoxes(node);
                }
            }
        }

        private bool IsAllChildrenChecked(TreeNode root)
        {
            if (root != null)
            {
                foreach (TreeNode node in root.Nodes)
                {
                    if (node.Checked == false)
                        return false;
                    if (IsAllChildrenChecked(node) == false)
                        return false;
                }
            }

            return true;
        }

        #endregion

        #region Server Tree Context Menu

        private static FilterSettingsForm filterSettingsForm = new FilterSettingsForm();
        private static Dictionary<TreeNode, FilterSettings> filters = new Dictionary<TreeNode, FilterSettings>();
        private const string filteredPostfix = " (filtered)";

        private void trvServer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new Point(e.X, e.Y);

                TreeNode node = trvServer.GetNodeAt(point);
                if (node != null)
                {
                    trvServer.SelectedNode = node;

                    NodeType nodeType = ((NodeTag)node.Tag).NodeType;

                    bool isShowContextMenu = false;

                    if (nodeType == NodeType.Tables ||
                        nodeType == NodeType.Views ||
                        nodeType == NodeType.Procedures ||
                        nodeType == NodeType.Functions ||
                        nodeType == NodeType.TVPs)
                    {
                        isShowContextMenu = true;
                        removeFilterToolStripMenuItem.Visible = true;
                        filterSettingsToolStripMenuItem.Visible = true;
                        removeFilterToolStripMenuItem.Enabled = filters.ContainsKey(node);
                    }
                    else
                    {
                        removeFilterToolStripMenuItem.Visible = false;
                        filterSettingsToolStripMenuItem.Visible = false;
                    }

                    if (nodeType == NodeType.Table ||
                        nodeType == NodeType.View ||
                        nodeType == NodeType.Procedure ||
                        nodeType == NodeType.Function ||
                        nodeType == NodeType.TVP)
                    {
                        isShowContextMenu = true;
                        refreshToolStripMenuItem.Visible = true;
                    }
                    else
                    {
                        refreshToolStripMenuItem.Visible = false;
                    }

                    if (isShowContextMenu)
                        contextMenuServerTree.Show(trvServer, point);
                    else
                        contextMenuServerTree.Hide();
                }
                else
                {
                    contextMenuServerTree.Hide();
                }
            }
        }

        private void removeFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = trvServer.SelectedNode;
            if (node == null)
                return;

            if (filters.ContainsKey(node))
            {
                FilterSettings filterSettings = filters[node];
                foreach (TreeNode child in filterSettings.Nodes)
                    node.Nodes.AddSorted(child);
                filters.Remove(node);
                node.Text = node.Text.Replace(filteredPostfix, string.Empty);
            }
        }

        private void filterSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = trvServer.SelectedNode;
            if (node == null)
                return;

            bool isContains = filters.ContainsKey(node);
            if (isContains)
                filterSettingsForm.SetFilter(filters[node]);
            else
                filterSettingsForm.ClearFilter();

            DialogResult dialogResult = filterSettingsForm.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                FilterSettings filterSettings = filterSettingsForm.GetFilter();
                if (isContains)
                {
                    filterSettings.Nodes = filters[node].Nodes;
                    filters.Remove(node);
                }
                filters.Add(node, filterSettings);
                if (isContains == false)
                    node.Text += filteredPostfix;

                NodeType nodeType = ((NodeTag)node.Tag).NodeType;

                List<TreeNode> outList = new List<TreeNode>();
                List<TreeNode> inList = new List<TreeNode>();

                if (nodeType == NodeType.Tables || nodeType == NodeType.Views)
                {
                    foreach (TreeNode child in node.Nodes)
                    {
                        Table table = (Table)((NodeTag)child.Tag).DbObject;
                        bool isMatchFilter = IsMatchFilter(filterSettings, table.table_name, table.table_schema);
                        if (isMatchFilter == false)
                            outList.Add(child);
                    }

                    foreach (TreeNode child in filterSettings.Nodes)
                    {
                        Table table = (Table)((NodeTag)child.Tag).DbObject;
                        bool isMatchFilter = IsMatchFilter(filterSettings, table.table_name, table.table_schema);
                        if (isMatchFilter)
                            inList.Add(child);
                    }
                }
                else if (nodeType == NodeType.Procedures || nodeType == NodeType.Functions)
                {
                    foreach (TreeNode child in node.Nodes)
                    {
                        Procedure procedure = (Procedure)((NodeTag)child.Tag).DbObject;
                        bool isMatchFilter = IsMatchFilter(filterSettings, procedure.routine_name, procedure.routine_schema);
                        if (isMatchFilter == false)
                            outList.Add(child);
                    }

                    foreach (TreeNode child in filterSettings.Nodes)
                    {
                        Procedure procedure = (Procedure)((NodeTag)child.Tag).DbObject;
                        bool isMatchFilter = IsMatchFilter(filterSettings, procedure.routine_name, procedure.routine_schema);
                        if (isMatchFilter)
                            inList.Add(child);
                    }
                }
                else if (nodeType == NodeType.TVPs)
                {
                    foreach (TreeNode child in node.Nodes)
                    {
                        TVP tvp = (TVP)((NodeTag)child.Tag).DbObject;
                        bool isMatchFilter = IsMatchFilter(filterSettings, tvp.tvp_name, tvp.tvp_schema);
                        if (isMatchFilter == false)
                            outList.Add(child);
                    }

                    foreach (TreeNode child in filterSettings.Nodes)
                    {
                        TVP tvp = (TVP)((NodeTag)child.Tag).DbObject;
                        bool isMatchFilter = IsMatchFilter(filterSettings, tvp.tvp_name, tvp.tvp_schema);
                        if (isMatchFilter)
                            inList.Add(child);
                    }
                }

                foreach (TreeNode child in outList)
                {
                    node.Nodes.Remove(child);
                    filterSettings.Nodes.Add(child);
                }

                foreach (TreeNode child in inList)
                {
                    filterSettings.Nodes.Remove(child);
                    node.Nodes.AddSorted(child);
                }
            }
        }

        private bool IsMatchFilter(FilterSettings filterSettings, string name, string schema)
        {
            bool isMatchFilter = true;
            if (string.IsNullOrEmpty(filterSettings.FilterName.Filter) == false)
            {
                if (filterSettings.FilterName.FilterType == FilterType.Equals)
                    isMatchFilter = (string.Compare(name, filterSettings.FilterName.Filter, true) == 0);
                else if (filterSettings.FilterName.FilterType == FilterType.Contains)
                    isMatchFilter = (name.IndexOf(filterSettings.FilterName.Filter, StringComparison.CurrentCultureIgnoreCase) != -1);
                else if (filterSettings.FilterName.FilterType == FilterType.Does_Not_Contain)
                    isMatchFilter = (name.IndexOf(filterSettings.FilterName.Filter, StringComparison.CurrentCultureIgnoreCase) == -1);
            }

            if (isMatchFilter == false)
                return false;

            isMatchFilter = true;
            if (string.IsNullOrEmpty(filterSettings.FilterSchema.Filter) == false)
            {
                if (filterSettings.FilterSchema.FilterType == FilterType.Equals)
                    isMatchFilter = (string.Compare(schema, filterSettings.FilterSchema.Filter, true) == 0);
                else if (filterSettings.FilterSchema.FilterType == FilterType.Contains)
                    isMatchFilter = (schema.IndexOf(filterSettings.FilterSchema.Filter, StringComparison.CurrentCultureIgnoreCase) != -1);
                else if (filterSettings.FilterSchema.FilterType == FilterType.Does_Not_Contain)
                    isMatchFilter = (schema.IndexOf(filterSettings.FilterSchema.Filter, StringComparison.CurrentCultureIgnoreCase) == -1);
            }

            return isMatchFilter;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = trvServer.SelectedNode;
            if (node == null)
                return;

            NodeType nodeType = ((NodeTag)node.Tag).NodeType;

            if (nodeType == NodeType.Table)
            {
                Table table = (Table)((NodeTag)node.Tag).DbObject;
                DbHelper.GetTableSchema(table, delegate(IDbObject dbObject)
                {
                    table.Error = null;
                }, delegate(IDbObject dbObject)
                {
                    AddTableNode(node.Parent, table);
                    node.Remove();
                    WritePocoToEditor();
                });
            }
            else if (nodeType == NodeType.View)
            {
                POCOGenerator.DbObject.View view = (POCOGenerator.DbObject.View)((NodeTag)node.Tag).DbObject;
                DbHelper.GetViewSchema(view, delegate(IDbObject dbObject)
                {
                    view.Error = null;
                }, delegate(IDbObject dbObject)
                {
                    AddViewNode(node.Parent, view);
                    node.Remove();
                    WritePocoToEditor();
                });
            }
            else if (nodeType == NodeType.Procedure)
            {
                Procedure procedure = (Procedure)((NodeTag)node.Tag).DbObject;
                DbHelper.GetProcedureSchema(procedure, delegate(IDbObject dbObject)
                {
                    procedure.Error = null;
                }, delegate(IDbObject dbObject)
                {
                    AddProcedureNode(node.Parent, procedure);
                    node.Remove();
                    WritePocoToEditor();
                });
            }
            else if (nodeType == NodeType.Function)
            {
                Function function = (Function)((NodeTag)node.Tag).DbObject;
                DbHelper.GetFunctionSchema(function, delegate(IDbObject dbObject)
                {
                    function.Error = null;
                }, delegate(IDbObject dbObject)
                {
                    AddFunctionNode(node.Parent, function);
                    node.Remove();
                    WritePocoToEditor();
                });
            }
            else if (nodeType == NodeType.TVP)
            {
                TVP tvp = (TVP)((NodeTag)node.Tag).DbObject;
                DbHelper.GetTVPSchema(tvp, delegate(IDbObject dbObject)
                {
                    tvp.Error = null;
                }, delegate(IDbObject dbObject)
                {
                    AddTVPNode(node.Parent, tvp);
                    node.Remove();
                    WritePocoToEditor();
                });
            }
        }

        #endregion

        #region POCO Options

        public bool IsDataMemebers { get; set; }
        public bool IsVirtualProperties { get; set; }
        public bool IsPartialClass { get; set; }
        public bool IsAllStructNullable { get; set; }
        public bool IsComments { get; set; }
        public bool IsCommentsWithoutNull { get; set; }
        public bool IsUsing { get; set; }
        public string Namespace { get; set; }
        public bool IsSingular { get; set; }
        public bool IsIncludeDB { get; set; }
        public string DBSeparator { get; set; }
        public bool IsIncludeSchema { get; set; }
        public bool IsIgnoreDboSchema { get; set; }
        public string SchemaSeparator { get; set; }
        public string WordsSeparator { get; set; }
        public bool IsCamelCase { get; set; }
        public bool IsUpperCase { get; set; }
        public bool IsLowerCase { get; set; }
        public string Search { get; set; }
        public string Replace { get; set; }
        public bool IsIgnoreCase { get; set; }
        public string FixedClassName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public bool IsEFCodeFirst { get; set; }
        public bool IsEFCodeFirstColumn { get; set; }
        public bool IsEFCodeFirstRequired { get; set; }
        public bool IsPetaPoco { get; set; }
        public bool IsPetaPocoExplicitColumns { get; set; }

        private void rdbProperties_CheckedChanged(object sender, EventArgs e)
        {
            IsDataMemebers = false;
            WritePocoToEditor();
        }

        private void rdbDataMembers_CheckedChanged(object sender, EventArgs e)
        {
            IsDataMemebers = true;
            WritePocoToEditor();
        }

        private void chkVirtualProperties_CheckedChanged(object sender, EventArgs e)
        {
            IsVirtualProperties = chkVirtualProperties.Checked;
            WritePocoToEditor();
        }

        private void chkPartialClass_CheckedChanged(object sender, EventArgs e)
        {
            IsPartialClass = chkPartialClass.Checked;
            WritePocoToEditor();
        }

        private void chkAllStructNullable_CheckedChanged(object sender, EventArgs e)
        {
            IsAllStructNullable = chkAllStructNullable.Checked;
            WritePocoToEditor();
        }

        private void chkComments_CheckedChanged(object sender, EventArgs e)
        {
            IsComments = chkComments.Checked;
            if (IsComments == false && IsCommentsWithoutNull)
            {
                IsCommentsWithoutNull = false;
                chkCommentsWithoutNull.Checked = false;
            }
            WritePocoToEditor();
        }

        private void chkCommentsWithoutNull_CheckedChanged(object sender, EventArgs e)
        {
            IsCommentsWithoutNull = chkCommentsWithoutNull.Checked;
            if (IsCommentsWithoutNull && IsComments == false)
            {
                IsComments = true;
                chkComments.Checked = true;
            }
            WritePocoToEditor();
        }

        private void chkUsing_CheckedChanged(object sender, EventArgs e)
        {
            IsUsing = chkUsing.Checked;
            WritePocoToEditor();
        }

        private void txtNamespace_TextChanged(object sender, EventArgs e)
        {
            Namespace = txtNamespace.Text;
            WritePocoToEditor();
        }

        private void chkSingular_CheckedChanged(object sender, EventArgs e)
        {
            IsSingular = chkSingular.Checked;
            WritePocoToEditor();
        }

        private void chkIncludeDB_CheckedChanged(object sender, EventArgs e)
        {
            IsIncludeDB = chkIncludeDB.Checked;
            WritePocoToEditor();
        }

        private void txtDBSeparator_TextChanged(object sender, EventArgs e)
        {
            DBSeparator = txtDBSeparator.Text;
            WritePocoToEditor();
        }

        private void chkIncludeSchema_CheckedChanged(object sender, EventArgs e)
        {
            IsIncludeSchema = chkIncludeSchema.Checked;
            WritePocoToEditor();
        }

        private void chkIgnoreDboSchema_CheckedChanged(object sender, EventArgs e)
        {
            IsIgnoreDboSchema = chkIgnoreDboSchema.Checked;
            WritePocoToEditor();
        }

        private void txtSchemaSeparator_TextChanged(object sender, EventArgs e)
        {
            SchemaSeparator = txtSchemaSeparator.Text;
            WritePocoToEditor();
        }

        private void txtWordsSeparator_TextChanged(object sender, EventArgs e)
        {
            WordsSeparator = txtWordsSeparator.Text;
            WritePocoToEditor();
        }

        private void chkCamelCase_CheckedChanged(object sender, EventArgs e)
        {
            IsCamelCase = chkCamelCase.Checked;
            if (IsCamelCase)
            {
                IsUpperCase = false;
                IsLowerCase = false;
                chkUpperCase.Checked = false;
                chkLowerCase.Checked = false;
            }
            WritePocoToEditor();
        }

        private void chkUpperCase_CheckedChanged(object sender, EventArgs e)
        {
            IsUpperCase = chkUpperCase.Checked;
            if (IsUpperCase)
            {
                IsCamelCase = false;
                IsLowerCase = false;
                chkCamelCase.Checked = false;
                chkLowerCase.Checked = false;
            }
            WritePocoToEditor();
        }

        private void chkLowerCase_CheckedChanged(object sender, EventArgs e)
        {
            IsLowerCase = chkLowerCase.Checked;
            if (IsLowerCase)
            {
                IsCamelCase = false;
                IsUpperCase = false;
                chkCamelCase.Checked = false;
                chkUpperCase.Checked = false;
            }
            WritePocoToEditor();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Search = txtSearch.Text;
            WritePocoToEditor();
        }

        private void txtReplace_TextChanged(object sender, EventArgs e)
        {
            Replace = txtReplace.Text;
            WritePocoToEditor();
        }

        private void chkIgnoreCase_CheckedChanged(object sender, EventArgs e)
        {
            IsIgnoreCase = chkIgnoreCase.Checked;
            WritePocoToEditor();
        }

        private void txtFixedClassName_TextChanged(object sender, EventArgs e)
        {
            FixedClassName = txtFixedClassName.Text;
            WritePocoToEditor();
        }

        private void txtPrefix_TextChanged(object sender, EventArgs e)
        {
            Prefix = txtPrefix.Text;
            WritePocoToEditor();
        }

        private void txtSuffix_TextChanged(object sender, EventArgs e)
        {
            Suffix = txtSuffix.Text;
            WritePocoToEditor();
        }

        private void chkEF_CheckedChanged(object sender, EventArgs e)
        {
            IsEFCodeFirst = chkEFCodeFirst.Checked;
            if (IsEFCodeFirst == false && IsEFCodeFirstRequired)
            {
                IsEFCodeFirstRequired = false;
                chkEFCodeFirstRequired.Checked = false;
            }
            if (IsEFCodeFirst == false && IsEFCodeFirstColumn)
            {
                IsEFCodeFirstColumn = false;
                chkEFCodeFirstColumn.Checked = false;
            }
            if (IsEFCodeFirst && IsPetaPoco)
            {
                IsPetaPoco = false;
                IsPetaPocoExplicitColumns = false;
                chkPetaPoco.Checked = false;
                chkPetaPocoExplicitColumns.Checked = false;
            }
            WritePocoToEditor();
        }

        private void chkEFCodeFirstColumn_CheckedChanged(object sender, EventArgs e)
        {
            IsEFCodeFirstColumn = chkEFCodeFirstColumn.Checked;
            if (IsEFCodeFirstColumn && IsEFCodeFirst == false)
            {
                IsEFCodeFirst = true;
                chkEFCodeFirst.Checked = true;
            }
            if (IsEFCodeFirst && IsPetaPoco)
            {
                IsPetaPoco = false;
                IsPetaPocoExplicitColumns = false;
                chkPetaPoco.Checked = false;
                chkPetaPocoExplicitColumns.Checked = false;
            }
            WritePocoToEditor();
        }

        private void chkEFCodeFirstRequired_CheckedChanged(object sender, EventArgs e)
        {
            IsEFCodeFirstRequired = chkEFCodeFirstRequired.Checked;
            if (IsEFCodeFirstRequired && IsEFCodeFirst == false)
            {
                IsEFCodeFirst = true;
                chkEFCodeFirst.Checked = true;
            }
            if (IsEFCodeFirst && IsPetaPoco)
            {
                IsPetaPoco = false;
                IsPetaPocoExplicitColumns = false;
                chkPetaPoco.Checked = false;
                chkPetaPocoExplicitColumns.Checked = false;
            }
            WritePocoToEditor();
        }

        private void chkPetaPoco_CheckedChanged(object sender, EventArgs e)
        {
            IsPetaPoco = chkPetaPoco.Checked;
            if (IsPetaPoco == false && IsPetaPocoExplicitColumns)
            {
                IsPetaPocoExplicitColumns = false;
                chkPetaPocoExplicitColumns.Checked = false;
            }
            if (IsPetaPoco && IsEFCodeFirst)
            {
                IsEFCodeFirst = false;
                IsEFCodeFirstRequired = false;
                IsEFCodeFirstColumn = false;
                chkEFCodeFirst.Checked = false;
                chkEFCodeFirstRequired.Checked = false;
                chkEFCodeFirstColumn.Checked = false;
            }
            WritePocoToEditor();
        }

        private void chkPetaPocoExplicitColumns_CheckedChanged(object sender, EventArgs e)
        {
            IsPetaPocoExplicitColumns = chkPetaPocoExplicitColumns.Checked;
            if (IsPetaPocoExplicitColumns && IsPetaPoco == false)
            {
                IsPetaPoco = true;
                chkPetaPoco.Checked = true;
            }
            if (IsPetaPoco && IsEFCodeFirst)
            {
                IsEFCodeFirst = false;
                IsEFCodeFirstRequired = false;
                IsEFCodeFirstColumn = false;
                chkEFCodeFirst.Checked = false;
                chkEFCodeFirstRequired.Checked = false;
                chkEFCodeFirstColumn.Checked = false;
            }
            WritePocoToEditor();
        }

        #endregion

        #region POCO Editor

        private static readonly Color colorKeyword = Color.FromArgb(0, 0, 255);
        private static readonly Color colorUserType = Color.FromArgb(43, 145, 175);
        private static readonly Color colorString = Color.FromArgb(163, 21, 21);
        private static readonly Color colorComment = Color.FromArgb(0, 128, 0);
        private static readonly Color colorError = Color.FromArgb(255, 0, 0);

        private void trvServer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            WritePocoToEditor(e.Node, false, null);
        }

        private void WritePocoToEditor()
        {
            WritePocoToEditor(trvServer.SelectedNode, true, null);
        }

        private void WritePocoToEditor(TreeNode node, bool forceRefresh, StringBuilder poco)
        {
            NodeTag txtPocoEditorNodeTag = txtPocoEditor.Tag as NodeTag;
            if (txtPocoEditorNodeTag == null)
            {
                txtPocoEditorNodeTag = new NodeTag() { NodeType = NodeType.None };
                txtPocoEditor.Tag = txtPocoEditorNodeTag;
            }

            if (node == null)
            {
                if (poco == null)
                {
                    txtPocoEditor.Clear();
                    txtPocoEditorNodeTag.NodeType = NodeType.None;
                    txtPocoEditorNodeTag.DbObject = null;
                }
            }
            else
            {
                NodeType nodeType = ((NodeTag)node.Tag).NodeType;

                if (nodeType == NodeType.Table || nodeType == NodeType.View || nodeType == NodeType.Columns || nodeType == NodeType.Column)
                {
                    Table table = null;
                    if (nodeType == NodeType.Table || nodeType == NodeType.View || nodeType == NodeType.Columns)
                        table = (Table)((NodeTag)node.Tag).DbObject;
                    else if (nodeType == NodeType.Column)
                        table = ((Column)((NodeTag)node.Tag).DbObject).Table;

                    if (forceRefresh || txtPocoEditorNodeTag.DbObject != table)
                    {
                        WritePocoToEditor(table, poco);
                        if (poco == null)
                        {
                            txtPocoEditorNodeTag.NodeType = NodeType.Table;
                            txtPocoEditorNodeTag.DbObject = table;
                        }
                    }
                }
                else if (nodeType == NodeType.Procedure || nodeType == NodeType.Function || nodeType == NodeType.ProcedureParameters || nodeType == NodeType.ProcedureParameter || nodeType == NodeType.ProcedureColumns || nodeType == NodeType.ProcedureColumn)
                {
                    Procedure procedure = null;
                    if (nodeType == NodeType.Procedure || nodeType == NodeType.Function || nodeType == NodeType.ProcedureParameters || nodeType == NodeType.ProcedureColumns)
                        procedure = (Procedure)((NodeTag)node.Tag).DbObject;
                    else if (nodeType == NodeType.ProcedureParameter)
                        procedure = ((ProcedureParameter)((NodeTag)node.Tag).DbObject).Procedure;
                    else if (nodeType == NodeType.ProcedureColumn)
                        procedure = ((ProcedureColumn)((NodeTag)node.Tag).DbObject).Procedure;

                    if (forceRefresh || txtPocoEditorNodeTag.DbObject != procedure)
                    {
                        WritePocoToEditor(procedure, poco);
                        if (poco == null)
                        {
                            txtPocoEditorNodeTag.NodeType = NodeType.Procedure;
                            txtPocoEditorNodeTag.DbObject = procedure;
                        }
                    }
                }
                else if (nodeType == NodeType.TVP || nodeType == NodeType.TVPColumns || nodeType == NodeType.TVPColumn)
                {
                    TVP tvp = null;
                    if (nodeType == NodeType.TVP || nodeType == NodeType.TVPColumns)
                        tvp = (TVP)((NodeTag)node.Tag).DbObject;
                    else if (nodeType == NodeType.TVPColumn)
                        tvp = ((TVPColumn)((NodeTag)node.Tag).DbObject).TVP;

                    if (forceRefresh || txtPocoEditorNodeTag.DbObject != tvp)
                    {
                        WritePocoToEditor(tvp, poco);
                        if (poco == null)
                        {
                            txtPocoEditorNodeTag.NodeType = NodeType.TVP;
                            txtPocoEditorNodeTag.DbObject = tvp;
                        }
                    }
                }
                else
                {
                    if (poco == null)
                    {
                        txtPocoEditor.Clear();
                        txtPocoEditorNodeTag.NodeType = NodeType.None;
                        txtPocoEditorNodeTag.DbObject = null;

                        if (nodeType == NodeType.Database)
                        {
                            Database database = (Database)((NodeTag)node.Tag).DbObject;
                            if (database.Errors.Count > 0)
                                WriteErrorToEditor(database.Errors);
                        }
                    }
                }
            }
        }

        private void WritePocoToEditor(Table table, StringBuilder poco = null)
        {
            if (poco == null)
                txtPocoEditor.Clear();

            if (table == null)
                return;

            if (table.Error != null)
            {
                WriteErrorToEditor(table.Error, poco);
                return;
            }

            WriteUsingToEditor(table, poco);

            string namespaceOffset = WriteNamespaceStartToEditor(poco);

            if (IsPetaPoco && (table is POCOGenerator.DbObject.View) == false)
                WritePetaPocoToEditor(table, namespaceOffset, poco);

            if (IsEFCodeFirst && (table is POCOGenerator.DbObject.View) == false)
                WriteEFCodeFirstToEditor(table, namespaceOffset, poco);

            NodeType nodeType = (table is POCOGenerator.DbObject.View ? NodeType.View : NodeType.Table);
            table.ClassName = WriteClassStartToEditor(table.Database.ToString(), table.table_schema, table.table_name, nodeType, namespaceOffset, poco);

            if (table.Columns != null)
            {
                WriteColumnAttributesToEditorHandler WriteColumnAttributesToEditor = GetWriteColumnAttributesToEditorHandler(table, namespaceOffset);

                foreach (Column column in table.Columns.OrderBy<Column, int>(c => c.ordinal_position ?? 0))
                {
                    WriteColumnToEditor(column, column.column_name, column.DataTypeDisplay, column.PrecisionDisplay, column.IsNullable, namespaceOffset, poco, WriteColumnAttributesToEditor);
                }
            }

            WriteClassEndToEditor(namespaceOffset, poco);

            WriteNamespaceEndToEditor(poco);
        }

        private WriteColumnAttributesToEditorHandler GetWriteColumnAttributesToEditorHandler(Table table, string namespaceOffset)
        {
            WriteColumnAttributesToEditorHandler WriteColumnAttributesToEditor = null;

            if (IsPetaPoco && IsPetaPocoExplicitColumns && (table is POCOGenerator.DbObject.View) == false)
            {
                WriteColumnAttributesToEditor = delegate(IDbObject dbObject, StringBuilder poco1)
                {
                    if (poco1 == null)
                    {
                        txtPocoEditor.AppendText("[");
                        txtPocoEditor.AppendText("PetaPoco", colorUserType);
                        txtPocoEditor.AppendText(".");
                        txtPocoEditor.AppendText("Column", colorUserType);
                        txtPocoEditor.AppendText("] ");
                    }
                    else
                    {
                        poco1.Append("[PetaPoco.Column] ");
                    }
                };
            }
            else if (IsEFCodeFirst && (table is POCOGenerator.DbObject.View) == false)
            {
                var primaryKeys = table.Columns.Where(c => c.IsPrimaryKey);
                bool isCompositePrimaryKey = (primaryKeys.Count() > 1);

                WriteColumnAttributesToEditor = delegate(IDbObject dbObject, StringBuilder poco1)
                {
                    Column column = dbObject as Column;

                    if (column.IsPrimaryKey)
                    {
                        if (poco1 == null)
                        {
                            txtPocoEditor.AppendText("[");
                            txtPocoEditor.AppendText("Key", colorUserType);
                            txtPocoEditor.AppendText("]");
                            txtPocoEditor.AppendText(Environment.NewLine);
                            txtPocoEditor.AppendText(namespaceOffset);
                            txtPocoEditor.AppendText("    ");

                            if (isCompositePrimaryKey)
                            {
                                txtPocoEditor.AppendText("[");
                                txtPocoEditor.AppendText("Column", colorUserType);
                                txtPocoEditor.AppendText("(");

                                if (IsEFCodeFirstColumn)
                                {
                                    txtPocoEditor.AppendText("Name = ");
                                    txtPocoEditor.AppendText("\"", colorString);
                                    txtPocoEditor.AppendText(column.column_name, colorString);
                                    txtPocoEditor.AppendText("\"", colorString);
                                    txtPocoEditor.AppendText(", TypeName = ");
                                    txtPocoEditor.AppendText("\"", colorString);
                                    txtPocoEditor.AppendText(column.data_type, colorString);
                                    txtPocoEditor.AppendText("\"", colorString);
                                    txtPocoEditor.AppendText(", ");
                                }

                                txtPocoEditor.AppendText("Order = ");
                                txtPocoEditor.AppendText(column.PrimaryKey.Ordinal.ToString());

                                txtPocoEditor.AppendText(")]");
                                txtPocoEditor.AppendText(Environment.NewLine);
                                txtPocoEditor.AppendText(namespaceOffset);
                                txtPocoEditor.AppendText("    ");
                            }
                        }
                        else
                        {
                            poco1.AppendLine("[Key]");
                            poco1.Append(namespaceOffset);
                            poco1.Append("    ");

                            if (isCompositePrimaryKey)
                            {
                                poco1.Append("[Column(");

                                if (IsEFCodeFirstColumn)
                                {
                                    poco1.Append("Name = \"");
                                    poco1.Append(column.column_name);
                                    poco1.Append("\", TypeName = \"");
                                    poco1.Append(column.data_type);
                                    poco1.Append("\", ");
                                }

                                poco1.Append("Order = ");
                                poco1.Append(column.PrimaryKey.Ordinal.ToString());
                                poco1.AppendLine(")]");
                                poco1.Append(namespaceOffset);
                                poco1.Append("    ");
                            }
                        }
                    }

                    if (IsEFCodeFirstColumn && (column.IsPrimaryKey == false || isCompositePrimaryKey == false))
                    {
                        if (poco1 == null)
                        {
                            txtPocoEditor.AppendText("[");
                            txtPocoEditor.AppendText("Column", colorUserType);
                            txtPocoEditor.AppendText("(Name = ");
                            txtPocoEditor.AppendText("\"", colorString);
                            txtPocoEditor.AppendText(column.column_name, colorString);
                            txtPocoEditor.AppendText("\"", colorString);
                            txtPocoEditor.AppendText(", TypeName = ");
                            txtPocoEditor.AppendText("\"", colorString);
                            txtPocoEditor.AppendText(column.data_type, colorString);
                            txtPocoEditor.AppendText("\"", colorString);
                            txtPocoEditor.AppendText(")]");
                            txtPocoEditor.AppendText(Environment.NewLine);
                            txtPocoEditor.AppendText(namespaceOffset);
                            txtPocoEditor.AppendText("    ");
                        }
                        else
                        {
                            poco1.Append("[Column(Name = \"");
                            poco1.Append(column.column_name);
                            poco1.Append("\", TypeName = \"");
                            poco1.Append(column.data_type);
                            poco1.AppendLine("\")]");
                            poco1.Append(namespaceOffset);
                            poco1.Append("    ");
                        }
                    }

                    if (column.data_type == "binary" || column.data_type == "char" || column.data_type == "nchar" || column.data_type == "nvarchar" || column.data_type == "varbinary" || column.data_type == "varchar")
                    {
                        if (poco1 == null)
                        {
                            txtPocoEditor.AppendText("[");
                            txtPocoEditor.AppendText("MaxLength", colorUserType);
                            if (column.character_maximum_length > 0)
                            {
                                txtPocoEditor.AppendText("(");
                                txtPocoEditor.AppendText(column.character_maximum_length.ToString());
                                txtPocoEditor.AppendText(")");
                            }
                            txtPocoEditor.AppendText("]");
                            txtPocoEditor.AppendText(Environment.NewLine);
                            txtPocoEditor.AppendText(namespaceOffset);
                            txtPocoEditor.AppendText("    ");
                        }
                        else
                        {
                            poco1.Append("[MaxLength");
                            if (column.character_maximum_length > 0)
                            {
                                poco1.Append("(");
                                poco1.Append(column.character_maximum_length.ToString());
                                poco1.Append(")");
                            }
                            poco1.AppendLine("]");
                            poco1.Append(namespaceOffset);
                            poco1.Append("    ");
                        }
                    }

                    if (column.data_type == "timestamp")
                    {
                        if (poco1 == null)
                        {
                            txtPocoEditor.AppendText("[");
                            txtPocoEditor.AppendText("Timestamp", colorUserType);
                            txtPocoEditor.AppendText("]");
                            txtPocoEditor.AppendText(Environment.NewLine);
                            txtPocoEditor.AppendText(namespaceOffset);
                            txtPocoEditor.AppendText("    ");
                        }
                        else
                        {
                            poco1.AppendLine("[Timestamp]");
                            poco1.Append(namespaceOffset);
                            poco1.Append("    ");
                        }
                    }

                    if (IsEFCodeFirstRequired)
                    {
                        if (column.IsNullable == false)
                        {
                            if (poco1 == null)
                            {
                                txtPocoEditor.AppendText("[");
                                txtPocoEditor.AppendText("Required", colorUserType);
                                txtPocoEditor.AppendText("]");
                                txtPocoEditor.AppendText(Environment.NewLine);
                                txtPocoEditor.AppendText(namespaceOffset);
                                txtPocoEditor.AppendText("    ");
                            }
                            else
                            {
                                poco1.AppendLine("[Required]");
                                poco1.Append(namespaceOffset);
                                poco1.Append("    ");
                            }
                        }
                    }
                };
            }

            return WriteColumnAttributesToEditor;
        }

        private void WritePocoToEditor(Procedure procedure, StringBuilder poco = null)
        {
            if (poco == null)
                txtPocoEditor.Clear();

            if (procedure == null)
                return;

            if (procedure.Error != null)
            {
                WriteErrorToEditor(procedure.Error, poco);
                return;
            }

            WriteUsingToEditor(procedure, poco);

            string namespaceOffset = WriteNamespaceStartToEditor(poco);

            NodeType nodeType = (procedure is Function ? NodeType.Function : NodeType.Procedure);
            procedure.ClassName = WriteClassStartToEditor(procedure.Database.ToString(), procedure.routine_schema, procedure.routine_name, nodeType, namespaceOffset, poco);

            if (procedure.ProcedureColumns != null)
            {
                foreach (ProcedureColumn column in procedure.ProcedureColumns.OrderBy<ProcedureColumn, int>(c => c.ColumnOrdinal ?? 0))
                {
                    WriteColumnToEditor(column, column.ColumnName, column.DataTypeDisplay, column.PrecisionDisplay, column.IsNullable, namespaceOffset, poco);
                }
            }

            WriteClassEndToEditor(namespaceOffset, poco);

            WriteNamespaceEndToEditor(poco);
        }

        private void WritePocoToEditor(TVP tvp, StringBuilder poco = null)
        {
            if (poco == null)
                txtPocoEditor.Clear();

            if (tvp == null)
                return;

            if (tvp.Error != null)
            {
                WriteErrorToEditor(tvp.Error, poco);
                return;
            }

            WriteUsingToEditor(tvp, poco);

            string namespaceOffset = WriteNamespaceStartToEditor(poco);

            NodeType nodeType = NodeType.TVP;
            tvp.ClassName = WriteClassStartToEditor(tvp.Database.ToString(), tvp.tvp_schema, tvp.tvp_name, nodeType, namespaceOffset, poco);

            if (tvp.TVPColumns != null)
            {
                foreach (TVPColumn column in tvp.TVPColumns.OrderBy<TVPColumn, int>(c => c.column_id))
                {
                    WriteColumnToEditor(column, column.name, column.DataTypeDisplay, column.PrecisionDisplay, column.IsNullable, namespaceOffset, poco);
                }
            }

            WriteClassEndToEditor(namespaceOffset, poco);

            WriteNamespaceEndToEditor(poco);
        }

        private void WriteUsingToEditor(IDbObject dbObject, StringBuilder poco = null)
        {
            if (IsUsing)
            {
                bool isSpecialSQLTypes = IsSpecialSQLTypes(dbObject);

                if (poco == null)
                {
                    txtPocoEditor.AppendText("using", colorKeyword);
                    txtPocoEditor.AppendText(" System;");
                    txtPocoEditor.AppendText(Environment.NewLine);

                    if (isSpecialSQLTypes)
                    {
                        txtPocoEditor.AppendText("using", colorKeyword);
                        txtPocoEditor.AppendText(" Microsoft.SqlServer.Types;");
                        txtPocoEditor.AppendText(Environment.NewLine);
                    }

                    if (IsEFCodeFirst && (dbObject is Table) && (dbObject is POCOGenerator.DbObject.View) == false)
                    {
                        txtPocoEditor.AppendText("using", colorKeyword);
                        txtPocoEditor.AppendText(" System.ComponentModel.DataAnnotations;");
                        txtPocoEditor.AppendText(Environment.NewLine);
                    }

                    txtPocoEditor.AppendText(Environment.NewLine);
                }
                else
                {
                    poco.AppendLine("using System;");
                    if (isSpecialSQLTypes)
                        poco.AppendLine("using Microsoft.SqlServer.Types;");
                    if (IsEFCodeFirst && (dbObject is Table) && (dbObject is POCOGenerator.DbObject.View) == false)
                        poco.AppendLine("using System.ComponentModel.DataAnnotations;");
                    poco.AppendLine();
                }
            }
        }

        private bool IsSpecialSQLTypes(IDbObject dbObject)
        {
            if (dbObject is Table)
            {
                Table table = (Table)dbObject;
                if (table.Columns != null)
                {
                    foreach (Column column in table.Columns)
                    {
                        string data_type = (column.data_type ?? string.Empty).ToLower();
                        if (data_type.Contains("geography") || data_type.Contains("geometry") || data_type.Contains("hierarchyid"))
                            return true;
                    }
                }
            }
            else if (dbObject is Procedure)
            {
                Procedure procedure = (Procedure)dbObject;
                if (procedure.ProcedureColumns != null)
                {
                    foreach (ProcedureColumn column in procedure.ProcedureColumns)
                    {
                        string data_type = (column.DataTypeName ?? string.Empty).ToLower();
                        if (data_type.Contains("geography") || data_type.Contains("geometry") || data_type.Contains("hierarchyid"))
                            return true;
                    }
                }
            }
            else if (dbObject is TVP)
            {
                TVP tvp = (TVP)dbObject;
                if (tvp.TVPColumns != null)
                {
                    foreach (TVPColumn column in tvp.TVPColumns)
                    {
                        string data_type = (column.data_type ?? string.Empty).ToLower();
                        if (data_type.Contains("geography") || data_type.Contains("geometry") || data_type.Contains("hierarchyid"))
                            return true;
                    }
                }
            }

            return false;
        }

        private string WriteNamespaceStartToEditor(StringBuilder poco = null)
        {
            string namespaceOffset = string.Empty;
            if (string.IsNullOrEmpty(Namespace) == false)
            {
                if (poco == null)
                {
                    txtPocoEditor.AppendText("namespace", colorKeyword);
                    txtPocoEditor.AppendText(" ");
                    txtPocoEditor.AppendText(Namespace);
                    txtPocoEditor.AppendText(Environment.NewLine);
                    txtPocoEditor.AppendText("{");
                    txtPocoEditor.AppendText(Environment.NewLine);
                }
                else
                {
                    poco.Append("namespace ");
                    poco.AppendLine(Namespace);
                    poco.AppendLine("{");
                }

                namespaceOffset = "    ";
            }
            return namespaceOffset;
        }

        private void WriteNamespaceEndToEditor(StringBuilder poco = null)
        {
            if (string.IsNullOrEmpty(Namespace) == false)
            {
                if (poco == null)
                {
                    txtPocoEditor.AppendText("}");
                    txtPocoEditor.AppendText(Environment.NewLine);
                }
                else
                {
                    poco.AppendLine("}");
                }
            }
        }

        private void WritePetaPocoToEditor(Table table, string namespaceOffset, StringBuilder poco)
        {
            string primaryKeysStr = null;
            bool autoIncrement = true;
            var primaryKeys = table.Columns.Where(c => c.IsPrimaryKey).OrderBy(pk => pk.PrimaryKey.Ordinal);
            if (primaryKeys.Count() > 0)
            {
                primaryKeysStr = primaryKeys.Select(pk => pk.column_name).Aggregate((current, next) => current + "," + next);
                autoIncrement = primaryKeys.Select(pk => pk.PrimaryKey.Is_Identity).Aggregate((current, next) => current && next);
            }

            if (poco == null)
            {
                txtPocoEditor.AppendText(namespaceOffset);
                txtPocoEditor.AppendText("[");
                txtPocoEditor.AppendText("PetaPoco", colorUserType);
                txtPocoEditor.AppendText(".");
                txtPocoEditor.AppendText("TableName", colorUserType);
                txtPocoEditor.AppendText("(");
                txtPocoEditor.AppendText("\"", colorString);
                if (table.table_schema != "dbo")
                {
                    txtPocoEditor.AppendText(table.table_schema, colorString);
                    txtPocoEditor.AppendText(".", colorString);
                }
                txtPocoEditor.AppendText(table.table_name, colorString);
                txtPocoEditor.AppendText("\"", colorString);
                txtPocoEditor.AppendText(")]");
                txtPocoEditor.AppendText(Environment.NewLine);

                if (string.IsNullOrEmpty(primaryKeysStr) == false)
                {
                    txtPocoEditor.AppendText(namespaceOffset);
                    txtPocoEditor.AppendText("[");
                    txtPocoEditor.AppendText("PetaPoco", colorUserType);
                    txtPocoEditor.AppendText(".");
                    txtPocoEditor.AppendText("PrimaryKey", colorUserType);
                    txtPocoEditor.AppendText("(");
                    txtPocoEditor.AppendText("\"", colorString);
                    txtPocoEditor.AppendText(primaryKeysStr, colorString);
                    txtPocoEditor.AppendText("\"", colorString);
                    if (autoIncrement == false)
                    {
                        txtPocoEditor.AppendText(", autoIncrement=");
                        txtPocoEditor.AppendText("false", colorKeyword);
                    }
                    txtPocoEditor.AppendText(")]");
                    txtPocoEditor.AppendText(Environment.NewLine);
                }

                if (IsPetaPocoExplicitColumns)
                {
                    txtPocoEditor.AppendText(namespaceOffset);
                    txtPocoEditor.AppendText("[");
                    txtPocoEditor.AppendText("PetaPoco", colorUserType);
                    txtPocoEditor.AppendText(".");
                    txtPocoEditor.AppendText("ExplicitColumns", colorUserType);
                    txtPocoEditor.AppendText("]");
                    txtPocoEditor.AppendText(Environment.NewLine);
                }
            }
            else
            {
                poco.Append(namespaceOffset);
                poco.Append("[PetaPoco.TableName(\"");
                if (table.table_schema != "dbo")
                {
                    poco.Append(table.table_schema);
                    poco.Append(".");
                }
                poco.Append(table.table_name);
                poco.AppendLine("\")]");

                if (string.IsNullOrEmpty(primaryKeysStr) == false)
                {
                    poco.Append(namespaceOffset);
                    poco.Append("[PetaPoco.PrimaryKey(\"");
                    poco.Append(primaryKeysStr);
                    poco.Append("\"");
                    if (autoIncrement == false)
                        poco.Append(", autoIncrement=false");
                    poco.AppendLine(")]");
                }

                if (IsPetaPocoExplicitColumns)
                {
                    poco.Append(namespaceOffset);
                    poco.AppendLine("[PetaPoco.ExplicitColumns]");
                }
            }
        }

        private void WriteEFCodeFirstToEditor(Table table, string namespaceOffset, StringBuilder poco)
        {
            if (poco == null)
            {
                txtPocoEditor.AppendText(namespaceOffset);
                txtPocoEditor.AppendText("[");
                txtPocoEditor.AppendText("Table", colorUserType);
                txtPocoEditor.AppendText("(");
                txtPocoEditor.AppendText("\"", colorString);
                if (table.table_schema != "dbo")
                {
                    txtPocoEditor.AppendText(table.table_schema, colorString);
                    txtPocoEditor.AppendText(".", colorString);
                }
                txtPocoEditor.AppendText(table.table_name, colorString);
                txtPocoEditor.AppendText("\"", colorString);
                txtPocoEditor.AppendText(")]");
                txtPocoEditor.AppendText(Environment.NewLine);
            }
            else
            {
                poco.Append(namespaceOffset);
                poco.Append("[Table(\"");
                if (table.table_schema != "dbo")
                {
                    poco.Append(table.table_schema);
                    poco.Append(".");
                }
                poco.Append(table.table_name);
                poco.AppendLine("\")]");
            }
        }

        private string WriteClassStartToEditor(string dbName, string schema, string name, NodeType nodeType, string namespaceOffset, StringBuilder poco = null)
        {
            string className = GetClassName(dbName, schema, name, nodeType);

            if (poco == null)
            {
                txtPocoEditor.AppendText(namespaceOffset);
                txtPocoEditor.AppendText("public", colorKeyword);
                txtPocoEditor.AppendText(" ");
                if (IsPartialClass)
                {
                    txtPocoEditor.AppendText("partial", colorKeyword);
                    txtPocoEditor.AppendText(" ");
                }
                txtPocoEditor.AppendText("class", colorKeyword);
                txtPocoEditor.AppendText(" ");
                txtPocoEditor.AppendText(className, colorUserType);
                txtPocoEditor.AppendText(Environment.NewLine);

                txtPocoEditor.AppendText(namespaceOffset);
                txtPocoEditor.AppendText("{");
                txtPocoEditor.AppendText(Environment.NewLine);
            }
            else
            {
                poco.Append(namespaceOffset);
                poco.Append("public ");
                if (IsPartialClass)
                    poco.Append("partial ");
                poco.Append("class ");
                poco.AppendLine(className);
                poco.Append(namespaceOffset);
                poco.AppendLine("{");
            }

            return className;
        }

        private void WriteClassEndToEditor(string namespaceOffset, StringBuilder poco = null)
        {
            if (poco == null)
            {
                txtPocoEditor.AppendText(namespaceOffset);
                txtPocoEditor.AppendText("}");
                txtPocoEditor.AppendText(Environment.NewLine);
            }
            else
            {
                poco.Append(namespaceOffset);
                poco.AppendLine("}");
            }
        }

        private string GetClassName(string dbName, string schema, string name, NodeType nodeType)
        {
            string className = null;

            // prefix
            if (string.IsNullOrEmpty(Prefix) == false)
                className += Prefix;

            if (string.IsNullOrEmpty(FixedClassName))
            {
                if (IsIncludeDB)
                {
                    // db
                    if (IsCamelCase || string.IsNullOrEmpty(WordsSeparator) == false)
                        className += GetCamelCase(dbName);
                    else if (IsUpperCase)
                        className += dbName.ToUpper();
                    else if (IsLowerCase)
                        className += dbName.ToLower();
                    else
                        className += dbName;

                    // db separator
                    if (string.IsNullOrEmpty(DBSeparator) == false)
                        className += DBSeparator;
                }

                if (IsIncludeSchema)
                {
                    if (IsIgnoreDboSchema == false || schema != "dbo")
                    {
                        // schema
                        if (IsCamelCase || string.IsNullOrEmpty(WordsSeparator) == false)
                            className += GetCamelCase(schema);
                        else if (IsUpperCase)
                            className += schema.ToUpper();
                        else if (IsLowerCase)
                            className += schema.ToLower();
                        else
                            className += schema;

                        // schema separator
                        if (string.IsNullOrEmpty(SchemaSeparator) == false)
                            className += SchemaSeparator;
                    }
                }

                // name
                if (IsSingular)
                    if (nodeType == NodeType.Table || nodeType == NodeType.View || nodeType == NodeType.TVP)
                        name = GetSingularName(name);

                if (IsCamelCase || string.IsNullOrEmpty(WordsSeparator) == false)
                    className += GetCamelCase(name);
                else if (IsUpperCase)
                    className += name.ToUpper();
                else if (IsLowerCase)
                    className += name.ToLower();
                else
                    className += name;

                if (string.IsNullOrEmpty(Search) == false)
                {
                    if (IsIgnoreCase)
                        className = Regex.Replace(className, Search, Replace ?? string.Empty, RegexOptions.IgnoreCase);
                    else
                        className = className.Replace(Search, Replace ?? string.Empty);
                }
            }
            else
            {
                // fixed name
                className += FixedClassName;
            }

            // postfix
            if (string.IsNullOrEmpty(Suffix) == false)
                className += Suffix;

            return className;
        }

        #region IrregularNouns

        private static readonly Dictionary<string, string> IrregularNouns = new Dictionary<string, string>() { 
            { "aircraft", "aircraft" },
            { "alias", "alias" },
            { "analyses", "analysis" },
            { "axes", "axis" },
            { "bases", "basis" },
            { "cacti", "cactus" },
            { "children", "child" },
            { "crises", "crisis" },
            { "criteria", "criterion" },
            { "data", "datum" },
            { "deer", "deer" },
            { "diagnoses", "diagnosis" },
            { "feet", "foot" },
            { "fish", "fish" },
            { "foci", "focus" },
            { "fungi", "fungus" },
            { "geese", "goose" },
            { "indices", "index" },
            { "lice", "louse" },
            { "matrices", "matrix" },
            { "men", "man" },
            { "mice", "mouse" },
            { "movies", "movie" },
            { "news", "news" },
            { "nuclei", "nucleus" },
            { "oases", "oasis" },
            { "octopi", "octopus" },
            { "parentheses", "parenthesis" },
            { "people", "person" },
            { "phenomena", "phenomenon" },
            { "potatoes", "potato" },
            { "prognoses", "prognosis" },
            { "series", "series" },
            { "sheep", "sheep" },
            { "shoes", "shoe" },
            { "species", "species" },
            { "status", "status" },
            { "syllabi", "syllabus" },
            { "syllabuses", "syllabus" },
            { "synopses", "synopsis" },
            { "teeth", "tooth" },
            { "testes", "testis" },
            { "theses", "thesis" },
            { "tomatoes", "tomato" },
            { "vertices", "vertex" },
            { "viri", "virus" },
            { "women", "woman" }
        };

        #endregion

        private string GetSingularName(string name)
        {
            string nameLower = name.ToLower();
            foreach (string noun in IrregularNouns.Keys)
            {
                if (nameLower.EndsWith(noun))
                {
                    int index = name.Length - noun.Length;
                    string nounWord = name.Substring(index);
                    bool isCapital = ('A' <= nounWord[0] && nounWord[0] <= 'Z');
                    string singularNoun = IrregularNouns[noun];
                    if (isCapital)
                        singularNoun = singularNoun.Substring(0, 1).ToUpper() + singularNoun.Substring(1);
                    string singular = name.Substring(0, index) + singularNoun;
                    return singular;
                }
            }

            if (nameLower.EndsWith("oes") || nameLower.EndsWith("xes") || nameLower.EndsWith("zes") || nameLower.EndsWith("ches") || nameLower.EndsWith("shes") || nameLower.EndsWith("sses"))
                return name.Substring(0, name.Length - 2);

            if (nameLower.EndsWith("ies"))
            {
                if (!nameLower.EndsWith("aies") && !nameLower.EndsWith("eies") && !nameLower.EndsWith("iies") && !nameLower.EndsWith("oies") && (!nameLower.EndsWith("uies") || nameLower.EndsWith("quies")) && !nameLower.EndsWith("yies"))
                {
                    bool isLower = (string.Compare(name.Substring(name.Length - 3, 3), nameLower.Substring(name.Length - 3, 3), false) == 0);
                    return name.Substring(0, name.Length - 3) + (isLower ? "y" : "Y");
                }
            }

            if (nameLower.EndsWith("lves") || nameLower.EndsWith("rves"))
            {
                bool isLower = (string.Compare(name.Substring(name.Length - 4, 4), nameLower.Substring(name.Length - 4, 4), false) == 0);
                return name.Substring(0, name.Length - 3) + (isLower ? "f" : "F");
            }

            if (nameLower.EndsWith("tives") || nameLower.EndsWith("hives"))
                return name.Substring(0, name.Length - 1);

            if (nameLower.EndsWith("ves"))
            {
                if (!nameLower.EndsWith("fves"))
                {
                    bool isLower = (string.Compare(name.Substring(name.Length - 3, 3), nameLower.Substring(name.Length - 3, 3), false) == 0);
                    return name.Substring(0, name.Length - 3) + (isLower ? "fe" : "FE");
                }
            }

            if (nameLower.EndsWith("nalyses"))
            {
                if (!nameLower.EndsWith("analyses"))
                {
                    bool isLower = (string.Compare(name.Substring(name.Length - 7, 7), nameLower.Substring(name.Length - 7, 7), false) == 0);
                    return name.Substring(0, name.Length - 3) + (isLower ? "sis" : "SIS");
                }
            }

            if (nameLower.EndsWith("ta") || nameLower.EndsWith("ia"))
            {
                bool isLower = (string.Compare(name.Substring(name.Length - 2, 2), nameLower.Substring(name.Length - 2, 2), false) == 0);
                return name.Substring(0, name.Length - 1) + (isLower ? "um" : "UM");
            }

            if (nameLower.EndsWith("s"))
                if (!nameLower.EndsWith("ss"))
                    return name.Substring(0, name.Length - 1);

            return name;
        }

        private static readonly Regex regexCamelCase = new Regex("(?<word>[A-Z]{2,}|[A-Z][^A-Z]*?|^[^A-Z]*?)(?=[A-Z]|$)", RegexOptions.Compiled);

        private string GetCamelCase(string name)
        {
            List<string> camelCaseWords = new List<string>();

            string[] words = name.Split('_');
            foreach (string word in words)
            {
                foreach (Match match in regexCamelCase.Matches(word))
                {
                    camelCaseWords.Add(match.Groups["word"].Value);
                }
            }

            name = null;
            int index = 0;
            foreach (string word in camelCaseWords)
            {
                if (IsCamelCase)
                    name += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
                else if (IsUpperCase)
                    name += word.ToUpper();
                else if (IsLowerCase)
                    name += word.ToLower();
                else
                    name += word;

                index++;
                if (index < camelCaseWords.Count)
                    name += WordsSeparator;
            }

            return name;
        }

        private delegate void WriteColumnAttributesToEditorHandler(IDbObject dbObject, StringBuilder poco);

        private void WriteColumnToEditor(IDbObject dbObject, string columnName, string dataType, string precision, bool isNullable, string namespaceOffset, StringBuilder poco = null, WriteColumnAttributesToEditorHandler WriteColumnAttributesToEditor = null)
        {
            if (poco == null)
            {
                txtPocoEditor.AppendText(namespaceOffset);
                txtPocoEditor.AppendText("    ");
                if (WriteColumnAttributesToEditor != null)
                    WriteColumnAttributesToEditor(dbObject, poco);
                txtPocoEditor.AppendText("public", colorKeyword);
                txtPocoEditor.AppendText(" ");
            }
            else
            {
                poco.Append(namespaceOffset);
                poco.Append("    ");
                if (WriteColumnAttributesToEditor != null)
                    WriteColumnAttributesToEditor(dbObject, poco);
                poco.Append("public ");
            }

            if (IsDataMemebers == false && IsVirtualProperties)
            {
                if (poco == null)
                {
                    txtPocoEditor.AppendText("virtual", colorKeyword);
                    txtPocoEditor.AppendText(" ");
                }
                else
                {
                    poco.Append("virtual ");
                }
            }

            switch ((dataType ?? string.Empty).ToLower())
            {
                case "bigint":
                    if (poco == null)
                        txtPocoEditor.AppendText("long", colorKeyword);
                    else
                        poco.Append("long");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "binary":
                    if (poco == null)
                    {
                        txtPocoEditor.AppendText("byte", colorKeyword);
                        txtPocoEditor.AppendText("[]");
                    }
                    else
                    {
                        poco.Append("byte[]");
                    }
                    break;

                case "bit":
                    if (poco == null)
                        txtPocoEditor.AppendText("bool", colorKeyword);
                    else
                        poco.Append("bool");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "char":
                    if (poco == null)
                        txtPocoEditor.AppendText("string", colorKeyword);
                    else
                        poco.Append("string");
                    break;

                case "date":
                    if (poco == null)
                        txtPocoEditor.AppendText("DateTime", colorUserType);
                    else
                        poco.Append("DateTime");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "datetime":
                    if (poco == null)
                        txtPocoEditor.AppendText("DateTime", colorUserType);
                    else
                        poco.Append("DateTime");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "datetime2":
                    if (poco == null)
                        txtPocoEditor.AppendText("DateTime", colorUserType);
                    else
                        poco.Append("DateTime");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "datetimeoffset":
                    if (poco == null)
                        txtPocoEditor.AppendText("DateTimeOffset", colorUserType);
                    else
                        poco.Append("DateTimeOffset");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "decimal":
                    if (poco == null)
                        txtPocoEditor.AppendText("decimal", colorKeyword);
                    else
                        poco.Append("decimal");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "filestream":
                    if (poco == null)
                    {
                        txtPocoEditor.AppendText("byte", colorKeyword);
                        txtPocoEditor.AppendText("[]");
                    }
                    else
                    {
                        poco.Append("byte[]");
                    }
                    break;

                case "float":
                    if (poco == null)
                        txtPocoEditor.AppendText("double", colorKeyword);
                    else
                        poco.Append("double");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "geography":
                    if (poco == null)
                    {
                        if (IsUsing == false)
                            txtPocoEditor.AppendText("Microsoft.SqlServer.Types.");
                        txtPocoEditor.AppendText("SqlGeography", colorUserType);
                    }
                    else
                    {
                        if (IsUsing == false)
                            poco.Append("Microsoft.SqlServer.Types.");
                        poco.Append("SqlGeography");
                    }
                    break;

                case "geometry":
                    if (poco == null)
                    {
                        if (IsUsing == false)
                            txtPocoEditor.AppendText("Microsoft.SqlServer.Types.");
                        txtPocoEditor.AppendText("SqlGeometry", colorUserType);
                    }
                    else
                    {
                        if (IsUsing == false)
                            poco.Append("Microsoft.SqlServer.Types.");
                        poco.Append("SqlGeometry");
                    }
                    break;

                case "hierarchyid":
                    if (poco == null)
                    {
                        if (IsUsing == false)
                            txtPocoEditor.AppendText("Microsoft.SqlServer.Types.");
                        txtPocoEditor.AppendText("SqlHierarchyId", colorUserType);
                    }
                    else
                    {
                        if (IsUsing == false)
                            poco.Append("Microsoft.SqlServer.Types.");
                        poco.Append("SqlHierarchyId");
                    }
                    break;

                case "image":
                    if (poco == null)
                    {
                        txtPocoEditor.AppendText("byte", colorKeyword);
                        txtPocoEditor.AppendText("[]");
                    }
                    else
                    {
                        poco.Append("byte[]");
                    }
                    break;

                case "int":
                    if (poco == null)
                        txtPocoEditor.AppendText("int", colorKeyword);
                    else
                        poco.Append("int");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "money":
                    if (poco == null)
                        txtPocoEditor.AppendText("decimal", colorKeyword);
                    else
                        poco.Append("decimal");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "nchar":
                    if (poco == null)
                        txtPocoEditor.AppendText("string", colorKeyword);
                    else
                        poco.Append("string");
                    break;

                case "ntext":
                    if (poco == null)
                        txtPocoEditor.AppendText("string", colorKeyword);
                    else
                        poco.Append("string");
                    break;

                case "numeric":
                    if (poco == null)
                        txtPocoEditor.AppendText("decimal", colorKeyword);
                    else
                        poco.Append("decimal");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "nvarchar":
                    if (poco == null)
                        txtPocoEditor.AppendText("string", colorKeyword);
                    else
                        poco.Append("string");
                    break;

                case "real":
                    if (poco == null)
                        txtPocoEditor.AppendText("Single", colorUserType);
                    else
                        poco.Append("Single");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "rowversion":
                    if (poco == null)
                    {
                        txtPocoEditor.AppendText("byte", colorKeyword);
                        txtPocoEditor.AppendText("[]");
                    }
                    else
                    {
                        poco.Append("byte[]");
                    }
                    break;

                case "smalldatetime":
                    if (poco == null)
                        txtPocoEditor.AppendText("DateTime", colorUserType);
                    else
                        poco.Append("DateTime");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "smallint":
                    if (poco == null)
                        txtPocoEditor.AppendText("short", colorKeyword);
                    else
                        poco.Append("short");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "smallmoney":
                    if (poco == null)
                        txtPocoEditor.AppendText("decimal", colorKeyword);
                    else
                        poco.Append("decimal");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "sql_variant":
                    if (poco == null)
                        txtPocoEditor.AppendText("object", colorKeyword);
                    else
                        poco.Append("object");
                    break;

                case "text":
                    if (poco == null)
                        txtPocoEditor.AppendText("string", colorKeyword);
                    else
                        poco.Append("string");
                    break;

                case "time":
                    if (poco == null)
                        txtPocoEditor.AppendText("TimeSpan", colorUserType);
                    else
                        poco.Append("TimeSpan");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "timestamp":
                    if (poco == null)
                    {
                        txtPocoEditor.AppendText("byte", colorKeyword);
                        txtPocoEditor.AppendText("[]");
                    }
                    else
                    {
                        poco.Append("byte[]");
                    }
                    break;

                case "tinyint":
                    if (poco == null)
                        txtPocoEditor.AppendText("byte", colorKeyword);
                    else
                        poco.Append("byte");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "uniqueidentifier":
                    if (poco == null)
                        txtPocoEditor.AppendText("Guid", colorUserType);
                    else
                        poco.Append("Guid");
                    if (isNullable || IsAllStructNullable)
                    {
                        if (poco == null)
                            txtPocoEditor.AppendText("?");
                        else
                            poco.Append("?");
                    }
                    break;

                case "varbinary":
                    if (poco == null)
                    {
                        txtPocoEditor.AppendText("byte", colorKeyword);
                        txtPocoEditor.AppendText("[]");
                    }
                    else
                    {
                        poco.Append("byte[]");
                    }
                    break;

                case "varchar":
                    if (poco == null)
                        txtPocoEditor.AppendText("string", colorKeyword);
                    else
                        poco.Append("string");
                    break;

                case "xml":
                    if (poco == null)
                        txtPocoEditor.AppendText("string", colorKeyword);
                    else
                        poco.Append("string");
                    break;

                default:
                    if (poco == null)
                        txtPocoEditor.AppendText("object", colorKeyword);
                    else
                        poco.Append("object");
                    break;
            }

            if (poco == null)
            {
                txtPocoEditor.AppendText(" ");
                txtPocoEditor.AppendText(columnName);
            }
            else
            {
                poco.Append(" ");
                poco.Append(columnName);
            }

            if (IsDataMemebers)
            {
                if (poco == null)
                    txtPocoEditor.AppendText(";");
                else
                    poco.Append(";");
            }
            else
            {
                if (poco == null)
                {
                    txtPocoEditor.AppendText(" { ");
                    txtPocoEditor.AppendText("get", colorKeyword);
                    txtPocoEditor.AppendText("; ");
                    txtPocoEditor.AppendText("set", colorKeyword);
                    txtPocoEditor.AppendText("; }");
                }
                else
                {
                    poco.Append(" { get; set; }");
                }
            }

            if (IsComments)
            {
                if (poco == null)
                {
                    txtPocoEditor.AppendText(" ");
                    txtPocoEditor.AppendText("//", colorComment);
                    txtPocoEditor.AppendText(" ");
                    txtPocoEditor.AppendText(dataType, colorComment);
                    txtPocoEditor.AppendText(precision ?? string.Empty, colorComment);
                }
                else
                {
                    poco.Append(" // ");
                    poco.Append(dataType);
                    poco.Append(precision ?? string.Empty);
                }

                if (IsCommentsWithoutNull == false)
                {
                    if (poco == null)
                    {
                        txtPocoEditor.AppendText(",", colorComment);
                        txtPocoEditor.AppendText(" ");
                        txtPocoEditor.AppendText((isNullable ? "null" : "not null"), colorComment);
                    }
                    else
                    {
                        poco.Append(isNullable ? ", null" : ", not null");
                    }
                }
            }

            if (poco == null)
                txtPocoEditor.AppendText(Environment.NewLine);
            else
                poco.AppendLine();
        }

        private void WriteErrorToEditor(Exception error, StringBuilder poco = null)
        {
            WriteErrorToEditor(new List<Exception>() { error }, poco);
        }

        private void WriteErrorToEditor(List<Exception> errors, StringBuilder poco = null)
        {
            if (poco == null)
                txtPocoEditor.Clear();

            foreach (Exception error in errors)
            {
                Exception currentError = error;
                while (currentError != null)
                {
                    if (poco == null)
                    {
                        txtPocoEditor.AppendText(currentError.Message, colorError);
                        txtPocoEditor.AppendText(Environment.NewLine);
                    }
                    else
                    {
                        poco.AppendLine(currentError.Message);
                    }
                    currentError = currentError.InnerException;
                }

                if (poco == null)
                    txtPocoEditor.AppendText(Environment.NewLine);
                else
                    poco.AppendLine();
            }
        }

        #endregion

        #region Copy

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtPocoEditor.Text);
            }
            catch { }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtPocoEditor.SelectedText ?? string.Empty);
            }
            catch { }
            txtPocoEditor.Focus();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtPocoEditor.SelectAll();
            txtPocoEditor.Focus();
        }

        #endregion

        #region Export

        private void btnFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialogExport.SelectedPath = txtFolder.Text;
            DialogResult dr = folderBrowserDialogExport.ShowDialog(this);
            if (dr == System.Windows.Forms.DialogResult.OK)
                txtFolder.Text = folderBrowserDialogExport.SelectedPath;
        }

        private void chkSingleFile_CheckedChanged(object sender, EventArgs e)
        {
            txtFileName.Enabled = chkSingleFile.Checked;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = string.Empty;
            Application.DoEvents();

            if (string.IsNullOrEmpty(txtFolder.Text))
            {
                toolStripStatusLabel.Text = "Folder isn't set";
                toolStripStatusLabel.ForeColor = Color.Red;
                return;
            }

            if (Directory.Exists(txtFolder.Text) == false)
            {
                toolStripStatusLabel.Text = "Folder doens't exist";
                toolStripStatusLabel.ForeColor = Color.Red;
                return;
            }

            List<Tuple<string, string, StringBuilder>> pocos = new List<Tuple<string, string, StringBuilder>>();
            TreeNode serverNode = trvServer.Nodes[0];
            GetPOCOs(serverNode, pocos);

            if (pocos.Count == 0 && (txtPocoEditor.Tag == null || ((NodeTag)txtPocoEditor.Tag).NodeType == NodeType.None))
                return;

            toolStripStatusLabel.Text = "Exporting...";
            toolStripStatusLabel.ForeColor = Color.Black;
            Application.DoEvents();

            List<Tuple<string, string, bool, Exception>> saveResults = new List<Tuple<string, string, bool, Exception>>();

            if (pocos.Count > 0)
            {
                if (chkSingleFile.Checked && string.IsNullOrEmpty(txtFileName.Text) == false)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Tuple<string, string, StringBuilder> poco in pocos)
                    {
                        sb.Append(poco.Item3);
                        sb.AppendLine();
                    }

                    if (pocos.Count > 1 && IsUsing)
                    {
                        string exportContent = sb.ToString();

                        bool isUsingSystem = exportContent.Contains("using System;");
                        bool isUsingSqlServerTypes = exportContent.Contains("using Microsoft.SqlServer.Types;");
                        bool isUsingDataAnnotations = exportContent.Contains("using System.ComponentModel.DataAnnotations;");

                        string usingLine1 = "using System;" + Environment.NewLine + "using Microsoft.SqlServer.Types;" + Environment.NewLine + "using System.ComponentModel.DataAnnotations;" + Environment.NewLine + Environment.NewLine;
                        string usingLine2 = "using System;" + Environment.NewLine + "using System.ComponentModel.DataAnnotations;" + Environment.NewLine + Environment.NewLine;
                        string usingLine3 = "using System;" + Environment.NewLine + "using Microsoft.SqlServer.Types;" + Environment.NewLine + Environment.NewLine;
                        string usingLine4 = "using System;" + Environment.NewLine + Environment.NewLine;

                        sb.Replace(usingLine1, string.Empty);
                        sb.Replace(usingLine2, string.Empty);
                        sb.Replace(usingLine3, string.Empty);
                        sb.Replace(usingLine4, string.Empty);

                        string usingLine = usingLine4;
                        if (isUsingSqlServerTypes && isUsingDataAnnotations)
                            usingLine = usingLine1;
                        else if (isUsingSqlServerTypes == false && isUsingDataAnnotations)
                            usingLine = usingLine2;
                        else if (isUsingSqlServerTypes && isUsingDataAnnotations == false)
                            usingLine = usingLine3;

                        sb.Insert(0, usingLine);
                    }

                    if (pocos.Count > 1 && string.IsNullOrEmpty(Namespace) == false)
                    {
                        string namespaceLine = "}" + Environment.NewLine + Environment.NewLine + "namespace " + Namespace + Environment.NewLine + "{" + Environment.NewLine;
                        sb.Replace(namespaceLine, Environment.NewLine);
                    }

                    string fileName = txtFileName.Text;

                    Exception error = null;
                    bool succeeded = SavePocosToFile(fileName, sb.ToString(), true, ref error);
                    saveResults.Add(new Tuple<string, string, bool, Exception>(fileName, (pocos.Count == 1 ? pocos[0].Item2 : null), succeeded, error));
                }
                else
                {
                    foreach (Tuple<string, string, StringBuilder> poco in pocos)
                    {
                        string fileName = poco.Item1 + ".cs";

                        Exception error = null;
                        bool succeeded = SavePocosToFile(fileName, poco.Item3.ToString(), false, ref error);
                        saveResults.Add(new Tuple<string, string, bool, Exception>(fileName, poco.Item2, succeeded, error));
                    }
                }
            }
            else
            {
                NodeTag txtPocoEditorNodeTag = txtPocoEditor.Tag as NodeTag;
                if (txtPocoEditorNodeTag == null)
                    txtPocoEditorNodeTag = new NodeTag() { NodeType = NodeType.None };

                string className = null;
                string dbName = null;
                if (txtPocoEditorNodeTag.NodeType == NodeType.Table)
                {
                    Table table = (Table)txtPocoEditorNodeTag.DbObject;
                    className = table.ClassName;
                    dbName = table.Database.ToString() + "." + table.ToString();

                    if (table.Error != null)
                    {
                        toolStripStatusLabel.Text = "Exporting " + dbName + " is aborted";
                        toolStripStatusLabel.ForeColor = Color.Red;
                        return;
                    }
                }
                else if (txtPocoEditorNodeTag.NodeType == NodeType.Procedure)
                {
                    Procedure procedure = (Procedure)txtPocoEditorNodeTag.DbObject;
                    className = procedure.ClassName;
                    dbName = procedure.Database.ToString() + "." + procedure.ToString();

                    if (procedure.Error != null)
                    {
                        toolStripStatusLabel.Text = "Exporting " + dbName + " is aborted";
                        toolStripStatusLabel.ForeColor = Color.Red;
                        return;
                    }
                }
                else if (txtPocoEditorNodeTag.NodeType == NodeType.TVP)
                {
                    TVP tvp = (TVP)txtPocoEditorNodeTag.DbObject;
                    className = tvp.ClassName;
                    dbName = tvp.Database.ToString() + "." + tvp.ToString();

                    if (tvp.Error != null)
                    {
                        toolStripStatusLabel.Text = "Exporting " + dbName + " is aborted";
                        toolStripStatusLabel.ForeColor = Color.Red;
                        return;
                    }
                }

                if (chkSingleFile.Checked && string.IsNullOrEmpty(txtFileName.Text) == false)
                {
                    string fileName = txtFileName.Text;

                    Exception error = null;
                    bool succeeded = SavePocosToFile(fileName, txtPocoEditor.Text + Environment.NewLine, true, ref error);
                    saveResults.Add(new Tuple<string, string, bool, Exception>(fileName, dbName, succeeded, error));
                }
                else
                {
                    string fileName = className + ".cs";

                    Exception error = null;
                    bool succeeded = SavePocosToFile(fileName, txtPocoEditor.Text, false, ref error);
                    saveResults.Add(new Tuple<string, string, bool, Exception>(fileName, dbName, succeeded, error));
                }
            }

            if (saveResults.Count == 1)
            {
                var saveResult = saveResults[0];
                if (saveResult.Item3)
                {
                    if (string.IsNullOrEmpty(saveResult.Item2))
                        toolStripStatusLabel.Text = string.Format("Exported to {0}", saveResult.Item1);
                    else
                        toolStripStatusLabel.Text = string.Format("Exported {0} to {1}", saveResult.Item2, saveResult.Item1);
                    toolStripStatusLabel.ForeColor = Color.Green;
                }
                else
                {
                    if (string.IsNullOrEmpty(saveResult.Item2))
                        toolStripStatusLabel.Text = string.Format("Failed to export to {0}", saveResult.Item1);
                    else
                        toolStripStatusLabel.Text = string.Format("Failed to export {0} to {1}", saveResult.Item2, saveResult.Item1);
                    toolStripStatusLabel.ForeColor = Color.Red;
                }
            }
            else if (saveResults.Count > 1)
            {
                var saveResultsFailed = saveResults.Where(r => !r.Item3).ToList();

                if (saveResultsFailed.Count == 0)
                {
                    toolStripStatusLabel.Text = "All POCOs exported successfully";
                    toolStripStatusLabel.ForeColor = Color.Green;
                }
                else if (saveResultsFailed.Count == 1)
                {
                    var saveResult = saveResultsFailed[0];
                    if (string.IsNullOrEmpty(saveResult.Item2))
                        toolStripStatusLabel.Text = string.Format("Failed to export to {0}", saveResult.Item1);
                    else
                        toolStripStatusLabel.Text = string.Format("Failed to export {0} to {1}", saveResult.Item2, saveResult.Item1);
                    toolStripStatusLabel.ForeColor = Color.Red;
                }
                else if (saveResultsFailed.Count < saveResults.Count)
                {
                    toolStripStatusLabel.Text = "Failed to export several POCOs";
                    toolStripStatusLabel.ForeColor = Color.Red;
                }
                else if (saveResultsFailed.Count == saveResults.Count)
                {
                    toolStripStatusLabel.Text = "Failed to export all POCOs";
                    toolStripStatusLabel.ForeColor = Color.Red;
                }
            }
        }

        private void GetPOCOs(TreeNode root, List<Tuple<string, string, StringBuilder>> pocos)
        {
            if (root.Checked)
            {
                NodeType nodeType = ((NodeTag)root.Tag).NodeType;

                if (nodeType == NodeType.Table ||
                    nodeType == NodeType.View ||
                    nodeType == NodeType.Procedure ||
                    nodeType == NodeType.Function ||
                    nodeType == NodeType.TVP)
                {
                    StringBuilder poco = new StringBuilder();
                    WritePocoToEditor(root, true, poco);

                    string className = null;
                    string dbName = null;
                    bool ignorePoco = false;

                    if (nodeType == NodeType.Table || nodeType == NodeType.View)
                    {
                        Table table = (Table)((NodeTag)root.Tag).DbObject;
                        className = table.ClassName;
                        dbName = table.Database.ToString() + "." + table.ToString();

                        if (table.Error != null)
                            ignorePoco = true;
                    }
                    else if (nodeType == NodeType.Procedure || nodeType == NodeType.Function)
                    {
                        Procedure procedure = (Procedure)((NodeTag)root.Tag).DbObject;
                        className = procedure.ClassName;
                        dbName = procedure.Database.ToString() + "." + procedure.ToString();

                        if (procedure.Error != null)
                            ignorePoco = true;
                    }
                    else if (nodeType == NodeType.TVP)
                    {
                        TVP tvp = (TVP)((NodeTag)root.Tag).DbObject;
                        className = tvp.ClassName;
                        dbName = tvp.Database.ToString() + "." + tvp.ToString();

                        if (tvp.Error != null)
                            ignorePoco = true;
                    }

                    if (ignorePoco == false)
                        pocos.Add(new Tuple<string, string, StringBuilder>(className, dbName, poco));
                }
            }

            foreach (TreeNode node in root.Nodes)
                GetPOCOs(node, pocos);
        }

        private bool SavePocosToFile(string fileName, string content, bool isAppend, ref Exception error)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName) || fileName == ".cs")
                    throw new Exception("File name isn't set");

                string path = txtFolder.Text.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + fileName;
                if (isAppend)
                    File.AppendAllText(path, content);
                else
                    File.WriteAllText(path, content);

                return true;
            }
            catch (Exception ex)
            {
                error = ex;
                return false;
            }
        }

        #endregion

        #region Type Mapping

        private static TypeMappingForm typeMappingForm = new TypeMappingForm();

        private void btnTypeMapping_Click(object sender, EventArgs e)
        {
            typeMappingForm.ShowDialog(this);
        }

        #endregion
    }
}
