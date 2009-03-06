using System;
using System.Drawing;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for GapiBitmapFont.
	/// </summary>
	public class GapiBitmapFont : GapiSurface
    {
        /// <summary>
        /// Creates a GapiBitmapFont which delegates for an exsiting surface handle.
        /// </summary>
        internal GapiBitmapFont(IntPtr handle) : base(handle) { }

        #region Public Constructors

        /// <summary>
        /// Creates an empty GapiBitmapFont. You should call CreateSurface followed by CreateFont
        /// to initialize this font.
        /// </summary>
        public GapiBitmapFont() { }

        #endregion

        protected override IntPtr CreateHandle()
        {
            return GdApi.CGapiBitmapFont_Create(GapiDraw.GlobalHandle);
        }

        protected override GapiResult DestroyHandle()
        {
            return GdApi.CGapiBitmapFont_Destroy(Handle);
        }

        /// <summary>
        /// Initializes this surface as a font.
        /// </summary>
        public unsafe void CreateFont(string charactersInFont, Color colorKey, bool simpleBitmap,
            int? tracking)
        {
            FontFX fx;
            FontFX* pFX = null;
            CreateFontOptions options = 0;

            if (tracking.HasValue || simpleBitmap)
            {
                fx = new FontFX();
                pFX = &fx;

                if (tracking.HasValue)
                {
                    options |= CreateFontOptions.Tracking;
                    fx.Tracking = tracking.Value;
                }

                if (simpleBitmap)
                    options |= CreateFontOptions.SimpleBitmap;
            }

            CheckResult(GdApi.CGapiBitmapFont_CreateFont(Handle,
                Str(charactersInFont), colorKey.ToColorRef(), options, pFX));
        }

        // Everything below is from the older Intuitex package, needs to be cleaned up to match above



		// public static extern UInt32 CGapiBitmapFont_CreateFont (IntPtr pBitmapFont, int dwFlags, ref GDFONTFX pGDFontFx);
		/*public void CreateFont(CreateFontOptions dwFlags, FontFX gdFontFx)
		{
			CheckResult (GdApi.CGapiBitmapFont_CreateFont_NoString(Handle, 0, GetPixel(0, 0), (int)dwFlags, ref gdFontFx));
		}

		public void CreateFont(string pString, int dwColorkey, CreateFontOptions dwFlags, FontFX gdFontFx)
		{
			CheckResult (GdApi.CGapiBitmapFont_CreateFont(Handle, Str(pString), dwColorkey, (int)dwFlags, ref gdFontFx));
		}*/

		// public static extern UInt32 CGapiBitmapFont_SetKerning (IntPtr pBitmapFont, char tcPreviousChar, char tcCharToAdjust, int lOffset);
		public void SetKerning(char tcPreviousChar, char tcCharToAdjust, int lOffset)
		{
			CheckResult (GdApi.CGapiBitmapFont_SetKerning(Handle, tcPreviousChar, tcCharToAdjust, lOffset));
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
