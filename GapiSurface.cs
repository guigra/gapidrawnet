using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for GapiSurface.
	/// </summary>
	public class GapiSurface : GapiObjectBase
	{
        Size size;

        public GapiSurface()
            : base(GdNet.CGapiSurface_Create(GapiDraw.GlobalHandle)) { }

        public GapiSurface(IntPtr gapiObject)
            : base(gapiObject) { }

        protected override void DestroyGapiObject(IntPtr gapiObject)
        {
            GdNet.CGapiSurface_Destroy(unmanagedGapiObject);
        }

        /// <summary>
        /// Gets the size of this surface.
        /// </summary>
        public Size Size
        {
            // Create on-demand and cache forever when it's nonzero, since surface sizes can't change
            get { return size != Size.Empty ? size : (size = new Size(GetWidth(), GetHeight())); }
        }

        /// <summary>
        /// Gets the effective bounding Rectangle for this surface, always at 0,0.
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, Width, Height); }
        }

		public UInt32 CreateSurface(string fileName)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdNet.CGapiSurface_CreateSurfaceFromFile(unmanagedGapiObject, 0, fileName);

			GapiUtility.RaiseExceptionOnError(hResult, fileName);

			return hResult;
		}

		public UInt32 CreateSurface(CreateSurfaceOptions dwFlags, string fileName)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdNet.CGapiSurface_CreateSurfaceFromFile(unmanagedGapiObject, (int)dwFlags, fileName);

			GapiUtility.RaiseExceptionOnError(hResult, fileName);

			return hResult;
		}
		
		public UInt32 CreateSurface(GapiSurface srcSurface)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdNet.CGapiSurface_CreateSurfaceFromSurface(unmanagedGapiObject, srcSurface.unmanagedGapiObject);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

		public UInt32 CreateSurface(byte[] imageBytes, CreateSurfaceOptions dwFlags)
		{
			UInt32 hResult = GdNet.CGapiSurface_CreateSurfaceFromMem (unmanagedGapiObject, (int)dwFlags, imageBytes, imageBytes.Length);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}
		//		public UInt32 CreateSurfaceFromMem (IntPtr pSurface, ref byte pImageFileMem, int dwImageFileSize);
		//		public UInt32 CreateSurfaceFromRes (IntPtr pSurface, IntPtr hInstance, int dwResourceID, string pResourceType);

		public UInt32 CreateSurface(IntPtr hInstance, CreateSurfaceOptions dwFlags, int dwResourceID, string pResourceType)
		{
			UInt32 hResult = GdNet.CGapiSurface_CreateSurfaceFromRes(unmanagedGapiObject, (int)dwFlags, hInstance, dwResourceID, pResourceType);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

		//		public UInt32 CreateSurfaceOfSize (IntPtr pSurface, int dwFlags, int dwWidth, int dwHeight);
		public void CreateSurface(CreateSurfaceOptions dwFlags, int dwWidth, int dwHeight)
		{
            // public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdNet.CGapiSurface_CreateSurface(unmanagedGapiObject, (int)dwFlags, dwWidth, dwHeight);

			GapiUtility.RaiseExceptionOnError(hResult);
		}

		public void CreateSurface(int dwWidth, int dwHeight)
		{
            // public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdNet.CGapiSurface_CreateSurface(unmanagedGapiObject, 0, dwWidth, dwHeight);

			GapiUtility.RaiseExceptionOnError(hResult);
		}

        public override void Dispose()
        {
            base.Dispose();
        }

		public int GetWidth()
		{
			return GdNet.CGapiSurface_GetWidth(unmanagedGapiObject);
		}

		public int Width
		{
            get { return Size.Width; }
		}

		public int GetHeight()
		{
			return GdNet.CGapiSurface_GetHeight(unmanagedGapiObject);
		}

		public int Height
		{
            get { return Size.Height; }
		}		

		
		//		public UInt32 GetColorKey (IntPtr pSurface, ref int pColorKey);
		public int GetColorKey()
		{
			int result;
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_GetColorKey(unmanagedGapiObject, out result));
			return result;
		}
		
		//		public UInt32 SetColorKey (IntPtr pSurface, int dwColorKey);
		public void SetColorKey(int dwColorKey)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_SetColorKey(unmanagedGapiObject, dwColorKey));
		}

		public int ColorKey
		{
			get { return GetColorKey(); }
			set { SetColorKey(value); }
		}

		public void SetColorKeyFromBottomLeftCorner()
		{
			// get from bottom left corner
			int height = Height;
			int color  = GetPixel(0, height - 1);
			SetColorKey(color);
		}

		public void SetColorKeyFromTopLeftCorner()
		{
			SetColorKey(GetPixel(0, 0));
		}
		
