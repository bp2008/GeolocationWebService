using BPUtil;
using BPUtil.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeolocationWebService
{
	public partial class SetupForm : SelfCenteredForm
	{
		public SetupForm()
		{
			InitializeComponent();
			openFileDialog1.InitialDirectory = Globals.ApplicationDirectoryBase;
			LoadFromSettings();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			DialogResult dr = openFileDialog1.ShowDialog();
			if (dr == DialogResult.OK)
				txtFilePath.Text = openFileDialog1.FileName;
		}

		private string csvPath;
		BackgroundWorker bw;
		bool lastImportHadError = false;
		private void btnImportNow_Click(object sender, EventArgs e)
		{
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
				Program.db.ImportData(csvPath, bw);
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

		private void linkToDataSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(linkToDataSite.Text);
		}

		private void cbResolveIPv4WithIPv6Table_CheckedChanged(object sender, EventArgs e)
		{
			SaveSettings();
		}

		private void LoadFromSettings()
		{
			cbResolveIPv4WithIPv6Table.Checked = Program.settings.resolveIPv4WithIPv6Table;
		}

		private void SaveSettings()
		{
			Program.settings.resolveIPv4WithIPv6Table = cbResolveIPv4WithIPv6Table.Checked;
			Program.settings.Save();
		}
	}
}
