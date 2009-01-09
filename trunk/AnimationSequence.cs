using System;
using System.IO;
using System.Drawing;

namespace GapiDrawNet
{
	public class FrameSettings
	{
		public FrameSettings()
		{
		}
		public GapiDrawNet.GDRect DrawRect;

		private int _XOffset = 0;
		public int XOffset
		{
			get { return _XOffset; }
			set
			{
				_XOffset = value;
				_XOffsetMirrored = DrawRect.Width - _XOffset;
			}
		}

		public int AttackX = -1;
		public int AttackY = -1;

		public override string ToString()
		{
			return String.Format("L {0} T {1} B {2} R {3} XO {4} Y) {5}", DrawRect.Left, DrawRect.Top, DrawRect.Bottom, DrawRect.Right, XOffset, YOffset);
		}

		private int _XOffsetMirrored = 0;
		public int XOffsetMirrored
		{
			get { return _XOffsetMirrored; }
		}
		
		public GDRect GetBoundingRect()
		{
			return new GDRect(-XOffset, -YOffset, XOffsetMirrored, DrawRect.Height - YOffset);
		}

		public GDRect GetBoundingRectXMirrored()
		{
			return new GDRect(-XOffsetMirrored, -YOffset, XOffset, DrawRect.Height - YOffset);
		}

		public int YOffset = 0;

		const string FRAMEHEADER = "FrameSettings";
		const int VERSION = 1;

		public void SaveToStream(Stream stream)
		{
			BinaryWriter writer = new BinaryWriter(stream);

			writer.Write(FRAMEHEADER);
			writer.Write(VERSION);

			writer.Write(DrawRect.Left);
			writer.Write(DrawRect.Top);
			writer.Write(DrawRect.Right);
			writer.Write(DrawRect.Bottom);

			writer.Write(XOffset);					
			writer.Write(YOffset);

			writer.Write(AttackX);	
			writer.Write(AttackY);	


		}

		public void LoadFromStream(Stream stream)
		{
			BinaryReader reader = new BinaryReader(stream);

			AnimationSequence.CheckString(reader, FRAMEHEADER, "Wrong frame header.");
			
			int version =  reader.ReadInt32();

			DrawRect.Left =  reader.ReadInt32();
			DrawRect.Top =  reader.ReadInt32();
			DrawRect.Right =  reader.ReadInt32();
			DrawRect.Bottom =  reader.ReadInt32();
			XOffset =  reader.ReadInt32();
			YOffset =  reader.ReadInt32();

			AttackX =  reader.ReadInt32();
			AttackY =  reader.ReadInt32();
		}
	}

	public class FrameSequence
	{
		public FrameSequence()
		{
		}

		FrameSettings[] Frames;

		public bool Looped = false;

		public int Length
		{
			get 
			{ 
				if(Frames == null){ return 0; }
				return Frames.Length;
			}

			set
			{
				Frames = new FrameSettings[value];
			}
		}
		public void  SetFrame(int frameNumber, FrameSettings frame)
		{
			Frames[frameNumber]= frame;
		}

		public FrameSettings  GetFrame(int frameNumber)
		{
			return Frames[frameNumber];
		}

		public bool IsLastFrame(int frameNumber)
		{
			return frameNumber >= Frames.Length -1;
		}

		public int NextFrame(int frameNumber)
		{
			frameNumber++;  
			if(frameNumber >= Frames.Length)
			{
				if(Looped)
				{
					return 0;
				}
				else
				{
					return Length - 1;
				}
			}
			return frameNumber;
		}
		
		const string SEQUENCEHEADER = "FrameSequence";
		const string FRAME_COUNT = "Frame Count";
		const int VERSION = 2;

		public void SaveToStream(Stream stream)
		{
			BinaryWriter writer = new BinaryWriter(stream);

			writer.Write(SEQUENCEHEADER);
			writer.Write(VERSION);

			writer.Write(Looped);

			writer.Write(FRAME_COUNT);
			int frameCount = Frames.Length;
			
			writer.Write(frameCount);
			for(int frameNo = 0; frameNo < frameCount; frameNo++)
			{
				//testWriter.Write("F: " + frameNo.ToString() + " Settings: " + Frames[sequence][frameNo].ToString() + "\r\n");
				Frames[frameNo].SaveToStream(stream);
			}			
		}

