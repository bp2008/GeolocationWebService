using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationWebService.database
{
	public class IPRecord
	{
		//[Unique]
		//[Collation("nocase")]
		[NotNull]
		[MaxLength(2)]
		public string country_code { get; set; }

		[NotNull]
		[MaxLength(64)]
		public string country_name { get; set; }

		[NotNull]
		[MaxLength(128)]
		public string region_name { get; set; }

		[NotNull]
		[MaxLength(128)]
		public string city_name { get; set; }

		[NotNull]
		public double latitude { get; set; }

		[NotNull]
		public double longitude { get; set; }

		[NotNull]
		[MaxLength(30)]
		public string zip_code { get; set; }

		[NotNull]
		[MaxLength(8)]
		public string time_zone { get; set; }
	}
	public class IPv4Record : IPRecord
	{
		[NotNull]
		[Indexed]
		public uint ip_from { get; set; }

		[NotNull]
		[Indexed]
		public uint ip_to { get; set; }
	}
	public class IPv6Record : IPRecord
	{
		[NotNull]
		[Indexed]
		[MaxLength(39)]
		public string ip_from { get; set; }

		[NotNull]
		[Indexed]
		[MaxLength(39)]
		public string ip_to { get; set; }
	}
}
