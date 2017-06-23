namespace DatabaseUtilityTool
{
    partial class ObjectTree
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinTree.Override _override1 = new Infragistics.Win.UltraWinTree.Override();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeViewObjects = new Infragistics.Win.UltraWinTree.UltraTree();
            this.contextMenuStripTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemNewGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemClass = new System.Windows.Forms.ToolStripMenuItem();
            this.removeGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewObjects)).BeginInit();
            this.contextMenuStripTreeView.SuspendLayout();
            this.contextMenuStripActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeViewObjects);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 542);
            this.panel1.TabIndex = 0;
            // 
            // treeViewObjects
            // 
            this.treeViewObjects.AllowDrop = true;
            this.treeViewObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewObjects.ImagePadding = 15;
            this.treeViewObjects.Location = new System.Drawing.Point(0, 0);
            this.treeViewObjects.Name = "treeViewObjects";
            _override1.LabelEdit = Infragistics.Win.DefaultableBoolean.True;
            this.treeViewObjects.Override = _override1;
            this.treeViewObjects.Size = new System.Drawing.Size(221, 542);
            this.treeViewObjects.TabIndex = 2;
            this.treeViewObjects.ViewStyle = Infragistics.Win.UltraWinTree.ViewStyle.Standard;
            this.treeViewObjects.AfterLabelEdit += new Infragistics.Win.UltraWinTree.AfterNodeChangedEventHandler(this.treeViewObjects_AfterLabelEdit);
            this.treeViewObjects.AfterSelect += new Infragistics.Win.UltraWinTree.AfterNodeSelectEventHandler(this.treeViewObjects_AfterSelect);
            this.treeViewObjects.BeforeLabelEdit += new Infragistics.Win.UltraWinTree.BeforeNodeChangedEventHandler(this.treeViewObjects_BeforeLabelEdit);
            this.treeViewObjects.BeforeExpand += new Infragistics.Win.UltraWinTree.BeforeNodeChangedEventHandler(this.treeViewObjects_BeforeExpand);
            this.treeViewObjects.SelectionDragStart += new System.EventHandler(this.treeViewObjects_SelectionDragStart);
            this.treeViewObjects.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewObjects_DragDrop);
            this.treeViewObjects.DragOver += new System.Windows.Forms.DragEventHandler(this.treeViewObjects_DragOver);
            this.treeViewObjects.DragLeave += new System.EventHandler(this.treeViewObjects_DragLeave);
            this.treeViewObjects.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.treeViewObjects_QueryContinueDrag);
            this.treeViewObjects.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeViewObjects_MouseDoubleClick);
            this.treeViewObjects.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeViewObjects_MouseUp);
            // 
            // contextMenuStripTreeView
            // 
            this.contextMenuStripTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemNewGroup,
            this.removeGroupToolStripMenuItem});
            this.contextMenuStripTreeView.Name = "contextMenuStrip1";
            this.contextMenuStripTreeView.Size = new System.Drawing.Size(153, 70);
            // 
            // ToolStripMenuItemNewGroup
            // 
            this.ToolStripMenuItemNewGroup.Name = "ToolStripMenuItemNewGroup";
            this.ToolStripMenuItemNewGroup.Size = new System.Drawing.Size(127, 22);
            this.ToolStripMenuItemNewGroup.Text = "New Group";
            this.ToolStripMenuItemNewGroup.Click += new System.EventHandler(this.ToolStripMenuItemNewGroup_Click);
            // 
            // contextMenuStripActions
            // 
            this.contextMenuStripActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemClass});
            this.contextMenuStripActions.Name = "contextMenuStrip1";
            this.contextMenuStripActions.Size = new System.Drawing.Size(159, 26);
            // 
            // toolStripMenuItemClass
            // 
            this.toolStripMenuItemClass.Name = "toolStripMenuItemClass";
            this.toolStripMenuItemClass.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItemClass.Text = "Generate Classes";
            this.toolStripMenuItemClass.Click += new System.EventHandler(this.toolStripMenuItemClass_Click);
            // 
            // removeGroupToolStripMenuItem
            // 
            this.removeGroupToolStripMenuItem.Name = "removeGroupToolStripMenuItem";
            this.removeGroupToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.removeGroupToolStripMenuItem.Text = "Remove Group";
            this.removeGroupToolStripMenuItem.Click += new System.EventHandler(this.removeGroupToolStripMenuItem_Click);
            // 
            // ObjectTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ObjectTree";
            this.Size = new System.Drawing.Size(221, 542);
            this.Load += new System.EventHandler(this.ObjectTree_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeViewObjects)).EndInit();
            this.contextMenuStripTreeView.ResumeLayout(false);
            this.contextMenuStripActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public Infragistics.Win.UltraWinTree.UltraTree treeViewObjects;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNewGroup;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripActions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClass;
        private System.Windows.Forms.ToolStripMenuItem removeGroupToolStripMenuItem;
    }
}
