using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationWebService
{
	public class RGBImage
	{
		public byte[] rgb;
		public int width;
		public int height;
		public int stride;
		public RGBImage(byte[] rgb, int width, int height, int stride)
		{
			this.rgb = rgb;
			this.width = width;
			this.height = height;
			this.stride = stride;
		}

		public void SetPixel(int x, int y, Color color)
		{
			SetPixel(x, y, color.R, color.G, color.B);
		}
		public void SetPixel(int x, int y, byte r, byte g, byte b)
		{
			int offset = GetOffset(x, y);
			rgb[offset] = b;
			rgb[offset + 1] = g;
			rgb[offset + 2] = r;
		}
		public Color GetPixel(int x, int y)
		{
			GetPixel(x, y, out byte r, out byte g, out byte b);
			return Color.FromArgb(r, g, b);
		}
		public void GetPixel(int x, int y, out byte r, out byte g, out byte b)
		{
			int offset = GetOffset(x, y);
			b = rgb[offset];
			g = rgb[offset + 1];
			r = rgb[offset + 2];
		}
		private int GetOffset(int x, int y)
		{
			x = x % width;
			if (x < 0)
				x = width + x;
			y = y % height;
			if (y < 0)
				y = height + y;
			return (x * 3) + (y * stride);
		}

		public RGBImage SubImage(int startX, int startY, int w, int h)
		{
			if (w > width || h > height || (w == width && h == height))
				return this;

			int newStride = (w * 3) + (w % 4);
			RGBImage sub = new RGBImage(new byte[newStride * h], w, h, newStride);

			int endX = startX + w;
			int endY = startY + h;
			for (int srcY = startY, subY = 0; srcY < endY; srcY++, subY++)
			{
				for (int srcX = startX, subX = 0; srcX < endX; srcX++, subX++)
				{
					sub.SetPixel(subX, subY, GetPixel(srcX, srcY));
				}
			}
			return sub;
		}
	}
}