		public void LoadFromStream(Stream stream)
		{
			BinaryReader reader = new BinaryReader(stream);

			AnimationSequence.CheckString(reader, SEQUENCEHEADER, "Wrong sequence header.");
			
			int version = reader.ReadInt32();

			Looped = reader.ReadBoolean();

			AnimationSequence.CheckString(reader, FRAME_COUNT, "Wrong frame count header.");
			int frameCount = reader.ReadInt32();

			Length = frameCount;
			
			for(int frameNo = 0; frameNo < frameCount; frameNo++)
			{

				FrameSettings frame = new FrameSettings();
				frame.LoadFromStream(stream);
				Frames[frameNo] = frame;

			}	
		}
	}

	public class AnimationSequence
	{
		public AnimationSequence()
		{

		}

		public FrameSequence[] Sequences;

		public int SequenceCount
		{
			get 
			{ 
				if(Sequences == null){ return 0; }
				return Sequences.Length;
			}

			set
			{
				Sequences = new FrameSequence[value];
				for(int f = 0; f < value; f++)
				{
					Sequences[f] = new FrameSequence();
				}
			}
		}

		public void SetSequenceLength(int sequence, int length)
		{
			Sequences[sequence].Length =length;
		}

		public int GetSequenceLength(int sequence)
		{
			if(Sequences == null){ return 0; }
			if(Sequences[sequence] == null){ return 0; }
			return Sequences[sequence].Length;
		}

		public void  SetFrame(int sequence, int frameNumber, FrameSettings frame)
		{
			Sequences[sequence].SetFrame(frameNumber, frame);
		}

		public FrameSettings  GetFrame(int sequence, int frameNumber)
		{
			return Sequences[sequence].GetFrame(frameNumber);
		}

		public int NextFrame(int sequence, int frameNumber)
		{
			return Sequences[sequence].NextFrame(frameNumber);
		}

		const string FILEHEADER = "Anim Sequence";
		const string SEQUENCE_COUNT = "Sequence Count";		
		const int VERSION = 2;

		public void SaveToFile(string fileName)
		{
			Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
			try
			{				
				SaveToStream(stream);
			}
			finally
			{
				stream.Close();
			}


		}		

		public void SaveToStream(Stream stream)
		{
			BinaryWriter writer = new BinaryWriter(stream);
			StreamWriter testWriter = new StreamWriter("anim.log");
			try
			{
				writer.Write(FILEHEADER);
				writer.Write(VERSION);
					
				writer.Write(SEQUENCE_COUNT);
				int sequenceCount = Sequences.Length;
				writer.Write(sequenceCount);

				for(int sequence = 0; sequence < sequenceCount; sequence++)
				{
					//testWriter.Write("S: " + sequence.ToString() + " F: " + frameNo.ToString() + " Settings: " + Frames[sequence][frameNo].ToString() + "\r\n");
					Sequences[sequence].SaveToStream(stream);
				}
			}
			finally
			{
				writer.Close();
				testWriter.Close();
			}
		}

		public void LoadFromFile(string fileName)
		{
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
			try
			{				
				LoadFromStream(stream);
			}
			finally
			{
				stream.Close();
			}
		}
	
		public void LoadFromStream(Stream stream)
		{
			BinaryReader reader = new BinaryReader(stream);
			try
			{
				CheckString(reader, FILEHEADER, "Wrong file header.");

				int version =  reader.ReadInt32();
				
				CheckString(reader, SEQUENCE_COUNT, "Wrong sequence count header.");
				int sequenceCount =  reader.ReadInt32();
				
				SequenceCount = sequenceCount;

				for(int sequence = 0; sequence < sequenceCount; sequence++)
				{
					Sequences[sequence].LoadFromStream(stream);
				}
			}
			finally
			{
				reader.Close();
			}
		}

		static public void CheckString(BinaryReader reader, string expected, string message)
		{
			if(reader.ReadString() != expected)
			{
				throw new Exception(message);
			}
		}
	}


}
