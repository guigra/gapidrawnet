using System;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for GapiDisplay.
	/// </summary>
	public class GapiDisplay : GapiSurface
	{

		public GapiDisplay()
		{
			unmanagedGapiObject =  GdNet.CGapiDisplay_Create(GapiDraw.GlobalHandle);
		}

		override public void Dispose()
		{
			IntPtr display = unmanagedGapiObject;
			unmanagedGapiObject = IntPtr.Zero;
			GdNet.CGapiDisplay_Destroy(display);
		}

		private GapiSurface backBuffer = new GapiSurface();
		public GapiSurface BackBuffer
		{
			get { return backBuffer; }
		}
		
		//public static extern UInt32 CGapiDisplay_OpenDisplay(IntPtr pDisplay, IntPtr hWnd, int dwFlags, int dwWidth, int dwHeight, int dwZoomWidth, int dwZoomHeight, int dwBPP, int dwHz);
		public void OpenDisplay(IntPtr hWnd, GapiDrawNet.OpenDisplayOptions flags)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_OpenDisplay(unmanagedGapiObject, (int)flags, hWnd, 240,320, 0, 0, 16, 0));
			GetBackBuffer();
		}

		//public static extern UInt32 CGapiDisplay_OpenDisplay(IntPtr pDisplay, IntPtr hWnd, int dwFlags, int dwWidth, int dwHeight, int dwZoomWidth, int dwZoomHeight, int dwBPP, int dwHz);
		public void OpenDisplay(IntPtr hWnd, GapiDrawNet.OpenDisplayOptions flags, int dwWidth, int dwHeight, int dwZoomWidth, int dwZoomHeight, int dwBPP, int dwHz)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_OpenDisplay(unmanagedGapiObject, (int)flags, hWnd, dwWidth,dwHeight, dwZoomWidth, dwZoomHeight, dwBPP, dwHz));
			GetBackBuffer();
		}

		
		// public static extern UInt32 CGapiDisplay_CreateOffscreenDisplay(IntPtr pDisplay, int dwFlags, int dwWidth, int dwHeight);
		public void CreateOffscreenDisplay(GapiDrawNet.OpenDisplayOptions flags, int dwWidth, int dwHeight)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_CreateOffscreenDisplay(unmanagedGapiObject, (int)flags, dwWidth, dwHeight));
			GetBackBuffer();
		}

		public void OpenDisplay(IntPtr hWnd)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_OpenDisplay(unmanagedGapiObject, 0, hWnd, 240,320, 0, 0, 16, 0));
			GetBackBuffer();
		}

		// public static extern UInt32 CGapiDisplay_OpenDisplayByName(IntPtr pDisplay, string pWindow, int dwFlags, int dwWidth, int dwHeight, int dwZoomWidth, int dwZoomHeight, int dwBPP, int dwHz);
		public void OpenDisplay(string windowName, GapiDrawNet.OpenDisplayOptions flags, int dwWidth, int dwHeight, int dwZoomWidth, int dwZoomHeight, int dwBPP, int dwHz)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_OpenDisplayByName(unmanagedGapiObject, (int)flags, windowName, dwWidth,dwHeight, dwZoomWidth, dwZoomHeight, dwBPP, dwHz));
			GetBackBuffer();
		}

		public void OpenDisplay(string windowName, GapiDrawNet.OpenDisplayOptions flags)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_OpenDisplayByName(unmanagedGapiObject, (int)flags, windowName, 240,320, 0, 0, 16, 0));
			GetBackBuffer();

		}

		private bool _displayOpen =false;

		public void OpenDisplay(string windowName)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_OpenDisplayByName(unmanagedGapiObject, 0, windowName, 240,320, 0, 0, 16, 0));
			GetBackBuffer();
		}

		//		public static extern UInt32 CGapiDisplay_CloseDisplay (IntPtr pDisplay);
		public void CloseDisplay()
		{
			if(_displayOpen)
			{
				GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_CloseDisplay(unmanagedGapiObject));
				_displayOpen = false;
			}
		}

		//		public static extern UInt32 CGapiDisplay_SetDisplayMode (IntPtr pDisplay, int dwMode);
		public void SetDisplayMode(GapiDrawNet.DisplayMode mode)
		{
			SystemFontPtr = IntPtr.Zero;
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_SetDisplayMode(unmanagedGapiObject, (int)mode));
			GetBackBuffer();
		}

		//		public static extern UInt32 CGapiDisplay_GetDisplayMode (IntPtr pDisplay, ref int pMode);
		public DisplayMode GetDisplayMode()
		{
			int mode = -1;
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_GetDisplayMode(unmanagedGapiObject, ref mode));
			return (DisplayMode)mode;

		}

		public DisplayMode Mode
		{
			get { return GetDisplayMode(); }
			set { SetDisplayMode(value); }
		}
		//		public static extern UInt32 CGapiDisplay_GetDisplayCaps (IntPtr pDisplay, ref int pCaps);
