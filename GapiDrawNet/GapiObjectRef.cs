using System;
using System.Text;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
    /// <summary>
    /// Represents and manages the lifecycle of a GapiDraw object.
    /// </summary>
    public abstract class GapiObjectRef : IDisposable
    {
        public IntPtr Handle { get; private set; }
        public bool OwnsHandle { get; private set; }

        protected GapiObjectRef()
        {
            this.Handle = CreateHandle();
            this.OwnsHandle = true;

#if LEAKS
            // This is the only way to get a stack trace in windows mobile
            try { throw new Exception(); }
            catch (Exception e) { stackTrace = e.StackTrace; }
#endif
        }

        protected GapiObjectRef(IntPtr existingHandle)
        {
            if (existingHandle == IntPtr.Zero)
                throw new InvalidOperationException("Cannot create a GapiObjectRef against a NULL handle.");

            this.Handle = existingHandle;
            GC.SuppressFinalize(this); // we don't need any cleanup
        }

#if LEAKS
        string stackTrace;

        ~GapiObjectRef()
        {
            if (OwnsHandle)
            {
                throw new Exception("Forgot to call dispose. Allocation stack trace was: " + stackTrace);
            }
        }
#else
        // Only called if OwnsHandle=true and Dispose() hasn't been called
        ~GapiObjectRef()
        {
            CheckResult(DestroyHandle());
        }
#endif

        public static void CheckForLeaks()
        {
#if LEAKS
            GC.Collect();
#endif
        }

        protected abstract IntPtr CreateHandle();
        protected abstract GapiResult DestroyHandle();

        public void Dispose()
        {
            if (OwnsHandle)
            {
                CheckResult(DestroyHandle());
                OwnsHandle = false;
                Handle = IntPtr.Zero;
                GC.SuppressFinalize(this);
            }
            else throw new InvalidOperationException("Cannot dispose this GapiDraw object because it does not own the underlying unmanaged GapiDraw object!");
        }

        protected GapiResult CheckResult(GapiResult result)
        {
            return CheckResult(result, null);
        }

        protected GapiResult CheckResult(GapiResult result, string relatedFile)
        {
            if (result != GapiResult.Ok)
            {
                if (Enum.IsDefined(typeof(GapiResult), result))
                    throw new GapiException(result, relatedFile);
                else
                    throw Marshal.GetExceptionForHR((int)result);
            }

            return result; // for chaining
        }

        /// <summary>
        /// Encodes a string for passing to the GapiDraw.dll library via DllImport. Normally
        /// DllImport would handle this automatically, but it breaks in the case of when a program
        /// running on the desktop uses this GapiDrawNet library which was compiled for .NET CF.
        /// </summary>
        protected byte[] Str(string s)
        {
            return s != null ? Encoding.Unicode.GetBytes(s + '\0') : null;
        }
    }
}
