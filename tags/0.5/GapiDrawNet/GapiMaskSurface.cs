using System;

namespace GapiDrawNet
{
	/// <summary>
    /// GapiMaskSurface is a surface specifically suited for collision detection masks.
	/// </summary>
	public class GapiMaskSurface : GapiSurface
	{
        protected override IntPtr CreateHandle()
        {
            return GdApi.CGapiMaskSurface_Create();
        }

        protected override GapiResult DestroyHandle()
        {
            return GdApi.CGapiMaskSurface_Destroy(Handle);
        }

        // Everything below is from the older Intuitex package, needs to be cleaned up to match above


		public void DrawMask(int dwX, int dwY, GapiSurface srcSurface, ref GDRect pSrcRect, int dwMaskID)
		{
			CheckResult(GdApi.CGapiMaskSurface_DrawMask(Handle,
                dwX, dwY, srcSurface.Handle, ref pSrcRect, dwMaskID));
		}

		public void DrawMask(ref GDRect pRect, int dwMaskID)
		{
            CheckResult(GdApi.CGapiMaskSurface_DrawMaskRect(Handle, ref pRect, dwMaskID));
		}

		public void DrawMask(int dwX, int dwY, int dwMaskID)
		{
            CheckResult(GdApi.CGapiMaskSurface_DrawMaskPixel(Handle, dwX, dwY, dwMaskID));
		}

		public int GetMaskID(int dwX, int dwY, GapiSurface srcSurface, ref GDRect pSrcRect, ref System.Drawing.Point pIntersection)
		{
			int MaskID;
            CheckResult(GdApi.CGapiMaskSurface_GetMaskID(Handle,
                dwX, dwY, srcSurface.Handle, ref pSrcRect, out MaskID, ref  pIntersection));
			return MaskID;
		}

		public int GetMaskID(ref GDRect pSrcRect, ref System.Drawing.Point pIntersection)
		{
			int MaskID;
			CheckResult(GdApi.CGapiMaskSurface_GetMaskIDRect(Handle,
                ref pSrcRect, out MaskID, ref  pIntersection));
			return MaskID;
		}

        public int GetMaskID(int dwX, int dwY, ref System.Drawing.Point pIntersection)
		{
			int MaskID;
            CheckResult(GdApi.CGapiMaskSurface_GetMaskIDPixel(Handle, dwX, dwY, out MaskID));
			return MaskID;
		}
	}
}
