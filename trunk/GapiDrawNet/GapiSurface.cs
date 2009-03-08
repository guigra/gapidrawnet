using System;
using System.Drawing;

namespace GapiDrawNet
{
	/// <summary>
    /// GapiSurface is a memory area to which you can draw images and primitives.
	/// </summary>
	public class GapiSurface : GapiObjectRef
	{
        /// <summary>
        /// Creates a GapiSurface which delegates for an exsiting surface handle.
        /// </summary>
        internal GapiSurface(IntPtr handle)
            : base(handle) { }

        #region Public Constructors

        /// <summary>
        /// Creates an empty GapiSurface.
        /// </summary>
        public GapiSurface() { }

        /// <summary>
        /// Initializes the surface with the given size and prepares it for graphic operations.
        /// </summary>
        public GapiSurface(int width, int height)
        {
            CreateSurface(width, height);
        }

        /// <summary>
        /// Initializes the surface with the given size and prepares it for graphic operations.
        /// </summary>
        public GapiSurface(int width, int height, CreateSurfaceOptions options)
        {
            CreateSurface(width, height, options);
        }

        /// <summary>
        /// Initializes the surface from the contents of the given file.
        /// </summary>
        public GapiSurface(string fileName)
        {
            CreateSurface(fileName);
        }

        /// <summary>
        /// Initializes the surface from the contents of the given file.
        /// </summary>
        public GapiSurface(string fileName, CreateSurfaceOptions options)
        {
            CreateSurface(fileName, options);
        }

        /// <summary>
        /// Initializes the surface from the contents of the given byte array.
        /// </summary>
        public GapiSurface(byte[] imageBytes)
        {
            CreateSurface(imageBytes);
        }

        /// <summary>
        /// Initialized the surface from the contents of the given byte array.
        /// </summary>
        public GapiSurface(byte[] imageBytes, CreateSurfaceOptions options)
        {
            CreateSurface(imageBytes, imageBytes.Length, options);
        }

        /// <summary>
        /// Initializes the surface from the contents of the given byte array.
        /// </summary>
        public GapiSurface(byte[] imageBytes, int length, CreateSurfaceOptions options)
        {
            CreateSurface(imageBytes, length, options);
        }

        /// <summary>
        /// Initializes the surface from the given ID of a Win32 bitmap resource.
        /// </summary>
        public GapiSurface(IntPtr hInstance, CreateSurfaceOptions options,
            int resourceID, string resourceType)
        {
            CreateSurface(hInstance, options, resourceID, resourceType);
        }

        /// <summary>
        /// Initializes the surface by copying an existing surface.
        /// </summary>
        public GapiSurface(GapiSurface sourceSurface)
        {
            CreateSurface(sourceSurface);
        }

        #endregion

        protected override IntPtr CreateHandle()
        {
            return GdApi.CGapiSurface_Create(GapiDraw.GlobalHandle);
        }

        protected override GapiResult DestroyHandle()
        {
            return GdApi.CGapiSurface_Destroy(Handle);
        }

        #region CreateSurface

        /// <summary>
        /// Allocates memory for a surface of the given size and prepares it for graphic operations.
        /// </summary>
        public void CreateSurface(int width, int height)
        {
            CreateSurface(width, height, 0);
        }

        /// <summary>
        /// Allocates memory for a surface of the given size and prepares it for graphic operations.
        /// </summary>
        public void CreateSurface(int width, int height, CreateSurfaceOptions options)
        {
            CheckResult(GdApi.CGapiSurface_CreateSurface(Handle, options, (uint)width, (uint)height));
        }

        /// <summary>
        /// Creates the surface from the contents of the given file.
        /// </summary>
        public void CreateSurface(string fileName)
        {
            CreateSurface(fileName, 0);
        }

        /// <summary>
        /// Creates the surface from the contents of the given file.
        /// </summary>
        public void CreateSurface(string fileName, CreateSurfaceOptions options)
        {
            CheckResult(GdApi.CGapiSurface_CreateSurfaceFromFile(Handle, options, Str(fileName)));
        }

        /// <summary>
        /// Creates the surface from the contents of the given byte array.
        /// </summary>
        public void CreateSurface(byte[] imageBytes)
        {
            CreateSurface(imageBytes, imageBytes.Length, 0);
        }

        /// <summary>
        /// Creates the surface from the contents of the given byte array.
        /// </summary>
        public void CreateSurface(byte[] imageBytes, CreateSurfaceOptions options)
        {
            CreateSurface(imageBytes, imageBytes.Length, options);
        }

