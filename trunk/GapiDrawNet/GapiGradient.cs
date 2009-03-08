using System;
using System.Drawing;

namespace GapiDrawNet
{
    public class GapiGradient : GapiObjectRef
    {
        /// <summary>
        /// Initializes a new GapiGradient.
        /// </summary>
        /// <param name="size">Number of pixel steps of the gradient. Must match the width 
        /// and height of the clipped rectangle that is later used to draw the gradient.</param>
        public GapiGradient(int size, Color first, Color last)
        {
            CreateGradient(size, first, last);
        }

        protected override IntPtr CreateHandle()
        {
            return GdApi.CGapiGradient_Create(GapiDraw.GlobalHandle);
        }

        protected override GapiResult DestroyHandle()
        {
            return GdApi.CGapiGradient_Destroy(Handle);
        }

        /// <summary>
        /// Recreates this gradient with the given new values.
        /// </summary>
        /// <param name="size">Number of pixel steps of the gradient. Must match the width 
        /// and height of the clipped rectangle that is later used to draw the gradient.</param>
        public void CreateGradient(int size, Color first, Color last)
        {
            CheckResult(GdApi.CGapiGradient_CreateGradient(Handle, size, 
                first.ToColorRef(), last.ToColorRef()));
        }

        /// <summary>
        /// Returns the number of pixels in the gradient.
        /// </summary>
        public int Size
        {
            get { return (int)GdApi.CGapiGradient_GetNumPixels(Handle); }
        }

        // These methods aren't working for me. API change?

        /// <summary>
        /// Gets the first color of the gradient.
        /// </summary>
        /// <returns></returns>
        public Color FirstColor
        {
            get
            {
                uint firstColor;
                CheckResult(GdApi.CGapiGradient_GetFirstColor(Handle, out firstColor));
                return firstColor.ToColor();
            }
        }

        /// <summary>
        /// Gets the last color of the gradient.
        /// </summary>
        public Color LastColor
        {
            get
            {
                uint lastColor;
                CheckResult(GdApi.CGapiGradient_GetLastColor(Handle, out lastColor));
                return lastColor.ToColor();
            }
        }
    }
}