//		public UInt32 CGapiSurface_Lock(IntPtr pSurface, ref GDRect pRect, ref GDSURFACEDESC pGDSurfaceDesc);

		
//		public UInt32 CGapiSurface_Unlock(IntPtr pSurface, ref GDRect pRect);

		
//		public UInt32 CGapiSurface_GetDC(IntPtr pSurface, IntPtr pDC);
		public IntPtr GetDC()
		{
			IntPtr result;
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_GetDC(unmanagedGapiObject, out result));
			return result;
		}

		
//		public UInt32 CGapiSurface_ReleaseDC(IntPtr pSurface, IntPtr hDC);
		public void ReleaseDC(IntPtr hDC)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_ReleaseDC(unmanagedGapiObject, hDC));
		}
		
//		public UInt32 CGapiSurface_GetBuffer (IntPtr pSurface, ref GDBUFFERDESC pGDBufferDesc);
		public GDBUFFERDESC GetBuffer()
		{
			GDBUFFERDESC pGDBufferDesc;
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_GetBuffer(unmanagedGapiObject, out pGDBufferDesc));
			return pGDBufferDesc;
		}

		
//		public UInt32 CGapiSurface_ReleaseBuffer (IntPtr pSurface);
		public void ReleaseBuffer()
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_ReleaseBuffer(unmanagedGapiObject));
		}
	
		// TODO : TEST THESE
		public UInt32 LockVideoSurface()
		{
			return GdNet.CGapiSurface_LockVideoSurface(unmanagedGapiObject);
		}

		// TODO : TEST THESE
		public UInt32 UnlockVideoSurface()
		{
			return GdNet.CGapiSurface_UnlockVideoSurface(unmanagedGapiObject);
		}

//		public UInt32 CGapiSurface_SaveSurface (IntPtr pSurface, ref char pBitmapFile);
		public void SaveSurface(string bitmapFilename, SaveSurfaceOptions options)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_SaveSurface(unmanagedGapiObject, bitmapFilename, (int)options));
		}

		public void SaveSurface(string bitmapFilename)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_SaveSurface(unmanagedGapiObject, bitmapFilename, (int)SaveSurfaceOptions.GDSAVESURFACE_BMP), bitmapFilename);
		}
		
//		public UInt32 CGapiSurface_GetSurfaceOptions (IntPtr pSurface, ref int pOptions);
		public CreateSurfaceOptions GetSurfaceFlags()
		{
			int result;
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_GetSurfaceFlags(unmanagedGapiObject, out result));
			return (CreateSurfaceOptions)result;
		}
		
//		public UInt32 CGapiSurface_SetSurfaceOptions (IntPtr pSurface, int dwOptions);
//		public void SetSurfaceOptions(GapiSurfaceOptions options)
//		{
//			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_SetSurfaceOptions(unmanagedGapiObject, (int)options));
//		}
		
