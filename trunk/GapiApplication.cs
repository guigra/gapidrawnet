using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using GapiDrawNet;

namespace GapiDrawNet
{
	public interface IGapiForm : IDisposable
	{
		void CreateSurfaces(GapiDisplay gapiDisplay);
		void ProcessNextFrame(bool frameOverflow);
		void StylusDown(Point p, System.Windows.Forms.MouseEventArgs e);
		void StylusUp(Point p, System.Windows.Forms.MouseEventArgs e);
		void StylusMove(Point p, System.Windows.Forms.MouseEventArgs e);
		void KeyDown(System.Windows.Forms.KeyEventArgs e);
		void KeyUp(System.Windows.Forms.KeyEventArgs e);
		void KeyPress(System.Windows.Forms.KeyPressEventArgs e);
		void Activate();
		void ReActivate();
		void Deactivate();
		void Open();
		bool Closed		
		{ get ;}
	}

	public class GapiApplication : IDisposable
	{
		public GapiApplication()
		{
			Instance = this;
		}

		public static GapiApplication Instance;

		virtual public void Dispose()
		{
			CloseDisplay();
			CloseInput();
			CloseTimer();
		}

		protected  GapiDisplay gapiDisplay = null;
		public GapiDisplay Display
		{
			get { return gapiDisplay; }
		}

		private GapiSurface backBuffer = null;
		public GapiSurface BackBuffer
		{
			get { return backBuffer; }
		}

		public void Flip()
		{
			gapiDisplay.Flip();
		}

		private  DisplayMode displayMode = DisplayMode.GDDISPMODE_NORMAL;
		public DisplayMode Mode
		{
			get {return displayMode; }
			set { SetDisplayMode(value); }
		}

		protected  OpenDisplayOptions displayOptions = OpenDisplayOptions.GDDISPLAY_WINDOW;
		public OpenDisplayOptions DisplayOptions
		{
			get { return displayOptions; }
			set { displayOptions = value; }
		}
				
		public string Title = "";

		private void SetUpForm()
		{
			form = new Form();
			if(Title == "")
			{
				Title = this.GetType().ToString();
			}

			form.Text = Title;

			if(Title == "")
			{
				throw new Exception("GapiApplication.Title must be set");
			}

			form.Closed += new EventHandler(GapiForm_Closed);
			form.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GapiForm_KeyDown);
			form.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GapiForm_KeyUp);
			form.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GapiForm_KeyPress);
//			form.Click += new System.EventHandler(this.GapiForm_Click);
			form.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GapiForm_MouseDown);
