using System;
using System.Drawing;

namespace GapiDrawNet
{
    /// <summary>
    /// GapiRgbaSurface is a memory area with embedded alpha information. You can use it 
    /// in operations such as GapiSurface.AlphaBlt for easy to use alpha blends.
    /// </summary>
    public class GapiRgbaSurface : GapiObjectRef
    {
        public GapiRgbaSurface()
            : base(GdApi.CGapiRGBASurface_Create(GapiDraw.GlobalHandle))
        {
        }

        internal GapiRgbaSurface(IntPtr handle, bool ownsHandle)
            : base(handle, ownsHandle) { }

        #region Convenience Constructors

        /// <summary>
        /// Initializes the surface with the given size and prepares it for graphic operations.
        /// </summary>
        public GapiRgbaSurface(int width, int height)
            : this()
        {
            CreateSurface(width, height);
        }

        /// <summary>
        /// Initializes the surface with the given size and prepares it for graphic operations.
        /// </summary>
        public GapiRgbaSurface(int width, int height, RgbaSurfaceOptions options)
            : this()
        {
            CreateSurface(width, height, options);
        }

        /// <summary>
        /// Initializes the surface from the contents of the given file.
        /// </summary>
        public GapiRgbaSurface(string fileName)
            : this()
        {
            CreateSurface(fileName);
        }

        /// <summary>
        /// Initializes the surface from the contents of the given file.
        /// </summary>
        public GapiRgbaSurface(string fileName, RgbaSurfaceOptions options)
            : this()
        {
            CreateSurface(fileName, options);
        }

        /// <summary>
        /// Initializes the surface from the contents of the given byte array.
        /// </summary>
        public GapiRgbaSurface(byte[] imageBytes)
            : this()
        {
            CreateSurface(imageBytes);
        }

        /// <summary>
        /// Initialized the surface from the contents of the given byte array.
        /// </summary>
        public GapiRgbaSurface(byte[] imageBytes, RgbaSurfaceOptions options)
            : this()
        {
            CreateSurface(imageBytes, imageBytes.Length, options);
        }

        /// <summary>
        /// Initializes the surface from the contents of the given byte array.
        /// </summary>
        public GapiRgbaSurface(byte[] imageBytes, int length, RgbaSurfaceOptions options)
            : this()
        {
            CreateSurface(imageBytes, length, options);
        }

        /// <summary>
        /// Initializes the surface from the given ID of a Win32 bitmap resource.
        /// </summary>
        public GapiRgbaSurface(IntPtr hInstance, RgbaSurfaceOptions options,
            int resourceID, string resourceType)
            : this()
        {
            CreateSurface(hInstance, options, resourceID, resourceType);
        }

        /// <summary>
        /// Initializes the surface by copying an existing surface.
        /// </summary>
        public GapiRgbaSurface(GapiRgbaSurface sourceSurface)
            : this()
        {
            CreateSurface(sourceSurface);
        }

        #endregion

        protected override void DestroyGapiObject(IntPtr gapiObject)
        {
            CheckResult(GdApi.CGapiRGBASurface_Destroy(Handle));
        }

        #region CreateSurface

        /// <summary>
        /// Allocates memory for a surface of the given size and prepares it for graphic operations.
        /// </summary>
        public void CreateSurface(int width, int height)
        {
            CreateSurface(width, height, 0);
        }

        /// <summary>
        /// Allocates memory for a surface of the given size and prepares it for graphic operations.
        /// </summary>
        public void CreateSurface(int width, int height, RgbaSurfaceOptions options)
        {
            CheckResult(GdApi.CGapiRGBASurface_CreateSurface(Handle, options, (uint)width, (uint)height));
        }

        /// <summary>
        /// Creates the surface from the contents of the given file.
        /// </summary>
        public void CreateSurface(string fileName)
        {
            CreateSurface(fileName, 0);
        }

        /// <summary>
        /// Creates the surface from the contents of the given file.
        /// </summary>
        public void CreateSurface(string fileName, RgbaSurfaceOptions options)
        {
            CheckResult(GdApi.CGapiRGBASurface_CreateSurfaceFromFile(Handle, options, Str(fileName)));
        }

        /// <summary>
        /// Creates the surface from the contents of the given byte array.
        /// </summary>
        public void CreateSurface(byte[] imageBytes)
        {
            CreateSurface(imageBytes, imageBytes.Length, 0);
        }

        /// <summary>
        /// Creates the surface from the contents of the given byte array.
        /// </summary>
        public void CreateSurface(byte[] imageBytes, RgbaSurfaceOptions options)
        {
            CreateSurface(imageBytes, imageBytes.Length, options);
        }

        /// <summary>
        /// Creates the surface from the contents of the given byte array.
        /// </summary>
        public void CreateSurface(byte[] imageBytes, int length, RgbaSurfaceOptions options)
        {
            CheckResult(GdApi.CGapiRGBASurface_CreateSurfaceFromMem(Handle,
                options, imageBytes, (uint)length));
        }

        /// <summary>
        /// Creates the surface from the given ID of a Win32 bitmap resource.
        /// </summary>
        public void CreateSurface(IntPtr hInstance, RgbaSurfaceOptions options,
            int resourceID, string resourceType)
        {
            CheckResult(GdApi.CGapiRGBASurface_CreateSurfaceFromRes(Handle,
                options, hInstance, (uint)resourceID, Str(resourceType)));
        }

        /// <summary>
        /// Creates the surface by copying an existing surface.
        /// </summary>
        public void CreateSurface(GapiRgbaSurface sourceSurface)
        {
            CheckResult(GdApi.CGapiRGBASurface_CreateSurfaceFromSurface(Handle, sourceSurface.Handle));
        }

        #endregion

        #region Size Metrics

        public int Width
        {
            get { return (int)GdApi.CGapiRGBASurface_GetWidth(Handle); }
        }

        public int Height
        {
            get { return (int)GdApi.CGapiRGBASurface_GetHeight(Handle); }
        }

        /// <summary>
        /// Gets the size of this surface.
        /// </summary>
        public Size Size
        {
            get { return new Size(Width, Height); }
        }

        /// <summary>
        /// Gets the effective bounding Rectangle for this surface, always located at 0,0.
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, Width, Height); }
        }

        #endregion

        #region Buffer Access

        /// <summary>
        /// Obtains a pointer to the internal memory buffer used by this surface.
        /// </summary>
        public BufferInfo GetBuffer()
        {
            BufferInfo buffer;
            CheckResult(GdApi.CGapiRGBASurface_GetBuffer(Handle, out buffer));
            return buffer;
        }

        /// <summary>
        /// Releases the previously locked internal memory buffer used by this surface.
        /// </summary>
        public void ReleaseBuffer()
        {
            CheckResult(GdApi.CGapiRGBASurface_ReleaseBuffer());
        }

        #endregion
    }
}
