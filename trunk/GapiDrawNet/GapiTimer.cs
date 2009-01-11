using System;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for CGapiTimer.
	/// </summary>
	public class GapiTimer : IDisposable
	{
		protected IntPtr unmanagedGapiObject;
		public IntPtr GapiObject
		{
			get { return unmanagedGapiObject; }
		}

		public GapiTimer()
		{
			unmanagedGapiObject = GdApi.CGapiTimer_Create();
		}

		virtual public void Dispose()
		{
			GdApi.CGapiTimer_Destroy(unmanagedGapiObject);
		}

		public void StartTimer(int TargetFrameRate)
		{
			UInt32 hResult = GdApi.CGapiTimer_StartTimer(unmanagedGapiObject, TargetFrameRate);
			// int hResult = CGapiTimer_StartTimer(unmanagedGapiObject, TargetFrameRate);
		}

		public GapiResult WaitForNextFrame()
		{
			GapiResult hResult = (GapiResult)GdApi.CGapiTimer_WaitForNextFrame(unmanagedGapiObject);

			if(hResult != 0 && hResult != GapiResult.FrameTimeOverflow)
			{
				GapiErrorHelper.RaiseExceptionOnError((uint)hResult);
			}
			
			return (GapiResult)hResult;
		}

		public double GetActualFrameRate()
		{
			float pActualFrameRate = 0;
			UInt32 hResult = GdApi.CGapiTimer_GetActualFrameRate(unmanagedGapiObject, ref pActualFrameRate);

			return pActualFrameRate;
		}

		public double GetActualFrameTimeMS()
		{
			float pActualFrameTime = 0;

			GdApi.CGapiTimer_GetActualFrameTime(unmanagedGapiObject, ref pActualFrameTime);
			return pActualFrameTime;
		}
	}
}