//			form.DoubleClick += new System.EventHandler(this.GapiForm_DoubleClick);
			form.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GapiForm_MouseUp);
			form.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GapiForm_MouseMove);
			form.Paint += new System.Windows.Forms.PaintEventHandler(this.GapiForm_Paint);

			if((DisplayOptions & OpenDisplayOptions.GDDISPLAY_FULLSCREEN) != 0)
			{
				// hide border if fullscreen to correct x position
				form.FormBorderStyle = FormBorderStyle.None;
			}

			form.Show();
			form.Deactivate += new EventHandler(GapiForm_Deactivate);
			form.Activated += new EventHandler(GapiForm_Activated);
		}

		protected int Width = 240;
		protected int Height = 320;
		protected int ZoomWidth = 0;
		protected int ZoomHeight = 0;
		protected int BPP = 16;
		protected int Hz = 0;
		private void SetUpDisplay()
		{
			CloseDisplay();

			try
			{
				SetUpForm();
				gapiDisplay = new GapiDisplay();
				//gapiDisplay.OpenDisplay(Title, displayOptions);
				gapiDisplay.OpenDisplay(Title, displayOptions, Width, Height, ZoomWidth, ZoomWidth, BPP, Hz);
				SetDisplayMode(displayMode);
			}
			catch
			{
				CloseDisplay();
				throw;
			}
		}

		
		public GapiTimer Timer = null;

		private int targetFrameRate = 30;
		public int TargetFrameRate
		{
			get { return targetFrameRate; }
			set 
			{ 
				targetFrameRate = value;
				if(Timer != null)
				{		
					SetUpTimer();
				}
			}
		}

		private void SetUpTimer()
		{
			Timer = new GapiTimer();
			Timer.StartTimer(targetFrameRate);
		}

		private void CloseTimer()
		{
			if(Timer != null)
			{
				Timer.Dispose();
				Timer = null;
			}
		}

		protected  GapiInput gapiInput;

		public GDKEYLIST KeyList;

		private void SetUpInput()
		{
			gapiInput = new GapiInput();
			gapiInput.OpenInput();
			UpdateKeys();
		}

		private void UpdateKeys()
		{
			if(gapiInput != null)
			{
				gapiInput.GetKeyList(ref KeyList);
			}
		}

		private void CloseInput()
		{
			if(gapiInput != null)
			{
				gapiInput.Dispose();
				gapiInput = null;
			}
		}

		public void RotateDisplay()
		{
			switch(gapiDisplay.DisplayMode)
			{
				case DisplayMode.GDDISPMODE_NORMAL: 
					SetDisplayMode(DisplayMode.GDDISPMODE_ROTATE90CCW);
					break;
				case DisplayMode.GDDISPMODE_ROTATE90CCW:
					SetDisplayMode(DisplayMode.GDDISPMODE_ROTATE90CW);
					break;
				case DisplayMode.GDDISPMODE_ROTATE90CW:
					SetDisplayMode(DisplayMode.GDDISPMODE_ROTATE180);
					break;
				case DisplayMode.GDDISPMODE_ROTATE180:
					SetDisplayMode(DisplayMode.GDDISPMODE_NORMAL);
					break;
				default:
					SetDisplayMode(DisplayMode.GDDISPMODE_NORMAL);
					break;
			}
		}

		private void SetDisplayMode(DisplayMode mode)
		{			
			displayMode = mode; 

			if(gapiDisplay != null)
			{
                gapiDisplay.DisplayMode = mode;
				backBuffer = gapiDisplay.BackBuffer;

				UpdateKeys();

				CreateSurfaces(gapiDisplay);
			}
		}

		private void CloseDisplay()
		{
			if(gapiDisplay != null)
			{
				gapiDisplay.Dispose();
				gapiDisplay = null;
			}

			if(form != null)
			{
				form.Close();
				form.Dispose();
				form = null;
			}
		}


		int CreateSurfacesChangeCount = 0;
		virtual protected void CreateSurfaces(GapiDisplay gapiDisplay)
		{
			CreateSurfacesChangeCount++;

			if(_CurrentForm != null)
			{
				_CurrentForm.CreateSurfaces(gapiDisplay);
			}
		}

		private IGapiForm _CurrentForm = null;
		protected IGapiForm CurrentForm
		{
			get { return _CurrentForm; }
			set
			{
				if(value == _CurrentForm){ return; }
				if( _CurrentForm != null)
				{
					_CurrentForm.Deactivate();
				}
				_CurrentForm = value;
				if(_CurrentForm != null)
				{
					_CurrentForm.Open();
					_CurrentForm.Activate();
					_CurrentForm.CreateSurfaces(gapiDisplay);					
				}
			}
		}

		virtual protected void ProcessNextFrame(bool frameOverflow)
		{
			if(_CurrentForm != null)
			{
				_CurrentForm.ProcessNextFrame(frameOverflow);
				return;
			}

			gapiDisplay.BackBuffer.FillRect(0, 0);
			gapiDisplay.BackBuffer.DrawText(gapiDisplay.Width / 2,gapiDisplay.Height / 2, "Press any key to exit", gapiDisplay.GetSystemFontPtr(), DrawTextOptions.GDDRAWTEXT_CENTER);
		}

		public void Shutdown()
		{
			isFinished = true;
		}

		private bool isFinished = false;
		private bool isSuspended = false;
		
		private Form form;

		private void SetUpForRun()
		{
			SetUpDisplay();
			SetUpTimer();
			SetUpInput();
		}

		public static uint FrameCount = 0;
		public void Run()
		{

			try
			{
				SetUpForRun();
				
				while(!isFinished)
				{
					GapiResults timerResult = Timer.WaitForNextFrame();
					Application.DoEvents();
					//System.Threading.Thread.Sleep(1);
					
					if(!isFinished && !isSuspended)  // can finish duing DoEvents
					{
						FrameCount++;
						bool frameOverflow = (timerResult == GapiResults.GDERR_FRAMETIMEOVERFLOW);
						ProcessNextFrame(frameOverflow);
					
						gapiDisplay.Flip();
					}
				}

			}
			finally
			{
				//System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
				ShutDownAfterRun();				
			}
		}

		public void Run(IGapiForm form)
		{
			IGapiForm oldForm = _CurrentForm;
			int oldCreateSurfacesChangeCount = CreateSurfacesChangeCount;

			try
			{
				if(oldForm == null)
				{
					SetUpForRun();
				}

				CurrentForm = form;
				try
				{
					form.Open();
					while(!isFinished && !isSuspended && !form.Closed)
					{
						GapiResults timerResult = Timer.WaitForNextFrame();
						Application.DoEvents();
						if(!isFinished && !isSuspended)  // can finish duing DoEvents
						{
							FrameCount++;
							bool frameOverflow = (timerResult == GapiResults.GDERR_FRAMETIMEOVERFLOW);
							form.ProcessNextFrame(frameOverflow);	
							if(gapiDisplay != null)
							{
								gapiDisplay.Flip();
							}
						}
					}
				}
				finally
				{
					_CurrentForm = oldForm;
					if(oldForm != null)
					{
						oldForm.ReActivate();
						if(oldCreateSurfacesChangeCount != CreateSurfacesChangeCount)
						{
							oldForm.CreateSurfaces(gapiDisplay);
						}
					}
				}

			}
			finally
			{
				//System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
				if((oldForm == null) || isFinished)
				{
					ShutDownAfterRun();				
				}
			}
		}

		private void ShutDownAfterRun()
		{
			CloseDisplay();
			CloseTimer();
			CloseInput();
		}

