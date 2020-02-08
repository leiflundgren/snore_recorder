using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace s1
{
    /// <summary>
    /// Class that reads samples from file in a background thread.
    /// Samples are read from the file, queued for a consumer thread.
    /// There is protection to avoid the queue from being over-filled, 
    /// rather just a few sample-chunks are available.
    /// When EOF is reached, will end with one empty-chunk (0 samples) and terminate.
    /// </summary>
    ///<remarks>Ugly implementation, using sleep rather then events or similar to wait
    ///on conditions. Should be re-written...</remarks>
    public class SampleRetriever
    {
        private ISampleSource sampleSource;
        private bool pleaseTerminate;
        private int chunkSize;
        private long beginSample, endSample;

        public long EndSample
        {
            get { return endSample >= 0 ? endSample : sampleSource.SampleCount; }
            set { endSample = value; }
        }

        public long BeginSample
        {
            get { return beginSample; }
            set { beginSample = value; }
        }

        private Thread thread;

        public int ChunkSize
        {
            get { return chunkSize; }
            set { chunkSize = value; }
        }

        public bool PleaseTerminate
        {
            get { return pleaseTerminate; }
            set { pleaseTerminate = value; }
        }

        public ISampleSource SampleSource
        {
            get { return sampleSource; }
        }

        public SampleRetriever(ISampleSource sampleSource, int chunkSize)
        {
            this.sampleSource = sampleSource;
            this.thread = null;
            this.beginSample = 0;
            this.endSample = -1;
            this.chunkSize = chunkSize;
        }


        public void Start()
        {
            if ( thread == null )
                thread = new System.Threading.Thread(new System.Threading.ThreadStart(Work));            
            if ( !thread.IsAlive )
                thread.Start();
            
        }

        /// <summary>
        /// The samples retrieved.
        /// </summary>
        /// <param name="samples">Samples, null if EOF reached</param>
        /// <param name="position">How far into the source-data the samples are</param>
        public delegate void SamplesRetrievedDelegate(object sender, double[] samples, long position);
        
        public event SamplesRetrievedDelegate SamplesRetrieved;
        public event EventHandler SamplesRetrievingDone;

        private void Work()
        {
            sampleSource.SetPosition(BeginSample);
            long pos = beginSample;
            while ( !pleaseTerminate && (endSample < 0 || pos < endSample ) )
            {
                double[] chunk = sampleSource.GetChunk(chunkSize);
                if (chunk.LongLength == 0)
                    chunk = null;

                if ( SamplesRetrieved != null )
                {
                    SamplesRetrieved(this, chunk, pos);
                }

                if ( chunk == null )
                    break;

                pos += chunk.LongLength;
            }

            if (SamplesRetrievingDone != null)
                SamplesRetrievingDone(this, new EventArgs());
        }

        internal void WaitFor()
        {
            if (this.thread == null || !this.thread.IsAlive)
                return;

            this.thread.Join();
        }
    }
}
