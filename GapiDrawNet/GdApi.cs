using System;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	public class GdApi
    {
        const string GapiDraw = "GapiDraw.dll";

        #region GapiDraw

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDraw_Create();

        [DllImport(GapiDraw)]
        public static extern UInt32 CGapiDraw_Destroy(IntPtr pGapiDraw);

        #endregion

        #region GapiDisplay

        // Initialization

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_Create(IntPtr pGapiDraw);

        [DllImport(GapiDraw)]
        public static extern uint CGapiDisplay_Destroy(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern uint CGapiDisplay_OpenDisplay(IntPtr pDisplay,
            OpenDisplayOptions dwFlags, IntPtr hWnd, uint dwWidth, uint dwHeight, 
            uint dwZoomWidth, uint dwZoomHeight, uint dwBPP, uint dwHz);

        [DllImport(GapiDraw)]
        public static extern uint CGapiDisplay_CreateOffscreenDisplay(IntPtr pDisplay,
            uint dwFlags, uint dwWidth, uint dwHeight);

        [DllImport(GapiDraw)]
        public static extern uint CGapiDisplay_CloseDisplay(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern uint CGapiDisplay_SetDisplayMode(IntPtr pDisplay, DisplayMode dwMode);

        [DllImport(GapiDraw)]
        public static extern uint CGapiDisplay_GetDisplayMode(IntPtr pDisplay, out DisplayMode pMode);

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_GetBackBuffer(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern uint CGapiDisplay_RenderSystemFont(IntPtr pDisplay, uint dwColor);

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_GetSystemFont(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_GetSystemFontBorder(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern uint CGapiDisplay_DrawDisplayInformation(IntPtr pDisplay,
            IntPtr pDestinationSurface, IntPtr pBitmapFont);

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_GetDirectDrawInterface(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiDisplay_GetDirectDrawPrimarySurface(IntPtr pDisplay);

        // HW Operations

        [DllImport(GapiDraw)]
        public static extern uint CGapiDisplay_GetMonitorFrequency(IntPtr pDisplay, out uint pFrequency);

        #endregion

        #region GapiSurface

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiSurface_Create(IntPtr pGapiDraw);

        [DllImport(GapiDraw)]
        public static extern uint CGapiSurface_Destroy(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern uint CGapiSurface_CreateSurface(IntPtr pSurface,
            CreateSurfaceOptions dwFlags, uint dwWidth, uint dwHeight);

        [DllImport(GapiDraw)]
        public static extern uint CGapiSurface_CreateSurfaceFromFile(IntPtr pSurface,
            CreateSurfaceOptions dwFlags, byte[] pImageFile);

        [DllImport(GapiDraw)]
        public static extern uint CGapiSurface_CreateSurfaceFromMem(IntPtr pSurface,
            CreateSurfaceOptions dwFlags, byte[] imageBytes, uint dwImageFileSize);

        [DllImport(GapiDraw)]
        public static extern uint CGapiSurface_CreateSurfaceFromRes(IntPtr pSurface,
            CreateSurfaceOptions dwFlags, IntPtr hInstance, uint dwResourceID, byte[] pResourceType);

        [DllImport(GapiDraw)]
        public static extern uint CGapiSurface_CreateSurfaceFromSurface(IntPtr pSurface, 
            IntPtr pSrcSurface);

        // TODO: CreateSurface from GapiVFS

        [DllImport(GapiDraw)]
        public static extern uint CGapiSurface_GetWidth(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern uint CGapiSurface_GetHeight(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public unsafe static extern uint CGapiSurface_SetClipper(IntPtr pSurface,
            GDRect* pRect);

        [DllImport(GapiDraw)]
        public unsafe static extern uint CGapiSurface_Blt(IntPtr pSurface,
            GDRect* pDestRect, IntPtr pSrcSurface, GDRect* pSrcRect,
            BltOptions dwFlags, BltFX* pGDBltFx);

        [DllImport(GapiDraw)]
        public unsafe static extern uint CGapiSurface_BltFast(IntPtr pSurface,
            int destX, int destY, IntPtr pSrcSurface, GDRect* pSrcRect,
            BltFastOptions dwFlags, BltFastFX* pGDBltFastFx);

        [DllImport(GapiDraw)]
        public unsafe static extern uint CGapiSurface_AlphaBltFast(IntPtr pSurface,
            int destX, int destY, IntPtr pSrcSurface, GDRect* pSrcRect,
            IntPtr pAlphaSurface, GDRect* pAlphaRect,
            AlphaBltFastOptions dwFlags, AlphaBltFastFX* pGDABltFastFx);

        [DllImport(GapiDraw)]
        public unsafe static extern uint CGapiSurface_AlphaBltFastRgba(IntPtr pSurface,
            int destX, int destY, IntPtr pSrcSurface, GDRect* pSrcRect,
            AlphaBltFastOptions dwFlags, AlphaBltFastFX* pGDABltFastFx);

        [DllImport(GapiDraw)]
        public unsafe static extern uint CGapiSurface_DrawLine(IntPtr pSurface,
            int x1, int y1, int x2, int y2, uint dwColor,
            DrawLineOptions dwFlags, LineFX* pGDLineFx);

        [DllImport(GapiDraw)]
        public unsafe static extern uint CGapiSurface_FillRect(IntPtr pSurface,
            GDRect* pRect, uint dwColor,
            FillRectOptions dwFlags, FillRectFX* pGDFillRectFx);

        #endregion

        #region GapiRgbaSurface

        // Initialization

        [DllImport(GapiDraw)]
        public static extern IntPtr CGapiRGBASurface_Create(IntPtr pGapiDraw);

        [DllImport(GapiDraw)]
        public static extern uint CGapiRGBASurface_Destroy(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern uint CGapiRGBASurface_CreateSurface(IntPtr pSurface,
            RgbaSurfaceOptions dwFlags, uint dwWidth, uint dwHeight);

        [DllImport(GapiDraw)]
        public static extern uint CGapiRGBASurface_CreateSurfaceFromFile(IntPtr pSurface,
            RgbaSurfaceOptions dwFlags, byte[] pImageFile);

        [DllImport(GapiDraw)]
        public static extern uint CGapiRGBASurface_CreateSurfaceFromMem(IntPtr pSurface,
            RgbaSurfaceOptions dwFlags, byte[] bytes, uint dwImageFileSize);

        [DllImport(GapiDraw)]
        public static extern uint CGapiRGBASurface_CreateSurfaceFromRes(IntPtr pSurface,
            RgbaSurfaceOptions dwFlags, IntPtr hInstance, uint dwResourceID, byte[] pResourceType);

        [DllImport(GapiDraw)]
        public static extern uint CGapiRGBASurface_CreateSurfaceFromSurface(IntPtr pSurface,
            IntPtr pSrcSurface);

        // TODO: CreateSurface from GapiVFS

        // Surface characteristics

        [DllImport(GapiDraw)]
        public static extern uint CGapiRGBASurface_GetWidth(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern uint CGapiRGBASurface_GetHeight(IntPtr pSurface);

        [DllImport(GapiDraw)]
        public static extern uint CGapiRGBASurface_GetBuffer(IntPtr pSurface,
            out BufferInfo pGDBufferDesc);

        [DllImport(GapiDraw)]
        public static extern uint CGapiRGBASurface_ReleaseBuffer();

        #endregion

        // Everything below is from the older Intuitex package, needs to be cleaned up to match above

// NEW FOR 201

//
//		[DllImport("GapiDraw.dll")]
//		public static extern UInt32 CGapiDisplay_GetMonitorFrequency(IntPtr pDisplay, ref int pFrequency);
//
//		[DllImport("GapiDraw.dll")]
//		public static extern UInt32 CGapiDisplay_GetAvailableVidMem(IntPtr pDisplay, int dwFlags, ref int pTotal, ref int pFree);
//
//		[DllImport("GapiDraw.dll")]
//		public static extern UInt32 CGapiDisplay_GetHWStatus(IntPtr pDisplay, ref int pStatus);
//



        [DllImport(GapiDraw)]
        public static extern uint CGapiDisplay_Flip(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern UInt32 CGapiDisplay_SuspendDisplay(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern UInt32 CGapiDisplay_ResumeDisplay(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern UInt32 CGapiDisplay_DeviceToLogicalRect(IntPtr pDisplay, ref GDRect pRect);

        [DllImport(GapiDraw)]
        public static extern UInt32 CGapiDisplay_DeviceToLogicalPoint(IntPtr pDisplay, ref System.Drawing.Point pPoint);

        [DllImport(GapiDraw)]
        public static extern UInt32 CGapiDisplay_SurfacesAreLost(IntPtr pDisplay);

        [DllImport(GapiDraw)]
        public static extern UInt32 CGapiDisplay_RestoreAllVideoSurfaces(IntPtr pDisplay);

///    SAME AS IN GAPI 104

//		public static extern UInt32 CGapiSurface_GetColorKey(IntPtr pSurface, out int pColorKey);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_GetColorKey(IntPtr pSurface, out int pColorKey);


//		public static extern UInt32 CGapiSurface_SetColorKey(IntPtr pSurface, int dwColorKey);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_SetColorKey(IntPtr pSurface, int dwColorKey);



//		public static extern UInt32 CGapiSurface_Lock(IntPtr pSurface, ref GDRect pRect, ref GDSURFACEDESC pGDSurfaceDesc);
//

//		public static extern UInt32 CGapiSurface_Unlock(IntPtr pSurface, ref GDRect pRect);
//

//		public static extern UInt32 CGapiSurface_GetDC(IntPtr pSurface, out IntPtr pDC);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_GetDC(IntPtr pSurface, out IntPtr pDC);


//		public static extern UInt32 CGapiSurface_ReleaseDC(IntPtr pSurface, IntPtr hDC);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_ReleaseDC(IntPtr pSurface, IntPtr hDC);


//		public static extern UInt32 CGapiSurface_GetBuffer(IntPtr pSurface, ref GDBUFFERDESC pGDBufferDesc);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_GetBuffer(IntPtr pSurface, out BufferInfo pGDBufferDesc);


//		public static extern UInt32 CGapiSurface_ReleaseBuffer(IntPtr pSurface);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_ReleaseBuffer();

		[DllImport("GapiDraw.dll")]
		// HRESULT __stdcall CGapiSurface_GetClipper(CGapiSurface* pSurface, RECT* pRect);
		public static extern UInt32 CGapiSurface_GetClipper(IntPtr pSurface, ref GDRect pRect);


//		public static extern UInt32 CGapiSurface_SaveSurface(IntPtr pSurface, string pBitmapFile);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_SaveSurface(IntPtr pSurface, byte[] pBitmapFile, int dwFlags);


		[DllImport(GapiDraw)]
		public static extern uint CGapiSurface_GetSurfaceFlags(IntPtr pSurface, 
            out CreateSurfaceOptions pFlags);


//		public static extern UInt32 CGapiSurface_SetSurfaceOptions(IntPtr pSurface, int dwOptions);

		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_LockVideoSurface(IntPtr pSurface);

		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_UnlockVideoSurface(IntPtr pSurface);

//		public static extern UInt32 CGapiSurface_AlphaBlt(IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, ref GDRect pSrcRect, IntPtr pAlphaSurface, ref GDRect pAlphaRect, int dwFlags, ref GDALPHABLTFX pGDAlphaBltFx);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_AlphaBlt(IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, ref GDRect pSrcRect, IntPtr pAlphaSurface, ref GDRect pAlphaRect, int dwFlags, ref AlphaBltFX pGDAlphaBltFx);

//		[DllImport("GdNet104.DLL", EntryPoint = "CGapiSurface_AlphaBlt")]
		[DllImport("GapiDraw.dll", EntryPoint = "CGapiSurface_AlphaBlt")]
		unsafe public static extern UInt32 CGapiSurface_AlphaBltNoRect(IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, GDRect* pSrcRect, IntPtr pAlphaSurface, ref GDRect pAlphaRect, int dwFlags, AlphaBltFX* pGDAlphaBltFx);

		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_AlphaBltRgba(IntPtr pSurface, ref GDRect pDestRect, IntPtr pSrcSurface, ref GDRect pSrcRect, int dwFlags, ref AlphaBltFX pGDAlphaBltFx);


//		public static extern UInt32 CGapiSurface_GetPixel(IntPtr pSurface, int dwX, int dwY, out int pColor);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_GetPixel(IntPtr pSurface, int dwX, int dwY, out int pColor);


//		public static extern UInt32 CGapiSurface_SetPixel(IntPtr pSurface, int dwX, int dwY, int dwColor);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_SetPixel(IntPtr pSurface, int dwX, int dwY, int dwColor);


//		public static extern UInt32 CGapiSurface_SetPixelsArray(IntPtr pSurface, ref GDPIXEL pFirst, int dwElementSize, int dwElementCount, int dwFlags);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_SetPixelsArray(IntPtr pSurface, ref Pixel pFirst, int dwElementSize, int dwElementCount, int dwFlags);


//		public static extern UInt32 CGapiSurface_SetPixelsList(IntPtr pSurface, ref GDPIXELNODE pHead, int dwFlags);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_SetPixelsList(IntPtr pSurface, ref PixelNode pHead, int dwFlags);


//		public static extern UInt32 CGapiSurface_DrawRect(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, ref GDLINEFX pGDLineFx);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_DrawRect(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, ref LineFX pGDLineFx);

//		[DllImport("GdNet104.DLL", EntryPoint = "CGapiSurface_DrawRect")]
//		unsafe public static extern UInt32 CGapiSurface_DrawRectNoOptions(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, GDLINEFX *pGDLineFx);
		[DllImport("GapiDraw.dll", EntryPoint = "CGapiSurface_DrawRect")]
		unsafe public static extern UInt32 CGapiSurface_DrawRectNoOptions(IntPtr pSurface, ref GDRect pRect, int dwColor, int dwFlags, LineFX *pGDLineFx);
		
//		[DllImport("GapiDraw.dll")]
//		public static extern UInt32 CGapiSurface_RenderSystemFont(int dwColor);


// 201	HRESULT __stdcall CGapiSurface_DrawTextBitmapFont(CGapiSurface* pSurface, DWORD dwX, DWORD dwY, const TCHAR* pString, CGapiBitmapFont* pFont, DWORD dwTextFlags, DWORD dwBltFastFlags, GDBLTFASTFX* pGDBltFastFx,   DWORD* pWidth);
// 202	HRESULT __stdcall CGapiSurface_DrawText          (CGapiSurface* pSurface, LONG x,    LONG y,    const TCHAR* pString, CGapiBitmapFont* pFont, DWORD dwTextFlags, GDDRAWTEXTFX* pGDDrawTextFx, DWORD dwBltFastFlags, GDBLTFASTFX* pGDBltFastFx);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_DrawText(IntPtr pSurface, int dwX, int dwY, byte[] pString, IntPtr pFont, int dwTextFlags, IntPtr pGDDrawTextFx, int dwBltFastFlags, IntPtr pGDBltFastFx);

//		public static extern UInt32 CGapiSurface_Intersect(int dwX1, int dwY1, IntPtr pSrcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, IntPtr pSrcSurface2, ref GDRect pSrcRect2, ref System.Drawing.Point pIntersection);
		[DllImport("GapiDraw.dll", EntryPoint = "CGapiSurface_Intersect")]
		public static extern UInt32 CGapiSurface_Intersect(int dwX1, int dwY1, IntPtr pSrcSurface1, ref GDRect pSrcRect1, int dwX2, int dwY2, IntPtr pSrcSurface2, ref GDRect pSrcRect2, ref System.Drawing.Point pIntersection);

		[DllImport("GapiDraw.dll", EntryPoint = "CGapiSurface_Intersect")]
		unsafe public static extern UInt32 CGapiSurface_IntersectNoRect(int dwX1, int dwY1, IntPtr pSrcSurface1, GDRect *pSrcRect1, int dwX2, int dwY2, IntPtr pSrcSurface2, GDRect *pSrcRect2, ref System.Drawing.Point pIntersection);

		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_ColorrefToNative(IntPtr pSurface, int dwColor, out int pNative);

		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiSurface_NativeToColorref(IntPtr pSurface, int dwNative, out int pColor);



//	201  __stdcall CGapiDisplay_OpenDisplayByName(CGapiDisplay* pDisplay, const TCHAR* pWindow, DWORD dwFlags, DWORD dwWidth, DWORD dwHeight, DWORD dwZoomWidth, DWORD dwZoomHeight, DWORD dwBPP, DWORD dwHz);
//  202	 __stdcall CGapiDisplay_OpenDisplayByName(CGapiDisplay* pDisplay, DWORD dwFlags, const TCHAR* pWindow, DWORD dwWidth, DWORD dwHeight, DWORD dwZoomWidth, DWORD dwZoomHeight, DWORD dwBPP, DWORD dwHz);
//  300	 __stdcall CGapiDisplay_OpenDisplayByName(CGapiDisplay* pDisplay, DWORD dwFlags, const TCHAR* pWindow, DWORD dwWidth, DWORD dwHeight, DWORD dwZoomWidth, DWORD dwZoomHeight, DWORD dwBPP, DWORD dwHz);


//		public static extern IntPtr CGapiMaskSurface_Create();
		[DllImport("GapiDraw.dll")]
		public static extern IntPtr CGapiMaskSurface_Create();


//		public static extern UInt32 CGapiMaskSurface_Destroy(IntPtr pMaskSurface);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiMaskSurface_Destroy(IntPtr pMaskSurface);


//		public static extern UInt32 CGapiMaskSurface_DrawMaskImage(IntPtr pMaskSurface, int dwX, int dwY, IntPtr pSrcSurface, ref GDRect pSrcRect, int dwMaskID);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiMaskSurface_DrawMask(IntPtr pMaskSurface, int dwX, int dwY, IntPtr pSrcSurface, ref GDRect pSrcRect, int dwMaskID);


//		public static extern UInt32 CGapiMaskSurface_DrawMaskRect(IntPtr pMaskSurface, ref GDRect pRect, int dwMaskID);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiMaskSurface_DrawMaskRect(IntPtr pMaskSurface, ref GDRect pRect, int dwMaskID);


//		public static extern UInt32 CGapiMaskSurface_DrawMaskPixel(IntPtr pMaskSurface, int dwX, int dwY, int dwMaskID);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiMaskSurface_DrawMaskPixel(IntPtr pMaskSurface, int dwX, int dwY, int dwMaskID);


//		public static extern UInt32 CGapiMaskSurface_GetMaskID(IntPtr pMaskSurface, int dwX, int dwY, IntPtr pSrcSurface, ref GDRect pSrcRect, out int pMaskID, ref System.Drawing.Point pIntersection);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiMaskSurface_GetMaskID(IntPtr pMaskSurface, int dwX, int dwY, IntPtr pSrcSurface, ref GDRect pSrcRect, out int pMaskID, ref System.Drawing.Point pIntersection);


//		public static extern UInt32 CGapiMaskSurface_GetMaskIDRect(IntPtr pMaskSurface, ref GDRect pRect, out int pMaskID, ref System.Drawing.Point pIntersection);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiMaskSurface_GetMaskIDRect(IntPtr pMaskSurface, ref GDRect pRect, out int pMaskID, ref System.Drawing.Point pIntersection);


//		public static extern UInt32 CGapiMaskSurface_GetMaskIDPixel(IntPtr pMaskSurface, int dwX, int dwY, out int pMaskID);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiMaskSurface_GetMaskIDPixel(IntPtr pMaskSurface, int dwX, int dwY, out int pMaskID);


//		public static extern IntPtr CGapiBitmapFont_Create();
		[DllImport("GapiDraw.dll")]
		public static extern IntPtr CGapiBitmapFont_Create();


//		public static extern UInt32 CGapiBitmapFont_Destroy(IntPtr pBitmapFont);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiBitmapFont_Destroy(IntPtr pBitmapFont);


//		public static extern UInt32 CGapiBitmapFont_CreateFont(IntPtr pBitmapFont, int dwFlags, ref GDFONTFX pGDFontFx);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiBitmapFont_CreateFont(IntPtr pBitmapFont, byte[] pString, int dwColorKey, int dwFlags, ref FontFX pGDFontFx);

		[DllImport("GapiDraw.dll", EntryPoint = "CGapiBitmapFont_CreateFont")]
		public static extern UInt32 CGapiBitmapFont_CreateFont_NoString(IntPtr pBitmapFont, int GivemeZero, int dwColorKey, int dwFlags, ref FontFX pGDFontFx);


//		public static extern UInt32 CGapiBitmapFont_SetKerning(IntPtr pBitmapFont, char tcPreviousChar, char tcCharToAdjust, int lOffset);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiBitmapFont_SetKerning(IntPtr pBitmapFont, char tcPreviousChar, char tcCharToAdjust, int lOffset);

		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiBitmapFont_GetStringWidth (IntPtr pBitmapFont, byte[] pString, out int pWidth);

		// HRESULT __stdcall CGapiBitmapFont_GetCharWidth(CGapiBitmapFont* pBitmapFont, TCHAR tcChar, DWORD* pWidth);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiBitmapFont_GetCharWidth (IntPtr pBitmapFont, char tcChar, out int pWidth);

		// HRESULT __stdcall CGapiBitmapFont_GetSpacing(CGapiBitmapFont* pBitmapFont, TCHAR tcChar1, TCHAR tcChar2, DWORD* pSpacing);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiBitmapFont_GetSpacing (IntPtr pBitmapFont, char tcChar1, char tcChar2, out int pWidth);

//		public static extern IntPtr CGapiCursor_Create();
		[DllImport("GapiDraw.dll")]
		public static extern IntPtr CGapiCursor_Create();


//		public static extern UInt32 CGapiCursor_Destroy(IntPtr pCursor);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiCursor_Destroy(IntPtr pCursor);


//		public static extern UInt32 CGapiCursor_CreateCursor(IntPtr pCursor, int dwFlags, int dwFrameCount, int dwFrameStep);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiCursor_CreateCursor(IntPtr pCursor, int dwFlags, int dwFrameCount, int dwFrameStep);


//		public static extern UInt32 CGapiCursor_SetHotSpot(IntPtr pCursor, int dwX, int dwY);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiCursor_SetHotSpot(IntPtr pCursor, int dwX, int dwY);


//		public static extern UInt32 CGapiCursor_SetFrameIndex(IntPtr pCursor, int dwFrameIndex);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiCursor_SetFrameIndex(IntPtr pCursor, int dwFrameIndex);


//		public static extern UInt32 CGapiCursor_SetPosition(IntPtr pCursor, int dwX, int dwY);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiCursor_SetPosition(IntPtr pCursor, int dwX, int dwY);


//		public static extern UInt32 CGapiCursor_DrawCursor(IntPtr pCursor, IntPtr pDestSurface);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiCursor_DrawCursor(IntPtr pCursor, IntPtr pDestSurface);


//		public static extern IntPtr CGapiInput_Create();
		[DllImport("GapiDraw.dll")]
		public static extern IntPtr CGapiInput_Create();


//		public static extern UInt32 CGapiInput_Destroy(IntPtr pInput);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiInput_Destroy(IntPtr pInput);


//		public static extern UInt32 CGapiInput_GetKeyList(IntPtr pInput, ref GDKEYLIST pKeyList);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiInput_GetKeyList(IntPtr pInput, ref KeyList pKeyList);


//		public static extern UInt32 CGapiInput_OpenInput(IntPtr pInput);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiInput_OpenInput(IntPtr pInput);


//		public static extern UInt32 CGapiInput_CloseInput(IntPtr pInput);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiInput_CloseInput(IntPtr pInput);


//		public static extern IntPtr CGapiTimer_Create();
		[DllImport("GapiDraw.dll")]
		public static extern IntPtr CGapiTimer_Create();


//		public static extern UInt32 CGapiTimer_Destroy(IntPtr pTimer);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiTimer_Destroy(IntPtr pTimer);
//

//		public static extern UInt32 CGapiTimer_StartTimer(IntPtr pTimer, int dwTargetFrameRate);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiTimer_StartTimer(IntPtr pTimer, int dwTargetFrameRate);


//		public static extern UInt32 CGapiTimer_WaitForNextFrame(IntPtr pTimer);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiTimer_WaitForNextFrame(IntPtr pTimer);


//		public static extern UInt32 CGapiTimer_GetActualFrameRate(IntPtr pTimer, ref float pActualFrameRate);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiTimer_GetActualFrameRate(IntPtr pTimer, ref float pActualFrameRate);


//		public static extern UInt32 CGapiTimer_GetActualFrameTime(IntPtr pTimer, ref float pActualFrameTime);
		[DllImport("GapiDraw.dll")]
		public static extern UInt32 CGapiTimer_GetActualFrameTime(IntPtr pTimer, ref float pActualFrameTime);
	}
}