// key events
		private void GapiForm_Closed(object sender, EventArgs  e)
		{
			Shutdown();
		}

		private void GapiForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			KeyDown(e);
		}

		private void GapiForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			KeyUp(e);
		}

		private void GapiForm_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			KeyPress(e);
		}

		virtual protected void KeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			if(_CurrentForm != null)
			{
				_CurrentForm.KeyDown(e);
			}
		}

		virtual protected void KeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			if(_CurrentForm == null)
			{

				return;
			}
			_CurrentForm.KeyUp(e);
			
		}

		virtual protected void KeyPress(System.Windows.Forms.KeyPressEventArgs e)
		{
			if(_CurrentForm == null)
			{

				return;
			}
			_CurrentForm.KeyPress(e);
			
		}

		private void GapiForm_Deactivate(object sender, EventArgs e)
		{
			if( gapiDisplay == null || gapiInput == null) { return; }

			gapiDisplay.SuspendDisplay();
			gapiInput.CloseInput();

			isSuspended =true;
		}

		private void GapiForm_Activated(object sender, EventArgs e)
		{
			if( gapiDisplay == null ) { return; }

			if(isSuspended)
			{
				gapiDisplay.ResumeDisplay();
				gapiInput.OpenInput();
				UpdateKeys();
				isSuspended = false;
			}
		}

// mouse events
		private void GapiForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point p = new Point(e.X, e.Y);
			gapiDisplay.DeviceToLogicalPoint(ref p);
			StylusDown(p, e);
		}

		private void GapiForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point p = new Point(e.X, e.Y);
			gapiDisplay.DeviceToLogicalPoint(ref p);
			StylusMove(p, e);
		}

		private void GapiForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Paint();
		}
	
		protected virtual void Paint()
		{
		}

		private Point lastPos = new Point(0,0);

		private void GapiForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point p = new Point(e.X, e.Y);
			gapiDisplay.DeviceToLogicalPoint(ref p);
			lastPos = p;
			StylusUp(p, e);
		}

//		private void GapiForm_Click(object sender, System.EventArgs e)
//		{
//			StylusClick(lastPos);
//		}

//		private void GapiForm_DoubleClick(object sender, System.EventArgs e)
//		{
//			StylusDoubleClick(lastPos);
//		}

//		virtual protected void StylusClick(Point p)
//		{
//			RotateDisplay();
//		}

//		virtual protected void StylusDoubleClick(Point p)
//		{
//			RotateDisplay();
//		}

		virtual protected void StylusDown(Point p, System.Windows.Forms.MouseEventArgs e)
		{
			if(_CurrentForm != null)
			{
				_CurrentForm.StylusDown(p, e);
			}
		}

		virtual protected void StylusUp(Point p, System.Windows.Forms.MouseEventArgs e)
		{
			if(_CurrentForm != null)
			{
				_CurrentForm.StylusUp(p, e);
			}	
		}

		virtual protected void StylusMove(Point p, System.Windows.Forms.MouseEventArgs e)
		{
			if(_CurrentForm != null)
			{
				_CurrentForm.StylusMove(p, e);
			}	
		}
	}
}

