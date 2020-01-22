using BPUtil;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationWebService.database
{
	public class DB : IDisposable
	{
		private static object syncLock = new object();
		SQLiteConnection db = null;
		/// <summary>
		/// This is meant to be accessed via ServiceWrapper.db.
		/// </summary>
		public DB()
		{
			lock (syncLock)
			{
				FileInfo fiDB = new FileInfo("GWSDB.s3db");

				db = new SQLiteConnection(fiDB.FullName);

				db.CreateTable<IPv4Record>();
				db.CreateTable<IPv6Record>();
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (syncLock)
				{
					db?.Close();
					db?.Dispose();
					db = null;
				}
			}
			if (!disposedValue)
			{

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~DB() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion

		/// <summary>
		/// Returns the IPv4Record or IPv6Record containing the specified address, or null. (null should be impossible if the database is correctly formed and the address is valid)
		/// </summary>
		/// <param name="address">The ipv4 or ipv6 address.</param>
		/// <returns></returns>
		public IPRecord GetIPRecord(string address)
		{
			if (IPAddress.TryParse(address, out IPAddress ip))
				return GetIPRecord(ip);
			return null;
		}

		public IPRecord GetIPRecord(IPAddress ip)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
				return Program.db.GetIPv4Record(ip);
			else if (ip.AddressFamily == AddressFamily.InterNetworkV6)
				return Program.db.GetIPv6Record(ip);
			else
				return null;
		}
		/// <summary>
		/// Returns the IPv4Record containing the specified address, or null. (null should be impossible if the database is correctly formed)
		/// </summary>
		/// <param name="ipv4Address">The ipv4 address.</param>
		/// <returns></returns>
		public IPv4Record GetIPv4Record(IPAddress ipv4Address)
		{
			uint ipInteger = ByteUtil.ReadUInt32(ipv4Address.GetAddressBytes(), 0);
			return GetIPv4Record(ipInteger);
		}

		/// <summary>
		/// Returns the IPv4Record containing the specified address, or null. (null should be impossible if the database is correctly formed)
		/// </summary>
		/// <param name="ipv4Address">The ipv4 address.</param>
		/// <returns></returns>
		public IPv4Record GetIPv4Record(uint ipv4Address)
		{
			lock (syncLock)
			{
				return db.Query<IPv4Record>("SELECT * FROM IPv4Record WHERE ip_to >= ? LIMIT 1", ipv4Address).FirstOrDefault();
			}
		}

		/// <summary>
		/// Returns the IPv6Record containing the specified address, or null. (null should be impossible if the database is correctly formed and the given ipv6 address is valid)
		/// </summary>
		/// <param name="ipv6Address">The ipv6 address.</param>
		/// <returns></returns>
		public IPv6Record GetIPv6Record(IPAddress ip)
		{
			byte[] data = ip.GetAddressBytes().Reverse().ToArray();
			BigInteger ipno = new BigInteger(data);
			lock (syncLock)
			{
				return db.Query<IPv6Record>("SELECT * FROM IPv6Record WHERE ip_to >= '?' LIMIT 1", ipno.ToString().PadLeft(39, '0')).FirstOrDefault();
			}
		}

		public void ImportIPv4Data(string fileName, BackgroundWorker bw)
		{
			ImportIPData(fileName, bw, MakeIPv4Record);
		}
		public void ImportIPv6Data(string fileName, BackgroundWorker bw)
		{
			ImportIPData(fileName, bw, MakeIPv6Record);
		}
		private void ImportIPData<T>(string fileName, BackgroundWorker bw, Func<string[], T> makeRecord)
		{
			lock (syncLock)
			{
				db.DeleteAll<T>();
				using (StreamReader sr = new StreamReader(fileName))
				{
					IEnumerable<string[]> rows = CSVFile.StreamingRead(sr);
					IEnumerable<T> ipRecords = GetIPRecords(rows, bw, makeRecord, sr.BaseStream);
					db.InsertAll(ipRecords);
				}
			}
		}
		private IEnumerable<T> GetIPRecords<T>(IEnumerable<string[]> rows, BackgroundWorker bw, Func<string[], T> makeRecord, Stream stream)
		{
			long recordsAdded = 0;
			int lastProgress = -1;
			double length = (double)stream.Length;
			foreach (string[] row in rows)
			{
				if (bw.CancellationPending)
					yield break;

				if (row.Length != 10)
					throw new Exception("ImportIPData > Row length was " + row.Length + ". Expected 10.");

				yield return makeRecord(row);

				recordsAdded++;

				int progress = (int)((stream.Position / length) * 100);
				if (progress != lastProgress)
					bw.ReportProgress(lastProgress = progress, recordsAdded);
			}
			if (!bw.CancellationPending)
				bw.ReportProgress(100, recordsAdded);
		}
		private IPv4Record MakeIPv4Record(string[] row)
		{
			IPv4Record r = new IPv4Record();
			r.ip_from = uint.Parse(row[0]);
			r.ip_to = uint.Parse(row[1]);
			SetGenericIPRecordFields(r, row);
			return r;
		}
		private IPv6Record MakeIPv6Record(string[] row)
		{
			IPv6Record r = new IPv6Record();
			r.ip_from = row[0].PadLeft(39, '0');
			r.ip_to = row[1].PadLeft(39, '0');
			SetGenericIPRecordFields(r, row);
			return r;
		}
		private void SetGenericIPRecordFields(IPRecord r, string[] row)
		{
			r.country_code = row[2];
			r.country_name = row[3];
			r.region_name = row[4];
			r.city_name = row[5];
			r.latitude = double.Parse(row[6]);
			r.longitude = double.Parse(row[7]);
			r.zip_code = row[8];
			r.time_zone = row[9];
		}
	}
}
