using System;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	/// <summary>
	/// CGapiDisplay is a representation of the display. It inherits all drawing functionality
    /// from CGapiSurface, and adds some extra features such as surface flipping and back buffer support.
	/// </summary>
	public class GapiDisplay : GapiSurface
	{
        public GapiDisplay()
            : base(GdNet.CGapiDisplay_Create(GapiDraw.GlobalHandle), true)
		{
        }

        protected override void DestroyGapiObject(IntPtr gapiObject)
        {
            CheckResult(GdNet.CGapiDisplay_Destroy(Handle));
        }

        public GapiSurface BackBuffer
        {
            get
            {
                IntPtr handle = GdNet.CGapiDisplay_GetBackBuffer(Handle);
                return handle != IntPtr.Zero ? new GapiSurface(handle, false) : null;
            }
        }

        #region OpenDisplay

        /// <summary>
        /// This method initializes the display-device hardware.
        /// </summary>
        /// <param name="hWnd">Window handle used for the application. Set to the calling application's top-level window handle (not a handle for any child windows created by the top-level window).</param>
        public void OpenDisplay(OpenDisplayOptions flags, IntPtr hWnd, int width, int height)
        {
            OpenDisplay(flags, hWnd, width, height, 0, 0, 0, 0);
        }

        /// <summary>
        /// This method initializes the display-device hardware.
        /// </summary>
        /// <param name="hWnd">Window handle used for the application. Set to the calling application's top-level window handle (not a handle for any child windows created by the top-level window).</param>
        public void OpenDisplay(OpenDisplayOptions flags, IntPtr hWnd,
            int width, int height, int zoomWidth, int zoomHeight, int bpp, int hz)
		{
            CheckResult(GdNet.CGapiDisplay_OpenDisplay(Handle, flags, hWnd, width, height,
                zoomWidth, zoomHeight, bpp, hz));
        }

        #endregion

        // public static extern UInt32 CGapiDisplay_CreateOffscreenDisplay(IntPtr pDisplay, int dwFlags, int dwWidth, int dwHeight);
		public void CreateOffscreenDisplay(GapiDrawNet.OpenDisplayOptions flags, int dwWidth, int dwHeight)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_CreateOffscreenDisplay(Handle, (int)flags, dwWidth, dwHeight));
		}

		// public static extern UInt32 CGapiDisplay_OpenDisplayByName(IntPtr pDisplay, string pWindow, int dwFlags, int dwWidth, int dwHeight, int dwZoomWidth, int dwZoomHeight, int dwBPP, int dwHz);
		public void OpenDisplay(string windowName, GapiDrawNet.OpenDisplayOptions flags, int dwWidth, int dwHeight, int dwZoomWidth, int dwZoomHeight, int dwBPP, int dwHz)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_OpenDisplayByName(Handle, (int)flags, windowName, dwWidth,dwHeight, dwZoomWidth, dwZoomHeight, dwBPP, dwHz));
		}

		public void OpenDisplay(string windowName, GapiDrawNet.OpenDisplayOptions flags)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_OpenDisplayByName(Handle, (int)flags, windowName, 240,320, 0, 0, 16, 0));

		}

		public void OpenDisplay(string windowName)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_OpenDisplayByName(Handle, 0, windowName, 240,320, 0, 0, 16, 0));
        }

        #region CloseDisplay

        public void CloseDisplay()
        {
            CheckResult(GdNet.CGapiDisplay_CloseDisplay(Handle));
        }

        #endregion

        #region DisplayMode

        public DisplayMode DisplayMode
		{
            get
            {
                DisplayMode mode;
                CheckResult(GdNet.CGapiDisplay_GetDisplayMode(Handle, out mode));
                return mode;
            }
            set
            {
                CheckResult(GdNet.CGapiDisplay_SetDisplayMode(Handle, value));
            }
        }

        #endregion

        //		public static extern UInt32 CGapiDisplay_GetDisplayCaps (IntPtr pDisplay, ref int pCaps);
//		public DisplayCaps GetDisplayCaps()
//		{
//			int caps = -1;
//			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_GetDisplayCaps(unmanagedGapiObject, ref caps));
//			return (DisplayCaps)caps;
//
//		}

		public bool SurfacesAreLost()
		{
			return ((uint)GapiResults.GDERR_SURFACELOST) == GdNet.CGapiDisplay_SurfacesAreLost(Handle);
		}

		public void  RestoreAllVideoSurfaces()
		{
			GdNet.CGapiDisplay_RestoreAllVideoSurfaces(Handle);
		}

		//		public static extern UInt32 CGapiDisplay_Flip (IntPtr pDisplay);
		public void Flip()
		{
			uint hResult = GdNet.CGapiDisplay_Flip(Handle);

			if(hResult == (uint)GapiResults.GD_OK){ return; }
			//if(hResult == (uint)GapiResults.GDERR_BACKBUFFERLOST)
			if(SurfacesAreLost()) 
			{
				RestoreAllVideoSurfaces();
				return;
			}
			GapiUtility.RaiseExceptionOnError (hResult);
		}


		//		public static extern UInt32 CGapiDisplay_SuspendDisplay (IntPtr pDisplay);
        public void SuspendDisplay()
        {
            GapiUtility.RaiseExceptionOnError(GdNet.CGapiDisplay_SuspendDisplay(Handle));
        }

		//		public static extern UInt32 CGapiDisplay_ResumeDisplay (IntPtr pDisplay);
        public void ResumeDisplay()
        {
            GapiUtility.RaiseExceptionOnError(GdNet.CGapiDisplay_ResumeDisplay(Handle));
        }

		//		public static extern UInt32 CGapiDisplay_DeviceToLogicalRect (IntPtr pDisplay, ref GDRect pRect);
		public void DeviceToLogicalRect(ref GDRect pRect)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_DeviceToLogicalRect(Handle, ref pRect));
		}

		//		public static extern UInt32 CGapiDisplay_DeviceToLogicalPoint (IntPtr pDisplay, ref System.Drawing.Point pPoint);
		public void DeviceToLogicalPoint(ref System.Drawing.Point pPoint)
		{
			GapiUtility.RaiseExceptionOnError (GdNet.CGapiDisplay_DeviceToLogicalPoint(Handle, ref pPoint));
		}

		static public IntPtr SystemFontPtr = IntPtr.Zero;

		public int RenderSystemFont(int dwColor)
		{
			int result = (int)GdNet.CGapiDisplay_RenderSystemFont(Handle, dwColor);
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
			return GdNet.CGapiDisplay_GetSystemFont (Handle);
		}

		public IntPtr GetSystemFontBorderPtr()
		{
			return GdNet.CGapiDisplay_GetSystemFontBorder(Handle);
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
