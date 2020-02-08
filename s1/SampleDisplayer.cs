using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ILNumerics;

namespace s1
{
    public partial class SampleDisplayer : UserControl
    {
        public SampleDisplayer()
        {
            InitializeComponent();
        }

        private SampleRetriever sampleRetriever;
        private SampleCollector sampleCollector;
        private SampleAggregator aggregator;

        private double frequenzyCutoffMax = 10000.0;
        private double frequenzyCutoffMin = 30.0;

        private double fftAmplitudeMin = 0.3;
        private double timeslotLength;

        private long samplePos;
        private double scale = 1.0;

        private System.Threading.Thread fftThread = null;
        private bool fftThreadTerminate = false;

        private class FFTJob
        {
            public FFTJob() {}
            public FFTJob(double[] data, double y_limit, int timeSlotNumber, int sampleRate) {
                this.data = data;
                this.y_limit = y_limit;
                this.timeSlotNumber = timeSlotNumber;
                this.sampleRate = sampleRate;
            }
            public double[] data;
            public double y_limit;
            public int timeSlotNumber;
            public int sampleRate;
        }
        private SyncronizedQueue<FFTJob> fftJobs = new SyncronizedQueue<FFTJob>();


        /// <summary>
        /// Minimum amplitude to be included in FFT output
        /// </summary>
        /// <value>[0.0-1.0]</value>
        public double FFTAmplitudeMin
        {
            get { return fftAmplitudeMin; }
            set { fftAmplitudeMin = Math.Max(0.0, Math.Min(1.0, value)); }
        }

        public double ScaleFactor
        {
            get { return scale; }
        }


        private int samplesPerSecond
        {
            get { return sampleRetriever != null ? sampleRetriever.SampleSource.SamplesPerSecond : 0; }
        }

        public double TimeslotLength
        {
            get { return timeslotLength; }
            set { timeslotLength = value; }
        }

        public double FrequenzyCutoffMax
        {
            get { return frequenzyCutoffMax; }
            set { frequenzyCutoffMax = value; }
        }
        public double FrequenzyCutoffMin
        {
            get { return frequenzyCutoffMin; }
            set { frequenzyCutoffMin = value; }
        }

        /// <summary>
        /// Sets sample
        /// </summary>
        /// <param name="samples"></param>
        /// <param name="samplesPerSecond"></param>
        /// <param name="beginSample">
        /// First sample to be shown. Default 0
        /// </param>
        /// <param name="endSample">
        /// Last+1 sample to be shown. 
        /// -1 to be until last sample, 
        /// default -1
        /// (If set to 10, samples up to 9 will be considered. 
        ///  Concept stolen from C++ stl. )
        /// </param>
        public void Fill(SampleRetriever sampleRetriever, long startPos, long endPos)
        {
            
            pictureGraph.DatalessColor = Color.PowderBlue;
            pictureFFT.Image = new Bitmap(pictureFFT.Width, pictureFFT.Height);
            using ( Graphics g = Graphics.FromImage(pictureFFT.Image))
            {
                g.FillRectangle(new SolidBrush(pictureGraph.DatalessColor), 0, 0, pictureFFT.Width, pictureFFT.Height);
            }

            // bufferedFFTImage =  SetToColor(pictureFFT, Color.PowderBlue);

            
            this.sampleRetriever = sampleRetriever;
            this.samplePos = 0;
            this.sampleRetriever.BeginSample = startPos;
            this.sampleRetriever.EndSample = endPos;
            this.maxSampleValue = 0.0;

            this.sampleCollector = new SampleCollector(this.sampleRetriever);

            this.aggregator = new SampleAggregator(this.sampleCollector);

            pictureGraph.Count = (int) this.sampleCollector.ExpectedCount;
            this.aggregator.SamplesRetreived += new SampleRetriever.SamplesRetrievedDelegate(samplesAggregated);
            this.sampleCollector.SamplesRetreived += new SampleRetriever.SamplesRetrievedDelegate(samplesCollector_Retrieved);
            this.sampleCollector.SamplesRetrievingDone += new EventHandler(sampleCollector_SamplesRetrievingDone);

            
            this.sampleRetriever.Start();
        }



        // Stops currently active draw-job
        public void Stop()
        {
            if (this.sampleRetriever == null)
                return;

            this.sampleRetriever.PleaseTerminate=true;
            this.sampleRetriever.WaitFor();
        }


        private static Image SetToColor(PictureBox pic, Color c)
        {
            Image img = new Bitmap(pic.Width, pic.Height);
            using ( Graphics g = Graphics.FromImage(img) )
            {
                Brush b = new SolidBrush(c);
                g.FillRectangle( b, 0, 0, img.Width, img.Height  );
            }
            pic.Image = img;

            return new Bitmap(img);
        }

        private SampleBuilder graphSampleBuilder = new SampleBuilder();
        private double maxSampleValue = 0.0;