//		public GapiSurfaceOptions SurfaceOptions
//		{
//			get { return GetSurfaceOptions(); }
//			set { SetSurfaceOptions(value); }
//		}

        #region SetClipper

        /// <summary>
        /// Removes the clipping area entirely.
        /// </summary>
        public void ClearClipper()
        {
            SetClipper(new GDRect(0, 0, 0, 0));
        }

        /// <summary>
        /// Clips to the entire area of this surface.
        /// </summary>
        public unsafe void SetClipper()
        {
            CheckResult(GdNet.CGapiSurface_SetClipper(
                unmanagedGapiObject, null));
        }

        /// <summary>
        /// Clips to the given area of this surface.
        /// </summary>
        public unsafe void SetClipper(GDRect clipRect)
        {
            CheckResult(GdNet.CGapiSurface_SetClipper(
                unmanagedGapiObject, &clipRect));
        }

        #endregion

        #region Blt

        /// <summary>
        /// Blits the entire area of the given surface on the entire area of this surface.
        /// </summary>
        public unsafe void Blt(GapiSurface surface)
        {
            CheckResult(GdNet.CGapiSurface_Blt(
                unmanagedGapiObject, null, surface.GapiObject, null, 0, null));
        }

        /// <summary>
        /// Blits the entire area of the given surface on the specified area of this surface.
        /// </summary>
        public unsafe void Blt(GDRect destRect, GapiSurface surface)
        {
            CheckResult(GdNet.CGapiSurface_Blt(
                unmanagedGapiObject, &destRect, surface.GapiObject, null, 0, null));
        }

        /// <summary>
        /// Blits the given area of the given surface on the specified area of this surface.
        /// </summary>
        public unsafe void Blt(GDRect destRect, GapiSurface surface, GDRect surfaceRect)
        {
            CheckResult(GdNet.CGapiSurface_Blt(
                unmanagedGapiObject, &destRect, surface.GapiObject, &surfaceRect, 0, null));
        }

        /// <summary>
        /// Blit the given area of the given surface onto the specified area of this surface with
        /// options and effects.
        /// </summary>
        public unsafe void Blt(GDRect destRect, GapiSurface surface, GDRect surfaceRect,
            BltOptions options, ref GDBLTFX fx)
        {
            fixed (GDBLTFX* pFX = &fx)
                CheckResult(GdNet.CGapiSurface_Blt(
                    unmanagedGapiObject, &destRect, surface.GapiObject, &surfaceRect, options, pFX));
        }

        #endregion

        #region BltFast

        /// <summary>
        /// Blits the entire given surface onto this surface at the specified coordinates.
        /// </summary>
        public unsafe void BltFast(int x, int y, GapiSurface surface)
        {
            CheckResult(GdNet.CGapiSurface_BltFast(
                unmanagedGapiObject, x, y, surface.GapiObject, null, 0, null));
        }

        /// <summary>
        /// Blits the given area of the specified surface onto this surface at the specified coordinates.
        /// </summary>
        public unsafe void BltFast(int x, int y, GapiSurface surface, GDRect surfaceRect)
        {
            CheckResult(GdNet.CGapiSurface_BltFast(
                unmanagedGapiObject, x, y, surface.GapiObject, &surfaceRect, 0, null));
        }

        /// <summary>
        /// Blits the given area of the specified surface onto this surface at the specified coordinates
        /// with options and effects.
        /// </summary>
        public unsafe void BltFast(int x, int y, GapiSurface surface, GDRect surfaceRect,
            BltFastOptions options, ref GDBLTFASTFX fx)
        {
            fixed (GDBLTFASTFX* pFX = &fx)
                CheckResult(GdNet.CGapiSurface_BltFast(
                    unmanagedGapiObject, x, y, surface.GapiObject, &surfaceRect, options, pFX));
        }

        #endregion

        #region AlphaBltFast

        /// <summary>
        /// Blits an entire surface with an alpha surface component onto this surface at the given coordinates.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiSurface surface, GapiSurface alphaSurface)
        {
            CheckResult(GdNet.CGapiSurface_AlphaBltFast(
                unmanagedGapiObject, x, y, surface.GapiObject, null,
                alphaSurface.GapiObject, null, 0, null));
        }

        /// <summary>
        /// Blits a surface with an alpha surface component onto this surface at the given coordinates.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiSurface surface, GDRect surfaceRect,
            GapiSurface alphaSurface, GDRect alphaRect)
        {
            CheckResult(GdNet.CGapiSurface_AlphaBltFast(
                unmanagedGapiObject, x, y, surface.GapiObject, &surfaceRect,
                alphaSurface.GapiObject, &alphaRect, 0, null));
        }

        /// <summary>
        /// Blits a surface with an alpha surface component onto this surface at the given coordinates
        /// and opacity.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiSurface surface, GDRect surfaceRect,
            GapiSurface alphaSurface, GDRect alphaRect, int opacity)
        {
            GDALPHABLTFASTFX fx;
            fx.dwOpacity = opacity;

            CheckResult(GdNet.CGapiSurface_AlphaBltFast(
                unmanagedGapiObject, x, y, surface.GapiObject, &surfaceRect,
                alphaSurface.GapiObject, &alphaRect,
                AlphaBltFastOptions.GDALPHABLTFAST_OPACITY, &fx));
        }

        /// <summary>
        /// Blits the entire given GapiRGBASurface onto this surface at the given coordinates.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiRGBASurface surface)
        {
            CheckResult(GdNet.CGapiSurface_AlphaBltFastRgba(
                unmanagedGapiObject, x, y, surface.GapiObject, null, 0, null));
        }

        /// <summary>
        /// Blits the given GapiRGBASurface region onto this surface at the given coordinates.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiRGBASurface surface, GDRect surfaceRect)
        {
            CheckResult(GdNet.CGapiSurface_AlphaBltFastRgba(
                unmanagedGapiObject, x, y, surface.GapiObject, &surfaceRect, 0, null));
        }

        /// <summary>
        /// Blits the given GapiRGBASurface region onto this surface at the given coordinates at the given
        /// opacity.
        /// </summary>
        /// <param name="opacity">Uses the opacity value in pGDABltFastFx->dwOpacity to adjust the overall weight of the alpha blend. Allowed range is from 0 (transparent) to 255 (opaque). If the opacity is set to 128, an optimized blending mode will be used.</param>
        public unsafe void AlphaBltFast(int x, int y, GapiRGBASurface surface, GDRect surfaceRect,
            int opacity)
        {
            GDALPHABLTFASTFX fx;
            fx.dwOpacity = opacity;

            CheckResult(GdNet.CGapiSurface_AlphaBltFastRgba(
                unmanagedGapiObject, x, y, surface.GapiObject, &surfaceRect,
                AlphaBltFastOptions.GDALPHABLTFAST_OPACITY, &fx));
        }

        #endregion

        #region DrawLine

        /// <summary>
        /// Draws a line on the surface without antialiasing.
        /// </summary>
        public unsafe void DrawLine(int x1, int y1, int x2, int y2, int color)
        {
            CheckResult(GdNet.CGapiSurface_DrawLine(
                unmanagedGapiObject, x1, y1, x2, y2, color, 0, null));
        }

        /// <summary>
        /// Draws a line on the surface with optional antialiasing.
        /// </summary>
        public unsafe void DrawLine(int x1, int y1, int x2, int y2, int color, bool antiAlias)
        {
            DrawLineOptions options = antiAlias ? DrawLineOptions.GDDRAWLINE_ANTIALIAS : 0;

            CheckResult(GdNet.CGapiSurface_DrawLine(
                unmanagedGapiObject, x1, y1, x2, y2, color, options, null));
        }

        /// <summary>
        /// Draws a line on the surface.
        /// </summary>
        /// <param name="opacity">Uses the opacity value in pGDABltFastFx->dwOpacity to adjust the overall weight of the alpha blend. Allowed range is from 0 (transparent) to 255 (opaque). If the opacity is set to 128, an optimized blending mode will be used.</param>
        public unsafe void DrawLine(int x1, int y1, int x2, int y2, int color, bool antiAlias, int opacity)
        {
            GDLINEFX fx;
            fx.dwOpacity = opacity;

            var options = DrawLineOptions.GDDRAWLINE_OPACITY;

            if (antiAlias)
                options |= DrawLineOptions.GDDRAWLINE_ANTIALIAS;

            CheckResult(GdNet.CGapiSurface_DrawLine(
                unmanagedGapiObject, x1, y1, x2, y2, color, options, &fx));
        }

        #endregion

        #region FillRect

        /// <summary>
        /// Fills the entire area of this surface with the given color.
        /// </summary>
        public unsafe void FillRect(int color)
        {
            CheckResult(GdNet.CGapiSurface_FillRect(
                unmanagedGapiObject, null, color, 0, null));
        }

        /// <summary>
        /// Fills the specified area of this surface with the given color.
        /// </summary>
        public unsafe void FillRect(GDRect rect, int color)
        {
            CheckResult(GdNet.CGapiSurface_FillRect(
                unmanagedGapiObject, &rect, color, 0, null));
        }

        /// <summary>
        /// Fills the entire area of this surface with the given color at the given opacity.
        /// </summary>
        public unsafe void FillRect(int color, int opacity)
        {
            GDFILLRECTFX fx;
            fx.dwOpacity = opacity;

            CheckResult(GdNet.CGapiSurface_FillRect(
                unmanagedGapiObject, null, color, FillRectOptions.GDFILLRECT_OPACITY, &fx));
        }
        
        /// <summary>
        /// Fills the specified area of this surface with the given color at the given opacity.
        /// </summary>
        public unsafe void FillRect(GDRect rect, int color, int opacity)
        {
            GDFILLRECTFX fx;
            fx.dwOpacity = opacity;

            CheckResult(GdNet.CGapiSurface_FillRect(
                unmanagedGapiObject, &rect, color, FillRectOptions.GDFILLRECT_OPACITY, &fx));
        }

        #endregion

        //		public UInt32 CGapiSurface_AlphaBlt (IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, ref GDRect pSrcRect, IntPtr pAlphaSurface, ref GDRect pAlphaRect, int dwFlags, ref GDALPHABLTFX pGDAlphaBltFx);
		public void AlphaBlt(ref GDRect pDestRect, GapiSurface srcSurface, ref GDRect pSrcRect, GapiSurface alphaSurface, ref GDRect alphaRect, AlphaBltOptions dwFlags, ref GDALPHABLTFX pGDAlphaBltFx)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_AlphaBlt(unmanagedGapiObject, ref pDestRect, srcSurface.GapiObject, ref pSrcRect, alphaSurface.GapiObject, ref alphaRect, (int)dwFlags, ref pGDAlphaBltFx));
		}
		
		public void AlphaBlt(ref GDRect pDestRect, GapiSurface srcSurface, GapiSurface alphaSurface, ref GDRect alphaRect)
		{
			unsafe
			{
				fixed (GDRect* pAlphaRect = &alphaRect)
					GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_AlphaBltNoRect(unmanagedGapiObject, ref pDestRect, srcSurface.GapiObject, null, alphaSurface.GapiObject, ref alphaRect, 0, null));
			}
		}
		public void AlphaBlt(ref GDRect pDestRect, GapiSurface srcSurface, ref GDRect srcRect, GapiSurface alphaSurface, ref GDRect alphaRect)
		{
			unsafe
			{
				fixed (GDRect* pSrcRect = &srcRect)
					fixed (GDRect* pAlphaRect = &alphaRect)
						GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_AlphaBltNoRect(unmanagedGapiObject, ref pDestRect, srcSurface.GapiObject, pSrcRect, alphaSurface.GapiObject, ref alphaRect, 0, null));
			}
		}

