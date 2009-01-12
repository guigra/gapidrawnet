using System;
using System.Drawing;

namespace GapiDrawNet
{
	/// <summary>
	/// CGapiDisplay is a representation of the display. It inherits all drawing functionality
    /// from CGapiSurface, and adds some extra features such as surface flipping and back buffer support.
	/// </summary>
	public class GapiDisplay : GapiSurface
	{
        /// <summary>
        /// Creates a new GapiDisplay.
        /// </summary>
        public GapiDisplay() { }

        protected override IntPtr CreateHandle()
        {
            return GdApi.CGapiDisplay_Create(GapiDraw.GlobalHandle);
        }

        protected override GapiResult DestroyHandle()
        {
            return GdApi.CGapiDisplay_Destroy(Handle);
        }

        public GapiSurface BackBuffer
        {
            get
            {
                IntPtr handle = GdApi.CGapiDisplay_GetBackBuffer(Handle);
                return handle != IntPtr.Zero ? new GapiSurface(handle) : null;
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
            CheckResult(GdApi.CGapiDisplay_OpenDisplay(Handle, flags, hWnd, (uint)width, (uint)height,
                (uint)zoomWidth, (uint)zoomHeight, (uint)bpp, (uint)hz));
        }

        /// <summary>
        /// Creates a HDC compatible back buffer for blitting to a window.
        /// </summary>
        public void CreateOffscreenDisplay(int width, int height)
        {
            // The "flags" in this function are "Reserved for future use - set to zero" according to docs
            CheckResult(GdApi.CGapiDisplay_CreateOffscreenDisplay(Handle, 0, (uint)width, (uint)height));
        }

        #endregion

        #region CloseDisplay

        public void CloseDisplay()
        {
            CheckResult(GdApi.CGapiDisplay_CloseDisplay(Handle));
        }

        #endregion

        #region DisplayMode

        public DisplayMode DisplayMode
        {
            get
            {
                DisplayMode mode;
                CheckResult(GdApi.CGapiDisplay_GetDisplayMode(Handle, out mode));
                return mode;
            }
            set
            {
                CheckResult(GdApi.CGapiDisplay_SetDisplayMode(Handle, value));
            }
        }

        #endregion

        /// <summary>
        /// Draws display information to a surface that can help developers work around device issues.
        /// </summary>
        /// <param name="destinationSurface">Surface upon which CGapiDisplay will output debug information.</param>
        /// <param name="font">The font used to draw the text. If set to NULL, CGapiDisplay will use the default system font stored in system memory.</param>
        public void DrawDisplayInformation(GapiSurface destinationSurface, GapiBitmapFont font)
        {
            CheckResult(GdApi.CGapiDisplay_DrawDisplayInformation(Handle, 
                destinationSurface.Handle, font != null ? font.Handle : IntPtr.Zero));
        }

        /// <summary>
        /// This method re-renders the system font in a user-specified color.
        /// </summary>
        /// <remarks>
        /// The system font can be of a user-specified color and with an optional black border. If you need another border color, or in other ways need to modify this font, it is delivered in a bitmap format with the GapiDraw distribution.
        /// </remarks>
        public void RenderSystemFont(Color color)
        {
            CheckResult(GdApi.CGapiDisplay_RenderSystemFont(Handle, color.ToColorRef()));
        }

        /// <summary>
        /// Gets a temporary pointer to the built-in system font.
        /// </summary>
        public GapiBitmapFont SystemFont
        {
            get
            {
                IntPtr systemFont = GdApi.CGapiDisplay_GetSystemFont(Handle);
                return systemFont != IntPtr.Zero ? new GapiBitmapFont(systemFont) : null;
            }
        }

        /// <summary>
        /// Gets a temporary pointer to the built-in system font with border.
        /// </summary>
        public GapiBitmapFont SystemFontBorder
        {
            get
            {
                IntPtr systemFont = GdApi.CGapiDisplay_GetSystemFontBorder(Handle);
                return systemFont != IntPtr.Zero ? new GapiBitmapFont(systemFont) : null;
            }
        }

        /// <summary>
        /// Checks the video hardware to see if any surfaces have been lost in a previous operation.
        /// </summary>
        public bool SurfacesLost
        {
            get
            {
                GapiResult result = GdApi.CGapiDisplay_SurfacesAreLost(Handle);

                switch (result)
                {
                    case GapiResult.Ok: return false;
                    case GapiResult.SurfaceLost: return true;
                    default: throw new GapiException(result);
                }
            }
        }

        /// <summary>
        /// Restores all surfaces stored in video memory, in the order they were created.
        /// </summary>
        public void RestoreAllVideoSurfaces()
        {
            GdApi.CGapiDisplay_RestoreAllVideoSurfaces(Handle);
        }

        /// <summary>
        /// Transfers the contents of the back buffer associated with the display surface
        /// to the display device.
        /// </summary>
        public void Flip()
        {
            CheckResult(GdApi.CGapiDisplay_Flip(Handle));
        }

        // Everything below is from the older Intuitex package, needs to be cleaned up to match above



		//		public static extern UInt32 CGapiDisplay_SuspendDisplay (IntPtr pDisplay);
        public void SuspendDisplay()
        {
            CheckResult(GdApi.CGapiDisplay_SuspendDisplay(Handle));
        }

		//		public static extern UInt32 CGapiDisplay_ResumeDisplay (IntPtr pDisplay);
        public void ResumeDisplay()
        {
            CheckResult(GdApi.CGapiDisplay_ResumeDisplay(Handle));
        }

		//		public static extern UInt32 CGapiDisplay_DeviceToLogicalRect (IntPtr pDisplay, ref GDRect pRect);
		public void DeviceToLogicalRect(ref GDRect pRect)
		{
			CheckResult (GdApi.CGapiDisplay_DeviceToLogicalRect(Handle, ref pRect));
		}

		//		public static extern UInt32 CGapiDisplay_DeviceToLogicalPoint (IntPtr pDisplay, ref System.Drawing.Point pPoint);
		public void DeviceToLogicalPoint(ref System.Drawing.Point pPoint)
		{
			CheckResult (GdApi.CGapiDisplay_DeviceToLogicalPoint(Handle, ref pPoint));
		}
	}
}
