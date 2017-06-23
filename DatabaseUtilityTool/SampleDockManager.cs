using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Infragistics.Win.UltraWinGrid;
using DatabaseUtilityTool;

namespace DatabaseUtlityTool
{
    public partial class SampleDockManager : Form
    {

        private void testBind()
        {
            try
            {
                var conn = ApplicationConstants.ApplicationConnectionString;
                var sql = "select * from task";
                SqlConnection cn = new SqlConnection(conn);
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                da.ReturnProviderSpecificTypes = true;
                DataSet dsResult = new DataSet();
                da.Fill(dsResult, 0, 10, "result");


                var listColumns = new List<string>();
                var listTypes = new List<string>();


                ultraGrid1.DataSource = dsResult.Tables[0];

                //if (dsResult.Tables[0].Rows.Count > 0)
                //{
                //    //for (int iCount = 0; iCount < dsResult.Tables[0].Columns.Count; iCount++)
                //    //{                        
                //    //    ultraGrid1.Rows[0].Cells[iCount].Value = dsResult.Tables[0].Columns[iCount].DataType.Name;
                //    //}
                //    //ultraGrid1.Rows[0].Fixed = true;

                //    this.ultraGrid1.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
                //    this.ultraGrid1.DisplayLayout.Bands[0].Columns[0].Header.
                //    this.ultraGrid1.DisplayLayout.Bands[0].SortedColumns.Add(listColumns[1], false, true);


                //}



                for (int iCount = 0; iCount < dsResult.Tables[0].Columns.Count; iCount++)
                {
                    var type = SqlColumnToColumn(dsResult.Tables[0].Columns[iCount].DataType.Name);
                    var column = dsResult.Tables[0].Columns[iCount].ColumnName;

                    this.ultraGrid1.DisplayLayout.Bands[0].Columns[iCount].Header.Caption = column + "\n" + type;
                    //var tmp = this.ultraGrid1.DisplayLayout.Bands[0].Columns[iCount].Header.Height;
                    //var tmp = this.ultraGrid1.DisplayLayout.Bands[0].Header.Height;
                    this.ultraGrid1.DisplayLayout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand, true);
                    this.ultraGrid1.DisplayLayout.ResetCaptionAppearance();



                    //this.ultraGrid1.DisplayLayout.Bands[0].Columns[iCount].Header.ResetAppearance();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        public SampleDockManager()
        {
            InitializeComponent();
        }

        void tabCtlEx1_OnClose(object sender, CloseEventArgs e)
        {
            this.tabCtlEx1.Controls.Remove(this.tabCtlEx1.TabPages[e.TabIndex]);
        }

        private void SampleDockManager_Load(object sender, EventArgs e)
        {
            testBind();
        }

    }
}