//		public UInt32 CGapiSurface_GetPixel (IntPtr pSurface, int dwX, int dwY, ref int pColor);
		public int GetPixel(int dwX, int dwY)
		{
			int result;
			GdNet.CGapiSurface_GetPixel(unmanagedGapiObject, dwX, dwY, out result);
			return result;
		}
		
//		public UInt32 CGapiSurface_SetPixel (IntPtr pSurface, int dwX, int dwY, int dwColor);
		public UInt32 SetPixel(int dwX, int dwY, int dwColor)
		{
			return GdNet.CGapiSurface_SetPixel(unmanagedGapiObject, dwX, dwY, dwColor);
		}

		
//		public UInt32 CGapiSurface_SetPixelsArray(IntPtr pSurface, ref GDPIXEL pFirst, int dwElementSize, int dwElementCount, int dwFlags);
		public void SetPixels(GDPIXEL[] pFirst, int dwElementSize, SetPixelsOptions dwFlags)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_SetPixelsArray(unmanagedGapiObject, ref pFirst[0], dwElementSize, pFirst.Length, (int)dwFlags));
		}

		public void SetPixels(GDPIXEL[] pFirst, int dwElementSize)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_SetPixelsArray(unmanagedGapiObject, ref pFirst[0], dwElementSize, pFirst.Length, 0));
		}