//		public DisplayCaps GetDisplayCaps()
//		{
//			int caps = -1;
//			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_GetDisplayCaps(unmanagedGapiObject, ref caps));
//			return (DisplayCaps)caps;
//
//		}

		//		public static extern UInt32 CGapiDisplay_GetBackBuffer (IntPtr pDisplay, IntPtr pBackbuffer);
		public GapiSurface GetBackBuffer()
		{
			//IntPtr pBackbuffer = new IntPtr(0);
			backBuffer.GapiObject = GdNet.CGapiDisplay_GetBackBuffer(unmanagedGapiObject);
			_displayOpen = true;
			return backBuffer;

		}


		public bool SurfacesAreLost()
		{
			return ((uint)GapiResults.GDERR_SURFACELOST) == GdNet.CGapiDisplay_SurfacesAreLost(unmanagedGapiObject);
		}

		public void  RestoreAllVideoSurfaces()
		{
			GdNet.CGapiDisplay_RestoreAllVideoSurfaces(unmanagedGapiObject);
		}

		//		public static extern UInt32 CGapiDisplay_Flip (IntPtr pDisplay);
		public void Flip()
		{
			uint hResult = GdNet.CGapiDisplay_Flip(unmanagedGapiObject);

			if(hResult == (uint)GapiResults.GD_OK){ return; }
			//if(hResult == (uint)GapiResults.GDERR_BACKBUFFERLOST)
			if(SurfacesAreLost()) 
			{
				RestoreAllVideoSurfaces();
				GetBackBuffer();
				return;
			}
			GapiUtility.RaiseExceptionOnError (hResult);
		}


		//		public static extern UInt32 CGapiDisplay_SuspendDisplay (IntPtr pDisplay);
		public void SuspendDisplay()
		{
			if(_displayOpen)
			{
				GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_SuspendDisplay(unmanagedGapiObject));
			}
		}

		//		public static extern UInt32 CGapiDisplay_ResumeDisplay (IntPtr pDisplay);
		public void ResumeDisplay()
		{
			if(_displayOpen)
			{
				GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_ResumeDisplay(unmanagedGapiObject));
			}
		}

		//		public static extern UInt32 CGapiDisplay_DeviceToLogicalRect (IntPtr pDisplay, ref GDRect pRect);
		public void DeviceToLogicalRect(ref GDRect pRect)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_DeviceToLogicalRect(unmanagedGapiObject, ref pRect));
		}

		//		public static extern UInt32 CGapiDisplay_DeviceToLogicalPoint (IntPtr pDisplay, ref System.Drawing.Point pPoint);
		public void DeviceToLogicalPoint(ref System.Drawing.Point pPoint)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_DeviceToLogicalPoint(unmanagedGapiObject, ref pPoint));
		}

		static public IntPtr SystemFontPtr = IntPtr.Zero;

		public int RenderSystemFont(int dwColor)
		{
			int result = (int)GdNet.CGapiDisplay_RenderSystemFont(unmanagedGapiObject, dwColor);
			SystemFontPtr = GetSystemFontPtr();
			return result;
		}


		//		public static extern UInt32 CGapiSurface_DrawTextSystemFont(IntPtr pSurface, int dwX, int dwY, string pString, int dwTextFlags, ref int pWidth);
		public int GetTextWidth(string drawString)
		{
			int result;
			GdNet.CGapiBitmapFont_GetStringWidth (GetSystemFontPtr(), drawString, out result);
			return result;
		}
		
		public IntPtr GetSystemFontPtr()
		{
			return GdNet.CGapiDisplay_GetSystemFont (unmanagedGapiObject);
		}

		public IntPtr GetSystemFontBorderPtr()
		{
			return GdNet.CGapiDisplay_GetSystemFontBorder(unmanagedGapiObject);
		}

		public void DrawOntoDcXp(IntPtr hdc, int x, int y)		
		{
//			IntPtr hdcSurf = BackBuffer.GetDC();
//
//			GdNet.BitBltXp(hdc, x, y, Width, Height, hdcSurf, 0, 0, 0x00CC0020);
//			    
//			BackBuffer.ReleaseDC(hdcSurf);
			IntPtr hdcSurf = BackBuffer.GetDC();

			if(hdcSurf != IntPtr.Zero)
			{
				try
				{
					GdNet.BitBltXp(hdc, x, y, Width, Height, hdcSurf, 0, 0, 0x00CC0020);
				}
				finally
				{
					BackBuffer.ReleaseDC(hdcSurf);
				}
			}   				
		}

		public void DrawOntoDcPpp(IntPtr hdc, int x, int y)		
		{
			IntPtr hdcSurf = BackBuffer.GetDC();

			if(hdcSurf != IntPtr.Zero && hdc  != IntPtr.Zero)
			{
				try
				{
					GdNet.BitBltPpp(hdc, x, y, Width, Height, hdcSurf, 0, 0, 0x00CC0020);
				}
				finally
				{
					BackBuffer.ReleaseDC(hdcSurf);
				}
			}   						
		}
