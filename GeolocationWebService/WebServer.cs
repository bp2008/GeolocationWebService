using BPUtil.SimpleHttp;
using GeolocationWebService.database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeolocationWebService
{
	public class WebServer : HttpServer
	{
		private static Regex rxMapURL = new Regex("map/(\\d\\d?)/(.+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private static Regex rxMapDebugURL = new Regex("mapdebug/(\\d\\d?)/([0-9.-]+)/([0-9.-]+)(?:\\.jpg)?", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		public WebServer(int port, int httpsPort) : base(port, httpsPort)
		{
		}


		public override void handleGETRequest(HttpProcessor p)
		{
			try
			{
				if (p.requestedPage == "")
				{
					p.writeSuccess();
					p.outputStream.WriteLine("<html>");
					p.outputStream.WriteLine("<head>");
					p.outputStream.WriteLine("<title>Geolocation Web Service</title>");
					p.outputStream.WriteLine("</head>");
					p.outputStream.WriteLine("<body>");
					p.outputStream.WriteLine("<h1>Geolocation Web Service " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "</h1>");
					p.outputStream.WriteLine("<h2>Examples</h2>");
					p.outputStream.WriteLine("<h3>HTML Page</h3>");
					p.outputStream.WriteLine("<p><a href=\"whois/8.8.8.8\">whois/8.8.8.8</a></p>");
					p.outputStream.WriteLine("<h3>HTML Embed</h3>");
					p.outputStream.WriteLine("<p><a href=\"embed/8.8.8.8\">embed/8.8.8.8</a></p>");
					p.outputStream.WriteLine("<h3>JSON Record</h3>");
					p.outputStream.WriteLine("<p><a href=\"ip/8.8.8.8\">ip/8.8.8.8</a> (or <a href=\"ip/8.8.8.8.json\">ip/8.8.8.8.json</a>)</p>");
					p.outputStream.WriteLine("<h3>Location on Map</h3>");
					p.outputStream.WriteLine("<p><a href=\"map/0/8.8.8.8\">map/0/8.8.8.8</a> (or <a href=\"map/0/8.8.8.8.jpg\">map/0/8.8.8.8.jpg</a>)</p>");
					p.outputStream.WriteLine("<p><a href=\"map/1/8.8.8.8.jpg\">map/1/8.8.8.8.jpg</a></p>");
					p.outputStream.WriteLine("<p><a href=\"map/2/8.8.8.8.jpg\">map/2/8.8.8.8.jpg</a></p>");
					p.outputStream.WriteLine("<p><a href=\"map/3/8.8.8.8.jpg\">map/3/8.8.8.8.jpg</a></p>");
					p.outputStream.WriteLine("<p><a href=\"map/4/8.8.8.8.jpg\">map/4/8.8.8.8.jpg</a></p>");
					p.outputStream.WriteLine("<p><a href=\"map/5/8.8.8.8.jpg\">map/5/8.8.8.8.jpg</a></p>");
					p.outputStream.WriteLine("<h2>Licensing and Attribution</h2>");
					p.outputStream.WriteLine("<p>Wherever data or maps from this service are used, it is required that you comply with the licensing and attribution requirements of the organizations listed below:</p>");
					p.outputStream.WriteLine("<h3>IP2Location</h3>");
					p.outputStream.WriteLine("<p>This site or product includes IP2Location LITE data available from <a href=\"http://www.ip2location.com\">http://www.ip2location.com</a>.</p>");
					p.outputStream.WriteLine("<h3>OpenStreetMap</h3>");
					p.outputStream.WriteLine("<p>Map data is <a href=\"https://www.openstreetmap.org/copyright\">© OpenStreetMap contributors and cartography is licensed as CC BY-SA.</a></p>");
					p.outputStream.WriteLine("</body>");
					p.outputStream.WriteLine("</html>");
				}
				else if (p.requestedPage.StartsWith("whois/"))
				{
					string input = p.requestedPage.Substring("whois/".Length);
					if (input.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
						input = input.Remove(input.Length - ".json".Length);
					IPRecord record = Program.db.GetIPRecord(input);
					if (record == null)
						p.writeFailure();
					else
					{
						p.writeSuccess(additionalHeaders: CoordsHeader(record));
						p.outputStream.WriteLine("<html>");
						p.outputStream.WriteLine("<head>");
						p.outputStream.WriteLine("<title>Geolocation Web Service</title>");
						p.outputStream.WriteLine("<style type=\"text/css\">");
						p.outputStream.WriteLine("table { border-collapse: collapse; }");
						p.outputStream.WriteLine("td { border: 1px solid black; padding: 2px 5px; }");
						p.outputStream.WriteLine("</style>");
						p.outputStream.WriteLine("</head>");
						p.outputStream.WriteLine("<body>");
						p.outputStream.WriteLine("<h1>Geolocation Information For " + input + "</h1>");
						p.outputStream.WriteLine("<p>");
						p.outputStream.WriteLine("<table>");
						p.outputStream.WriteLine("<tbody>");
						p.outputStream.WriteLine("<tr><td>Latitude</td><td>" + record.latitude + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Longitude</td><td>" + record.longitude + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Country</td><td>" + record.country_code + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Country Name</td><td>" + record.country_name + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Region</td><td>" + record.region_name + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>City</td><td>" + record.city_name + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Zip Code</td><td>" + record.zip_code + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Time Zone</td><td>" + record.time_zone + "</td></tr>");
						p.outputStream.WriteLine("</tbody>");
						p.outputStream.WriteLine("</table>");
						p.outputStream.WriteLine("</p>");
						p.outputStream.WriteLine("<p>");
						p.outputStream.WriteLine("<img src=\"../map/1/" + input + ".jpg\" alt=\"map\"/>");
						p.outputStream.WriteLine("</p>");
						p.outputStream.WriteLine("<p>This site or product includes IP2Location LITE data available from <a href=\"http://www.ip2location.com\">http://www.ip2location.com</a>.</p>");
						p.outputStream.WriteLine("<p>Map data is <a href=\"https://www.openstreetmap.org/copyright\">© OpenStreetMap contributors and cartography is licensed as CC BY-SA.</a></p>");
						p.outputStream.WriteLine("</body>");
						p.outputStream.WriteLine("</html>");
					}
				}
				else if (p.requestedPage.StartsWith("embed/"))
				{
					string input = p.requestedPage.Substring("embed/".Length);
					IPRecord record = Program.db.GetIPRecord(input);
					if (record == null)
						p.writeFailure();
					else
					{
						LocationMap map = LocationMapper.GetMap(record, 1, 256, 256);
						p.writeSuccess(additionalHeaders: CoordsHeader(record));
						p.outputStream.WriteLine("<style type=\"text/css\">");
						p.outputStream.WriteLine(".gwsembed { font-family: sans-serif; }");
						p.outputStream.WriteLine(".gwsembed .heading { font-size: 1.3em; font-weight: bold; margin: 3px 0px 8px 0px }");
						p.outputStream.WriteLine(".gwsembed .data { display: inline-block; vertical-align: top; margin-right: 5px; margin-bottom: 5px; font-family: consolas, monospace; }");
						p.outputStream.WriteLine(".gwsembed .physical_address { white-space: pre-wrap; }");
						p.outputStream.WriteLine(".gwsembed table { border-collapse: collapse; margin-top: 5px;}");
						p.outputStream.WriteLine(".gwsembed td { border: 1px solid black; padding: 2px 5px; }");
						p.outputStream.WriteLine(".gwsembed .img { width: 256px; height: 256px; display: inline-block; vertical-align: top; }");
						p.outputStream.WriteLine(".gwsembed .body > div, .gwsembed .body > p { margin: 5px 0px; }");
						p.outputStream.WriteLine("</style>");
						p.outputStream.WriteLine("<div class=\"gwsembed\">");
						if (!p.GetBoolParam("hideTitle"))
							p.outputStream.WriteLine("<div class=\"heading\">Geolocation for " + input + "</div>");
						p.outputStream.WriteLine("<div class=\"body\">");
						p.outputStream.WriteLine("<div class=\"info\">");
						p.outputStream.WriteLine("<div class=\"data\">");
						p.outputStream.Write("<div class=\"physical_address\">");
						p.outputStream.WriteLine(record.city_name + ", " + record.region_name + "  " + record.zip_code);
						p.outputStream.Write(record.country_code + " (" + record.country_name + ")");
						p.outputStream.WriteLine("</div>"); // end physical_address
						p.outputStream.WriteLine("<table>");
						p.outputStream.WriteLine("<tbody>");
						p.outputStream.WriteLine("<tr><td>Latitude</td><td>" + record.latitude + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Longitude</td><td>" + record.longitude + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>City</td><td>" + record.city_name + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Region</td><td>" + record.region_name + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Country</td><td>" + record.country_code + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Country Name</td><td>" + record.country_name + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Zip Code</td><td>" + record.zip_code + "</td></tr>");
						p.outputStream.WriteLine("<tr><td>Time Zone</td><td>" + record.time_zone + "</td></tr>");
						p.outputStream.WriteLine("</tbody>");
						p.outputStream.WriteLine("</table>");
						p.outputStream.WriteLine("</div>"); // end data
						p.outputStream.WriteLine("<img src=\"" + map.ToDataUri() + "\" alt=\"map\"/>");
						p.outputStream.WriteLine("</div>"); // end info
						p.outputStream.WriteLine("<p>This information includes IP2Location LITE data available from <a href=\"http://www.ip2location.com\">http://www.ip2location.com</a>.</p>");
						p.outputStream.WriteLine("<p>Map data is <a href=\"https://www.openstreetmap.org/copyright\">© OpenStreetMap contributors and cartography is licensed as CC BY-SA.</a></p>");
						p.outputStream.WriteLine("</div>"); // end body
						p.outputStream.WriteLine("</div>"); // end gwsembed
					}
				}
				else if (p.requestedPage.StartsWith("ip/"))
				{
					string input = p.requestedPage.Substring("ip/".Length);
					if (input.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
						input = input.Remove(input.Length - ".json".Length);
					IPRecord record = Program.db.GetIPRecord(input);
					if (record == null)
						p.writeFailure();
					else
					{
						p.writeSuccess("application/json");
						p.outputStream.Write(JsonConvert.SerializeObject(record));
					}
				}
				else
				{
					string page = p.requestedPage;
					if (page.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
						page = page.Remove(page.Length - ".jpg".Length);
					Match m = rxMapURL.Match(page);
					if (m.Success) // map/zoom/ip
					{
						int.TryParse(m.Groups[1].Value, out int zoomLevel);
						string input = m.Groups[2].Value;
						IPRecord record = Program.db.GetIPRecord(input);
						if (record == null)
							p.writeFailure();
						else
						{
							LocationMap map = LocationMapper.GetMap(record, zoomLevel, 256, 256);
							p.writeSuccess(map.mimeType, map.imgData.Length, additionalHeaders: CoordsHeader(record));
							p.outputStream.Flush();
							p.tcpStream.Write(map.imgData, 0, map.imgData.Length);
						}
					}
					else
					{
						m = rxMapDebugURL.Match(page);
						if (m.Success) // mapdebug/zoom/lat/lon
						{
							int.TryParse(m.Groups[1].Value, out int zoomLevel);
							double.TryParse(m.Groups[2].Value, out double lat);
							double.TryParse(m.Groups[3].Value, out double lon);

							IPRecord record = new IPv4Record();
							record.latitude = lat;
							record.longitude = lon;

							LocationMap map = LocationMapper.GetMap(record, zoomLevel, 256, 256);
							p.writeSuccess(map.mimeType, map.imgData.Length, additionalHeaders: CoordsHeader(record));
							p.outputStream.Flush();
							p.tcpStream.Write(map.imgData, 0, map.imgData.Length);
						}
					}
				}
			}
			catch (Exception ex)
			{
				p.writeFailure("500 Internal Server Error", ex.ToString());
			}
		}
		private List<KeyValuePair<string, string>> CoordsHeader(IPRecord record)
		{
			return new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("Coords", record.latitude + "," + record.longitude) };
		}

		public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
		{
		}

		protected override void stopServer()
		{
		}
	}
}
