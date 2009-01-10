using System;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for GapiCursor.
	/// </summary>
	public class GapiCursor : GapiSurface
	{
		public GapiCursor()
            : base(GdNet.CGapiCursor_Create(), true)
		{
		}

        protected override void DestroyGapiObject(IntPtr gapiObject)
        {
            CheckResult(GdNet.CGapiCursor_Destroy(Handle));
        }


//		Create a CGapiCursor object. 
//		Call CGapiCursor::CreateSurface and supply a valid cursor bitmap. 
//		Call CGapiCursor::SetColorKey if the cursor is not alpha blended. 
//		Call CGapiCursor::CreateCursor to set the number of frames and update interval. 
//		On each mouse move, call CGapiCursor::SetPosition. 
//		On each frame, call CGapiCursor::DrawCursor. 

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_CreateCursor(IntPtr pCursor, int dwFlags, int dwFrameCount, int dwFrameStep);
		public UInt32 CreateCursor(int FrameCount, int dwFrameStep)
		{
			UInt32 hResult = GdNet.CGapiCursor_CreateCursor(Handle, 0, FrameCount, dwFrameStep);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_SetHotSpot(IntPtr pCursor, int dwX, int dwY);
		public UInt32 SetHotSpot(int dwX, int dwY)
		{
			UInt32 hResult = GdNet.CGapiCursor_SetHotSpot(Handle, dwX, dwY);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_SetFrameIndex(IntPtr pCursor, int dwFrameIndex);
		public UInt32 SetFrameIndex(int dwFrameIndex)
		{
			UInt32 hResult = GdNet.CGapiCursor_SetFrameIndex(Handle, dwFrameIndex);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_SetPosition(IntPtr pCursor, int dwX, int dwY);
		public UInt32 SetPosition(int dwX, int dwY)
		{
			UInt32 hResult = GdNet.CGapiCursor_SetPosition(Handle, dwX, dwY);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_DrawCursor(IntPtr pCursor, IntPtr pDestSurface);
		public UInt32 DrawCursor(GapiSurface destSurface)
		{
			UInt32 hResult = GdNet.CGapiCursor_DrawCursor(Handle, destSurface.Handle);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

	}
}
