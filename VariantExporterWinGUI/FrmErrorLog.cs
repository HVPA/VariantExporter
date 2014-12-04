using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NHibernate;
using DBConnLib;

namespace VariantExporterWinGUI
{
    public partial class FrmErrorLog : Form
    {
        FrmMain _frmMain;
        
        public FrmErrorLog(FrmMain frmMain, DataTable errorDt)
        {
            InitializeComponent();
            LoadDataGrid(errorDt);

            _frmMain = frmMain;
        }

        private void LoadDataGrid(DataTable dt)
        {
            dgErrorLog.DataSource = dt;
            dgErrorLog.Columns["Status"].Visible = false;
            dgErrorLog.Columns["DateSubmitted"].Visible = false;
        }

        /// <summary>
        /// Closes the error form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIgnore_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Close the error screen and reloads the data again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReload_Click(object sender, EventArgs e)
        {
            _frmMain.LoadDataGrid(_frmMain.GetDataSourcePath());   
            this.Close();
        }
    }
}
