using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UltraTree_Drag_and_Drop_CS;
using DatabaseUtilityTool.Components.Core;
using Infragistics.Win;
using Infragistics.Win.UltraWinTree;

namespace DatabaseUtilityTool
{
    public partial class ObjectTree : UserControl
    {

        #region private variables

        string DataStoreKey = "SQLDBTool";

        //Create a new instance of the DrawFilter class to 
        //Handle drawing the DropHighlight/DropLines
        private UltraTree_DropHightLight_DrawFilter_Class UltraTree_DropHightLight_DrawFilter = new UltraTree_DropHightLight_DrawFilter_Class();

        UltraTreeNode tNodeNewGrpClicked = null;
        ImageList treeViewImageList = null;
        string cellTextBeforeEdit = string.Empty;

        #endregion

        #region Construtor

        public ObjectTree()
        {
            InitializeComponent();
            treeViewObjects.GotFocus += new EventHandler(treeViewObjects_GotFocus);
            UltraTree_DropHightLight_DrawFilter.Invalidate += new EventHandler(this.UltraTree_DropHightLight_DrawFilter_Invalidate);
            UltraTree_DropHightLight_DrawFilter.QueryStateAllowedForNode += new UltraTree_DropHightLight_DrawFilter_Class.QueryStateAllowedForNodeEventHandler(this.UltraTree_DropHightLight_DrawFilter_QueryStateAllowedForNode);
        }

        #endregion

        #region Properties

        private string ApplicationConnectionString
        {
            get
            {
                return ApplicationConstants.ApplicationConnectionString;
            }
        }

