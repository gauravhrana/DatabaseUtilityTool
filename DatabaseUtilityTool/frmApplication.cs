using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseUtilityTool.Components.Core;
using Infragistics.Win.UltraWinTree;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;


namespace DatabaseUtilityTool
{
    public partial class frmApplication : Form
    {

        #region Constructor

        public frmApplication()
        {
            InitializeComponent();
            ApplicationUserId = 100;
        }

        #endregion

        #region public variables

        public string path = @"C:\Temp\";
        public int timeout = 2400;

        public int ConnectionId
        {
            get;
            set;
        }

        public string Server
        {
            get;
            set;
        }

        public string Database
        {
            get;
            set;
        }

        public string User
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string ConnectionString
        {
            get
            {
                return @"Data Source=" + Server + ";Initial Catalog=" + Database + ";Persist Security Info=True;User ID=" + User + ";Password=" + Password + ";Connect Timeout=0";
                //return @"Data Source=IVR-SQL-01\SQL01;Initial Catalog=TaskTimeTracker;Persist Security Info=True;User ID=706;Password=Welcome1;Connect Timeout=0";
            }
        }

        #endregion

        #region private variables

        int NoOfRecordsReturned;

        bool ShowLineNumbers;

        int ApplicationUserId;

        private ObjectTree objTree;

        private ScintillaNET.Scintilla txtLog;

        private string ApplicationConnectionString
        {
            get
            {
                return ApplicationConstants.ApplicationConnectionString;
            }
        }

