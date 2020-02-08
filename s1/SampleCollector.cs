using System;
using System.Collections.Generic;
using System.Text;

namespace s1
{
    /// <summary>
    /// Collects samples from a SampleRetriever, aggregates a number of samples and stores the aggregation
    /// </summary>
    public class SampleCollector
    {
        private SampleBuilder sampleBuilder;
        private long sampleSize;
        private long currentPosition = 0;

        private SampleRetriever retriever;

        public event SampleRetriever.SamplesRetrievedDelegate SamplesRetreived;
        public event EventHandler SamplesRetrievingDone;


        public long SampleSize
        {
            get { return sampleSize; }
            set { sampleSize = value; }
        }

        public TimeSpan SampleLength
        {
            get { return TimeSpan.FromSeconds((double)sampleSize / (double)retriever.SampleSource.SamplesPerSecond);  }
            set { SampleSize = (long) Math.Ceiling(value.TotalSeconds * retriever.SampleSource.SamplesPerSecond); }
        }


        public SampleCollector(SampleRetriever retriever)
        {
            this.retriever = retriever;
            this.sampleSize = 5 * retriever.SampleSource.SamplesPerSecond;
            this.retriever.SamplesRetrieved += new SampleRetriever.SamplesRetrievedDelegate(retriever_SamplesRetrieved);
            this.retriever.SamplesRetrievingDone += new EventHandler(retriever_SamplesRetrievingDone);

            this.sampleBuilder = new SampleBuilder();
        }

        /// <summary>
        /// Expected number of samples to be received from retriever
        /// </summary>
        public long ExpectedCount
        {
            get {
                long expectedSamples = retriever.EndSample - retriever.BeginSample;
                long res = expectedSamples / sampleSize;
                if ((expectedSamples % sampleSize) > 0)
                    res++;
                return res;
            }
        }

        void retriever_SamplesRetrieved(object sender, double[] samples, long position)
        {
            AddSamples(samples);
        }

        public void AddSamples(double[] samples)
        {
            if ( samples != null )
                sampleBuilder.Add(samples);
    
            if ( sampleBuilder.Count >= sampleSize || samples != null && sampleBuilder.Count > 0 )
            {
                FireSamplesRetrieved(sampleSize);
            }
        }

        void retriever_SamplesRetrievingDone(object sender, EventArgs e)
        {
            if (sampleBuilder.Count > 0)
                // Fire away the "last" package
                FireSamplesRetrieved(sampleBuilder.Count);

            if (SamplesRetrievingDone != null)
                SamplesRetrievingDone(sender, e);
        }


        /// <summary>
        /// Sends an event that some samples are recieved. 
        /// </summary>
        /// <param name="sampleSize">Number of samples to fire/consume</param>
        private void FireSamplesRetrieved(long sampleSize)
        {
            double[] samples = sampleBuilder.Pop(sampleSize);
            if (SamplesRetreived != null)
                SamplesRetreived(retriever, samples, currentPosition);
            currentPosition += sampleSize;
        }

    }
}