        private string path = @"C:\Temp\";
        private int timeout = 2400;
        private string _connectionString;
        private int _connectionId;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }

        }

        public int ConnectionId
        {
            get { return _connectionId; }
            set { _connectionId = value; }

        }

        public string Database
        {
            get;
            set;
        }

        public event EventHandler MouseDoubleClick;
        public event EventHandler ClassGenerated;

        public string GeneratedHelperClass;
        public string GeneratedClass;

        #endregion

        #region private methods

        private void RemoveChildNodes(UltraTreeNode parentNode)
        {
            while (parentNode.Nodes.Count > 0)
            {
                var nodeTag = Convert.ToString(parentNode.Nodes[0].Tag);
                if (nodeTag == "group")
                {
                    if (parentNode.Nodes[0].Nodes.Count > 0)
                    {
                        RemoveChildNodes(parentNode.Nodes[0]);
                    }
                    var data = new Category.Data();
                    data.CategoryId = Convert.ToInt32(GetKey(parentNode.Nodes[0].Key));
                    Category.Delete(data);
                    parentNode.Nodes[0].Remove();
                }
                else
                {
                    var data = new CategoryItem.Data();
                    data.CategoryItemId = Convert.ToInt32(GetKey(parentNode.Nodes[0].Key));
                    CategoryItem.Delete(data);

                    //nodeTag
                    //parentNode.Nodes[i].Reposition(

                    for (int j = 0; j < treeViewObjects.Nodes[0].Nodes.Count; j++)
                    {
                        var parentTag = Convert.ToString(treeViewObjects.Nodes[0].Nodes[j].Tag);
                        if (parentTag == nodeTag + "s")
                        {
                            parentNode.Nodes[0].Reposition(treeViewObjects.Nodes[0].Nodes[j].Nodes);
                            break;
                        }
                    }
                }
            }
        }

        private string GetKey(string key)
        {
            string result = string.Empty;

            result = key.Split(new string[] { "$$" }, StringSplitOptions.None)[0];

            return result;
        }

        private void InitializeTreeControl()
        {
            // Set various properties on the tree control

            // Specify whether the tree will scroll during a 
            // drag operation
            this.treeViewObjects.AllowAutoDragScrolling = true;

            // Specify whether the tree will look for the
            // next matching node as the user types.
            this.treeViewObjects.AllowKeyboardSearch = true;

            // Specify the amount of milliseconds to wait
            // before expanding a node during a drag
            // operation.
            this.treeViewObjects.AutoDragExpandDelay = 500;

            // Set the border style of the control
            this.treeViewObjects.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;

            // Specify whether clicking anywhere on the 
            // row the node is on will select the node.
            this.treeViewObjects.FullRowSelect = false;

            // Specify whether selected nodes remain
            // highlighted when the tree loses focus.
            this.treeViewObjects.HideSelection = true;

            // Specify the padding around images in pixels.
            //this.treeViewObjects.ImagePadding = 3;

            // Specify how many pixels to indent each
            // node level.
            //this.treeViewObjects.Indent = 8;

            // Specify the default size of images that will
            // appear to the left and right of a node's text.
            this.treeViewObjects.LeftImagesSize = new Size(16, 16);
            //this.treeViewObjects.RightImagesSize = new Size(8, 8);            

            // Specify the style and color os the node 
            // connector lines.
            this.treeViewObjects.NodeConnectorColor = Color.Red;
            this.treeViewObjects.NodeConnectorStyle = NodeConnectorStyle.Dotted;

            // Specify the string to be used to separate
            // ancestor nodes when getting a node's
            // 'FullPath' property. For example:
            //this.ultraTree1.ActiveNode.FullPath;
            this.treeViewObjects.PathSeparator = @"\";

            // Specify whether scrollbars will be shown
            this.treeViewObjects.Scrollable = Infragistics.Win.UltraWinTree.Scrollbar.ShowIfNeeded;

            // Specify whether lines will appear between nodes
            this.treeViewObjects.ShowLines = true;
            this.treeViewObjects.Indent = 20;
            this.treeViewObjects.Override.ItemHeight = 18;

            // Specify whether room for lines and expansion
            // indicators will be reserved to the left of root
            // nodes (i.e. nodes without a parent node).
            //
            // Note: If the 'ShowLines' property is set to
            // false then the root lines won't be visible
            // even if the 'ShowRootLines' property is true.
            // In that case just the expansion indicators
            // will show.
            this.treeViewObjects.ShowRootLines = true;

        }

        private void CreatChildNodes(UltraTreeNode tNode)
        {
            try
            {
                var sql = string.Empty;
                var nodeTag = string.Empty;
                var isRootNode = false;
                var imgIndex = 0;
                if (tNode.Tag == "tables")
                {
                    //sql = "select name, (SELECT SUM (row_count) FROM sys.dm_db_partition_stats WHERE object_id=s.id) as 'rowcount' from sysobjects s WHERE xtype='u' ORDER BY name";
                    sql = "SELECT so.name, [RowCount] = MAX(si.rows) FROM sysobjects so, sysindexes si WHERE so.xtype = 'U' AND si.id = OBJECT_ID(so.name) GROUP BY so.name ORDER BY 1";
                    nodeTag = "table";
                    isRootNode = true;
                    imgIndex = 2;
                }
                else if (tNode.Tag == "procs")
                {
                    sql = "select name from sysobjects where xtype='p' ORDER BY name";
                    nodeTag = "proc";
                    isRootNode = true;
                    imgIndex = 3;
                }
                else if (tNode.Tag == "views")
                {
                    sql = "select name from sysobjects where xtype='v' ORDER BY name";
                    nodeTag = "view";
                    isRootNode = true;
                    imgIndex = 4;
                }
                else if (tNode.Tag == "functions")
                {
                    sql = "select name from sysobjects where xtype='fn' ORDER BY name";
                    nodeTag = "function";
                    isRootNode = true;
                    imgIndex = 5;
                }
                var isCleared = false;
                var initCount = tNode.Nodes.Count;
                if (isRootNode && tNode.Nodes.Count == 1)
                {
                    tNode.Nodes.Clear();
                    isCleared = true;
                    Framework.Components.DataAccess.StartUp.EntryPoint(path, timeout, Database);
                    var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, Database);
                    if (oDT.DBTable != null && oDT.DBTable.Rows.Count > 0)
                    {
                        var categoryItemList = CategoryItem.GetList();
                        foreach (DataRow dr in oDT.DBTable.Rows)
                        {
                            bool isExist = false;
                            if (categoryItemList != null && categoryItemList.Rows.Count > 0)
                            {
                                var rows = categoryItemList.Select(" Name = '" + Convert.ToString(dr["name"]) + "'");
                                if (rows.Length > 0)
                                {
                                    isExist = true;
                                }
                            }

                            if (!tNode.Nodes.Exists(Convert.ToString(dr["name"])) && !isExist)
                            {
                                var nodeName = Convert.ToString(dr["name"]);
                                if (tNode.Tag == "tables")
                                {
                                    nodeName += "(" + Convert.ToString(dr["rowcount"]) + ")";
                                }
                                var tmpNode = tNode.Nodes.Add(Convert.ToString(dr["name"]), nodeName);
                                tmpNode.Tag = nodeTag;
                                tmpNode.AllowCellEdit = AllowCellEdit.Disabled;
                                tmpNode.Override.NodeAppearance.Image = GetImageIndex(nodeTag);
                            }
                        }
                    }
                }

                // For Group also and for Root Nodes Also.
                if (initCount == 1)
                {
                    if (!isCleared)
                    {
                        tNode.Nodes.Clear();
                    }

                    // Get Child Category
                    var data = new Category.Data();
                    if (tNode.Tag.ToString() == "group")
                    {
                        var tmpId = GetKey(tNode.Key);
                        data.ParentCategoryId = Convert.ToInt32(tmpId);
                    }
                    else
                    {
                        data.ParentCategoryId = Convert.ToInt32(tNode.Key);
                    }
                    var dtCategory = Category.Search(data);
                    if (dtCategory != null && dtCategory.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCategory.Rows)
                        {
                            if (!tNode.Nodes.Exists(Convert.ToString(dr["name"])))
                            {
                                var tmpNode = tNode.Nodes.Add(Convert.ToString(dr["CategoryId"]) + "$$group", Convert.ToString(dr["Name"]));
                                tmpNode.Tag = "group";
                                tmpNode.Override.NodeAppearance.Image = GetImageIndex(tmpNode.Tag.ToString());
                                int childCount = GetChildCount("group", Convert.ToInt32(dr["CategoryId"]));
                                if (childCount > 0)
                                {
                                    tmpNode.Nodes.Add("Sample Group" + tmpNode.Key, "Sample Group");
                                }
                            }
                        }
                    }

                    // Get Child Object Items
                    var dataItem = new CategoryItem.Data();
                    if (tNode.Tag.ToString() == "group")
                    {
                        var tmpId = GetKey(tNode.Key);
                        dataItem.CategoryId = Convert.ToInt32(tmpId);
                    }
                    else
                    {
                        dataItem.CategoryId = Convert.ToInt32(tNode.Key);
                    }
                    var dtCategoryItem = CategoryItem.Search(dataItem);
                    if (dtCategoryItem != null && dtCategoryItem.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCategoryItem.Rows)
                        {
                            var tag = Convert.ToString(dr["ItemType"]).ToLower();
                            var name = Convert.ToString(dr["Name"]);
                            if (tag == "table")
                            {
                                name = name + "(" + GetTableRowCount(name) + ")";
                            }

                            var tmpNode = tNode.Nodes.Add(Convert.ToString(dr["CategoryItemId"]) + "$$" + tag, name);
                            tmpNode.Tag = tag;
                            tmpNode.Override.NodeAppearance.Image = GetImageIndex(Convert.ToString(dr["ItemType"]).ToLower());
                        }
                    }
                }

                //toolStripStatusLabelExecute.Text = "Objects Listed Successfully";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetImageIndex(string nodeTag)
        {
            if (nodeTag == "group")
                nodeTag = "folder";
            var imgIndex = treeViewImageList.Images.IndexOfKey(nodeTag);

            //DatabaseTool.UI.Resources.TreeIcons.DBTreeIcons.t

            return imgIndex;
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
            data.CreatedDate = DateTime.Now;
            nodeId = Category.Create(data, ApplicationConnectionString);

            return nodeId;
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

                ////Framework.Components.DataAccess.StartUp.EntryPoint(path, timeout, ConnectionString);
                var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, DataStoreKey);
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

        private void CreateImageList()
        {
            treeViewImageList = new ImageList();
            foreach (var propertyInfo in typeof(DatabaseTool.Resources.TreeIcons.DBTreeIcons).GetProperties().Where(info => info.PropertyType == typeof(Bitmap)))
            {
                treeViewImageList.Images.Add(propertyInfo.Name, (Bitmap)propertyInfo.GetValue(null, null));
            }
        }

        private int GetTableRowCount(string tableName)
        {
            var result = 0;
            var sql = "SELECT so.name, [RowCount] = MAX(si.rows) FROM sysobjects so, sysindexes si WHERE so.xtype = 'U' AND si.id = OBJECT_ID(so.name) and so.name = 'category' GROUP BY so.name ORDER BY 2 DESC";
            var oDT = new Framework.Components.DataAccess.DBDataTable("Get List", sql, Database);
            if (oDT.DBTable != null && oDT.DBTable.Rows.Count > 0)
            {
                result = Convert.ToInt32(oDT.DBTable.Rows[0]["RowCount"]);
            }
            return result;
        }

        #endregion

        #region public methods

        public void SetConnectionString(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void SetDatabase(string database)
        {
            this.Database = database;
        }

        public void SetConnectionId(int connectionId)
        {
            this.ConnectionId = connectionId;
        }

        #endregion

        #region Events

        private void ObjectTree_Load(object sender, EventArgs e)
        {
            InitializeTreeControl();
            CreateImageList();
            treeViewObjects.ImageList = treeViewImageList;
            //Attach the Drawfiler to the tree
            treeViewObjects.DrawFilter = UltraTree_DropHightLight_DrawFilter;

            treeViewObjects.Override.SelectionType = Infragistics.Win.UltraWinTree.SelectType.SingleAutoDrag;
            //TestMethod();
        }

        private void treeViewObjects_BeforeExpand(object sender, CancelableNodeEventArgs e)
        {
            if (e.TreeNode != null)
            {
                CreatChildNodes(e.TreeNode);
            }
        }

        private void treeViewObjects_AfterLabelEdit(object sender, NodeEventArgs e)
        {
            if (e.TreeNode.Text != null)
            {
                if (e.TreeNode.Text.Length > 0)
                {
                    if (e.TreeNode.Text.IndexOfAny(new char[] { '@', '.', ',', '!' }) == -1)
                    {
                        if (cellTextBeforeEdit != e.TreeNode.Text)
                        {
                            if (e.TreeNode.Key.Contains("New Group"))
                            {
                                // Stop editing without canceling the label change.
                                var nodeId = CheckCategoryExists(e.TreeNode.Text, Convert.ToInt32(GetKey(e.TreeNode.Parent.Key)));
                                if (nodeId == null)
                                {
                                    nodeId = CreateCategory(e.TreeNode.Text, Convert.ToInt32(GetKey(e.TreeNode.Parent.Key)));
                                    e.TreeNode.Key = Convert.ToString(nodeId.Value) + "$$" + e.TreeNode.Tag;
                                    e.TreeNode.EndEdit(false);
                                    e.TreeNode.Override.NodeAppearance.Image = GetImageIndex(Convert.ToString(e.TreeNode.Tag));
                                }
                                else if (Convert.ToInt32(GetKey(e.TreeNode.Parent.Key)) != nodeId.Value)
                                {
                                    // Cancel the label edit action, inform the user, and place the node in edit mode again.                         
                                    e.TreeNode.CancelUpdate();
                                    MessageBox.Show("Invalid Group Name.\n" +
                                       "This Group Name already Exists");
                                    treeViewObjects.Focus();
                                    treeViewObjects.ActiveNode = e.TreeNode;
                                    if (!e.TreeNode.IsEditing)
                                    {
                                        e.TreeNode.BeginEdit();
                                    }
                                }
                            }
                            else
                            {
                                var data = new Category.Data();
                                data.ConnectionId = ConnectionId;
                                data.Name = e.TreeNode.Text;
                                data.CategoryId = Convert.ToInt32(GetKey(e.TreeNode.Key));
                                data.ParentCategoryId = Convert.ToInt32(GetKey(e.TreeNode.Parent.Key));
                                data.CreatedDate = DateTime.Now;
                                Category.Update(data);

                                e.TreeNode.EndEdit(false);

                                e.TreeNode.Override.NodeAppearance.Image = GetImageIndex(Convert.ToString(e.TreeNode.Tag));
                            }
                        }
                    }
                    else
                    {

                        // Cancel the label edit action, inform the user, and place the node in edit mode again.                         
                        e.TreeNode.CancelUpdate();
                        MessageBox.Show(Properties.Resources.ObjectTree_treeViewObjects_AfterLabelEdit_, "Node Label Edit");
                        treeViewObjects.Focus();
                        treeViewObjects.ActiveNode = e.TreeNode;
                        if (!e.TreeNode.IsEditing)
                        {
                            e.TreeNode.BeginEdit();
                        }
                    }
                }
                else
                {
                    treeViewObjects.ActiveNode = e.TreeNode;
                    // Cancel the label edit action, inform the user, and  place the node in edit mode again. 
                    e.TreeNode.CancelUpdate();
                    MessageBox.Show("Invalid tree node label.\nThe label cannot be blank",
                       "Node Label Edit");
                    treeViewObjects.Focus();
                    treeViewObjects.ActiveNode = e.TreeNode;
                    if (!e.TreeNode.IsEditing)
                    {
                        e.TreeNode.BeginEdit();
                    }
                }
            }
            cellTextBeforeEdit = string.Empty;
        }

        private void treeViewObjects_MouseUp(object sender, MouseEventArgs e)
        {
            // Show menu only if the right mouse button is clicked.
            if (e.Button == MouseButtons.Right)
            {
                // Point where the mouse is clicked.
                Point p = new Point(e.X, e.Y);

                // Get the node that the user has clicked.
                tNodeNewGrpClicked = treeViewObjects.GetNodeFromPoint(p);
                if (tNodeNewGrpClicked != null)
                {
                    var tagName = Convert.ToString(tNodeNewGrpClicked.Tag);
                    if (tagName == "tables" || tagName == "procs" || tagName == "views" || tagName == "functions" || tagName == "group" || tagName == "root")
                    {
                        treeViewObjects.ActiveNode = tNodeNewGrpClicked;
                        contextMenuStripTreeView.Show(treeViewObjects, p);
                        if (tagName == "group")
                        {
                            removeGroupToolStripMenuItem.Visible = true;
                        }
                        else
                        {
                            removeGroupToolStripMenuItem.Visible = false;
                        }
                    }
                    else if (tagName == "table")
                    {
                        contextMenuStripActions.Show(treeViewObjects, p);
                    }
                }
            }
        }

        private void treeViewObjects_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseDoubleClick(this, new EventArgs());
        }

        private void treeViewObjects_AfterSelect(object sender, SelectEventArgs e)
        {
            treeViewObjects.ActiveNode.Override.SelectedNodeAppearance.ResetBackColor();
            treeViewObjects.ActiveNode.Override.SelectedNodeAppearance.ResetForeColor();
            treeViewObjects.DrawsFocusRect = DefaultableBoolean.True;
        }

        private void treeViewObjects_GotFocus(object sender, EventArgs e)
        {
            treeViewObjects.DrawsFocusRect = DefaultableBoolean.False;
            try
            {
                treeViewObjects.ActiveNode.Override.SelectedNodeAppearance.BackColor = Color.White;
                treeViewObjects.ActiveNode.Override.SelectedNodeAppearance.ForeColor = Color.Black;
            }
            catch { }
        }

        private void treeViewObjects_SelectionDragStart(object sender, EventArgs e)
        {
            //Start a DragDrop operation
            try
            {
                if (treeViewObjects.SelectedNodes != null && treeViewObjects.SelectedNodes.Count > 0)
                {
                    var tagName = Convert.ToString(treeViewObjects.SelectedNodes[0].Tag);
                    if (tagName == "proc" || tagName == "table" || tagName == "function" || tagName == "view" || tagName == "group")
                    {
                        treeViewObjects.DoDragDrop(treeViewObjects.SelectedNodes, DragDropEffects.Move);
                    }
                }
            }
            catch { }

        }

        private void treeViewObjects_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            //Did the user press escape? 
            if (e.EscapePressed)
            {
                //User pressed escape
                //Cancel the Drag
                e.Action = DragAction.Cancel;
                //Clear the Drop highlight, since we are no longer
                //dragging
                UltraTree_DropHightLight_DrawFilter.ClearDropHighlight();
            }
        }

        private void treeViewObjects_DragDrop(object sender, DragEventArgs e)
        {
            //A dummy node variable used for various things
            UltraTreeNode aNode;
            //The SelectedNodes which will be dropped
            SelectedNodesCollection SelectedNodes;
            //The Node to Drop On
            UltraTreeNode DropNode;
            //An integer used for loops
            int i;

            //Set the DropNode
            DropNode = UltraTree_DropHightLight_DrawFilter.DropHightLightNode;

            //Get the Data and put it into a SelectedNodes collection,
            //then clone it and work with the clone
            //These are the nodes that are being dragged and dropped
            SelectedNodes = (SelectedNodesCollection)e.Data.GetData(typeof(SelectedNodesCollection));
            SelectedNodes = SelectedNodes.Clone() as SelectedNodesCollection;

            //Sort the selected nodes into their visible position. 
            //This is done so that they stay in the same order when
            //they are repositioned. 
            SelectedNodes.SortByPosition();

            //Determine where we are dropping based on the current
            //DropLinePosition of the DrawFilter
            switch (UltraTree_DropHightLight_DrawFilter.DropLinePosition)
            {
                case DropLinePositionEnum.OnNode: //Drop ON the node
                    {
                        //Loop through the SelectedNodes and reposition
                        //them to the node that was dropped on.
                        //Note that the DrawFilter keeps track of what
                        //node the mouse is over, so we can just use
                        //DropHighLightNode as the drop target. 
                        for (i = 0; i <= (SelectedNodes.Count - 1); i++)
                        {
                            try
                            {
                                aNode = SelectedNodes[i];
                                var newParentTagName = Convert.ToString(DropNode.Tag);
                                var oldParentTagName = Convert.ToString(aNode.Parent.Tag);
                                var oldParentCategoryId = Convert.ToInt32(GetKey(aNode.Parent.Key));
                                var newParentCategoryId = Convert.ToInt32(GetKey(DropNode.Key));
                                var nodeTagName = Convert.ToString(aNode.Tag);

                                if (string.IsNullOrEmpty(newParentTagName) || string.IsNullOrEmpty(oldParentTagName))
                                    continue;

                                if (nodeTagName == "group")
                                {
                                    var data = new Category.Data();
                                    data.CategoryId = Convert.ToInt32(GetKey(aNode.Key));
                                    data.ParentCategoryId = newParentCategoryId;
                                    data.Name = aNode.Text.Split(new char[] { '(' })[0];
                                    data.ConnectionId = ConnectionId;

                                    Category.Update(data);

                                }
                                else
                                {
                                    if (newParentTagName == "root")
                                        continue;
                                    List<string> lstRootTags = new List<string>() { "tables", "procs", "views", "functions" };
                                    var isUpdate = false; var isInsert = false; var isDelete = false;
                                    if (newParentTagName == "group" && oldParentTagName == "group")
                                    {
                                        isUpdate = true;
                                    }
                                    else if (newParentTagName == "group" && lstRootTags.Contains(oldParentTagName))
                                    {

                                        if (nodeTagName + "s" != oldParentTagName)
                                        {
                                            isUpdate = true;
                                        }
                                        else
                                        {
                                            isInsert = true;
                                        }
                                    }
                                    else if (oldParentTagName == "group" && lstRootTags.Contains(newParentTagName))
                                    {

                                        if (nodeTagName + "s" != newParentTagName)
                                        {
                                            isUpdate = true;
                                        }
                                        else
                                        {
                                            isDelete = true;
                                        }
                                    }
                                    else if (lstRootTags.Contains(newParentTagName) && lstRootTags.Contains(oldParentTagName))
                                    {
                                        if (nodeTagName + "s" != newParentTagName && nodeTagName + "s" != oldParentTagName)
                                        {
                                            isUpdate = true;
                                        }
                                        else if (nodeTagName + "s" == oldParentTagName && nodeTagName + "s" != newParentTagName)
                                        {
                                            isInsert = true;
                                        }
                                        else if (nodeTagName + "s" == newParentTagName && nodeTagName + "s" != oldParentTagName)
                                        {
                                            isDelete = true;
                                        }
                                    }

                                    var data = new CategoryItem.Data();
                                    data.CategoryId = newParentCategoryId;
                                    data.ItemType = nodeTagName;
                                    data.Name = aNode.Text.Split(new char[] { '(' })[0];
                                    data.Status = "new";
                                    if (isInsert)
                                    {
                                        data.CreatedDate = DateTime.Now;
                                        var categoryItemId = CategoryItem.Create(data, ApplicationConnectionString);
                                        aNode.Key = Convert.ToString(categoryItemId) + "$$" + aNode.Tag;
                                    }
                                    else if (isUpdate)
                                    {
                                        data.CategoryItemId = Convert.ToInt32(GetKey(aNode.Key));
                                        CategoryItem.Update(data);
                                    }
                                    else if (isDelete)
                                    {
                                        data.CategoryItemId = Convert.ToInt32(GetKey(aNode.Key));
                                        CategoryItem.Delete(data);
                                    }
                                }
                                //MessageBox.Show("aNode: " + aNode.Text);
                                //MessageBox.Show("Drop Node: " + DropNode.Text);

                                aNode.Reposition(DropNode.Nodes);
                            }
                            catch { }
                        }
                        break;
                    }
            }

            //After the drop is complete, erase the current drop
            //highlight. 
            UltraTree_DropHightLight_DrawFilter.ClearDropHighlight();
        }

        private void treeViewObjects_DragOver(object sender, DragEventArgs e)
        {
            //A dummy node variable used to hold nodes for various 
            //things
            UltraTreeNode aNode;
            //The Point that the mouse cursor is on, in Tree coords. 
            //This event passes X and Y in form coords. 
            System.Drawing.Point PointInTree;

            //Get the position of the mouse in the tree, as opposed
            //to form coords
            PointInTree = treeViewObjects.PointToClient(new Point(e.X, e.Y));

            //Get the node the mouse is over.
            aNode = treeViewObjects.GetNodeFromPoint(PointInTree);

            //Make sure the mouse is over a node
            //if (aNode == null)
            //{
            //    //The Mouse is not over a node
            //    //Do not allow dropping here
            //    e.Effect = DragDropEffects.None;
            //    //Erase any DropHighlight
            //    UltraTree_DropHightLight_DrawFilter.ClearDropHighlight();
            //    //Exit stage left
            //    return;
            //}

            //Check to see if we are dropping onto a node who//s
            //parent (grandparent, etc) is selected.
            //This is to prevent the user from dropping a node onto
            //one of it//s own descendents. 
            if (aNode != null)
            {
                if (IsAnyParentSelected(aNode))
                {
                    //Mouse is over a node whose parent is selected
                    //Do not allow the drop
                    e.Effect = DragDropEffects.None;
                    //Clear the DropHighlight
                    UltraTree_DropHightLight_DrawFilter.ClearDropHighlight();
                    //Exit stage left
                    return;
                }

                //If we//ve reached this point, it//s okay to drop on this node
                //Tell the DrawFilter where we are by calling SetDropHighlightNode
                UltraTree_DropHightLight_DrawFilter.SetDropHighlightNode(aNode, PointInTree);
            }

            //Allow Dropping here. 
            e.Effect = DragDropEffects.Move;
        }

        private void treeViewObjects_DragLeave(object sender, EventArgs e)
        {
            //When the mouse goes outside the control, clear the 
            //drophighlight. 

            //Since the DropHighlight is cleared when the 
            //mouse is not over a node, anyway, 
            //this is probably not needed

            //But, just in case the user goes from a node directly
            //off the control...
            //UltraTree_DropHightLight_DrawFilter.ClearDropHighlight();
        }

        //Occassionally, the DrawFilter will let us know that the 
        //control needs to be invalidated. 
        private void UltraTree_DropHightLight_DrawFilter_Invalidate(object sender, System.EventArgs e)
        {
            //Any time the drophighlight changes, the control needs 
            //to know that it has to repaint. 
            //It would be more efficient to only invalidate the area
            //that needs it, but this works and is very clean.
            treeViewObjects.Invalidate();
        }

        //This event is fired by the DrawFilter to let us determine
        //what kinds of drops we want to allow on any particular node
        private void UltraTree_DropHightLight_DrawFilter_QueryStateAllowedForNode(Object sender, UltraTree_DropHightLight_DrawFilter_Class.QueryStateAllowedForNodeEventArgs e)
        {
            try
            {
                //Don't let any Object Item be dropped on Root Node
                var selectedNodeTag = Convert.ToString(treeViewObjects.SelectedNodes[0].Tag);
                if (selectedNodeTag != "group")
                {
                    if (Convert.ToString(e.Node.Tag) == "root")
                    {

                        e.StatesAllowed = DropLinePositionEnum.None;
                    }
                    else
                    {
                        e.StatesAllowed = DropLinePositionEnum.OnNode;
                    }
                }
                else
                {
                    e.StatesAllowed = DropLinePositionEnum.OnNode;
                }
            }
            catch { }
        }

        private void toolStripMenuItemClass_Click(object sender, EventArgs e)
        {
            var strHelperClass = string.Empty;
            var strClass = string.Empty;
            if (tNodeNewGrpClicked != null)
            {
                var tableName = tNodeNewGrpClicked.Text.Split(new char[] { '(' })[0];
                if (!string.IsNullOrEmpty(tableName))
                {
                    var sql = "SELECT column_Name, data_type FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + tableName + "' ";
                    Framework.Components.DataAccess.StartUp.EntryPoint(path, timeout, Database);
                    var oDT = new Framework.Components.DataAccess.DBDataTable("Get Columns", sql, Database);
                    if (oDT != null && oDT.DBTable.Rows.Count > 0)
                    {
                        GeneratedHelperClass = ClassGenerator.ConstructHelperClass(tableName, oDT.DBTable);
                        GeneratedClass = ClassGenerator.ConstructClass(tableName, oDT.DBTable);
                        ClassGenerated(this, new EventArgs());
                    }
                }
            }
        }

        private void treeViewObjects_BeforeLabelEdit(object sender, CancelableNodeEventArgs e)
        {
            var nodeTag = e.TreeNode.Tag.ToString();
            if (nodeTag == "root" || nodeTag == "tables" || nodeTag == "procs" || nodeTag == "views" || nodeTag == "functions")
            {
                e.Cancel = true;
            }
            else if (nodeTag == "table" || nodeTag == "proc" || nodeTag == "view" || nodeTag == "function")
            {
                e.Cancel = true;
            }

            else
            {
                cellTextBeforeEdit = e.TreeNode.Text;
            }
        }

        private void removeGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tNodeNewGrpClicked != null)
            {
                if (tNodeNewGrpClicked.Nodes.Count > 0)
                {
                    //MessageBox.Show("Group having child items can not be deleted");
                    if (MessageBox.Show("This group has child items. Do you want to remove them all?", "SQT", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        RemoveChildNodes(tNodeNewGrpClicked);

                        var data = new Category.Data();
                        data.CategoryId = Convert.ToInt32(GetKey(tNodeNewGrpClicked.Key));
                        Category.Delete(data);
                        tNodeNewGrpClicked.Remove();
                    }
                }
                else
                {
                    var data = new Category.Data();
                    data.CategoryId = Convert.ToInt32(GetKey(tNodeNewGrpClicked.Key));
                    Category.Delete(data);

                    tNodeNewGrpClicked.Remove();
                }
            }
        }

        private void ToolStripMenuItemNewGroup_Click(object sender, EventArgs e)
        {
            if (tNodeNewGrpClicked != null)
            {
                if (!tNodeNewGrpClicked.Expanded)
                {
                    tNodeNewGrpClicked.Expanded = true;
                    //CreatChildNodes(tNodeNewGrpClicked);
                }
                var newGrpNode = tNodeNewGrpClicked.Nodes.Add("New Group" + tNodeNewGrpClicked.Key, "New Group");
                //newGrpNode.e .EnsureVisible();
                //treeViewObjects.SelectedNode = newGrpNode;
                //treeViewObjects.LabelEdit = true;                
                newGrpNode.Tag = "group";
                //treeViewObjects.Override.LabelEdit = DefaultableBoolean.True;
                //newGrpNode.image
                if (!newGrpNode.IsEditing)
                {
                    try
                    {
                        newGrpNode.BeginEdit();
                    }
                    catch { }
                }
                //tNodeNewGrpClicked.Expand();
                newGrpNode.Override.NodeAppearance.Image = GetImageIndex(Convert.ToString(newGrpNode.Tag));
            }
        }

        #endregion

    }
}
