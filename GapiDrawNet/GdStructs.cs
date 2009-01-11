using System;
using System.Drawing;

namespace System.Drawing
{
    public static class ColorExtensions
    {
        public static uint ToColorRef(this Color c)
        {
            return (uint)(c.R + (c.G << 8) + (c.B << 16));
        }

        public static Color ToColor(this int c)
        {
            return Color.FromArgb(c & 0x000000FF, (c & 0x0000FF00) >> 8, (c & 0x00FF0000) >> 16);
        }

        public static Color WithAlpha(this Color c, int alpha)
        {
            return Color.FromArgb((int)((uint)(c.ToArgb() & 0x00FFFFFF) + (uint)(((byte)alpha) << 24)));
        }

        public static Color Opaque(this Color c)
        {
            return WithAlpha(c, 255);
        }
    }
}

namespace GapiDrawNet
{
	public struct GDRect
	{
		public GDRect(int left, int top, int right, int bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}
		
        public int Left;
		public int Top;
		public int Right;
		public int Bottom;

        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = Top + value; }
        }

        public int Width
        {
            get { return Right - Left; }
            set { Right = Left + value; }
        }

		public bool Contains(Point p)
		{
			return (p.X >= Left) && (p.X <= Right) && (p.Y >= Top) && (p.Y <= Bottom);
		}
		public bool Contains(int x, int y)
		{
			return (x >= Left) && (x <= Right) && (y >= Top) && (y <= Bottom);
		}

		public void Inflate(int inflateBy)
		{
			Left-= inflateBy;
			Top -= inflateBy;
			Right += inflateBy;
			Bottom += inflateBy;
		}

        public static GDRect FromRectangle(Rectangle rect)
        {
            return new GDRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static implicit operator Rectangle(GDRect rect)
        {
            return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

        public static implicit operator GDRect(Rectangle rect)
        {
            return new GDRect(rect.X, rect.Y, rect.Right, rect.Bottom);
        }
	}
}