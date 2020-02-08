using System;
using System.Collections.Generic;
using System.Text;

namespace s1
{
    public class WavefileSampleSource : ISampleSource
    {
        WaveFile waveSource;

        public WavefileSampleSource(WaveFile waveSource)
        {
            this.waveSource = waveSource;
        }


        #region ISampleSource Members

        public double MaxSampleValue
        {
            get { return 1.0; }
        }

        public double MinSampleValue
        {
            get { return -1.0; }
        }

        public int SamplesPerSecond
        {
            get { return (int) waveSource.SamplesPerSecond; }
        }

        public long SampleCount 
        {
            get { return waveSource.Length;  }
        }

        public double[] GetChunk(int size)
        {
            byte[] buf = new byte[size*waveSource.BytesPerSample];
            int read = waveSource.ReadSamples(buf, 0, size);
            if (read < 0)
                return null;

            double[] res = new double[read];
            waveSource.TranslateAudio(res, 0, buf, 0, read);
            return res;
        }

        /// <summary>
        /// Position, measured in number of samples.
        /// </summary>
        public long Position
        {
            get
            {
                return waveSource.Position;
            }
        }

        public void SetPosition(long pos)
        {
            waveSource.Position = waveSource.SamplesToBytes(pos);
        }

        #endregion
    }
}
