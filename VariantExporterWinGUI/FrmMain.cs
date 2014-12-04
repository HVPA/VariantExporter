using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Scripting.Utils;
using System.Deployment.Application;

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
using ExporterCommon.Plugins;
using ExporterCommon.Output;
using ExporterCommon.Core;
using ExporterCommon.Core.StandardColumns;
using AutoUpdaterDotNET;

namespace VariantExporterWinGUI
{
    public partial class FrmMain : Form
    {
        public int _uploadID = 0;
        private DefaultExtractorContext context;
        private IExtractor plugin;
        private IWebProxy _proxy; //web proxy settings to connect to web if needed
        private bool SelectAll = false; // used for switching select/de-select all rows in dgv
        private ExporterCommon.Conf.Configuration conf;
        public string _OrgHashCode; // org hash code - is loaded from the SiteConf REST based from the upload selected by user
        public DataTable invalidResults = new DataTable();
        private string _spreadsheetPath = "";
        private FileWatcher _watcher;
        private FormWindowState m_previousWindowState;
        private Log log;
        private bool ClearCache = true; //clear "temp" and "raw" directory after data is sent. By default its true, turn off for debugging
        public bool _CancelledAuth = false; //if user closed the proxy auth dialogue box
        public string _Version;
        public SQLiteDBFactory _slFactory;
        public SiteConf.OrgSite.Object _orgSite;
        public bool _release; // set the release type of application, it will determine where this app to look at and set local/user data.
        public string _commonAppPath;
        private bool _quickClose = false; // closes app without prompting user

        private SplashScreen ss;
        
        // background workers
        private BackgroundWorker bw_readingData;
        private BackgroundWorker bw_validatingData;

        private DataTable dtReadResult;
        private DataTable dtValidResults;

        public void init_backgroundWorkers()
        {
            bw_readingData = new BackgroundWorker();

            bw_readingData.DoWork += new DoWorkEventHandler(bw_readingData_DoWork);
            bw_readingData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_readingData_RunWorkerCompleted);

            bw_validatingData = new BackgroundWorker();