        private void  samplesAggregated(object sender, double[] samples, long position)
        {
            if (samples != null)
            {
                {
                    for ( int i=0; i<samples.Length; ++i )
                        samples[i] = Math.Abs(samples[i]);
                }

                graphSampleBuilder.Add(samples);

                if ( graphSampleBuilder.Count >= 20 )
                {
                    double[] s2 = graphSampleBuilder.Pop();
                    pictureGraph.SetValues(-1, s2, 0, s2.Length);
                    for (int i = 0; i < samples.Length; ++i)
                        maxSampleValue = Math.Max(maxSampleValue, samples[i]);
                }

                //load_fft(sampleRetriever, samples, samplePos);
                samplePos += samples.Length;
            }
            else
            {
                // done;

                double[] s2 = graphSampleBuilder.Pop();
                pictureGraph.SetValues(-1, s2, 0, s2.Length);

                // scale drawn image
                pictureGraph.RescalePixelValues( 1/maxSampleValue);
            }
        }

        private void samplesCollector_Retrieved(object sender, double[] samples, long position)
        {
            load_fft(sampleRetriever, samples, position);
        }
        void sampleCollector_SamplesRetrievingDone(object sender, EventArgs e)
        {
            this.fftThreadTerminate = true;
        }

        void graphCollector_SamplesRetreived(object sender, double[] samples, long position)
        {
            throw new NotImplementedException();
        }

        private delegate void UpdateImageDelegate(PictureBox pic,  Image img);

        private void UpdateImage(PictureBox pic,  Image img)
        {
            lock (img)
            {
                pic.Image = new Bitmap(img);
            }
        }

        private int ImageWidth
        {
            get { return pictureGraph.Width; }
        }
        
        private double SampleToTime(long s)
        {
            return ((double)s) / (double)samplesPerSecond;
        }
        private double SampleToTime(double s)
        {
            return s / (double)samplesPerSecond;
        }

        private long PixelToSampleN(int x)
        {
            return x* sampleRetriever.SampleSource.SampleCount/ ImageWidth;
        }
        
        private int SampleToPixel(long sample)
        {
            return (int)(sample * ImageWidth  / sampleRetriever.SampleSource.SampleCount);
        }


        private static double AbsMax(ICollection<double> data)
        {
            double tmax = 0.0;
            foreach (double t in data)
            {
                tmax = Math.Max(tmax, Math.Abs(t));
            }
            return tmax;
        }


        private SampleBuilder fftBuilder = new SampleBuilder();
        private int splitIntoNumberOfParts = 100;
        
    
        private void load_fft(SampleRetriever sender, double[] samples, long samplePos)
        {
            while (fftJobs.Count > 5 && !fftThreadTerminate)
                System.Threading.Thread.Sleep(500);
            if (fftThreadTerminate)
                return;
            
            fftBuilder.Add(samples);

            long limit = sender.SampleSource.SampleCount / splitIntoNumberOfParts;
            if (fftBuilder.Count < limit)
                return;

            int timeslotNumber = (int)((samplePos+samples.Length) / limit);

            double[] data = fftBuilder.ToArray();
            fftBuilder.Clear();

            double y_limit = FFTAmplitudeMin * sampleRetriever.SampleSource.MaxSampleValue;
            int sampleRate = sampleRetriever.SampleSource.SamplesPerSecond;

            fftJobs.Push(new FFTJob(data, y_limit, timeslotNumber, sampleRate));

            
            if (fftThread == null)
            {
                fftThread = new System.Threading.Thread(new System.Threading.ThreadStart(FFTTheadMain));
                fftThread.Start();
            }
            
            //System.Threading.ThreadPool.QueueUserWorkItem( new System.Threading.WaitCallback(PerformFFT), new Object[] { data, y_limit, timeslotNumber, sampleRate } );
        }

        private void FFTTheadMain()
        {
            while ( !fftThreadTerminate )
            {
                FFTJob job = fftJobs.Pop();
                if ( job != null )
                    PerformFFT(job.data, job.y_limit, job.timeSlotNumber, job.sampleRate);
                else
	                System.Threading.Thread.Sleep(500);
            }            
        }

        private void PerformFFT(object args)
        {
            object[] arr = (object[])args;
            PerformFFT( (double[]) arr[0], (double)arr[1], (int)arr[2], (int)arr[3]);
        }


        private void PerformFFT(double[] data, double y_limit, int timeSlotNumber, int sampleRate)
        {
            double[] x_freq;
            double[] y_coof;

            PerformFFT(data, y_limit, sampleRate, frequenzyCutoffMin, frequenzyCutoffMax, out x_freq, out y_coof);

            DrawFFTTimeSlot(timeSlotNumber, splitIntoNumberOfParts, x_freq, y_coof);

        }
        
