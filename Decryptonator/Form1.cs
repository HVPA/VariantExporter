using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Decyprtonator.Utils;

namespace VariantExporterWinGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
