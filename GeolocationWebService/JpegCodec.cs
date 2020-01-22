﻿using BPUtil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GeolocationWebService
{
	public static class JpegCodec
	{
		public static byte[] Decode(byte[] compressed, out int width, out int height)
		{
			using (MemoryStream ms = new MemoryStream(compressed))
			{
				using (Bitmap bmp = (Bitmap)Bitmap.FromStream(ms))
				{
					width = bmp.Width;
					height = bmp.Height;
					BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
					byte[] data = new byte[Math.Abs(bitmapData.Stride * bitmapData.Height)];
					Marshal.Copy(bitmapData.Scan0, data, 0, data.Length);
					bmp.UnlockBits(bitmapData);
					return data;
				}
			}
		}

		public static byte[] Encode(byte[] rgb, int width, int height, int quality)
		{
			using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
			{
				BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
				Marshal.Copy(rgb, 0, bitmapData.Scan0, rgb.Length);
				bmp.UnlockBits(bitmapData);
				using (MemoryStream ms = new MemoryStream())
				{
					ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
					EncoderParameters parameters = new EncoderParameters(1);
					parameters.Param[0] = new EncoderParameter(Encoder.Quality, BPMath.Clamp(quality, 1, 100));
					bmp.Save(ms, jpgEncoder, parameters);
					return ms.ToArray();
				}
			}
		}
		private static ImageCodecInfo GetEncoder(ImageFormat format)
		{
			return ImageCodecInfo.GetImageDecoders().FirstOrDefault(d => d.FormatID == format.Guid);
		}
	}
}
