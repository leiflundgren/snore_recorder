using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Specialized;


namespace KadeSoft
{
	/// <summary>
	/// This class gives you repurposable read/write access to a wave file.
	/// </summary>
	class WaveFileReader : IDisposable
	{
		public BinaryReader reader;

		riffChunk mainfile;
		fmtChunk format;
		factChunk fact;
		dataChunk data;

#region General Utilities
		/*
		 * WaveFileReader(string) - 2004 July 28
		 * A fairly standard constructor that opens a file using the filename supplied to it.
		 */
		public WaveFileReader(string filename)
		{
			reader = new BinaryReader(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read));
		}

		/*
		 * long GetPosition() - 2004 July 28
		 * Returns the current position of the reader's BaseStream.
		 */
		public long GetPosition()
		{
			return reader.BaseStream.Position;
		}

		/*
		 * string GetChunkName() - 2004 July 29
		 * Reads the next four bytes from the file, converts the 
		 * char array into a string, and returns it.
		 */
		public string GetChunkName()
		{
			return new string(reader.ReadChars(4));
		}

		/*
		 * void AdvanceToNext() - 2004 August 2
		 * Advances to the next chunk in the file.  This is fine, 
		 * since we only really care about the fmt and data 
		 * streams for now.
		 */
		public void AdvanceToNext()
		{
            long pos = GetPosition();
			long NextOffset = (long) reader.ReadUInt32(); //Get next chunk offset
			//Seek to the next offset from current position
			reader.BaseStream.Seek(NextOffset,SeekOrigin.Current);
		}
#endregion
#region Header Extraction Methods
		/*
		 * WaveFileFormat ReadMainFileHeader - 2004 July 28
		 * Read in the main file header.  Not much more to say, really.
		 * For XML serialization purposes, I "correct" the dwFileLength
		 * field to describe the whole file's length.
		 */
		public riffChunk ReadMainFileHeader()
		{
			mainfile = new riffChunk();

			mainfile.sGroupID = new string(reader.ReadChars(4));
			mainfile.dwFileLength = reader.ReadUInt32()+8;
			mainfile.sRiffType = new string(reader.ReadChars(4));
			return mainfile;
		}

		//fmtChunk ReadFormatHeader() - 2004 July 28
		//Again, not much to say.
		public fmtChunk ReadFormatHeader()
		{
			format = new fmtChunk();

			format.sChunkID = "fmt ";

            int pos = 0;
            format.dwChunkSize = reader.ReadUInt32();
            
			
            format.wFormatTag = reader.ReadUInt16();
            pos += 2;

			format.wChannels = reader.ReadUInt16();
            pos += 2;
            
			format.dwSamplesPerSec = reader.ReadUInt32();
            pos += 4;
            
            format.dwAvgBytesPerSec = reader.ReadUInt32();
            pos += 4;
            
            format.wBlockAlign = reader.ReadUInt16();            
            pos += 2;

            if (pos + 4 <= format.dwChunkSize)
            {
                format.dwBitsPerSample = reader.ReadUInt32();
                pos += 4;
            }
            else
                format.dwBitsPerSample = 8 * format.dwAvgBytesPerSec / format.dwSamplesPerSec;

            while ( pos < format.dwChunkSize )
            {
                reader.ReadByte();
                pos++;
            }

			return format;
		} 

		//factChunk ReadFactHeader() - 2004 July 28
		//Again, not much to say.
		public factChunk ReadFactHeader()
		{
			fact = new factChunk();

			fact.sChunkID = "fact";
			fact.dwChunkSize = reader.ReadUInt32();
			fact.dwNumSamples = reader.ReadUInt32();
			return fact;
		} 


		//dataChunk ReadDataHeader() - 2004 July 28
		//Again, not much to say.
		public dataChunk ReadDataHeader()
		{
			data = new dataChunk();

			data.sChunkID = "data";
			data.dwChunkSize = reader.ReadUInt32();
			data.lFilePosition = reader.BaseStream.Position;
			if (fact != null )
				data.dwNumSamples = fact.dwNumSamples;
			else
				data.dwNumSamples = data.dwChunkSize / (format.dwBitsPerSample/8 * format.wChannels);
			//The above could be written as data.dwChunkSize / format.wBlockAlign, but I want to emphasize what the frames look like.
			data.dwMinLength = (data.dwChunkSize / format.dwAvgBytesPerSec) / 60;
			data.dSecLength = ((double)data.dwChunkSize / (double)format.dwAvgBytesPerSec) - (double)data.dwMinLength*60;
			return data;
		} 
#endregion
#region IDisposable Members

		public void Dispose() 
		{
			if(reader != null) 
				reader.Close();
		}

#endregion


	}


    public class WaveFile
    {
        public riffChunk maindata;
        public fmtChunk format;
        public factChunk fact;
        public dataChunk data;

        private WaveFileReader reader;

        public BinaryReader Reader
        {
            get { return reader.reader;  }
        }

        public FileStream FileStream
        {
            get { return (FileStream)reader.reader.BaseStream;  }
        }

        public WaveFile()
        {}

        public WaveFile(string filename)
        {
            reader = new WaveFileReader(filename);
            maindata = reader.ReadMainFileHeader();
            maindata.FileName = filename;
            long chunkSize = 0;
            string chunkName = null ;
            while (chunkName != "data" && reader.GetPosition() < (long)maindata.dwFileLength && reader.GetPosition() + chunkSize < maindata.dwFileLength)
            {
                
                chunkName = reader.GetChunkName();
                switch (chunkName)
                {
                    case "fmt ":

                        format = reader.ReadFormatHeader();
                        chunkSize = format.dwChunkSize;
                        break;

                    case "fact":

                        fact = reader.ReadFactHeader();
                        chunkSize = fact.dwChunkSize;
                        break;

                    case "data":

                        data = reader.ReadDataHeader();
                        chunkSize = data.dwChunkSize;
                        break;

                    default:
                        //This provides the required skipping of unsupported chunks.
                        // reader.AdvanceToNext();

                        chunkSize = 0;
                        break;
                }
            }
        }

    }

}
