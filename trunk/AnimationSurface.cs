using System;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for AnimationSurface.
	/// </summary>
	public class AnimationSurface : GapiSurface
	{
		public AnimationSurface()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		//public GDRect FrameSize;

		private int _FrameCountX = 0;
		public int FrameCountX
		{
			get { return _FrameCountX; }
			set
			{
				_FrameCountX = value;

				if(value > 0)
				{
					_FrameWidth = Width / value;
				//	_FrameHeight = Height / value;
				}
				else
				{
					_FrameCountX = 0;
					_FrameWidth = 0;
				//	_FrameHeight = 0;

				}
			}
		}
	
		public int XOffset = 0;
		public int YOffset = 0;

		private int _FrameCountY = 0;
		public int FrameCountY
		{
			get { return _FrameCountY; }
			set
			{
				_FrameCountY = value;

				if(value > 0)
				{
					//_FrameWidth = Width / value;
					_FrameHeight = Height / value;
				}
				else
				{
					_FrameCountY = 0;
					//_FrameWidth = 0;
					_FrameHeight = 0;

				}
			}
		}

		private int _FrameCount;
		public int FrameCount
		{
			get { return _FrameCount; }
		}

		private int _FrameWidth;
		public int FrameWidth
		{
			get {return _FrameWidth; }
		}

		private int _FrameHeight;
		public int FrameHeight
		{
			get {return _FrameHeight; }
		}

		public int CalcFrameYFromFrameNum(int FramesAcross, int FrameNum)
		{
			int result = (int)(FrameNum / FramesAcross); 

			return result;

		}

		public GDRect[] FrameRects = new GDRect[0];

		public BltOptions Options = BltOptions.GDBLT_KEYSRC;
		public void SetOptionsValue()
		{
			Options = BltOptions.GDBLT_KEYSRC;
			if(_MirrorLeftRight)
			{
				Options |= BltOptions.GDBLT_MIRRORLEFTRIGHT;
			}
		}

		private bool _MirrorLeftRight = false;
		public bool MirrorLeftRight
		{
			get { return _MirrorLeftRight; }
			set
			{
				_MirrorLeftRight = value;
				SetOptionsValue();
			}
		}

		public GDBLTFX Fx;

		GDRect destRect;		

		public void DrawFrame(GapiSurface destSurface, int X, int Y, int frameNumber, int xOffset, int yOffset, bool mirrorLeftRight)
		{

			destRect.Left = X - xOffset;
			destRect.Right = destRect.Left + _FrameWidth;
			destRect.Top = Y - yOffset;
			destRect.Bottom = destRect.Top + _FrameHeight;

			GDRect sourceRect = FrameRects[frameNumber];			

			GapiUtility.ReduceBound(destRect.Right > destSurface.Width, ref sourceRect.Right, ref sourceRect.Left, mirrorLeftRight, ref destRect.Right, destRect.Right - destSurface.Width);		
			GapiUtility.ReduceBound(destRect.Left < 0, ref sourceRect.Left, ref sourceRect.Right, mirrorLeftRight, ref destRect.Left, destRect.Left);		

			if(sourceRect.Right < sourceRect.Left){ return;}

			destSurface.Blt(destRect, this, sourceRect, Options, ref Fx);
		}

		public void DrawFrame(GapiSurface destSurface, FrameSettings frame, bool mirrorLeftRight, int X, int Y)
		{
			BltOptions options = BltOptions.GDBLT_KEYSRC;

			GDRect destRect;

			if(mirrorLeftRight)
			{
				destRect.Left = X - frame.XOffset;
				options |= BltOptions.GDBLT_MIRRORLEFTRIGHT;
			}
			else
			{
				destRect.Left = X - frame.XOffsetMirrored ;
			}
			destRect.Left = X - frame.XOffset;
			destRect.Right = destRect.Left + frame.DrawRect.Width;
			destRect.Top = Y - frame.YOffset;
			destRect.Bottom = destRect.Top + frame.DrawRect.Height;

			GDRect sourceRect = frame.DrawRect;			

			GapiUtility.ReduceBound(destRect.Right > destSurface.Width, ref sourceRect.Right, ref sourceRect.Left, mirrorLeftRight, ref destRect.Right, destRect.Right - destSurface.Width);		
			GapiUtility.ReduceBound(destRect.Left < 0, ref sourceRect.Left, ref sourceRect.Right, mirrorLeftRight, ref destRect.Left, destRect.Left);		

			if(sourceRect.Right < sourceRect.Left){ return;}

			destSurface.Blt(destRect, this, sourceRect, options, ref Fx);
		}

		public UInt32 CreateFramesFromFile(string fileName, int frameCountX, int frameCountY)
		{
			UInt32 result = CreateSurface(fileName);

			if(result != (UInt32)GapiResults.GD_OK)
			{
				return result;
			}

			_FrameCountX = frameCountX;
			_FrameCountY = frameCountY;
			_FrameCount = frameCountX * frameCountY;

			_FrameWidth = Width / _FrameCountX;
			_FrameHeight = Height / _FrameCountY;

			FrameRects = new GDRect[_FrameCount];

			for(int x = 0; x < _FrameCountX; x++)
			{
				for(int y = 0; y < _FrameCountY; y++)
				{
					int frame = x + y * _FrameCountX;					
					FrameRects[frame] = GetFrameRect(x,y);
				}
			}

			return result;
		}

		private GDRect GetFrameRect(int x, int y)
		{
			return new GDRect(x* _FrameWidth, y * _FrameHeight, (x + 1) * _FrameWidth - 1, (y + 1) * _FrameHeight - 1);
		}

		public UInt32 CreateFramesFromFileSeries(string formatFileName, int startFrameNumber, int endFrameNumber, int FramesAcross)
		{
			string fileName = string.Format(formatFileName, startFrameNumber);

			GapiSurface tempFrame = new GapiSurface();
			tempFrame.CreateSurface(0, fileName);

			_FrameWidth = tempFrame.Width;
			_FrameHeight = tempFrame.Height;
			_FrameCount = endFrameNumber - startFrameNumber + 1;

			FrameRects = new GDRect[_FrameCount];

			_FrameCountX = FramesAcross;
			_FrameCountY = CalcFrameYFromFrameNum(FramesAcross, _FrameCount + FramesAcross - 1);

			CreateSurface(0, _FrameWidth * _FrameCountX, _FrameHeight * _FrameCountY);
 
			FrameRects[0] = GetFrameRect(0,0);

			BltFast(0,0, tempFrame);
 
			for(int frameNumber = 1; frameNumber < _FrameCount; frameNumber++)
			{
				int fileIndex = frameNumber + startFrameNumber;
				int X = (frameNumber) % _FrameCountX;
				int Y = CalcFrameYFromFrameNum(FramesAcross, frameNumber); 
				fileName = string.Format(formatFileName, fileIndex);
				tempFrame.CreateSurface(0, fileName);

				BltFast(X * _FrameWidth, Y * _FrameHeight, tempFrame);
				//DrawText(X * _FrameWidth + 8, Y * _FrameHeight + 24, fileIndex.ToString(), 0xffffff, DrawTextOptions.GDDRAWTEXT_CENTER);

				FrameRects[frameNumber] = GetFrameRect(X,Y);

			}

			return (UInt32)GapiResults.GD_OK;
		}
	}
}
