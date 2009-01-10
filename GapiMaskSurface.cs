using System;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for CGapiMaskSurface.
	/// </summary>
	public class GapiMaskSurface : GapiSurface
	{
		public GapiMaskSurface()
            : base(GdApi.CGapiMaskSurface_Create(), true)
		{
		}

        protected override void DestroyGapiObject(IntPtr gapiObject)
        {
            CheckResult(GdApi.CGapiMaskSurface_Destroy(Handle));
        }

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiMaskSurface_DrawMaskImage(IntPtr pMaskSurface, int dwX, int dwY, IntPtr pSrcSurface, ref GDRect pSrcRect, int dwMaskID);
		public UInt32 DrawMask(int dwX, int dwY, GapiSurface srcSurface, ref GDRect pSrcRect, int dwMaskID)
		{
			UInt32 hResult = GdApi.CGapiMaskSurface_DrawMask(Handle, dwX, dwY, srcSurface.Handle, ref pSrcRect, dwMaskID);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiMaskSurface_DrawMaskRect(IntPtr pMaskSurface, ref GDRect pRect, int dwMaskID);
		public UInt32 DrawMask(ref GDRect pRect, int dwMaskID)
		{
			UInt32 hResult = GdApi.CGapiMaskSurface_DrawMaskRect(Handle, ref pRect, dwMaskID);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiMaskSurface_DrawMaskPixel(IntPtr pMaskSurface, int dwX, int dwY, int dwMaskID);
		public UInt32 DrawMask(int dwX, int dwY, int dwMaskID)
		{
			UInt32 hResult = GdApi.CGapiMaskSurface_DrawMaskPixel(Handle, dwX, dwY, dwMaskID);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiMaskSurface_GetMaskID(IntPtr pMaskSurface, int dwX, int dwY, IntPtr pSrcSurface, ref GDRect pSrcRect, ref int pMaskID, ref System.Drawing.Point pIntersection);
		public int GetMaskID(int dwX, int dwY, GapiSurface srcSurface, ref GDRect pSrcRect, ref System.Drawing.Point pIntersection)
		{
			int MaskID;
			UInt32 hResult = GdApi.CGapiMaskSurface_GetMaskID(Handle, dwX, dwY, srcSurface.Handle, ref pSrcRect, out MaskID, ref  pIntersection);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return MaskID;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiMaskSurface_GetMaskIDRect(IntPtr pMaskSurface, ref GDRect pRect, ref int pMaskID, ref System.Drawing.Point pIntersection);
		public int GetMaskID(ref GDRect pSrcRect, ref System.Drawing.Point pIntersection)
		{
			int MaskID;
			UInt32 hResult = GdApi.CGapiMaskSurface_GetMaskIDRect(Handle, ref pSrcRect, out MaskID, ref  pIntersection);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return MaskID;
		}
//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiMaskSurface_GetMaskIDPixel(IntPtr pMaskSurface, int dwX, int dwY, ref int pMaskID);
		public int GetMaskID(int dwX, int dwY, ref System.Drawing.Point pIntersection)
		{
			int MaskID;
			UInt32 hResult = GdApi.CGapiMaskSurface_GetMaskIDPixel(Handle, dwX, dwY, out MaskID);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return MaskID;
		}
	}
}
