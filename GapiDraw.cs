using System;
using System.Collections.Generic;
using System.Text;

namespace GapiDrawNet
{
    public static class GapiDraw
    {
        public static readonly IntPtr GlobalHandle;

        static GapiDraw()
        {
            GlobalHandle = GdNet.CGapiDraw_Create();
        }

        public static void Dispose()
        {
            GdNet.CGapiDraw_Destroy(GlobalHandle);
        }
    }
}
