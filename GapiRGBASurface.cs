using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	public class GapiRGBASurface : IDisposable
	{
        Size size;
		protected IntPtr unmanagedGapiObject;
		protected bool OwnsGapiObject = true;
		public IntPtr GapiObject
		{
			get { return unmanagedGapiObject; }
		}

		public GapiRGBASurface()
		{			
			unmanagedGapiObject = GdApi.CGapiRGBASurface_Create(GapiDraw.GlobalHandle);
		}

		public GapiRGBASurface(IntPtr gapiObject)
		{	
			unmanagedGapiObject = gapiObject;
			
		}

        /// <summary>
        /// Gets the size of this surface.
        /// </summary>
        public Size Size
        {
            // Create on-demand and cache forever when it's nonzero, since surface sizes can't change
            get { return size != Size.Empty ? size : (size = new Size(GetWidth(), GetHeight())); }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, Width, Height); }
        }

		virtual public void Dispose()
		{
			OwnsGapiObject = false;
			GdApi.CGapiRGBASurface_Destroy(unmanagedGapiObject);
		}

		public UInt32 CreateSurface(string fileName)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdApi.CGapiRGBASurface_CreateSurfaceFromFile(unmanagedGapiObject, 0, fileName);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

		public UInt32 CreateSurface(GapiRGBASurface srcSurface)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdApi.CGapiRGBASurface_CreateSurfaceFromSurface(unmanagedGapiObject, srcSurface.unmanagedGapiObject);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

		public UInt32 CreateSurface(CreateSurfaceOptions dwFlags, string fileName)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdApi.CGapiRGBASurface_CreateSurfaceFromFile(unmanagedGapiObject, (int)dwFlags, fileName);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}
		
		public UInt32 CreateSurface(byte[] imageBytes, CreateSurfaceOptions dwFlags)
		{
			UInt32 hResult = GdApi.CGapiRGBASurface_CreateSurfaceFromMem (unmanagedGapiObject, (int)dwFlags, imageBytes, imageBytes.Length);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}
		//		public UInt32 CreateSurfaceFromMem (IntPtr pSurface, ref byte pImageFileMem, int dwImageFileSize);
		//		public UInt32 CreateSurfaceFromRes (IntPtr pSurface, IntPtr hInstance, int dwResourceID, string pResourceType);

		public UInt32 CreateSurface(IntPtr hInstance, CreateSurfaceOptions dwFlags, int dwResourceID, string pResourceType)
		{
			UInt32 hResult = GdApi.CGapiRGBASurface_CreateSurfaceFromRes(unmanagedGapiObject, (int)dwFlags, hInstance, dwResourceID, pResourceType);

			GapiErrorHelper.RaiseExceptionOnError(hResult);

			return hResult;
		}

		//		public UInt32 CreateSurfaceOfSize (IntPtr pSurface, int dwFlags, int dwWidth, int dwHeight);
		public void CreateSurface(CreateSurfaceOptions dwFlags, int dwWidth, int dwHeight)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdApi.CGapiRGBASurface_CreateSurface(unmanagedGapiObject, (int)dwFlags, dwWidth, dwHeight);

			GapiErrorHelper.RaiseExceptionOnError(hResult);
		}

		public void CreateSurface(int dwWidth, int dwHeight)
		{
			// public UInt32 CreateSurfaceFromFile (IntPtr pSurface, ref char pImageFile);
			UInt32 hResult = GdApi.CGapiRGBASurface_CreateSurface(unmanagedGapiObject, 0, dwWidth, dwHeight);

			GapiErrorHelper.RaiseExceptionOnError(hResult);
		}
		public int GetWidth()
		{
			return GdApi.CGapiRGBASurface_GetWidth(unmanagedGapiObject);
		}

		public int Width
		{
            get { return Size.Width; }
		}

		public int GetHeight()
		{
			return GdApi.CGapiRGBASurface_GetHeight(unmanagedGapiObject);
		}

		public int Height
		{
            get { return Size.Height; }
		}				
		
		//		public UInt32 CGapiRGBASurface_GetBuffer (IntPtr pSurface, ref GDBUFFERDESC pGDBufferDesc);
		public void GetBuffer(ref GDBUFFERDESC pGDBufferDesc)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiRGBASurface_GetBuffer(unmanagedGapiObject, ref pGDBufferDesc));
		}

		
		//		public UInt32 CGapiRGBASurface_ReleaseBuffer (IntPtr pSurface);
		public void ReleaseBuffer(IntPtr hDC)
		{
			GapiErrorHelper.RaiseExceptionOnError(GdApi.CGapiRGBASurface_ReleaseBuffer(unmanagedGapiObject));
		}
	}
}
