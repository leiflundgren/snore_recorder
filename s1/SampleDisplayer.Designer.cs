namespace s1
{
    partial class SampleDisplayer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureFFT = new System.Windows.Forms.PictureBox();
            this.pictureGraph = new s1.SampleGraphDisplay();
            this.coordLabel1 = new s1.CoordLabel();
            this.coordLabel2 = new s1.CoordLabel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFFT)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureGraph);
            this.splitContainer1.Panel1.Controls.Add(this.coordLabel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.coordLabel2);
            this.splitContainer1.Panel2.Controls.Add(this.pictureFFT);
            this.splitContainer1.Size = new System.Drawing.Size(534, 284);
            this.splitContainer1.SplitterDistance = 170;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureFFT
            // 
            this.pictureFFT.BackColor = System.Drawing.Color.GreenYellow;
            this.pictureFFT.Location = new System.Drawing.Point(16, 16);
            this.pictureFFT.Name = "pictureFFT";
            this.pictureFFT.Size = new System.Drawing.Size(498, 67);
            this.pictureFFT.TabIndex = 0;
            this.pictureFFT.TabStop = false;
            // 
            // pictureGraph
            // 
            this.pictureGraph.BackColor = System.Drawing.Color.Goldenrod;
            this.pictureGraph.Count = 0;
            this.pictureGraph.DatalessColor = System.Drawing.Color.BlanchedAlmond;
            this.pictureGraph.Location = new System.Drawing.Point(16, 40);
            this.pictureGraph.Name = "pictureGraph";
            this.pictureGraph.Position = 0;
            this.pictureGraph.Samples = new double[0];
            this.pictureGraph.Size = new System.Drawing.Size(488, 60);
            this.pictureGraph.TabIndex = 1;
            this.pictureGraph.TabStop = false;
            // 
            // coordLabel1
            // 
            this.coordLabel1.AutoSize = true;
            this.coordLabel1.BackgroundControl = null;
            this.coordLabel1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.coordLabel1.Location = new System.Drawing.Point(0, 0);
            this.coordLabel1.Name = "coordLabel1";
            this.coordLabel1.Size = new System.Drawing.Size(10, 35);
            this.coordLabel1.TabIndex = 2;
            // 
            // coordLabel2
            // 
            this.coordLabel2.AutoSize = true;
            this.coordLabel2.BackgroundControl = null;
            this.coordLabel2.Cursor = System.Windows.Forms.Cursors.Cross;
            this.coordLabel2.Location = new System.Drawing.Point(0, 0);
            this.coordLabel2.Name = "coordLabel2";
            this.coordLabel2.Size = new System.Drawing.Size(10, 35);
            this.coordLabel2.TabIndex = 2;
            // 
            // SampleDisplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SampleDisplayer";
            this.Size = new System.Drawing.Size(534, 284);
            this.Load += new System.EventHandler(this.SampleDisplayer_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureFFT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private SampleGraphDisplay pictureGraph;
        private CoordLabel coordLabel1;
        private CoordLabel coordLabel2;
        private System.Windows.Forms.PictureBox pictureFFT;
    }
}
