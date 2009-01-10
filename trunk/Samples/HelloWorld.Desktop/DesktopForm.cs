using System;
using System.Drawing;
using System.Windows.Forms;

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
            Display.CreateOffscreenDisplay(0, ClientSize.Width, ClientSize.Height);
        }

        protected override void PaintBuffer(Graphics g)
        {
            IntPtr hDC = g.GetHdc();
            Display.DrawOntoDcXp(hDC, 0, 0);
            g.ReleaseHdc();
        }
    }
}