//		public void DrawOntoControl(Control control, int x, int y)
//		{
//			IntPtr hdcSurf = BackBuffer.GetDC();
////			Graphics surf = Graphics.FromHdc(handle);
//			using (Graphics g = this.CreateGraphics())
//			{
//				IntPtr hdc = g.GetHdc();
//				try
//				{
//					
//					BitBlt(hdc, 0, 0, Width, Height, hdcSurf, 0, 0, 0x00CC0020);
//				}
//				finally
//				{
//					g.ReleaseHdc(hdc);
//				}
//            	
//			}
//			BackBuffer.ReleaseDC(hdcSurf);
//		}

//		public int DrawText(int dwX, int dwY, string drawString, DrawTextOptions dwFlags)
//		{
//			int result;
//			/// TODO : Fix add overloads!
//			if(SystemFontPtr == IntPtr.Zero)
//			{
//				return (int)GapiResults.GDERR_INVALIDPARAMS;
//			}
//
//			result = (int)GdNet.CGapiSurface_DrawText(unmanagedGapiObject, dwX, dwY, drawString, GetSystemFontPtr(), (int)dwFlags,IntPtr.Zero, 0, IntPtr.Zero);			
//			return result; //result;
//		}

//		public int DrawText(int dwX, int dwY, string drawString, int dwColor, DrawTextOptions dwFlags)
//		{
//			int result;
//			/// TODO : Fix
//
//			//public static extern UInt32 CGapiSurface_DrawTextSystemFont(IntPtr pSurface, int dwX, int dwY, string pString, int dwTextFlags, out int pWidth);
//			// GdNet.CGapiSurface_DrawTextSystemFont(unmanagedGapiObject, dwX, dwY, drawString, (int)dwFlags, out result);
//			result = (int)GdNet.CGapiSurface_DrawText(unmanagedGapiObject, dwX, dwY, drawString, GetSystemFontPtr(), (int)dwFlags,IntPtr.Zero, 0, IntPtr.Zero);			}
//			return result;
//		}
	}
}
