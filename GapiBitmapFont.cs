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
		{
			unmanagedGapiObject = GdNet.CGapiBitmapFont_Create();
		}

		override public void Dispose()
		{
			GdNet.CGapiBitmapFont_Destroy(unmanagedGapiObject);
		}

		// public static extern UInt32 CGapiBitmapFont_CreateFont (IntPtr pBitmapFont, int dwFlags, ref GDFONTFX pGDFontFx);
		public void CreateFont(CreateFontOptions dwFlags, GDFONTFX gdFontFx)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiBitmapFont_CreateFont_NoString(unmanagedGapiObject, 0, GetPixel(0, 0), (int)dwFlags, ref gdFontFx));
		}

		public void CreateFont(string pString, int dwColorkey, CreateFontOptions dwFlags, GDFONTFX gdFontFx)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiBitmapFont_CreateFont(unmanagedGapiObject, pString, dwColorkey, (int)dwFlags, ref gdFontFx));
		}
		public void CreateFont(string bitmapFilename)
		{
			CreateFontAdvanced(bitmapFilename);
		}

		public void CreateFontSimple(string bitmapFilename)
		{
			//			Call CGapiBitmapFont::CreateSurface and supply a valid font bitmap. 
			CreateSurface(0, bitmapFilename);
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
			CreateSurface(0, bitmapFilename);
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
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiBitmapFont_SetKerning(unmanagedGapiObject, tcPreviousChar, tcCharToAdjust, lOffset));
		}

		public int GetTextWidth(string drawString)
		{
			int result;

			GdNet.CGapiBitmapFont_GetStringWidth (unmanagedGapiObject, drawString, out result);
			return result;
		}

		public int GetCharWidth(char tcChar)
		{
			// public static extern UInt32 CGapiBitmapFont_GetCharWidth (IntPtr pBitmapFont, char tcChar, out int pWidth);
			int result;

			GdNet.CGapiBitmapFont_GetCharWidth (unmanagedGapiObject, tcChar, out result);
			return result;
		}

		public int GetSpacing (char tcChar1, char tcChar2)
		{
			// public static extern UInt32 CGapiBitmapFont_GetSpacing (IntPtr pBitmapFont, char tcChar1, char tcChar2, out int pWidth);
			int result;

			GdNet.CGapiBitmapFont_GetSpacing (unmanagedGapiObject, tcChar1, tcChar2, out result);
			return result;
		}
	}
}
