namespace POCOGenerator
{
    partial class POCOGeneratorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POCOGeneratorForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trvServer = new System.Windows.Forms.TreeView();
            this.contextMenuServerTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListDbObjects = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtPocoEditor = new System.Windows.Forms.RichTextBox();
            this.contextMenuPocoEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain = new System.Windows.Forms.Panel();
            this.chkIgnoreCase = new System.Windows.Forms.CheckBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.lblWith = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblReplace = new System.Windows.Forms.Label();
            this.lblSingularDesc = new System.Windows.Forms.Label();
            this.lblORMAnnotationsTablesDesc = new System.Windows.Forms.Label();
            this.chkEFCodeFirstColumn = new System.Windows.Forms.CheckBox();
            this.chkEFCodeFirstRequired = new System.Windows.Forms.CheckBox();
            this.chkEFCodeFirst = new System.Windows.Forms.CheckBox();
            this.chkPartialClass = new System.Windows.Forms.CheckBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblORMAnnotations = new System.Windows.Forms.Label();
            this.chkPetaPocoExplicitColumns = new System.Windows.Forms.CheckBox();
            this.chkPetaPoco = new System.Windows.Forms.CheckBox();
            this.chkSingular = new System.Windows.Forms.CheckBox();
            this.chkUsing = new System.Windows.Forms.CheckBox();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtSchemaSeparator = new System.Windows.Forms.TextBox();
            this.lblSchemaSeparator = new System.Windows.Forms.Label();
            this.chkIgnoreDboSchema = new System.Windows.Forms.CheckBox();
            this.chkIncludeSchema = new System.Windows.Forms.CheckBox();
            this.txtDBSeparator = new System.Windows.Forms.TextBox();
            this.lblDBSeparator = new System.Windows.Forms.Label();
            this.chkIncludeDB = new System.Windows.Forms.CheckBox();
            this.lblFixedName = new System.Windows.Forms.Label();
            this.lblWordsSeparatorDesc = new System.Windows.Forms.Label();
            this.btnTypeMapping = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnFolder = new System.Windows.Forms.Button();
            this.chkSingleFile = new System.Windows.Forms.CheckBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.lblExport = new System.Windows.Forms.Label();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtWordsSeparator = new System.Windows.Forms.TextBox();
            this.lblWordsSeparator = new System.Windows.Forms.Label();
            this.chkLowerCase = new System.Windows.Forms.CheckBox();
            this.chkUpperCase = new System.Windows.Forms.CheckBox();
            this.chkCamelCase = new System.Windows.Forms.CheckBox();
            this.lblPOCO = new System.Windows.Forms.Label();
            this.chkCommentsWithoutNull = new System.Windows.Forms.CheckBox();
            this.chkComments = new System.Windows.Forms.CheckBox();
            this.txtFixedClassName = new System.Windows.Forms.TextBox();
            this.chkAllStructNullable = new System.Windows.Forms.CheckBox();
            this.chkVirtualProperties = new System.Windows.Forms.CheckBox();
            this.lblClassName = new System.Windows.Forms.Label();
            this.rdbDataMembers = new System.Windows.Forms.RadioButton();
            this.rdbProperties = new System.Windows.Forms.RadioButton();
            this.folderBrowserDialogExport = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuServerTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuPocoEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trvServer);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 650;
            this.splitContainer1.Size = new System.Drawing.Size(1054, 712);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 0;
            // 
            // trvServer
            // 
            this.trvServer.CheckBoxes = true;
            this.trvServer.ContextMenuStrip = this.contextMenuServerTree;
            this.trvServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvServer.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.trvServer.HideSelection = false;
            this.trvServer.ImageIndex = 0;
            this.trvServer.ImageList = this.imageListDbObjects;
            this.trvServer.Location = new System.Drawing.Point(0, 0);
            this.trvServer.Name = "trvServer";
            this.trvServer.SelectedImageIndex = 0;
            this.trvServer.Size = new System.Drawing.Size(400, 712);
            this.trvServer.TabIndex = 1;
            this.trvServer.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvServer_AfterCheck);
            this.trvServer.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.trvServer_DrawNode);
            this.trvServer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvServer_AfterSelect);
            this.trvServer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trvServer_MouseUp);
            // 
            // contextMenuServerTree
            // 
            this.contextMenuServerTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeFilterToolStripMenuItem,
            this.filterSettingsToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuServerTree.Name = "contextMenuServerTree";
            this.contextMenuServerTree.Size = new System.Drawing.Size(147, 70);
            // 
            // removeFilterToolStripMenuItem
            // 
            this.removeFilterToolStripMenuItem.Name = "removeFilterToolStripMenuItem";
            this.removeFilterToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.removeFilterToolStripMenuItem.Text = "Remove Filter";
            this.removeFilterToolStripMenuItem.Click += new System.EventHandler(this.removeFilterToolStripMenuItem_Click);
            // 
            // filterSettingsToolStripMenuItem
            // 
            this.filterSettingsToolStripMenuItem.Name = "filterSettingsToolStripMenuItem";
            this.filterSettingsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.filterSettingsToolStripMenuItem.Text = "Filter Settings";
            this.filterSettingsToolStripMenuItem.Click += new System.EventHandler(this.filterSettingsToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // imageListDbObjects
            // 
            this.imageListDbObjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDbObjects.ImageStream")));
            this.imageListDbObjects.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDbObjects.Images.SetKeyName(0, "Server.gif");
            this.imageListDbObjects.Images.SetKeyName(1, "Database.gif");
            this.imageListDbObjects.Images.SetKeyName(2, "Folder.gif");
            this.imageListDbObjects.Images.SetKeyName(3, "Table.gif");
            this.imageListDbObjects.Images.SetKeyName(4, "View.gif");
            this.imageListDbObjects.Images.SetKeyName(5, "Procedure.gif");
            this.imageListDbObjects.Images.SetKeyName(6, "Function.gif");
            this.imageListDbObjects.Images.SetKeyName(7, "Column.gif");
            this.imageListDbObjects.Images.SetKeyName(8, "PK.gif");
            this.imageListDbObjects.Images.SetKeyName(9, "FK.gif");
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtPocoEditor);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panelMain);
            this.splitContainer2.Size = new System.Drawing.Size(650, 712);
            this.splitContainer2.SplitterDistance = 415;
            this.splitContainer2.TabIndex = 0;
            // 
            // txtPocoEditor
            // 
            this.txtPocoEditor.BackColor = System.Drawing.Color.White;
            this.txtPocoEditor.ContextMenuStrip = this.contextMenuPocoEditor;
            this.txtPocoEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPocoEditor.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtPocoEditor.Location = new System.Drawing.Point(0, 0);
            this.txtPocoEditor.Name = "txtPocoEditor";
            this.txtPocoEditor.ReadOnly = true;
            this.txtPocoEditor.Size = new System.Drawing.Size(650, 415);
            this.txtPocoEditor.TabIndex = 0;
            this.txtPocoEditor.Text = "";
            this.txtPocoEditor.WordWrap = false;
            // 
            // contextMenuPocoEditor
            // 
            this.contextMenuPocoEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.contextMenuPocoEditor.Name = "contextMenuPocoEditor";
            this.contextMenuPocoEditor.Size = new System.Drawing.Size(123, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.chkIgnoreCase);
            this.panelMain.Controls.Add(this.txtReplace);
            this.panelMain.Controls.Add(this.lblWith);
            this.panelMain.Controls.Add(this.txtSearch);
            this.panelMain.Controls.Add(this.lblReplace);
            this.panelMain.Controls.Add(this.lblSingularDesc);
            this.panelMain.Controls.Add(this.lblORMAnnotationsTablesDesc);
            this.panelMain.Controls.Add(this.chkEFCodeFirstColumn);
            this.panelMain.Controls.Add(this.chkEFCodeFirstRequired);
            this.panelMain.Controls.Add(this.chkEFCodeFirst);
            this.panelMain.Controls.Add(this.chkPartialClass);
            this.panelMain.Controls.Add(this.statusStrip);
            this.panelMain.Controls.Add(this.lblORMAnnotations);
            this.panelMain.Controls.Add(this.chkPetaPocoExplicitColumns);
            this.panelMain.Controls.Add(this.chkPetaPoco);
            this.panelMain.Controls.Add(this.chkSingular);
            this.panelMain.Controls.Add(this.chkUsing);
            this.panelMain.Controls.Add(this.txtNamespace);
            this.panelMain.Controls.Add(this.lblNamespace);
            this.panelMain.Controls.Add(this.btnCopy);
            this.panelMain.Controls.Add(this.txtSchemaSeparator);
            this.panelMain.Controls.Add(this.lblSchemaSeparator);
            this.panelMain.Controls.Add(this.chkIgnoreDboSchema);
            this.panelMain.Controls.Add(this.chkIncludeSchema);
            this.panelMain.Controls.Add(this.txtDBSeparator);
            this.panelMain.Controls.Add(this.lblDBSeparator);
            this.panelMain.Controls.Add(this.chkIncludeDB);
            this.panelMain.Controls.Add(this.lblFixedName);
            this.panelMain.Controls.Add(this.lblWordsSeparatorDesc);
            this.panelMain.Controls.Add(this.btnTypeMapping);
            this.panelMain.Controls.Add(this.btnClose);
            this.panelMain.Controls.Add(this.btnExport);
            this.panelMain.Controls.Add(this.btnFolder);
            this.panelMain.Controls.Add(this.chkSingleFile);
            this.panelMain.Controls.Add(this.txtFileName);
            this.panelMain.Controls.Add(this.txtFolder);
            this.panelMain.Controls.Add(this.lblExport);
            this.panelMain.Controls.Add(this.txtSuffix);
            this.panelMain.Controls.Add(this.lblSuffix);
            this.panelMain.Controls.Add(this.txtPrefix);
            this.panelMain.Controls.Add(this.lblPrefix);
            this.panelMain.Controls.Add(this.txtWordsSeparator);
            this.panelMain.Controls.Add(this.lblWordsSeparator);
            this.panelMain.Controls.Add(this.chkLowerCase);
            this.panelMain.Controls.Add(this.chkUpperCase);
            this.panelMain.Controls.Add(this.chkCamelCase);
            this.panelMain.Controls.Add(this.lblPOCO);
            this.panelMain.Controls.Add(this.chkCommentsWithoutNull);
            this.panelMain.Controls.Add(this.chkComments);
            this.panelMain.Controls.Add(this.txtFixedClassName);
            this.panelMain.Controls.Add(this.chkAllStructNullable);
            this.panelMain.Controls.Add(this.chkVirtualProperties);
            this.panelMain.Controls.Add(this.lblClassName);
            this.panelMain.Controls.Add(this.rdbDataMembers);
            this.panelMain.Controls.Add(this.rdbProperties);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(650, 293);
            this.panelMain.TabIndex = 1;
            // 
            // chkIgnoreCase
            // 
            this.chkIgnoreCase.AutoSize = true;
            this.chkIgnoreCase.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIgnoreCase.Location = new System.Drawing.Point(448, 141);
            this.chkIgnoreCase.Name = "chkIgnoreCase";
            this.chkIgnoreCase.Size = new System.Drawing.Size(83, 17);
            this.chkIgnoreCase.TabIndex = 28;
            this.chkIgnoreCase.Text = "Ignore Case";
            this.chkIgnoreCase.UseVisualStyleBackColor = true;
            this.chkIgnoreCase.CheckedChanged += new System.EventHandler(this.chkIgnoreCase_CheckedChanged);
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point(395, 139);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(45, 20);
            this.txtReplace.TabIndex = 27;
            this.txtReplace.TextChanged += new System.EventHandler(this.txtReplace_TextChanged);
            // 
            // lblWith
            // 
            this.lblWith.AutoSize = true;
            this.lblWith.Location = new System.Drawing.Point(358, 143);
            this.lblWith.Name = "lblWith";
            this.lblWith.Size = new System.Drawing.Size(29, 13);
            this.lblWith.TabIndex = 0;
            this.lblWith.Text = "With";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(305, 139);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(45, 20);
            this.txtSearch.TabIndex = 26;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblReplace
            // 
            this.lblReplace.AutoSize = true;
            this.lblReplace.Location = new System.Drawing.Point(250, 143);
            this.lblReplace.Name = "lblReplace";
            this.lblReplace.Size = new System.Drawing.Size(47, 13);
            this.lblReplace.TabIndex = 0;
            this.lblReplace.Text = "Replace";
            // 
            // lblSingularDesc
            // 
            this.lblSingularDesc.AutoSize = true;
            this.lblSingularDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblSingularDesc.Location = new System.Drawing.Point(322, 24);
            this.lblSingularDesc.Name = "lblSingularDesc";
            this.lblSingularDesc.Size = new System.Drawing.Size(125, 13);
            this.lblSingularDesc.TabIndex = 0;
            this.lblSingularDesc.Text = "(Tables, Views and TVPs)";
            // 
            // lblORMAnnotationsTablesDesc
            // 
            this.lblORMAnnotationsTablesDesc.AutoSize = true;
            this.lblORMAnnotationsTablesDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblORMAnnotationsTablesDesc.Location = new System.Drawing.Point(356, 197);
            this.lblORMAnnotationsTablesDesc.Name = "lblORMAnnotationsTablesDesc";
            this.lblORMAnnotationsTablesDesc.Size = new System.Drawing.Size(43, 13);
            this.lblORMAnnotationsTablesDesc.TabIndex = 0;
            this.lblORMAnnotationsTablesDesc.Text = "(Tables)";
            // 
            // chkEFCodeFirstColumn
            // 
            this.chkEFCodeFirstColumn.AutoSize = true;
            this.chkEFCodeFirstColumn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEFCodeFirstColumn.Location = new System.Drawing.Point(347, 223);
            this.chkEFCodeFirstColumn.Name = "chkEFCodeFirstColumn";
            this.chkEFCodeFirstColumn.Size = new System.Drawing.Size(61, 17);
            this.chkEFCodeFirstColumn.TabIndex = 33;
            this.chkEFCodeFirstColumn.Text = "Column";
            this.chkEFCodeFirstColumn.UseVisualStyleBackColor = true;
            this.chkEFCodeFirstColumn.CheckedChanged += new System.EventHandler(this.chkEFCodeFirstColumn_CheckedChanged);
            // 
            // chkEFCodeFirstRequired
            // 
            this.chkEFCodeFirstRequired.AutoSize = true;
            this.chkEFCodeFirstRequired.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEFCodeFirstRequired.Location = new System.Drawing.Point(416, 223);
            this.chkEFCodeFirstRequired.Name = "chkEFCodeFirstRequired";
            this.chkEFCodeFirstRequired.Size = new System.Drawing.Size(69, 17);
            this.chkEFCodeFirstRequired.TabIndex = 34;
            this.chkEFCodeFirstRequired.Text = "Required";
            this.chkEFCodeFirstRequired.UseVisualStyleBackColor = true;
            this.chkEFCodeFirstRequired.CheckedChanged += new System.EventHandler(this.chkEFCodeFirstRequired_CheckedChanged);
            // 
            // chkEFCodeFirst
            // 
            this.chkEFCodeFirst.AutoSize = true;
            this.chkEFCodeFirst.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEFCodeFirst.Location = new System.Drawing.Point(250, 223);
            this.chkEFCodeFirst.Name = "chkEFCodeFirst";
            this.chkEFCodeFirst.Size = new System.Drawing.Size(89, 17);
            this.chkEFCodeFirst.TabIndex = 32;
            this.chkEFCodeFirst.Text = "EF Code-First";
            this.chkEFCodeFirst.UseVisualStyleBackColor = true;
            this.chkEFCodeFirst.CheckedChanged += new System.EventHandler(this.chkEF_CheckedChanged);
            // 
            // chkPartialClass
            // 
            this.chkPartialClass.AutoSize = true;
            this.chkPartialClass.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPartialClass.Location = new System.Drawing.Point(117, 47);
            this.chkPartialClass.Name = "chkPartialClass";
            this.chkPartialClass.Size = new System.Drawing.Size(83, 17);
            this.chkPartialClass.TabIndex = 5;
            this.chkPartialClass.Text = "Partial Class";
            this.chkPartialClass.UseVisualStyleBackColor = true;
            this.chkPartialClass.CheckedChanged += new System.EventHandler(this.chkPartialClass_CheckedChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 271);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(650, 22);
            this.statusStrip.TabIndex = 34;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // lblORMAnnotations
            // 
            this.lblORMAnnotations.AutoSize = true;
            this.lblORMAnnotations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblORMAnnotations.Location = new System.Drawing.Point(250, 197);
            this.lblORMAnnotations.Name = "lblORMAnnotations";
            this.lblORMAnnotations.Size = new System.Drawing.Size(106, 13);
            this.lblORMAnnotations.TabIndex = 0;
            this.lblORMAnnotations.Text = "ORM Annotations";
            // 
            // chkPetaPocoExplicitColumns
            // 
            this.chkPetaPocoExplicitColumns.AutoSize = true;
            this.chkPetaPocoExplicitColumns.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPetaPocoExplicitColumns.Location = new System.Drawing.Point(331, 248);
            this.chkPetaPocoExplicitColumns.Name = "chkPetaPocoExplicitColumns";
            this.chkPetaPocoExplicitColumns.Size = new System.Drawing.Size(102, 17);
            this.chkPetaPocoExplicitColumns.TabIndex = 36;
            this.chkPetaPocoExplicitColumns.Text = "Explicit Columns";
            this.chkPetaPocoExplicitColumns.UseVisualStyleBackColor = true;
            this.chkPetaPocoExplicitColumns.CheckedChanged += new System.EventHandler(this.chkPetaPocoExplicitColumns_CheckedChanged);
            // 
            // chkPetaPoco
            // 
            this.chkPetaPoco.AutoSize = true;
            this.chkPetaPoco.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPetaPoco.Location = new System.Drawing.Point(250, 248);
            this.chkPetaPoco.Name = "chkPetaPoco";
            this.chkPetaPoco.Size = new System.Drawing.Size(73, 17);
            this.chkPetaPoco.TabIndex = 35;
            this.chkPetaPoco.Text = "PetaPoco";
            this.chkPetaPoco.UseVisualStyleBackColor = true;
            this.chkPetaPoco.CheckedChanged += new System.EventHandler(this.chkPetaPoco_CheckedChanged);
            // 
            // chkSingular
            // 
            this.chkSingular.AutoSize = true;
            this.chkSingular.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSingular.Location = new System.Drawing.Point(250, 24);
            this.chkSingular.Name = "chkSingular";
            this.chkSingular.Size = new System.Drawing.Size(64, 17);
            this.chkSingular.TabIndex = 16;
            this.chkSingular.Text = "Singular";
            this.chkSingular.UseVisualStyleBackColor = true;
            this.chkSingular.CheckedChanged += new System.EventHandler(this.chkSingular_CheckedChanged);
            // 
            // chkUsing
            // 
            this.chkUsing.AutoSize = true;
            this.chkUsing.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUsing.Location = new System.Drawing.Point(4, 118);
            this.chkUsing.Name = "chkUsing";
            this.chkUsing.Size = new System.Drawing.Size(51, 17);
            this.chkUsing.TabIndex = 9;
            this.chkUsing.Text = "using";
            this.chkUsing.UseVisualStyleBackColor = true;
            this.chkUsing.CheckedChanged += new System.EventHandler(this.chkUsing_CheckedChanged);
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(135, 116);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(100, 20);
            this.txtNamespace.TabIndex = 10;
            this.txtNamespace.TextChanged += new System.EventHandler(this.txtNamespace_TextChanged);
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.Location = new System.Drawing.Point(63, 120);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(64, 13);
            this.lblNamespace.TabIndex = 0;
            this.lblNamespace.Text = "Namespace";
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.AutoSize = true;
            this.btnCopy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCopy.Location = new System.Drawing.Point(606, 5);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(41, 23);
            this.btnCopy.TabIndex = 37;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtSchemaSeparator
            // 
            this.txtSchemaSeparator.Location = new System.Drawing.Point(591, 70);
            this.txtSchemaSeparator.Name = "txtSchemaSeparator";
            this.txtSchemaSeparator.Size = new System.Drawing.Size(45, 20);
            this.txtSchemaSeparator.TabIndex = 21;
            this.txtSchemaSeparator.TextChanged += new System.EventHandler(this.txtSchemaSeparator_TextChanged);
            // 
            // lblSchemaSeparator
            // 
            this.lblSchemaSeparator.AutoSize = true;
            this.lblSchemaSeparator.Location = new System.Drawing.Point(488, 74);
            this.lblSchemaSeparator.Name = "lblSchemaSeparator";
            this.lblSchemaSeparator.Size = new System.Drawing.Size(95, 13);
            this.lblSchemaSeparator.TabIndex = 0;
            this.lblSchemaSeparator.Text = "Schema Separator";
            // 
            // chkIgnoreDboSchema
            // 
            this.chkIgnoreDboSchema.AutoSize = true;
            this.chkIgnoreDboSchema.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIgnoreDboSchema.Location = new System.Drawing.Point(361, 72);
            this.chkIgnoreDboSchema.Name = "chkIgnoreDboSchema";
            this.chkIgnoreDboSchema.Size = new System.Drawing.Size(119, 17);
            this.chkIgnoreDboSchema.TabIndex = 20;
            this.chkIgnoreDboSchema.Text = "Ignore dbo Schema";
            this.chkIgnoreDboSchema.UseVisualStyleBackColor = true;
            this.chkIgnoreDboSchema.CheckedChanged += new System.EventHandler(this.chkIgnoreDboSchema_CheckedChanged);
            // 
            // chkIncludeSchema
            // 
            this.chkIncludeSchema.AutoSize = true;
            this.chkIncludeSchema.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncludeSchema.Location = new System.Drawing.Point(250, 72);
            this.chkIncludeSchema.Name = "chkIncludeSchema";
            this.chkIncludeSchema.Size = new System.Drawing.Size(103, 17);
            this.chkIncludeSchema.TabIndex = 19;
            this.chkIncludeSchema.Text = "Include Schema";
            this.chkIncludeSchema.UseVisualStyleBackColor = true;
            this.chkIncludeSchema.CheckedChanged += new System.EventHandler(this.chkIncludeSchema_CheckedChanged);
            // 
            // txtDBSeparator
            // 
            this.txtDBSeparator.Location = new System.Drawing.Point(416, 45);
            this.txtDBSeparator.Name = "txtDBSeparator";
            this.txtDBSeparator.Size = new System.Drawing.Size(45, 20);
            this.txtDBSeparator.TabIndex = 18;
            this.txtDBSeparator.TextChanged += new System.EventHandler(this.txtDBSeparator_TextChanged);
            // 
            // lblDBSeparator
            // 
            this.lblDBSeparator.AutoSize = true;
            this.lblDBSeparator.Location = new System.Drawing.Point(337, 49);
            this.lblDBSeparator.Name = "lblDBSeparator";
            this.lblDBSeparator.Size = new System.Drawing.Size(71, 13);
            this.lblDBSeparator.TabIndex = 0;
            this.lblDBSeparator.Text = "DB Separator";
            // 
            // chkIncludeDB
            // 
            this.chkIncludeDB.AutoSize = true;
            this.chkIncludeDB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncludeDB.Location = new System.Drawing.Point(250, 47);
            this.chkIncludeDB.Name = "chkIncludeDB";
            this.chkIncludeDB.Size = new System.Drawing.Size(79, 17);
            this.chkIncludeDB.TabIndex = 17;
            this.chkIncludeDB.Text = "Include DB";
            this.chkIncludeDB.UseVisualStyleBackColor = true;
            this.chkIncludeDB.CheckedChanged += new System.EventHandler(this.chkIncludeDB_CheckedChanged);
            // 
            // lblFixedName
            // 
            this.lblFixedName.AutoSize = true;
            this.lblFixedName.Location = new System.Drawing.Point(250, 169);
            this.lblFixedName.Name = "lblFixedName";
            this.lblFixedName.Size = new System.Drawing.Size(63, 13);
            this.lblFixedName.TabIndex = 0;
            this.lblFixedName.Text = "Fixed Name";
            // 
            // lblWordsSeparatorDesc
            // 
            this.lblWordsSeparatorDesc.AutoSize = true;
            this.lblWordsSeparatorDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblWordsSeparatorDesc.Location = new System.Drawing.Point(398, 97);
            this.lblWordsSeparatorDesc.Name = "lblWordsSeparatorDesc";
            this.lblWordsSeparatorDesc.Size = new System.Drawing.Size(214, 13);
            this.lblWordsSeparatorDesc.TabIndex = 0;
            this.lblWordsSeparatorDesc.Text = "(Words between _ and words in CamelCase)";
            // 
            // btnTypeMapping
            // 
            this.btnTypeMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTypeMapping.AutoSize = true;
            this.btnTypeMapping.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnTypeMapping.Location = new System.Drawing.Point(513, 245);
            this.btnTypeMapping.Name = "btnTypeMapping";
            this.btnTypeMapping.Size = new System.Drawing.Size(85, 23);
            this.btnTypeMapping.TabIndex = 38;
            this.btnTypeMapping.Text = "Type Mapping";
            this.btnTypeMapping.UseVisualStyleBackColor = true;
            this.btnTypeMapping.Click += new System.EventHandler(this.btnTypeMapping_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSize = true;
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(604, 245);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(43, 23);
            this.btnClose.TabIndex = 39;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.AutoSize = true;
            this.btnExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExport.Location = new System.Drawing.Point(4, 245);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(47, 23);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnFolder
            // 
            this.btnFolder.AutoSize = true;
            this.btnFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFolder.Location = new System.Drawing.Point(4, 192);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(46, 23);
            this.btnFolder.TabIndex = 11;
            this.btnFolder.Text = "Folder";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // chkSingleFile
            // 
            this.chkSingleFile.AutoSize = true;
            this.chkSingleFile.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSingleFile.Location = new System.Drawing.Point(4, 223);
            this.chkSingleFile.Name = "chkSingleFile";
            this.chkSingleFile.Size = new System.Drawing.Size(94, 17);
            this.chkSingleFile.TabIndex = 13;
            this.chkSingleFile.Text = "Append to File";
            this.chkSingleFile.UseVisualStyleBackColor = true;
            this.chkSingleFile.CheckedChanged += new System.EventHandler(this.chkSingleFile_CheckedChanged);
            // 
            // txtFileName
            // 
            this.txtFileName.Enabled = false;
            this.txtFileName.Location = new System.Drawing.Point(106, 221);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(100, 20);
            this.txtFileName.TabIndex = 14;
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(58, 193);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(180, 20);
            this.txtFolder.TabIndex = 12;
            // 
            // lblExport
            // 
            this.lblExport.AutoSize = true;
            this.lblExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblExport.Location = new System.Drawing.Point(4, 169);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(88, 13);
            this.lblExport.TabIndex = 0;
            this.lblExport.Text = "Export to Files";
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(564, 165);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(45, 20);
            this.txtSuffix.TabIndex = 31;
            this.txtSuffix.TextChanged += new System.EventHandler(this.txtSuffix_TextChanged);
            // 
            // lblSuffix
            // 
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Location = new System.Drawing.Point(523, 169);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new System.Drawing.Size(33, 13);
            this.lblSuffix.TabIndex = 0;
            this.lblSuffix.Text = "Suffix";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(470, 165);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(45, 20);
            this.txtPrefix.TabIndex = 30;
            this.txtPrefix.TextChanged += new System.EventHandler(this.txtPrefix_TextChanged);
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(429, 169);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(33, 13);
            this.lblPrefix.TabIndex = 0;
            this.lblPrefix.Text = "Prefix";
            // 
            // txtWordsSeparator
            // 
            this.txtWordsSeparator.Location = new System.Drawing.Point(345, 93);
            this.txtWordsSeparator.Name = "txtWordsSeparator";
            this.txtWordsSeparator.Size = new System.Drawing.Size(45, 20);
            this.txtWordsSeparator.TabIndex = 22;
            this.txtWordsSeparator.TextChanged += new System.EventHandler(this.txtWordsSeparator_TextChanged);
            // 
            // lblWordsSeparator
            // 
            this.lblWordsSeparator.AutoSize = true;
            this.lblWordsSeparator.Location = new System.Drawing.Point(250, 97);
            this.lblWordsSeparator.Name = "lblWordsSeparator";
            this.lblWordsSeparator.Size = new System.Drawing.Size(87, 13);
            this.lblWordsSeparator.TabIndex = 0;
            this.lblWordsSeparator.Text = "Words Separator";
            // 
            // chkLowerCase
            // 
            this.chkLowerCase.AutoSize = true;
            this.chkLowerCase.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLowerCase.Location = new System.Drawing.Point(439, 118);
            this.chkLowerCase.Name = "chkLowerCase";
            this.chkLowerCase.Size = new System.Drawing.Size(77, 17);
            this.chkLowerCase.TabIndex = 25;
            this.chkLowerCase.Text = "lower case";
            this.chkLowerCase.UseVisualStyleBackColor = true;
            this.chkLowerCase.CheckedChanged += new System.EventHandler(this.chkLowerCase_CheckedChanged);
            // 
            // chkUpperCase
            // 
            this.chkUpperCase.AutoSize = true;
            this.chkUpperCase.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUpperCase.Location = new System.Drawing.Point(337, 118);
            this.chkUpperCase.Name = "chkUpperCase";
            this.chkUpperCase.Size = new System.Drawing.Size(94, 17);
            this.chkUpperCase.TabIndex = 24;
            this.chkUpperCase.Text = "UPPER CASE";
            this.chkUpperCase.UseVisualStyleBackColor = true;
            this.chkUpperCase.CheckedChanged += new System.EventHandler(this.chkUpperCase_CheckedChanged);
            // 
            // chkCamelCase
            // 
            this.chkCamelCase.AutoSize = true;
            this.chkCamelCase.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCamelCase.Location = new System.Drawing.Point(250, 118);
            this.chkCamelCase.Name = "chkCamelCase";
            this.chkCamelCase.Size = new System.Drawing.Size(79, 17);
            this.chkCamelCase.TabIndex = 23;
            this.chkCamelCase.Text = "CamelCase";
            this.chkCamelCase.UseVisualStyleBackColor = true;
            this.chkCamelCase.CheckedChanged += new System.EventHandler(this.chkCamelCase_CheckedChanged);
            // 
            // lblPOCO
            // 
            this.lblPOCO.AutoSize = true;
            this.lblPOCO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblPOCO.Location = new System.Drawing.Point(4, 5);
            this.lblPOCO.Name = "lblPOCO";
            this.lblPOCO.Size = new System.Drawing.Size(41, 13);
            this.lblPOCO.TabIndex = 0;
            this.lblPOCO.Text = "POCO";
            // 
            // chkCommentsWithoutNull
            // 
            this.chkCommentsWithoutNull.AutoSize = true;
            this.chkCommentsWithoutNull.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCommentsWithoutNull.Location = new System.Drawing.Point(87, 95);
            this.chkCommentsWithoutNull.Name = "chkCommentsWithoutNull";
            this.chkCommentsWithoutNull.Size = new System.Drawing.Size(82, 17);
            this.chkCommentsWithoutNull.TabIndex = 8;
            this.chkCommentsWithoutNull.Text = "Without null";
            this.chkCommentsWithoutNull.UseVisualStyleBackColor = true;
            this.chkCommentsWithoutNull.CheckedChanged += new System.EventHandler(this.chkCommentsWithoutNull_CheckedChanged);
            // 
            // chkComments
            // 
            this.chkComments.AutoSize = true;
            this.chkComments.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkComments.Location = new System.Drawing.Point(4, 95);
            this.chkComments.Name = "chkComments";
            this.chkComments.Size = new System.Drawing.Size(75, 17);
            this.chkComments.TabIndex = 7;
            this.chkComments.Text = "Comments";
            this.chkComments.UseVisualStyleBackColor = true;
            this.chkComments.CheckedChanged += new System.EventHandler(this.chkComments_CheckedChanged);
            // 
            // txtFixedClassName
            // 
            this.txtFixedClassName.Location = new System.Drawing.Point(321, 165);
            this.txtFixedClassName.Name = "txtFixedClassName";
            this.txtFixedClassName.Size = new System.Drawing.Size(100, 20);
            this.txtFixedClassName.TabIndex = 29;
            this.txtFixedClassName.TextChanged += new System.EventHandler(this.txtFixedClassName_TextChanged);
            // 
            // chkAllStructNullable
            // 
            this.chkAllStructNullable.AutoSize = true;
            this.chkAllStructNullable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAllStructNullable.Location = new System.Drawing.Point(4, 72);
            this.chkAllStructNullable.Name = "chkAllStructNullable";
            this.chkAllStructNullable.Size = new System.Drawing.Size(127, 17);
            this.chkAllStructNullable.TabIndex = 6;
            this.chkAllStructNullable.Text = "Struct Types Nullable";
            this.chkAllStructNullable.UseVisualStyleBackColor = true;
            this.chkAllStructNullable.CheckedChanged += new System.EventHandler(this.chkAllStructNullable_CheckedChanged);
            // 
            // chkVirtualProperties
            // 
            this.chkVirtualProperties.AutoSize = true;
            this.chkVirtualProperties.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkVirtualProperties.Location = new System.Drawing.Point(4, 47);
            this.chkVirtualProperties.Name = "chkVirtualProperties";
            this.chkVirtualProperties.Size = new System.Drawing.Size(105, 17);
            this.chkVirtualProperties.TabIndex = 4;
            this.chkVirtualProperties.Text = "Virtual Properties";
            this.chkVirtualProperties.UseVisualStyleBackColor = true;
            this.chkVirtualProperties.CheckedChanged += new System.EventHandler(this.chkVirtualProperties_CheckedChanged);
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblClassName.Location = new System.Drawing.Point(250, 5);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(73, 13);
            this.lblClassName.TabIndex = 0;
            this.lblClassName.Text = "Class Name";
            // 
            // rdbDataMembers
            // 
            this.rdbDataMembers.AutoSize = true;
            this.rdbDataMembers.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdbDataMembers.Location = new System.Drawing.Point(84, 24);
            this.rdbDataMembers.Name = "rdbDataMembers";
            this.rdbDataMembers.Size = new System.Drawing.Size(94, 17);
            this.rdbDataMembers.TabIndex = 3;
            this.rdbDataMembers.Text = "Data Members";
            this.rdbDataMembers.UseVisualStyleBackColor = true;
            this.rdbDataMembers.CheckedChanged += new System.EventHandler(this.rdbDataMembers_CheckedChanged);
            // 
            // rdbProperties
            // 
            this.rdbProperties.AutoSize = true;
            this.rdbProperties.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rdbProperties.Checked = true;
            this.rdbProperties.Location = new System.Drawing.Point(4, 24);
            this.rdbProperties.Name = "rdbProperties";
            this.rdbProperties.Size = new System.Drawing.Size(72, 17);
            this.rdbProperties.TabIndex = 2;
            this.rdbProperties.TabStop = true;
            this.rdbProperties.Text = "Properties";
            this.rdbProperties.UseVisualStyleBackColor = true;
            this.rdbProperties.CheckedChanged += new System.EventHandler(this.rdbProperties_CheckedChanged);
            // 
            // POCOGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1054, 712);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(1070, 750);
            this.Name = "POCOGeneratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POCO Generator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.POCOGeneratorForm_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuServerTree.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuPocoEditor.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView trvServer;
        private System.Windows.Forms.ImageList imageListDbObjects;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox txtPocoEditor;
        private System.Windows.Forms.ContextMenuStrip contextMenuPocoEditor;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.RadioButton rdbProperties;
        private System.Windows.Forms.RadioButton rdbDataMembers;
        private System.Windows.Forms.CheckBox chkVirtualProperties;
        private System.Windows.Forms.CheckBox chkAllStructNullable;
        private System.Windows.Forms.CheckBox chkComments;
        private System.Windows.Forms.CheckBox chkCommentsWithoutNull;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.TextBox txtFixedClassName;
        private System.Windows.Forms.CheckBox chkLowerCase;
        private System.Windows.Forms.CheckBox chkUpperCase;
        private System.Windows.Forms.CheckBox chkCamelCase;
        private System.Windows.Forms.TextBox txtWordsSeparator;
        private System.Windows.Forms.Label lblWordsSeparator;
        private System.Windows.Forms.Label lblPOCO;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.CheckBox chkSingleFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label lblExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTypeMapping;
        private System.Windows.Forms.Label lblWordsSeparatorDesc;
        private System.Windows.Forms.Label lblFixedName;
        private System.Windows.Forms.TextBox txtDBSeparator;
        private System.Windows.Forms.Label lblDBSeparator;
        private System.Windows.Forms.CheckBox chkIncludeDB;
        private System.Windows.Forms.TextBox txtSchemaSeparator;
        private System.Windows.Forms.Label lblSchemaSeparator;
        private System.Windows.Forms.CheckBox chkIgnoreDboSchema;
        private System.Windows.Forms.CheckBox chkIncludeSchema;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.CheckBox chkUsing;
        private System.Windows.Forms.CheckBox chkSingular;
        private System.Windows.Forms.CheckBox chkPetaPoco;
        private System.Windows.Forms.CheckBox chkPetaPocoExplicitColumns;
        private System.Windows.Forms.Label lblORMAnnotations;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.CheckBox chkPartialClass;
        private System.Windows.Forms.CheckBox chkEFCodeFirst;
        private System.Windows.Forms.CheckBox chkEFCodeFirstRequired;
        private System.Windows.Forms.CheckBox chkEFCodeFirstColumn;
        private System.Windows.Forms.Label lblORMAnnotationsTablesDesc;
        private System.Windows.Forms.Label lblSingularDesc;
        private System.Windows.Forms.ContextMenuStrip contextMenuServerTree;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterSettingsToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkIgnoreCase;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Label lblWith;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblReplace;
    }
}