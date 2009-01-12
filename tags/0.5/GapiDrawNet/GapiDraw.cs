using System;

namespace GapiDrawNet
{
    /// <summary>
    /// Manages the GapiDraw Global Handle, which ties all GapiDraw objects together in an application.
    /// </summary>
    public static class GapiDraw
    {
        public static readonly IntPtr GlobalHandle;

        static GapiDraw()
        {
            GlobalHandle = GdApi.CGapiDraw_Create();
        }

        public static void Dispose()
        {
            GdApi.CGapiDraw_Destroy(GlobalHandle);
        }
    }
}
