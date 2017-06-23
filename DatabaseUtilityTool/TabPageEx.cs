using System.Windows.Forms;

namespace DatabaseUtilityTool
{

    /// <summary>
    /// Summary description for TabPage.
    /// </summary>
    public partial class TabPageEx : System.Windows.Forms.TabPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        public TabPageEx(System.ComponentModel.IContainer container)
        {
            ///
            /// Required for Windows.Forms Class Composition Designer support
            ///
            container.Add(this);
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public TabPageEx()
        {
            ///
            /// Required for Windows.Forms Class Composition Designer support
            ///
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public TabPageEx(string text)
        {
            ///
            /// Required for Windows.Forms Class Composition Designer support
            ///
            InitializeComponent();
            this.Text = text;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            components = new System.ComponentModel.Container();


        }

        public override string Text
        {
            get
            {
                return base.Text + "        ";
            }
            set
            {
                base.Text = value;
            }
        }
        private ContextMenu ctxtMenu = null;
        public ContextMenu Menu
        {
            get
            {
                return this.ctxtMenu;
            }
            set
            {

                this.ctxtMenu = value;
            }
        }




        #endregion
    }
}
