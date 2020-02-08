using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ILNumerics;

namespace s1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Wav files (*.wav)|*.wav|All files (*.*)|*.*";
            fd.CheckFileExists = true;
            fd.Title = "Select wav-file";

            DialogResult res = fd.ShowDialog(this);
            if (res != DialogResult.OK)
                return;

            UpdateSampleView(fd.FileName);
        }


        private void btnLoadDefault_Click(object sender, EventArgs e)
        {
            //LoadFile(@"C:\Program Files\Netwise\QueueManager\VoicePrompts\Welcome.wav");
            //UpdateSampleView(@"c:\Temp\download\440Hz_44100Hz_16bit_30sec.wav");
            UpdateSampleView(@"G:\sova101227.wav");
        }

        private string filename;
        private TimeSpan chunkTimeLength = TimeSpan.FromSeconds(5);
 
        private void LoadFile(string filename)
        {
            WaveFile wav = new WaveFile(filename);

            int chunkSize = (int)(chunkTimeLength.TotalSeconds * wav.SamplesPerSecond);

            SampleRetriever retr = new SampleRetriever(new WavefileSampleSource(wav), chunkSize);
            retr.SamplesRetrievingDone += new EventHandler(retr_SamplesRetrievingDone);

            long fromSample, toSample;
            if (numFromSample.Value < 0)
                fromSample = 0;
            else
                fromSample = (long)numFromSample.Value * retr.SampleSource.SampleCount / retr.SampleSource.SamplesPerSecond;
            if(numToSample.Value < 0 )
                toSample = -1;
            else
                toSample = (long)numToSample.Value * retr.SampleSource.SampleCount / retr.SampleSource.SamplesPerSecond;

            sampleDisplayer1.Fill(retr, fromSample, toSample);
        }

       

//         private delegate void SetVisibleFalseDelegate(Control ctrl);
//         private void SetVisibleFalse(Control ctrl)
//         {
//             ctrl.Visible = false;
//         }

        void retr_SamplesRetrievingDone(object sender, EventArgs e)
        {
            btnStop.BeginInvoke(new Action<Control>(delegate(Control ctrl) { ctrl.Visible = false; }), btnStop);
            btnLoad.BeginInvoke(new Action<Control>(delegate(Control ctrl) { ctrl.Enabled = true; }), btnLoad);
            btnLoadDefault.BeginInvoke(new Action<Control>(delegate(Control ctrl) { ctrl.Enabled = true; }), btnLoadDefault);

            // btnStop.BeginInvoke(new SetVisibleFalseDelegate(SetVisibleFalse), btnStop);
        }

        private  void UpdateSampleView(string filename)
        {
            this.filename = filename;
            UpdateSampleView();
        }

        private  void UpdateSampleView()
        {
            if (btnStop.Visible)
                sampleDisplayer1.Stop();                

            btnLoadDefault.Enabled = false;
            btnLoad.Enabled = false;
            btnStop.Visible = true;
            LoadFile(filename);
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            sampleDisplayer1.Dock = DockStyle.Fill;
            btnStop.Visible = false;
            btnLoadDefault_Click(sender, e);
        }

        private void numFromSample_ValueChanged(object sender, EventArgs e)
        {
            //UpdateSampleView();
        }

        private void numToSample_ValueChanged(object sender, EventArgs e)
        {
            //UpdateSampleView();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            sampleDisplayer1.Stop();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            sampleDisplayer1.Stop();
        }




    }
}