//		public UInt32 CGapiSurface_SetPixelsList(IntPtr pSurface, ref GDPIXELNODE pHead, int dwFlags);
		public void SetPixels(ref GDPIXELNODE pHead, int dwFlags)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_SetPixelsList(unmanagedGapiObject, ref pHead, dwFlags));
		}

		public void SetPixelsArray(GDPIXEL[] pFirst, int dwElementSize)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_SetPixelsArray(unmanagedGapiObject, ref pFirst[0], dwElementSize, pFirst.Length, 0));
		}

//		public UInt32 CGapiSurface_DrawRect(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, ref GDLINEFX pGDLineFx);
		public void DrawRect(ref GDRect pRect, int dwColor, DrawLineOptions dwFlags, ref GDLINEFX pGDLineFx)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_DrawRect(unmanagedGapiObject, ref pRect, dwColor, (int)dwFlags, ref pGDLineFx));
		}
		
		public void DrawRect(ref GDRect pRect, int dwColor)
		{
			unsafe
			{
				GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_DrawRectNoOptions(unmanagedGapiObject, ref pRect, dwColor, 0, null));
			}
		}

//		public static extern UInt32 CGapiSurface_DrawTextBitmapFont(IntPtr pSurface, int dwX, int dwY, string pString, IntPtr pFont, int dwTextFlags, int dwBltFastFlags, IntPtr pGDBltFastFx, out int pWidth);
		public int GetTextWidth(string drawString, GapiBitmapFont font)
		{
			int result;

			GdNet.CGapiBitmapFont_GetStringWidth (font.GapiObject, drawString, out result);
			/// TODO : fix GetTextWidth
			// GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_DrawTextBitmapFont(unmanagedGapiObject, 0, 0, drawString, font.GapiObject, (int)DrawTextOptions.GDDRAWTEXT_CALCWIDTH, 0, IntPtr.Zero, out result));
			return result;
		}
		
		public void GetClipper(ref GDRect clipRect)
		{
			GdNet.CGapiSurface_GetClipper(unmanagedGapiObject, ref clipRect);
		}

