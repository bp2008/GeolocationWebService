namespace GeolocationWebService
{
	partial class ImportDataForm
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
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtFilePath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnImportNow = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.lblProgress = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "IP2LOCATION-LITE-DB11.CSV";
			this.openFileDialog1.Filter = "CSV files|*.csv";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(246, 12);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtFilePath
			// 
			this.txtFilePath.Location = new System.Drawing.Point(12, 41);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.Size = new System.Drawing.Size(309, 20);
			this.txtFilePath.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(173, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "IP2Location DB11.LITE IPvX CSV:";
			// 
			// btnImportNow
			// 
			this.btnImportNow.Location = new System.Drawing.Point(104, 67);
			this.btnImportNow.Name = "btnImportNow";
			this.btnImportNow.Size = new System.Drawing.Size(108, 23);
			this.btnImportNow.TabIndex = 3;
			this.btnImportNow.Text = "Import Data Now";
			this.btnImportNow.UseVisualStyleBackColor = true;
			this.btnImportNow.Click += new System.EventHandler(this.btnImportNow_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 96);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(309, 23);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 4;
			// 
			// lblProgress
			// 
			this.lblProgress.AutoSize = true;
			this.lblProgress.Location = new System.Drawing.Point(102, 124);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(21, 13);
			this.lblProgress.TabIndex = 6;
			this.lblProgress.Text = "0%";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 124);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Records Added:";
			// 
			// ImportDataForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(333, 146);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.btnImportNow);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtFilePath);
			this.Controls.Add(this.btnBrowse);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ImportDataForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Import IPv4 Data";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportDataForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

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
	}
}