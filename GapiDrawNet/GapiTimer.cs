using System;

namespace GapiDrawNet
{
	/// <summary>
    /// GapiTimer contains functions to limit the maximum number of frame updates each second 
    /// for better battery usage. You can also use GapiTimer to retrieve average frame render 
    /// time and frame rate.
	/// </summary>
	public class GapiTimer : GapiObjectRef
	{
        protected override IntPtr CreateHandle()
        {
            return GdApi.CGapiTimer_Create();
        }

        protected override GapiResult DestroyHandle()
        {
            return GdApi.CGapiTimer_Destroy(Handle);
        }

        // Everything below is from the older Intuitex package, needs to be cleaned up to match above


		public void StartTimer(int TargetFrameRate)
		{
			GapiResult hResult = GdApi.CGapiTimer_StartTimer(Handle, TargetFrameRate);
			// int hResult = CGapiTimer_StartTimer(Handle, TargetFrameRate);
		}

		public GapiResult WaitForNextFrame()
		{
			GapiResult hResult = (GapiResult)GdApi.CGapiTimer_WaitForNextFrame(Handle);

			if(hResult != 0 && hResult != GapiResult.FrameTimeOverflow)
			{
				CheckResult(hResult);
			}
			
			return (GapiResult)hResult;
		}

		public double GetActualFrameRate()
		{
			float pActualFrameRate = 0;
			GapiResult hResult = GdApi.CGapiTimer_GetActualFrameRate(Handle, ref pActualFrameRate);

			return pActualFrameRate;
		}

		public double GetActualFrameTimeMS()
		{
			float pActualFrameTime = 0;

			GdApi.CGapiTimer_GetActualFrameTime(Handle, ref pActualFrameTime);
			return pActualFrameTime;
		}
	}
}