        private int ApplicationId
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationId"]);
            }
        }

        private Dictionary<string, string> editorResultPair = new Dictionary<string, string>();
        UltraTreeNode tNodeNewGrpClicked = null;
        //public static string ConnectionString = @"Data Source=IVR-SQL-01\SQL01;Initial Catalog=TaskTimeTracker;Persist Security Info=True;User ID=706;Password=Welcome1;Connect Timeout=0";

        #endregion

        #region methods

        private void ResetHeader(List<UltraGrid> lstGrids)
        {
            foreach (UltraGrid uGrid in lstGrids)
            {
                try
                {
                    DataTable dt = (DataTable)uGrid.DataSource;
                    for (int iCount = 0; iCount < dt.Columns.Count; iCount++)
                    {
                        var type = SqlColumnToColumn(dt.Columns[iCount].DataType.Name);
                        var column = dt.Columns[iCount].ColumnName;

                        uGrid.DisplayLayout.Bands[0].Columns[iCount].Header.Caption = column + "\n" + type;
                    }
                }
                catch { }
            }
        }

        private string SqlColumnToColumn(string columnName)
        {
            var result = columnName;
            result = result.Replace("Sql", "");
            result = result.Replace("32", "");
            result = result.Replace("64", "");
            return result;
        }

        private void FormatQuery()
        {
            if (txtQueryEditor.Lines.Current != null)
            {
                if (txtQueryEditor.Lines.Current.Text.ToLower().Contains("from") && txtQueryEditor.Lines.Current.Text.ToLower().Trim().LastIndexOf("from") > 0)
                {
                    var lineText = txtQueryEditor.Lines.Current.Text;
                    var fromIndex = txtQueryEditor.Lines.Current.Text.ToLower().LastIndexOf("from");

                    var beforeFromText = txtQueryEditor.Lines.Current.Text.Substring(0, fromIndex - 1);
                    var afterFromText = txtQueryEditor.Lines.Current.Text.Substring(fromIndex);
                    var selectIndex = 0;
                    var newLineText = string.Empty;
                    if (beforeFromText.ToLower().Contains("select"))
                    {
                        selectIndex = beforeFromText.ToLower().LastIndexOf("select");
                        for (int iCount = 0; iCount < selectIndex; iCount++)
                        {
                            if (beforeFromText[iCount] == '\t')
                                newLineText += "\t";
                            else
                                newLineText += " ";
                        }
                    }
                    newLineText += afterFromText.Trim() + " ";
                    //newLines.Add(beforeFromText + "\n" + newLineText);
                    //newLineNumbers.Add(i);
                    txtQueryEditor.Lines.Current.Text = beforeFromText + "\n" + newLineText;
                    txtQueryEditor.Refresh();
                    if (txtQueryEditor.Lines.Current.Next != null)
                    {
                        txtQueryEditor.CurrentPos = txtQueryEditor.Lines.Current.Next.EndPosition;
                    }
                }
            }
        }

        public void ChangeTextEditorBackgroundColor(string colorName)
        {
            txtQueryEditor.BackColor = Color.FromName(colorName);
            txtQueryEditor.Refresh();
        }

        public void ChangeNoOfRecordsReturned(string noOfRecords)
        {
            NoOfRecordsReturned = Convert.ToInt32(noOfRecords);
        }

        public void ShowLineNumbersInTextEditor(string flag)
        {
            ShowLineNumbers = Convert.ToBoolean(flag);
            if (ShowLineNumbers)
            {
                txtQueryEditor.Margins[0].Width = 35;
            }
            else
            {
                txtQueryEditor.Margins[0].Width = 0;
            }
            txtQueryEditor.Refresh();
        }

        private void InitializeTextLog()
        {
            txtLog = new ScintillaNET.Scintilla();

            this.pnlLogContainer.Controls.Add(this.txtLog);

            ((System.ComponentModel.ISupportInitialize)(this.txtLog)).BeginInit();
            txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            txtLog.Location = new System.Drawing.Point(0, 0);
            txtLog.Name = "txtLog";
            ((System.ComponentModel.ISupportInitialize)(this.txtLog)).EndInit();
            txtLog.ConfigurationManager.Language = "mssql";
            txtLog.ConfigurationManager.Configure();
        }

        private void LogSQL(string sql)
        {
            //var filePath = @"C:\Temp\DB Tool\";
            //var fileName = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".txt";
            //if (!Directory.Exists(filePath))
            //{
            //    Directory.CreateDirectory(filePath);
            //}

            //StreamWriter sw = null;
            //if (!File.Exists(filePath + fileName))
            //{
            //    sw = File.CreateText(filePath + fileName);
            //}
            //else
            //{
            //    sw = File.AppendText(filePath + fileName);
            //}

            //sw.WriteLine("--------Executed on: " + DateTime.Now.ToString("MM-dd-yyyy") + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "--------");
            //sw.WriteLine(sql);
            //sw.WriteLine("");

            //sw.Close();

            var data = new QueryLog.Data();
            data.ConnectionId = ConnectionId;
            data.ViewId = 0;
            data.QueryText = sql;
            data.CreatedDate = DateTime.Now;
            QueryLog.Create(data, ApplicationConnectionString);
        }

        private void LoadSQLLog()
        {
            txtLog.ResetText();
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

        private int? CheckCategoryExists(string nodeName, int? parentNodeId)
        {
            int? nodeId = null;

            var data = new Category.Data();
            data.ConnectionId = ConnectionId;
            data.Name = nodeName;
            if (parentNodeId == null)
            {
                data.ParentCategoryId = 0;
            }
            else
            {
                data.ParentCategoryId = parentNodeId;
            }

            var dt = Category.DoesExist(data);
            if (dt != null && dt.Rows.Count > 0)
            {
                nodeId = Convert.ToInt32(dt.Rows[0]["CategoryId"]);
            }
            return nodeId;
        }

        private int CreateCategory(string nodeName, int? parentNodeId)
        {
            int nodeId;

            var data = new Category.Data();
            data.ConnectionId = ConnectionId;
            data.Name = nodeName;
            if (parentNodeId != null)
            {
                data.ParentCategoryId = parentNodeId;
            }
            else
            {
                data.ParentCategoryId = 0;
            }
            data.CreatedDate = DateTime.Now;
            nodeId = Category.Create(data, ApplicationConnectionString);

            return nodeId;
        }

        private int CheckAndCreateRootCategory(string nodeName, int? parentNodeId)
        {
            int? nodeId = null;
            nodeId = CheckCategoryExists(nodeName, parentNodeId);
            if (nodeId == null)
            {
                nodeId = CreateCategory(nodeName, parentNodeId);
            }
            return nodeId.Value;
        }

        private void CheckAndCreateResult()
        {
            foreach (Control control in splitContainer2.Panel2.Controls[0].Controls)
            {
                control.Visible = false;
            }

            if (editorResultPair != null)
            {
                if (tabControlQueryEditors.TabPages.Count > 0)
                {
                    if (!editorResultPair.Keys.Contains(tabControlQueryEditors.SelectedTab.Name))
                    {
                        TabControl tabControl = new TabControl();
                        tabControl.Dock = DockStyle.Fill;
                        splitContainer2.Panel2.Controls[0].Controls.Add(tabControl);
                        tabControl.Name = tabControlQueryEditors.SelectedTab.Name.Replace("obj_", "tab_");

                        var tabTitle = "Result";
                        TabPage myTabPage = new TabPage(tabTitle);

                        DataGridView dgvResult1 = new DataGridView();
                        dgvResult1.AllowUserToAddRows = false;
                        dgvResult1.AllowUserToDeleteRows = false;
                        dgvResult1.AutoGenerateColumns = true;
                        dgvResult1.ReadOnly = true;
                        dgvResult1.Dock = DockStyle.Fill;

                        myTabPage.Controls.Add(dgvResult1);
                        tabControl.TabPages.Add(myTabPage);

                        editorResultPair.Add(tabControlQueryEditors.SelectedTab.Name, tabControl.Name);
                    }
                    else
                    {
                        var tabName = Convert.ToString(editorResultPair[tabControlQueryEditors.SelectedTab.Name]);
                        var controls = splitContainer2.Panel2.Controls[0].Controls.Find(tabName, false);
                        if (controls != null && controls.Length > 0)
                        {
                            controls[0].Visible = true;
                        }
                    }
                }
            }
        }

        private string GetDragText(string objName, string objType)
        {
            var resultText = objName;
            if (objType == "table")
            {
                resultText = "SELECT * FROM dbo." + objName;

            }
            else if (objType == "proc")
            {
                var sql = "sp_helptext 'dbo." + objName + "'";
                var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, Database);
                var procText = new StringBuilder();
                if (oDT.DBTable != null)
                {
                    foreach (DataRow dr in oDT.DBTable.Rows)
                    {
                        procText.Append(Convert.ToString(dr["Text"]));
                    }
                }
                resultText = procText.ToString();
            }
            return resultText;
        }

        private int GetChildCount(string objType, int? categoryId)
        {
            int count = 0;
            var sql = string.Empty;
            if (objType != "group")
            {
                if (objType == "table")
                {
                    sql = "select * from sysobjects where xtype='u' ORDER BY name";
                }
                else if (objType == "proc")
                {
                    sql = "select * from sysobjects where xtype='p' ORDER BY name";
                }
                else if (objType == "view")
                {
                    sql = "select * from sysobjects where xtype='v' ORDER BY name";
                }
                else if (objType == "function")
                {
                    sql = "select * from sysobjects where xtype='fn' ORDER BY name";
                }


                var localConnectionString = ConfigurationManager.ConnectionStrings[Database];
                if (localConnectionString == null)
                {
                    AddNewConnectionString(Server, Database, User, Password);
                }

                var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, Database);

                if (oDT.DBTable != null)
                {
                    count = oDT.DBTable.Rows.Count;
                }
                if (count > 0)
                {
                    return count;
                }
            }

            if (categoryId != null)
            {
                // Get Child Category Count
                var data = new Category.Data();
                data.ParentCategoryId = categoryId;
                var dtCategory = Category.Search(data);
                if (dtCategory != null && dtCategory.Rows.Count > 0)
                {
                    count = dtCategory.Rows.Count;
                    return count;
                }

                // Get Child Object Items Count
                var dataItem = new CategoryItem.Data();
                dataItem.CategoryId = categoryId;
                var dtCategoryItem = CategoryItem.Search(dataItem);
                if (dtCategoryItem != null && dtCategoryItem.Rows.Count > 0)
                {
                    count = dtCategoryItem.Rows.Count;
                }
            }
            return count;
        }

        private void AddNewConnectionString(string serverName, string databaseName, string userName, string userPwd)
        {
            StringBuilder Con = new StringBuilder("Data Source=");
            Con.Append(serverName);
            Con.Append(";Initial Catalog=");
            Con.Append(databaseName);
            Con.Append(";Persist Security Info=True");
            Con.Append(";User ID=" + userName);
            Con.Append(";Password=" + userPwd);
            Con.Append(";Connect Timeout=0");

            //updating config file
            XmlDocument XmlDoc = new XmlDocument();
            //Loading the Config file
            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            foreach (XmlElement xElement in XmlDoc.DocumentElement)
            {
                if (xElement.Name == "connectionStrings")
                {
                    //setting the coonection string
                    //xElement.FirstChild.Attributes[1].Value = Con.ToString();
                    var xNewNode = xElement.FirstChild.Clone();
                    xNewNode.Attributes[0].Value = databaseName;
                    xNewNode.Attributes[1].Value = Con.ToString();
                    xElement.AppendChild(xNewNode);
                }
            }
            //writing the connection string in config file
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            //to refresh connection string each time else it will use             previous connection string
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        private int GetImageIndex(string nodeTag)
        {
            var imgIndex = 0;
            if (nodeTag == "table")
            {
                imgIndex = 2;
            }
            else if (nodeTag == "proc")
            {
                imgIndex = 3;
            }
            else if (nodeTag == "view")
            {
                imgIndex = 4;
            }
            else if (nodeTag == "function")
            {
                imgIndex = 5;
            }
            return imgIndex;
        }

        public void LoadTreeView()
        {
            LoadSQLLog();
            objTree.SetConnectionId(ConnectionId);
            objTree.SetConnectionString(ConnectionString);
            objTree.SetDatabase(Database);
            objTree.treeViewObjects.Nodes.Clear();
            // Create DB Root Node
            var rootNodeId = CheckAndCreateRootCategory(Database, null);
            var tDB = objTree.treeViewObjects.Nodes.Add(rootNodeId.ToString(), Database);
            tDB.Tag = "root";
            tDB.Override.NodeAppearance.Image = 0;

            // Create Tables Node
            var tableNodeId = CheckAndCreateRootCategory(Database + ".Tables", rootNodeId);
            var tNodeTables = tDB.Nodes.Add(tableNodeId.ToString(), "Tables");
            tNodeTables.Tag = "tables";
            tNodeTables.Override.NodeAppearance.Image = 1;
            if (GetChildCount("table", tableNodeId) > 0)
            {
                tNodeTables.Nodes.Add("Sample Table", "Sample Table");
            }

            // Create Stoed Procedure Node
            var procNodeId = CheckAndCreateRootCategory(Database + ".Stored Procedures", rootNodeId);
            var tNodeProcs = tDB.Nodes.Add(procNodeId.ToString(), "Stored Procedures");
            tNodeProcs.Tag = "procs";
            tNodeProcs.Override.NodeAppearance.Image = 1;
            if (GetChildCount("proc", procNodeId) > 0)
            {
                tNodeProcs.Nodes.Add("Sample Proc", "Sample Proc");
            }

            // Create Views Node
            var viewNodeId = CheckAndCreateRootCategory(Database + ".Views", rootNodeId);
            var tNodeViews = tDB.Nodes.Add(viewNodeId.ToString(), "Views");
            tNodeViews.Tag = "views";
            tNodeViews.Override.NodeAppearance.Image = 1;
            if (GetChildCount("view", viewNodeId) > 0)
            {
                tNodeViews.Nodes.Add("Sample View", "Sample View");
            }

            // Create Functions Node
            var functionNodeId = CheckAndCreateRootCategory(Database + ".Functions", rootNodeId);
            var tNodeFunctions = tDB.Nodes.Add(functionNodeId.ToString(), "Functions");
            tNodeFunctions.Tag = "functions";
            tNodeFunctions.Override.NodeAppearance.Image = 1;
            if (GetChildCount("function", functionNodeId) > 0)
            {
                tNodeFunctions.Nodes.Add("Sample Functions", "Sample Functions");
            }

            // Get Child Category
            var data = new Category.Data();
            data.ParentCategoryId = Convert.ToInt32(tDB.Key);
            var dtCategory = Category.Search(data);
            if (dtCategory != null && dtCategory.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCategory.Rows)
                {
                    var categoryName = Convert.ToString(dr["Name"]).ToLower();
                    if (!categoryName.Contains(".tables") && !categoryName.Contains(".stored procedures") && !categoryName.Contains(".views") && !categoryName.Contains(".functions"))
                    {
                        var tmpNode = tDB.Nodes.Add(Convert.ToString(dr["CategoryId"]), Convert.ToString(dr["Name"]));
                        tmpNode.Tag = "group";
                        tmpNode.Override.NodeAppearance.Image = 1;
                        int childCount = GetChildCount("group", Convert.ToInt32(dr["CategoryId"]));
                        if (childCount > 0)
                        {
                            tmpNode.Nodes.Add("Sample Group", "Sample Group");
                        }
                    }
                }
            }

        }

        //Walks up the parent chain for a node to determine if any
        //of it's parent nodes are selected
        private bool IsAnyParentSelected(UltraTreeNode Node)
        {
            UltraTreeNode ParentNode;
            bool ReturnValue = false;

            ParentNode = Node.Parent;

            while (ParentNode != null)
            {
                if (ParentNode.Selected)
                {
                    ReturnValue = true;
                    break;
                }
                else
                {
                    ParentNode = ParentNode.Parent;
                }
            }

            return ReturnValue;
        }

        private void SetApplicationUser()
        {
            //var data = new ApplicationUser.Data();
            ////data.ApplicationId = ApplicationId;
            //data.FirstName = "Admin";
            //var result = ApplicationUser.Search(data, 11);
            //if (result != null && result.Rows.Count > 0)
            //{
            //    ApplicationUserId = Convert.ToInt32(result.Rows[0]["ApplicationUserId"]);
            //}
        }

        private string LoadSettingsByKeyName(string keyName)
        {
            //var result = string.Empty;
            //var dataKey = new UserPreferenceKey.Data();
            //dataKey.Name = keyName;
            ////dataKey.ApplicationId = ApplicationId;
            //var resultKeyTable = UserPreferenceKey.Search(dataKey, ApplicationUserId);
            //if (resultKeyTable != null && resultKeyTable.Rows.Count > 0)
            //{
            //    var data = new UserPreference.Data();
            //    data.UserPreferenceKeyId = Convert.ToInt32(resultKeyTable.Rows[0]["UserPreferenceKeyId"]);
            //    data.ApplicationId = ApplicationId;
            //    data.ApplicationUserId = ApplicationUserId;
            //    var resultTable = UserPreference.Search(data, ApplicationUserId);
            //    if (resultTable != null && resultTable.Rows.Count > 0)
            //    {
            //        result = Convert.ToString(resultTable.Rows[0]["Value"]);
            //    }
            //    else
            //    {
            //        data.DataTypeId = Convert.ToInt32(resultKeyTable.Rows[0]["DataTypeId"]);
            //        data.Value = Convert.ToString(resultKeyTable.Rows[0]["Value"]);
            //        data.UserPreferenceCategoryId = 73;
            //        result = Convert.ToString(resultKeyTable.Rows[0]["Value"]);
            //        UserPreference.Create(data, ApplicationUserId);
            //    }
            //}

            var result = string.Empty;

            return result;
        }

        #endregion

        #region Form Events

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeTextLog();
                SetApplicationUser();
                objTree = new ObjectTree();
                objTree.Dock = DockStyle.Fill;
                objTree.MouseDoubleClick += new EventHandler(objTree_MouseDoubleClick);
                objTree.ClassGenerated += new EventHandler(objTree_ClassGenerated);
                pnlTreeContainer.Controls.Add(objTree);

                txtQueryEditor = new ScintillaNET.Scintilla();
                txtQueryEditor.Dock = DockStyle.Fill;
                txtQueryEditor.AllowDrop = true;
                txtQueryEditor.DragEnter += txtQueryEditor_DragEnter;
                txtQueryEditor.DragDrop += txtQueryEditor_DragDrop;
                txtQueryEditor.KeyUp += new KeyEventHandler(txtQueryEditor_KeyUp);


                try
                {
                    txtQueryEditor.BackColor = Color.FromName(LoadSettingsByKeyName("TextEditorBackgroundColor"));
                    ShowLineNumbersInTextEditor(LoadSettingsByKeyName("ShowLineNumbers"));

                    NoOfRecordsReturned = Convert.ToInt32(LoadSettingsByKeyName("NoOfRecordsReturned"));
                }
                catch
                {
                    ShowLineNumbersInTextEditor("False");
                    txtQueryEditor.BackColor = Color.FromName("White");
                    NoOfRecordsReturned = 500;
                }
                tabControlQueryEditors.TabPages[0].Controls.Add(txtQueryEditor);

                Server = @"GAU-LAPTOP\SQLEXPRESS";
                Database = "IVR.TaskTimeTracker";
                User = "sa";
                Password = "pass@word1";

                txtQueryEditor.ConfigurationManager.Language = "mssql";
                txtQueryEditor.ConfigurationManager.Configure();

                //frmConnection connection = new frmConnection(Server, Database, User, Password);
                //connection.ShowDialog(this);

                ConnectionId = 2;
                LoadTreeView();

                editorResultPair.Add(tabControlQueryEditors.SelectedTab.Name, tabControlResults.Name);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.E)
            {
                toolStripBtnExecute_Click(sender, new EventArgs());
                txtQueryEditor.Focus();
            }

            //else if (e.Control && e.Key Code == Keys.N)
            //{
            //    var tmpCnt = tabControlQueryEditors.TabPages.Count;
            //FindNextName:
            //    if (tabControlQueryEditors.TabPages.ContainsKey("obj_PageQueryEditor" + tmpCnt + 1))
            //    {
            //        tmpCnt++;
            //        goto FindNextName;
            //    }

            //    var tabTitle = "Query" + tmpCnt + 1;
            //    TabPage myTabPage = new TabPage(tabTitle);
            //    myTabPage.Name = "obj_PageQueryEditor" + tmpCnt + 1;

            //    var txtProcText = new ScintillaNET.Scintilla();
            //    txtProcText.ConfigurationManager.Language = "mssql";
            //    txtProcText.ConfigurationManager.Configure();

            //    txtProcText.Text = string.Empty;
            //    txtProcText.Dock = DockStyle.Fill;
            //    txtProcText.AllowDrop = true;
            //    txtProcText.DragEnter += txtQueryEditor_DragEnter;
            //    txtProcText.BackColor = Color.FromName(LoadSettingsByKeyName("TextEditorBackgroundColor"));
            //    myTabPage.Controls.Add(txtProcText);

            //    tabControlQueryEditors.TabPages.Add(myTabPage);
            //    tabControlQueryEditors.SelectedTab = myTabPage;
            //}
        }

        #endregion

        #region Events

        void txtQueryEditor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                FormatQuery();
            }
        }

        void tabControlQueryEditors_OnClose(object sender, CloseEventArgs e)
        {
            this.tabControlQueryEditors.Controls.Remove(this.tabControlQueryEditors.TabPages[e.TabIndex]);
        }

        void objTree_ClassGenerated(object sender, EventArgs e)
        {
            try
            {
                var objectName = "GeneratedHelperClass";
                if (!tabControlQueryEditors.TabPages.ContainsKey("obj_" + objectName))
                {
                    var procText = objTree.GeneratedHelperClass;

                    var tabTitle = objectName;
                    TabPageEx myTabPage = new TabPageEx(tabTitle);
                    myTabPage.Name = "obj_" + objectName;
                    myTabPage.Menu = null;
                    myTabPage.UseVisualStyleBackColor = true;

                    var txtProcText = new ScintillaNET.Scintilla();
                    txtProcText.ConfigurationManager.Language = "cs";
                    txtProcText.ConfigurationManager.Configure();

                    if (ShowLineNumbers)
                    {
                        txtProcText.Margins[0].Width = 35;
                    }
                    else
                    {
                        txtProcText.Margins[0].Width = 0;
                    }

                    txtProcText.Text = procText.ToString();
                    txtProcText.Dock = DockStyle.Fill;
                    txtProcText.AllowDrop = true;
                    txtProcText.DragEnter += txtQueryEditor_DragEnter; try
                    {
                        txtProcText.BackColor = Color.FromName(LoadSettingsByKeyName("TextEditorBackgroundColor"));
                    }
                    catch
                    {
                        txtProcText.BackColor = Color.FromName("White");
                    }
                    myTabPage.Controls.Add(txtProcText);

                    tabControlQueryEditors.TabPages.Add(myTabPage);
                    tabControlQueryEditors.SelectedTab = myTabPage;

                }
                else
                {
                    tabControlQueryEditors.SelectedTab = tabControlQueryEditors.TabPages["obj_" + objectName];
                }

                objectName = "GeneratedClass";
                if (!tabControlQueryEditors.TabPages.ContainsKey("obj_" + objectName))
                {
                    var procText = objTree.GeneratedClass;

                    var tabTitle = objectName;
                    TabPageEx myTabPage = new TabPageEx(tabTitle);
                    myTabPage.Name = "obj_" + objectName;
                    myTabPage.Menu = null;
                    myTabPage.UseVisualStyleBackColor = true;
                    var txtProcText = new ScintillaNET.Scintilla();
                    txtProcText.ConfigurationManager.Language = "cs";
                    txtProcText.ConfigurationManager.Configure();

                    if (ShowLineNumbers)
                    {
                        txtProcText.Margins[0].Width = 35;
                    }
                    else
                    {
                        txtProcText.Margins[0].Width = 0;
                    }

                    txtProcText.Text = procText.ToString();
                    txtProcText.Dock = DockStyle.Fill;
                    txtProcText.AllowDrop = true;
                    txtProcText.DragEnter += txtQueryEditor_DragEnter;
                    try
                    {
                        txtProcText.BackColor = Color.FromName(LoadSettingsByKeyName("TextEditorBackgroundColor"));
                    }
                    catch
                    {
                        txtProcText.BackColor = Color.FromName("White");
                    }
                    myTabPage.Controls.Add(txtProcText);

                    tabControlQueryEditors.TabPages.Add(myTabPage);
                    tabControlQueryEditors.SelectedTab = myTabPage;

                }
                else
                {
                    tabControlQueryEditors.SelectedTab = tabControlQueryEditors.TabPages["obj_" + objectName];
                }
                CheckAndCreateResult();
            }
            catch { }
        }

        void objTree_MouseDoubleClick(object sender, EventArgs e)
        {
            if (objTree.treeViewObjects.ActiveNode != null && Convert.ToString(objTree.treeViewObjects.ActiveNode.Tag) == "proc")
            {
                try
                {
                    //tabControlQueryEditors.
                    var objectName = objTree.treeViewObjects.ActiveNode.Key;

                    if (!tabControlQueryEditors.TabPages.ContainsKey("obj_" + objectName))
                    {
                        var sql = "sp_helptext 'dbo." + objectName + "'";
                        var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, Database);
                        var procText = new StringBuilder();
                        if (oDT.DBTable != null)
                        {
                            foreach (DataRow dr in oDT.DBTable.Rows)
                            {
                                procText.Append(Convert.ToString(dr["Text"]));
                            }
                        }

                        var tabTitle = "dbo." + objectName;
                        TabPageEx myTabPage = new TabPageEx(tabTitle);
                        myTabPage.Name = "obj_" + objectName;
                        myTabPage.UseVisualStyleBackColor = true;
                        myTabPage.Menu = null;

                        var txtProcText = new ScintillaNET.Scintilla();
                        txtProcText.ConfigurationManager.Language = "mssql";
                        txtProcText.ConfigurationManager.Configure();

                        if (ShowLineNumbers)
                        {
                            txtProcText.Margins[0].Width = 35;
                        }
                        else
                        {
                            txtProcText.Margins[0].Width = 0;
                        }

                        txtProcText.Text = procText.ToString();
                        txtProcText.Dock = DockStyle.Fill;
                        txtProcText.AllowDrop = true;
                        txtProcText.DragEnter += txtQueryEditor_DragEnter;
                        txtProcText.BackColor = Color.FromName(LoadSettingsByKeyName("TextEditorBackgroundColor"));
                        myTabPage.Controls.Add(txtProcText);

                        tabControlQueryEditors.TabPages.Add(myTabPage);
                        tabControlQueryEditors.SelectedTab = myTabPage;

                    }
                    else
                    {
                        tabControlQueryEditors.SelectedTab = tabControlQueryEditors.TabPages["obj_" + objectName];
                    }
                    CheckAndCreateResult();
                }
                catch { }
            }
        }

        private void toolStripBtnSettings_Click(object sender, EventArgs e)
        {
            var settings = new frmSettings();
            settings.Owner = this;
            settings.ShowDialog();
            //settings.ShowDialog(this);
        }

        private void toolStripBtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                //var strQuery = ((ScintillaNET.Scintilla)tabControlQueryEditors.SelectedTab.Controls[0]).Text; 

                var strQuery = string.Empty;
                strQuery = ((ScintillaNET.Scintilla)tabControlQueryEditors.SelectedTab.Controls[0]).Selection.Text;

                if (string.IsNullOrEmpty(strQuery.Trim()))
                {
                    strQuery = ((ScintillaNET.Scintilla)tabControlQueryEditors.SelectedTab.Controls[0]).Text;
                }

                if (!string.IsNullOrEmpty(strQuery.Trim()))
                {
                    var sql = strQuery;

                    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[Database].ConnectionString);
                    SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                    da.ReturnProviderSpecificTypes = true;
                    DataSet dsResult = new DataSet();
                    da.Fill(dsResult, 0, NoOfRecordsReturned, "result");

                    // ds.Tables[0].Columns[2].DataType

                    LogSQL(sql);
                    LoadSQLLog();
                    var tabName = Convert.ToString(editorResultPair[tabControlQueryEditors.SelectedTab.Name]);
                    var controls = splitContainer2.Panel2.Controls[0].Controls.Find(tabName, false);
                    if (controls != null && controls.Length > 0)
                    {

                        List<UltraGrid> lstGrids = new List<UltraGrid>();
                        var tabControl = (TabControl)controls[0];
                        tabControl.TabPages.Clear();
                        var resultGridSetting = LoadSettingsByKeyName("ResultGrid");
                        if (dsResult.Tables.Count > 0)
                        {
                            if (string.IsNullOrEmpty(resultGridSetting) || resultGridSetting == "Multi Tab View")
                            {
                                #region Multi Tab View

                                for (var iCount = 0; iCount < dsResult.Tables.Count; iCount++)
                                {
                                    var tabTitle = "Result";
                                    if (tabControl.TabCount > 0)
                                    {
                                        tabTitle += (tabControl.TabCount).ToString();
                                    }

                                    TabPage myTabPage = new TabPage(tabTitle);

                                    var dgvResult1 = new UltraGrid();

                                    dgvResult1.Dock = DockStyle.Fill;
                                    //dataTable.AsEnumerable().Take(50).CopyToDataTable(newdataTable)
                                    dgvResult1.DataSource = dsResult.Tables[iCount];
                                    dgvResult1.InitializeLayout += ultraGridResults_InitializeLayout;

                                    dgvResult1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    dgvResult1.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;

                                    myTabPage.Controls.Add(dgvResult1);
                                    tabControl.TabPages.Add(myTabPage);
                                    lstGrids.Add(dgvResult1);
                                }

                                #endregion
                            }
                            else
                            {
                                #region Single Tab View

                                TabPage myTabPage = new TabPage("Result");
                                if (dsResult.Tables.Count == 1)
                                {
                                    var dgvResult1 = new UltraGrid();

                                    dgvResult1.Dock = DockStyle.Fill;
                                    dgvResult1.DataSource = dsResult.Tables[0];
                                    //dgvResult1.DataSource = dsResult.Tables[0];
                                    dgvResult1.InitializeLayout += ultraGridResults_InitializeLayout;

                                    dgvResult1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    dgvResult1.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
                                    myTabPage.Controls.Add(dgvResult1);
                                    lstGrids.Add(dgvResult1);
                                    //ResetHeader(dsResult.Tables[0], dgvResult1);
                                }
                                else
                                {
                                    SplitterPanel pnlContainer = null;
                                    for (var iCount = 0; iCount < dsResult.Tables.Count; iCount++)
                                    {
                                        var dgvResult1 = new UltraGrid();

                                        dgvResult1.Dock = DockStyle.Fill;
                                        dgvResult1.DataSource = dsResult.Tables[iCount];
                                        //dgvResult1.DataSource = dsResult.Tables[iCount];
                                        dgvResult1.InitializeLayout += ultraGridResults_InitializeLayout;

                                        dgvResult1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                        dgvResult1.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;

                                        if (iCount != dsResult.Tables.Count - 1)
                                        {
                                            var splitContainer = new SplitContainer();
                                            splitContainer.Orientation = Orientation.Horizontal;
                                            splitContainer.Panel1MinSize = 20;
                                            splitContainer.Panel2MinSize = 20;
                                            splitContainer.SplitterDistance = dgvResult1.Height;
                                            splitContainer.Dock = DockStyle.Fill;

                                            splitContainer.Panel1.Controls.Add(dgvResult1);
                                            if (pnlContainer == null)
                                            {
                                                myTabPage.Controls.Add(splitContainer);
                                            }
                                            else
                                            {
                                                pnlContainer.Controls.Add(splitContainer);
                                            }
                                            pnlContainer = splitContainer.Panel2;
                                        }
                                        else
                                        {
                                            pnlContainer.Controls.Add(dgvResult1);
                                        }
                                        lstGrids.Add(dgvResult1);

                                    }
                                }
                                tabControl.TabPages.Add(myTabPage);

                                #endregion
                            }
                            if (lstGrids.Count > 0)
                            {
                                ResetHeader(lstGrids);
                                lstGrids.Clear();
                            }
                        }
                    }
                    toolStripStatusLabelExecute.Text = "Query Executed Successfully";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripBtnChangeConnection_Click(object sender, EventArgs e)
        {
            frmConnection connection = new frmConnection(Server, Database, User, Password);
            connection.Owner = this;
            connection.ShowDialog();
            //connection.ShowDialog(this);
        }

        private void tabControlQueryEditors_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndCreateResult();
        }

        private void toolStripBtnLog_Click(object sender, EventArgs e)
        {
            var formLog = new frmLog(ConnectionId);
            formLog.Owner = this;
            formLog.ShowDialog();
            //formLog.ShowDialog(this);
        }

        private void txtQueryEditor_DragEnter(object sender, DragEventArgs e)
        {

            toolStripStatusLabel1.Text = "Drag Entered";
            e.Effect = DragDropEffects.Move;
        }

        private void txtQueryEditor_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text).ToString();

            toolStripStatusLabel1.Text = "Dropped Text: " + data;

            //MessageBox.Show(data);
            //var tmpStrings = data.Split(new string[] { ":::" }, StringSplitOptions.None);

            //if (tmpStrings != null && tmpStrings.Length >= 2)
            //{
            //    var objName = tmpStrings[0];
            //    var tagName = tmpStrings[1];
            //    txtQueryEditor.Text = GetDragText(objName, tagName);
            //}
        }

        private void txtQueryEditor_DragOver(object sender, DragEventArgs e)
        {
            // Determine whether string data exists in the drop data. If not, then
            // the drop effect reflects that the drop cannot occur.
            if (!e.Data.GetDataPresent(typeof(System.String)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            // Set the effect based upon the KeyState.
            if ((e.KeyState & (8 + 32)) == (8 + 32) &&
                (e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
            {
                // KeyState 8 + 32 = CTL + ALT
                // Link drag and drop effect.
                e.Effect = DragDropEffects.Link;
            }
            else if ((e.KeyState & 32) == 32 &&
              (e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
            {
                // ALT KeyState for link.
                e.Effect = DragDropEffects.Link;
            }
            else if ((e.KeyState & 4) == 4 &&
              (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                // SHIFT KeyState for move.
                e.Effect = DragDropEffects.Move;
            }
            else if ((e.KeyState & 8) == 8 &&
              (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                // CTL KeyState for copy.
                e.Effect = DragDropEffects.Copy;
            }
            else if ((e.AllowedEffect & DragDropEffects.Move) ==
                                           DragDropEffects.Move)
            {
                // By default, the drop action should be move, if allowed.
                e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;


        }

        private void ultraGridResults_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout aboutBox = new frmAbout();
            aboutBox.Owner = this;
            aboutBox.ShowDialog();
            //aboutBox.ShowDialog(this);
        }

        private void dBObjectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ultraDockManager1.ResetComponentSettings();
            //ultraDockManager1.ResetControlPanes();
            //ultraDockManager1.ShowNavigator();
            //ultraDockManager1.PinAll();
            //ultraDockManager1.clea
        }

        private void toolStripButtonFormatQuery_Click(object sender, EventArgs e)
        {
            FormatQuery();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOptions options = new frmOptions();
            options.Owner = this;
            options.ShowDialog();
        }

        #endregion

    }
}
