using System.Drawing;
using GapiDrawNet;

namespace HelloWorld
{
    class DrawStuff
    {
        public static void Onto(GapiSurface gapiSurface)
        {
            gapiSurface.FillRect(Color.Red);
        }
    }
}
