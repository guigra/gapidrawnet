using System;

namespace GapiDrawNet
{
    /// <summary>
    /// Represents and manages the lifecycle of a GapiDraw object.
    /// </summary>
    public abstract class GapiObjectRef : IDisposable
    {
        public IntPtr Handle { get; private set; }
        public bool OwnsHandle { get; private set; }

        protected GapiObjectRef(IntPtr handle)
            : this(handle, true) { }

        protected GapiObjectRef(IntPtr handle, bool ownsHandle)
        {
            if (handle == IntPtr.Zero)
                throw new InvalidOperationException("Cannot create a GapiObjectBase against a NULL handle.");

            this.Handle = handle;
            this.OwnsHandle = ownsHandle;
#if LEAKS
            // This is the only way to get a stack trace in windows mobile
            try { throw new Exception(); }
            catch (Exception e) { stackTrace = e.StackTrace; }
#endif
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

        public virtual void Dispose()
        {
            if (OwnsHandle)
            {
                DestroyGapiObject(Handle);
                OwnsHandle = false;
                Handle = IntPtr.Zero;
            }
        }

        protected uint CheckResult(uint result)
        {
            GapiErrorHelper.RaiseExceptionOnError(result);
            return result;
        }

        protected abstract void DestroyGapiObject(IntPtr handle);
    }
}
