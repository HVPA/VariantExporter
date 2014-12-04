namespace VariantExporterWinGUI
{
    partial class FrmConf
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtAutoUpdateLink = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPassKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTestServerAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtServerAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSetPrivateKey = new System.Windows.Forms.Button();
            this.txtPrivateKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOrganisationHashCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPortalWebsite = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxClearCache = new System.Windows.Forms.CheckBox();
            this.txtProxyAddress = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtProxyUsername = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtProxyPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtProxyUserDomain = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtLinkageKey = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxValidateMedicareChecksum = new System.Windows.Forms.CheckBox();
            this.cbxSuppliesMedicareDigits5to9Only = new System.Windows.Forms.CheckBox();
            this.cbxExcludeMedicare = new System.Windows.Forms.CheckBox();
            this.cbxAdditionalYOBPCodeHash = new System.Windows.Forms.CheckBox();
            this.cbxYoBFatherSonChecking = new System.Windows.Forms.CheckBox();
            this.cbxTightSexMatching = new System.Windows.Forms.CheckBox();
            this.cbxNameTranspositionAllowed = new System.Windows.Forms.CheckBox();
            this.cbxEnableGRHANITE = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.mtxtProxyPort = new System.Windows.Forms.MaskedTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(469, 365);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(550, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtAutoUpdateLink
            // 
            this.txtAutoUpdateLink.Location = new System.Drawing.Point(9, 261);
            this.txtAutoUpdateLink.Name = "txtAutoUpdateLink";
            this.txtAutoUpdateLink.Size = new System.Drawing.Size(600, 20);
            this.txtAutoUpdateLink.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Auto Update URL Address";
            // 
            // txtPassKey
            // 
            this.txtPassKey.Location = new System.Drawing.Point(8, 101);
            this.txtPassKey.Name = "txtPassKey";
            this.txtPassKey.Size = new System.Drawing.Size(600, 20);
            this.txtPassKey.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(387, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Pass Key ( If this is blank or has not been set, please contact HVP for your code" +
    ")";
            // 
            // txtTestServerAddress
            // 
            this.txtTestServerAddress.Location = new System.Drawing.Point(9, 222);
            this.txtTestServerAddress.Name = "txtTestServerAddress";
            this.txtTestServerAddress.Size = new System.Drawing.Size(600, 20);
            this.txtTestServerAddress.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Test Upload Server URL Address ";
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.Location = new System.Drawing.Point(8, 181);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(600, 20);
            this.txtServerAddress.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Upload Server URL Address ";
            // 
            // btnSetPrivateKey
            // 
            this.btnSetPrivateKey.Location = new System.Drawing.Point(364, 142);
            this.btnSetPrivateKey.Name = "btnSetPrivateKey";
            this.btnSetPrivateKey.Size = new System.Drawing.Size(75, 20);
            this.btnSetPrivateKey.TabIndex = 6;
            this.btnSetPrivateKey.Text = "Set Key";
            this.btnSetPrivateKey.UseVisualStyleBackColor = true;
            this.btnSetPrivateKey.Click += new System.EventHandler(this.btnSetPrivateKey_Click);
            // 
            // txtPrivateKey
            // 
            this.txtPrivateKey.Location = new System.Drawing.Point(9, 142);
            this.txtPrivateKey.Name = "txtPrivateKey";
            this.txtPrivateKey.ReadOnly = true;
            this.txtPrivateKey.Size = new System.Drawing.Size(349, 20);
            this.txtPrivateKey.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(438, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Private Key file (If you don\'t have a Private Key file please contact HVP for you" +
    "r private key)";
            // 
            // txtOrganisationHashCode
            // 
            this.txtOrganisationHashCode.Location = new System.Drawing.Point(9, 59);
            this.txtOrganisationHashCode.Name = "txtOrganisationHashCode";
            this.txtOrganisationHashCode.Size = new System.Drawing.Size(600, 20);
            this.txtOrganisationHashCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(451, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Organisation Hashcode (If this is blank or has not been set, please contact HVP f" +
    "or your code)";
            // 
            // txtPortalWebsite
            // 
            this.txtPortalWebsite.Location = new System.Drawing.Point(9, 20);
            this.txtPortalWebsite.Name = "txtPortalWebsite";
            this.txtPortalWebsite.Size = new System.Drawing.Size(600, 20);
            this.txtPortalWebsite.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "HVP Portal Website";
            // 
            // cbxClearCache
            // 
            this.cbxClearCache.AutoSize = true;
            this.cbxClearCache.Location = new System.Drawing.Point(9, 288);
            this.cbxClearCache.Name = "cbxClearCache";
            this.cbxClearCache.Size = new System.Drawing.Size(142, 17);
            this.cbxClearCache.TabIndex = 15;
            this.cbxClearCache.Text = "Clear cache after upload";
            this.cbxClearCache.UseVisualStyleBackColor = true;
            // 
            // txtProxyAddress
            // 
            this.txtProxyAddress.Location = new System.Drawing.Point(8, 47);
            this.txtProxyAddress.Name = "txtProxyAddress";
            this.txtProxyAddress.Size = new System.Drawing.Size(600, 20);
            this.txtProxyAddress.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Proxy Address";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Proxy Port";
            // 
            // txtProxyUsername
            // 
            this.txtProxyUsername.Location = new System.Drawing.Point(8, 127);
            this.txtProxyUsername.Name = "txtProxyUsername";
            this.txtProxyUsername.Size = new System.Drawing.Size(284, 20);
            this.txtProxyUsername.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Proxy Username";
            // 
            // txtProxyPassword
            // 
            this.txtProxyPassword.Location = new System.Drawing.Point(298, 127);
            this.txtProxyPassword.Name = "txtProxyPassword";
            this.txtProxyPassword.PasswordChar = '•';
            this.txtProxyPassword.Size = new System.Drawing.Size(284, 20);
            this.txtProxyPassword.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(295, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Proxy Password";
            // 
            // txtProxyUserDomain
            // 
            this.txtProxyUserDomain.Location = new System.Drawing.Point(8, 167);
            this.txtProxyUserDomain.Name = "txtProxyUserDomain";
            this.txtProxyUserDomain.Size = new System.Drawing.Size(284, 20);
            this.txtProxyUserDomain.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 150);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Proxy User Domain";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(627, 347);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbxClearCache);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtAutoUpdateLink);
            this.tabPage1.Controls.Add(this.txtPortalWebsite);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtPassKey);
            this.tabPage1.Controls.Add(this.txtOrganisationHashCode);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtTestServerAddress);
            this.tabPage1.Controls.Add(this.txtPrivateKey);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.btnSetPrivateKey);
            this.tabPage1.Controls.Add(this.txtServerAddress);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(619, 321);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Site Configuration";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.mtxtProxyPort);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.txtProxyUserDomain);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtProxyPassword);
            this.tabPage2.Controls.Add(this.txtProxyAddress);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.txtProxyUsername);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(619, 321);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Proxy Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtLinkageKey);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.cbxEnableGRHANITE);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(619, 321);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "GRHANITE Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtLinkageKey
            // 
            this.txtLinkageKey.Location = new System.Drawing.Point(19, 58);
            this.txtLinkageKey.Name = "txtLinkageKey";
            this.txtLinkageKey.Size = new System.Drawing.Size(590, 20);
            this.txtLinkageKey.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(434, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Linkage Key (If this is blank or has not been set, please contact HVP for your Li" +
    "nkage Key)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxValidateMedicareChecksum);
            this.groupBox1.Controls.Add(this.cbxSuppliesMedicareDigits5to9Only);
            this.groupBox1.Controls.Add(this.cbxExcludeMedicare);
            this.groupBox1.Controls.Add(this.cbxAdditionalYOBPCodeHash);
            this.groupBox1.Controls.Add(this.cbxYoBFatherSonChecking);
            this.groupBox1.Controls.Add(this.cbxTightSexMatching);
            this.groupBox1.Controls.Add(this.cbxNameTranspositionAllowed);
            this.groupBox1.Location = new System.Drawing.Point(19, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 151);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hashing Options";
            // 
            // cbxValidateMedicareChecksum
            // 
            this.cbxValidateMedicareChecksum.AutoSize = true;
            this.cbxValidateMedicareChecksum.Location = new System.Drawing.Point(6, 100);
            this.cbxValidateMedicareChecksum.Name = "cbxValidateMedicareChecksum";
            this.cbxValidateMedicareChecksum.Size = new System.Drawing.Size(299, 17);
            this.cbxValidateMedicareChecksum.TabIndex = 7;
            this.cbxValidateMedicareChecksum.Text = "Validate Medicare Checksum (Medicare must be 10 digits)";
            this.cbxValidateMedicareChecksum.UseVisualStyleBackColor = true;
            // 
            // cbxSuppliesMedicareDigits5to9Only
            // 
            this.cbxSuppliesMedicareDigits5to9Only.AutoSize = true;
            this.cbxSuppliesMedicareDigits5to9Only.Location = new System.Drawing.Point(233, 72);
            this.cbxSuppliesMedicareDigits5to9Only.Name = "cbxSuppliesMedicareDigits5to9Only";
            this.cbxSuppliesMedicareDigits5to9Only.Size = new System.Drawing.Size(179, 17);
            this.cbxSuppliesMedicareDigits5to9Only.TabIndex = 6;
            this.cbxSuppliesMedicareDigits5to9Only.Text = "Medicare digits 5-9 only supplied";
            this.cbxSuppliesMedicareDigits5to9Only.UseVisualStyleBackColor = true;
            // 
            // cbxExcludeMedicare
            // 
            this.cbxExcludeMedicare.AutoSize = true;
            this.cbxExcludeMedicare.Location = new System.Drawing.Point(6, 72);
            this.cbxExcludeMedicare.Name = "cbxExcludeMedicare";
            this.cbxExcludeMedicare.Size = new System.Drawing.Size(111, 17);
            this.cbxExcludeMedicare.TabIndex = 5;
            this.cbxExcludeMedicare.Text = "Exclude Medicare";
            this.cbxExcludeMedicare.UseVisualStyleBackColor = true;
            // 
            // cbxAdditionalYOBPCodeHash
            // 
            this.cbxAdditionalYOBPCodeHash.AutoSize = true;
            this.cbxAdditionalYOBPCodeHash.Location = new System.Drawing.Point(233, 46);
            this.cbxAdditionalYOBPCodeHash.Name = "cbxAdditionalYOBPCodeHash";
            this.cbxAdditionalYOBPCodeHash.Size = new System.Drawing.Size(175, 17);
            this.cbxAdditionalYOBPCodeHash.TabIndex = 4;
            this.cbxAdditionalYOBPCodeHash.Text = "Additional YoB Post Code Hash";
            this.cbxAdditionalYOBPCodeHash.UseVisualStyleBackColor = true;
            // 
            // cbxYoBFatherSonChecking
            // 
            this.cbxYoBFatherSonChecking.AutoSize = true;
            this.cbxYoBFatherSonChecking.Location = new System.Drawing.Point(6, 46);
            this.cbxYoBFatherSonChecking.Name = "cbxYoBFatherSonChecking";
            this.cbxYoBFatherSonChecking.Size = new System.Drawing.Size(181, 17);
            this.cbxYoBFatherSonChecking.TabIndex = 3;
            this.cbxYoBFatherSonChecking.Text = "Year of Birth father son checking";
            this.cbxYoBFatherSonChecking.UseVisualStyleBackColor = true;
            // 
            // cbxTightSexMatching
            // 
            this.cbxTightSexMatching.AutoSize = true;
            this.cbxTightSexMatching.Location = new System.Drawing.Point(233, 19);
            this.cbxTightSexMatching.Name = "cbxTightSexMatching";
            this.cbxTightSexMatching.Size = new System.Drawing.Size(118, 17);
            this.cbxTightSexMatching.TabIndex = 2;
            this.cbxTightSexMatching.Text = "Tight Sex Matching";
            this.cbxTightSexMatching.UseVisualStyleBackColor = true;
            // 
            // cbxNameTranspositionAllowed
            // 
            this.cbxNameTranspositionAllowed.AutoSize = true;
            this.cbxNameTranspositionAllowed.Location = new System.Drawing.Point(6, 19);
            this.cbxNameTranspositionAllowed.Name = "cbxNameTranspositionAllowed";
            this.cbxNameTranspositionAllowed.Size = new System.Drawing.Size(160, 17);
            this.cbxNameTranspositionAllowed.TabIndex = 1;
            this.cbxNameTranspositionAllowed.Text = "Name Transposition Allowed";
            this.cbxNameTranspositionAllowed.UseVisualStyleBackColor = true;
            // 
            // cbxEnableGRHANITE
            // 
            this.cbxEnableGRHANITE.AutoSize = true;
            this.cbxEnableGRHANITE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEnableGRHANITE.Location = new System.Drawing.Point(19, 18);
            this.cbxEnableGRHANITE.Name = "cbxEnableGRHANITE";
            this.cbxEnableGRHANITE.Size = new System.Drawing.Size(133, 17);
            this.cbxEnableGRHANITE.TabIndex = 0;
            this.cbxEnableGRHANITE.Text = "Enable GRHANITE";
            this.cbxEnableGRHANITE.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(9, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(564, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "NOTE: Leave these settings blank if your Internet connection is not going through" +
    " a Proxy Server.";
            // 
            // mtxtProxyPort
            // 
            this.mtxtProxyPort.Location = new System.Drawing.Point(9, 86);
            this.mtxtProxyPort.Mask = "000000";
            this.mtxtProxyPort.Name = "mtxtProxyPort";
            this.mtxtProxyPort.Size = new System.Drawing.Size(56, 20);
            this.mtxtProxyPort.TabIndex = 5;
            // 
            // FrmConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 395);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtPassKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTestServerAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtServerAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSetPrivateKey;
        private System.Windows.Forms.TextBox txtPrivateKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOrganisationHashCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPortalWebsite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAutoUpdateLink;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbxClearCache;
        private System.Windows.Forms.TextBox txtProxyUserDomain;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProxyPassword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtProxyUsername;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtProxyAddress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbxNameTranspositionAllowed;
        private System.Windows.Forms.CheckBox cbxEnableGRHANITE;
        private System.Windows.Forms.TextBox txtLinkageKey;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cbxValidateMedicareChecksum;
        private System.Windows.Forms.CheckBox cbxSuppliesMedicareDigits5to9Only;
        private System.Windows.Forms.CheckBox cbxExcludeMedicare;
        private System.Windows.Forms.CheckBox cbxAdditionalYOBPCodeHash;
        private System.Windows.Forms.CheckBox cbxYoBFatherSonChecking;
        private System.Windows.Forms.CheckBox cbxTightSexMatching;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.MaskedTextBox mtxtProxyPort;
    }
}