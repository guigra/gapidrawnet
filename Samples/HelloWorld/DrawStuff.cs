using System.Drawing;
using GapiDrawNet;

namespace HelloWorld
{
    class DrawStuff
    {
        public static void Onto(GapiSurface gapiSurface, GapiBitmapFont font)
        {
            gapiSurface.FillRect(Color.Red);
            
            gapiSurface.DrawText(10, 10, "Hello World!", font, 0);
        }
    }
}
