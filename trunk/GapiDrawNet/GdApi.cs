using System;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	static class GdApi
    {
        const string GapiDraw = "GapiDraw.dll";

        #region GapiDraw

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDraw_Create();

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDraw_Destroy(IntPtr pGapiDraw);

        #endregion

        #region GapiDisplay

        // Initialization

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_Create(IntPtr pGapiDraw);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_Destroy(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_OpenDisplay(IntPtr pDisplay,
            OpenDisplayOptions dwFlags, IntPtr hWnd, uint dwWidth, uint dwHeight, 
            uint dwZoomWidth, uint dwZoomHeight, uint dwBPP, uint dwHz);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_CreateOffscreenDisplay(IntPtr pDisplay,
            uint dwFlags, uint dwWidth, uint dwHeight);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_CloseDisplay(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_SetDisplayMode(IntPtr pDisplay, DisplayMode dwMode);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_GetDisplayMode(IntPtr pDisplay, out DisplayMode pMode);

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_GetBackBuffer(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_RenderSystemFont(IntPtr pDisplay, uint dwColor);

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_GetSystemFont(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_GetSystemFontBorder(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_DrawDisplayInformation(IntPtr pDisplay,
            IntPtr pDestinationSurface, IntPtr pBitmapFont);

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_GetDirectDrawInterface(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_GetDirectDrawPrimarySurface(IntPtr pDisplay);

        // HW Operations

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_GetMonitorFrequency(IntPtr pDisplay,
            out uint pFrequency);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiDisplay_GetAvailableVidMem(IntPtr pDisplay,
            CreateSurfaceOptions dwFlags, out int pTotal, out int pFree);

        #endregion

        #region GapiSurface

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiSurface_Create(IntPtr pGapiDraw);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiSurface_Destroy(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiSurface_CreateSurface(IntPtr pSurface,
            CreateSurfaceOptions dwFlags, uint dwWidth, uint dwHeight);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiSurface_CreateSurfaceFromFile(IntPtr pSurface,
            CreateSurfaceOptions dwFlags, byte[] pImageFile);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiSurface_CreateSurfaceFromMem(IntPtr pSurface,
            CreateSurfaceOptions dwFlags, byte[] imageBytes, uint dwImageFileSize);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiSurface_CreateSurfaceFromRes(IntPtr pSurface,
            CreateSurfaceOptions dwFlags, IntPtr hInstance, uint dwResourceID, byte[] pResourceType);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiSurface_CreateSurfaceFromSurface(IntPtr pSurface, 
            IntPtr pSrcSurface);

        // TODO: CreateSurface from GapiVFS

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiSurface_GetWidth(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiSurface_GetHeight(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public unsafe static extern GapiResult CGapiSurface_SetClipper(IntPtr pSurface,
            GDRect* pRect);

        [DllImport(GapiDraw)]
        public unsafe static extern GapiResult CGapiSurface_Blt(IntPtr pSurface,
            GDRect* pDestRect, IntPtr pSrcSurface, GDRect* pSrcRect,
            BltOptions dwFlags, BltFX* pGDBltFx);

        [DllImport(GapiDraw)]
        public unsafe static extern GapiResult CGapiSurface_BltFast(IntPtr pSurface,
            int destX, int destY, IntPtr pSrcSurface, GDRect* pSrcRect,
            BltFastOptions dwFlags, BltFastFX* pGDBltFastFx);

        [DllImport(GapiDraw)]
        public unsafe static extern GapiResult CGapiSurface_AlphaBlt(IntPtr pSurface, 
            GDRect* pDestRect, IntPtr pSrcSurface, GDRect* pSrcRect, 
            IntPtr pAlphaSurface, GDRect* pAlphaRect, AlphaBltOptions dwFlags, AlphaBltFX* pGDAlphaBltFx);

        [DllImport(GapiDraw)]
        public unsafe static extern GapiResult CGapiSurface_AlphaBltFast(IntPtr pSurface,
            int destX, int destY, IntPtr pSrcSurface, GDRect* pSrcRect,
            IntPtr pAlphaSurface, GDRect* pAlphaRect,
            AlphaBltFastOptions dwFlags, AlphaBltFastFX* pGDABltFastFx);

        [DllImport(GapiDraw)]
        public unsafe static extern GapiResult CGapiSurface_AlphaBltFastRgba(IntPtr pSurface,
            int destX, int destY, IntPtr pSrcSurface, GDRect* pSrcRect,
            AlphaBltFastOptions dwFlags, AlphaBltFastFX* pGDABltFastFx);

        [DllImport(GapiDraw)]
        public unsafe static extern GapiResult CGapiSurface_DrawLine(IntPtr pSurface,
            int x1, int y1, int x2, int y2, uint dwColor,
            DrawLineOptions dwFlags, LineFX* pGDLineFx);

        [DllImport(GapiDraw)]
        public unsafe static extern GapiResult CGapiSurface_FillRect(IntPtr pSurface,
            GDRect* pRect, uint dwColor,
            FillRectOptions dwFlags, FillRectFX* pGDFillRectFx);

        #endregion

        #region GapiRgbaSurface

        // Initialization

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiRGBASurface_Create(IntPtr pGapiDraw);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiRGBASurface_Destroy(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiRGBASurface_CreateSurface(IntPtr pSurface,
            RgbaSurfaceOptions dwFlags, uint dwWidth, uint dwHeight);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiRGBASurface_CreateSurfaceFromFile(IntPtr pSurface,
            RgbaSurfaceOptions dwFlags, byte[] pImageFile);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiRGBASurface_CreateSurfaceFromMem(IntPtr pSurface,
            RgbaSurfaceOptions dwFlags, byte[] bytes, uint dwImageFileSize);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiRGBASurface_CreateSurfaceFromRes(IntPtr pSurface,
            RgbaSurfaceOptions dwFlags, IntPtr hInstance, uint dwResourceID, byte[] pResourceType);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiRGBASurface_CreateSurfaceFromSurface(IntPtr pSurface,
            IntPtr pSrcSurface);

        // TODO: CreateSurface from GapiVFS

        // Surface characteristics

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiRGBASurface_GetWidth(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiRGBASurface_GetHeight(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiRGBASurface_GetBuffer(IntPtr pSurface,
            out BufferInfo pGDBufferDesc);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiRGBASurface_ReleaseBuffer();

        #endregion

        // Everything below is from the older Intuitex package, needs to be cleaned up to match above

// NEW FOR 201

//
//		[DllImport(GapiDraw)]
//		public static extern GapiResult CGapiDisplay_GetMonitorFrequency(IntPtr pDisplay, ref int pFrequency);
//
//
//		[DllImport(GapiDraw)]
//		public static extern GapiResult CGapiDisplay_GetHWStatus(IntPtr pDisplay, ref int pStatus);
//



        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_Flip(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_SuspendDisplay(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_ResumeDisplay(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_DeviceToLogicalRect(IntPtr pDisplay, ref GDRect pRect);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_DeviceToLogicalPoint(IntPtr pDisplay, ref System.Drawing.Point pPoint);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_SurfacesAreLost(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiDisplay_RestoreAllVideoSurfaces(IntPtr pDisplay);

///    SAME AS IN GAPI 104


        [DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_GetColorKey(IntPtr pSurface, out uint pColorKey);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_SetColorKey(IntPtr pSurface, uint dwColorKey);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_GetDC(IntPtr pSurface, out IntPtr pDC);

        [DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_ReleaseDC(IntPtr pSurface, IntPtr hDC);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_GetBuffer(IntPtr pSurface, out BufferInfo pGDBufferDesc);

        [DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_ReleaseBuffer();

        [DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_GetClipper(IntPtr pSurface, out GDRect pRect);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiSurface_SaveSurface(IntPtr pSurface, 
            byte[] pBitmapFile, SaveSurfaceOptions dwFlags);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_GetSurfaceFlags(IntPtr pSurface, out SurfaceOptions pFlags);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_LockVideoSurface(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiSurface_UnlockVideoSurface(IntPtr pSurface);



//		public static extern GapiResult CGapiSurface_AlphaBlt(IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, ref GDRect pSrcRect, IntPtr pAlphaSurface, ref GDRect pAlphaRect, int dwFlags, ref GDALPHABLTFX pGDAlphaBltFx);
		//[DllImport(GapiDraw)]
		//public static extern GapiResult CGapiSurface_AlphaBlt(IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, ref GDRect pSrcRect, IntPtr pAlphaSurface, ref GDRect pAlphaRect, int dwFlags, ref AlphaBltFX pGDAlphaBltFx);

//		[DllImport("GdNet104.DLL", EntryPoint = "CGapiSurface_AlphaBlt")]
		[DllImport(GapiDraw, EntryPoint = "CGapiSurface_AlphaBlt")]
		public unsafe static extern GapiResult CGapiSurface_AlphaBltNoRect(IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, GDRect* pSrcRect, IntPtr pAlphaSurface, ref GDRect pAlphaRect, int dwFlags, AlphaBltFX* pGDAlphaBltFx);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_AlphaBltRgba(IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, ref GDRect pSrcRect, int dwFlags, ref AlphaBltFX pGDAlphaBltFx);


//		public static extern GapiResult CGapiSurface_GetPixel(IntPtr pSurface, int dwX, int dwY, out int pColor);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_GetPixel(IntPtr pSurface, int dwX, int dwY, out int pColor);


//		public static extern GapiResult CGapiSurface_SetPixel(IntPtr pSurface, int dwX, int dwY, int dwColor);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_SetPixel(IntPtr pSurface, int dwX, int dwY, int dwColor);


//		public static extern GapiResult CGapiSurface_SetPixelsArray(IntPtr pSurface, ref GDPIXEL pFirst, int dwElementSize, int dwElementCount, int dwFlags);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_SetPixelsArray(IntPtr pSurface, ref Pixel pFirst, int dwElementSize, int dwElementCount, int dwFlags);


//		public static extern GapiResult CGapiSurface_SetPixelsList(IntPtr pSurface, ref GDPIXELNODE pHead, int dwFlags);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_SetPixelsList(IntPtr pSurface, ref PixelNode pHead, int dwFlags);


//		public static extern GapiResult CGapiSurface_DrawRect(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, ref GDLINEFX pGDLineFx);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_DrawRect(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, ref LineFX pGDLineFx);

//		[DllImport("GdNet104.DLL", EntryPoint = "CGapiSurface_DrawRect")]
//		unsafe public static extern GapiResult CGapiSurface_DrawRectNoOptions(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, GDLINEFX *pGDLineFx);
		[DllImport(GapiDraw, EntryPoint = "CGapiSurface_DrawRect")]
		public unsafe static extern GapiResult CGapiSurface_DrawRectNoOptions(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, LineFX *pGDLineFx);
		
//		[DllImport(GapiDraw)]
//		public static extern GapiResult CGapiSurface_RenderSystemFont(int dwColor);


// 201	HRESULT __stdcall CGapiSurface_DrawTextBitmapFont(CGapiSurface* pSurface, DWORD dwX, DWORD dwY, const TCHAR* pString, CGapiBitmapFont* pFont, DWORD dwTextFlags, DWORD dwBltFastFlags, GDBLTFASTFX* pGDBltFastFx,   DWORD* pWidth);
// 202	HRESULT __stdcall CGapiSurface_DrawText          (CGapiSurface* pSurface, LONG x,    LONG y,    const TCHAR* pString, CGapiBitmapFont* pFont, DWORD dwTextFlags, GDDRAWTEXTFX* pGDDrawTextFx, DWORD dwBltFastFlags, GDBLTFASTFX* pGDBltFastFx);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_DrawText(IntPtr pSurface, int dwX, int dwY, byte[] pString, IntPtr pFont, int dwTextFlags, IntPtr pGDDrawTextFx, int dwBltFastFlags, IntPtr pGDBltFastFx);

//		public static extern GapiResult CGapiSurface_Intersect(int dwX1, int dwY1, IntPtr pSrcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, IntPtr pSrcSurface2, ref GDRect pSrcRect2, ref System.Drawing.Point pIntersection);
		[DllImport(GapiDraw, EntryPoint = "CGapiSurface_Intersect")]
		public static extern GapiResult CGapiSurface_Intersect(int dwX1, int dwY1, IntPtr pSrcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, IntPtr pSrcSurface2, ref GDRect pSrcRect2, ref System.Drawing.Point pIntersection);

		[DllImport(GapiDraw, EntryPoint = "CGapiSurface_Intersect")]
		public unsafe static extern GapiResult CGapiSurface_IntersectNoRect(int dwX1, int dwY1, IntPtr pSrcSurface1, GDRect *pSrcRect1, int dwX2, int dwY2, IntPtr pSrcSurface2, GDRect *pSrcRect2, ref System.Drawing.Point pIntersection);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_ColorrefToNative(IntPtr pSurface, int dwColor, out int pNative);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiSurface_NativeToColorref(IntPtr pSurface, int dwNative, out int pColor);



//	201  __stdcall CGapiDisplay_OpenDisplayByName(CGapiDisplay* pDisplay, const TCHAR* pWindow, DWORD dwFlags, DWORD dwWidth, DWORD dwHeight, DWORD dwZoomWidth, DWORD dwZoomHeight, DWORD dwBPP, DWORD dwHz);
//  202	 __stdcall CGapiDisplay_OpenDisplayByName(CGapiDisplay* pDisplay, DWORD dwFlags, const TCHAR* pWindow, DWORD dwWidth, DWORD dwHeight, DWORD dwZoomWidth, DWORD dwZoomHeight, DWORD dwBPP, DWORD dwHz);
//  300	 __stdcall CGapiDisplay_OpenDisplayByName(CGapiDisplay* pDisplay, DWORD dwFlags, const TCHAR* pWindow, DWORD dwWidth, DWORD dwHeight, DWORD dwZoomWidth, DWORD dwZoomHeight, DWORD dwBPP, DWORD dwHz);


//		public static extern IntPtr CGapiMaskSurface_Create();
		[DllImport(GapiDraw)]
		public static extern IntPtr CGapiMaskSurface_Create();


//		public static extern GapiResult CGapiMaskSurface_Destroy(IntPtr pMaskSurface);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiMaskSurface_Destroy(IntPtr pMaskSurface);


//		public static extern GapiResult CGapiMaskSurface_DrawMaskImage(IntPtr pMaskSurface, int dwX, int dwY, IntPtr pSrcSurface, ref GDRect pSrcRect, int dwMaskID);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiMaskSurface_DrawMask(IntPtr pMaskSurface, int dwX, int dwY, IntPtr pSrcSurface, ref GDRect pSrcRect, int dwMaskID);


//		public static extern GapiResult CGapiMaskSurface_DrawMaskRect(IntPtr pMaskSurface, ref GDRect pRect, int dwMaskID);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiMaskSurface_DrawMaskRect(IntPtr pMaskSurface, ref GDRect pRect, int dwMaskID);


//		public static extern GapiResult CGapiMaskSurface_DrawMaskPixel(IntPtr pMaskSurface, int dwX, int dwY, int dwMaskID);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiMaskSurface_DrawMaskPixel(IntPtr pMaskSurface, int dwX, int dwY, int dwMaskID);


//		public static extern GapiResult CGapiMaskSurface_GetMaskID(IntPtr pMaskSurface, int dwX, int dwY, IntPtr pSrcSurface, ref GDRect pSrcRect, out int pMaskID, ref System.Drawing.Point pIntersection);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiMaskSurface_GetMaskID(IntPtr pMaskSurface, int dwX, int dwY, IntPtr pSrcSurface, ref GDRect pSrcRect, out int pMaskID, ref System.Drawing.Point pIntersection);


//		public static extern GapiResult CGapiMaskSurface_GetMaskIDRect(IntPtr pMaskSurface, ref GDRect pRect, out int pMaskID, ref System.Drawing.Point pIntersection);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiMaskSurface_GetMaskIDRect(IntPtr pMaskSurface, ref GDRect pRect, out int pMaskID, ref System.Drawing.Point pIntersection);


//		public static extern GapiResult CGapiMaskSurface_GetMaskIDPixel(IntPtr pMaskSurface, int dwX, int dwY, out int pMaskID);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiMaskSurface_GetMaskIDPixel(IntPtr pMaskSurface, int dwX, int dwY, out int pMaskID);


		[DllImport(GapiDraw)]
		public static extern IntPtr CGapiBitmapFont_Create();

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiBitmapFont_Destroy(IntPtr pBitmapFont);

        [DllImport(GapiDraw)]
        public unsafe static extern GapiResult CGapiBitmapFont_CreateFont(IntPtr pBitmapFont,
            byte[] pString, uint dwColorKey, CreateFontOptions dwFlags, FontFX* pGDFontFx);


//		public static extern GapiResult CGapiBitmapFont_CreateFont(IntPtr pBitmapFont, int dwFlags, ref GDFONTFX pGDFontFx);

		//[DllImport(GapiDraw, EntryPoint = "CGapiBitmapFont_CreateFont")]
		//public static extern GapiResult CGapiBitmapFont_CreateFont_NoString(IntPtr pBitmapFont, int GivemeZero, int dwColorKey, int dwFlags, ref FontFX pGDFontFx);


//		public static extern GapiResult CGapiBitmapFont_SetKerning(IntPtr pBitmapFont, char tcPreviousChar, char tcCharToAdjust, int lOffset);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiBitmapFont_SetKerning(IntPtr pBitmapFont, char tcPreviousChar, char tcCharToAdjust, int lOffset);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiBitmapFont_GetStringWidth (IntPtr pBitmapFont, byte[] pString, out int pWidth);

		// HRESULT __stdcall CGapiBitmapFont_GetCharWidth(CGapiBitmapFont* pBitmapFont, TCHAR tcChar, DWORD* pWidth);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiBitmapFont_GetCharWidth (IntPtr pBitmapFont, char tcChar, out int pWidth);

		// HRESULT __stdcall CGapiBitmapFont_GetSpacing(CGapiBitmapFont* pBitmapFont, TCHAR tcChar1, TCHAR tcChar2, DWORD* pSpacing);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiBitmapFont_GetSpacing (IntPtr pBitmapFont, char tcChar1, char tcChar2, out int pWidth);


		[DllImport(GapiDraw)]
		public static extern IntPtr CGapiCursor_Create();

        [DllImport(GapiDraw)]
		public static extern GapiResult CGapiCursor_Destroy(IntPtr pCursor);

        [DllImport(GapiDraw)]
		public static extern GapiResult CGapiCursor_CreateCursor(IntPtr pCursor,
            uint dwFlags, uint dwFrameCount, uint dwFrameStep);


//		public static extern GapiResult CGapiCursor_SetHotSpot(IntPtr pCursor, int dwX, int dwY);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiCursor_SetHotSpot(IntPtr pCursor, int dwX, int dwY);


//		public static extern GapiResult CGapiCursor_SetFrameIndex(IntPtr pCursor, int dwFrameIndex);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiCursor_SetFrameIndex(IntPtr pCursor, int dwFrameIndex);


//		public static extern GapiResult CGapiCursor_SetPosition(IntPtr pCursor, int dwX, int dwY);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiCursor_SetPosition(IntPtr pCursor, int dwX, int dwY);


//		public static extern GapiResult CGapiCursor_DrawCursor(IntPtr pCursor, IntPtr pDestSurface);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiCursor_DrawCursor(IntPtr pCursor, IntPtr pDestSurface);



        [DllImport(GapiDraw)]
		public static extern IntPtr CGapiInput_Create();

        [DllImport(GapiDraw)]
        public static extern GapiResult CGapiInput_Destroy(IntPtr pInput);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiInput_GetKeyList(IntPtr pInput, out KeyList pKeyList);

        [DllImport(GapiDraw)]
		public static extern GapiResult CGapiInput_OpenInput(IntPtr pInput);

		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiInput_CloseInput(IntPtr pInput);


//		public static extern IntPtr CGapiTimer_Create();
		[DllImport(GapiDraw)]
		public static extern IntPtr CGapiTimer_Create();


//		public static extern GapiResult CGapiTimer_Destroy(IntPtr pTimer);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiTimer_Destroy(IntPtr pTimer);
//

//		public static extern GapiResult CGapiTimer_StartTimer(IntPtr pTimer, int dwTargetFrameRate);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiTimer_StartTimer(IntPtr pTimer, int dwTargetFrameRate);


//		public static extern GapiResult CGapiTimer_WaitForNextFrame(IntPtr pTimer);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiTimer_WaitForNextFrame(IntPtr pTimer);


//		public static extern GapiResult CGapiTimer_GetActualFrameRate(IntPtr pTimer, ref float pActualFrameRate);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiTimer_GetActualFrameRate(IntPtr pTimer, ref float pActualFrameRate);


//		public static extern GapiResult CGapiTimer_GetActualFrameTime(IntPtr pTimer, ref float pActualFrameTime);
		[DllImport(GapiDraw)]
		public static extern GapiResult CGapiTimer_GetActualFrameTime(IntPtr pTimer, ref float pActualFrameTime);
	}
}