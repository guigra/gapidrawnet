using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for GapiForm.
	/// </summary>
	public class GapiForm : IGapiForm 
	{
		public GapiApplication GDApplication;
		public GapiSurface BackBuffer = null;

		public GapiForm(GapiApplication application)
		{
			GDApplication = application;
			BackBuffer = application.BackBuffer;
		}

		public virtual void Dispose()
		{
			if( _Widgets != null)
			{
				foreach(GapiWidget widget in _Widgets)
				{
					widget.Dispose();
				}
			}
		}


		protected WidgetList _Widgets = null;
		public WidgetList Widgets
		{
			get
			{
				if(_Widgets == null)
				{
					_Widgets = new WidgetList();
				}
				return _Widgets;
			}
		}

		protected bool _Closed = false;
		public bool Closed
		{
			get { return _Closed; }
		}

		public void AddWidget(GapiWidget widget)
		{
			Widgets.Add(widget);
		}

		public virtual void Activate()
		{
			
		}

		public virtual void Deactivate()
		{
		}

		public virtual void ReActivate()
		{
		}
//		public virtual void ReActivate()
//		{
//		}

		public virtual void CreateSurfaces(GapiDisplay gapiDisplay)
		{
			BackBuffer = GDApplication.BackBuffer;

			if( _Widgets != null)
			{
				foreach(GapiWidget widget in _Widgets)
				{
					widget.CreateSurfaces(gapiDisplay);
				}
			}		
		}

		public virtual void ProcessNextFrame(bool frameOverflow)
		{
			if( _Widgets != null)
			{
				foreach(GapiWidget widget in _Widgets)
				{
					if(widget.Visible)
					{
						widget.Draw(GDApplication.BackBuffer);
					}
				}
			}

		}

		public virtual void StylusDown(GapiWidget widget, Point p, System.Windows.Forms.MouseEventArgs e)
		{
		}

		public virtual void StylusDown(Point p, System.Windows.Forms.MouseEventArgs e)
		{
			if( _Widgets != null)
			{
				for(int f = _Widgets.Count - 1; f >= 0; f--)
				{
					GapiWidget widget = _Widgets[f];

					if(widget.Bounds.Contains(p))
					{
						widget.StylusDown(p, e);
						StylusDown(widget, p, e);
						return;
					}
				}
			}
		}

		public virtual void StylusUp(Point p, System.Windows.Forms.MouseEventArgs e)
		{
		
		}

		public virtual void StylusMove(Point p, System.Windows.Forms.MouseEventArgs e)
		{
		
		}

		public virtual void KeyDown(System.Windows.Forms.KeyEventArgs e)
		{
		}

		public virtual void KeyUp(System.Windows.Forms.KeyEventArgs e)
		{
		
		}

		public virtual void KeyPress(System.Windows.Forms.KeyPressEventArgs e)
		{
		
		}

		public void Close()
		{
			_Closed = true;
		}

		public void Open()
		{
			_Closed = false;
		}

	}
}
