namespace DatabaseUtilityTool
{
    partial class frmMaster
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
            this.mnuMenu = new System.Windows.Forms.MenuStrip();
            this.formsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sampleDockManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formsToolStripMenuItem});
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Size = new System.Drawing.Size(1180, 24);
            this.mnuMenu.TabIndex = 0;
            this.mnuMenu.Text = "menuStrip1";
            // 
            // formsToolStripMenuItem
            // 
            this.formsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem,
            this.testFormToolStripMenuItem,
            this.sampleDockManagerToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.connectionManagerToolStripMenuItem});
            this.formsToolStripMenuItem.Name = "formsToolStripMenuItem";
            this.formsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.formsToolStripMenuItem.Text = "Forms";
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.applicationToolStripMenuItem.Text = "Application";
            this.applicationToolStripMenuItem.Click += new System.EventHandler(this.applicationToolStripMenuItem_Click);
            // 
            // testFormToolStripMenuItem
            // 
            this.testFormToolStripMenuItem.Name = "testFormToolStripMenuItem";
            this.testFormToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.testFormToolStripMenuItem.Text = "Test Form";
            this.testFormToolStripMenuItem.Click += new System.EventHandler(this.testFormToolStripMenuItem_Click);
            // 
            // sampleDockManagerToolStripMenuItem
            // 
            this.sampleDockManagerToolStripMenuItem.Name = "sampleDockManagerToolStripMenuItem";
            this.sampleDockManagerToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.sampleDockManagerToolStripMenuItem.Text = "Sample Dock Manager";
            this.sampleDockManagerToolStripMenuItem.Click += new System.EventHandler(this.sampleDockManagerToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // connectionManagerToolStripMenuItem
            // 
            this.connectionManagerToolStripMenuItem.Name = "connectionManagerToolStripMenuItem";
            this.connectionManagerToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.connectionManagerToolStripMenuItem.Text = "Connection Manager";
            this.connectionManagerToolStripMenuItem.Click += new System.EventHandler(this.connectionManagerToolStripMenuItem_Click);
            // 
            // frmMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 496);
            this.Controls.Add(this.mnuMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMenu;
            this.Name = "frmMaster";
            this.Text = "Database Tool";
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMenu;
        private System.Windows.Forms.ToolStripMenuItem formsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sampleDockManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionManagerToolStripMenuItem;
    }
}