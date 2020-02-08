using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace s1
{
    public partial class SampleGraphDisplay : UserControl
    {
        public SampleGraphDisplay()
        {
            InitializeComponent();
        }

        private void pictureGraph_Click(object sender, EventArgs e)
        {

        }


        public double[] Samples
        {
            get { return samples; }
            set { 
                samples = value;
                if (initiated_done)
                    UpdateBitmap();
            }
        }

        public int Count
        {
            get { return samples.Length; }
            set 
            {
                if (value != samples.Length)
                {
                    double[] old = samples;
                    double[] ny = new double[value];
                    Array.Copy(old, ny, Math.Min(old.Length, ny.Length));
                    Samples = ny;
                }
            }
        }

        public int Position
        {
            get { return position; }
            set { position = value; }
        }
        
        Image bufferGraphImage;
        private double[] samples = new double[0];
        private int position = 0;

        private Color datalessColor = Color.BlanchedAlmond;

        /// <summary>
        /// The color to use when there is no data...
        /// </summary>
        public Color DatalessColor
        {
            get { return datalessColor; }
            set { datalessColor = value; }
        }
             

        private double[] valuesPerPixel = new double[0];

        /// <summary>
        /// Sets the internal array of samples
        /// </summary>
        /// <param name="targetPos">index into internal 'samples' buffer to write to. 
        /// Might be outside current range of samples. 
        /// Use -1 to add to "last" relative previous SetValues operation</param>
        /// <param name="new_samples">data to read from</param>
        /// <param name="pos">index into new_samples</param>
        /// <param name="length">length of new_samples to use</param>
        public void SetValues(int targetPos, double[] new_samples, int pos, int length)
        {
            if (targetPos < 0)
                targetPos = Position;


            if (targetPos + length > samples.Length)
                this.Count = targetPos + length;

            Array.Copy(new_samples, pos, samples, targetPos, length);
            if (targetPos >= position)
                position += length;
            UpdateBitmap();


        }

        public void UpdateBitmap()
        {
            if (Position < 0 || Count < 0)
                return;
            Bitmap img = new Bitmap(bufferGraphImage);
            if (Count >= bufferGraphImage.Width)
                UpdateBitmapMoreSamplesThenPixels(img, Samples, Position, ForeColor, BackColor, DatalessColor );
            else
                UpdateBitmapLessSamplesThenPixels(img, Samples, Position, ForeColor, BackColor, DatalessColor);

            UpdateImage(pictureGraph, img);
        }

        private static void UpdateBitmapLessSamplesThenPixels(Image img, double[] samples, int sample_max, Color ForeColor, Color BackColor, Color DatalessColor)
        {           

            using (Graphics g = Graphics.FromImage(img))
            {
                g.FillRectangle(new SolidBrush(BackColor), 0, 0, img.Width, img.Height);

                if (samples.Length > 0 && sample_max > 0)
                {
                    double pixelsPerSample = img.Width / (double)sample_max;
                    int x_max = img.Width * sample_max / samples.Length;
                    double end = (double)Math.Min(img.Width, x_max);

                    if (x_max < img.Width)
                        g.FillRectangle(new SolidBrush(DatalessColor), x_max, 0, img.Width - x_max, img.Height);

                    if (end > 0)
                    {
                        Pen p = new Pen(ForeColor);

                        // first pos
                        int x_last = -1;
                        int y_last = (int)(img.Height * (1 - samples[0]));
                        int pos = 0;

                        for (double xd = pixelsPerSample; xd < end; xd += pixelsPerSample, ++pos)
                        {
                            int x = (int)xd;
                            int y = (int)(img.Height * (1 - samples[pos]));

                            // draw a 4-point poly
                            Point[] poly = new Point[] {
                            new Point(x_last, img.Height), // down left
                            new Point(x_last, y_last), // up left
                            new Point(x, y), // up right
                            new Point(x, img.Height), // down right
                        };

                            g.DrawPolygon(p, poly);
                        }
                    }
                }
            }
        }


        private static void UpdateBitmapMoreSamplesThenPixels(Bitmap img, double[] samples, int sample_max, Color foreColor, Color backColor, Color datalessColor)
        {
            int x_max = img.Width*sample_max / samples.Length;

            using ( Graphics g = Graphics.FromImage(img))
            {
                int end = Math.Min(img.Width, x_max);
                g.FillRectangle(new SolidBrush(backColor), 0, 0, img.Width, img.Height);


                if ( x_max < img.Width )
                    g.FillRectangle(new SolidBrush(datalessColor), x_max, 0, img.Width-x_max, img.Height);

                if (sample_max > 0)
                {
                    double[] aggregated = AggregateSamples(samples, img.Width, sample_max);

                    Pen p = new Pen(foreColor);
                    for (int x = 0; x < end; ++x)
                    {
                        int y = (int)(img.Height * (1 - aggregated[x]));
                        g.DrawLine(p, x, img.Height, x, y);
                    }
                }
            }
        }

        /// <summary>
        /// Takes a number of samples, aggregates the samples to 1 or more in_sample per out_sample.
        /// If there should be 1,5 in_samples per out_sample, it will vary between 1 and 2 samples...
        /// </summary>
        /// <param name="in_samples"></param>
        /// <param name="aggregateTargetSize"></param>
        /// <returns></returns>
        private static double[] AggregateSamples(double[] in_samples, int aggregateTargetSize, int onlyDoFirstNSamples)
        {
            double[] outSamples = new double[aggregateTargetSize];
            int len = aggregateTargetSize;
            double inPerOutRatio = in_samples.Length / (double)aggregateTargetSize;

            onlyDoFirstNSamples = Math.Min(onlyDoFirstNSamples, aggregateTargetSize);

            for ( int outPos = 0, inPos=0; outPos < onlyDoFirstNSamples && inPos < in_samples.Length; ++outPos)
            {
                int outLeft = outSamples.Length-outPos;
                int inLeft = in_samples.Length-inPos;
                int samplesInThisChunk = inLeft / outLeft;
                System.Diagnostics.Debug.Assert(samplesInThisChunk > 0);

                double sum = 0.0;
                for (int aggLen = 0; aggLen < samplesInThisChunk; ++aggLen, ++inPos)
                {
                    sum += in_samples[inPos];
                }
                outSamples[outPos] = sum/samplesInThisChunk;                
            }

            return outSamples;
        }


        public void RescalePixelValues(double scale)
        {
            for (int i = 0; i < samples.Length; ++i)
                samples[i] *= scale ;
            UpdateBitmap();
        }

        private void SampleGraphDisplay_Resize(object sender, EventArgs e)
        {
            if (initiated_done)
            {
                bufferGraphImage = new Bitmap(pictureGraph.Width, pictureGraph.Height);
                UpdateBitmap();
            }
        }

        public static void UpdateImage(PictureBox pic, Image img)
        {
            if (!pic.InvokeRequired)
            {
                lock (img)
                {
                    pic.Image = new Bitmap(img);
                }
            }
            else
            {
                pic.BeginInvoke(
                    new Action<Image>(delegate(Image img2) { UpdateImage(pic,img2); }),
                    img);
            }
        }

        private bool initiated_done = false;
        private void SampleGraphDisplay_Load(object sender, EventArgs e)
        {
            initiated_done = true;
            SampleGraphDisplay_Resize(this, new EventArgs());
        }

    }
}
