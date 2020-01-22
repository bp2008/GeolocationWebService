using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationWebService
{
	partial class GeolocationService : ServiceBase
	{
		private WebServer webServer;

		public GeolocationService()
		{
			InitializeComponent();
		}


		public void DoStart()
		{
			OnStart(null);
		}

		public void DoStop()
		{
			OnStop();
		}
		protected override void OnStart(string[] args)
		{
			webServer?.Stop();
			webServer = new WebServer(Program.settings.http_port, Program.settings.https_port);
			webServer.Start();
		}

		protected override void OnStop()
		{
			webServer?.Stop();
			webServer = null;
		}
	}
}
