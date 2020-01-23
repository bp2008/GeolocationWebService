using GeolocationWebService.database;
using Microsoft.MapPoint;
using System;
using System.Drawing;
using System.IO;

namespace GeolocationWebService
{
	public class LocationMap
	{
		public byte[] imgData;
		public string mimeType;

		public LocationMap(byte[] imgData, string mimeType)
		{
			this.imgData = imgData;
			this.mimeType = mimeType;
		}
		public string ToDataUri()
		{
			return "data:" + mimeType + ";base64," + Convert.ToBase64String(imgData);
		}
	}
	public static class LocationMapper
	{
		private static object largeMapLock = new object();
		public static LocationMap GetMap(IPRecord record, int zoomLevel, int desiredWidth, int desiredHeight)
		{
			string mapFile = null;
			if (zoomLevel == 0)
				mapFile = "OSM256.jpg";
			else if (zoomLevel == 1)
				mapFile = "OSM512.jpg";
			else if (zoomLevel == 2)
				mapFile = "OSM1024.jpg";
			else if (zoomLevel == 3)
				mapFile = "OSM2048.jpg";
			else if (zoomLevel == 4)
				mapFile = "OSM4096.jpg";
			else
			{
				mapFile = "OSM8192.jpg";
				zoomLevel = 5;
			}

			byte[] jpegData;
			if (zoomLevel < 3)
				jpegData = CreateMapSynchronized(record, zoomLevel, desiredWidth, desiredHeight, mapFile);
			else
				lock (largeMapLock) // Only work on one large map at a time to prevent excessive memory usage.
				{
					jpegData = CreateMapSynchronized(record, zoomLevel, desiredWidth, desiredHeight, mapFile);
				}

			return new LocationMap(jpegData, "image/jpeg");
		}

		private static byte[] CreateMapSynchronized(IPRecord record, int zoomLevel, int desiredWidth, int desiredHeight, string mapFile)
		{
			byte[] sourceMapCompressed = File.ReadAllBytes("maps/" + mapFile);
			byte[] rgb = JpegCodec.Decode(sourceMapCompressed, out int w, out int h);

			RGBImage image = new RGBImage(rgb, w, h, rgb.Length / h);
			// Mark the image at the location of the IPRecord
			TileSystem.LatLongToPixelXY(record.latitude, record.longitude, zoomLevel, out int pixelX, out int pixelY);
			Color color = Color.Red;
			image.SetPixel(pixelX, pixelY, color);
			for (int i = 1; i < 8; i++)
			{
				image.SetPixel(pixelX + i, pixelY, color);
				image.SetPixel(pixelX - i, pixelY, color);
				image.SetPixel(pixelX, pixelY + i, color);
				image.SetPixel(pixelX, pixelY - i, color);
			}

			// Crop to desired size
			image = image.SubImage(pixelX - (desiredWidth / 2), pixelY - (desiredHeight / 2), desiredWidth, desiredHeight);

			byte[] jpegData = JpegCodec.Encode(image.rgb, image.width, image.height, 90);
			return jpegData;
		}
	}
}