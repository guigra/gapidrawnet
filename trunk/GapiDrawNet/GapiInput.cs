using System;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for GapiInput.
	/// </summary>
	public class GapiInput : IDisposable
	{
//		[DllImport("GdNet.dll")]
//		private static extern IntPtr CGapiInput_Create();
//		// private static extern int CGdApplication_Create(ref IntPtr pApp, IntPtr hInst);
//
//		[DllImport("GdNet.dll")]
//		private static extern int  CGapiInput_Destroy(IntPtr pApp);

		virtual public void Dispose()
		{
			GdApi.CGapiInput_Destroy(unmanagedGapiObject);
		}

		public GapiInput()
		{
			unmanagedGapiObject = GdApi.CGapiInput_Create();
		}

		protected IntPtr unmanagedGapiObject;
		public IntPtr GapiObject
		{
			get { return unmanagedGapiObject; }
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiInput_GetKeyList(IntPtr pInput, ref GDKEYLIST pKeyList);
		public UInt32 GetKeyList(ref KeyList pKeyList)
		{
			UInt32 hResult = GdApi.CGapiInput_GetKeyList(unmanagedGapiObject, ref pKeyList);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiInput_OpenInput(IntPtr pInput);
		public UInt32 OpenInput()
		{
			UInt32 hResult = GdApi.CGapiInput_OpenInput(unmanagedGapiObject);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

//		[DllImport("GdNet.DLL")]
//		public static extern UInt32 CGapiInput_CloseInput(IntPtr pInput);
		public UInt32 CloseInput()
		{
			UInt32 hResult = GdApi.CGapiInput_CloseInput(unmanagedGapiObject);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

	}
}