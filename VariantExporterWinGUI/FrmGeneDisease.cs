using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
using Microsoft.VisualBasic;

namespace VariantExporterWinGUI
{
    public partial class FrmGeneDisease : Form
    {
        private FrmMain _main;
        public int _GeneID;
        public int _UploadID;

        public FrmGeneDisease(FrmMain main, int GeneID, int uploadID)
        {
            InitializeComponent();

            _main = main;
            _GeneID = GeneID;
            _UploadID = uploadID;

            LoadGenesDropDown(_UploadID, _GeneID);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadGenesDropDown(int uploadID, int geneID)
        {
            IList<SiteConf.Gene.Object> geneList = ExporterCommon.DataLoader.GetGeneList(uploadID);

            ddlGenes.ValueMember = "ID";
            ddlGenes.DisplayMember = "GeneName";
            ddlGenes.DataSource = geneList;

            ddlGenes.SelectedValue = geneID;
        }

        private void LoadDiseaseTagListBox(int geneID)
        {
            SiteConf.Gene.Object gene = ExporterCommon.DataLoader.GetGene(geneID);
            List<SiteConf.DiseaseTag.Object> tagList = ExporterCommon.DataLoader.GetDiseaseTagList(geneID);
            lsbxDiseaseTags.DisplayMember = "Tag";
            lsbxDiseaseTags.ValueMember = "ID";
            lsbxDiseaseTags.DataSource = tagList;
        }

        private void ddlGenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDiseaseTagListBox(int.Parse(ddlGenes.SelectedValue.ToString()));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string selectedText = lsbxDiseaseTags.GetItemText(lsbxDiseaseTags.SelectedItem);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + selectedText + "'"
                
                , "Delete Disease Tag: " + selectedText, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // get DiseaseTag
                SiteConf.DiseaseTag.Object tag = ExporterCommon.DataLoader.GetDiseaseTag(
                    int.Parse(lsbxDiseaseTags.SelectedValue.ToString()));

                ExporterCommon.DataSaver.DeleteRestObject(tag);

                // reload the listbox to see the new addition
                LoadDiseaseTagListBox(int.Parse(ddlGenes.SelectedValue.ToString()));
            } 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // code to align the input box in the center
            const int approxInputBoxWidth = 370;
            const int approxInputBoxHeight = 158;

            FrmGeneDisease form = this;
            int left = form.Left + (form.Width / 2) - (approxInputBoxWidth / 2);
            left = left < 0 ? 0 : left;
            int top = form.Top + (form.Height / 2) - (approxInputBoxHeight / 2);
            top = top < 0 ? 0 : top;
            
            string tagStr = Interaction.InputBox("Enter disease tag:", "Input", "", left, top);

            if (tagStr != string.Empty)
            {
                // get current gene
                SiteConf.Gene.Object gene = ExporterCommon.DataLoader.GetGene(
                    int.Parse(ddlGenes.SelectedValue.ToString()));

                // get disease tags associated with gene
                List<SiteConf.DiseaseTag.Object> tagList = ExporterCommon.DataLoader.GetDiseaseTagList(gene.ID);

                // check if tag already exist or not
                foreach (SiteConf.DiseaseTag.Object t in tagList)
                {
                    // if tag exist then don't add it again
                    if (t.Tag.ToLower() == tagStr.ToLower())
                    {
                        // display message
                        lblErrorMsg.Text = tagStr + " already exists!";
                        lblErrorMsg.Visible = true;
                        return;
                    }
                }

                SiteConf.DiseaseTag.Object tag = new SiteConf.DiseaseTag.Object();
                tag.gene = @"/api/v1/gene/" + gene.ID.ToString() + "/";
                tag.Tag = tagStr;

                ExporterCommon.DataSaver.SaveNewRestObject(tag);

                lblErrorMsg.Visible = false;

                // reload the listbox to see the new addition
                LoadDiseaseTagListBox(int.Parse(ddlGenes.SelectedValue.ToString()));
            }
        }
    }
}
