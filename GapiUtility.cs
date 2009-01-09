using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for GapiUtility.
	/// </summary>
	public class GapiUtility
	{
		public GapiUtility()
		{
			//
			// TODO: Add constructor logic here
			//
		}

#if (PocketPC)
		[DllImport("coredll.dll")] 
		internal static extern IntPtr GetModuleHandle(String lpModuleName); 

		public static IntPtr GetHInstance()
		{
			return GetModuleHandle(null); ;
		}
#else
      public static IntPtr GetHInstance()
		{
			return System.Runtime.InteropServices.Marshal.GetHINSTANCE(typeof(GapiApplication).Module);
		}
#endif

		public static int RGB(int red, int green, int blue)
		{
			return red + (green << 8) + (blue << 16);
		}

        public static int RGB(Color color)
        {
            return Color.FromArgb(color.B, color.G, color.R).ToArgb();
        }

		public static void ReduceBound(bool shouldReduce, ref int sourceBound, ref int sourceBoundMirror, bool useMirror, ref int destBound2, int reduction)
		{
			if(shouldReduce == false){ return; }
			
			if(useMirror)
			{
				sourceBoundMirror +=reduction;
			}
			else
			{
				sourceBound -= reduction;
			}
			destBound2 -= reduction;
		}

		public static bool IsLeftButton(System.Windows.Forms.MouseButtons button)
		{
			// on my pocket pc, left button == 1, not MouseButtons.Left
			return ((int)button == 1) || (button == System.Windows.Forms.MouseButtons.Left);
		}

		public static void RaiseExceptionOnError(UInt32 hResult)
		{
			if(hResult ==  (UInt32)GapiResults.GD_OK){ return;}

			string ErrorMessage = GetErrorMessage(hResult);
			
			throw new Exception("Error " + hResult.ToString("X") + ": " + ErrorMessage);
		}
		public static void RaiseExceptionOnError(UInt32 hResult, string additionalInfo)
		{
			if(hResult ==  (UInt32)GapiResults.GD_OK){ return;}

			string ErrorMessage = GetErrorMessage(hResult);
			
			throw new Exception("Error " + hResult.ToString("X") + ": " + ErrorMessage + " (" + additionalInfo + ")");
		}

		static uint SEED = 93186752;
		public static int Random(int uBound)
		{			
			const uint a = 1588635695;
			const uint q = 2, r = 1117695901;

			SEED = a*(SEED % q) - r*(SEED / q);
			return (int)(SEED % uBound);
		}

		public static int[] _CuttoffPoints = null;
		public static void InitialiseBiasCutoffPoints(int uBound)
		{
			int max = (int)(uBound * 1.5)+1;

			_CuttoffPoints = new int[max];

			int cuttoff = 0;
			for(int f = 0; f< max; f++)
			{
				
				_CuttoffPoints[f] = cuttoff;	
				cuttoff += f + 1;
			}
		}

		public static int _HighBiasedValue(int uBound, int value)
		{
			for(int f = uBound-1; f >= 0; f--)
			{
				if(value >= _CuttoffPoints[f])
				{
					return f;
				}				
			}
			return 0;
		}

		public static int _LowBiasedValue(int uBound, int value)
		{
			return uBound - 1 - _HighBiasedValue(uBound, value);
		}

		public static int LowBiasedRandom(int uBound)
		{	
			if(_CuttoffPoints == null || _CuttoffPoints.Length < uBound)
			{
				InitialiseBiasCutoffPoints(uBound);
			}

			int max = _CuttoffPoints[uBound];
			int point = Random(max);
			return _LowBiasedValue(uBound, point);
		}

		public static bool OnceInAWhile(int odds)
		{
			return Random(odds) == 0;
		}

		public static bool FlipACoin()
		{
			return Random(2) == 0;
		}

		public static string GetErrorMessage(UInt32 hResult)
		{
			switch(hResult)
			{
				case (UInt32)GapiResults.GD_OK  : return "OK";
				case (UInt32)GapiResults.GDERR_OUTOFMEMORY                 : return "OUTOFMEMORY: Memory allocation failed.";
				case (UInt32)GapiResults.GDERR_INVALIDPARAMS               : return "INVALIDPARAMS: One or more arguments are either NULL or contain invalid values.";
				case (UInt32)GapiResults.GDERR_NOTLOCKED                   : return "NOTLOCKED: The surface is not locked, and cannot be unlocked.";
				case (UInt32)GapiResults.GDERR_NODC                        : return "NODC: Cannot unlock surface using DC since it was not locked using GetDC.";
				case (UInt32)GapiResults.GDERR_DCLOCKED                    : return "DCLOCKED: The surface is locked using a DC, and cannot be unlocked using Unlock().";
				case (UInt32)GapiResults.GDERR_CANTCREATEDC                : return "CANTCREATEDC: Attempt to create DC failed.";
				case (UInt32)GapiResults.GDERR_BITMAPWRITEERROR            : return "BITMAPWRITEERROR: The bitmap file could not be written.";
				case (UInt32)GapiResults.GDERR_BITMAPNOTFOUND              : return "BITMAPNOTFOUND: The specified bitmap could not be found.";
				case (UInt32)GapiResults.GDERR_INVALIDBITMAP               : return "INVALIDBITMAP: The bitmap file could not be parsed.";
				case (UInt32)GapiResults.GDERR_NOTINITIALIZED              : return "NOTINITIALIZED: All surface objects must be created before use (OpenDisplay/CreateSurface).";
				case (UInt32)GapiResults.GDERR_INVALIDSURFACETYPE          : return "INVALIDSURFACETYPE: Cannot call CreateSurface on objects of type CGapiDisplay.";
				case (UInt32)GapiResults.GDERR_INCOMPATIBLEPRIMARY         : return "INCOMPATIBLEPRIMARY: Primary surface does not exist or is aligned different to the current surface.";
				case (UInt32)GapiResults.GDERR_PRIMARYSURFACEALREADYEXISTS : return "PRIMARYSURFACEALREADYEXISTS: Cannot call OpenDisplay if a primary surface already has been assigned.";
				case (UInt32)GapiResults.GDERR_LOCKEDSURFACES              : return "LOCKEDSURFACES: One or more surfaces are locked, preventing operation.";
				case (UInt32)GapiResults.GDERR_LOCKEDKEYS                  : return "LOCKEDKEYS: Keys have already been locked in a previous operation.";
				case (UInt32)GapiResults.GDERR_INVALIDRECT                 : return "INVALIDRECT: One or more GDRects are invalid.";
				//case (UInt32)GapiResults.GDERR_BACKBUFFERLOST              : return "BACKBUFFERLOST: The back surface of the display was lost during a flip, please request a new using GetBackBuffer";
				case (UInt32)GapiResults.GDERR_FRAMETIMEOVERFLOW           : return "FRAMETIMEOVERFLOW: Unable to maintain target frame rate";
				case (UInt32)GapiResults.GDERR_INVALIDMODE                 : return "case (UInt32)GapiResults.GDERR_INVALIDMODE: Invalid display mode";
				case (UInt32)GapiResults.GDERR_UNSUPPORTEDMODE             : return "case (UInt32)GapiResults.GDERR_UNSUPPORTEDMODE: Unsupported display resolution or incompatible (8-bit) device";
				case (UInt32)GapiResults.GDERR_NOGAPI                      : return "case (UInt32)GapiResults.GDERR_NOGAPI: The file 'gx.dll' is not available on the target device.";
				default : return "Unknown";
			}
		}
	}
}