//		public void DrawText(int dwX, int dwY, string drawString, GapiBitmapFont font, DrawTextOptions dwFlags, GDTEXTFX gDTextFx)
//		{
//			int dummy;
//			unsafe
//			{
//				IntPtr textFx = new IntPtr(&gDTextFx);
//				GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_DrawTextGapiFont(unmanagedGapiObject, dwX, dwY, drawString,  font.GapiObject, (int)dwFlags, textFx, out dummy));
//			}
//		}

		public int DrawText(int dwX, int dwY, string drawString, IntPtr pFont, DrawTextOptions dwFlags)
		{
			int result;
			/// TODO : Overload with missing paramters
			result = (int)GdNet.CGapiSurface_DrawText(unmanagedGapiObject, dwX, dwY, drawString, pFont, (int)dwFlags,IntPtr.Zero, 0, IntPtr.Zero);
			return result;
		}

		public int DrawText(int dwX, int dwY, string drawString, GapiBitmapFont font, DrawTextOptions dwFlags)
		{
			int result;
			/// TODO : Overload with missing paramters
			result = (int)GdNet.CGapiSurface_DrawText(unmanagedGapiObject, dwX, dwY, drawString, font.GapiObject, (int)dwFlags,IntPtr.Zero, 0, IntPtr.Zero);
			return result;
		}
		
