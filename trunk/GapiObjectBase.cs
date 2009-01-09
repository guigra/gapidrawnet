using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace GapiDrawNet
{
    public abstract class GapiObjectBase : IDisposable
    {
        protected IntPtr unmanagedGapiObject;
        protected bool OwnsGapiObject = true;

        public GapiObjectBase(IntPtr unmanagedGapiObject)
        {
            this.unmanagedGapiObject = unmanagedGapiObject;
#if LEAKS
            try { throw new Exception(); }
            catch (Exception e) { stackTrace = e.StackTrace; }
#endif
        }

#if LEAKS
        string stackTrace;

        ~GapiObjectBase()
        {
            if (OwnsGapiObject)
            {
                throw new Exception("Forgot to call dispose. Allocation stack trace was: " + stackTrace);
            }
        }
#endif

        public virtual void Dispose()
        {
            OwnsGapiObject = false;
            DestroyGapiObject(unmanagedGapiObject);
            unmanagedGapiObject = IntPtr.Zero;
        }

        public IntPtr GapiObject
        {
            get { return unmanagedGapiObject; }
            set { unmanagedGapiObject = value; }
        }

        protected void CheckResult(uint result)
        {
            GapiUtility.RaiseExceptionOnError(result);
        }

        protected abstract void DestroyGapiObject(IntPtr gapiObject);
    }
}
