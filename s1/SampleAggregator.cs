using System;
using System.Collections.Generic;
using System.Text;

namespace s1
{
    public class SampleAggregator
    {
        private List<double> aggregatedSamples;
        private double[] result;

        public event SampleRetriever.SamplesRetrievedDelegate SamplesRetreived;
       
        public double[] Result
        {
            get
            {
                double[] res = result;
                if (res == null)
                {
                    lock (aggregatedSamples)
                    {
                        result = res = aggregatedSamples.ToArray();
                    }
                }
                return res;
            }
        }

        public SampleAggregator(SampleCollector collector)
        {
            aggregatedSamples = new List<double>();
            collector.SamplesRetreived += new SampleRetriever.SamplesRetrievedDelegate(collector_SamplesRetreived);
            collector.SamplesRetrievingDone += new EventHandler(collector_SamplesRetrievingDone);
        }

        public int Count
        {
            get { return aggregatedSamples.Count;  }
        }

        public void Clear()
        {
            aggregatedSamples = new List<double>();
            result = null;
        }

        void collector_SamplesRetreived(object sender, double[] samples, long ignored_position)
        {
            double v;
            lock (aggregatedSamples)
            {
                result = null; // invalidate cache

                double sum = 0.0;
                foreach (double d in samples)
                    sum += Math.Abs(d);
                v = sum / samples.LongLength;
                aggregatedSamples.Add(v);
            }

            if ( SamplesRetreived != null )
            {
                SamplesRetreived(sender, new double[] { v }, Count);
            }
        }

        void collector_SamplesRetrievingDone(object sender, EventArgs e)
        {
            if (SamplesRetreived != null)
                SamplesRetreived(sender, null, Count);
        }

    }
}
