using DatabaseUtlityTool;
using System;
using System.Windows.Forms;

namespace DatabaseUtilityTool
{
    public partial class frmMaster : Form
    {

        #region Form Object Declaration

        SampleDockManager frmDockManager;
        frmApplication frmApplication;
        frmTest frmTest;
        frmSettings frmSettings;
        frmConnectionManager frmConnectionMgr;

        #endregion

        #region methods

        public void MdiChild(Form p_childCtrl, Form p_mdiCtrl)
        {
            try
            {
                foreach (Form _objFrm in p_mdiCtrl.MdiChildren)
                {
                    if (_objFrm.Name == p_childCtrl.Name)
                    {
                        //p_childCtrl.Visible = false;
                        _objFrm.Activate();
                        //p_childCtrl.Visible = true;
                        return;
                    }
                }
                p_childCtrl.MdiParent = p_mdiCtrl;
                //p_childCtrl.Visible = false;
                p_childCtrl.WindowState = FormWindowState.Maximized;
                p_childCtrl.Show();
                //p_childCtrl.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Constructor

        public frmMaster()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplication = new frmApplication();
            MdiChild(frmApplication, this);
        }

        private void testFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTest = new frmTest();
            MdiChild(frmTest, this);
        }

        private void sampleDockManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDockManager = new SampleDockManager();
            MdiChild(frmDockManager, this);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings = new frmSettings();
            MdiChild(frmSettings, this);
        }

        private void connectionManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConnectionMgr = new frmConnectionManager();
            MdiChild(frmConnectionMgr, this);
        }

        #endregion

    }
}
