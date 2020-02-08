namespace s1
{
    partial class CoordLabel
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
            this.lblCoordinats = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCoordinats
            // 
            this.lblCoordinats.AutoSize = true;
            this.lblCoordinats.Location = new System.Drawing.Point(0, 0);
            this.lblCoordinats.Margin = new System.Windows.Forms.Padding(0);
            this.lblCoordinats.Name = "lblCoordinats";
            this.lblCoordinats.Size = new System.Drawing.Size(67, 13);
            this.lblCoordinats.TabIndex = 0;
            this.lblCoordinats.Text = "lblCoordinats";
            this.lblCoordinats.Resize += new System.EventHandler(this.lblCoordinats_Resize);
            // 
            // CoordLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.lblCoordinats);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Name = "CoordLabel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCoordinats;
    }
}
