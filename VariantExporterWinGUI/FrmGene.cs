using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using DBConnLib;
using AuditLogDB;
using SiteConf;
using VariantExporterWinGUI.Util;

namespace VariantExporterWinGUI
{
    public partial class FrmGene : Form
    {
        public int _GeneID;
        public int _uploadID;
        private FrmMain _main;

        public FrmGene(FrmMain main, int GeneID, int uploadID)
        {
            InitializeComponent();
            _main = main;
            _GeneID = GeneID;
            _uploadID = uploadID;

            if (_GeneID == -1)
                AddSettings();
            else
                EditSettings();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadGene(int geneID)
        {
            SiteConf.Gene.Object gene = ExporterCommon.DataLoader.GetGene(geneID);
            if (gene != null)
            {
                txtGene.Text = gene.GeneName;
                txtRefSeq.Text = gene.RefSeqName;
                txtRefSeqVersion.Text = gene.RefSeqVersion;

                _GeneID = gene.ID;
            }
        }

        public void AddSettings()
        {
            this.btnSave.Text = "Add";
            this.Text = "Gene Add";
        }

        public void EditSettings()
        {
            this.btnSave.Text = "Update";
            this.Text = "Gene Edit";
            LoadGene(_GeneID);
        }

        public void SetText(string gene, string refSeq)
        {
            this.txtGene.Text = gene;
            this.txtRefSeq.Text = refSeq;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Checks if gene field has been field correctly, display error msg if not
            if (txtGene.Text.Trim() == string.Empty)
                lblGeneError.Visible = true;
            else
            {
                lblGeneError.Visible = false;
                
                // add the gene to the upload
                SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(_uploadID);

                // if valid then we can save
                try
                {
                    if (_GeneID == -1)
                    {
                        // Saving
                        SiteConf.Gene.Object gene = new SiteConf.Gene.Object();
                        gene.GeneName = txtGene.Text;
                        gene.RefSeqName = txtRefSeq.Text;
                        gene.RefSeqVersion = txtRefSeqVersion.Text;

                        gene.upload = @"/api/v1/upload/" + upload.ID.ToString() + "/";

                        ExporterCommon.DataSaver.SaveNewRestObject(gene);
                    }
                    else
                    {
                        // Editing
                        // get gene from id
                        SiteConf.Gene.Object gene = ExporterCommon.DataLoader.GetGene(_GeneID);
                        if (gene != null)
                        {
                            gene.GeneName = txtGene.Text;
                            gene.RefSeqName = txtRefSeq.Text;
                            gene.RefSeqVersion = txtRefSeqVersion.Text;

                            ExporterCommon.DataSaver.UpdateRestObject(gene);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // error saving data
                    MessageBox.Show("An error has occured while trying to save a gene. Please try again, if issue continues please contact HVP.", "Error Saving gene!", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // update the datagrid on main form
                    _main.ReloadDgGene(upload);
                }
                
                // close the form
                this.Close();
            }
        }
    }
}
