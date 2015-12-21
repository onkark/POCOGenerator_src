namespace POCOGenerator
{
    partial class ConnectionForm
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
            this.lblServer = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.chkOverrideConnectionString = new System.Windows.Forms.CheckBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtServer = new System.Windows.Forms.ComboBox();
            this.lblAuthentication = new System.Windows.Forms.Label();
            this.ddlAuthentication = new System.Windows.Forms.ComboBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.chkDatabaseAll = new System.Windows.Forms.CheckBox();
            this.txtDatabase = new System.Windows.Forms.ComboBox();
            this.btnDatabaseRefresh = new System.Windows.Forms.Button();
            this.lblLoadingServers = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(12, 9);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(38, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(12, 67);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(60, 13);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "User Name";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 96);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(140, 63);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(274, 20);
            this.txtUserName.TabIndex = 3;
            this.txtUserName.Text = "cooApplication";
            this.txtUserName.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(140, 92);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(274, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.Text = "cooApp#22";
            this.txtPassword.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.Enabled = false;
            this.txtConnectionString.Location = new System.Drawing.Point(140, 150);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(432, 20);
            this.txtConnectionString.TabIndex = 6;
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(12, 154);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(91, 13);
            this.lblConnectionString.TabIndex = 9;
            this.lblConnectionString.Text = "Connection String";
            // 
            // chkOverrideConnectionString
            // 
            this.chkOverrideConnectionString.AutoSize = true;
            this.chkOverrideConnectionString.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverrideConnectionString.Location = new System.Drawing.Point(119, 153);
            this.chkOverrideConnectionString.Name = "chkOverrideConnectionString";
            this.chkOverrideConnectionString.Size = new System.Drawing.Size(15, 14);
            this.chkOverrideConnectionString.TabIndex = 5;
            this.chkOverrideConnectionString.UseVisualStyleBackColor = true;
            this.chkOverrideConnectionString.CheckedChanged += new System.EventHandler(this.chkOverrideConnectionString_CheckedChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConnect.AutoSize = true;
            this.btnConnect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConnect.Location = new System.Drawing.Point(12, 182);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(57, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSize = true;
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(529, 182);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(43, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtServer
            // 
            this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServer.FormattingEnabled = true;
            this.txtServer.Location = new System.Drawing.Point(140, 5);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(274, 21);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "WDVWD99A0572\\ABF1_DEV";
            this.txtServer.SelectedIndexChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtServer.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // lblAuthentication
            // 
            this.lblAuthentication.AutoSize = true;
            this.lblAuthentication.Location = new System.Drawing.Point(12, 38);
            this.lblAuthentication.Name = "lblAuthentication";
            this.lblAuthentication.Size = new System.Drawing.Size(75, 13);
            this.lblAuthentication.TabIndex = 13;
            this.lblAuthentication.Text = "Authentication";
            // 
            // ddlAuthentication
            // 
            this.ddlAuthentication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlAuthentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAuthentication.FormattingEnabled = true;
            this.ddlAuthentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.ddlAuthentication.Location = new System.Drawing.Point(140, 34);
            this.ddlAuthentication.Name = "ddlAuthentication";
            this.ddlAuthentication.Size = new System.Drawing.Size(274, 21);
            this.ddlAuthentication.TabIndex = 2;
            this.ddlAuthentication.SelectedIndexChanged += new System.EventHandler(this.ddlAuthentication_SelectedIndexChanged);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTest.AutoSize = true;
            this.btnTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnTest.Location = new System.Drawing.Point(87, 182);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(95, 23);
            this.btnTest.TabIndex = 8;
            this.btnTest.Text = "Test Connection";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(12, 125);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(53, 13);
            this.lblDatabase.TabIndex = 14;
            this.lblDatabase.Text = "Database";
            // 
            // chkDatabaseAll
            // 
            this.chkDatabaseAll.AutoSize = true;
            this.chkDatabaseAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDatabaseAll.Location = new System.Drawing.Point(97, 123);
            this.chkDatabaseAll.Name = "chkDatabaseAll";
            this.chkDatabaseAll.Size = new System.Drawing.Size(37, 17);
            this.chkDatabaseAll.TabIndex = 15;
            this.chkDatabaseAll.Text = "All";
            this.chkDatabaseAll.UseVisualStyleBackColor = true;
            this.chkDatabaseAll.CheckedChanged += new System.EventHandler(this.chkDatabaseAll_CheckedChanged);
            // 
            // txtDatabase
            // 
            this.txtDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDatabase.Enabled = false;
            this.txtDatabase.FormattingEnabled = true;
            this.txtDatabase.Location = new System.Drawing.Point(140, 121);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(274, 21);
            this.txtDatabase.TabIndex = 16;
            this.txtDatabase.SelectedIndexChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtDatabase.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // btnDatabaseRefresh
            // 
            this.btnDatabaseRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDatabaseRefresh.AutoSize = true;
            this.btnDatabaseRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDatabaseRefresh.Location = new System.Drawing.Point(430, 120);
            this.btnDatabaseRefresh.Name = "btnDatabaseRefresh";
            this.btnDatabaseRefresh.Size = new System.Drawing.Size(54, 23);
            this.btnDatabaseRefresh.TabIndex = 17;
            this.btnDatabaseRefresh.Text = "Refresh";
            this.btnDatabaseRefresh.UseVisualStyleBackColor = true;
            this.btnDatabaseRefresh.Click += new System.EventHandler(this.btnDatabaseRefresh_Click);
            // 
            // lblLoadingServers
            // 
            this.lblLoadingServers.AutoSize = true;
            this.lblLoadingServers.Location = new System.Drawing.Point(422, 9);
            this.lblLoadingServers.Name = "lblLoadingServers";
            this.lblLoadingServers.Size = new System.Drawing.Size(91, 13);
            this.lblLoadingServers.TabIndex = 0;
            this.lblLoadingServers.Text = "Loading servers...";
            this.lblLoadingServers.Visible = false;
            // 
            // ConnectionForm
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(584, 214);
            this.Controls.Add(this.lblLoadingServers);
            this.Controls.Add(this.btnDatabaseRefresh);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.chkDatabaseAll);
            this.Controls.Add(this.lblDatabase);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.ddlAuthentication);
            this.Controls.Add(this.lblAuthentication);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.chkOverrideConnectionString);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to Server";
            this.Load += new System.EventHandler(this.ConnectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.CheckBox chkOverrideConnectionString;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox txtServer;
        private System.Windows.Forms.Label lblAuthentication;
        private System.Windows.Forms.ComboBox ddlAuthentication;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.CheckBox chkDatabaseAll;
        private System.Windows.Forms.ComboBox txtDatabase;
        private System.Windows.Forms.Button btnDatabaseRefresh;
        private System.Windows.Forms.Label lblLoadingServers;
    }
}

