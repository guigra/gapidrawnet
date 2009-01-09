using System;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for TextWidget.
	/// </summary>
	public class TextWidget : GapiWidget
	{
		public TextWidget(IGapiForm form) : base(form)
		{

		}

		public TextWidget(IGapiForm form, string text, DrawTextOptions options) : base(form)
		{
			Text = text;
			Options = options;
		}

		private int X = 0;
		private int Y = 0;

		private void SetXY()
		{
			if((int)DrawTextOptions.GDDRAWTEXT_LEFT == (0xF &  (int)Options))
			{
				X = Bounds.Left;
			}
			else if((int)DrawTextOptions.GDDRAWTEXT_RIGHT == (0xF &  (int)Options))
			{
				X = Bounds.Right;
			}
			else
			{
				X = (Bounds.Left + Bounds.Right) >> 1;
			}

			int fontHeight;
			if(Font == null)
			{
				fontHeight = 8;
			}
			else
			{
				fontHeight = Font.Height;
			}

			
			Y = Bounds.Top + 1 + ((Bounds.Height - fontHeight) >> 1);
		}


		public DrawTextOptions Options = DrawTextOptions.GDDRAWTEXT_LEFT;

		public GapiBitmapFont Font = null;
		public string Text = "";

		public override void Draw(GapiSurface surface)
		{
			

			base.Draw(surface);

			SetXY();

			if(Text != "")
			{
				if(Font == null)
				{					
					surface.DrawText(X, Y, Text, GapiDisplay.SystemFontPtr, Options);
				}
				else
				{
					surface.DrawText(X, Y, Text, Font, Options);
				}
			}
				
		}
	}
}