        /// <summary>
        /// Creates the surface from the contents of the given byte array.
        /// </summary>
        public void CreateSurface(byte[] imageBytes, int length, CreateSurfaceOptions options)
        {
            CheckResult(GdApi.CGapiSurface_CreateSurfaceFromMem(Handle,
                options, imageBytes, (uint)length));
        }

        /// <summary>
        /// Creates the surface from the given ID of a Win32 bitmap resource.
        /// </summary>
        public void CreateSurface(IntPtr hInstance, CreateSurfaceOptions options,
            int resourceID, string resourceType)
        {
            CheckResult(GdApi.CGapiSurface_CreateSurfaceFromRes(Handle,
                options, hInstance, (uint)resourceID, Str(resourceType)));
        }

        /// <summary>
        /// Creates the surface by copying an existing surface.
        /// </summary>
        public void CreateSurface(GapiSurface sourceSurface)
        {
            CheckResult(GdApi.CGapiSurface_CreateSurfaceFromSurface(Handle, sourceSurface.Handle));
        }

        #endregion

        #region Size Metrics

        public int Width
		{
            get { return (int)GdApi.CGapiSurface_GetWidth(Handle); }
		}

		public int Height
		{
            get { return (int)GdApi.CGapiSurface_GetHeight(Handle); }
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

        #region Clipper

        /// <summary>
        /// Gets the current clipping area.
        /// </summary>
        public Rectangle GetClipper()
        {
            GDRect rect;
            CheckResult(GdApi.CGapiSurface_GetClipper(Handle, out rect));
            return rect;
        }

        /// <summary>
        /// Removes the clipping area entirely.
        /// </summary>
        public void ClearClipper()
        {
            SetClipper(Rectangle.Empty);
        }

        /// <summary>
        /// Clips to the entire area of this surface.
        /// </summary>
        public unsafe void SetClipper()
        {
            CheckResult(GdApi.CGapiSurface_SetClipper(Handle, null));
        }

        /// <summary>
        /// Clips to the given area of this surface.
        /// </summary>
        public unsafe void SetClipper(Rectangle clipRect)
        {
            GDRect rect = clipRect;
            CheckResult(GdApi.CGapiSurface_SetClipper(Handle, &rect));
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
        public unsafe void Blt(Rectangle destRect, GapiSurface surface)
        {
            GDRect gdRect = destRect;
            CheckResult(GdApi.CGapiSurface_Blt(
                Handle, &gdRect, surface.Handle, null, 0, null));
        }

        /// <summary>
        /// Blits the given area of the given surface on the specified area of this surface.
        /// </summary>
        public unsafe void Blt(Rectangle destRect, GapiSurface surface, Rectangle surfaceRect)
        {
            GDRect gdDest = destRect, gdSurface = surfaceRect;
            CheckResult(GdApi.CGapiSurface_Blt(
                Handle, &gdDest, surface.Handle, &gdSurface, 0, null));
        }

        /// <summary>
        /// Blit the given area of the given surface onto the specified area of this surface with
        /// options and effects.
        /// </summary>
        public unsafe void Blt(Rectangle destRect, GapiSurface surface, Rectangle surfaceRect,
            BltOptions options, ref BltFX fx)
        {
            GDRect gdDest = destRect, gdSurface = surfaceRect;
            fixed (BltFX* pFX = &fx)
                CheckResult(GdApi.CGapiSurface_Blt(
                    Handle, &gdDest, surface.Handle, &gdSurface, options, pFX));
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
        public unsafe void BltFast(int x, int y, GapiSurface surface, Rectangle surfaceRect)
        {
            GDRect gdRect = surfaceRect;
            CheckResult(GdApi.CGapiSurface_BltFast(
                Handle, x, y, surface.Handle, &gdRect, 0, null));
        }

        /// <summary>
        /// Blits the given area of the specified surface onto this surface at the specified coordinates
        /// with options and effects.
        /// </summary>
        public unsafe void BltFast(int x, int y, GapiSurface surface, Rectangle surfaceRect,
            BltFastOptions options, ref BltFastFX fx)
        {
            GDRect gdRect = surfaceRect;
            fixed (BltFastFX* pFX = &fx)
                CheckResult(GdApi.CGapiSurface_BltFast(
                    Handle, x, y, surface.Handle, &gdRect, options, pFX));
        }

        #endregion

        #region AlphaBlt

        public unsafe void AlphaBlt(Rectangle destRect, GapiSurface srcSurface, Rectangle srcRect,
            GapiSurface alphaSurface, Rectangle alphaRect, AlphaBltOptions options, ref AlphaBltFX fx)
        {
            GDRect pDestRect = destRect, pSrcRect = srcRect, alphRect = alphaRect;
            fixed (AlphaBltFX* pFX = &fx)
                CheckResult(GdApi.CGapiSurface_AlphaBlt(
                    Handle, &pDestRect, srcSurface.Handle, &pSrcRect, alphaSurface.Handle, &alphRect, options, pFX));
        }

        public void AlphaBlt(ref GDRect pDestRect, GapiSurface srcSurface, GapiSurface alphaSurface, ref GDRect alphaRect)
        {
            unsafe
            {
                fixed (GDRect* pAlphaRect = &alphaRect)
                    CheckResult(GdApi.CGapiSurface_AlphaBltNoRect(Handle, ref pDestRect, srcSurface.Handle, null, alphaSurface.Handle, ref alphaRect, 0, null));
            }
        }
        public void AlphaBlt(ref GDRect pDestRect, GapiSurface srcSurface, ref GDRect srcRect, GapiSurface alphaSurface, ref GDRect alphaRect)
        {
            unsafe
            {
                fixed (GDRect* pSrcRect = &srcRect)
                fixed (GDRect* pAlphaRect = &alphaRect)
                    CheckResult(GdApi.CGapiSurface_AlphaBltNoRect(Handle, ref pDestRect, srcSurface.Handle, pSrcRect, alphaSurface.Handle, ref alphaRect, 0, null));
            }
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
        public unsafe void AlphaBltFast(int x, int y, GapiSurface surface, Rectangle surfaceRect,
            GapiSurface alphaSurface, Rectangle alphaRect)
        {
            GDRect gdAlpha = alphaRect, gdSurface = surfaceRect;
            CheckResult(GdApi.CGapiSurface_AlphaBltFast(
                Handle, x, y, surface.Handle, &gdSurface,
                alphaSurface.Handle, &gdAlpha, 0, null));
        }

        /// <summary>
        /// Blits a surface with an alpha surface component onto this surface at the given coordinates
        /// and opacity.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiSurface surface, Rectangle surfaceRect,
            GapiSurface alphaSurface, Rectangle alphaRect, int opacity)
        {
            GDRect gdAlpha = alphaRect, gdSurface = surfaceRect;

            AlphaBltFastFX fx;
            fx.Opacity = (uint)opacity;

            CheckResult(GdApi.CGapiSurface_AlphaBltFast(
                Handle, x, y, surface.Handle, &gdSurface,
                alphaSurface.Handle, &gdAlpha,
                AlphaBltFastOptions.Opacity, &fx));
        }

        /// <summary>
        /// Blits the entire given GapiRgbaSurface onto this surface at the given coordinates.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiRgbaSurface surface)
        {
            CheckResult(GdApi.CGapiSurface_AlphaBltFastRgba(
                Handle, x, y, surface.Handle, null, 0, null));
        }

        /// <summary>
        /// Blits the given GapiRgbaSurface region onto this surface at the given coordinates.
        /// </summary>
        public unsafe void AlphaBltFast(int x, int y, GapiRgbaSurface surface, Rectangle surfaceRect)
        {
            GDRect rect = surfaceRect;
            CheckResult(GdApi.CGapiSurface_AlphaBltFastRgba(
                Handle, x, y, surface.Handle, &rect, 0, null));
        }

        /// <summary>
        /// Blits the given GapiRgbaSurface region onto this surface at the given coordinates at the given
        /// opacity.
        /// </summary>
        /// <param name="opacity">Uses the opacity value in pGDABltFastFx->dwOpacity to adjust the overall weight of the alpha blend. Allowed range is from 0 (transparent) to 255 (opaque). If the opacity is set to 128, an optimized blending mode will be used.</param>
        public unsafe void AlphaBltFast(int x, int y, GapiRgbaSurface surface, Rectangle surfaceRect,
            int opacity)
        {
            GDRect rect = surfaceRect;
            AlphaBltFastFX fx;
            fx.Opacity = (uint)opacity;

            CheckResult(GdApi.CGapiSurface_AlphaBltFastRgba(
                Handle, x, y, surface.Handle, &rect,
                AlphaBltFastOptions.Opacity, &fx));
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
        public unsafe void DrawLine(int x1, int y1, int x2, int y2, Color color, bool antiAlias)
        {
            LineFX* pFX = null;
            DrawLineOptions options = 0;
            LineFX fx;

            if (color.A < 255)
            {
                options |= DrawLineOptions.Opacity;
                fx.Opacity = color.A;
                pFX = &fx;
            }

            if (antiAlias)
                options |= DrawLineOptions.AntiAlias;

            CheckResult(GdApi.CGapiSurface_DrawLine(
                Handle, x1, y1, x2, y2, color.ToColorRef(), options, pFX));
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills the entire area of this surface with the given color (which can contain opacity).
        /// </summary>
        public unsafe void Fill(Color color)
        {
            if (color.A < 255)
            {
                FillRectFX fx;
                fx.Opacity = color.A;
                CheckResult(GdApi.CGapiSurface_FillRect(
                    Handle, null, color.ToColorRef(), FillRectOptions.Opacity, &fx));
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
                FillRectFX fx;
                fx.Opacity = color.A;
                CheckResult(GdApi.CGapiSurface_FillRect(
                    Handle, &gdRect, color.ToColorRef(), FillRectOptions.Opacity, &fx));
            }
            else CheckResult(GdApi.CGapiSurface_FillRect(
                    Handle, &gdRect, color.ToColorRef(), 0, null));
        }

        #endregion

        #region Gradient

        /// <summary>
        /// Draws a filled gradient on the entire surface with the given options. The gradient must
        /// line up exactly with the bounds of this surface (see GapiDraw documentation).
        /// </summary>
        public unsafe void Gradient(GapiGradient gradient, GradientRectOptions options)
        {
            CheckResult(GdApi.CGapiSurface_GradientRect(
                Handle, null, gradient.Handle, options, null));
        }

        /// <summary>
        /// Draws a filled gradient rectangle on the surface with the given options. The gradient must
        /// line up exactly with the bounds of the rect (see GapiDraw documentation).
        /// </summary>
        public unsafe void GradientRect(Rectangle rect, GapiGradient gradient,
            GradientRectOptions options)
        {
            GDRect gdRect = rect;

            CheckResult(GdApi.CGapiSurface_GradientRect(
                Handle, &gdRect, gradient.Handle, options, null));
        }

        /// <summary>
        /// Draws a filled gradient rectangle on the surface with the given options and opacity.
        /// The gradient must line up exactly with the bounds of the rect (see GapiDraw documentation).
        /// </summary>
        public unsafe void GradientRect(Rectangle rect, GapiGradient gradient, 
            GradientRectOptions options, int opacity)
        {
            GDRect gdRect = rect;

            if (opacity < 255)
            {
                GradientRectFX fx;
                fx.Opacity = (uint)opacity;
                options |= GradientRectOptions.Opacity;

                CheckResult(GdApi.CGapiSurface_GradientRect(
                    Handle, &gdRect, gradient.Handle, options, &fx));
            }
            else GradientRect(rect, gradient, options);
        }

        #endregion

        /// <summary>
        /// Gets or sets the color to treat as transparent in this surface.
        /// </summary>
        public Color ColorKey
        {
            get
            {
                uint colorKey;
                CheckResult(GdApi.CGapiSurface_GetColorKey(Handle, out colorKey));
                return colorKey.ToColor();
            }
            set
            {
                CheckResult(GdApi.CGapiSurface_SetColorKey(Handle, value.ToColorRef()));
            }
        }

        /// <summary>
        /// Creates a GDI-compatible handle of a device context for the surface.
        /// </summary>
        /// <returns></returns>
        public IntPtr GetDC()
        {
            IntPtr result;
            CheckResult(GdApi.CGapiSurface_GetDC(Handle, out result));
            return result;
        }

        /// <summary>
        /// Releases the handle of a device context previously obtained by using the CGapiSurface::GetDC method.
        /// </summary>
        /// <param name="hDC"></param>
        public void ReleaseDC(IntPtr hDC)
        {
            CheckResult(GdApi.CGapiSurface_ReleaseDC(Handle, hDC));
        }

        /// <summary>
        /// This method obtains a pointer to the internal memory buffer used by this surface.
        /// The surface will be locked for your exclusive access to the buffer until you call
        /// ReleaseBuffer.
        /// </summary>
        public BufferInfo GetBuffer()
        {
            BufferInfo bufferInfo;
            CheckResult(GdApi.CGapiSurface_GetBuffer(Handle, out bufferInfo));
            return bufferInfo;
        }

        /// <summary>
        /// Releases the previously locked internal memory buffer used by this surface.
        /// </summary>
        public void ReleaseBuffer()
        {
            CheckResult(GdApi.CGapiSurface_ReleaseBuffer());
        }

        /// <summary>
        /// Locks the surface if it is stored in video memory.
        /// </summary>
        public void LockVideoSurface()
        {
            CheckResult(GdApi.CGapiSurface_LockVideoSurface(Handle));
        }

        /// <summary>
        /// Unlocks the surface if it is stored in video memory.
        /// </summary>
        public void UnlockVideoSurface()
        {
            CheckResult(GdApi.CGapiSurface_UnlockVideoSurface(Handle));
        }

        /// <summary>
        /// Saves the contents of the surface as a bitmap file.
        /// </summary>
        public void SaveSurface(string fileName)
        {
            CheckResult(GdApi.CGapiSurface_SaveSurface(Handle, Str(fileName), 0));
        }

        /// <summary>
        /// Saves the contents of the surface as a bitmap file.
        /// </summary>
        public void SaveSurface(string fileName, SaveSurfaceOptions options)
        {
            CheckResult(GdApi.CGapiSurface_SaveSurface(Handle, Str(fileName), options));
        }
		
        /// <summary>
        /// Gets the current flags for this surface.
        /// </summary>
        public SurfaceOptions SurfaceOptions
		{
            get
            {
                SurfaceOptions options;
                CheckResult(GdApi.CGapiSurface_GetSurfaceFlags(Handle, out options));
                return options;
            }
		}

        // Everything below is from the older Intuitex package, needs to be cleaned up to match above


        //		public UInt32 CGapiSurface_AlphaBlt (IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, ref GDRect pSrcRect, IntPtr pAlphaSurface, ref GDRect pAlphaRect, int dwFlags, ref GDALPHABLTFX pGDAlphaBltFx);
		

//		public UInt32 CGapiSurface_GetPixel (IntPtr pSurface, int dwX, int dwY, ref int pColor);
		public int GetPixel(int dwX, int dwY)
		{
			int result;
			GdApi.CGapiSurface_GetPixel(Handle, dwX, dwY, out result);
			return result;
		}
		
//		public UInt32 CGapiSurface_SetPixel (IntPtr pSurface, int dwX, int dwY, int dwColor);
		public void SetPixel(int dwX, int dwY, int dwColor)
		{
            CheckResult(GdApi.CGapiSurface_SetPixel(Handle, dwX, dwY, dwColor));
		}

		
//		public UInt32 CGapiSurface_SetPixelsArray(IntPtr pSurface, ref GDPIXEL pFirst, int dwElementSize, int dwElementCount, int dwFlags);
		public void SetPixels(Pixel[] pFirst, int dwElementSize, SetPixelOptions dwFlags)
		{
			CheckResult(GdApi.CGapiSurface_SetPixelsArray(Handle, ref pFirst[0], dwElementSize, pFirst.Length, (int)dwFlags));
		}

		public void SetPixels(Pixel[] pFirst, int dwElementSize)
		{
			CheckResult(GdApi.CGapiSurface_SetPixelsArray(Handle, ref pFirst[0], dwElementSize, pFirst.Length, 0));
		}

//		public UInt32 CGapiSurface_SetPixelsList(IntPtr pSurface, ref GDPIXELNODE pHead, int dwFlags);
		public void SetPixels(ref PixelNode pHead, int dwFlags)
		{
			CheckResult(GdApi.CGapiSurface_SetPixelsList(Handle, ref pHead, dwFlags));
		}

		public void SetPixelsArray(Pixel[] pFirst, int dwElementSize)
		{
			CheckResult(GdApi.CGapiSurface_SetPixelsArray(Handle, ref pFirst[0], dwElementSize, pFirst.Length, 0));
		}

//		public UInt32 CGapiSurface_DrawRect(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, ref GDLINEFX pGDLineFx);
		public void DrawRect(ref GDRect pRect, int dwColor, DrawLineOptions dwFlags, ref LineFX pGDLineFx)
		{
			CheckResult(GdApi.CGapiSurface_DrawRect(Handle, ref pRect, dwColor, (int)dwFlags, ref pGDLineFx));
		}
		
		public void DrawRect(ref GDRect pRect, int dwColor)
		{
			unsafe
			{
				CheckResult(GdApi.CGapiSurface_DrawRectNoOptions(Handle, ref pRect, dwColor, 0, null));
			}
		}

//		public static extern UInt32 CGapiSurface_DrawTextBitmapFont(IntPtr pSurface, int dwX, int dwY, string pString, IntPtr pFont, int dwTextFlags, int dwBltFastFlags, IntPtr pGDBltFastFx, out int pWidth);
		public int GetTextWidth(string drawString, GapiBitmapFont font)
		{
			int result;

			GdApi.CGapiBitmapFont_GetStringWidth (font.Handle, Str(drawString), out result);
			// TODO : fix GetTextWidth
			// GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_DrawTextBitmapFont(Handle, 0, 0, drawString, font.GapiObject, (int)DrawTextOptions.GDDRAWTEXT_CALCWIDTH, 0, IntPtr.Zero, out result));
			return result;
		}
		
//		public void DrawText(int dwX, int dwY, string drawString, GapiBitmapFont font, DrawTextOptions dwFlags, GDTEXTFX gDTextFx)
//		{
//			int dummy;
//			unsafe
//			{
//				IntPtr textFx = new IntPtr(&gDTextFx);
//				GapiUtility.RaiseExceptionOnError(GdNet.CGapiSurface_DrawTextGapiFont(Handle, dwX, dwY, drawString,  font.GapiObject, (int)dwFlags, textFx, out dummy));
//			}
//		}

		public int DrawText(int dwX, int dwY, string drawString, IntPtr pFont, DrawTextOptions dwFlags)
		{
			int result;
			// TODO : Overload with missing paramters
			result = (int)GdApi.CGapiSurface_DrawText(Handle, dwX, dwY, Str(drawString), pFont, (int)dwFlags,IntPtr.Zero, 0, IntPtr.Zero);
			return result;
		}

		public int DrawText(int dwX, int dwY, string drawString, GapiBitmapFont font, DrawTextOptions dwFlags)
		{
			int result;
			// TODO : Overload with missing paramters
			result = (int)GdApi.CGapiSurface_DrawText(Handle, dwX, dwY, Str(drawString), font.Handle, (int)dwFlags,IntPtr.Zero, 0, IntPtr.Zero);
			return result;
		}
		/*
//		public UInt32 Intersect(int dwX1, int dwY1, IntPtr pSrcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, IntPtr pSrcSurface2, ref GDRect pSrcRect2, ref System.Drawing.Point pIntersection);
		public static void Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, GapiSurface srcSurface2, ref GDRect pSrcRect2, ref System.Drawing.Point pIntersection)
		{
			CheckResult(GdApi.CGapiSurface_Intersect(dwX1, dwY1, srcSurface1.Handle, ref pSrcRect1, dwX2, dwY2, srcSurface2.Handle, ref pSrcRect2, ref pIntersection));
		}
		
		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, GapiSurface srcSurface2, ref GDRect pSrcRect2)
		{
			System.Drawing.Point point = new System.Drawing.Point(-1, -1);
			CheckResult(GdApi.CGapiSurface_Intersect(dwX1, dwY1, srcSurface1.Handle, ref pSrcRect1, dwX2, dwY2, srcSurface2.Handle, ref pSrcRect2, ref point));
			return point;
		}

		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, GapiSurface srcSurface2)
		{
			unsafe
			{
				System.Drawing.Point point = new System.Drawing.Point(-1, -1);
				
				fixed (GDRect* pSrcRect = &pSrcRect1)
					CheckResult(GdApi.CGapiSurface_IntersectNoRect(dwX1, dwY1, srcSurface1.Handle, pSrcRect, dwX2, dwY2, srcSurface2.Handle, null, ref point));
				return point;
			}
		}

		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, int dwX2, int dwY2, GapiSurface srcSurface2, ref GDRect pSrcRect2)
		{
			unsafe
			{
				System.Drawing.Point point = new System.Drawing.Point(-1, -1);
				
				fixed (GDRect* pSrcRect = &pSrcRect2)
					CheckResult(GdApi.CGapiSurface_IntersectNoRect(dwX1, dwY1, srcSurface1.Handle, null, dwX2, dwY2, srcSurface2.Handle, pSrcRect, ref point));
			
				return point;
			}

		}

		public static System.Drawing.Point Intersect(int dwX1, int dwY1, GapiSurface srcSurface1, int dwX2, int dwY2, GapiSurface srcSurface2)
		{
			System.Drawing.Point point = new System.Drawing.Point(-1, -1);
			unsafe
			{
				CheckResult(GdApi.CGapiSurface_IntersectNoRect(dwX1, dwY1, srcSurface1.Handle, null, dwX2, dwY2, srcSurface2.Handle, null, ref point));
			}
			return point;
		}*/

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