            bw_validatingData.DoWork += new DoWorkEventHandler(bw_validatingData_DoWork);
            bw_validatingData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_validatingData_RunWorkerCompleted);
        }

        public FrmMain()
        {
            InitializeComponent();
            
            // ******* testing - backgroundworkers
            init_backgroundWorkers();
            // ******* testing - backgroundworkers

            // Display loading splash screen while app init all vars
            //SplashScreen ss = new SplashScreen();
            ss = new SplashScreen();
            ss.Show();
            
            m_previousWindowState = (this.WindowState == FormWindowState.Minimized ? FormWindowState.Normal : this.WindowState);

            context = new DefaultExtractorContext();
            
            // get conf
            conf = context.GetConfiguration();

            // init logger
            log = new Log(bool.Parse(conf.VerboseLogEnable), txtStatusLog);
            
            // get webproxy and set webproxy
            ReloadProxy();

            // get orgSite
            bool RESTError = false;
            bool retry = true;

            // A loop that checks connection to the web, if unable to connect it allows user to jump into proxy
            // settings and retry connecting until either a connection can be established or user kills it.
            while (retry)
            {
                try
                {
                    // try to get orgsite via REST calls if this fails then close app.
                    _orgSite = DataLoader.GetOrgSite("OrgHashCode", conf.OrganisationHashCode);
                    retry = false;
                    RESTError = false;
                }
                catch (Exception ex)
                {
                    // close the splash screen
                    ss.Close();

                    RESTError = true;
                    // write debug message to log
                    log.write(ex.ToString());

                    // rename buttons to messagebox
                    MessageBoxManager.Yes = "Retry";
                    MessageBoxManager.No = "Settings";
                    MessageBoxManager.Cancel = "Quit";
                    // registers the buttons renamed
                    MessageBoxManager.Register();

                    DialogResult result = MessageBox.Show("Could not connect to HVP servers, there maybe issues with your current connection or the server maybe down please try again. \nIf you are connected to the internet via proxies please click on settings to configure your web proxies.",
                        "Error! Could not connect to HVP servers", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                    
                    // unregisters the naming back to default
                    MessageBoxManager.Unregister();

                    // no == settings
                    if (result == DialogResult.No)
                    {
                        // open settings frm
                        FrmConf frmConf = new FrmConf(conf, context, this, true);
                        frmConf.ShowDialog();
                    }
                    
                    // cancel == cancel
                    if (result == DialogResult.Cancel)
                    {
                        // kill loop
                        retry = false;
                        // exit app
                        System.Environment.Exit(0);
                    }
                }
            }

            // close loading splash screen
            ss.Close();

            // if no error from REST
            if (RESTError == false)
            {
                // nibhernate factory for sqlite db connection
                _slFactory = new SQLiteDBFactory();
            
                // Clear Cache
                ClearCache = bool.Parse(conf.ClearCache);

                // start the file watcher if there are any uploads that require it.
                _watcher = new Util.FileWatcher(this);
                
                // check if there is an update when program loads up
                AutoUpdater.Start(conf.AutoUpdateLink);

                // set the timer for auto update in milliseconds
                CheckForUpdatesTimedEvent(3600000); // every 1 hour

                // get the commonAppPath, if release version then should be under user data
                _commonAppPath = CommonAppPath.GetCommonAppPath();

                // start with the modal upload form on screen first
                FrmUpload frmUpload = new FrmUpload(this, conf.OrganisationHashCode);
                frmUpload.Focus();
                frmUpload.ShowDialog();
            }

            // get the build version from the assembly
            _Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // display the version number in the form window text
            // remove the last 2 chars from version since we dont display that to the user
            this.Text = "HVP - Variant Exporter (" + _Version.Remove(_Version.Length - 2) + ")";

            // display notes on icon hover
            notifyIcon1.Icon = this.Icon;
        }

        public void ReloadProxy()
        {
            _proxy = SetProxy(conf.ServerAddress);
            DataLoader.SetProxy(_proxy);
            DataSaver.SetProxy(_proxy);
        }

        /// <summary>
        /// Advise user before closing app that closing app will stop file watcher.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_quickClose)
            {
                DialogResult frmclose = MessageBox.Show("Are you sure you want to exit the app? The app will no longer monitor any spreadsheets you may want to upload. You can minimise this app to the system tray by clicking and it will still continue to monitor for uploads.", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (frmclose != DialogResult.Yes)
                    e.Cancel = true;
                else
                    _watcher.StopAllWatching();
            }
            // we stop all file watchers so they release any held resources they may hold
            _watcher.StopAllWatching();
        }

        /// <summary>
        /// Check if Private and Public keys exist in the app Keys directory.
        /// </summary>
        /// <param name="conf"></param>
        private bool CheckKeys(ExporterCommon.Conf.Configuration conf)
        {
            string path = _commonAppPath + "\\Keys\\" + conf.PrivateKey;
            log.write("Checking for private keys in: " + path);
            
            if (System.IO.File.Exists(path))
                return true;
            else
                return false;
        }

        private void TrayMinimizerForm_Resize(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "Exporter Minimised";
            notifyIcon1.BalloonTipText = "Double click on this icon to return to the Exporter";

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _quickClose = true;
            this.Close();
        }

        public void SetTitleLabel(string titleLabel)
        {
            lblTitle.Text = titleLabel;
        }

        /// <summary>
        /// Loads an upload to main form by filling in all the details in the form based
        /// on the UploadID of that upload.
        /// </summary>
        /// <param name="uploadID"></param>
        public void LoadUpload(int uploadID)
        {
            // set the uploadID 
            _uploadID = uploadID;
            
            SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(uploadID);

            displayUploadDetails(upload);

            ReloadDgGene(upload);

            plugin = PluginLoader.LoadExtractor(upload.Plugin);
            
            // hide the save message
            lblSavedMessage.Visible = false;

            // clear the datagrid in the send data tab
            dgVariant.DataSource = null;

            RefreshTabs();

            // display message if private key not found on startup
            if (!CheckKeys(conf))
            {
                MessageBox.Show("The system can not locate the Private Key, a connection can not be established without the private key. You can add a private key from the Tools menu and selecting Import Private Key. If you don't have a private key please contact someone from HVP.", 
                    "No private key set", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Sets the datasource radiobuttons and loads the path in the textbox
        /// </summary>
        /// <param name="upload"></param>
        private void displayUploadDetails(SiteConf.Upload.Object upload)
        {
            txtUpload.Text = upload.Name;
            // selects the datasourcetype radiobutton
            switch (upload.DataSourceType)
            {
                case DataSourceType.Database:
                    rbnDatabase.Checked = true;
                    rbnSpreadsheet.Checked = false;
                    rbnSqlServer.Checked = false;
                    pnlFileSource.Visible = true;
                    pnlServer.Visible = false;
                    testDBServerConnectivityToolStripMenuItem.Enabled = false;
                    break;
                case DataSourceType.Spreadsheet:
                    rbnDatabase.Checked = false;
                    rbnSpreadsheet.Checked = true;
                    rbnSqlServer.Checked = false;
                    pnlFileSource.Visible = true;
                    pnlServer.Visible = false;
                    testDBServerConnectivityToolStripMenuItem.Enabled = false;
                    break;
                case DataSourceType.Server:
                    rbnDatabase.Checked = false;
                    rbnSpreadsheet.Checked = false;
                    rbnSqlServer.Checked = true;
                    pnlFileSource.Visible = false;
                    pnlServer.Visible = true;
                    testDBServerConnectivityToolStripMenuItem.Enabled = true;
                    break;
            }

            if (upload.DataSourceType == DataSourceType.Server)
            {
                txtDatasourceServer.Text = upload.DataSourceName;
                txtDatabase.Text = upload.DatabaseName;
                txtDsUsername.Text = upload.UserName;
                txtDsPassword.Text = upload.Password;
            }
            else
            {
                txtDataSourcePath.Text = upload.DataSourceName;
            }

            // displays the Upload in the title
            SetTitleLabel(upload.Name);
        }

        private delegate void SetSpreadsheetPathDelegate(string path);

        public void SetSpreadsheetPath(string path)
        {
            if (InvokeRequired)
            {
                Invoke(new SetSpreadsheetPathDelegate(SetSpreadsheetPath), path);
            }
            else
            {
                _spreadsheetPath = path;
            }
        }

        private void RefreshTabs()
        {
            // if uploadID == -1 then user is creating a new Upload
            if (_uploadID == -1)
            {
                // disable the upload tab until user has setup the datasource page first
                if (tabControl1.TabPages.Contains(tabDataUpload))
                    tabControl1.TabPages.Remove(tabDataUpload);
            }

            else
            {
                if (!tabControl1.TabPages.Contains(tabDataUpload))
                    tabControl1.TabPages.Add(tabDataUpload);
            }
        }

        /// <summary>
        /// This will set all columns in the datagrid to be readonly except the Select
        /// column. Should only call this after the datagrid has been binded with data
        /// as they are no specified columns before data is binded.
        /// </summary>
        private void SetReadOnlyColumns()
        {
            foreach (DataGridViewColumn col in dgVariant.Columns)
            {
                if (col.HeaderText != "Select")
                    col.ReadOnly = true;
            }
        }

        private void getHashTableRefSeq(Hashtable refSeqNameHT, Hashtable refSeqVerHT, SiteConf.Upload.Object upload)
        {
            IList<SiteConf.Gene.Object> geneList = ExporterCommon.DataLoader.GetGeneList(upload.ID);

            foreach (SiteConf.Gene.Object gene in geneList)
            {
                refSeqNameHT[gene.GeneName] = gene.RefSeqName;
                refSeqVerHT[gene.GeneName] = gene.RefSeqVersion;
            }
        }

        /// <summary>
        /// Process data from datagrid which includes encrypting and zipping data and
        /// sending data to the specified destination
        /// </summary>
        /// <param name="serverAddress">Destination server data is sent to</param>
        /// <param name="audit">Log data sent in the internal audit database</param>
        private void ProcessAndSendData(string serverAddress, bool audit, bool isTest)
        {
            bool DataSuccessfullySent = false;

            int sentVariants = 0;

            Cursor.Current = Cursors.WaitCursor;
            SplashScreen ss = new SplashScreen();
            ss.Show();
            DisableAllControls();

            // if data is read from a data source file check if file is in use by another
            // process, otherwise file can not be moved to the "sent" subdirectory.
            if (_spreadsheetPath != string.Empty)
            {
                try
                {
                    using (System.IO.Stream stream = new System.IO.FileStream(_spreadsheetPath, System.IO.FileMode.Open))
                    {
                        // Nothing to see here. Just testing to see if file is not opened or in use by another process.
                    }
                }
                catch (Exception ex)
                {
                    ss.Close();
                    EnableAllControls();
                    
                    // alert user file is being used by another process
                    MessageBox.Show("The current file: " + _spreadsheetPath +
                                            " is being used by another process. " +
                                            "\n" +
                                            "\nIf you have the file open in an application, please close the application before continuing.",
                                            "File in use:  " + _spreadsheetPath,
                                            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    // log error exception
                    log.write("The file: " + _spreadsheetPath + " is being used by another process.");
                    log.write(ex.ToString());
                    log.write("No data was sent!");

                    // end this function
                    return;
                }
            }

            if (dgVariant.Rows.Count != 0)
            {
                DataTable original = (DataTable)dgVariant.DataSource;
                DataTable yesOnly = original.Clone();

                SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(_uploadID);
                // get all the genes with the disease tags
                IList<SiteConf.Gene.Object> geneList = ExporterCommon.DataLoader.GetGeneList(upload.ID);

                // sorting data to send
                log.write("======= Begin Data Transfer =======");
                log.write("Sorting data for sending....");
                foreach (DataGridViewRow row in dgVariant.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        yesOnly.ImportRow(((DataRowView)row.DataBoundItem).Row);
                    }
                }

                sentVariants = yesOnly.Rows.Count;

                if (yesOnly.Rows.Count > 0)
                {
                    // timestamp for use in the xml and zip file names
                    DateTime dt = DateTime.Now;
                    string timestamp = String.Format("{0:yyyyMMddHHmmss}", dt);

                    XmlOutput output = new XmlOutput();

                    string xmlFileName = timestamp + ".xml";

                    CommonAppPath.CreateDirectory("raw");
                    string xmlFilePath = _commonAppPath + "\\raw\\" + xmlFileName;

                    // hash table to store the refseq names and versions
                    Hashtable refSeqNameHT = new Hashtable();
                    Hashtable refSeqVerHT = new Hashtable();

                    // hash tables to store patient grhanite hashes
                    getHashTableRefSeq(refSeqNameHT, refSeqVerHT, upload);

                    // generate xml file
                    log.write("Writing to xml...");
                    output.Generate(conf.PortalWebSite, xmlFilePath, yesOnly, refSeqNameHT,
                        refSeqVerHT, _OrgHashCode, geneList);

                    // send xml to server
                    try
                    {
                        string encryptedFile = timestamp + ".txt";

                        CommonAppPath.CreateDirectory("temp");
                        string encryptedFilePath = _commonAppPath + "\\temp\\" + encryptedFile;

                        // start the encryption executable
                        log.write("Encrypting data....");

                        // location of HVP_Encryption executable
                        string appPath = System.IO.Path.GetDirectoryName(
                            System.Reflection.Assembly.GetEntryAssembly().Location)
                            + "\\Executables\\HVP_Encryption.exe";

                        // Encrypt file
                        Encryption.HVP_EncryptFile(appPath, xmlFilePath, encryptedFilePath, _commonAppPath + "\\keys\\",
                            conf.PrivateKey, conf.PublicKey, log);

                        // zip file name is combination of org hash code and date
                        string zippedFileName = _orgSite.OrgHashCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        string zippedFilePath = _commonAppPath + "\\temp\\" + zippedFileName;

                        // zip files
                        Decompression.ZippUpTransferFiles(zippedFileName, encryptedFilePath, _commonAppPath + "\\temp\\", conf.PrivateKey.Replace(".private", ""), log);

                        // send the zip file to destination server
                        log.write("Sending data to: " + serverAddress);

                        // webclient for uploading data
                        WebClient client = new WebClient();
                        // set proxy to webclient if any
                        client.Proxy = _proxy;

                        // send zipped file
                        string sent = Transmit.SendData(zippedFilePath + ".zip", serverAddress, client, log);

                        if (_CancelledAuth)
                        {
                            log.write("User cancelled proxy auth login");
                            return;
                        }

                        // sent ok we should get a post
                        if (sent == "<HTML>POST OK.</HTML>")
                        {
                            log.write("Data successfully sent!!! " + sentVariants + " variants sent.");

                            // only log data sent to db if sending to live server and not the test server
                            if (audit)
                            {
                                // save the transaction of each instance to the auditdb
                                log.write("Saving transaction to db...");
                                SaveAuditTransaction(yesOnly, xmlFilePath);
                            }

                            // clear the Invalid results table and disable the invalid data button
                            invalidResults = null;
                            btnErrors.Enabled = false;

                            // rename the sent xls and move to folder called "sent", creates folder if not exist
                            if (rbnSpreadsheet.Checked == true && isTest != true)
                            {
                                if (!System.IO.File.Exists(upload.DataSourceName + "\\sent"))
                                {
                                    // create new directory called sent in the source location of xls
                                    System.IO.Directory.CreateDirectory(upload.DataSourceName + "\\sent");
                                }

                                // get the source file extension
                                string sourceFileExt = System.IO.Path.GetExtension(_spreadsheetPath);

                                // set the name of file to the datee/time sent
                                string renamedFile = "(" + timestamp + ")" + sourceFileExt;

                                // move spreadsheet to directory called sent
                                bool fileMoved = false;
                                // loop to check if file is in use by another process, will continue to loop
                                // until file can be moved to the sent folder or user acknowledges it can't 
                                // be moved by the program and user should move it themselves manually to 
                                // avoid sending data from the file again.
                                while (!fileMoved)
                                {
                                    try
                                    {
                                        System.IO.File.Move(_spreadsheetPath, upload.DataSourceName +
                                            "\\sent\\" + System.IO.Path.GetFileName(_spreadsheetPath).Replace(sourceFileExt, "") + renamedFile);
                                        fileMoved = true;

                                    }
                                    catch (System.IO.IOException ioEx)
                                    {
                                        // if unable to rename/move file
                                        var result = MessageBox.Show("Unable to access file: " + _spreadsheetPath +
                                            "\n" +
                                            "\nThe exporter has successfully uploaded the data, however it is unable to move the file from datasource folder to the sent sub folder." +
                                            "\n" +
                                            "\nIf you have the file open in an application, please close the application and click Retry." +
                                            "\n" +
                                            "\nOr click Cancel and the file will remain in your datasource folder, you will need to manually move that file out of the datasource folder otherwise you may end up resending the data again.",
                                            "Can not access file:  " + _spreadsheetPath,
                                            MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk);

                                        if (result == DialogResult.Cancel)
                                        {
                                            // if cancel exit loop with error
                                            fileMoved = true;

                                            // log error
                                            log.write("Unable to access the file: " + _spreadsheetPath);
                                            log.write(ioEx.ToString());
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ss.Close();
                                        EnableAllControls();

                                        // log error
                                        log.write("The data has been successfully uploaded however the Variant Exporter has encountered an error.");
                                        log.write(ex.ToString());

                                        // set to true to exit out of the loop
                                        fileMoved = true;
                                    }
                                }
                            }

                            // Data has been successfully sent
                            DataSuccessfullySent = true;

                            // clear the datagrid
                            dgVariant.DataSource = null;

                            // clear the spreadsheet path now that it has been sent
                            // keep the path if it was only a test send
                            if (!isTest)
                                _spreadsheetPath = "";
                        }
                        else
                        {
                            ss.Close();
                            EnableAllControls();
                            log.write("Error connecting to server!!!!");
                            MessageBox.Show("Error connecting to server, please try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                        }
                    }
                    catch (WebException ex)
                    {
                        ss.Close();
                        EnableAllControls();
                        log.write("Unexpected error!!!");
                        log.write(ex.ToString());
                        MessageBox.Show("Error connecting to server, please try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                        ss.Close();
                        EnableAllControls();

                        if (ClearCache)
                        {
                            // always clear the contents in the temp
                            log.write("Clearing contents from temp folder....");
                            System.IO.DirectoryInfo temp = new System.IO.DirectoryInfo(_commonAppPath + "\\temp");
                            foreach (System.IO.FileInfo file in temp.GetFiles())
                            {
                                file.Delete();
                            }

                            // remove the raw xml files
                            log.write("Clearing the contents from raw folder....");
                            System.IO.DirectoryInfo raw = new System.IO.DirectoryInfo(_commonAppPath + " \\raw");
                            foreach (System.IO.FileInfo file in raw.GetFiles())
                            {
                                file.Delete();
                            }
                        }

                        // clear the filename from title
                        lblTitle.Text = upload.Name;
                    }
                }
                else
                {
                    ss.Close();
                    EnableAllControls();
                    MessageBox.Show("You have not selected any rows!!!", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ss.Close();
                EnableAllControls();
                MessageBox.Show("There is nothing to send", "No Data selected");
            }

            log.write("======= Data Transfer Complete =======");

            if (DataSuccessfullySent)
                MessageBox.Show("Data Successfully sent. " + sentVariants + " variants sent.", "Data sent", MessageBoxButtons.OK);

        }
        
        private void sendDataToTestServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckKeys(conf))
                ProcessAndSendData(conf.TestServerAddress, false, true);
            else
                MessageBox.Show("The system can not locate the Private Key, a connection can not be established without the private key. You can add a private key from the Tools menu and selecting Import Private Key. If you don't have a private key please contact someone from HVP.",
                    "No private key set", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (CheckKeys(conf))
                ProcessAndSendData(conf.ServerAddress, true, false);
            else
                MessageBox.Show("The system can not locate the Private Key, a connection can not be established without the private key. You can add a private key from the Tools menu and selecting Import Private Key. If you don't have a private key please contact someone from HVP.", 
                    "No private key set", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void SaveAuditTransaction(DataTable dt, string xmlFileName)
        {
            // assuming the file location of the xml is in the same directory of app
            System.IO.FileInfo fi = new System.IO.FileInfo(xmlFileName);

            string fileSize = fi.Length.ToString();

            // create new transaction record
            SiteConf.HVPTran.Object trans = new SiteConf.HVPTran.Object();
            trans.Date = DateTime.Now;
                
            // optional meta fields for users to comment on. NB: This info will not be uploaded to HVP
            trans.Who = txtWho.Text; // initials or name of person who conducted upload
            // log and location have not yet been implemented into the app interface
            trans.Log = ""; // Internal log message for clinicians to enter a descriptive blurb about the upload
            trans.Location = ""; // lab or department data is sent from
            trans.Byte = fileSize;
            trans.orgsite = @"/api/v1/hvptran/" + _orgSite.ID + "/";
            ExporterCommon.DataSaver.SaveNewRestObject(trans);

            try
            {
                // save instance and details to this trans
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    using (ISession session = _slFactory.OpenSession())
                    {
                        ExporterCommon.DataSaver.SaveInstanceAudit(session, _uploadID, dr, trans);
                    }
                }
            }
            catch (Exception ex)
            {
                log.write(ex.ToString());
                throw ex;
            }
        }

        private void btnLoadDataSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = null;
            FolderBrowserDialog fbd = null;

            if (rbnDatabase.Checked)
            {
                // for file based datasources (etc: access .mdb, .db)
                ofd = new OpenFileDialog();
            }
            else
            {
                // folder for excel spreadsheets
                fbd = new FolderBrowserDialog();
            }

            // set the path if database
            if (ofd != null)
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    txtDataSourcePath.Text = ofd.FileName;
            }
            // set the pat if spreadsheet
            if (fbd != null)
            {
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    txtDataSourcePath.Text = fbd.SelectedPath;
            }
        }

        private void btnErrors_Click(object sender, EventArgs e)
        {
            ShowErrorPage();
        }

        private void ShowErrorPage()
        {
            FrmErrorLog errorLog = new FrmErrorLog(this, invalidResults);
            errorLog.Show();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // if the the datasource is null then there has not been a new spreadsheet
            // detected by the FileWatcher.
            string dataSourcePath = GetDataSourcePath();

            if (dataSourcePath != "")
            {
                // load the datagrid
                log.write("======= Begin Data Reading =======");
                log.write("Reading data for exporting....");
                LoadDataGrid(dataSourcePath);
            }
            else
                MessageBox.Show("No Datasource path set or there are no files for upload detected, you may have already previously sent a file. If you wish to re-send it please copy that file into the datasource directory path again, alternatively you can find the previously sent file under the sent directory", "No Data Detected");
        }

        private void bw_readingData_DoWork(object sender, DoWorkEventArgs e)
        {
            ss.Show();

            string path = (string)e.Argument;

            SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(_uploadID);
            
            // append the spreadsheet path to upload.DataSource if datasourceType is spreadsheet
            if (upload.DataSourceType == DataSourceType.Spreadsheet)
            {
                // add the file name to upload path only for spreadsheets
                upload.DataSourceName = path;

                // set spreadsheet path
                _spreadsheetPath = path;
            }

            dtReadResult = plugin.GetData(context, upload);
        }
        
        private void bw_readingData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(_uploadID);

            // add an extra column to store errormesages in the invalidResult datatable
            DataColumn colType = dtReadResult.Columns.Add("InvalidReason");
            colType.SetOrdinal(0);

            log.write("Finish reading data!!!");

            // datatable placeholder for valid results
            dtValidResults = dtReadResult.Clone();
            // datatable placeholder for invalid results
            invalidResults = dtReadResult.Clone();

            // Validations - in this order:
            // - Divide valid and invalid rows into separate tables
            // - Check if recorded in auditDB before
            // - Diff check
            // - Basic field check
            // - HGVS stuff

            // divide valid and invalid results into separate tables
            log.write("Validating data....");

            bw_validatingData.RunWorkerAsync(dtValidResults);
        }

        private void bw_validatingData_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable validResults = (DataTable)e.Argument;
            
            SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(_uploadID);
            
            ValidateAndExtractData(dtReadResult, validResults, invalidResults, upload.RefMapper);
        }

        private void bw_validatingData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(_uploadID);

            try
            {
                // There is no need for diff checks when uploading from xls file as each
                // spreadsheet data is treated as new all the time. Updates and deletes do 
                // not apply.
                if (rbnDatabase.Checked)
                {
                    using (ISession session = _slFactory.OpenSession())
                    {
                        // diff check using auditdb
                        DiffCheck(session, dtValidResults);

                        // remove any rows that have been sent
                        RemoveDeletedRows(session, dtReadResult, dtValidResults);
                    }
                }
                else
                {
                    // sets all the status to new for spreadsheets
                    AllNew(dtValidResults);
                }

                // valid rows show up on the datagrid
                dgVariant.DataSource = dtValidResults;
                dgVariant.Columns["Status"].Visible = false;
                dgVariant.Columns["DateSubmitted"].Visible = false;

                // disables editing to all the other columns but the select column
                SetReadOnlyColumns();

                // if there are invalid rows then we enable the error button
                if (invalidResults.Rows.Count > 0)
                    btnErrors.Enabled = true;
                else
                    btnErrors.Enabled = false;

                // by default we select/check all the rows
                for (int i = 0; i < dgVariant.Rows.Count; i++)
                {
                    dgVariant.Rows[i].Cells[0].Value = true;
                }

                if (dtValidResults.Rows.Count == 0)
                {
                    log.write("No data loaded from the datasource, possible errors or datasource is empty");
                }

                log.write("Finished validating data!!!");
                log.write("======= Data Reading Complete =======");

                ss.Close();
            }
            catch (Exception ex)
            {
                ss.Close();
                MessageBox.Show("An error occured while reading from the datasource. Please check that the datasource is the correct file you are reading from.", "Error reading from datasource");
                log.write("Error reading from datasource: " + upload.DataSourceName + _spreadsheetPath);
                log.write(ex.ToString());
            }
            finally
            {
                // renable all the controls
                EnableAllControls();
            }
            
            // show invalid data window if invalid results exist
            if (invalidResults.Rows.Count > 0)
            {
                ShowErrorPage();
            }
        }

        /// <summary>
        /// Loads datagrid from datasource, before loading each result validation 
        /// and diff checking is applied.
        /// </summary>
        /// <param name="path"></param>
        public void LoadDataGrid(string path)
        {
            // sets the mouse cursor to the default waiting mode
            Cursor.Current = Cursors.WaitCursor;

            //SplashScreen ss = new SplashScreen();
            ss.Show();
            
            // disable controls
            DisableAllControls();

            // get the current upload
            SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(_uploadID);
            
            if (upload.DataSourceType == DataSourceType.Spreadsheet)
            {
                // set the filename to form label
                lblTitle.Text = upload.Name + " - File: " + System.IO.Path.GetFileName(path);
            }

            bw_readingData.RunWorkerAsync(path);
        }

        /// <summary>
        /// For spreadsheet data it is always assumed that the data is being sent
        /// is always new. This sets all the valid results status to New.
        /// </summary>
        /// <param name="validResultsDt"></param>
        private void AllNew(DataTable validResultsDt)
        {
            foreach (DataRow dr in validResultsDt.Rows)
            {
                dr[VariantInstance.Status] = "New";
            }
        }

        /// <summary>
        /// Saves all datarows in datatable to audit log db.
        /// Also checks for previous uploaded rows that have been saved. 
        /// When checks have been done, a status is added to instance to indicate,
        /// if it is new or updated. Anything that has been sent and has not 
        /// changed is ignored.
        /// </summary>
        /// <param name="dt"></param>
        private void DiffCheck(ISession iSession, DataTable validResultsDt)
        {
            // List of datarow of instances that has been recorded as been sent before
            List<DataRow> sentDrList = new List<DataRow>();

            for (int i = 0; i < validResultsDt.Rows.Count; i++)
            {
                DataRow dr = validResultsDt.Rows[i];

                Instance instance = ExporterCommon.DataLoader.GetInstance(iSession, 
                       dr["VariantHashCode"].ToString(),_uploadID);

                // if match found then this instance has been sent before. 
                if (instance != null)
                {
                    // A comparison of the checksum will determine if anything has changed
                    string checksum = HashEncoder.EncodeDataRow(dr);
                    
                    // get the previous transfered instance to compare checksum with
                    Details details = ExporterCommon.DataLoader.GetDetails(iSession, instance);

                    if (checksum != details.CheckSum)
                    {
                        dr[VariantInstance.Status] = "Update";
                    }
                    else
                    {
                        // nothing has changed we can ignore it, add it to list of rows to remove
                        sentDrList.Add(dr);
                    }
                }
                else
                {
                    // No record of this instance being sent before therefore it is new
                    dr[VariantInstance.Status] = "New";
                }
            }

            // cycle through the list of rows that have already been sent once 
            // remove those rows so they won't be sent again
            foreach (DataRow sentDr in sentDrList)
            {
                validResultsDt.Rows.Remove(sentDr);
            }
        }

        /// <summary>
        /// Cycles through the sent instances in the audit db and checks them 
        /// against the datasource provided, if instance from audit db is not
        /// found in the datasource it is assumed that record has been removed.
        /// The datarow is created with the status delete to indicate this record
        /// has been removed.
        /// </summary>
        /// <param name="iSession"></param>
        /// <param name="fullResultDt"></param>
        private void RemoveDeletedRows(ISession iSession, DataTable fullResultDt, DataTable validDt)
        {
            IList<Instance> instanceList = ExporterCommon.DataLoader.GetInstanceList(iSession);

            for (int i = 0; i < instanceList.Count; i++)
            {
                Instance instance = instanceList[i];

                Details details = ExporterCommon.DataLoader.GetDetails(iSession, instance);

                if (details.Status != "Delete")
                {
                    bool found = false;

                    foreach (DataRow dr in fullResultDt.Rows)
                    {
                        if (instance.EncryptedHashCode == dr[VariantInstance.HashCode].ToString())
                        {
                            found = true;
                            break;
                        }
                    }

                    // could not find this instance we need to create a new row in the datatable
                    // and mark the status to delete
                    if (!found)
                    {
                        DataRow removeDr = validDt.NewRow();

                        // get basic details for deletion 
                        removeDr[0] = instance.VariantInstanceID;
                        removeDr[ExporterCommon.Core.StandardColumns.Gene.GeneName] = instance.GeneName;
                        removeDr[VariantInstance.HashCode] = instance.EncryptedHashCode;
                        removeDr[Organisation.HashCode] = _OrgHashCode;
                        removeDr[VariantInstance.Status] = "Delete";

                        validDt.Rows.Add(removeDr);
                    }
                }
            }
        }

        private void ValidateDataRow(Validator validator, string refMapperPath, 
            DataTable validResults, DataTable invalidResults, DataRow dr)
        {
            bool valid = true;

            // standard generic validation
            if (!validator.ValidateDataRow(dr))
                valid = false;

            // site/lab specific validation
            if (!plugin.Validate(dr, refMapperPath))
                valid = false;

            // depending on valid result the datarow is imported to its equivalent table
            if (valid)
                validResults.ImportRow(dr);
            else
                invalidResults.ImportRow(dr);
        }

        /// <summary>
        /// Cycle through the results and copy valid and non valid rows to separate tables.
        /// </summary>
        /// <param name="results"></param>
        /// <param name="validResults"></param>
        /// <param name="invalidResults"></param>
        private void ValidateAndExtractData(DataTable results, DataTable validResults, 
            DataTable invalidResults, string refMapperPath)
        {
            Validator validator = new Validator();

            // split up the work load
            DataTable dtOdd = results.Clone();
            DataTable dtEven = results.Clone();
            for (int i = 0; i < results.Rows.Count; i++)
            {
                DataRow dr = results.Rows[i];
                if (i % 2 == 0)
                    dtEven.ImportRow(dr);
                else
                    dtOdd.ImportRow(dr);
            }

            // setup 2 worker delegates
            Microsoft.Scripting.Utils.Action oddWorker = delegate 
            {
                List<DataRow> dataRows = dtOdd.Select().ToList();
                for (int i = 0; i < dataRows.Count; i++)
                {
                    ValidateDataRow(validator, refMapperPath, validResults, invalidResults, dataRows[i]);
                }
            };

            DataTable ValidResults2 = results.Clone();
            DataTable InvalidResults2 = results.Clone();
            Microsoft.Scripting.Utils.Action evenWorker = delegate 
            {
                List<DataRow> dataRows = dtEven.Select().ToList();
                for (int i = 0; i < dataRows.Count; i++)
                {
                    ValidateDataRow(validator, refMapperPath, ValidResults2, InvalidResults2, dataRows[i]);
                }
            };

            // run 2 delegates asynchronously
            IAsyncResult evenHandler = evenWorker.BeginInvoke(null, null);
            IAsyncResult oddHandler = oddWorker.BeginInvoke(null, null);

            // wait for both to finish
            evenWorker.EndInvoke(evenHandler);
            oddWorker.EndInvoke(oddHandler);

            // merge the results
            validResults.Merge(ValidResults2);
            invalidResults.Merge(InvalidResults2);

            // remove the invalidReason column for the validResults table
            validResults.Columns.Remove("InvalidReason");
        }

        /// <summary>
        /// Selects/De-Selects all items in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            // get the default select all value and set it to the opposite
            // each time the this event is triggered the SelectAll value will 
            // be set to the opposite
            if (SelectAll == true)
                SelectAll = false;
            else
                SelectAll = true;

            for (int i = 0; i < dgVariant.Rows.Count; i++)
            {
                dgVariant.Rows[i].Cells[0].Value = SelectAll;
            }
        }

        private void changeUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // reset the spreadsheet path when changing upload
            _spreadsheetPath = string.Empty;
            
            FrmUpload frmUpload = new FrmUpload(this, conf.OrganisationHashCode);
            frmUpload.ShowDialog();
        }

        public void NewUploadForm()
        {
            // set the ID to -1 for new Upload
            _uploadID = -1;

            txtUpload.Text = string.Empty;
            rbnDatabase.Checked = false;
            rbnSpreadsheet.Checked = false;
            txtDataSourcePath.Text = string.Empty;

            // clear the gene datagrid
            dgGenes.DataSource = null;

            // remove the data upload tab
            RefreshTabs();

            // hide saved message
            lblSavedMessage.Visible = false;

            // clear the datagrid in the send data tab
            dgVariant.DataSource = null;

            // set the title
            SetTitleLabel("New Upload");

            // clear the spreadsheet path
            _spreadsheetPath = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (IsDataSourceDetailsValid())
            {
                SiteConf.Upload.Object upload;

                if (_uploadID == -1)
                {
                    // saves a new upload if ID is -1
                    upload = new SiteConf.Upload.Object();

                    upload.Name = txtUpload.Text;
                    if (rbnDatabase.Checked)
                        upload.DataSourceType = DataSourceType.Database;
                    if (rbnSpreadsheet.Checked)
                        upload.DataSourceType = DataSourceType.Spreadsheet;

                    if (rbnSqlServer.Checked)
                    {
                        upload.DataSourceType = DataSourceType.Server;
                        upload.DataSourceName = txtDatasourceServer.Text;
                        upload.DatabaseName = txtDatabase.Text;
                        upload.UserName = txtDsUsername.Text;
                        upload.Password = txtDsPassword.Text;
                    }
                    else
                        upload.DataSourceName = txtDataSourcePath.Text;

                    ExporterCommon.DataSaver.SaveNewRestObject(upload);

                    // enable the data upload tab if it is not enabled
                    tabControl1.TabPages.Add(tabDataUpload);

                    // set the saved UploadID
                    _uploadID = upload.ID;
                }
                else
                {
                    // updates the current upload based on ID
                    upload = ExporterCommon.DataLoader.GetUpload(_uploadID);

                    if (upload != null)
                    {
                        upload.Name = txtUpload.Text;
                        if (rbnDatabase.Checked)
                            upload.DataSourceType = DataSourceType.Database;
                        if (rbnSpreadsheet.Checked)
                            upload.DataSourceType = DataSourceType.Spreadsheet;

                        if (rbnSqlServer.Checked)
                        {
                            upload.DataSourceType = DataSourceType.Server;
                            upload.DataSourceName = txtDatasourceServer.Text;
                            upload.DatabaseName = txtDatabase.Text;
                            upload.UserName = txtDsUsername.Text;
                            upload.Password = txtDsPassword.Text;
                        }
                        else
                            upload.DataSourceName = txtDataSourcePath.Text;

                        ExporterCommon.DataSaver.UpdateRestObject(upload);
                    }
                    else
                    {
                        MessageBox.Show("An error occured while saving, please try again or restart the application.", "Save error");   
                        return;
                    }
                }

                // start the filewatcher
                _watcher.StartWatching(upload);

                // display save message
                lblSavedMessage.Visible = true;
            }
            else
                MessageBox.Show("Could not save upload, please check you have filled in all required fields correctly.", "Save error");

            Cursor.Current = Cursors.Default;
        }

        public void ReloadDgGene(SiteConf.Upload.Object upload)
        {
            dgGenes.DataSource = ExporterCommon.DataLoader.GetGeneList(upload.ID);
            // hide the first and last columns of the datagrid
            GetColumnID("ID", dgGenes).Visible = false;
            GetColumnID("DiseaseTags", dgGenes).Visible = false;
            GetColumnID("upload", dgGenes).Visible = false;
            GetColumnID("resource_uri", dgGenes).Visible = false;
            
            //dgGenes.Columns[0].Visible = false;
            //dgGenes.Columns[4].Visible = false;
            //dgGenes.Columns[5].Visible = false;
        }

        /// <summary>
        /// Get the column ID from the header text
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        private DataGridViewColumn GetColumnID(string header, DataGridView dg)
        {
            for (int i = 0; i < dg.Columns.Count; i++)
            {
                DataGridViewColumn column = dg.Columns[i];

                if (column.HeaderText == header)
                {
                    return column;
                }
            }
            // return a empty column to prevent crashing
            return new DataGridViewColumn();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmGene frmGene = new FrmGene(this, -1, _uploadID);
            frmGene.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // get the ID column index
            int indexID = GetColumnID("ID", dgGenes).Index;

            if (dgGenes.RowCount != 0)
            {
                int geneID = (int)dgGenes.SelectedRows[0].Cells[indexID].Value;

                FrmGene frmGene = new FrmGene(this, geneID, _uploadID);
                frmGene.ShowDialog();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int indexID = GetColumnID("ID", dgGenes).Index;
            int indexGeneName = GetColumnID("GeneName", dgGenes).Index;
            
            if (dgGenes.RowCount != 0)
            {
                int geneID = (int)dgGenes.SelectedRows[0].Cells[indexID].Value;
                string geneName = dgGenes.SelectedRows[0].Cells[indexGeneName].Value.ToString();

                DialogResult removeMsg = MessageBox.Show(
                        "Are you sure you want to delete " + geneName + "?",
                        "Delete " + geneName, MessageBoxButtons.YesNo);

                if (removeMsg == DialogResult.Yes)
                {
                    SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(_uploadID);

                    // get the gene
                    SiteConf.Gene.Object gene = ExporterCommon.DataLoader.GetGene(geneID);

                    // remove gene
                    ExporterCommon.DataSaver.DeleteRestObject(gene);

                    // refresh the datagrid
                    ReloadDgGene(upload);
                }
            }
        }

        private bool IsDataSourceDetailsValid()
        {
            if (txtUpload.Text == string.Empty)
                return false;
            
            // For Database and Spreadsheet
            if (pnlFileSource.Visible)
            {
                if (txtDataSourcePath.Text == string.Empty)
                    return false;

                if (rbnSpreadsheet.Checked)
                {
                    // check for other uploads using the same path 
                    IList<SiteConf.Upload.Object> uploadList = ExporterCommon.DataLoader.GetUploadList(_OrgHashCode);

                    for (int i = 0; i < uploadList.Count; i++)
                    {
                        SiteConf.Upload.Object upload = uploadList[i];

                        // skip the current upload 
                        if (upload.ID != _uploadID)
                        {
                            if (txtDataSourcePath.Text == upload.DataSourceName)
                            {
                                MessageBox.Show("The Folder path you selected already exist for another Upload, they can not share the same folder and must have separate folders for each.", "Folder Path error");
                                return false;
                            }
                        }
                    }
                }
            }
            
            // For SQL Server
            if (pnlServer.Visible)
            {
                if (txtDatasourceServer.Text == string.Empty)
                    return false;

                if (txtDatabase.Text == string.Empty)
                    return false;

                // Can Username and Password be left blank? Do all DB servers need U/P?
                if (txtDsUsername.Text == string.Empty)
                    return false;

                if (txtDsPassword.Text == string.Empty)
                    return false;
            }

            // if all checks passed
            return true;
        }

        private delegate void LoadSpreadSheetDataDelegate(SiteConf.Upload.Object upload, string path);

        /// <summary>
        /// This function is to be invoked from the FileWatcher
        /// </summary>
        public void LoadSpreadsheetData(SiteConf.Upload.Object upload, string path)
        {
            // required if calling this function from another thread
            if (InvokeRequired)
            {
                Invoke(new LoadSpreadSheetDataDelegate(LoadSpreadsheetData), upload, path);
            }
            else
            {
                try
                {
                    // load the tab first if not exist and 
                    if (!tabControl1.TabPages.Contains(tabDataUpload))
                        tabControl1.TabPages.Add(tabDataUpload);
                    // switch to the upload tab
                    tabControl1.SelectedTab = tabDataUpload;

                    this.Show();
                    this.WindowState = FormWindowState.Normal;

                    // Bring the form to the front
                    this.TopMost = true;
                    this.TopMost = false;

                    // load the upload details
                    LoadUpload(upload.ID);

                    // prepare the variant source for uploading
                    LoadDataGrid(path);
                    if (invalidResults.Rows.Count > 0)
                    {
                        ShowErrorPage();
                    }
                    
                    // set the path to the spreadsheet if user decides to do a reload
                    _spreadsheetPath = path;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Returns the correct datasource path depending on whether
        /// the datasourcetype is a spreadsheet or access database.
        /// </summary>
        /// <returns></returns>
        public string GetDataSourcePath()
        {
            string path = "";

            if (rbnSqlServer.Checked)
                path = txtDatasourceServer.Text;
            
            if (rbnDatabase.Checked)
                path = txtDataSourcePath.Text;

            if (rbnSpreadsheet.Checked)
            {   
                if (_spreadsheetPath == string.Empty)
                {
                    // check the spreadsheet datasource folder for spreadsheets
                    SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(_uploadID);
                    // if upload.DataSourceName is null then return empty string
                    if (upload.DataSourceName == null)
                        return "";

                    string[] files = System.IO.Directory.GetFiles(upload.DataSourceName);

                    // ingore the files that start with "~$" these are the ms temp files when file is open.
                    List<string> validFiles = new List<string>();
                    foreach (string file in files)
                    {
                        if (!file.Contains("~$"))
                        {
                            validFiles.Add(file);
                        }
                    }

                    if (validFiles.Count == 0)
                    {
                        path = "";
                    }
                    else
                    {
                        if (validFiles.Count > 1)
                        {
                            // if more than 1 file then we alert the user and process 1 file at a time
                            MessageBox.Show(@"There are multiple files detected in the data source directory. The Exporter can only process one file at a time, when you have finished uploading this file click the ""Load/Refresh Data"" button again to load the next file.", 
                                "Multiple files detected in source directory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        path = files[0];
                    }
                }
                else
                {
                    path = _spreadsheetPath;
                }
            }

            return path;
        }

        // test encryption key by sending a encrypted file to be decrypted by server
        // this will encrypt and zip a testfile then uploaded to the webserver where it will
        // unzip, decrypt file.
        private string TestServerEncryption(string serverAddress, string serverName)
        {
            //SplashScreen ss = new SplashScreen();
            //ss.Show();
            try
            {
                Cursor = Cursors.WaitCursor;
                
                log.write("======= Starting test connectivity =======");

                string filepath = Application.StartupPath + "\\testfile";
                CommonAppPath.CreateDirectory("temp");
                string encryptedFilePath = _commonAppPath + "\\temp\\" +
                    String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".txt";

                //EncryptFile(filepath, encryptedFilePath);
                // location of HVP_Encryption executable
                string appPath = System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetEntryAssembly().Location) 
                    + "\\Executables\\HVP_Encryption.exe";
                
                //Encrypt test file
                Encryption.HVP_EncryptFile(appPath, filepath, encryptedFilePath, _commonAppPath + "\\keys\\", 
                    conf.PrivateKey, conf.PublicKey, log);

                // Zip up encrypted test file
                string zippedFileName = _orgSite.OrgHashCode + "_TestEncryption";
                string zippedFilePath = _commonAppPath + "\\temp\\" + zippedFileName;
                
                // zip files
                Decompression.ZippUpTransferFiles(zippedFileName, encryptedFilePath, _commonAppPath + "\\temp\\", conf.PrivateKey.Replace(".private", ""), log);

                // send encrypted and zipped up test file
                log.write("Sending data to: " + serverAddress);

                // webclient for uploading data
                WebClient client = new WebClient();
                // set proxy to webclient if any
                //GetProxy(client, serverAddress);
                client.Proxy = _proxy;

                // send zipped file
                string sent = Transmit.SendData(zippedFilePath + ".zip", serverAddress, client, log);

                if (_CancelledAuth)
                {
                    log.write("User cancelled proxy auth login");
                    return "";
                }

                // check whether the decryption process has failed on the server end.
                switch (sent)
                {
                    case "<HTML>TEST POST SUCCESSFUL.</HTML>":
                        log.write("======= Test completed succesfully =======");
                        return "Test successful";
                    case "PROXY_AUTH_ERROR":
                        // no need to log any more info
                        return "";
                    //case "CANCELLED":
                        // stop hammer time!!!!
                    //    log.write("User cancelled proxy auth login");
                    //    return "";
                    default:
                        log.write("Encrypting/Decrypting has failed on the server");
                        return "Encrypting/Decrypting data to the " + serverName + " has failed! You can connect to the server, but the encryption keys used to encrypt and decrypt data has failed. Please contact HVP for more assistance";
                }
            }
            // connection errors
            catch (System.Net.WebException ex)
            {
                log.write("Connection failed");
                log.write(ex.ToString());
                
                return "Could not connect to " + serverName + ". The server may be down temporarily or there is an issue with your network connections.";
            }
            // all other errors
            catch (Exception ex)
            {
                log.write(ex.ToString());
                return ex.ToString();
            }
            finally
            {
                Cursor = Cursors.Default;
                //ss.Close();
                
                System.IO.DirectoryInfo temp = new System.IO.DirectoryInfo(_commonAppPath + "\\temp");
                foreach (System.IO.FileInfo file in temp.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        public IWebProxy GetProxy()
        {
            return _proxy;
        }

        private IWebProxy SetProxy(string serverAddress)
        {
            IWebProxy proxy;
            // if using custom proxy settings
            if (conf.ProxyAddress != string.Empty)
            {
                // if using an auto conf script
                //IWebProxy proxy;
                if (conf.ProxyPort != string.Empty)
                    proxy = new WebProxy(conf.ProxyAddress, int.Parse(conf.ProxyPort));
                else
                    proxy = new WebProxy(conf.ProxyAddress);
                proxy.Credentials = new NetworkCredential(conf.ProxyUser, conf.ProxyPassword, conf.ProxyUserDomain);
                //client.Proxy = proxy;
            }
            else
            {
                // if no custom proxy use default system proxy settings
                proxy = WebRequest.GetSystemWebProxy();
                //client.UseDefaultCredentials = true;
                //client.Credentials = CredentialCache.DefaultCredentials;
                proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                //client.Proxy = proxy;

                // checked if user requires to authenticate to access proxy
                CheckForProxyAuthentication(proxy, serverAddress);
            }

            // if user cancelled on authentication
            if (_CancelledAuth)
                _CancelledAuth = false;

            return proxy;
        }

        private void CheckForProxyAuthentication(IWebProxy proxy, string serverAddress)
        {
            // checked if user requires to authenticate to access proxy
            try
            {
                log.write("Testing for proxy authentication");
                // gets the url domain from the url address data is sent to
                string domain = "http://" + new Uri(serverAddress).Host;
                WebRequest req = WebRequest.Create(domain);

                using (WebResponse res = req.GetResponse())
                {
                    if (res == null)
                    {
                        log.write("Unable to connect to server: " + domain);
                        return;
                    }
                }

                log.write("No authentication required");
            }
            catch (WebException ex)
            {
                // if error is '407' proxy error then display message to user
                var response = ex.Response as HttpWebResponse;
                if ((int)response.StatusCode == 407)
                {
                    log.write("Proxy detected: user authentication required");
                    // get user and password from user via dialogue box
                    FrmProxyAuth frmProxyAuth = new FrmProxyAuth(this, proxy);
                    frmProxyAuth.ShowDialog();

                }
                else
                {
                    log.write(ex.ToString());
                }
            }
        }
        
        /// <summary>
        /// Checks connection between client and HVP server. Also checks public and private keys are working
        /// and data can be encrypted and decrypted correctly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testServerEncryptionDecryptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckKeys(conf))
            {
                string liveResult = TestServerEncryption(conf.ServerAddress, "Live Server");
                string testResult = TestServerEncryption(conf.TestServerAddress, "Test Server");

                if (liveResult == "Test successful" && testResult == "Test successful")
                {
                    MessageBox.Show("Live and Test server connections was successful!", "Test Success!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (liveResult == "Test successful")
                    {
                        MessageBox.Show("Application was able to connect to the live server but failed to connect to Test server: \n\n" + testResult +
                            "\n\nPlease note, you should still be able to send data to the live server.",
                            "Live Server Connection was successful but Test server was not", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // unable to connect due to live server is down, if the live server is down then the test server
                        // that piggy backs off the live server will be down as well
                        MessageBox.Show(liveResult, "Test Connection Failed!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            else
                MessageBox.Show("The system can not locate the Private Key, a connection can not be established without the private key. You can add a private key from the Tools menu and selecting Import Private Key. If you don't have a private key please contact someone from HVP.",
                    "No private key set", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void testDBServerConnectivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            
            SiteConf.Upload.Object upload = ExporterCommon.DataLoader.GetUpload(_uploadID);
            bool connTest = plugin.TestSqlConnection(upload);
            if (connTest)
                MessageBox.Show("Database server connection test successful!", "Test Success!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Could not connect to the database server, check your db settings are correct. For more information check the log file.", "Error!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            Cursor = Cursors.Default;
        }

        private void btnClearLogText_Click(object sender, EventArgs e)
        {
            txtStatusLog.Clear();
        }

        private void decryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDecrypt frmDecrypt = new FrmDecrypt(conf.PassKey);
            frmDecrypt.ShowDialog();
        }

        private void btnAddDisease_Click(object sender, EventArgs e)
        {
            if (dgGenes.Rows.Count != 0)
            {
                int indexID = GetColumnID("ID", dgGenes).Index;
                int geneID = (int)dgGenes.SelectedRows[0].Cells[indexID].Value;
                FrmGeneDisease frmGeneDisease = new FrmGeneDisease(this, geneID, _uploadID);
                frmGeneDisease.ShowDialog();
            }
        }

        /// <summary>
        /// Manual check for updates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.write("Checking for updates....");
            bool UpdateAvailble = AutoUpdater.Start(conf.AutoUpdateLink);

            if (UpdateAvailble == false)
            {
                MessageBox.Show("You are currently running the latest version " + _Version.Remove(_Version.Length - 2) + ".", "No update available");
                _quickClose = false;
                log.write("You are currently running the latest version " + _Version.Remove(_Version.Length - 2) + ".");
            }
            else
            {
                _quickClose = true;
                log.write("New version available");
            }
        }

        /// <summary>
        /// Checks for an update at every interval specified.
        /// </summary>
        /// <param name="n">time in milliseconds</param>
        private void CheckForUpdatesTimedEvent(int n)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

            timer.Tick += new EventHandler(OnUpdateTimedEvent);
            timer.Interval = n;
            timer.Start();
        }

        private void OnUpdateTimedEvent(object source, EventArgs e)
        {
            AutoUpdater.Start(conf.AutoUpdateLink);
        }

        private void importPrivateKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // check if private key already exist
            if (CheckKeys(conf))
            {
                DialogResult result = MessageBox.Show("There is a Private key setup already, continuing will overwrite the previous Private key. Are you sure you want to continue?", 
                    "Private key already exist", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

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
                    string copyPath = _commonAppPath + "\\Keys\\" + fileName;
                    log.write("Private key Copied location: " + copyPath);
                    System.IO.File.Copy(ofd.FileName, copyPath, true);

                    // set the new private key filename and write to xml file
                    conf.PrivateKey = fileName;
                    context.SerializeObject();

                    // inform user new private key has been set
                    MessageBox.Show("New Private Key has been set", "Success!", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to import file please check file path is correct \n" + ex.ToString(), 
                        "Error importing file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Configuration XML settings
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open configuration xml application form dialogue box
            FrmConf frmConf = new FrmConf(conf, context, this, false);
            frmConf.ShowDialog();
        }

        private void minimiseToSystemTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void exportLogsToATextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 2;
            sfd.FileName = "log.txt";
            
            try
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.Copy(log.GetLogPath(), sfd.FileName, true);

                    MessageBox.Show("Log file has been saved to: " + sfd.FileName, "Log file Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The log file could not be saved to: " + sfd.FileName + "\n" + ex.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Disables all controls in a Form, during application "thinking" time and you don't 
        /// want your uses clicking on controls.
        /// </summary>
        /// <param name="frm"></param>
        public void DisableAllControls()
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
        public void EnableAllControls()
        {
            foreach (Control con in this.Controls)
            {
                con.Enabled = true;
            }
        }

        private void userDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string userDocURL = @"http://portal.hvpaustralia.org.au/VariantExporter/VariantExporterUserDoc.pdf";
            System.Diagnostics.Process.Start(userDocURL);
        }
    }
}
