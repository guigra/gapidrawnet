using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using GapiDrawNet;

namespace Stylus2002
{
	/// <summary>
	/// Copy this to use as a starting point
	/// </summary>
	public class CopyApplication : GapiApplication
	{
		public CopyApplication()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		override public void Dispose()
		{
			base.Dispose();
		}

		override protected void CreateSurfaces(GapiDisplay gapiDisplay)
		{
		}

		override protected void ProcessNextFrame(bool frameOverflow)
		{
			gapiDisplay.BackBuffer.DrawText(gapiDisplay.Width / 2,gapiDisplay.Height / 2, "Press any key to exit", gapiDisplay.GetSystemFontPtr(), DrawTextOptions.GDDRAWTEXT_CENTER);
		}

		override protected void KeyDown(System.Windows.Forms.KeyEventArgs e)
		{
		}

		override protected void KeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			Shutdown();
		}

		override protected void StylusDown(Point p, System.Windows.Forms.MouseEventArgs e)
		{
			
		}

		override protected void StylusUp(Point p, System.Windows.Forms.MouseEventArgs e)
		{
			
		}

		override protected void StylusMove(Point p, System.Windows.Forms.MouseEventArgs e)
		{
			
		}
	}
}
