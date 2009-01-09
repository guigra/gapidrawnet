using System;

namespace GapiDrawNet
{
	/// <summary>
	/// Summary description for Sprite.
	/// </summary>
	public class Sprite
	{
		public Sprite()
		{
		}

		public GapiSurface SourceSurface = null;
		public AnimationSequence Sequences = null;

		public int X = 0;
		public int Y = 0;
		public int SequenceNumber = 0;
		public int FrameNumber = 0;

		public bool MirrorLeftRight = false;

		public GDBLTFX Fx;

		public void DrawSprite(GapiSurface destSurface)
		{
			//FrameSettings frame = Sequences.GetFrame(SequenceNumber,FrameNumber );
			DrawFrame(destSurface, GetCurrentFrame(), MirrorLeftRight, X, Y);
		}

		public void NextFrame()
		{
			FrameNumber = Sequences.NextFrame(SequenceNumber, FrameNumber);
		}

		public bool IsLastFrame()
		{
			return Sequences.Sequences[SequenceNumber].IsLastFrame(FrameNumber);
		}

		public FrameSettings GetCurrentFrame()
		{
			return Sequences.GetFrame(SequenceNumber, FrameNumber);
		}

		public int GetMaxFrameNumber()
		{
			return Sequences.Sequences[SequenceNumber].Length -1;
		}

		public void SetSequenceNumber(int sequenceNumber)
		{
			SequenceNumber = sequenceNumber;
			FrameNumber = 0;
		}

		public void SetSequenceNumber(int sequenceNumber, int frameNumber)
		{
			SequenceNumber = sequenceNumber;
			FrameNumber = frameNumber;
		}

		public void NextSequence()
		{
			SequenceNumber++;
			FrameNumber = 0;
			if(SequenceNumber >= Sequences.SequenceCount)
			{
				SequenceNumber = 0;				
			}
		}

		private void DrawFrame(GapiSurface destSurface, FrameSettings frame, bool mirrorLeftRight, int X, int Y)
		{
			BltOptions options = BltOptions.GDBLT_KEYSRC;

			GDRect destRect;

			if(mirrorLeftRight)
			{
				destRect.Left = X - frame.XOffsetMirrored;
				options |= BltOptions.GDBLT_MIRRORLEFTRIGHT;
			}
			else
			{
				destRect.Left = X - frame.XOffset;
			}
			//destRect.Left = X - frame.XOffset;
			destRect.Right = destRect.Left + frame.DrawRect.Width;
			destRect.Top = Y - frame.YOffset;
			destRect.Bottom = destRect.Top + frame.DrawRect.Height;

			GDRect sourceRect = frame.DrawRect;			

			GapiUtility.ReduceBound(destRect.Right > destSurface.Width, ref sourceRect.Right, ref sourceRect.Left, mirrorLeftRight, ref destRect.Right, destRect.Right - destSurface.Width);		
			GapiUtility.ReduceBound(destRect.Left < 0, ref sourceRect.Left, ref sourceRect.Right, mirrorLeftRight, ref destRect.Left, destRect.Left);		

			if(sourceRect.Right < sourceRect.Left){ return;}

		    destSurface.Blt(destRect, SourceSurface, sourceRect, options, ref Fx);
		}
	}
}
