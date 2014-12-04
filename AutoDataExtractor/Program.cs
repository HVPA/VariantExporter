using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;

using ExporterCommon;
using ExporterCommon.Output;
using AutoDataExtractor.Plugins;
using SiteConf;

namespace AutoDataExtractor
{
    public class Program
    {
        private static Configuration _conf;
        private static IExtractor _plugin;
        private static string _path;

        /// <summary>
        /// Command line exit Code Summary:
        /// 0 – Application completed its task with no errors
        /// 1 – Configuration.xml error
        /// 2 – Missing plugin dll
        /// 3 - Missing refmapper xml
        /// 4 - Datasource directory error
        /// 5 - XML output directory error
        /// 6 - Data Transfer failed
        /// 7 - Log directory error
        /// 8 - Failed to move sent file to the "completed" subdirectory
        /// 9 - No data to send. There was no valid data to send, double check source files.
        /// </summary>

        public static void Main(string[] args)
        {
            // path of where app is running from
            _path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            // init _conf and check files
            if (CheckFiles(_path))
            {
                // log to catch errors
                Log log = new Log(_conf.LogDirectory);
                
                // load plugin
                _plugin = PluginLoader.LoadExtractor("plugins\\" + _conf.Plugin);

                // DataTable to store all the extracted data from files
                DataTable results = new DataTable();
                
                string[] files = System.IO.Directory.GetFiles(_conf.DataSourceDirectory);
                // scans folder for file
                foreach (string file in files)
                {
                    // extract data
                    DataTable dt = _plugin.GetData(_conf, file);

                    // valid datatable of all valid rows
                    DataTable validDt = dt.Clone();
                    
                    // validate the data
                    foreach (DataRow dr in dt.Rows)
                    {
                        Validator validator = new Validator();
                        bool valid = true;

                        if (!validator.ValidateDataRow(dr))
                            valid = false;

                        if (!_plugin.Validate(dr, _path + "\\plugins\\" + _conf.RefMapper))
                            valid =  false;

                        // if valid import to validDt
                        if (valid)
                        {
                            validDt.ImportRow(dr);
                        }
                        else
                        {
                            log.write("Error validating row in " + file + ": ");
                            // print datarow contents to log file
                            foreach (DataColumn dc in dr.Table.Columns)
                            {
                                log.write(dc.ColumnName + " : " + dr[dc].ToString());
                            }
                        }
                    }

                    // merged data to Result
                    results.Merge(validDt);
                }
                
                // timestamp for use in the xml
                DateTime date = DateTime.Now;
                string timestamp = String.Format("{0:yyyyMMddHHmmss}", date);

                // if results table is empty then don't generate an empty xml
                if (results.Rows.Count != 0)
                {
                    // generate xml
                    XmlOutput output = new XmlOutput(null);
                    string xmlFileName = timestamp + ".xml";
                    // need to parse an emtpy genelist
                    IList<SiteConf.Gene.Object> geneList = new List<SiteConf.Gene.Object>();
                    output.Generate("", _conf.XmlOutputDirectory + "\\" + xmlFileName, results, null, null, _conf.OrgHashCode, geneList);

                    // call VariantExporterCmdLn to send data
                    if (SendData())
                    {
                        // check if completed directory exist
                        if (!System.IO.Directory.Exists(_conf.DataSourceDirectory + "\\completed"))
                        {
                            // create "completed" sub directory inside the xmlOutputDirectory
                            System.IO.Directory.CreateDirectory(_conf.DataSourceDirectory + "\\completed");
                        }
                        
                        // if send successful move files to completed sub directory
                        foreach (string file in files)
                        {
                            // rename file by appending  the date to the end of file
                            // from this "file.tsv" to this "file_20140716143423.tsv"
                            string renamedFile = System.IO.Path.GetFileNameWithoutExtension(file) + "(" + 
                                DateTime.Now.ToString("yyyyMMddHHmmss") + ")" + System.IO.Path.GetExtension(file);

                            try
                            {
                                System.IO.File.Move(file, _conf.DataSourceDirectory + "\\completed\\" + renamedFile);
                                log.write("Data from file: " + file + " sent.");
                            }
                            catch
                            {
                                log.write("Data from file: " + file + " was sent, but due to an error the file could not be moved to the completed sub directory.");
                            }
                        }
                    }
                }
                else
                {
                    log.write("No valid data to send. Check source that all mandatory fields are provided and correct.");
                    System.Environment.ExitCode = 9;
                }
            }
        }

