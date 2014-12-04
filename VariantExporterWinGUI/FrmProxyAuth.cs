using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace VariantExporterWinGUI
{
    public partial class FrmProxyAuth : Form
    {
        private FrmMain _frmMain;
        private IWebProxy _proxy;
        
        public FrmProxyAuth(FrmMain frmMain, IWebProxy proxy)
        {
            _frmMain = frmMain;
            _proxy = proxy;

            InitializeComponent();
        }

        private void GetCredientials()
        {
            //validate text fields
            if (txtUsername.Text != string.Empty || txtPassword.Text != string.Empty)
            {
                _proxy.Credentials = new NetworkCredential(txtUsername.Text, txtPassword.Text);
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Username and Password can not be blank", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            GetCredientials();
        }

        /// <summary>
        /// catches the user "Enter" key press which would submit form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                GetCredientials();
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                _frmMain._CancelledAuth = true;
                this.Close();
            }
        }

        private void FrmProxyAuth_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
