using System;

namespace GapiDrawNet
{
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