        private static bool SendData()
        {
            bool sentSuccess = false;
            
            // sends all xml from output directory
            string[] xmlFiles = System.IO.Directory.GetFiles(_conf.XmlOutputDirectory);
            
            foreach (string xmlFile in xmlFiles)
            {
                // run the VariantExporterCmdLn.exe tool to encrypted, zip and send the data to HVP
                Process process = new Process();
                process.StartInfo.FileName = _path + "\\exporter\\VariantExporterCmdLn.exe";
                process.StartInfo.Arguments = "-f " + xmlFile;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // if exitcode is not "0" something went wrong.
                if (process.ExitCode != 0)
                {
                    sentSuccess = false;
                    Log log = new Log(_conf.LogDirectory);
                    log.write("Error sending file, check the VariantExporterCmdLn.exe ExitCode " +
                        "for precise error. VariantExporterCmdLn.exe ExitCode: " + process.ExitCode.ToString());
                    System.Environment.ExitCode = 6;
                }
                else
                {
                    sentSuccess = true;
                    
                    // check if completed directory exist
                    if (!System.IO.Directory.Exists(_conf.XmlOutputDirectory + "\\completed"))
                    {
                        // create "completed" sub directory inside the xmlOutputDirectory
                        System.IO.Directory.CreateDirectory(_conf.XmlOutputDirectory + "\\completed");
                    }
                    
                    // move sent xml to the completed directory
                    try
                    {
                        System.IO.File.Move(xmlFile, _conf.XmlOutputDirectory + "\\completed\\" + System.IO.Path.GetFileName(xmlFile));
                    }
                    catch (Exception ex)
                    {
                        Log log = new Log(_conf.LogDirectory);
                        log.write("Failed to move sent file to the \"completed\" subdirectory");
                        log.write(ex.ToString());
                        System.Environment.ExitCode = 8;
                    }
                }
                
                process.Close();
            }

            return sentSuccess;
        }

        private static bool CheckFiles(string path)
        {
            // Configuration.xml should be in the same location of autoextractor
            if (!System.IO.File.Exists(path + "\\Configuration.xml"))
            {
                System.Environment.ExitCode = 1;
                // if this is missing we can't write to the log dir
                //log.write("Error reading Configuration.xml file, check that file exists.");
                return false;
            }

            // deserialize conf xml
            _conf = XmlDeserialiser<Configuration>.Deserialise(path + "\\Configuration.xml");
            
            // check log path exists
            string logPath = "";
            if (!System.IO.Directory.Exists(_conf.LogDirectory))
            {
                logPath = path + "\\" + _conf.LogDirectory + "\\";
                if (!System.IO.Directory.Exists(logPath))
                {
                    System.Environment.ExitCode = 7;
                    // if missing directory then can not write to log file
                    return false;
                }
            }
            else
                logPath = _conf.LogDirectory + "\\";

            Log log = new Log(logPath);

            // check plugin dll
            if (!System.IO.File.Exists(path + "\\plugins\\" + _conf.Plugin))
            {
                System.Environment.ExitCode = 2;
                log.write("Error reading the dll plugin from the plugin directory, check that file exists.");
                return false;
            }

            // check refmapper xml
            if (!System.IO.File.Exists(path + "\\plugins\\" + _conf.RefMapper))
            {
                System.Environment.ExitCode = 3;
                log.write("Error reading the xml refmapper file from the plugin directory, check that file exists.");
                return false;
            }

            // check Data Source Directory
            if (!System.IO.Directory.Exists(_conf.DataSourceDirectory))
            {
                System.Environment.ExitCode = 4;
                log.write("Error could not find datasource directory, check folder location is correct.");
                return false;
            }

            // check output directory
            if (!System.IO.Directory.Exists(_conf.XmlOutputDirectory))
            {
                System.Environment.ExitCode = 5;
                log.write("Error could not find xml output directory, check folder location in correct.");
                return false;
            }

            // if all conditions passed then return truen
            return true;
        }
    }
}
