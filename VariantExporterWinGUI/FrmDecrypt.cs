using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ExporterCommon;

namespace VariantExporterWinGUI
{
    public partial class FrmDecrypt : Form
    {
        public FrmDecrypt(string passKey)
        {
            InitializeComponent();

            // TODO: get the passkey from the xml config file
            txtPassKey.Text = passKey;
            
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string result = Encryption.Decrypt(txtEncrypted.Text, txtPassKey.Text);

                lblResult.Text = result;
            }
            catch (Exception ex)
            {
                // oh noes, better do something!
                lblResult.Text = ex.ToString(); 
            }
        }

        private void btnClearAll_Click_1(object sender, EventArgs e)
        {
            txtEncrypted.Text = "";
            txtPassKey.Text = "";
            lblResult.Text = "";
        }

       
    }
}
