namespace GeolocationWebService
{
	partial class SetupForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtFilePath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnImportNow = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.lblProgress = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.cbResolveIPv4WithIPv6Table = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.linkToDataSite = new System.Windows.Forms.LinkLabel();
			this.label6 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "IP2LOCATION-LITE-DB11.CSV";
			this.openFileDialog1.Filter = "CSV files|*.csv";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(3, 145);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(109, 23);
			this.btnBrowse.TabIndex = 20;
			this.btnBrowse.Text = "Browse for CSV";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtFilePath
			// 
			this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilePath.Location = new System.Drawing.Point(118, 147);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.Size = new System.Drawing.Size(391, 20);
			this.txtFilePath.TabIndex = 30;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(115, 122);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(147, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "IP2Location DB11.LITE CSV:";
			// 
			// btnImportNow
			// 
			this.btnImportNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnImportNow.Location = new System.Drawing.Point(3, 173);
			this.btnImportNow.Name = "btnImportNow";
			this.btnImportNow.Size = new System.Drawing.Size(109, 23);
			this.btnImportNow.TabIndex = 40;
			this.btnImportNow.Text = "Begin Import";
			this.btnImportNow.UseVisualStyleBackColor = true;
			this.btnImportNow.Click += new System.EventHandler(this.btnImportNow_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(118, 173);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(391, 23);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 4;
			// 
			// lblProgress
			// 
			this.lblProgress.AutoSize = true;
			this.lblProgress.Location = new System.Drawing.Point(205, 199);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(21, 13);
			this.lblProgress.TabIndex = 6;
			this.lblProgress.Text = "0%";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(115, 199);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Records Added:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3, 31);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(506, 87);
			this.label4.TabIndex = 10;
			this.label4.Text = resources.GetString("label4.Text");
			// 
			// cbResolveIPv4WithIPv6Table
			// 
			this.cbResolveIPv4WithIPv6Table.AutoSize = true;
			this.cbResolveIPv4WithIPv6Table.Location = new System.Drawing.Point(3, 32);
			this.cbResolveIPv4WithIPv6Table.Name = "cbResolveIPv4WithIPv6Table";
			this.cbResolveIPv4WithIPv6Table.Size = new System.Drawing.Size(218, 17);
			this.cbResolveIPv4WithIPv6Table.TabIndex = 0;
			this.cbResolveIPv4WithIPv6Table.Text = "Resolve IPv4 queries with the IPv6 table";
			this.cbResolveIPv4WithIPv6Table.UseVisualStyleBackColor = true;
			this.cbResolveIPv4WithIPv6Table.CheckedChanged += new System.EventHandler(this.cbResolveIPv4WithIPv6Table_CheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(189, 24);
			this.label5.TabIndex = 14;
			this.label5.Text = "Service Configuration";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.cbResolveIPv4WithIPv6Table);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(266, 222);
			this.panel1.TabIndex = 15;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.Control;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.linkToDataSite);
			this.panel2.Controls.Add(this.btnBrowse);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.txtFilePath);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.btnImportNow);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.progressBar1);
			this.panel2.Controls.Add(this.lblProgress);
			this.panel2.Location = new System.Drawing.Point(284, 12);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(516, 222);
			this.panel2.TabIndex = 16;
			// 
			// linkToDataSite
			// 
			this.linkToDataSite.AutoSize = true;
			this.linkToDataSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkToDataSite.Location = new System.Drawing.Point(251, 0);
			this.linkToDataSite.Name = "linkToDataSite";
			this.linkToDataSite.Size = new System.Drawing.Size(229, 24);
			this.linkToDataSite.TabIndex = 10;
			this.linkToDataSite.TabStop = true;
			this.linkToDataSite.Text = "https://lite.ip2location.com/";
			this.linkToDataSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkToDataSite_LinkClicked);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(3, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(206, 24);
			this.label6.TabIndex = 15;
			this.label6.Text = "IP2Location Data Import";
			// 
			// SetupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(812, 247);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "SetupForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Setup GeolocationWebService";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportDataForm_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtFilePath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnImportNow;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox cbResolveIPv4WithIPv6Table;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.LinkLabel linkToDataSite;
		private System.Windows.Forms.Label label6;
	}
}