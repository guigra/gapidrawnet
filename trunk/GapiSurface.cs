using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;

namespace GapiDrawNet
{
	/// <summary>
    /// CGapiSurface is a memory area to which you can draw images and primitives.
	/// </summary>
	public class GapiSurface : GapiObjectRef
	{
        public GapiSurface()
            : base(GdApi.CGapiSurface_Create(GapiDraw.GlobalHandle)) { }

        public GapiSurface(IntPtr gapiObject, bool ownsHandle)
            : base(gapiObject, ownsHandle) { }

        protected override void DestroyGapiObject(IntPtr gapiObject)
        {
            CheckResult(GdApi.CGapiSurface_Destroy(Handle));
        }

        #region CreateSurface

        public UInt32 CreateSurface(string fileName)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdApi.CGapiSurface_CreateSurfaceFromFile(Handle, 0, fileName);

			GapiErrorHelper.RaiseExceptionOnError(hResult, fileName);

			return hResult;
		}

		public UInt32 CreateSurface(CreateSurfaceOptions dwFlags, string fileName)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdApi.CGapiSurface_CreateSurfaceFromFile(Handle, (int)dwFlags, fileName);

			GapiErrorHelper.RaiseExceptionOnError(hResult, fileName);

			return hResult;
		}
		
		public UInt32 CreateSurface(GapiSurface srcSurface)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdApi.CGapiSurface_CreateSurfaceFromSurface(Handle, srcSurface.Handle);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

		public UInt32 CreateSurface(byte[] imageBytes, CreateSurfaceOptions dwFlags)
		{
			UInt32 hResult = GdApi.CGapiSurface_CreateSurfaceFromMem (Handle, (int)dwFlags, imageBytes, imageBytes.Length);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}
		//		public UInt32 CreateSurfaceFromMem (IntPtr pSurface, ref byte pImageFileMem, int dwImageFileSize);
		//		public UInt32 CreateSurfaceFromRes (IntPtr pSurface, IntPtr hInstance, int dwResourceID, string pResourceType);

		public UInt32 CreateSurface(IntPtr hInstance, CreateSurfaceOptions dwFlags, int dwResourceID, string pResourceType)
		{
			UInt32 hResult = GdApi.CGapiSurface_CreateSurfaceFromRes(Handle, (int)dwFlags, hInstance, dwResourceID, pResourceType);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

		//		public UInt32 CreateSurfaceOfSize (IntPtr pSurface, int dwFlags, int dwWidth, int dwHeight);
		public void CreateSurface(CreateSurfaceOptions dwFlags, int dwWidth, int dwHeight)
		{
            // public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdApi.CGapiSurface_CreateSurface(Handle, (int)dwFlags, dwWidth, dwHeight);

			GapiErrorHelper.RaiseExceptionOnError(hResult);
		}

		public void CreateSurface(int dwWidth, int dwHeight)
		{
            // public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdApi.CGapiSurface_CreateSurface(Handle, 0, dwWidth, dwHeight);

			GapiErrorHelper.RaiseExceptionOnError(hResult);
        }

        #endregion

        #region Size Metrics

        public int Width
		{
            get { return GdApi.CGapiSurface_GetWidth(Handle); }
		}

		public int Height
		{
            get { return GdApi.CGapiSurface_GetHeight(Handle); }
		}

        /// <summary>
        /// Gets the size of this surface.
        /// </summary>
        public Size Size
        {
            get { return new Size(Width, Height); }
        }

        /// <summary>
        /// Gets the effective bounding Rectangle for this surface, always located at 0,0.
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, Width, Height); }
        }

        #endregion

        //		public UInt32 GetColorKey (IntPtr pSurface, ref int pColorKey);
		public int GetColorKey()
		{
			int result;
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_GetColorKey(Handle, out result));
			return result;
		}
		
		//		public UInt32 SetColorKey (IntPtr pSurface, int dwColorKey);
		public void SetColorKey(int dwColorKey)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_SetColorKey(Handle, dwColorKey));
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
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_GetDC(Handle, out result));
			return result;
		}

		
//		public UInt32 CGapiSurface_ReleaseDC(IntPtr pSurface, IntPtr hDC);
		public void ReleaseDC(IntPtr hDC)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_ReleaseDC(Handle, hDC));
		}
		
