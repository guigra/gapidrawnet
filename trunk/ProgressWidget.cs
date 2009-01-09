using System;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for ProgressWidget.
	/// </summary>
	public class ProgressWidget : GapiWidget
	{
		public ProgressWidget(IGapiForm form) : base(form)
		{
			//
			// TODO: Add constructor logic here
			//
		}
	
		public int Progress = 0;

		public override void Draw(GapiSurface surface)
		{
			if(Visible == false){ return; }

			base.Draw(surface);

			GDRect progressRect = Bounds;

			if(Appearance.ShowBorder)
			{
				progressRect.Inflate(-2);
			}

			int newWidth = progressRect.Width * Progress / 100;
			progressRect.Right = progressRect.Left + newWidth;

			if(newWidth > 0)
			{
				surface.FillRect(progressRect, Appearance.ForeColor);	
			}
		}

	}
}
