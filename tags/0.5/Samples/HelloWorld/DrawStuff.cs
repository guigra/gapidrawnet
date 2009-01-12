using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using GapiDrawNet;

namespace HelloWorld
{
    class DrawStuff
    {
        public static void Onto(GapiSurface gapiSurface, GapiBitmapFont font)
        {
            // this works on both CE and the desktop
            Uri assemblyPath = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string assemblyDir = Path.GetDirectoryName(assemblyPath.LocalPath);
            var redFade = new GapiRgbaSurface(Path.Combine(assemblyDir, "RedFade.png"));

            gapiSurface.FillRect(Color.Gray);
            gapiSurface.DrawText(10, 200, "Hello World!", font, 0);
            gapiSurface.AlphaBltFast(0, 0, redFade);
        }
    }
}
