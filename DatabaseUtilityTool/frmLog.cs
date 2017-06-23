using System;
using System.Data;
using System.Windows.Forms;
using DatabaseUtilityTool.Components.Core;

namespace DatabaseUtilityTool
{
    public partial class frmLog : Form
    {
        private ScintillaNET.Scintilla txtLog;

        private int ConnectionId
        {
            get;
            set;
        }

        public frmLog()
        {
            InitializeComponent();
        }

        public frmLog(int connectionId)
        {
            InitializeComponent();
            this.ConnectionId = connectionId;
        }

        private void Setup()
        {
            txtLog = new ScintillaNET.Scintilla();

            this.panel1.Controls.Add(this.txtLog);

            ((System.ComponentModel.ISupportInitialize)(this.txtLog)).BeginInit();
            txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            txtLog.Location = new System.Drawing.Point(0, 0);
            txtLog.Name = "txtLog";
            txtLog.Size = new System.Drawing.Size(1256, 329);
            txtLog.TabIndex = 0;
            ((System.ComponentModel.ISupportInitialize)(this.txtLog)).EndInit();
        }

        private void frmLog_Load(object sender, EventArgs e)
        {
            try
            {
                Setup();

                txtLog.ConfigurationManager.Language = "mssql";
                txtLog.ConfigurationManager.Configure();
                //txtLog.Text = string.Empty;
                //var filePath = @"C:\Temp\DB Tool\";
                //var fileName = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".txt";
                //if (!Directory.Exists(filePath))
                //{
                //    Directory.CreateDirectory(filePath);
                //}
                //if (File.Exists(filePath + fileName))
                //{
                //    txtLog.Text = File.ReadAllText(filePath + fileName);
                //}

                var data = new QueryLog.Data();
                data.ConnectionId = ConnectionId;
                data.FromDate = DateTime.Today;

                var logList = QueryLog.Search(data);
                if (logList != null && logList.Rows.Count > 0)
                {
                    foreach (DataRow dr in logList.Rows)
                    {
                        var dt = Convert.ToDateTime(dr["CreatedDate"]);
                        txtLog.AppendText("--------Executed on: " + dt.ToString("MM-dd-yyyy") + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second + "-------\n");
                        txtLog.AppendText(Convert.ToString(dr["QueryText"]) + "\n");
                        txtLog.AppendText("\n");
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