        private static void PerformFFT(double[] input_data, double y_limit, int sampleRate, double frequenzyCutoffMin, double frequenzyCutoffMax, out double[] x_freq, out double[] y_coof)
        {
            ILArray<double> arr_in;
            ILArray<complex> arr_res ;
            try
            {
                arr_in = new ILArray<double>(input_data);

                arr_res = ILNumerics.BuiltInFunctions.ILMath.fft(arr_in);
            }
            catch ( Exception ex ) {
                MessageBox.Show("Error performing FFT-operation: " + ex);
                throw;
            }

            double fftFreqMax = arr_res.GetValue(arr_res.Length - 1).imag;
            double maxCutofFreq = (frequenzyCutoffMax * fftFreqMax) / sampleRate;
            double minCutofFreq = (frequenzyCutoffMin * fftFreqMax) / sampleRate;

            List<double> x_freq_ls = new List<double>();
            List<double> y_coof_ls = new List<double>();
            for (int i = 0; i < arr_res.Length; ++i)
            {
                complex c = arr_res.GetValue(i);

                double real = c.real;
                double imag = c.imag;

                if (Math.Abs( real ) < y_limit)
                    continue;

                double abs_freq = Math.Abs(imag);

                if (abs_freq < minCutofFreq)
                    continue;
                if (abs_freq > maxCutofFreq)
                    continue;

                double freq = abs_freq / maxCutofFreq; // freq will now be 0-1
                System.Diagnostics.Debug.Assert(freq <= 1.0);
                x_freq_ls.Add(abs_freq); 
                y_coof_ls.Add(Math.Abs(real)/255.0);
            }
            
            x_freq = x_freq_ls.ToArray();
            y_coof = y_coof_ls.ToArray();

            arr_in = null;
            arr_res = null;
        }

        private Object drawFFTLock = new Object();
        private void DrawFFTTimeSlot(int nSlot, int numberOfSlots, double[] x_freq, double[] y_coof)
        {
            if ( x_freq.LongLength != y_coof.LongLength )
                throw new ArgumentException("x- and y-arrays must be same length!");

            lock (drawFFTLock)
            {

                Bitmap bmp = pictureFFT.Image != null ? new Bitmap(pictureFFT.Image) : new Bitmap(pictureFFT.Width, pictureFFT.Height);
                int slot_pixel_len = bmp.Width / numberOfSlots;

                int x_start = slot_pixel_len * nSlot;
                int x_end = x_start + slot_pixel_len;

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(new SolidBrush(Color.White), x_start, 0, x_end, bmp.Height);

                    for (long i = 0; i < x_freq.LongLength; ++i)
                    {
                        // the bigger coof, the blacker...
                        Color c = ColorManip.Darken(Color.White, Math.Abs(y_coof[i]));

                        int y = (int)(bmp.Height * (1 - x_freq[i]));

                        g.DrawLine(new Pen(c), x_start, y, x_end, y);

                        //int t_ms = samples.Length * nSlot * 1000 / (samplesPerSecond * numberOfSlots);
                        //System.Diagnostics.Debug.WriteLine("t=" + t_ms + "ms  f=" + x_freq[i] + " coof=" + y_coof[i]);
                    }
                }

                pictureFFT.Image = bmp;
            }
        }



        private void SampleDisplayer_Load(object sender, EventArgs e)
        {
            pictureGraph.BackColor = Color.White;
            pictureFFT.BackColor = this.BackColor;

                pictureGraph.Dock = DockStyle.Fill;
            pictureFFT.Dock = DockStyle.Fill;
            
            coordLabel1.BackgroundControl = pictureGraph;
            coordLabel1.CoordinatesChangedEvent += new CoordLabel.CoordinatesChangedDelegate(coordLabel1_CoordinatesChangedEvent);

            coordLabel2.BackgroundControl = pictureFFT;
            coordLabel2.CoordinatesChangedEvent += new CoordLabel.CoordinatesChangedDelegate(coordLabel2_CoordinatesChangedEvent);

            splitIntoNumberOfParts = pictureFFT.Width;
        }

        void coordLabel2_CoordinatesChangedEvent(CoordLabel.CoordinatesChangedEventArgs args)
        {
            //throw new NotImplementedException();
        }

        void coordLabel1_CoordinatesChangedEvent(CoordLabel.CoordinatesChangedEventArgs args)
        {
            double relX = (double)args.Location.X / (double)args.MaxValue.Width;
            double relY =  1.0 - (double)args.Location.Y / (double)args.MaxValue.Height;

            TimeSpan t = TimeSpan.FromSeconds(  SampleToTime(PixelToSampleN(args.Location.X)) );

            String st = "";
            bool started = false;
            if (t.Hours > 0)
            {
                st += t.Hours + "h ";
                started = true;
            }
            if ( started || t.Minutes > 0 )
            {
                st += t.Minutes + "m ";
                started = true;
            }

            st += t.Seconds + "s";

            double v = relY / ScaleFactor;

            args.Text = String.Format("t={0} v={1:0.000}", st, v);
        }

    }
}
