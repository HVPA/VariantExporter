namespace VariantExporterWinGUI
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimiseToSystemTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testDBServerConnectivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testServerEncryptionDecryptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendDataToTestServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportLogsToATextFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPrivateKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtStatusLog = new System.Windows.Forms.TextBox();
            this.btnClearLogText = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDataSource = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.dgGenes = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddDisease = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlFileSource = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLoadDataSource = new System.Windows.Forms.Button();
            this.txtDataSourcePath = new System.Windows.Forms.TextBox();
            this.pnlServer = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDsPassword = new System.Windows.Forms.TextBox();
            this.txtDsUsername = new System.Windows.Forms.TextBox();
            this.txtDatasourceServer = new System.Windows.Forms.TextBox();
            this.lblSavedMessage = new System.Windows.Forms.Label();
            this.gbRbtnLst = new System.Windows.Forms.GroupBox();
            this.rbnSqlServer = new System.Windows.Forms.RadioButton();
            this.rbnSpreadsheet = new System.Windows.Forms.RadioButton();
            this.rbnDatabase = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUpload = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabDataUpload = new System.Windows.Forms.TabPage();
            this.panel11 = new System.Windows.Forms.Panel();
            this.dgVariant = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.btnSendData = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWho = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnErrors = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabDataSource.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGenes)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlFileSource.SuspendLayout();
            this.pnlServer.SuspendLayout();
            this.gbRbtnLst.SuspendLayout();
            this.tabDataUpload.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVariant)).BeginInit();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.testToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(801, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeUploadToolStripMenuItem,
            this.minimiseToSystemTrayToolStripMenuItem,
            this.exitApplicationToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // changeUploadToolStripMenuItem
            // 
            this.changeUploadToolStripMenuItem.Name = "changeUploadToolStripMenuItem";
            this.changeUploadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.changeUploadToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.changeUploadToolStripMenuItem.Text = "Open Existing Upload";
            this.changeUploadToolStripMenuItem.Click += new System.EventHandler(this.changeUploadToolStripMenuItem_Click);
            // 
            // minimiseToSystemTrayToolStripMenuItem
            // 
            this.minimiseToSystemTrayToolStripMenuItem.Name = "minimiseToSystemTrayToolStripMenuItem";
            this.minimiseToSystemTrayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.minimiseToSystemTrayToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.minimiseToSystemTrayToolStripMenuItem.Text = "Minimise to System Tray";
            this.minimiseToSystemTrayToolStripMenuItem.Click += new System.EventHandler(this.minimiseToSystemTrayToolStripMenuItem_Click);
            // 
            // exitApplicationToolStripMenuItem
            // 
            this.exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
            this.exitApplicationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Q)));
            this.exitApplicationToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.exitApplicationToolStripMenuItem.Text = "Exit Application";
            this.exitApplicationToolStripMenuItem.Click += new System.EventHandler(this.exitApplicationToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testDBServerConnectivityToolStripMenuItem,
            this.testServerEncryptionDecryptionToolStripMenuItem,
            this.sendDataToTestServerToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // testDBServerConnectivityToolStripMenuItem
            // 
            this.testDBServerConnectivityToolStripMenuItem.Name = "testDBServerConnectivityToolStripMenuItem";
            this.testDBServerConnectivityToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.testDBServerConnectivityToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.testDBServerConnectivityToolStripMenuItem.Text = "Test DB Server Connectivity";
            this.testDBServerConnectivityToolStripMenuItem.Click += new System.EventHandler(this.testDBServerConnectivityToolStripMenuItem_Click);
            // 
            // testServerEncryptionDecryptionToolStripMenuItem
            // 
            this.testServerEncryptionDecryptionToolStripMenuItem.Name = "testServerEncryptionDecryptionToolStripMenuItem";
            this.testServerEncryptionDecryptionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.testServerEncryptionDecryptionToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.testServerEncryptionDecryptionToolStripMenuItem.Text = "Test Server Connectivity";
            this.testServerEncryptionDecryptionToolStripMenuItem.Click += new System.EventHandler(this.testServerEncryptionDecryptionToolStripMenuItem_Click);
            // 
            // sendDataToTestServerToolStripMenuItem
            // 
            this.sendDataToTestServerToolStripMenuItem.Name = "sendDataToTestServerToolStripMenuItem";
            this.sendDataToTestServerToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.sendDataToTestServerToolStripMenuItem.Text = "Send Data to Test Server";
            this.sendDataToTestServerToolStripMenuItem.Click += new System.EventHandler(this.sendDataToTestServerToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportLogsToATextFileToolStripMenuItem,
            this.decryptToolStripMenuItem,
            this.importPrivateKeyToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // exportLogsToATextFileToolStripMenuItem
            // 
            this.exportLogsToATextFileToolStripMenuItem.Name = "exportLogsToATextFileToolStripMenuItem";
            this.exportLogsToATextFileToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.exportLogsToATextFileToolStripMenuItem.Text = "Export logs to a text file";
            this.exportLogsToATextFileToolStripMenuItem.Click += new System.EventHandler(this.exportLogsToATextFileToolStripMenuItem_Click);
            // 
            // decryptToolStripMenuItem
            // 
            this.decryptToolStripMenuItem.Name = "decryptToolStripMenuItem";
            this.decryptToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.decryptToolStripMenuItem.Text = "Decryptonator";
            this.decryptToolStripMenuItem.Click += new System.EventHandler(this.decryptToolStripMenuItem_Click);
            // 
            // importPrivateKeyToolStripMenuItem
            // 
            this.importPrivateKeyToolStripMenuItem.Name = "importPrivateKeyToolStripMenuItem";
            this.importPrivateKeyToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.importPrivateKeyToolStripMenuItem.Text = "Import Private Key";
            this.importPrivateKeyToolStripMenuItem.Click += new System.EventHandler(this.importPrivateKeyToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userDocumentationToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // userDocumentationToolStripMenuItem
            // 
            this.userDocumentationToolStripMenuItem.Name = "userDocumentationToolStripMenuItem";
            this.userDocumentationToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.userDocumentationToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.userDocumentationToolStripMenuItem.Text = "User Documentation";
            this.userDocumentationToolStripMenuItem.Click += new System.EventHandler(this.userDocumentationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Double-click to restore application";
            this.notifyIcon1.Text = "Variant Exporter";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.optionsToolStripMenuItem.Text = "Options..";
            // 
            // txtStatusLog
            // 
            this.txtStatusLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatusLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatusLog.Location = new System.Drawing.Point(0, 0);
            this.txtStatusLog.Multiline = true;
            this.txtStatusLog.Name = "txtStatusLog";
            this.txtStatusLog.ReadOnly = true;
            this.txtStatusLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatusLog.Size = new System.Drawing.Size(658, 128);
            this.txtStatusLog.TabIndex = 19;
            // 
            // btnClearLogText
            // 
            this.btnClearLogText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearLogText.Location = new System.Drawing.Point(21, 92);
            this.btnClearLogText.Name = "btnClearLogText";
            this.btnClearLogText.Size = new System.Drawing.Size(99, 23);
            this.btnClearLogText.TabIndex = 20;
            this.btnClearLogText.Text = "Clear Log Text";
            this.btnClearLogText.UseVisualStyleBackColor = true;
            this.btnClearLogText.Click += new System.EventHandler(this.btnClearLogText_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel9);
            this.groupBox2.Controls.Add(this.panel8);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 585);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(801, 153);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.txtStatusLog);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 22);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(658, 128);
            this.panel9.TabIndex = 22;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnClearLogText);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(661, 22);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(137, 128);
            this.panel8.TabIndex = 21;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 541);
            this.panel1.TabIndex = 22;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDataSource);
            this.tabControl1.Controls.Add(this.tabDataUpload);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 100);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(801, 541);
            this.tabControl1.TabIndex = 17;
            // 
            // tabDataSource
            // 
            this.tabDataSource.Controls.Add(this.panel3);
            this.tabDataSource.Controls.Add(this.panel2);
            this.tabDataSource.Location = new System.Drawing.Point(4, 22);
            this.tabDataSource.Name = "tabDataSource";
            this.tabDataSource.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataSource.Size = new System.Drawing.Size(793, 515);
            this.tabDataSource.TabIndex = 0;
            this.tabDataSource.Text = "Data Source";
            this.tabDataSource.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 210);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(787, 302);
            this.panel3.TabIndex = 39;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(787, 257);
            this.panel5.TabIndex = 41;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel7);
            this.groupBox1.Controls.Add(this.panel6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(787, 257);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gene/Disease";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.dgGenes);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 43);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(781, 211);
            this.panel7.TabIndex = 33;
            // 
            // dgGenes
            // 
            this.dgGenes.AllowUserToAddRows = false;
            this.dgGenes.AllowUserToDeleteRows = false;
            this.dgGenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgGenes.Location = new System.Drawing.Point(0, 0);
            this.dgGenes.MultiSelect = false;
            this.dgGenes.Name = "dgGenes";
            this.dgGenes.ReadOnly = true;
            this.dgGenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgGenes.Size = new System.Drawing.Size(781, 211);
            this.dgGenes.TabIndex = 23;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 16);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(781, 27);
            this.panel6.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(482, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Here you can specify and list the different genes and/or diseases that are associ" +
    "ated with this upload";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnRemove);
            this.panel4.Controls.Add(this.btnAddDisease);
            this.panel4.Controls.Add(this.btnEdit);
            this.panel4.Controls.Add(this.btnAdd);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 257);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 45);
            this.panel4.TabIndex = 40;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(573, 11);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 37;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAddDisease
            // 
            this.btnAddDisease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDisease.Location = new System.Drawing.Point(654, 11);
            this.btnAddDisease.Name = "btnAddDisease";
            this.btnAddDisease.Size = new System.Drawing.Size(123, 23);
            this.btnAddDisease.TabIndex = 39;
            this.btnAddDisease.Text = "Add Disease to Gene";
            this.btnAddDisease.UseVisualStyleBackColor = true;
            this.btnAddDisease.Click += new System.EventHandler(this.btnAddDisease_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(492, 11);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 38;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(411, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 36;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlFileSource);
            this.panel2.Controls.Add(this.pnlServer);
            this.panel2.Controls.Add(this.lblSavedMessage);
            this.panel2.Controls.Add(this.gbRbtnLst);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtUpload);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(787, 207);
            this.panel2.TabIndex = 38;
            // 
            // pnlFileSource
            // 
            this.pnlFileSource.Controls.Add(this.label7);
            this.pnlFileSource.Controls.Add(this.btnLoadDataSource);
            this.pnlFileSource.Controls.Add(this.txtDataSourcePath);
            this.pnlFileSource.Location = new System.Drawing.Point(20, 75);
            this.pnlFileSource.Name = "pnlFileSource";
            this.pnlFileSource.Size = new System.Drawing.Size(651, 59);
            this.pnlFileSource.TabIndex = 45;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Data Source Location";
            // 
            // btnLoadDataSource
            // 
            this.btnLoadDataSource.Location = new System.Drawing.Point(431, 23);
            this.btnLoadDataSource.Name = "btnLoadDataSource";
            this.btnLoadDataSource.Size = new System.Drawing.Size(75, 23);
            this.btnLoadDataSource.TabIndex = 34;
            this.btnLoadDataSource.Text = "Browse";
            this.btnLoadDataSource.UseVisualStyleBackColor = true;
            this.btnLoadDataSource.Click += new System.EventHandler(this.btnLoadDataSource_Click);
            // 
            // txtDataSourcePath
            // 
            this.txtDataSourcePath.Location = new System.Drawing.Point(6, 25);
            this.txtDataSourcePath.Name = "txtDataSourcePath";
            this.txtDataSourcePath.ReadOnly = true;
            this.txtDataSourcePath.Size = new System.Drawing.Size(419, 20);
            this.txtDataSourcePath.TabIndex = 35;
            // 
            // pnlServer
            // 
            this.pnlServer.Controls.Add(this.label14);
            this.pnlServer.Controls.Add(this.txtDatabase);
            this.pnlServer.Controls.Add(this.label13);
            this.pnlServer.Controls.Add(this.label12);
            this.pnlServer.Controls.Add(this.label11);
            this.pnlServer.Controls.Add(this.txtDsPassword);
            this.pnlServer.Controls.Add(this.txtDsUsername);
            this.pnlServer.Controls.Add(this.txtDatasourceServer);
            this.pnlServer.Location = new System.Drawing.Point(127, 71);
            this.pnlServer.Name = "pnlServer";
            this.pnlServer.Size = new System.Drawing.Size(330, 133);
            this.pnlServer.TabIndex = 44;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(29, 59);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "Database";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(90, 56);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(218, 20);
            this.txtDatabase.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(29, 111);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Password";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(29, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Username";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Database Server Address";
            // 
            // txtDsPassword
            // 
            this.txtDsPassword.Location = new System.Drawing.Point(90, 108);
            this.txtDsPassword.Name = "txtDsPassword";
            this.txtDsPassword.PasswordChar = '•';
            this.txtDsPassword.Size = new System.Drawing.Size(218, 20);
            this.txtDsPassword.TabIndex = 3;
            // 
            // txtDsUsername
            // 
            this.txtDsUsername.Location = new System.Drawing.Point(90, 82);
            this.txtDsUsername.Name = "txtDsUsername";
            this.txtDsUsername.Size = new System.Drawing.Size(218, 20);
            this.txtDsUsername.TabIndex = 2;
            // 
            // txtDatasourceServer
            // 
            this.txtDatasourceServer.Location = new System.Drawing.Point(22, 27);
            this.txtDatasourceServer.Name = "txtDatasourceServer";
            this.txtDatasourceServer.Size = new System.Drawing.Size(286, 20);
            this.txtDatasourceServer.TabIndex = 0;
            // 
            // lblSavedMessage
            // 
            this.lblSavedMessage.AutoSize = true;
            this.lblSavedMessage.ForeColor = System.Drawing.Color.Blue;
            this.lblSavedMessage.Location = new System.Drawing.Point(28, 138);
            this.lblSavedMessage.Name = "lblSavedMessage";
            this.lblSavedMessage.Size = new System.Drawing.Size(41, 13);
            this.lblSavedMessage.TabIndex = 43;
            this.lblSavedMessage.Text = "Saved!";
            this.lblSavedMessage.Visible = false;
            // 
            // gbRbtnLst
            // 
            this.gbRbtnLst.Controls.Add(this.rbnSqlServer);
            this.gbRbtnLst.Controls.Add(this.rbnSpreadsheet);
            this.gbRbtnLst.Controls.Add(this.rbnDatabase);
            this.gbRbtnLst.Location = new System.Drawing.Point(121, 31);
            this.gbRbtnLst.Name = "gbRbtnLst";
            this.gbRbtnLst.Size = new System.Drawing.Size(336, 40);
            this.gbRbtnLst.TabIndex = 42;
            this.gbRbtnLst.TabStop = false;
            // 
            // rbnSqlServer
            // 
            this.rbnSqlServer.AutoSize = true;
            this.rbnSqlServer.Enabled = false;
            this.rbnSqlServer.Location = new System.Drawing.Point(112, 15);
            this.rbnSqlServer.Name = "rbnSqlServer";
            this.rbnSqlServer.Size = new System.Drawing.Size(105, 17);
            this.rbnSqlServer.TabIndex = 2;
            this.rbnSqlServer.TabStop = true;
            this.rbnSqlServer.Text = "Database Server";
            this.rbnSqlServer.UseVisualStyleBackColor = true;
            // 
            // rbnSpreadsheet
            // 
            this.rbnSpreadsheet.AutoSize = true;
            this.rbnSpreadsheet.Enabled = false;
            this.rbnSpreadsheet.Location = new System.Drawing.Point(236, 15);
            this.rbnSpreadsheet.Name = "rbnSpreadsheet";
            this.rbnSpreadsheet.Size = new System.Drawing.Size(41, 17);
            this.rbnSpreadsheet.TabIndex = 1;
            this.rbnSpreadsheet.TabStop = true;
            this.rbnSpreadsheet.Text = "File";
            this.rbnSpreadsheet.UseVisualStyleBackColor = true;
            // 
            // rbnDatabase
            // 
            this.rbnDatabase.AutoSize = true;
            this.rbnDatabase.Enabled = false;
            this.rbnDatabase.Location = new System.Drawing.Point(6, 15);
            this.rbnDatabase.Name = "rbnDatabase";
            this.rbnDatabase.Size = new System.Drawing.Size(90, 17);
            this.rbnDatabase.TabIndex = 0;
            this.rbnDatabase.TabStop = true;
            this.rbnDatabase.Text = "Database File";
            this.rbnDatabase.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Datasource Type";
            // 
            // txtUpload
            // 
            this.txtUpload.Location = new System.Drawing.Point(74, 10);
            this.txtUpload.Name = "txtUpload";
            this.txtUpload.Size = new System.Drawing.Size(185, 20);
            this.txtUpload.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Upload";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(26, 159);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 34);
            this.btnSave.TabIndex = 38;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabDataUpload
            // 
            this.tabDataUpload.Controls.Add(this.panel11);
            this.tabDataUpload.Controls.Add(this.panel12);
            this.tabDataUpload.Controls.Add(this.panel10);
            this.tabDataUpload.Location = new System.Drawing.Point(4, 22);
            this.tabDataUpload.Name = "tabDataUpload";
            this.tabDataUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataUpload.Size = new System.Drawing.Size(793, 515);
            this.tabDataUpload.TabIndex = 1;
            this.tabDataUpload.Text = "Send Data";
            this.tabDataUpload.UseVisualStyleBackColor = true;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.dgVariant);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(3, 130);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(787, 275);
            this.panel11.TabIndex = 32;
            // 
            // dgVariant
            // 
            this.dgVariant.AllowUserToAddRows = false;
            this.dgVariant.AllowUserToDeleteRows = false;
            this.dgVariant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVariant.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgVariant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVariant.Location = new System.Drawing.Point(0, 0);
            this.dgVariant.Name = "dgVariant";
            this.dgVariant.Size = new System.Drawing.Size(787, 275);
            this.dgVariant.TabIndex = 12;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Select";
            this.Column1.Name = "Column1";
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.panel13);
            this.panel12.Controls.Add(this.label8);
            this.panel12.Controls.Add(this.label2);
            this.panel12.Controls.Add(this.txtWho);
            this.panel12.Controls.Add(this.label9);
            this.panel12.Controls.Add(this.btnErrors);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel12.Location = new System.Drawing.Point(3, 405);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(787, 107);
            this.panel12.TabIndex = 31;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.btnSendData);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel13.Location = new System.Drawing.Point(654, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(133, 107);
            this.panel13.TabIndex = 29;
            // 
            // btnSendData
            // 
            this.btnSendData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendData.Location = new System.Drawing.Point(12, 30);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(111, 48);
            this.btnSendData.TabIndex = 7;
            this.btnSendData.Text = "Send Data";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(277, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 43);
            this.label8.TabIndex = 27;
            this.label8.Text = "Name or Initials of the person sending  the data. (Optional - This is for Auditin" +
    "g purposes)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "User/Clinician";
            // 
            // txtWho
            // 
            this.txtWho.Location = new System.Drawing.Point(94, 16);
            this.txtWho.Name = "txtWho";
            this.txtWho.Size = new System.Drawing.Size(177, 20);
            this.txtWho.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(140, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(379, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "When there are invalid data detected click here to view all the invalid datasets." +
    "";
            // 
            // btnErrors
            // 
            this.btnErrors.Enabled = false;
            this.btnErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnErrors.Location = new System.Drawing.Point(21, 67);
            this.btnErrors.Name = "btnErrors";
            this.btnErrors.Size = new System.Drawing.Size(111, 30);
            this.btnErrors.TabIndex = 8;
            this.btnErrors.Text = "See Invalid Data";
            this.btnErrors.UseVisualStyleBackColor = true;
            this.btnErrors.Click += new System.EventHandler(this.btnErrors_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.label1);
            this.panel10.Controls.Add(this.btnLoad);
            this.panel10.Controls.Add(this.btnSelectAll);
            this.panel10.Controls.Add(this.label10);
            this.panel10.Controls.Add(this.label4);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(3, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(787, 127);
            this.panel10.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(480, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Loads all the data specified from the Data Source tab, checks and validates the d" +
    "ata before sending";
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(10, 14);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(173, 34);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Load/Refresh Data";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.Location = new System.Drawing.Point(10, 63);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(130, 23);
            this.btnSelectAll.TabIndex = 24;
            this.btnSelectAll.Text = "Select All Rows";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(292, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "NB: Identifiers will be encyrpted when data is sent";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(148, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Selects / Deselects all rows from the grid below";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 20);
            this.lblTitle.TabIndex = 18;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(801, 738);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HVP - Variant Exporter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Resize += new System.EventHandler(this.TrayMinimizerForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabDataSource.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgGenes)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlFileSource.ResumeLayout(false);
            this.pnlFileSource.PerformLayout();
            this.pnlServer.ResumeLayout(false);
            this.pnlServer.PerformLayout();
            this.gbRbtnLst.ResumeLayout(false);
            this.gbRbtnLst.PerformLayout();
            this.tabDataUpload.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgVariant)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitApplicationToolStripMenuItem;
        private System.Windows.Forms.TextBox txtStatusLog;
        private System.Windows.Forms.Button btnClearLogText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem decryptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPrivateKeyToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem minimiseToSystemTrayToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDataSource;
        private System.Windows.Forms.TabPage tabDataUpload;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnErrors;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.TextBox txtWho;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlServer;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDsPassword;
        private System.Windows.Forms.TextBox txtDsUsername;
        private System.Windows.Forms.TextBox txtDatasourceServer;
        private System.Windows.Forms.Panel pnlFileSource;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLoadDataSource;
        private System.Windows.Forms.TextBox txtDataSourcePath;
        private System.Windows.Forms.Label lblSavedMessage;
        private System.Windows.Forms.GroupBox gbRbtnLst;
        private System.Windows.Forms.RadioButton rbnSqlServer;
        private System.Windows.Forms.RadioButton rbnSpreadsheet;
        private System.Windows.Forms.RadioButton rbnDatabase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUpload;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgGenes;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAddDisease;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.DataGridView dgVariant;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.ToolStripMenuItem exportLogsToATextFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testDBServerConnectivityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testServerEncryptionDecryptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendDataToTestServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userDocumentationToolStripMenuItem;
    }
}

