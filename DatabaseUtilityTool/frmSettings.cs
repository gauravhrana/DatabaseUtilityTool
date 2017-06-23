using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Configuration;

namespace DatabaseUtilityTool
{
    public partial class frmSettings : Form
    {

        #region private variables

        int? resultGridId = null;
        int? textEditorColorId = null;
        int? showLineNumberId = null;
        int? noOfRecordsReturnedId = null;

        private int UserPreferenceCategoryId
        {
            get
            {
                return 73;
            }
        }

        private int ApplicationId
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationId"]);
            }
        }

        private int ApplicationUserId
        {
            get
            {
                return 11;
            }
        }

        #endregion

        #region Constructor

        public frmSettings()
        {
            InitializeComponent();
        }

        #endregion

        #region methods
        private void LoadShortcuts()
        {
            var dt = Components.Core.Shortcut.GetList();
            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    dt.Columns["Name"].ReadOnly = true;
                    dt.Columns["Description"].ReadOnly = true;
                }
                catch { }
                ultraGridShortcuts.DataSource = dt;
            }
            else
            {
                ultraGridShortcuts.DataSource = null;
            }
        }

        private void LoadAndSetColors()
        {
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                cmbTextBoxBackColor.Items.Add(c.Name);
            }
        }

        #endregion

        #region Events

        private void frmSettings_Load(object sender, EventArgs e)
        {
            LoadAndSetColors();
            LoadShortcuts();
        }

        private void cmbGridResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void UpdateUserPreference(int? userPreferenceId, string value)
        {
            //var data = new UserPreference.Data();
            //data.UserPreferenceId = userPreferenceId;
            //var dt = UserPreference.GetDetails(data, ApplicationUserId);
            //if (dt != null & dt.Rows.Count > 0)
            //{
            //    data.ApplicationUserId = Convert.ToInt32(dt.Rows[0]["ApplicationUserId"]);
            //    data.UserPreferenceKeyId = Convert.ToInt32(dt.Rows[0]["UserPreferenceKeyId"]);
            //    data.UserPreferenceCategoryId = Convert.ToInt32(dt.Rows[0]["UserPreferenceCategoryId"]);
            //    data.DataTypeId = Convert.ToInt32(dt.Rows[0]["DataTypeId"]);
            //    data.ApplicationId = ApplicationId;
            //    data.Value = value;
            //    UserPreference.Update(data, ApplicationUserId);
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Update Result Grid Setting                
                UpdateUserPreference(resultGridId, cmbGridResult.Text);

                //Update Text Box Background Color
                UpdateUserPreference(textEditorColorId, cmbTextBoxBackColor.Text);
                try
                {
                    ((frmApplication)this.Owner).ChangeTextEditorBackgroundColor(cmbTextBoxBackColor.Text);
                }
                catch { }

                //Update Show Line Number Setting
                UpdateUserPreference(showLineNumberId, cmbLineNumbers.Text);
                try
                {
                    //((Form1)this.Owner).ShowLineNumbersInTextEditor(cmbLineNumbers.Text);
                }
                catch { }

                //Update No Of Records Returned Setting
                UpdateUserPreference(noOfRecordsReturnedId, txtRecordReturned.Text);
                try
                {
                    //((Form1)this.Owner).ChangeTextEditorBackgroundColor(txtRecordReturned.Text);
                }
                catch { }

                MessageBox.Show("Settings saved Successfully");



            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void frmSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void ultraGridShortcuts_KeyDown(object sender, KeyEventArgs e)
        {
            if (ultraGridShortcuts.ActiveCell != null)
            {
                if (ultraGridShortcuts.ActiveCell.Column.Key == "Value")
                {
                    e.Handled = true;




                }
            }
        }

        // put mouse cursor in a textbox to see keys as they are typed.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ultraGridShortcuts.ActiveCell != null)
            {
                if (ultraGridShortcuts.ActiveCell.Column.Key == "Value")
                {
                    /*
                    // was Message a keypress?, this method should always see 256 anyway
                    if (msg.Msg == 256) // OS always sends 256, user code sends ???
                    {
                        switch (keyData)
                        {
                            //case Keys.Control | Keys.A:
                            //    this.label1.Text = "Ctrl-A was pressed.";
                            //    break;
                            //case Keys.Control | Keys.Shift | Keys.U:
                            //    this.label1.Text = "Ctrl-Shift-U was pressed.";
                            //    break;
                            //case Keys.Up: // Keys.Down, Keys.Right, Keys.Left
                            //    this.Text = "Up Arrow was pressed.";
                            //    break;
                            //case Keys.Shift | Keys.ShiftKey | Keys.Control:
                            //case Keys.ControlKey | Keys.Control:
                            //case Keys.ShiftKey | Keys.Shift:
                            // break; // code above ignores modifiers-only key presses
                            default:
                                break;
                        }

                        ultraGridShortcuts.ActiveCell.Value = keyData.ToString();
                    }
                    */

                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        private void cmbTextBoxBackColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

    }
}
