using System;
using System.Text;

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
        }

#if LEAKS
        string stackTrace;

        ~GapiObjectBase()
        {
            if (OwnsHandle)
            {
                throw new Exception("Forgot to call dispose. Allocation stack trace was: " + stackTrace);
            }
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

        public virtual void Dispose()
        {
            if (OwnsHandle)
            {
                CheckResult(DestroyHandle());
                OwnsHandle = false;
                Handle = IntPtr.Zero;
            }
        }

        protected GapiResult CheckResult(GapiResult result)
        {
            return CheckResult(result, null);
        }

        protected GapiResult CheckResult(GapiResult result, string relatedFile)
        {
            if (result != GapiResult.Ok)
                throw new GapiException(result, relatedFile);

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
