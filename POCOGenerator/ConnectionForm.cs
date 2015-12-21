using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using POCOGenerator.DbObject;

namespace POCOGenerator
{
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            ddlAuthentication.SelectedIndex = 1;
            //txtUserName.Text = Environment.MachineName + "\\" + Environment.UserName;
            txtConnectionString.Text = ConnectionString;

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += delegate(object sender1, EventArgs e1)
            {
                timer.Enabled = false;
                GetServers();
            };
            timer.Enabled = true;
        }

        private void GetServers()
        {
            try
            {
                lblLoadingServers.Visible = true;
                Application.DoEvents();

                List<Server> servers = DbHelper.GetServers();
                if (servers != null && servers.Count > 0)
                {
                    txtServer.Items.AddRange(servers.OrderBy<Server, string>(s => s.ToString()).ToArray());
                    txtServer.SelectedIndex = 0;
                    txtConnectionString.Text = ConnectionString;
                }
            }
            catch
            {
            }
            finally
            {
                lblLoadingServers.Visible = false;
            }
        }

        private void btnDatabaseRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ConnectionStringWithoutCatalog))
                return;

            GetDatabasesAsync();
        }

        private void GetDatabasesAsync()
        {
            new Thread(new ThreadStart(delegate()
            {
                this.Invoke(new Action(GetDatabases));
            })).Start();
        }

        private void GetDatabases()
        {
            try
            {
                DbHelper.ConnectionString = ConnectionStringWithoutCatalog;
                List<Database> databases = DbHelper.GetDatabases();
                txtDatabase.Items.Clear();
                if (databases != null && databases.Count > 0)
                {
                    txtDatabase.Items.AddRange(databases.OrderBy<Database, string>(d => d.ToString()).ToArray());
                    txtDatabase.SelectedIndex = 0;
                    txtConnectionString.Text = ConnectionString;
                }
            }
            catch
            {
            }
        }

        public string ConnectionStringWithoutCatalog
        {
            get
            {
                string connectionString = null;

                if (chkOverrideConnectionString.Checked)
                    return txtConnectionString.Text;

                if (string.IsNullOrEmpty(txtServer.Text))
                    return null;
                connectionString += string.Format("Server={0};", txtServer.Text);

                if (ddlAuthentication.SelectedIndex == 0)
                {
                    connectionString += "Integrated Security=SSPI;";
                }
                else if (ddlAuthentication.SelectedIndex == 1)
                {
                    if (string.IsNullOrEmpty(txtUserName.Text) == false)
                        connectionString += string.Format("User Id={0};", txtUserName.Text);

                    if (string.IsNullOrEmpty(txtPassword.Text) == false)
                        connectionString += string.Format("Password={0};", txtPassword.Text);
                }

                return connectionString;
            }
        }

        public string ConnectionString
        {
            get
            {
                string connectionString = ConnectionStringWithoutCatalog;

                if (string.IsNullOrEmpty(connectionString))
                    return null;

                if (chkOverrideConnectionString.Checked)
                    return connectionString;

                if (chkDatabaseAll.Checked == false)
                {
                    if (txtDatabase.SelectedIndex != -1)
                    {
                        Database database = (Database)txtDatabase.SelectedItem;
                        connectionString += string.Format("Initial Catalog={0};", database.database_name);
                    }
                    else if (string.IsNullOrEmpty(txtDatabase.Text) == false)
                    {
                        connectionString += string.Format("Initial Catalog={0};", txtDatabase.Text);
                    }
                }

                return connectionString;
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (chkOverrideConnectionString.Checked == false)
                txtConnectionString.Text = ConnectionString;
        }

        private void ddlAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAuthentication.SelectedIndex == 0)
            {
                lblUserName.Text = "User Name";
                lblUserName.Enabled = false;
                txtUserName.Enabled = false;
                lblPassword.Enabled = false;
                txtPassword.Enabled = false;
                txtUserName.Text = Environment.MachineName + "\\" + Environment.UserName;
                txtPassword.Text = string.Empty;
            }
            else if (ddlAuthentication.SelectedIndex == 1)
            {
                lblUserName.Text = "Login";
                lblUserName.Enabled = true;
                txtUserName.Enabled = true;
                lblPassword.Enabled = true;
                txtPassword.Enabled = true;
                //txtUserName.Text = string.Empty;
                //txtPassword.Text = string.Empty;
            }
        }

        private void chkOverrideConnectionString_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOverrideConnectionString.Checked == false)
                txtConnectionString.Text = ConnectionString;
            txtConnectionString.Enabled = chkOverrideConnectionString.Checked;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool TestConnection()
        {
            try
            {
                DbHelper.ConnectionString = ConnectionString;
                return DbHelper.TestConnection();
            }
            catch
            {
                return false;
            }
        }

        private void chkDatabaseAll_CheckedChanged(object sender, EventArgs e)
        {
            txtDatabase.Enabled = !chkDatabaseAll.Checked;
            txtConnectionString.Text = ConnectionString;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ConnectionString))
                return;

            if (TestConnection())
                MessageBox.Show("Connection Succeeded" + Environment.NewLine + ConnectionString, "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Connection Failed" + Environment.NewLine + ConnectionString, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ConnectionString))
                return;

            if (TestConnection() == false)
            {
                MessageBox.Show("Connection Failed" + Environment.NewLine + ConnectionString, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Server server = null;
            if (txtServer.SelectedIndex != -1)
            {
                server = (Server)txtServer.SelectedItem;
            }
            else
            {
                server = new Server();
                server.ServerName = txtServer.Text;
            }

            string initialCatalog = null;
            if (chkDatabaseAll.Checked == false)
            {
                if (txtDatabase.SelectedIndex != -1)
                {
                    initialCatalog = ((Database)txtDatabase.SelectedItem).database_name;
                }
                else
                {
                    initialCatalog = txtDatabase.Text;
                }
            }

            this.Hide();
            var form = new POCOGeneratorForm(server, initialCatalog, this);
            form.Show();
        }
    }
}
