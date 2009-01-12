using System;

namespace GapiDrawNet
{
	/// <summary>
    /// GapiInput locks all hardware keys on Windows CE and maps up/down/left/right to the
    /// correct display orientation.
	/// </summary>
	public class GapiInput : GapiObjectRef
	{
        protected override IntPtr CreateHandle()
        {
            return GdApi.CGapiInput_Create();
        }

        protected override GapiResult DestroyHandle()
        {
            return GdApi.CGapiInput_Destroy(Handle);
        }

        /// <summary>
        /// Enables a full, exclusive lock of all hardware keys on Windows CE.
        /// This is automatically done in the constructor.
        /// </summary>
        public void OpenInput()
        {
            CheckResult(GdApi.CGapiInput_OpenInput(Handle));
        }

        /// <summary>
        /// Returns a list of virtual key codes matching the current display orientation.
        /// </summary>
		public KeyList KeyList
		{
            get
            {
                KeyList keyList;
                CheckResult(GdApi.CGapiInput_GetKeyList(Handle, out keyList));
                return keyList;
            }
		}

        /// <summary>
        /// Releases the exclusive lock on all hardware keys. This is automatically done in Dispose.
        /// </summary>
		public void CloseInput()
		{
            CheckResult(GdApi.CGapiInput_CloseInput(Handle));
		}
	}
}