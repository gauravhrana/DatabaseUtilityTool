using DatabaseUtilityTool;

namespace DatabaseUtlityTool
{
    partial class SampleDockManager
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
            Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane1 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedLeft, new System.Guid("098facd4-e43c-44c0-ac1d-718959606785"));
            Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane1 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("896f32b4-d8d6-4e84-aca5-b5995b58e405"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("098facd4-e43c-44c0-ac1d-718959606785"), -1);
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ultraDockManager1 = new Infragistics.Win.UltraWinDock.UltraDockManager(this.components);
            this._SampleDockManagerUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._SampleDockManagerUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._SampleDockManagerUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._SampleDockManagerUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._SampleDockManagerAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
            this.windowDockingArea1 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
            this.dockableWindow1 = new Infragistics.Win.UltraWinDock.DockableWindow();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.tabCtlEx1 = new DatabaseUtilityTool.TabCtlEx();
            this.tabPage1 = new DatabaseUtilityTool.TabPageEx(this.components);
            this.tabPage2 = new DatabaseUtilityTool.TabPageEx(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).BeginInit();
            this.windowDockingArea1.SuspendLayout();
            this.dockableWindow1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            this.tabCtlEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(0, 18);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(190, 398);
            this.treeView1.TabIndex = 0;
            // 
            // ultraDockManager1
            // 
            dockableControlPane1.Control = this.treeView1;
            dockableControlPane1.OriginalControlBounds = new System.Drawing.Rectangle(47, 190, 121, 97);
            dockableControlPane1.Size = new System.Drawing.Size(100, 100);
            dockableControlPane1.Text = "treeView1";
            dockAreaPane1.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
            dockableControlPane1});
            dockAreaPane1.Size = new System.Drawing.Size(190, 416);
            this.ultraDockManager1.DockAreas.AddRange(new Infragistics.Win.UltraWinDock.DockAreaPane[] {
            dockAreaPane1});
            this.ultraDockManager1.HostControl = this;
            // 
            // _SampleDockManagerUnpinnedTabAreaLeft
            // 
            this._SampleDockManagerUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._SampleDockManagerUnpinnedTabAreaLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._SampleDockManagerUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 0);
            this._SampleDockManagerUnpinnedTabAreaLeft.Name = "_SampleDockManagerUnpinnedTabAreaLeft";
            this._SampleDockManagerUnpinnedTabAreaLeft.Owner = this.ultraDockManager1;
            this._SampleDockManagerUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 416);
            this._SampleDockManagerUnpinnedTabAreaLeft.TabIndex = 0;
            // 
            // _SampleDockManagerUnpinnedTabAreaRight
            // 
            this._SampleDockManagerUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._SampleDockManagerUnpinnedTabAreaRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._SampleDockManagerUnpinnedTabAreaRight.Location = new System.Drawing.Point(1263, 0);
            this._SampleDockManagerUnpinnedTabAreaRight.Name = "_SampleDockManagerUnpinnedTabAreaRight";
            this._SampleDockManagerUnpinnedTabAreaRight.Owner = this.ultraDockManager1;
            this._SampleDockManagerUnpinnedTabAreaRight.Size = new System.Drawing.Size(0, 416);
            this._SampleDockManagerUnpinnedTabAreaRight.TabIndex = 1;
            // 
            // _SampleDockManagerUnpinnedTabAreaTop
            // 
            this._SampleDockManagerUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._SampleDockManagerUnpinnedTabAreaTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._SampleDockManagerUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 0);
            this._SampleDockManagerUnpinnedTabAreaTop.Name = "_SampleDockManagerUnpinnedTabAreaTop";
            this._SampleDockManagerUnpinnedTabAreaTop.Owner = this.ultraDockManager1;
            this._SampleDockManagerUnpinnedTabAreaTop.Size = new System.Drawing.Size(1263, 0);
            this._SampleDockManagerUnpinnedTabAreaTop.TabIndex = 2;
            // 
            // _SampleDockManagerUnpinnedTabAreaBottom
            // 
            this._SampleDockManagerUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._SampleDockManagerUnpinnedTabAreaBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._SampleDockManagerUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 416);
            this._SampleDockManagerUnpinnedTabAreaBottom.Name = "_SampleDockManagerUnpinnedTabAreaBottom";
            this._SampleDockManagerUnpinnedTabAreaBottom.Owner = this.ultraDockManager1;
            this._SampleDockManagerUnpinnedTabAreaBottom.Size = new System.Drawing.Size(1263, 0);
            this._SampleDockManagerUnpinnedTabAreaBottom.TabIndex = 3;
            // 
            // _SampleDockManagerAutoHideControl
            // 
            this._SampleDockManagerAutoHideControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._SampleDockManagerAutoHideControl.Location = new System.Drawing.Point(0, 0);
            this._SampleDockManagerAutoHideControl.Name = "_SampleDockManagerAutoHideControl";
            this._SampleDockManagerAutoHideControl.Owner = this.ultraDockManager1;
            this._SampleDockManagerAutoHideControl.Size = new System.Drawing.Size(0, 0);
            this._SampleDockManagerAutoHideControl.TabIndex = 4;
            // 
            // windowDockingArea1
            // 
            this.windowDockingArea1.Controls.Add(this.dockableWindow1);
            this.windowDockingArea1.Dock = System.Windows.Forms.DockStyle.Left;
            this.windowDockingArea1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowDockingArea1.Location = new System.Drawing.Point(0, 0);
            this.windowDockingArea1.Name = "windowDockingArea1";
            this.windowDockingArea1.Owner = this.ultraDockManager1;
            this.windowDockingArea1.Size = new System.Drawing.Size(195, 416);
            this.windowDockingArea1.TabIndex = 6;
            // 
            // dockableWindow1
            // 
            this.dockableWindow1.Controls.Add(this.treeView1);
            this.dockableWindow1.Location = new System.Drawing.Point(0, 0);
            this.dockableWindow1.Name = "dockableWindow1";
            this.dockableWindow1.Owner = this.ultraDockManager1;
            this.dockableWindow1.Size = new System.Drawing.Size(190, 416);
            this.dockableWindow1.TabIndex = 9;
            // 
            // ultraGrid1
            // 
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGrid1.DisplayLayout.Appearance = appearance13;
            this.ultraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance22.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance22.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.GroupByBox.Appearance = appearance22;
            appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance23;
            this.ultraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance24.BackColor2 = System.Drawing.SystemColors.Control;
            appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance24;
            this.ultraGrid1.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid1.DisplayLayout.MaxRowScrollRegions = 1;
            appearance14.BackColor = System.Drawing.SystemColors.Window;
            appearance14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance14;
            appearance15.BackColor = System.Drawing.SystemColors.Highlight;
            appearance15.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance15;
            this.ultraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance16.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance16;
            appearance17.BorderColor = System.Drawing.Color.Silver;
            appearance17.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGrid1.DisplayLayout.Override.CellAppearance = appearance17;
            this.ultraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGrid1.DisplayLayout.Override.CellPadding = 0;
            this.ultraGrid1.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.Button;
            this.ultraGrid1.DisplayLayout.Override.FixedRowSortOrder = Infragistics.Win.UltraWinGrid.FixedRowSortOrder.FixOrder;
            this.ultraGrid1.DisplayLayout.Override.FixedRowStyle = Infragistics.Win.UltraWinGrid.FixedRowStyle.Top;
            appearance18.BackColor = System.Drawing.SystemColors.Control;
            appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance18.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance18.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance18;
            appearance19.TextHAlignAsString = "Left";
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance19;
            this.ultraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance20.BackColor = System.Drawing.SystemColors.Window;
            appearance20.BorderColor = System.Drawing.Color.Silver;
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance20;
            this.ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance21.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance21;
            this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid1.Location = new System.Drawing.Point(196, 72);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(1055, 332);
            this.ultraGrid1.TabIndex = 8;
            this.ultraGrid1.Text = "ultraGrid1";
            // 
            // tabCtlEx1
            // 
            this.tabCtlEx1.ConfirmOnClose = false;
            this.tabCtlEx1.Controls.Add(this.tabPage1);
            this.tabCtlEx1.Controls.Add(this.tabPage2);
            this.tabCtlEx1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabCtlEx1.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabCtlEx1.ItemSize = new System.Drawing.Size(300, 24);
            this.tabCtlEx1.Location = new System.Drawing.Point(786, 33);
            this.tabCtlEx1.Multiline = true;
            this.tabCtlEx1.Name = "tabCtlEx1";
            this.tabCtlEx1.SelectedIndex = 0;
            this.tabCtlEx1.Size = new System.Drawing.Size(207, 10);
            this.tabCtlEx1.TabIndex = 7;
            this.tabCtlEx1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 52);
            this.tabPage1.Menu = null;
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(199, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1                                                                         " +
    "                               ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 52);
            this.tabPage2.Menu = null;
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(199, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2                                                                         " +
    "                               ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SampleDockManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 416);
            this.Controls.Add(this._SampleDockManagerAutoHideControl);
            this.Controls.Add(this.ultraGrid1);
            this.Controls.Add(this.tabCtlEx1);
            this.Controls.Add(this.windowDockingArea1);
            this.Controls.Add(this._SampleDockManagerUnpinnedTabAreaTop);
            this.Controls.Add(this._SampleDockManagerUnpinnedTabAreaBottom);
            this.Controls.Add(this._SampleDockManagerUnpinnedTabAreaLeft);
            this.Controls.Add(this._SampleDockManagerUnpinnedTabAreaRight);
            this.Name = "SampleDockManager";
            this.Text = "SampleDockManager";
            this.Load += new System.EventHandler(this.SampleDockManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).EndInit();
            this.windowDockingArea1.ResumeLayout(false);
            this.dockableWindow1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            this.tabCtlEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinDock.UltraDockManager ultraDockManager1;
        private Infragistics.Win.UltraWinDock.AutoHideControl _SampleDockManagerAutoHideControl;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _SampleDockManagerUnpinnedTabAreaTop;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _SampleDockManagerUnpinnedTabAreaBottom;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _SampleDockManagerUnpinnedTabAreaLeft;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _SampleDockManagerUnpinnedTabAreaRight;
        private System.Windows.Forms.TreeView treeView1;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea1;
        private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow1;
        private TabCtlEx tabCtlEx1;
        private TabPageEx tabPage1;
        private TabPageEx tabPage2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;

    }
}