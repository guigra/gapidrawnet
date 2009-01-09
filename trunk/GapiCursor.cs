using System;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for GapiCursor.
	/// </summary>
	public class GapiCursor : GapiSurface
	{
//		[DllImport("GdNet.dll")]
//		private static extern IntPtr CGapiCursor_Create();
//		// private static extern int CGdApplication_Create(ref IntPtr pApp, IntPtr hInst);
//
//		[DllImport("GdNet.dll")]
//		private static extern int  CGapiCursor_Destroy(IntPtr pApp);

//		protected IntPtr unmanagedGapiObject;
//		public IntPtr GapiObject
//		{
//			get { return unmanagedGapiObject; }
//		}

		public GapiCursor()
		{
			unmanagedGapiObject = GdNet.CGapiCursor_Create();
		}

		override public void Dispose()
		{
			GdNet.CGapiCursor_Destroy(unmanagedGapiObject);
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
			UInt32 hResult = GdNet.CGapiCursor_CreateCursor(unmanagedGapiObject, 0, FrameCount, dwFrameStep);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_SetHotSpot(IntPtr pCursor, int dwX, int dwY);
		public UInt32 SetHotSpot(int dwX, int dwY)
		{
			UInt32 hResult = GdNet.CGapiCursor_SetHotSpot(unmanagedGapiObject, dwX, dwY);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_SetFrameIndex(IntPtr pCursor, int dwFrameIndex);
		public UInt32 SetFrameIndex(int dwFrameIndex)
		{
			UInt32 hResult = GdNet.CGapiCursor_SetFrameIndex(unmanagedGapiObject, dwFrameIndex);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_SetPosition(IntPtr pCursor, int dwX, int dwY);
		public UInt32 SetPosition(int dwX, int dwY)
		{
			UInt32 hResult = GdNet.CGapiCursor_SetPosition(unmanagedGapiObject, dwX, dwY);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiCursor_DrawCursor(IntPtr pCursor, IntPtr pDestSurface);
		public UInt32 DrawCursor(GapiSurface destSurface)
		{
			UInt32 hResult = GdNet.CGapiCursor_DrawCursor(unmanagedGapiObject, destSurface.GapiObject);

			GapiUtility.RaiseExceptionOnError(hResult);

			return hResult;
		}

	}
}
