using System;
using System.Collections.Generic;
using System.Text;

namespace s1
{
    public interface ISampleSource
    {
        /// <summary>
        /// Maximum possible value for data from this source
        /// </summary>
        double MaxSampleValue { get; }
        /// <summary>
        /// Minimum possible value for data from this source
        /// </summary>
        double MinSampleValue { get; }

        int SamplesPerSecond { get; }

        /// <summary>
        /// Number of samples in source
        /// </summary>
        long SampleCount { get; }

        /// <summary>
        /// Position, measured in number of samples.
        /// </summary>
        long Position { get; }

        /// <summary>
        /// Sets the position in the wave-file
        /// </summary>
        /// <param name="pos">number of samples from beginning</param>
        /// <exception cref="ArgumentOutOfRangeException">If pos is bigger then SampleCount</exception>
        void SetPosition(long pos);



        /// <summary>
        /// Read a chunk of data
        /// </summary>
        /// <param name="size">Number of samples</param>
        /// <returns>data read, null if EOF</returns>
        double[]  GetChunk(int size);
    }
}
