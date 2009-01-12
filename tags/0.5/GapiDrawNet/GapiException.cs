using System;

namespace GapiDrawNet
{
    /// <summary>
    /// Thrown when a GapiDraw function returns an error code.
    /// </summary>
    public class GapiException : Exception
    {
        public GapiResult GapiResult { get; private set; }
        public string RelatedFile { get; private set; }

        internal GapiException(GapiResult result)
            : this(result, null) { }

        internal GapiException(GapiResult result, string relatedFile)
            : base(BuildMessage(result, relatedFile))
        {
            this.GapiResult = result;
            this.RelatedFile = relatedFile;
        }

        static string BuildMessage(GapiResult result, string relatedFile)
        {
            string message = string.Format("Error {0:X}: {1}", result, GetDescription(result));

            if (relatedFile != null)
                message += string.Format(" ({0})", relatedFile);

            return message;
        }

        static string GetDescription(GapiResult result)
        {
            switch (result)
            {
                case GapiResult.Ok: return "OK";
                case GapiResult.OutOfMemory: return "Memory allocation failed.";
                case GapiResult.InvalidParams: return "One or more arguments are either NULL or contain invalid values.";
                case GapiResult.NotLocked: return "The surface is not locked, and cannot be unlocked.";
                case GapiResult.NoDC: return "Cannot unlock surface using DC since it was not locked using GetDC.";
                case GapiResult.LockedDC: return "The surface is locked using a DC, and cannot be unlocked using Unlock().";
                case GapiResult.CantCreateDC: return "Attempt to create DC failed.";
                case GapiResult.BitmapWriteError: return "The bitmap file could not be written.";
                case GapiResult.BitmapNotFound: return "The specified bitmap could not be found.";
                case GapiResult.InvalidBitmap: return "The bitmap file could not be parsed.";
                case GapiResult.NotInitialized: return "All surface objects must be created before use (OpenDisplay/CreateSurface).";
                case GapiResult.InvalidSurfaceType: return "Cannot call CreateSurface on objects of type CGapiDisplay.";
                case GapiResult.IncompatiblePrimary: return "Primary surface does not exist or is aligned different to the current surface.";
                case GapiResult.PrimarySurfaceAlreadyExists: return "Cannot call OpenDisplay if a primary surface already has been assigned.";
                case GapiResult.LockedSurfaces: return "One or more surfaces are locked, preventing operation.";
                case GapiResult.LockedKeys: return "Keys have already been locked in a previous operation.";
                case GapiResult.InvalidRect: return "One or more rectangles are invalid.";
                case GapiResult.FrameTimeOverflow: return "Unable to maintain target frame rate";
                case GapiResult.InvalidMode: return "Invalid display mode";
                case GapiResult.UnsupportedMode: return "Unsupported display resolution or incompatible device";
                case GapiResult.NoGapi: return "The GAPI file \"gx.dll\" is not available on the target device.";
                case GapiResult.NoVideoHardware: return "The device does not have any video acceleration";
                case GapiResult.NoVideoSurface: return "The surface is not stored in video memory";
                case GapiResult.SurfaceLost: return "The contents of a video surface was destroyed";
                case GapiResult.SurfaceBusy: return "A surface is already busy with a hardware blit operation";
                case GapiResult.ReadonlySurface: return "This is a read-only surface";
                default: return "Unknown GapiResult.";
            }
        }
    }
}
