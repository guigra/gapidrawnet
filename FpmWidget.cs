using System;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for FpmWidget.
	/// </summary>
	public class FpmWidget : TextWidget
	{
		public FpmWidget(IGapiForm form) : base(form)
		{

		}

		private double oldFrameTime = -1.0;
		private double oldFrameRate = -1.0;
		public override void Draw(GapiSurface surface)
		{
			if(GapiApplication.Instance.Timer == null)
			{
				return;
			}

			double frameTime = (GapiApplication.Instance.Timer.GetActualFrameTimeMS());
			//string frameTimeString = "Frame Time (ms) " + frameTime.ToString("{##.000}");
			//DrawFrameInfo(BackBuffer, dwScreenHeight - 40, frameTimeString, GapiUtility.RGB(170, 90, 60)); 

			double frameRate = (GapiApplication.Instance.Timer.GetActualFrameRate());
			//string frameRateString = "Frame Rate " + frameRate.ToString("{##.000}");

			if(oldFrameTime != frameTime || oldFrameRate != frameRate)
			{
				oldFrameTime = frameTime;
				oldFrameRate = frameRate;
				Text = string.Format("FPS {0:##.00}, {1:##.00} mS", frameRate, frameTime);
			}
			base.Draw (surface);
		}

	}
}
