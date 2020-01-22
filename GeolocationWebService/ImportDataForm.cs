using BPUtil;
using BPUtil.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeolocationWebService
{
	public partial class ImportDataForm : SelfCenteredForm
	{
		public ImportDataForm()
		{
			InitializeComponent();
			openFileDialog1.InitialDirectory = Globals.ApplicationDirectoryBase;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			DialogResult dr = openFileDialog1.ShowDialog();
			if (dr == DialogResult.OK)
				txtFilePath.Text = openFileDialog1.FileName;
		}


		private string ipVersion;
		public string IpVersion
		{
			get
			{
				return ipVersion;
			}
			set
			{
				ipVersion = value;
				this.Text = "Import " + ipVersion + " Data";
				this.label1.Text = "IP2Location DB11.LITE " + ipVersion + " CSV:";
			}
		}
		private string csvPath;
		BackgroundWorker bw;
		bool lastImportHadError = false;
		private void btnImportNow_Click(object sender, EventArgs e)
		{
			if (ipVersion != "IPv4" && ipVersion != "IPv6")
			{
				MessageBox.Show("Application error. Invalid ipVersion.");
				return;
			}

			if (!File.Exists(txtFilePath.Text))
			{
				MessageBox.Show("The specified file does not exist!");
				return;
			}

			csvPath = txtFilePath.Text;

			progressBar1.Value = 0;
			lblProgress.Text = "0%";

			btnBrowse.Enabled = false;
			btnImportNow.Enabled = false;
			lastImportHadError = false;

			bw = new BackgroundWorker();
			bw.DoWork += Bw_DoWork;
			bw.ProgressChanged += Bw_ProgressChanged;
			bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
			bw.WorkerReportsProgress = true;
			bw.WorkerSupportsCancellation = true;
			bw.RunWorkerAsync();
		}


		private void Bw_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (ipVersion == "IPv4")
					Program.db.ImportIPv4Data(csvPath, bw);
				else if (ipVersion == "IPv6")
					Program.db.ImportIPv6Data(csvPath, bw);
			}
			catch (Exception ex)
			{
				lastImportHadError = true;
				MessageBox.Show(ex.ToString());
			}
		}

		private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
			lblProgress.Text = (long)e.UserState + " (" + e.ProgressPercentage + "%)";
		}

		private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (lastImportHadError)
			{
				btnBrowse.Enabled = true;
				btnImportNow.Enabled = true;
			}
			else
				this.Text = "Data Import Complete!";
		}

		private void ImportDataForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (bw != null && bw.IsBusy)
			{
				bw.CancelAsync();
				MessageBox.Show("Canceled data import before completion. Please repeat the import later, otherwise the database will be incomplete.");
			}
		}
	}
}
