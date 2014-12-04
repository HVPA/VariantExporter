using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography;

using ExporterCommon.Plugins;
using ExporterCommon.Conf;
using ExporterCommon.Core;
using ExporterCommon.Core.StandardColumns;
using AuditLogDB;
using SiteConf;

using GRHANITE_HasherDLL;

namespace ExporterCommon.Output
{
    public class XmlOutput
    {
        private Configuration _Conf;
        private GRHANITEHasher _Hasher;
        private bool _EnableGrhanite = false;
        
        public XmlOutput()
        {
            DefaultExtractorContext context = new DefaultExtractorContext();
            _Conf = context.GetConfiguration();
            _EnableGrhanite = _Conf.EnableGRHANITE;
            _Hasher = InitGrhaniteHasher(_Conf);
        }

        public XmlOutput(Configuration conf)
        {
            _Conf = conf;
            if (conf != null)
                _Hasher = InitGrhaniteHasher(_Conf);
            else
                _Hasher = null;
        }

        /// <summary>
        /// Checks for sufficient paitent details to generate a grhanite hash
        /// </summary>
        /// <returns></returns>
        private bool ValidGrhaniteFields(DataRow dr)
        {
            // check if sufficient fields are here to do a proper grhanite linkage
            string Surname = dr[Patient.Surname].ToString();
            string FirstName = dr[Patient.FirstName].ToString();
            string Sex = dr[Patient.Sex].ToString();
            string DoB = dr[Patient.DoB].ToString();
            string Medicare = dr[Patient.Medicare].ToString();
            string PostCode = dr[Patient.PostCode].ToString();

            // bare minimum fields should have all of these 
            if (Surname == string.Empty || FirstName == string.Empty || DoB == string.Empty)
                return false;

            // should have at least 1 of these fields
            if (Medicare == string.Empty && PostCode == string.Empty)
                return false;

            // past both if statements checks then return true
            return true;
        }

        /// <summary>
        /// Generates a MD5 hash for use for the GRHANITE GUID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CreateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }

            return sb.ToString();
        }

        /// <summary>
        /// Initiates the GRHANITE hasher with all the params required to get it working from the conf.
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        private GRHANITEHasher InitGrhaniteHasher(Configuration conf)
        {
            GRHANITEHasher hasher = new GRHANITEHasher();

            // init hasher - with conf
            bool initialiseResult = hasher.InitialiseGRHANITEHashDefinition(conf.OrganisationHashCode, conf.LinkageKey, conf.AdditionalYOBPCodeHash,
                conf.ExcludeMedicare, conf.NameTranspositionAllowed, conf.SuppliesMedicareDigits5to9Only, conf.TightSexMatching, conf.ValidateMedicareChecksum,
                conf.YOBFatherSonChecking);

            if (!initialiseResult)
                throw new Exception("Error - unable to validate the GRHANITE License for this computer - please license it.");

            return hasher;
        }

        /// <summary>
        /// Gets the Grhanite sex type
        /// </summary>
        /// <param name="sexType"></param>
        /// <returns></returns>
        private GRHANITEHasher.SexType GetSexType(string sexType)
        {
            switch (sexType.ToLower()[0])
            {
                // male
                case 'm':
                    return GRHANITEHasher.SexType.M;

                // female
                case 'f':
                    return GRHANITEHasher.SexType.F;

                // indeterminate
                case 'i':
                    return GRHANITEHasher.SexType.I;

                // unknown
                case 'n':
                    return GRHANITEHasher.SexType.N;

                // unknown
                case 'u':
                    return GRHANITEHasher.SexType.N;

                // default to unknown
                default:
                    return GRHANITEHasher.SexType.N;
            }
        }

