using BPUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationWebService
{
	public class Settings : SerializableObjectBase
	{
		public int http_port = 52280;
		public int https_port = -1;
		public bool resolveIPv4WithIPv6Table = true;
	}
}