//		public UInt32 CGapiSurface_GetBuffer (IntPtr pSurface, ref GDBUFFERDESC pGDBufferDesc);
		public GDBUFFERDESC GetBuffer()
		{
			GDBUFFERDESC pGDBufferDesc;
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_GetBuffer(Handle, out pGDBufferDesc));
			return pGDBufferDesc;
		}

		
//		public UInt32 CGapiSurface_ReleaseBuffer (IntPtr pSurface);
		public void ReleaseBuffer()
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_ReleaseBuffer(Handle));
		}
	
		// TODO : TEST THESE
		public UInt32 LockVideoSurface()
		{
			return GdApi.CGapiSurface_LockVideoSurface(Handle);
		}

		// TODO : TEST THESE
		public UInt32 UnlockVideoSurface()
		{
			return GdApi.CGapiSurface_UnlockVideoSurface(Handle);
		}

//		public UInt32 CGapiSurface_SaveSurface (IntPtr pSurface, ref char pBitmapFile);
		public void SaveSurface(string bitmapFilename, SaveSurfaceOptions options)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_SaveSurface(Handle, bitmapFilename, (int)options));
		}

		public void SaveSurface(string bitmapFilename)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_SaveSurface(Handle, bitmapFilename, (int)SaveSurfaceOptions.GDSAVESURFACE_BMP), bitmapFilename);
		}
		
