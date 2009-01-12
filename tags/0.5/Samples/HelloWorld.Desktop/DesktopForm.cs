using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HelloWorld.Desktop
{
    /// <summary>
    /// Modifies the DeviceForm in the HelloWorld sample to draw into a window on the desktop using GDI.
    /// </summary>
    public partial class DesktopForm : DeviceForm
    {
        public DesktopForm()
        {
            this.WindowState = FormWindowState.Normal;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.ClientSize = new Size(240, 320);
            this.Text = "GapiDrawNet";
        }

        protected override void OpenDisplay()
        {
            Display.CreateOffscreenDisplay(ClientSize.Width, ClientSize.Height);
        }

        protected override void PaintBuffer(Graphics g)
        {
            IntPtr hDC = g.GetHdc();
            IntPtr hBufferDC = Display.BackBuffer.GetDC();

            BitBlt(hDC, 0, 0, ClientSize.Width, ClientSize.Height, hBufferDC, 0, 0, SRCCOPY);

            g.ReleaseHdc(hDC);
            Display.BackBuffer.ReleaseDC(hBufferDC);
        }

        const uint SRCCOPY = 0x00CC0020;

        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight,
            IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
    }
}
