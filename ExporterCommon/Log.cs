using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace ExporterCommon
{
    public class Log
    {
        private bool _enabled;
        private TextBox _txtLog;
        private string _path = null; // path overrides existing path from getLogDir()

        public Log(bool enable)
        {
            _enabled = enable;
        }

        /// <summary>
        /// Will create log obj that will write logs
        /// to a specified location.
        /// </summary>
        /// <param name="path"></param>
        public Log(string path)
        {
            _path = path;
            _enabled = true;
        }

        public Log(bool enable, TextBox txtLog)
        {
            _enabled = enable;
            _txtLog = txtLog;
        }

        public void write(string message)
        {
            if (_enabled)
                WriteToLog(message);
        }

        public string GetLogPath()
        {
            return getLogDir() + "log.txt";
        }

        private void WriteToLog(string message)
        {
            string logDir;
            if (_path == null)
                logDir = getLogDir();
            else
                logDir = _path;

            string logFile = "log.txt";
            string defaultLogPathFile = Path.Combine(logDir, logFile);
            
            DateTime now = DateTime.Now;
            string messageLog = now + ": " + message;

            StreamWriter sw;

            if (File.Exists(defaultLogPathFile))
            {
                FileInfo log = new FileInfo(defaultLogPathFile);

                // if initial log file size is bigger than 4mb then we rename existing log file
                // appending the date to
                if (log.Length > 4194304)
                {
                    // rename current file and create a new one
                    log.MoveTo(logDir + "log_"
                        + now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".txt");
                    // create new log file
                    sw = File.CreateText(defaultLogPathFile);
                }
                else
                {
                    // append message to existing file
                    sw = File.AppendText(defaultLogPathFile);
                }
            }
            else
            {
                // check if directory exist
                if (!Directory.Exists(getLogDir()))
                {
                    // create log directory
                    Directory.CreateDirectory(getLogDir());
                }
                
                // creates log file if none exist
                sw = File.CreateText(defaultLogPathFile);
            }

            sw.WriteLine(messageLog);

            // if textbox is specified for logging, then log messages to the textbox.
            if (_txtLog != null)
            {
                // if textbox has over 10000 lines we clear to prevent the app taking
                // up too much system resources.
                if (_txtLog.Lines.Length > 10000)
                    _txtLog.Clear();

                // writes to the GUI log text box
                _txtLog.AppendText(messageLog + Environment.NewLine);
            }
            sw.Close();
        }

        private string getLogDir()
        {
            string path = CommonAppPath.GetCommonAppPath();

            CommonAppPath.CreateDirectory("log");

            return path + "\\log\\";
            /*
            string assemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            
            if (System.IO.File.Exists(assemblyPath + "\\debug"))
                return Application.StartupPath + "\\log\\";
            else
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\VariantExporter\\log";*/
        }
    }
}
