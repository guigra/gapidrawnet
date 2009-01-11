using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace GapiDrawNet
{
	/// <summary>
	/// Provides checking of GapiDraw return values.
	/// </summary>
	class GapiErrorHelper
	{
		public static void RaiseExceptionOnError(UInt32 hResult)
		{
			if(hResult ==  (UInt32)GapiResult.Ok){ return;}

			string ErrorMessage = GetErrorMessage(hResult);
			
			throw new Exception("Error " + hResult.ToString("X") + ": " + ErrorMessage);
		}

		public static void RaiseExceptionOnError(UInt32 hResult, string additionalInfo)
		{
			if(hResult ==  (UInt32)GapiResult.Ok){ return;}

			string ErrorMessage = GetErrorMessage(hResult);
			
			throw new Exception("Error " + hResult.ToString("X") + ": " + ErrorMessage + " (" + additionalInfo + ")");
		}
		
		public static string GetErrorMessage(UInt32 hResult)
		{
			switch(hResult)
			{
				case (UInt32)GapiResult.Ok  : return "OK";
				case (UInt32)GapiResult.OutOfMemory                 : return "OUTOFMEMORY: Memory allocation failed.";
				case (UInt32)GapiResult.InvalidParams               : return "INVALIDPARAMS: One or more arguments are either NULL or contain invalid values.";
				case (UInt32)GapiResult.NotLocked                   : return "NOTLOCKED: The surface is not locked, and cannot be unlocked.";
				case (UInt32)GapiResult.NoDC                        : return "NODC: Cannot unlock surface using DC since it was not locked using GetDC.";
				case (UInt32)GapiResult.LockedDC                    : return "DCLOCKED: The surface is locked using a DC, and cannot be unlocked using Unlock().";
				case (UInt32)GapiResult.CantCreateDC                : return "CANTCREATEDC: Attempt to create DC failed.";
				case (UInt32)GapiResult.BitmapWriteError            : return "BITMAPWRITEERROR: The bitmap file could not be written.";
				case (UInt32)GapiResult.BitmapNotFound              : return "BITMAPNOTFOUND: The specified bitmap could not be found.";
				case (UInt32)GapiResult.InvalidBitmap               : return "INVALIDBITMAP: The bitmap file could not be parsed.";
				case (UInt32)GapiResult.NotInitialized              : return "NOTINITIALIZED: All surface objects must be created before use (OpenDisplay/CreateSurface).";
				case (UInt32)GapiResult.InvalidSurfaceType          : return "INVALIDSURFACETYPE: Cannot call CreateSurface on objects of type CGapiDisplay.";
				case (UInt32)GapiResult.IncompatiblePrimary         : return "INCOMPATIBLEPRIMARY: Primary surface does not exist or is aligned different to the current surface.";
				case (UInt32)GapiResult.PrimarySurfaceAlreadyExists : return "PRIMARYSURFACEALREADYEXISTS: Cannot call OpenDisplay if a primary surface already has been assigned.";
				case (UInt32)GapiResult.LockedSurfaces              : return "LOCKEDSURFACES: One or more surfaces are locked, preventing operation.";
				case (UInt32)GapiResult.LockedKeys                  : return "LOCKEDKEYS: Keys have already been locked in a previous operation.";
				case (UInt32)GapiResult.InvalidRect                 : return "INVALIDRECT: One or more GDRects are invalid.";
				//case (UInt32)GapiResults.GDERR_BACKBUFFERLOST              : return "BACKBUFFERLOST: The back surface of the display was lost during a flip, please request a new using GetBackBuffer";
				case (UInt32)GapiResult.FrameTimeOverflow           : return "FRAMETIMEOVERFLOW: Unable to maintain target frame rate";
				case (UInt32)GapiResult.InvalidMode                 : return "case (UInt32)GapiResults.GDERR_INVALIDMODE: Invalid display mode";
				case (UInt32)GapiResult.UnsupportedMode             : return "case (UInt32)GapiResults.GDERR_UNSUPPORTEDMODE: Unsupported display resolution or incompatible (8-bit) device";
				case (UInt32)GapiResult.NoGapi                      : return "case (UInt32)GapiResults.GDERR_NOGAPI: The file 'gx.dll' is not available on the target device.";
				default : return "Unknown";
			}
		}
	}
}