        public void Generate(string portalWebSite, string xmlOutputFile, DataTable instances, 
            Hashtable refSeqName, Hashtable refSeqVer, string organisation, 
            IList<SiteConf.Gene.Object> geneList)
        {
            string destination = portalWebSite;

            //Hashtable refSeqName = new Hashtable();
            //refSeqName["BRCA1"] = "NM001732";
            //Hashtable refSeqVer = new Hashtable();
            //refSeqVer["BRCA1"] = "1";

            string[] optionalColumns = new string[] {
                VariantInstance.PatientAge,
                VariantInstance.TestMethod,
                VariantInstance.SampleTissue,
                VariantInstance.SampleSource,
                VariantInstance.Justification,
                VariantInstance.PubMed,
                VariantInstance.RecordedInDatabase,
                VariantInstance.SampleStored,
                VariantInstance.PedigreeAvailable,
                VariantInstance.VariantSegregatesWithDisease,
                VariantInstance.HistologyStored
            };

            FileStream fileStream = new FileStream(xmlOutputFile, FileMode.Create);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter xmlWriter = XmlWriter.Create(fileStream, settings);

            xmlWriter.WriteStartDocument();
            {
                xmlWriter.WriteStartElement("HVP_Transaction");
                {
                    xmlWriter.WriteAttributeString("version", "1.0");
                    xmlWriter.WriteAttributeString("dateCreated", DateTime.Now.ToString("yyyy-MM-dd-HH:mm"));
                    xmlWriter.WriteAttributeString("destination", destination);

                    // gene/disease tags
                    xmlWriter.WriteStartElement("GeneDiseaseTags");
                    {
                        foreach (SiteConf.Gene.Object gene in geneList)
                        {
                            xmlWriter.WriteStartElement("Gene");
                            {
                                xmlWriter.WriteAttributeString("GeneName", gene.GeneName);
                                
                                // get all the DiseaseTags associated to theis gene
                                List<SiteConf.DiseaseTag.Object> tagList = DataLoader.GetDiseaseTagList(gene.ID);
                                foreach (SiteConf.DiseaseTag.Object tag in tagList)
                                {
                                    xmlWriter.WriteElementString("Tag", tag.Tag);
                                }
                            }
                            xmlWriter.WriteEndElement();
                        }
                    }
                    xmlWriter.WriteEndElement();

                    // uploadSystem
                    xmlWriter.WriteStartElement("UploadSystem");
                    {
                        xmlWriter.WriteAttributeString("name", "HVP");
                        xmlWriter.WriteAttributeString("version", "1.0");
                    }

                    foreach (DataRow dr in instances.Rows)
                    {
                        // Fill Null with blank strings
                        foreach (DataColumn col in instances.Columns)
                        {
                            if ((dr[col] == null || dr[col] == System.DBNull.Value) && col.DataType == typeof(string))
                            {
                                dr[col] = "";
                            }
                        }

                        xmlWriter.WriteStartElement("VariantInstance");
                        {
                            // Organisation
                            xmlWriter.WriteElementString(Organisation.HashCode, organisation);

                            // Patient
                            xmlWriter.WriteElementString(Patient.HashCode, (string)dr[Patient.HashCode]);

                            // Grhanite - Only when enabled from the conf
                            if (_EnableGrhanite)
                            {
                                xmlWriter.WriteStartElement("GrhaniteHashes");
                                {
                                    // generate grhanite hash if there is sufficient patient details to do so
                                    if (ValidGrhaniteFields(dr))
                                    {
                                        // begin hashing of patient details
                                        ArrayList aList = _Hasher.GenerateGRHANITEHashes(dr[Patient.HashCode].ToString(), dr[Patient.Surname].ToString(),
                                            dr[Patient.FirstName].ToString(), (DateTime)dr[Patient.DoB], ((DateTime)dr[Patient.DoB]).Year.ToString(),
                                            GetSexType(dr[Patient.Sex].ToString()), dr[Patient.PostCode].ToString(), dr[Patient.Medicare].ToString());

                                        //for (int i = 0; i < aList.Count; i++)
                                        for (int i = 0; i < aList.Count; i++)
                                        {
                                            xmlWriter.WriteStartElement("GrhaniteHash");
                                            {
                                                GRHANITEHasher.GRHANITE_HashResults result = (GRHANITEHasher.GRHANITE_HashResults)aList[i];

                                                xmlWriter.WriteElementString("HashType", result.Hashtype.ToString());
                                                xmlWriter.WriteElementString("Hash", result.Hash);
                                                xmlWriter.WriteElementString("AgrWeight", result.AgrWeight);
                                                xmlWriter.WriteElementString("GUID", CreateMD5Hash(i.ToString() + dr[Patient.HashCode].ToString()));
                                            }
                                            xmlWriter.WriteEndElement();
                                        }
                                    }
                                }
                                xmlWriter.WriteEndElement();
                            }

                            // Gene
                            string geneName = (string)dr[Core.StandardColumns.Gene.GeneName];
                            xmlWriter.WriteElementString(Core.StandardColumns.Gene.GeneName, geneName);
                            //xmlWriter.WriteElementString(Core.StandardColumns.Gene.RefSeqName, (string)refSeqName[(string)dr[Core.StandardColumns.Gene.GeneName]]);
                            //xmlWriter.WriteElementString(Core.StandardColumns.Gene.RefSeqVer, (string)refSeqVer[(string)dr[Core.StandardColumns.Gene.GeneName]]);
                            xmlWriter.WriteElementString(Core.StandardColumns.Gene.RefSeqName, dr[Gene.RefSeqName].ToString());
                            xmlWriter.WriteElementString(Core.StandardColumns.Gene.RefSeqVer, dr[Gene.RefSeqVer].ToString());

                            // Variant Instance
                            xmlWriter.WriteElementString(VariantInstance.Status, dr[VariantInstance.Status].ToString());
                            xmlWriter.WriteElementString(VariantInstance.HashCode, (string)dr[VariantInstance.HashCode]);
                            xmlWriter.WriteElementString(VariantInstance.VariantClass, (string)dr[VariantInstance.VariantClass]);
                            xmlWriter.WriteElementString(VariantInstance.cDNA, (string)dr[VariantInstance.cDNA]);
                            xmlWriter.WriteElementString(VariantInstance.mRNA, (string)dr[VariantInstance.mRNA]);
                            xmlWriter.WriteElementString(VariantInstance.Genomic, (string)dr[VariantInstance.Genomic]);
                            xmlWriter.WriteElementString(VariantInstance.Protein, (string)dr[VariantInstance.Protein]);
                            // this field may be empty if we are sending an instance that is marked for deletion
                            if (dr[VariantInstance.InstanceDate].ToString() != string.Empty)
                                xmlWriter.WriteElementString(VariantInstance.InstanceDate, ((DateTime)dr[VariantInstance.InstanceDate]).ToString("yyyy-MM-dd-HH:mm"));
                            
                            // date of when this xml was written
                            xmlWriter.WriteElementString(VariantInstance.DateSubmitted, DateTime.Now.ToString("yyyy-MM-dd-HH:mm"));
                            xmlWriter.WriteElementString(VariantInstance.Pathogenicity, (string)dr[VariantInstance.Pathogenicity]);

                            foreach (string optional in optionalColumns)
                            {
                                if (dr[optional] != null)
                                {
                                    if (dr[optional] is bool)
                                    {
                                        xmlWriter.WriteElementString(optional, dr[optional].ToString().ToLower());
                                    }
                                    else
                                    {
                                        xmlWriter.WriteElementString(optional, dr[optional].ToString());
                                    }
                                }
                            }
                        }
                        xmlWriter.WriteEndElement();
                    }

                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndDocument();

            xmlWriter.Flush();
            fileStream.Close();
        }
    }
}
