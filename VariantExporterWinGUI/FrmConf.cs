using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ExporterCommon;
using ExporterCommon.Conf;
using ExporterCommon.Plugins;

namespace VariantExporterWinGUI
{
    public partial class FrmConf : Form
    {
        private Configuration _conf = null;
        private DefaultExtractorContext _context = null;
        private FrmMain _frmMain = null;

        public FrmConf(Configuration conf, DefaultExtractorContext context, FrmMain frmMain, bool jumpToProxyTab)
        {
            InitializeComponent();

            _conf = conf;
            _context = context;
            _frmMain = frmMain;

            LoadControls(conf);       

            // jumps straight into the proxy tab
            if (jumpToProxyTab)
                tabControl1.SelectedTab = tabPage2;
        }

        private void LoadControls(Configuration conf)
        {
            // Basic Site Conf Settings
            txtPortalWebsite.Text = conf.PortalWebSite;
            txtOrganisationHashCode.Text = conf.OrganisationHashCode;
            txtPassKey.Text = conf.PassKey;
            txtPrivateKey.Text = conf.PrivateKey;
            txtServerAddress.Text = conf.ServerAddress;
            txtTestServerAddress.Text = conf.TestServerAddress;
            txtAutoUpdateLink.Text = conf.AutoUpdateLink;
            cbxClearCache.Checked = bool.Parse(conf.ClearCache);

            // Proxy Settings
            txtProxyAddress.Text = conf.ProxyAddress;
            mtxtProxyPort.Text = conf.ProxyPort;
            txtProxyUsername.Text = conf.ProxyUser;
            txtProxyPassword.Text = conf.ProxyPassword;
            txtProxyUserDomain.Text = conf.ProxyUserDomain;
            
            // GRHANITE Settings
            cbxEnableGRHANITE.Checked = conf.EnableGRHANITE;
            txtLinkageKey.Text = conf.LinkageKey;
            cbxNameTranspositionAllowed.Checked = conf.NameTranspositionAllowed;
            cbxTightSexMatching.Checked = conf.TightSexMatching;
            cbxYoBFatherSonChecking.Checked = conf.YOBFatherSonChecking;
            cbxAdditionalYOBPCodeHash.Checked = conf.AdditionalYOBPCodeHash;
            cbxExcludeMedicare.Checked = conf.ExcludeMedicare;
            cbxSuppliesMedicareDigits5to9Only.Checked = conf.SuppliesMedicareDigits5to9Only;
            cbxValidateMedicareChecksum.Checked = conf.ValidateMedicareChecksum;
        }

        private void SaveControls()
        {
            // Basic Site Conf Settings
            _conf.PortalWebSite = txtPortalWebsite.Text;
            _conf.OrganisationHashCode = txtOrganisationHashCode.Text;
            _conf.PassKey = txtPassKey.Text;
            _conf.PrivateKey = txtPrivateKey.Text;
            _conf.ServerAddress = txtServerAddress.Text;
            _conf.TestServerAddress = txtTestServerAddress.Text;
            _conf.AutoUpdateLink = txtAutoUpdateLink.Text;
            _conf.ClearCache = cbxClearCache.Checked.ToString();

            // Proxy Settings
            _conf.ProxyAddress = txtProxyAddress.Text;
            _conf.ProxyPort = mtxtProxyPort.Text;
            _conf.ProxyUser = txtProxyUsername.Text;
            _conf.ProxyPassword = txtProxyPassword.Text;
            _conf.ProxyUserDomain = txtProxyUserDomain.Text;

            // GRHANITE Settings
            _conf.EnableGRHANITE = cbxEnableGRHANITE.Checked;
            _conf.LinkageKey = txtLinkageKey.Text;
            _conf.NameTranspositionAllowed = cbxNameTranspositionAllowed.Checked;
            _conf.TightSexMatching = cbxTightSexMatching.Checked;
            _conf.YOBFatherSonChecking = cbxYoBFatherSonChecking.Checked;
            _conf.AdditionalYOBPCodeHash = cbxAdditionalYOBPCodeHash.Checked;
            _conf.ExcludeMedicare = cbxExcludeMedicare.Checked;
            _conf.SuppliesMedicareDigits5to9Only = cbxSuppliesMedicareDigits5to9Only.Checked;
            _conf.ValidateMedicareChecksum = cbxValidateMedicareChecksum.Checked;

            // save fields by serializing to xml
            _context.SerializeObject();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreen ss = new SplashScreen();

            try
            {
                // wait while do stuff
                ss.Show();

                // disable controls for this form and frmMain
                DisableAllControls();
                _frmMain.DisableAllControls();

                // checked if proxy settings have changed
                bool proxyChang = HasProxyChanged();

                // write changes to config.xml
                SaveControls();

                // reload fields into main form
                if (proxyChang)
                    _frmMain.ReloadProxy();

                ss.Close();

                // close this dialogue form
                this.Close();
            }
            catch(Exception ex)
            {
                ss.Close();
                Log log = new Log(true);
                log.write(ex.ToString());
                MessageBox.Show("Something went wrong while saving. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // re-enable all controls again
                EnableAllControls();
                _frmMain.EnableAllControls();
            }
        }

        private bool HasProxyChanged()
        {
            if (txtProxyAddress.Text != _conf.ProxyAddress)
                return true;

            if (mtxtProxyPort.Text != _conf.ProxyPort)
                return true;

            if (txtProxyUsername.Text != _conf.ProxyUser)
                return true;

            if (txtProxyPassword.Text != _conf.ProxyPassword)
                return true;

            if (txtProxyUserDomain.Text != _conf.ProxyUserDomain)
                return true;

            return false;
        }

        /// <summary>
        /// Disables all controls in a Form, during application "thinking" time and you don't 
        /// want your uses clicking on controls.
        /// </summary>
        /// <param name="frm"></param>
        private void DisableAllControls()
        {
            foreach (Control con in this.Controls)
            {
                con.Enabled = false;
            }
        }

        /// <summary>
        /// Enables all controls in a Form, re-enable all controls on form after disabling them during 
        /// an applications "thinking time" where you don't want the user to click on your form controls.
        /// </summary>
        /// <param name="frm"></param>
        private void EnableAllControls()
        {
            foreach (Control con in this.Controls)
            {
                con.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSetPrivateKey_Click(object sender, EventArgs e)
        {
            Log log = new Log(true);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Private Key Files (.private)|*.private";
            ofd.ShowDialog();

            if (ofd.FileName != string.Empty)
            {
                log.write("Source private key location: " + ofd.FileName);

                // need to overwrite the new file name in the conf xml
                string fileName = System.IO.Path.GetFileName(ofd.FileName);

                try
                {
                    // copy file to app keys directory, this will overwrite existing file if filenames are the same
                    string copyPath = CommonAppPath.GetCommonAppPath() +"\\Keys\\" + fileName;
                    log.write("Private key Copied location: " + copyPath);
                    System.IO.File.Copy(ofd.FileName, copyPath, true);

                    // set the new private key filename and write to xml file
                    _conf.PrivateKey = fileName;
                    _context.SerializeObject();

                    // update the field
                    txtPrivateKey.Text = _conf.PrivateKey;

                    // inform user new private key has been set
                    MessageBox.Show("New Private Key has been set", "Success!", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to import file please check file path is correct /n" + ex.ToString(), "Error importing file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
