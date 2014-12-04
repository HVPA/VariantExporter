namespace VariantExporterWinGUI
{
    partial class FrmGene
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtRefSeq = new System.Windows.Forms.TextBox();
            this.txtGene = new System.Windows.Forms.TextBox();
            this.lblGeneError = new System.Windows.Forms.Label();
            this.txtRefSeqVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gene Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "RefSeq Name";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(63, 182);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(183, 182);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtRefSeq
            // 
            this.txtRefSeq.Location = new System.Drawing.Point(113, 84);
            this.txtRefSeq.Name = "txtRefSeq";
            this.txtRefSeq.Size = new System.Drawing.Size(145, 20);
            this.txtRefSeq.TabIndex = 2;
            // 
            // txtGene
            // 
            this.txtGene.Location = new System.Drawing.Point(113, 35);
            this.txtGene.Name = "txtGene";
            this.txtGene.Size = new System.Drawing.Size(145, 20);
            this.txtGene.TabIndex = 1;
            // 
            // lblGeneError
            // 
            this.lblGeneError.AutoSize = true;
            this.lblGeneError.ForeColor = System.Drawing.Color.Red;
            this.lblGeneError.Location = new System.Drawing.Point(110, 58);
            this.lblGeneError.Name = "lblGeneError";
            this.lblGeneError.Size = new System.Drawing.Size(159, 13);
            this.lblGeneError.TabIndex = 6;
            this.lblGeneError.Text = "*Gene name is a mandatory field";
            this.lblGeneError.Visible = false;
            // 
            // txtRefSeqVersion
            // 
            this.txtRefSeqVersion.Location = new System.Drawing.Point(113, 134);
            this.txtRefSeqVersion.Name = "txtRefSeqVersion";
            this.txtRefSeqVersion.Size = new System.Drawing.Size(145, 20);
            this.txtRefSeqVersion.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "RefSeq Version";
            // 
            // FrmGene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 217);
            this.Controls.Add(this.txtRefSeqVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblGeneError);
            this.Controls.Add(this.txtGene);
            this.Controls.Add(this.txtRefSeq);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGene";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HVP - Gene";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtRefSeq;
        private System.Windows.Forms.TextBox txtGene;
        private System.Windows.Forms.Label lblGeneError;
        private System.Windows.Forms.TextBox txtRefSeqVersion;
        private System.Windows.Forms.Label label4;
    }
}