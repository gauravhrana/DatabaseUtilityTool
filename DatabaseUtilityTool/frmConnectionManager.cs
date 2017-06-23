using System;
using System.Windows.Forms;
using DatabaseUtilityTool.Components.Core;

namespace DatabaseUtilityTool
{
    public partial class frmConnectionManager : Form
    {

        #region private variables

        private string ApplicationConnectionString
        {
            get
            {
                return ApplicationConstants.ApplicationConnectionString;
            }
        }

        #endregion

        #region Constructor

        public frmConnectionManager()
        {
            InitializeComponent();
        }

        public frmConnectionManager(string server, string database, string user, string password)
        {
            InitializeComponent();
            txtServer.Text = server;
            txtDatabase.Text = database;
            txtUser.Text = user;
            txtPassword.Text = password;
            btnConnect.Select(); 
            btnConnect.Focus();
        }

        #endregion

        #region private methods

        private void LoadConnections()
        {
            var dt = Connection.GetList();
            ultraGridConnections.DataSource = dt;
        }

        private bool checkConnection()
        {
            try
            {
                // I think this has to be done earlier in static call somwhere ...
                // put into local variable instead of tall this inline calls
                // makes it easier to understand.  Compiler will taker care of performance where applicable.
                //Framework.Components.DataAccess.StartUp.EntryPoint(((Form1)(this.Owner)).path, ((Form1)(this.Owner)).timeout, ((Form1)(this.Owner)).ConnectionString);

                //var data          = new Connection.Data();
                //data.Server       = txtServer.Text.Trim();
                //data.DatabaseName = txtDatabase.Text.Trim();
                //data.UserName     = txtUser.Text.Trim();
                //var dt            = Connection.DoesExist(data);

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    ((Form1)(this.Owner)).ConnectionId = Convert.ToInt32(dt.Rows[0]["ConnectionId"]);
                //}
                //else
                //{
                //    ((Form1)(this.Owner)).ConnectionId = Connection.Create(data, ApplicationConnectionString);
                //}

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        #endregion

        #region Events

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ((frmApplication)(this.Owner)).Server = txtServer.Text.Trim();
            ((frmApplication)(this.Owner)).Database = txtDatabase.Text.Trim();
            ((frmApplication)(this.Owner)).User = txtUser.Text.Trim();
            ((frmApplication)(this.Owner)).Password = txtPassword.Text.Trim();
            if (checkConnection())
            {
                ((frmApplication)(this.Owner)).LoadTreeView();
                this.Close();
            }
            else
            {
                ((frmApplication)(this.Owner)).Server = string.Empty;
                ((frmApplication)(this.Owner)).Database = string.Empty;
                ((frmApplication)(this.Owner)).User = string.Empty;
                ((frmApplication)(this.Owner)).Password = string.Empty;
            }
        }

        private void frmConnectionManager_Load(object sender, EventArgs e)
        {
            LoadConnections();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
