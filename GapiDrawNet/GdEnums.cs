using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GapiDrawNet
{
	[Flags]
	public enum OpenDisplayOptions : uint
	{
		/// <summary>Request a full screen lock of the display</summary>
        FullScreen             = 0x0010,
		/// <summary>Open the display in windowed mode - default</summary>
        Window                 = 0x0020,
		/// <summary>Backbuffer is stored in system memory - default windowed</summary>
        BackBufferSystemMemory = 0x0040,
		/// <summary>Backbuffer is stored in video memory - default full screen</summary>
        BackBufferVideoMemory  = 0x0080,
        /// <summary>(Windows Mobile) Quarter size display - store all surface in system memory by default</summary>
        QuarterSize = 0x0100,
        /// <summary>Try to use VSync when calling Flip() to prevent image tearing.</summary>
        VSync = 0x1000
    }

	[Flags]
	public enum CreateSurfaceOptions : uint
	{
        // Surface flags used in CreateSurface

        /// <summary>The surface is cleared after creation</summary>
        Clear = 0x0001,
        /// <summary>Use only alpha information when loading a transparent image</summary>
        Alpha = 0x0002,
        /// <summary>Coordinate verifications are disabled</summary>
        NoCoordsCheck = 0x0004,
        /// <summary>The surface is stored in system memory - default windowed</summary>
        SystemMemory = 0x0100,
        /// <summary>The surface is stored in video memory - default full screen</summary>
        VideoMemory = 0x0200,
        /// <summary>(Stationary PCs) The surface is stored in local video memory - default</summary>
        LocalVideoMemory = 0x0400,
        /// <summary>(Stationary PCs) The surface is stored in non local video memory</summary>
        NonLocalVideoMemory = 0x0800,
	}

    [Flags]
    public enum SurfaceOptions : uint
    {
        // Surface flags used in CreateSurface

        /// <summary>The surface is cleared after creation</summary>
        Clear = 0x0001,
        /// <summary>Use only alpha information when loading a transparent image</summary>
        Alpha = 0x0002,
        /// <summary>Coordinate verifications are disabled</summary>
        NoCoordsCheck = 0x0004,
        /// <summary>The surface is stored in system memory - default windowed</summary>
        SystemMemory = 0x0100,
        /// <summary>The surface is stored in video memory - default full screen</summary>
        VideoMemory = 0x0200,
        /// <summary>(Stationary PCs) The surface is stored in local video memory - default</summary>
        LocalVideoMemory = 0x0400,
        /// <summary>(Stationary PCs) The surface is stored in non local video memory</summary>
        NonLocalVideoMemory = 0x0800,

        // Surface flags set internally by GapiDraw

        /// <summary>The surface is locked</summary>
        Locked = 0x1000,
        /// <summary>The surface is video locked</summary>
        VideoLocked = 0x2000,
        /// <summary>The display surface can be drawn to directly</summary>
        Primary = 0x4000,
        /// <summary>The surface is stored as a GDI bitmap</summary>
        Gdi = 0x8000
    }

	[Flags]
	public enum RgbaSurfaceOptions : uint
	{
		/// <summary>The surface is cleared after creation</summary>
        Clear = 0x0001,
        /// <summary>Use 12-bit RGBA format (444A)</summary>
        Surface12Bit = 0x0010,
        /// <summary>Use 32-bit RGBA format (555A or 565A) - default</summary>
        Surface32Bit = 0x0020,
        /// <summary>Create the surface in system memory (default)</summary>
        SystemMemory = 0x0100
    }

	public enum DisplayMode : uint
	{
        /// <summary>Use normal (frame buffer aligned) display mode - default</summary>
        Normal = 0x0001,
        /// <summary>Rotate display 90 degrees counter clockwise</summary>
        Rotate90CCW = 0x0002,
        /// <summary>Rotate display 90 degrees clockwise</summary>
        Rotate90CW = 0x0004,
        /// <summary>Rotate display 180 degrees</summary>
        Rotate180 = 0x0008
    }

	public enum PixelFormat : uint
	{
        /// <summary>16-bit RGB  (xxxxrrrr ggggbbbb)</summary>
        Rgb444 = 0x0001,
        /// <summary>16-bit RGB  (xrrrrrgg gggbbbbb)</summary>
        Rgb555 = 0x0002,
        /// <summary>16-bit RGB  (rrrrrggg gggbbbbb) </summary>
        Rgb565 = 0x0004,
        /// <summary>16-bit RGBA (rrrrgggg bbbbaaaa)</summary>
        Rgb444A = 0x0010,
        /// <summary>32-bit RGBA (xrrrrrgg gggbbbbb xxxxxxxx aaaaaaaa)</summary>
        Rgb555A = 0x0020,
        /// <summary>32-bit RGBA (rrrrrggg gggbbbbb xxxxxxxx aaaaaaaa)</summary>
        Rgb565A = 0x0040,
    }

	[Flags]
	public enum BltOptions : uint
	{
        /// <summary>Uses the color key in the source surface</summary>
        KeySource = 0x0001,
        /// <summary>Uses the specified color instead of source image color (source image can be NULL)</summary>
        ColorFill = 0x0002,
        /// <summary>Specifies opacity of the source image (0 transparent - 255 opaque)</summary>
        Opacity = 0x0004,
        /// <summary>Mirrors the source image left-right</summary>
        MirrorLeftRight = 0x0010,
        /// <summary>Mirrors the source image upside down</summary>
        MirrorUpDown = 0x0020,
        /// <summary>Clockwise rotation angle in 1/100 degrees (3000 = 30.00 degrees)</summary>
        RotationAngle = 0x0100,
        /// <summary>Rotation scale in percentage (100 = 100% of original scale)</summary>
        RotationScale = 0x0200,
        /// <summary>Rotation pivot point center (can be negative)</summary>
        RotationCenter = 0x0400
    }

	[Flags]
	public enum BltFastOptions : uint
	{
        /// <summary>Masks out the colors specified by KEYSRC before blitting</summary>
        KeySource = 0x0001,
        /// <summary>Uses the specified color instead of source image color (source image can be NULL)</summary>
        ColorFill = 0x0002,
        /// <summary>Specifies opacity of the source image (0 transparent - 255 opaque)</summary>
        Opacity = 0x0004,
        /// <summary>FX Opacity: Uses the value in GDBLTFASTFX.dwFXopacity to control amount of effect (0-255)</summary>
        FxOpacity = 0x0008,
        /// <summary>FX Color multiply: Uses the color in GDBLTFASTFX.dwFXcolor1 to multiply source</summary>
        Multiply = 0x0010,
        /// <summary>FX Color screen: Uses the color in GDBLTFASTFX.dwFXcolor1 to screen source</summary>
        Screen = 0x0020,
        /// <summary>FX Color overlay: Uses the color in GDBLTFASTFX.dwFXcolor1 to overlay source</summary>
        Overlay = 0x0040,
        /// <summary>FX Color tint: Uses the angle in GDBLTFASTFX.dwFXparam1 to adjust source hue ((-180)-(180) degrees)</summary>
        Tint = 0x0100,
        /// <summary>FX Colorize: Uses the color in GDBLTFASTFX.dwFXcolor1 to colorize source</summary>
        Colorize = 0x0200
    }

	[Flags]
	public enum AlphaBltOptions : uint
	{
        /// <summary>Specifies opacity of the source image (0 transparent - 255 opaque)</summary>
        Opacity = 0x0001,
        /// <summary>Mirrors the source left-right</summary>
        MirrorLeftRight = 0x0010,
        /// <summary>Mirrors the source upside down</summary>
        MirrorUpDown = 0x0020
    }

	[Flags]
	public enum AlphaBltFastOptions : uint
	{
        /// <summary>Specifies opacity of the source image (0 transparent - 255 opaque)</summary>
        Opacity = 0x0001
    }

    [Flags]
    public enum SetPixelOptions : uint
    {
        // SetPixel options
        /// <summary>Uses opacity to blend the pixel with the background </summary>
        Opacity = 0x0001,
        /// <summary>Do not draw this pixel</summary>
        Disabled = 0x0100,
        /// <summary>Use 16:16 fixed point coordinates</summary>
        FixedPoint = 0x1000,
    }

	[Flags]
	public enum DrawLineOptions : uint
	{
        /// <summary>Uses opacity to blend the lines with the background </summary>
        Opacity = 0x0001,
        /// <summary>Uses a fast fixed point WU algorithm to smooth the line</summary>
        AntiAlias = 0x0002
    }

	[Flags]
	public enum FillRectOptions : uint
	{
        /// <summary>Specifies opacity of the rectangle (0 transparent - 255 opaque)</summary>
        Opacity = 0x0001
    }

    [Flags]
    public enum GradientRectOptions : uint
    {
        /// <summary>Specifies opacity of the rectangle (0 transparent - 255 opaque)</summary>
        Opacity = 0x0001,
        /// <summary>First color = top, last color = bottom (Default)</summary>
        TopToBottom = 0x0010,
        /// <summary>First color = bottom, last color = top</summary>
        BottomToTop = 0x0020,
        /// <summary>First color = left, last color = right</summary>
        LeftToRight = 0x0040,
        /// <summary>First color = right, last color = left</summary>
        RightToLeft = 0x0080
    }

	[Flags]
	public enum DrawTextOptions : uint
	{
        /// <summary>Draw left-aligned text (dwX = text left, dwY = text top) - default mode</summary>
        Left = 0x0001,
        /// <summary>Draw centered text (dwX = text center)</summary>
        Center = 0x0002,
        /// <summary>Draw right-aligned text (dwX = text right)</summary>
        Right = 0x0004
    }

	[Flags]
	public enum CreateFontOptions : uint
	{
        /// <summary>Font tracking (space between characters) in pixels</summary>
        Tracking = 0x0001,
        /// <summary>Use simple bitmap format with no adjustable character kerning</summary>
        SimpleBitmap = 0x0002
    }

	[Flags]
	public enum SaveSurfaceOptions : uint
	{
        /// <summary>Save surface as 24-bit BMP image - default</summary>
        Bmp = 0x0001,
        /// <summary>Save surface as true color PNG image</summary>
        Png = 0x0002
    }

	[Flags]
	public enum VideoHardwareFlags : uint
	{
        /// <summary>Video memory is available and enabled</summary>
        VideoMemoryEnabled = 0x0001,
        /// <summary>Video HW can sync flip to vertical blank</summary>
        VSync = 0x0010
    }

//	//	Return codes ______________________________________________________________

    public enum GapiResult : uint
    {
        /// <summary>OK</summary>
        Ok = 0,
        /// <summary>Memory allocation failed.</summary>
        OutOfMemory = 1,
        /// <summary>One or more arguments are either NULL or contain invalid values.</summary>
        InvalidParams = 2,
        /// <summary>The surface is not locked, and cannot be unlocked.</summary>
        NotLocked = ((uint)0xC8660010),
        /// <summary>Cannot unlock surface using DC since it was not locked using GetDC.</summary>
        NoDC = ((uint)0xC8660020),
        /// <summary>The surface is locked using a DC, and cannot be unlocked using Unlock().</summary>
        LockedDC = ((uint)0xC8660030),
        /// <summary>Attempt to create DC failed.</summary>
        CantCreateDC = ((uint)0xC8660040),
        /// <summary>The bitmap file could not be written.</summary>
        BitmapWriteError = ((uint)0xC8660050),
        /// <summary>The specified bitmap could not be found.</summary>
        BitmapNotFound = ((uint)0xC8660060),
        /// <summary>The bitmap file could not be parsed.</summary>
        InvalidBitmap = ((uint)0xC8660070),
        /// <summary>All surface objects must be created before use (OpenDisplay/CreateSurface).</summary>
        NotInitialized = ((uint)0xC8660080),
        /// <summary>Cannot call CreateSurface on objects of type CGapiDisplay.</summary>
        InvalidSurfaceType = ((uint)0xC8660090),
        /// <summary>Primary surface does not exist or is aligned different to the current surface.</summary>
        IncompatiblePrimary = ((uint)0xC86600a0),
        /// <summary>Cannot call OpenDisplay if a primary surface already has been assigned.</summary>
        PrimarySurfaceAlreadyExists = ((uint)0xC86600b0),
        /// <summary>One or more surfaces are locked, preventing operation.</summary>
        LockedSurfaces = ((uint)0xC86600c0),
        /// <summary>Keys have already been locked in a previous operation.</summary>
        LockedKeys = ((uint)0xC86600d0),
        /// <summary>One or more rectangles are invalid.</summary>
        InvalidRect = ((uint)0xC86600e0),
        /// <summary>Unable to maintain target frame rate</summary>
        FrameTimeOverflow = ((uint)0x486600f0),
        /// <summary>Invalid display mode</summary>
        InvalidMode = ((uint)0xC8660100),
        /// <summary>Unsupported display resolution or incompatible device</summary>
        UnsupportedMode = ((uint)0xC8660110),
        /// <summary>The GAPI file "gx.dll" is not available on the target device.</summary>
        NoGapi = ((uint)0xC8660120),
        /// <summary>The device does not have any video acceleration</summary>
        NoVideoHardware = ((uint)0xC8660130),
        /// <summary>The surface is not stored in video memory</summary>
        NoVideoSurface = ((uint)0xC8660140),
        /// <summary>The contents of a video surface was destroyed</summary>
        SurfaceLost = ((uint)0xC8660150),
        /// <summary>A surface is already busy with a hardware blit operation</summary>
        SurfaceBusy = ((uint)0xC8660160),
        /// <summary>This is a read-only surface</summary>
        ReadonlySurface = ((uint)0xC8660170)
    }

    /// <summary>
    /// GapiInput list of key codes.
    /// </summary>
    public struct KeyList
    {
        /// <summary>Key code for up</summary>
        public uint Up;
        /// <summary>Key code for down</summary>
        public uint Down;
        /// <summary>Key code for left</summary>
        public uint Left;
        /// <summary>Key code for right</summary>
        public uint Right;
        /// <summary>Hardware key 'Start' ('Enter' on stationary PCs)</summary>
        public uint Start;
        /// <summary>Hardware key #1</summary>
        public uint A;
        /// <summary>Hardware key #2</summary>
        public uint B;
        /// <summary>Hardware key #3</summary>
        public uint C;
    };

    /// <summary>
    /// Surface description returned from GapiDisplay.GetBuffer.
    /// </summary>
    public struct BufferInfo
    {
        /// <summary>Buffer width in pixels</summary>
        public uint Width;
        /// <summary>Buffer height in pixels</summary>
        public uint Height;
        /// <summary>xPitch in bytes</summary>
        public int PitchX;
        /// <summary>yPitch in bytes</summary>
        public int PitchY;
        /// <summary>Buffer pointer</summary>
        public IntPtr BufferPointer;
        /// <summary>Pixel format - currently GDPIXELFORMAT_555 and GDPIXELFORMAT_565 are supported</summary>
        public uint PixelFormat;
    };

    /// <summary>
    /// Blt parameters.
    /// </summary>
    public struct BltFX
    {
        /// <summary>Uses the specified color instead of source image color</summary>
        public uint FillColor;
        /// <summary>Specifies opacity of the source image (0 transparent - 128 (50% quick alpha) - 255 opaque)</summary>
        public uint Opacity;
        /// <summary>1/100 degrees clockwise rotation angle - can be negative</summary>
        public int RotationAngle;
        /// <summary>Scale rotated image by percentage (100 = 1:1 copy)</summary>
        public uint RotationScale;
        /// <summary>Rotation center on target surface</summary>
        public Point RotationCenter;
    };

    /// <summary>
    /// BltFast parameters.
    /// </summary>
    public struct BltFastFX
    {
        /// <summary>Uses the specified color instead of source image color</summary>
        public uint FillColorRef;
        /// <summary>Specifies opacity of the source image (0 transparent - 128 (50% quick alpha) - 255 opaque)</summary>
        public uint Opacity;
        /// <summary>Parameter1 to send to the blit effects. See documentation for details.</summary>
        public int FXParam1;
        /// <summary>Color1 to send to the blit effects. See documentation for details.</summary>
        public uint FXColor1Ref;
        /// <summary>The amount of blending to use for the effect. See documentation for details.</summary>
        public uint FXOpacity;
    };

    /// <summary>
    /// AlphaBlt parameters
    /// </summary>
    public struct AlphaBltFX
    {
        /// <summary>Specifies opacity of the source image (0 transparent - 128 (50% quick alpha) - 255 opaque)</summary>
        public uint Opacity;
    };

    /// <summary>
    /// AlphaBltFast parameters.
    /// </summary>
    public struct AlphaBltFastFX
    {
        /// <summary>Specifies opacity of the source image (0 transparent - 128 (50% quick alpha) - 255 opaque)</summary>
        public uint Opacity;
    };
    
    /// <summary>
    /// SetPixels parameters.
    /// </summary>
    public struct PixelFX
    {
        /// <summary>Specifies opacity of each pixel (0 transparent - 128 (50% quick alpha) - 255 opaque)</summary>
        public uint Opacity;
    };

    /// <summary>
    /// DrawLine/DrawRect parameters.
    /// </summary>
    public struct LineFX
    {
        /// <summary>Specifies opacity of the line (0 transparent - 128 (50% quick alpha) - 255 opaque)</summary>
        public uint Opacity;
    };

    /// <summary>
    /// FillRect parameters.
    /// </summary>
    public struct FillRectFX
    {
        /// <summary>Specifies opacity of the area (0 transparent - 128 (50% quick alpha) - 255 opaque)</summary>
        public uint Opacity;
    };

    /// <summary>
    /// GradientRect parameters.
    /// </summary>
    public struct GradientRectFX
    {
        /// <summary>Specifies opacity of the area (0 transparent - 128 (50% quick alpha) - 255 opaque)</summary>
        public uint Opacity;
    };

    /// <summary>
    /// DrawText parameters (currently unused).
    /// </summary>
    public struct DrawTextFX
    {
    };

    /// <summary>
    /// Bitmapped font creation.
    /// </summary>
    public struct FontFX
    {
        /// <summary>Put a space of n pixels between all font characters. Can be negative.</summary>
        public int Tracking;
    };

    /// <summary>
    /// Pixel structure used by SetPixels.
    /// </summary>
    public struct Pixel
    {
        public int X;
        public int Y;
        public uint ColorRef;
        public SetPixelOptions dwFlags;
        public PixelFX pixelfx;
    };

    /// <summary>
    /// This struct is used by the linked list version of GapiSurface.SetPixels.
    /// </summary>
    unsafe public struct PixelNode
    {
        public Pixel pixel;
        public PixelNode* pNext;
    };
}