//		public UInt32 CGapiSurface_GetSurfaceOptions (IntPtr pSurface, ref int pOptions);
		public CreateSurfaceOptions GetSurfaceFlags()
		{
			int result;
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_GetSurfaceFlags(Handle, out result));
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
            CheckResult(GdApi.CGapiSurface_SetClipper(
                Handle, null));
        }

        /// <summary>
        /// Clips to the given area of this surface.
        /// </summary>
        public unsafe void SetClipper(GDRect clipRect)
        {
            CheckResult(GdApi.CGapiSurface_SetClipper(
                Handle, &clipRect));
        }

        #endregion

        #region Blt

        /// <summary>
        /// Blits the entire area of the given surface on the entire area of this surface.
        /// </summary>
        public unsafe void Blt(GapiSurface surface)
        {
            CheckResult(GdApi.CGapiSurface_Blt(
                Handle, null, surface.Handle, null, 0, null));
        }

        /// <summary>
        /// Blits the entire area of the given surface on the specified area of this surface.
        /// </summary>
        public unsafe void Blt(GDRect destRect, GapiSurface surface)
        {
            CheckResult(GdApi.CGapiSurface_Blt(
                Handle, &destRect, surface.Handle, null, 0, null));
        }

        /// <summary>
        /// Blits the given area of the given surface on the specified area of this surface.
        /// </summary>
        public unsafe void Blt(GDRect destRect, GapiSurface surface, GDRect surfaceRect)
        {
            CheckResult(GdApi.CGapiSurface_Blt(
                Handle, &destRect, surface.Handle, &surfaceRect, 0, null));
        }

        /// <summary>
        /// Blit the given area of the given surface onto the specified area of this surface with
        /// options and effects.
        /// </summary>
        public unsafe void Blt(GDRect destRect, GapiSurface surface, GDRect surfaceRect,
            BltOptions options, ref GDBLTFX fx)
        {
            fixed (GDBLTFX* pFX = &fx)
                CheckResult(GdApi.CGapiSurface_Blt(
                    Handle, &destRect, surface.Handle, &surfaceRect, options, pFX));
        }

        #endregion

        #region BltFast

        /// <summary>
        /// Blits the entire given surface onto this surface at the specified coordinates.
        /// </summary>
        public unsafe void BltFast(int x, int y, GapiSurface surface)
        {
            CheckResult(GdApi.CGapiSurface_BltFast(
                Handle, x, y, surface.Handle, null, 0, null));
        }

        /// <summary>
        /// Blits the given area of the specified surface onto this surface at the specified coordinates.
        /// </summary>
        public unsafe void BltFast(int x, int y, GapiSurface surface, GDRect surfaceRect)
        {
            CheckResult(GdApi.CGapiSurface_BltFast(
                Handle, x, y, surface.Handle, &surfaceRect, 0, null));
        }

        /// <summary>
        /// Blits the given area of the specified surface onto this surface at the specified coordinates
        /// with options and effects.
        /// </summary>
        public unsafe void BltFast(int x, int y, GapiSurface surface, GDRect surfaceRect,
            BltFastOptions options, ref GDBLTFASTFX fx)
        {
            fixed (GDBLTFASTFX* pFX = &fx)
                CheckResult(GdApi.CGapiSurface_BltFast(
                    Handle, x, y, surface.Handle, &surfaceRect, options, pFX));
        }

        #endregion

        #region AlphaBltFast

        /// <summary>
        /// Blits an entire surface with an alpha surface component onto this surface at the given coordinates.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiSurface surface, GapiSurface alphaSurface)
        {
            CheckResult(GdApi.CGapiSurface_AlphaBltFast(
                Handle, x, y, surface.Handle, null,
                alphaSurface.Handle, null, 0, null));
        }

        /// <summary>
        /// Blits a surface with an alpha surface component onto this surface at the given coordinates.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiSurface surface, GDRect surfaceRect,
            GapiSurface alphaSurface, GDRect alphaRect)
        {
            CheckResult(GdApi.CGapiSurface_AlphaBltFast(
                Handle, x, y, surface.Handle, &surfaceRect,
                alphaSurface.Handle, &alphaRect, 0, null));
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

            CheckResult(GdApi.CGapiSurface_AlphaBltFast(
                Handle, x, y, surface.Handle, &surfaceRect,
                alphaSurface.Handle, &alphaRect,
                AlphaBltFastOptions.GDALPHABLTFAST_OPACITY, &fx));
        }

        /// <summary>
        /// Blits the entire given GapiRGBASurface onto this surface at the given coordinates.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiRGBASurface surface)
        {
            CheckResult(GdApi.CGapiSurface_AlphaBltFastRgba(
                Handle, x, y, surface.GapiObject, null, 0, null));
        }

        /// <summary>
        /// Blits the given GapiRGBASurface region onto this surface at the given coordinates.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiRGBASurface surface, GDRect surfaceRect)
        {
            CheckResult(GdApi.CGapiSurface_AlphaBltFastRgba(
                Handle, x, y, surface.GapiObject, &surfaceRect, 0, null));
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

            CheckResult(GdApi.CGapiSurface_AlphaBltFastRgba(
                Handle, x, y, surface.GapiObject, &surfaceRect,
                AlphaBltFastOptions.GDALPHABLTFAST_OPACITY, &fx));
        }

        #endregion

        #region DrawLine

        /// <summary>
        /// Draws a line on the surface without antialiasing.
        /// </summary>
        public unsafe void DrawLine(int x1, int y1, int x2, int y2, Color color)
        {
            DrawLine(x1, y2, x2, y2, color, false);
        }

        /// <summary>
        /// Draws a line on the surface.
        /// </summary>
        /// <param name="opacity">Uses the opacity value in pGDABltFastFx->dwOpacity to adjust the overall weight of the alpha blend. Allowed range is from 0 (transparent) to 255 (opaque). If the opacity is set to 128, an optimized blending mode will be used.</param>
        public unsafe void DrawLine(int x1, int y1, int x2, int y2, Color color, bool antiAlias)
        {
            GDLINEFX* pFX = null;
            DrawLineOptions options = 0;
            GDLINEFX fx;

            if (color.A < 255)
            {
                options |= DrawLineOptions.GDDRAWLINE_OPACITY;
                fx.dwOpacity = color.A;
                pFX = &fx;
            }
            
            if (antiAlias)
                options |= DrawLineOptions.GDDRAWLINE_ANTIALIAS;

            CheckResult(GdApi.CGapiSurface_DrawLine(
                Handle, x1, y1, x2, y2, color.ToColorRef(), options, pFX));
        }

        #endregion

        #region FillRect

        /// <summary>
        /// Fills the entire area of this surface with the given color (which can contain opacity).
        /// </summary>
        public unsafe void FillRect(Color color)
        {
            if (color.A < 255)
            {
                GDFILLRECTFX fx;
                fx.dwOpacity = color.A;
                CheckResult(GdApi.CGapiSurface_FillRect(
                    Handle, null, color.ToColorRef(), FillRectOptions.GDFILLRECT_OPACITY, &fx));
            }
            else CheckResult(GdApi.CGapiSurface_FillRect(
                    Handle, null, color.ToColorRef(), 0, null));
        }
        
        /// <summary>
        /// Fills the specified area of this surface with the given color at the given opacity.
        /// </summary>
        public unsafe void FillRect(Rectangle rect, Color color)
        {
            GDRect gdRect = rect;

            if (color.A < 255)
            {
                GDFILLRECTFX fx;
                fx.dwOpacity = color.A;
                CheckResult(GdApi.CGapiSurface_FillRect(
                    Handle, &gdRect, color.ToColorRef(), FillRectOptions.GDFILLRECT_OPACITY, &fx));
            }
            else CheckResult(GdApi.CGapiSurface_FillRect(
                    Handle, &gdRect, color.ToColorRef(), 0, null));
        }

        #endregion

        //		public UInt32 CGapiSurface_AlphaBlt (IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, ref GDRect pSrcRect, IntPtr pAlphaSurface, ref GDRect pAlphaRect, int dwFlags, ref GDALPHABLTFX pGDAlphaBltFx);
		public void AlphaBlt(ref GDRect pDestRect, GapiSurface srcSurface, ref GDRect pSrcRect, GapiSurface alphaSurface, ref GDRect alphaRect, AlphaBltOptions dwFlags, ref GDALPHABLTFX pGDAlphaBltFx)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_AlphaBlt(Handle, ref pDestRect, srcSurface.Handle, ref pSrcRect, alphaSurface.Handle, ref alphaRect, (int)dwFlags, ref pGDAlphaBltFx));
		}
		
		public void AlphaBlt(ref GDRect pDestRect, GapiSurface srcSurface, GapiSurface alphaSurface, ref GDRect alphaRect)
		{
			unsafe
			{
				fixed (GDRect* pAlphaRect = &alphaRect)
					GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_AlphaBltNoRect(Handle, ref pDestRect, srcSurface.Handle, null, alphaSurface.Handle, ref alphaRect, 0, null));
			}
		}
		public void AlphaBlt(ref GDRect pDestRect, GapiSurface srcSurface, ref GDRect srcRect, GapiSurface alphaSurface, ref GDRect alphaRect)
		{
			unsafe
			{
				fixed (GDRect* pSrcRect = &srcRect)
					fixed (GDRect* pAlphaRect = &alphaRect)
						GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_AlphaBltNoRect(Handle, ref pDestRect, srcSurface.Handle, pSrcRect, alphaSurface.Handle, ref alphaRect, 0, null));
			}
		}

