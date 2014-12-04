using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Deployment.Application;

using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Utils;
using ExporterCommon.Core;
using ExporterCommon.Core.StandardColumns;

namespace ExporterCommon
{ 
    /// <summary>
    /// Validation rules for variant exportation from labs.
    /// </summary>
    public class Validator
    {
        public Validator()
        {
            
        }

        /// <summary>
        /// Validates a variant string, returns true if variant string is a correct HVGS nomnclature
        /// </summary>
        /// <param name="variant"></param>
        /// <returns></returns>
        public bool VariantValidate(string variant)
        {
            try
            {
                Process process = new Process();
                if (ApplicationDeployment.IsNetworkDeployed)
                    process.StartInfo.FileName = ApplicationDeployment.CurrentDeployment.DataDirectory + "\\Executables\\Validator.exe";
                else
                    process.StartInfo.FileName = Application.StartupPath + "\\Executables\\Validator.exe";
                process.StartInfo.Arguments = "-v " + "\"" + variant + "\"";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();

                return bool.Parse(output);
            }
            catch (Exception ex)
            {
                // something went wrong, we log it
                Log log = new Log(true);
                log.write("Error parsing variant: " + variant);
                log.write(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Validates that all columns in a datarow of the VariatInstanceTable are valid and meet
        /// the bare minimum requirements. Any errors that are caught will have an error message appended
        /// to the InvalidReason column of the datarow.
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public bool ValidateDataRow(DataRow dr)
        {
            bool valid = true;

            // check hashcode for organisation
            if (dr[Organisation.HashCode].ToString() == string.Empty)
            {
                valid = false;
                dr[0] = dr[0].ToString() + "organisationID: there was no OrganisationID, please check the Configuration.xml. ";
            }

            // check for patient hashcode
            if (dr[Patient.HashCode].ToString() == string.Empty)
            {
                valid = false;
                dr[0] = dr[0].ToString() + "PatientID: there was no recorded PatientID. ";
            }

            // check for variant instance hashcode
            if (dr[VariantInstance.HashCode].ToString() == string.Empty)
            {
                valid = false;
                dr[0] = dr[0].ToString() + "ResultID: there was no recorded ResultID. ";
            }

            // check for gene name
            if (dr[Gene.GeneName].ToString() == string.Empty)
            {
                valid = false;
                dr[0] = dr[0].ToString() + "GeneName: there was no recorded GeneName. ";
            }

            // check for RefSeq name 
            if (dr[Gene.RefSeqName].ToString() == string.Empty)
            {
                valid = false;
                dr[0] = dr[0].ToString() + "RefSeqName: there was no recorded RefSeqName. ";
            }

            // variant validation assumption here ensures that at least one variant
            // field is filled in.
            if (dr[VariantInstance.cDNA].ToString() == string.Empty &&
                dr[VariantInstance.mRNA].ToString() == string.Empty &&
                dr[VariantInstance.Genomic].ToString() == string.Empty &&
                dr[VariantInstance.Protein].ToString() == string.Empty
                )
            {
                valid = false;
                dr[0] = dr[0].ToString() + "No variant found: there should be at least one valid variant. ";
            }

            // check pathogenicity field is not blank
            if (dr[VariantInstance.Pathogenicity].ToString() == string.Empty)
            {
                valid = false;
                dr[0] = dr[0].ToString() + "Pathogenicity: there was no recorded pathogenicity. ";
            }
            else
            {
                // is the value numeric(int)
                int n;
                if (int.TryParse(dr[VariantInstance.Pathogenicity].ToString(), out n))
                {
                    // check pathogenicity ID match what is from HVP
                    // the ID should between 1 to 5
                    if (n > 0 && n <= 5) { }
                    else
                    {
                        valid = false;
                        dr[0] = dr[0].ToString() + "Pathogenicity: Must be an ID between 1 to 5. ";
                    }
                }
                else
                {
                    valid = false;
                    dr[0] = dr[0].ToString() + "Pathogenicity: PathID not a valid number. ";
                }
            }

            // HGVS check of each of the variant types
            // cDNA
            if (dr[VariantInstance.cDNA].ToString() != string.Empty)
            {
                string variant = dr[Gene.RefSeqName].ToString() + "." + dr[Gene.RefSeqVer].ToString() + ":" + dr[VariantInstance.cDNA].ToString();
                if (!VariantValidate(dr[VariantInstance.cDNA].ToString()))
                //if (!VariantValidate2(variant))
                {
                    valid = false;
                    dr[0] = dr[0].ToString() + "cDNA: was not in a correct HGVS nomenclature. ";
                }
            }
            // mRNA
            if (dr[VariantInstance.mRNA].ToString() != string.Empty)
            {
                // disabled until proper mRNA validation has been setup
                /*
                if (!VariantValidate(dr[VariantInstance.mRNA].ToString()))
                {
                    valid = false;
                    dr[0] = dr[0].ToString() + "mRNA: was not in a correct HGVS nomenclature. ";
                }*/
            }

            // Genomic
            if (dr[VariantInstance.Genomic].ToString() != string.Empty)
            {
                if (!VariantValidate(dr[VariantInstance.Genomic].ToString()))
                {
                    valid = false;
                    dr[0] = dr[0].ToString() + "Genomic: was not in a correct HGVS nomenclature. ";
                }
            }
            // Protein
            if (dr[VariantInstance.Protein].ToString() != string.Empty)
            {
                if (!VariantValidate(dr[VariantInstance.Protein].ToString()))
                {
                    valid = false;
                    dr[0] = dr[0].ToString() + "Protein: was not in a correct HGVS nomenclature. ";
                }
            }

            // Check instance date
            /*
            if (dr[VariantInstance.InstanceDate].ToString() == string.Empty)
            {
                valid = false;
                dr[0] = dr[0].ToString() + "Instance Date: there was no recorded instance date. ";
            }
            */
            return valid;
        }
    }
}
