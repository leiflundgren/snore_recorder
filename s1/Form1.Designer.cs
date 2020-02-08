namespace s1
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnLoadDefault = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numFromSample = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numToSample = new System.Windows.Forms.NumericUpDown();
            this.btnStop = new System.Windows.Forms.Button();
            this.sampleDisplayer1 = new s1.SampleDisplayer();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFromSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToSample)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnLoadDefault
            // 
            this.btnLoadDefault.Location = new System.Drawing.Point(93, 13);
            this.btnLoadDefault.Name = "btnLoadDefault";
            this.btnLoadDefault.Size = new System.Drawing.Size(75, 23);
            this.btnLoadDefault.TabIndex = 0;
            this.btnLoadDefault.Text = "Load 2";
            this.btnLoadDefault.UseVisualStyleBackColor = true;
            this.btnLoadDefault.Click += new System.EventHandler(this.btnLoadDefault_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.sampleDisplayer1);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1030, 399);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // numFromSample
            // 
            this.numFromSample.Location = new System.Drawing.Point(237, 16);
            this.numFromSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numFromSample.Name = "numFromSample";
            this.numFromSample.Size = new System.Drawing.Size(102, 20);
            this.numFromSample.TabIndex = 2;
            this.numFromSample.ValueChanged += new System.EventHandler(this.numFromSample_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "From (s)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "To (s)";
            // 
            // numToSample
            // 
            this.numToSample.Location = new System.Drawing.Point(423, 16);
            this.numToSample.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numToSample.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numToSample.Name = "numToSample";
            this.numToSample.Size = new System.Drawing.Size(102, 20);
            this.numToSample.TabIndex = 4;
            this.numToSample.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numToSample.ValueChanged += new System.EventHandler(this.numToSample_ValueChanged);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStop.Location = new System.Drawing.Point(958, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(84, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "&Stop drawing";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // sampleDisplayer1
            // 
            this.sampleDisplayer1.FrequenzyCutoffMax = 10000;
            this.sampleDisplayer1.FrequenzyCutoffMin = 30;
            this.sampleDisplayer1.Location = new System.Drawing.Point(6, 19);
            this.sampleDisplayer1.Name = "sampleDisplayer1";
            this.sampleDisplayer1.Size = new System.Drawing.Size(1026, 360);
            this.sampleDisplayer1.TabIndex = 0;
            this.sampleDisplayer1.TimeslotLength = 0;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnLoad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnStop;
            this.ClientSize = new System.Drawing.Size(1054, 452);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numToSample);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numFromSample);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoadDefault);
            this.Controls.Add(this.btnLoad);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numFromSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToSample)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnLoadDefault;
        private System.Windows.Forms.GroupBox groupBox1;
        private SampleDisplayer sampleDisplayer1;
        private System.Windows.Forms.NumericUpDown numFromSample;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numToSample;
        private System.Windows.Forms.Button btnStop;
    }
}

