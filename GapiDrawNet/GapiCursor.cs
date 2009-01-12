using System;

namespace GapiDrawNet
{
	/// <summary>
    /// GapiCursor is used to draw a cursor when an application is run in 
    /// full screen mode on a Stationary computer.
	/// </summary>
	public class GapiCursor : GapiSurface
	{
        /// <summary>
        /// Creates an empty GapiCursor. You should call CreateSurface followed by CreateCursor
        /// to initialize the cursor.
        /// </summary>
		public GapiCursor() { }

        protected override IntPtr CreateHandle()
        {
            return GdApi.CGapiCursor_Create();
        }

        protected override GapiResult DestroyHandle()
        {
            return GdApi.CGapiCursor_Destroy(Handle);
        }

        /// <summary>
        /// Creates the cursor.
        /// </summary>
		public void CreateCursor(int frameCount, int frameStep)
		{
            CheckResult(GdApi.CGapiCursor_CreateCursor(Handle, 0, (uint)frameCount, (uint)frameStep));
		}

        // Everything below is from the older Intuitex package, needs to be cleaned up to match above



//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_SetHotSpot(IntPtr pCursor, int dwX, int dwY);
		public void SetHotSpot(int dwX, int dwY)
		{
            CheckResult(GdApi.CGapiCursor_SetHotSpot(Handle, dwX, dwY));
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_SetFrameIndex(IntPtr pCursor, int dwFrameIndex);
		public void SetFrameIndex(int dwFrameIndex)
		{
            CheckResult(GdApi.CGapiCursor_SetFrameIndex(Handle, dwFrameIndex));
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_SetPosition(IntPtr pCursor, int dwX, int dwY);
		public void SetPosition(int dwX, int dwY)
		{
            CheckResult(GdApi.CGapiCursor_SetPosition(Handle, dwX, dwY));
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_DrawCursor(IntPtr pCursor, IntPtr pDestSurface);
		public void DrawCursor(GapiSurface destSurface)
		{
            CheckResult(GdApi.CGapiCursor_DrawCursor(Handle, destSurface.Handle));
		}
	}
}
