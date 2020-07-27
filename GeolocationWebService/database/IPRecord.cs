using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

		public IPRecord() { }
		public IPRecord(IPRecord copyFrom)
		{
			if (copyFrom != null)
			{
				country_code = copyFrom.country_code;
				country_name = copyFrom.country_name;
				region_name = copyFrom.region_name;
				city_name = copyFrom.city_name;
				latitude = copyFrom.latitude;
				longitude = copyFrom.longitude;
				zip_code = copyFrom.zip_code;
				time_zone = copyFrom.time_zone;
			}
		}
	}
	public class IPv4Record : IPRecord
	{
		[Ignore]
		public bool ipv4 { get; } = true;

		[NotNull]
		public uint ip_from { get; set; }

		[NotNull]
		[Indexed]
		public uint ip_to { get; set; }

		public IPv4Record() { }
		public IPv4Record(IPRecord copyFrom) : base(copyFrom)
		{
			if (copyFrom is IPv4Record)
			{
				IPv4Record v4 = copyFrom as IPv4Record;
				ip_from = v4.ip_from;
				ip_to = v4.ip_to;
			}
			else if (copyFrom is IPv6Record)
			{
				IPv6Record v6 = copyFrom as IPv6Record;
				if (BigInteger.TryParse(v6.ip_from, out BigInteger from) && BigInteger.TryParse(v6.ip_to, out BigInteger to))
				{
					ip_from = (uint)(from & 0xFFFFFFFF);
					ip_to = (uint)(to & 0xFFFFFFFF);
				}
			}
		}
	}
	public class IPv6Record : IPRecord
	{
		[Ignore]
		public bool ipv6 { get; } = true;

		[NotNull]
		[MaxLength(39)]
		public string ip_from { get; set; }

		[NotNull]
		[Indexed]
		[MaxLength(39)]
		public string ip_to { get; set; }

		public IPv6Record() { }
		public IPv6Record(IPRecord copyFrom) : base(copyFrom)
		{
			if (copyFrom is IPv6Record)
			{
				IPv6Record v6 = copyFrom as IPv6Record;
				ip_from = v6.ip_from;
				ip_to = v6.ip_to;
			}
			else if (copyFrom is IPv4Record)
			{
				IPv4Record v4 = copyFrom as IPv4Record;
				ip_from = (((ulong)v4.ip_from) | 0xFFFF00000000).ToString();
				ip_to = (((ulong)v4.ip_to) | 0xFFFF00000000).ToString();
			}
		}
	}
}
