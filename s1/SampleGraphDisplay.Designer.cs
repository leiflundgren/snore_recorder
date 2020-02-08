namespace s1
{
    partial class SampleGraphDisplay
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
            this.pictureGraph = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureGraph
            // 
            this.pictureGraph.BackColor = System.Drawing.Color.Goldenrod;
            this.pictureGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureGraph.Location = new System.Drawing.Point(0, 0);
            this.pictureGraph.Name = "pictureGraph";
            this.pictureGraph.Size = new System.Drawing.Size(680, 228);
            this.pictureGraph.TabIndex = 2;
            this.pictureGraph.TabStop = false;
            this.pictureGraph.Click += new System.EventHandler(this.pictureGraph_Click);
            // 
            // SampleGraphDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureGraph);
            this.Name = "SampleGraphDisplay";
            this.Size = new System.Drawing.Size(680, 228);
            this.Load += new System.EventHandler(this.SampleGraphDisplay_Load);
            this.Resize +=new System.EventHandler(SampleGraphDisplay_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureGraph;
    }
}
