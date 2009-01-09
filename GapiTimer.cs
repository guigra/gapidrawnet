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
			unmanagedGapiObject = GdNet.CGapiTimer_Create();
		}

		virtual public void Dispose()
		{
			GdNet.CGapiTimer_Destroy(unmanagedGapiObject);
		}

		public void StartTimer(int TargetFrameRate)
		{
			UInt32 hResult = GdNet.CGapiTimer_StartTimer(unmanagedGapiObject, TargetFrameRate);
			// int hResult = CGapiTimer_StartTimer(unmanagedGapiObject, TargetFrameRate);
		}

		public GapiResults WaitForNextFrame()
		{
			GapiResults hResult = (GapiResults)GdNet.CGapiTimer_WaitForNextFrame(unmanagedGapiObject);

			if(hResult != 0 && hResult != GapiResults.GDERR_FRAMETIMEOVERFLOW)
			{
				GapiUtility.RaiseExceptionOnError((uint)hResult);
			}
			
			return (GapiResults)hResult;
		}

		public double GetActualFrameRate()
		{
			float pActualFrameRate = 0;
			UInt32 hResult = GdNet.CGapiTimer_GetActualFrameRate(unmanagedGapiObject, ref pActualFrameRate);

			return pActualFrameRate;
		}

		public double GetActualFrameTimeMS()
		{
			float pActualFrameTime = 0;

			GdNet.CGapiTimer_GetActualFrameTime(unmanagedGapiObject, ref pActualFrameTime);
			return pActualFrameTime;
		}
	}
}