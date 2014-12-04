using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Linq;

using NHibernate;
using DBConnLib;
using AuditLogDB;
using SiteConf;
using ExporterCommon;

namespace VariantExporterWinGUI.Util
{
    public class FileWatcher
    {
        private FrmMain _frmMain;
        List<FileWatchers> _watcherList;
        
        public FileWatcher(FrmMain frmMain)
        {
            _frmMain = frmMain;
            
            // list to store all fileWatchers for each upload
            _watcherList = new List<FileWatchers>();

            List<SiteConf.Upload.Object> uploadList = null;

            // checked if orgSite is an Admin
            if (_frmMain._orgSite.HVPAdmin.HasValue)
            {
                if (_frmMain._orgSite.HVPAdmin.Value)
                {
                    // get all the orgsites associated with the admin site
                    List<SiteConf.OrgSite.Object> sites = DataLoader.GetAdminOrgSites(_frmMain._orgSite.ID);

                    // concatenated list of uploads
                    uploadList = new List<SiteConf.Upload.Object>();

                    // get all the uploads for each site
                    foreach (SiteConf.OrgSite.Object site in sites)
                    {
                        List<SiteConf.Upload.Object> uploads = DataLoader.GetUploadList(site.OrgHashCode);

                        uploadList = uploadList.Concat(uploads).ToList();
                    }
                }
            }
            
            // if orgSite is not admin then just get the uploads using the hashcode from it.
            if (uploadList == null)
                uploadList = DataLoader.GetSpreadSheetUploadList(_frmMain._orgSite.OrgHashCode);
            

            foreach (SiteConf.Upload.Object upload in uploadList)
            {
                if (upload.DataSourceName == null || upload.DataSourceName == string.Empty)
                {
                    continue;
                }
                else
                {
                    try
                    {
                        FileWatchers fw = new FileWatchers();
                        fw.FileWatcher = StartWatching(upload.DataSourceName);
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

        public void StartWatching(SiteConf.Upload.Object upload)
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
                    fw.FileWatcher = StartWatching(upload.DataSourceName);
                    fw.Upload = upload;

                    _watcherList.Add(fw);
                }
                else
                {
                    // restart existing
                    result.Upload = upload;
                    result.FileWatcher.Dispose(); // releases any held resources from previous source
                    result.FileWatcher.Path = upload.DataSourceName; // update if user has made changes
                    result.FileWatcher.EnableRaisingEvents = true;
                }
            }
        }

        public void StopWatching(SiteConf.Upload.Object upload)
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

                // check the file extension for a valid spreadsheet and csv format
                //if (extension == ".xls" || extension == ".xlsm" || extension == ".xlsx" 
                //    || extension == ".csv" || extension == ".xltm")
                
                FileSystemWatcher watcher = (FileSystemWatcher)source;
                
                // get the upload for this watcher
                FileWatchers result = _watcherList.Find(
                    delegate(FileWatchers fw)
                    {
                        return fw.FileWatcher == source;
                    }
                );

                if (result != null)
                {
                    _frmMain.LoadSpreadsheetData(result.Upload, path);
                    _frmMain.SetSpreadsheetPath(path);
                }
                else
                    throw new Exception("Could not find the watcher for this upload.");
            }
        }
    }

    public class FileWatchers
    {
        public FileSystemWatcher FileWatcher { get; set; }
        public SiteConf.Upload.Object Upload { get; set; }
    }
}
