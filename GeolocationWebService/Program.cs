using BPUtil;
using BPUtil.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using GeolocationWebService.database;
using System.Windows.Forms;
using System.Diagnostics;

namespace GeolocationWebService
{
	class Program
	{
		public static Settings settings;
		public static DB db;
		private static GeolocationService debugService;

		[STAThread]
		static void Main(string[] args)
		{
			string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			Globals.Initialize(exePath);
			PrivateAccessor.SetStaticFieldValue(typeof(Globals), "errorFilePath", Globals.WritableDirectoryBase + "GWS_Log.txt");

			FileInfo fiExe = new FileInfo(exePath);
			Environment.CurrentDirectory = fiExe.Directory.FullName;

			settings = new Settings();
			settings.Load();
			settings.SaveIfNoExist();

			db = new DB();

			if (Environment.UserInteractive)
			{
				string Title = "GeolocationWebService " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " Service Manager";
				string ServiceName = "GeolocationWebService";
				ButtonDefinition[] additionalButtons = new ButtonDefinition[] {
					new ButtonDefinition("Import IPv4 Geolocation Data", btnImportIPv4GeolocationData_Click),
					new ButtonDefinition("Import IPv6 Geolocation Data", btnImportIPv6GeolocationData_Click),
					new ButtonDefinition("Open Web Interface", btnOpenWebInterface_Click)
				};

				if (Debugger.IsAttached)
				{
					debugService = new GeolocationService();
					debugService.DoStart();
				}

				System.Windows.Forms.Application.Run(new ServiceManager(Title, ServiceName, additionalButtons));

				debugService?.DoStop();
			}
			else
			{
				GeolocationService[] ServicesToRun;
				ServicesToRun = new GeolocationService[]
				{
					new GeolocationService()
				};
				ServiceBase.Run(ServicesToRun);
			}
		}

		private static void btnImportIPv4GeolocationData_Click(object sender, EventArgs e)
		{
			ImportDataForm idf = new ImportDataForm();
			idf.IpVersion = "IPv4";
			idf.Show(((Control)sender).Parent);
		}
		private static void btnImportIPv6GeolocationData_Click(object sender, EventArgs e)
		{
			ImportDataForm idf = new ImportDataForm();
			idf.IpVersion = "IPv6";
			idf.Show(((Control)sender).Parent);
		}

		private static void btnOpenWebInterface_Click(object sender, EventArgs e)
		{
			if (settings.https_port > 0)
				Process.Start("https://127.0.0.1" + (settings.https_port == 443 ? "" : ":" + settings.https_port) + "/");
			else if (settings.http_port > 0)
				Process.Start("http://127.0.0.1" + (settings.http_port == 80 ? "" : ":" + settings.http_port) + "/");
			else
				MessageBox.Show("Neither server port is configured.");
		}
	}
}
