using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using NHibernate;
using DBConnLib;
using AuditLogDB;

namespace ExporterCommon
{
    public class FileWatcher
    {
        List<FileWatchers> _watcherList;
        
        public FileWatcher()
        {
            // list to store all fileWatchers for each upload
            _watcherList = new List<FileWatchers>();

            // get all the directories that need to be watched
            ISession iSession = DBFactory.CreateNewSession();
            IList<Upload> uploadList = DataLoader.GetSpreadSheetUploadList(iSession);
            iSession.Close();

            foreach (Upload upload in uploadList)
            {
                if (upload.DataSource != null)
                {
                    try
                    {
                        FileWatchers fw = new FileWatchers();
                        fw.FileWatcher = StartWatching(upload.DataSource);
                        fw.Upload = upload;

                        _watcherList.Add(fw);
                    }
                    catch
                    {
                        MessageBox.Show("The directory location path for " + upload.Name + " can not be found please check if the path name is correct.", 
                        "Spreadsheet directory path error");
                    }
                }
            }
        }

        private FileSystemWatcher StartWatching(string Path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher(Path);
            watcher.NotifyFilter = NotifyFilters.FileName;
            //watcher.Filter = "*.xls";

            // Add event handlers
            watcher.Created += new FileSystemEventHandler(OnChanged);

            // Begin watching
            watcher.EnableRaisingEvents = true;

            return watcher;
        }

        public void StartWatching(Upload upload)
        {
            // only need to watch if the datasource is a spreadsheet
            if (upload.DataSourceType == DataSourceType.Spreadsheet)
            {
                // try and find an existing watcher
                FileWatchers result = _watcherList.Find(
                    delegate(FileWatchers fw)
                    {
                        return fw.Upload == upload;
                    }
                );

                if (result == null)
                {
                    // start new
                    FileWatchers fw = new FileWatchers();
                    fw.FileWatcher = StartWatching(upload.DataSource);
                    fw.Upload = upload;

                    _watcherList.Add(fw);
                }
                else
                {
                    // restart existing
                    result.Upload = upload;
                    result.FileWatcher.Dispose(); // releases any held resources from previous source
                    result.FileWatcher.Path = upload.DataSource; // update if user has made changes
                    result.FileWatcher.EnableRaisingEvents = true;
                }
            }
        }

        public void StopWatching(Upload upload)
        {
            FileWatchers result = _watcherList.Find(
                delegate(FileWatchers fw)
                {
                    return fw.Upload == upload;
                }
            );

            // stop the watcher and release any resources
            if (result != null)
            {
                //result.FileWatcher.EnableRaisingEvents = false;
                result.FileWatcher.Dispose(); // dispose of any resources held by watcher
            }
            else
                throw new Exception("Could not find the watcher for this upload.");
        }

        /// <summary>
        /// Stops all file watchers and force them to release an held resources by the watcher.
        /// </summary>
        public void StopAllWatching()
        {
            foreach (FileWatchers fw in _watcherList)
            {
                fw.FileWatcher.EnableRaisingEvents = false;
                fw.FileWatcher.Dispose();
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            string path = e.FullPath;

            string fileName = System.IO.Path.GetFileName(path);

            // ignore any files that contain '~' at the front of the file name
            // this is a temp file that is created when the file is opened by the 
            // ms office interop methods to generate an csv file from the xls.
            if (fileName[0] != '~')
            {
                string extension = System.IO.Path.GetExtension(path);

                // check the file extension for a valid spreadsheet format
                if (extension == ".xls" || extension == ".xlsm" || extension == ".xlsx")
                {
                    FileSystemWatcher watcher = (FileSystemWatcher)source;
                    //watcher.Dispose();

                    // get the upload for this watcher
                    FileWatchers result = _watcherList.Find(
                        delegate(FileWatchers fw)
                        {
                            return fw.FileWatcher == source;
                        }
                    );

                    if (result != null)
                    {
                        // starts the app and begins the spreadsheet upload process upon startup
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        // assumes the variant program is called "VariantExporter.exe"
                        process.StartInfo.FileName = Application.StartupPath + "\\VariantExporter.exe";
                        process.StartInfo.EnvironmentVariables.Add(result.Upload.ID.ToString(), null);
                        process.StartInfo.EnvironmentVariables.Add(path, null);
                        process.Start();
                    }
                    else
                        throw new Exception("Could not find the watcher for this upload.");
                }
            }
        }
    }

    public class FileWatchers
    {
        public FileSystemWatcher FileWatcher { get; set; }
        public Upload Upload { get; set; }
    }
}