//		public UInt32 Intersect(int dwX1, int dwY1, IntPtr pSrcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, IntPtr pSrcSurface2, ref GDRect pSrcRect2, ref System.Drawing.Point pIntersection);
		public static void Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, GapiSurface srcSurface2, ref GDRect pSrcRect2, ref System.Drawing.Point pIntersection)
		{
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_Intersect(dwX1, dwY1, srcSurface1.GapiObject, ref pSrcRect1, dwX2, dwY2, srcSurface2.GapiObject, ref pSrcRect2, ref pIntersection));
		}
		
		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, GapiSurface srcSurface2, ref GDRect pSrcRect2)
		{
			System.Drawing.Point point = new System.Drawing.Point(-1, -1);
			GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_Intersect(dwX1, dwY1, srcSurface1.GapiObject, ref pSrcRect1, dwX2, dwY2, srcSurface2.GapiObject, ref pSrcRect2, ref point));
			return point;
		}

		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, GapiSurface srcSurface2)
		{
			unsafe
			{
				System.Drawing.Point point = new System.Drawing.Point(-1, -1);
				
				fixed (GDRect* pSrcRect = &pSrcRect1)
					GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_IntersectNoRect(dwX1, dwY1, srcSurface1.GapiObject, pSrcRect, dwX2, dwY2, srcSurface2.GapiObject, null, ref point));
				return point;
			}
		}

		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, int dwX2, int dwY2, GapiSurface srcSurface2, ref GDRect pSrcRect2)
		{
			unsafe
			{
				System.Drawing.Point point = new System.Drawing.Point(-1, -1);
				
				fixed (GDRect* pSrcRect = &pSrcRect2)
					GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_IntersectNoRect(dwX1, dwY1, srcSurface1.GapiObject, null, dwX2, dwY2, srcSurface2.GapiObject, pSrcRect, ref point));
			
				return point;
			}

		}

		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, int dwX2, int dwY2, GapiSurface srcSurface2)
		{
			System.Drawing.Point point = new System.Drawing.Point(-1, -1);
			unsafe
			{
				GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_IntersectNoRect(dwX1, dwY1, srcSurface1.GapiObject, null, dwX2, dwY2, srcSurface2.GapiObject, null, ref point));
			}
			return point;
		}

		// TODO : TEST THESE
		//public static extern UInt32 CGapiSurface_ColorrefToNative(IntPtr pSurface, int dwColor, out int pNative);
		public int ColorrefToNative(int dwColor)
		{
			int pNative;
			GdNet.CGapiSurface_ColorrefToNative(unmanagedGapiObject, dwColor, out pNative);
			return pNative;
		}
		
		// TODO : TEST THESE
		//public static extern UInt32 CGapiSurface_NativeToColorref(IntPtr pSurface, int dwNative, out int pColor);
		public int NativeToColorref(int dwNative)
		{
			int pColor;
			GdNet.CGapiSurface_NativeToColorref(unmanagedGapiObject, dwNative, out pColor);
			return pColor;
		}
    }
}