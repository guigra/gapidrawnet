using System;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for GapiBitmapFont.
	/// </summary>
	public class GapiBitmapFont : GapiSurface
	{
//		[DllImport("GdNet.dll")]
//		private static extern IntPtr CGapiBitmapFont_Create();
//		// private static extern int CGdApplication_Create(ref IntPtr pApp, IntPtr hInst);
//
//		[DllImport("GdNet.dll")]
//		private static extern int  CGapiBitmapFont_Destroy(IntPtr pApp);

		public GapiBitmapFont()
            : base(GdApi.CGapiBitmapFont_Create(), true)
		{
		}

        internal GapiBitmapFont(IntPtr handle, bool ownsHandle)
            : base(handle, ownsHandle) { }

        protected override void DestroyGapiObject(IntPtr gapiObject)
        {
            CheckResult(GdApi.CGapiBitmapFont_Destroy(Handle));
        }

		// public static extern UInt32 CGapiBitmapFont_CreateFont (IntPtr pBitmapFont, int dwFlags, ref GDFONTFX pGDFontFx);
		public void CreateFont(CreateFontOptions dwFlags, GDFONTFX gdFontFx)
		{
			GapiErrorHelper.RaiseExceptionOnError (GdApi.CGapiBitmapFont_CreateFont_NoString(Handle, 0, GetPixel(0, 0), (int)dwFlags, ref gdFontFx));
		}

		public void CreateFont(string pString, int dwColorkey, CreateFontOptions dwFlags, GDFONTFX gdFontFx)
		{
			GapiErrorHelper.RaiseExceptionOnError (GdApi.CGapiBitmapFont_CreateFont(Handle, Str(pString), dwColorkey, (int)dwFlags, ref gdFontFx));
		}
		public void CreateFont(string bitmapFilename)
		{
			CreateFontAdvanced(bitmapFilename);
		}

		public void CreateFontSimple(string bitmapFilename)
		{
			//			Call CGapiBitmapFont::CreateSurface and supply a valid font bitmap. 
			CreateSurface(bitmapFilename);
			//			Call CGapiBitmapFont::SetColorKey to specify the color key of your font bitmap. 
			SetColorKeyFromBottomLeftCorner();
			//			Call CGapiBitmapFont::CreateFont to calculate the font offsets and widths. 
			GDFONTFX gdFontFx;
			gdFontFx.lTracking = 0;
			CreateFont(CreateFontOptions.GDCREATEFONT_SIMPLEBITMAP, gdFontFx);
		}

		public void CreateFontAdvanced(string bitmapFilename)
		{
			//			Call CGapiBitmapFont::CreateSurface and supply a valid font bitmap. 
			CreateSurface(bitmapFilename);
			//			Call CGapiBitmapFont::SetColorKey to specify the color key of your font bitmap. 
			// get from bottom left corner
			SetColorKeyFromBottomLeftCorner();

			//			Call CGapiBitmapFont::CreateFont to calculate the font offsets and widths. 
			GDFONTFX gdFontFx;
			gdFontFx.lTracking = 0;
			CreateFont(0, gdFontFx);
		}

		// public static extern UInt32 CGapiBitmapFont_SetKerning (IntPtr pBitmapFont, char tcPreviousChar, char tcCharToAdjust, int lOffset);
		public void SetKerning(char tcPreviousChar, char tcCharToAdjust, int lOffset)
		{
			GapiErrorHelper.RaiseExceptionOnError (GdApi.CGapiBitmapFont_SetKerning(Handle, tcPreviousChar, tcCharToAdjust, lOffset));
		}

		public int GetTextWidth(string drawString)
		{
			int result;

			GdApi.CGapiBitmapFont_GetStringWidth (Handle, Str(drawString), out result);
			return result;
		}

		public int GetCharWidth(char tcChar)
		{
			// public static extern UInt32 CGapiBitmapFont_GetCharWidth (IntPtr pBitmapFont, char tcChar, out int pWidth);
			int result;

			GdApi.CGapiBitmapFont_GetCharWidth (Handle, tcChar, out result);
			return result;
		}

		public int GetSpacing (char tcChar1, char tcChar2)
		{
			// public static extern UInt32 CGapiBitmapFont_GetSpacing (IntPtr pBitmapFont, char tcChar1, char tcChar2, out int pWidth);
			int result;

			GdApi.CGapiBitmapFont_GetSpacing (Handle, tcChar1, tcChar2, out result);
			return result;
		}
	}
}