//		public UInt32 CGapiSurface_GetPixel (IntPtr pSurface, int dwX, int dwY, ref int pColor);
		public int GetPixel(int dwX, int dwY)
		{
			int result;
			GdApi.CGapiSurface_GetPixel(Handle, dwX, dwY, out result);
			return result;
		}
		
//		public UInt32 CGapiSurface_SetPixel (IntPtr pSurface, int dwX, int dwY, int dwColor);
		public UInt32 SetPixel(int dwX, int dwY, int dwColor)
		{
			return GdApi.CGapiSurface_SetPixel(Handle, dwX, dwY, dwColor);
		}

		
//		public UInt32 CGapiSurface_SetPixelsArray(IntPtr pSurface, ref GDPIXEL pFirst, int dwElementSize, int dwElementCount, int dwFlags);
		public void SetPixels(GDPIXEL[] pFirst, int dwElementSize, SetPixelsOptions dwFlags)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_SetPixelsArray(Handle, ref pFirst[0], dwElementSize, pFirst.Length, (int)dwFlags));
		}

		public void SetPixels(GDPIXEL[] pFirst, int dwElementSize)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_SetPixelsArray(Handle, ref pFirst[0], dwElementSize, pFirst.Length, 0));
		}

//		public UInt32 CGapiSurface_SetPixelsList(IntPtr pSurface, ref GDPIXELNODE pHead, int dwFlags);
		public void SetPixels(ref GDPIXELNODE pHead, int dwFlags)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_SetPixelsList(Handle, ref pHead, dwFlags));
		}

		public void SetPixelsArray(GDPIXEL[] pFirst, int dwElementSize)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_SetPixelsArray(Handle, ref pFirst[0], dwElementSize, pFirst.Length, 0));
		}

//		public UInt32 CGapiSurface_DrawRect(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, ref GDLINEFX pGDLineFx);
		public void DrawRect(ref GDRect pRect, int dwColor, DrawLineOptions dwFlags, ref GDLINEFX pGDLineFx)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_DrawRect(Handle, ref pRect, dwColor, (int)dwFlags, ref pGDLineFx));
		}
		
		public void DrawRect(ref GDRect pRect, int dwColor)
		{
			unsafe
			{
				GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_DrawRectNoOptions(Handle, ref pRect, dwColor, 0, null));
			}
		}

