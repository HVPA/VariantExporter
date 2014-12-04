using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using DBConnLib;
using AuditLogDB;
using SiteConf;
using VariantExporterWinGUI.Util;
using ExporterCommon;
using ExporterCommon.Conf;

namespace VariantExporterWinGUI
{
    public partial class FrmUpload : Form
    {
        private FrmMain _frmMain;
        private string _OrgHashCode;
        
        public FrmUpload(FrmMain frmMain, string OrgHashCode)
        {
            _frmMain = frmMain;
            _OrgHashCode = OrgHashCode;
            
            InitializeComponent();

            if (_OrgHashCode != string.Empty)
                LoadListBox();
        }

        public void LoadListBox()
        {
            lstUpload.DisplayMember = "Name";
            lstUpload.ValueMember = "ID";

            SiteConf.OrgSite.Object orgSite = ExporterCommon.DataLoader.GetOrgSite("OrgHashCode", _OrgHashCode);

            List<SiteConf.Upload.Object> uploads = new List<SiteConf.Upload.Object>();
            if (orgSite.HVPAdmin.HasValue)
            {
                // check if orgsite is HVPAdmin site
                if (orgSite.HVPAdmin.Value)
                {
                    // get all the other sites related
                    List<SiteConf.OrgSite.Object> sites = ExporterCommon.DataLoader.GetAdminOrgSites(orgSite.ID);

                    foreach (SiteConf.OrgSite.Object site in sites)
                    {
                        List<SiteConf.Upload.Object> siteUploads = ExporterCommon.DataLoader.GetUploadList(site.OrgHashCode);

                        uploads = uploads.Concat(siteUploads).ToList();
                    }
                }
            }
            // if not HVP admin site then get the uploads using the orghashcode
            if (uploads.Count == 0)
                uploads = ExporterCommon.DataLoader.GetUploadList(_OrgHashCode);

            lstUpload.DataSource = uploads;
        }

        private void SelectFromList()
        {
            try
            {
                string uploadID = lstUpload.SelectedValue.ToString();
                SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(int.Parse(uploadID));
                SiteConf.OrgSite.Object orgSite = ExporterCommon.DataLoader.GetOrgSite("ID", upload.GetOrgSiteID().Value.ToString());

                _frmMain._OrgHashCode = orgSite.OrgHashCode;
                _frmMain.LoadUpload(int.Parse(uploadID));
                
                this.Close();
            }
            catch (Exception ex)
            {
                // log error
                Log log = new Log(true);
                log.write(ex.ToString());
                // display nice message to user
                MessageBox.Show("Could not load the selected Uploaded. Please check you are using the latest version of Variant Exporter.", "Selection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectFromList();
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SelectFromList();
            }
        }

        private void lstUpload_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // get the list item value from index
            SelectFromList();
        }

        private void SelectUpload(int uploadID)
        {
            _frmMain.LoadUpload(uploadID);
            this.Close();
        }

        private void FrmUpload_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if no upload has been previously selected(uploadID == 0), then it is a first time start up
            // if user closes this window then we exit the app completely
            if (_frmMain._uploadID == 0)
                Application.Exit();
        }
    }
}
