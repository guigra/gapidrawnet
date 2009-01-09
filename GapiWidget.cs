using System;
using System.Drawing;

using System.Collections;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for GapiWidget.
	/// </summary>
	public class GapiWidget : IDisposable
	{
		protected IGapiForm Form;

		public GapiWidget(IGapiForm form)
		{
			Form = form;
		}

		public bool Visible = true;
		public WidgetAppearance Appearance;

		public virtual void Draw(GapiSurface surface)
		{
			if(Appearance.ShowBackColor)
			{
				if(Appearance.BlendBackColor)
				{
					surface.FillRect(Bounds, Appearance.BackColor, 128);
				}
				else
				{
					surface.FillRect(Bounds, Appearance.BackColor);
				}
			}

			if(Appearance.ShowBorder)
			{
				surface.DrawRect(ref Bounds, Appearance.BorderColor);
			}

		}

		public GDRect Bounds = new GDRect(0,0,0,0);

		public virtual void CreateSurfaces(GapiDisplay gapiDisplay)
		{

		}

		public void CentreX(int x, int width)
		{
			Bounds.Left = x - (width >> 1);
			Bounds.Right = x + (width >> 1);
		}

		public void CentreY(int y, int height)
		{
			Bounds.Top = y - (height >> 1);
			Bounds.Bottom = y + (height >> 1);
		}

		public virtual void StylusDown(Point p, System.Windows.Forms.MouseEventArgs e)
		{
		}

		#region IDisposable Members

		public void Dispose()
		{
			// TODO:  Add GapiWidget.Dispose implementation
		}

		#endregion
	}

	public class WidgetList : ArrayList
	{
		public WidgetList():base()
		{
		}

		public int Add(GapiWidget value)
		{
			return base.Add (value);
		}

		public new GapiWidget this[int index]
		{
			get { return (GapiWidget)base[index]; }
		}
	}

	public struct WidgetAppearance
	{
		public int  BackColor;
		public bool BlendBackColor;
		public bool ShowBackColor;
		public int  ForeColor;
		public int  BorderColor;
		public bool ShowBorder;
	}
}
