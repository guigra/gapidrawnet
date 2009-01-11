using System;
using System.Drawing;
using System.Windows.Forms;
using GapiDrawNet;

namespace HelloWorld
{
    /// <summary>
    /// A basic example of one technique for drawing with GapiDraw and getting the results
    /// onto the screen.
    /// </summary>
    public partial class DeviceForm : Form
    {
        public GapiDisplay Display { get; private set; }

        public DeviceForm()
        {
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Display = new GapiDisplay();
            OpenDisplay();
            
            // draw some stuff onto the BackBuffer
            DrawStuff.Onto(Display.BackBuffer, Display.SystemFont);
        }

        // Quit the app when you click anywhere
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Close();
        }

        protected virtual void OpenDisplay()
        {
            Display.OpenDisplay(OpenDisplayOptions.FullScreen, Handle, Width, Height);
        }

        protected virtual void PaintBuffer(Graphics g)
        {
            Display.Flip();
        }

        protected override void Dispose(bool disposing)
        {
            Display.Dispose();
            GapiDraw.Dispose();

            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintBuffer(e.Graphics);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // don't let windows paint the background color, it may cause flicker
        }
    }
}