//		public static extern UInt32 CGapiSurface_DrawTextBitmapFont(IntPtr pSurface, int dwX, int dwY, string pString, IntPtr pFont, int dwTextFlags, int dwBltFastFlags, IntPtr pGDBltFastFx, out int pWidth);
		public int GetTextWidth(string drawString, GapiBitmapFont font)
		{
			int result;

			GdApi.CGapiBitmapFont_GetStringWidth (font.Handle, drawString, out result);
			/// TODO : fix GetTextWidth
			// GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_DrawTextBitmapFont(unmanagedGapiObject, 0, 0, drawString, font.GapiObject, (int)DrawTextOptions.GDDRAWTEXT_CALCWIDTH, 0, IntPtr.Zero, out result));
			return result;
		}
		
		public void GetClipper(ref GDRect clipRect)
		{
			GdApi.CGapiSurface_GetClipper(Handle, ref clipRect);
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
			result = (int)GdApi.CGapiSurface_DrawText(Handle, dwX, dwY, drawString, pFont, (int)dwFlags,IntPtr.Zero, 0, IntPtr.Zero);
			return result;
		}

		public int DrawText(int dwX, int dwY, string drawString, GapiBitmapFont font, DrawTextOptions dwFlags)
		{
			int result;
			/// TODO : Overload with missing paramters
			result = (int)GdApi.CGapiSurface_DrawText(Handle, dwX, dwY, drawString, font.Handle, (int)dwFlags,IntPtr.Zero, 0, IntPtr.Zero);
			return result;
		}
		
//		public UInt32 Intersect(int dwX1, int dwY1, IntPtr pSrcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, IntPtr pSrcSurface2, ref GDRect pSrcRect2, ref System.Drawing.Point pIntersection);
		public static void Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, GapiSurface srcSurface2, ref GDRect pSrcRect2, ref System.Drawing.Point pIntersection)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_Intersect(dwX1, dwY1, srcSurface1.Handle, ref pSrcRect1, dwX2, dwY2, srcSurface2.Handle, ref pSrcRect2, ref pIntersection));
		}
		
		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, GapiSurface srcSurface2, ref GDRect pSrcRect2)
		{
			System.Drawing.Point point = new System.Drawing.Point(-1, -1);
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_Intersect(dwX1, dwY1, srcSurface1.Handle, ref pSrcRect1, dwX2, dwY2, srcSurface2.Handle, ref pSrcRect2, ref point));
			return point;
		}

		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, GapiSurface srcSurface2)
		{
			unsafe
			{
				System.Drawing.Point point = new System.Drawing.Point(-1, -1);
				
				fixed (GDRect* pSrcRect = &pSrcRect1)
					GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_IntersectNoRect(dwX1, dwY1, srcSurface1.Handle, pSrcRect, dwX2, dwY2, srcSurface2.Handle, null, ref point));
				return point;
			}
		}

		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, int dwX2, int dwY2, GapiSurface srcSurface2, ref GDRect pSrcRect2)
		{
			unsafe
			{
				System.Drawing.Point point = new System.Drawing.Point(-1, -1);
				
				fixed (GDRect* pSrcRect = &pSrcRect2)
					GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_IntersectNoRect(dwX1, dwY1, srcSurface1.Handle, null, dwX2, dwY2, srcSurface2.Handle, pSrcRect, ref point));
			
				return point;
			}

		}

		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, int dwX2, int dwY2, GapiSurface srcSurface2)
		{
			System.Drawing.Point point = new System.Drawing.Point(-1, -1);
			unsafe
			{
				GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiSurface_IntersectNoRect(dwX1, dwY1, srcSurface1.Handle, null, dwX2, dwY2, srcSurface2.Handle, null, ref point));
			}
			return point;
		}

		// TODO : TEST THESE
		//public static extern UInt32 CGapiSurface_ColorrefToNative(IntPtr pSurface, int dwColor, out int pNative);
		public int ColorrefToNative(int dwColor)
		{
			int pNative;
			GdApi.CGapiSurface_ColorrefToNative(Handle, dwColor, out pNative);
			return pNative;
		}
		
		// TODO : TEST THESE
		//public static extern UInt32 CGapiSurface_NativeToColorref(IntPtr pSurface, int dwNative, out int pColor);
		public int NativeToColorref(int dwNative)
		{
			int pColor;
			GdApi.CGapiSurface_NativeToColorref(Handle, dwNative, out pColor);
			return pColor;
		}
    }
}
