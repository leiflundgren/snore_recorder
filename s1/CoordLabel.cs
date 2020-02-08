using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace s1
{
    public partial class CoordLabel : UserControl
    {
        public CoordLabel()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.lblCoordinats.Visible = false;
            }
        }

        private Control backgroundControl;

        public Control BackgroundControl
        {
            get { return backgroundControl; }
            set
            {
                ListenersRecursive(backgroundControl, false);

                backgroundControl = value;

                ListenersRecursive(backgroundControl, true);
            }
        }

        private void ListenersRecursive(Control ctrl, bool addListeners)
        {
            if (ctrl != null)
            {
                if (addListeners)
                {
                    ctrl.MouseMove += new MouseEventHandler(OnMouseMove);
                    ctrl.MouseEnter += new EventHandler(OnMouseEnter);
                    ctrl.MouseLeave += new EventHandler(OnMouseLeave);
                }
                else
                {
                    ctrl.MouseMove -= new MouseEventHandler(OnMouseMove);
                    ctrl.MouseEnter -= new EventHandler(OnMouseEnter);
                    ctrl.MouseLeave -= new EventHandler(OnMouseLeave);
                }

                foreach (Control child in ctrl.Controls)
                    ListenersRecursive(child, addListeners);
            }
        }

        public class CoordinatesChangedEventArgs : EventArgs
        {
            public Point Location;
            public String Text;
            public Size MaxValue;
        }

        public delegate void CoordinatesChangedDelegate(CoordinatesChangedEventArgs args);

        public event CoordinatesChangedDelegate CoordinatesChangedEvent;

        private Cursor oldCursor = null;

        public void OnMouseEnter(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            oldCursor = c.Cursor;
            c.Cursor = Cursors.Cross;
            System.Diagnostics.Debug.WriteLine("Entered");
        }

        public void OnMouseLeave(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            if ( oldCursor != null )
            {
                c.Cursor = oldCursor;
            }
            lblCoordinats.Visible = false;
            System.Diagnostics.Debug.WriteLine("Left");
        }


        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            string lbl = e.X.ToString() + "," + e.Y.ToString();
            if ( CoordinatesChangedEvent != null ) {
                CoordinatesChangedEventArgs args = new CoordinatesChangedEventArgs();
                args.Location = e.Location;
                args.Text = lbl;
                args.MaxValue = backgroundControl.Size;
                CoordinatesChangedEvent(args);
                lbl = args.Text;
            }

            lblCoordinats.Text = lbl;
            this.Location =  this.Parent.PointToClient( Control.MousePosition );

            if (!lblCoordinats.Visible)
            {
                lblCoordinats.Visible = true;
                
                //lblCoordinats.BringToFront();
                System.Diagnostics.Debug.WriteLine("Pos " + lblCoordinats.Text + " made visible");
                //lblCoordinats.Show();
            }
            else
                System.Diagnostics.Debug.WriteLine("Pos " + lblCoordinats.Text + " already visible");

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (backgroundControl != null)
            {
                Bitmap imp = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height);
                backgroundControl.DrawToBitmap(imp, e.ClipRectangle);

                e.Graphics.DrawImage(imp, e.ClipRectangle.Location);
                //base.OnPaintBackground(e);
            }
        }

        private void lblCoordinats_Resize(object sender, EventArgs e)
        {
            this.Size = lblCoordinats.Size;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT

                return cp;
            }
        }
    }
}
