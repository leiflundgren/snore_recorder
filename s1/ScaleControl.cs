using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace s1
{
    public partial class ScaleControl : UserControl
    {
        public ScaleControl()
        {
            InitializeComponent();
        }

        private void ScaleControl_Load(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
        }

        private long begin = 0, end = 100;

        public long End
        {
            get { return end; }
            set { end = value;
                UpdateImage();
            }
        }

        public long Begin
        {
            get { return begin; }
            set { 
                begin = value;
                UpdateImage();
            }
        }

        private void ScaleControl_SizeChanged(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (begin >= end)
                return; // invalid value
            if ( Width <= 0 || Height <= 0 )
                return; // invalid value

            Bitmap img = new Bitmap(Width, Height);
            using ( Graphics g = Graphics.FromImage(img))
            {
                DrawScale(g, Height, Width, ForeColor);
                g.Dispose();
            }

            pictureBox1.Image = img;
        }

        private void DrawScale(Graphics g, int height, int width, Color c)
        {
            int textheight = 0;
            Pen p = new Pen(c);
            int middle = (height-textheight)/2;

            // x-axis line.
            g.DrawLine(p, 0, middle, width, middle);

            int marker_y1 = textheight;
            int marker_y2 = height;

            for ( int i=0; i<10; ++i )
            {

            }
        }
    }
}
