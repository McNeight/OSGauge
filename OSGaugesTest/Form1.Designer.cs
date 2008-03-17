namespace OSGaugesTest
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
        	this.components = new System.ComponentModel.Container();
        	this.button1 = new System.Windows.Forms.Button();
        	this.trackBar1 = new System.Windows.Forms.TrackBar();
        	this.osG_Round1 = new OSGaugesLib.OSG_Round(this.components);
        	((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// button1
        	// 
        	this.button1.Location = new System.Drawing.Point(410, 28);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(75, 23);
        	this.button1.TabIndex = 1;
        	this.button1.Text = "button1";
        	this.button1.UseVisualStyleBackColor = true;
        	this.button1.Click += new System.EventHandler(this.button1_Click);
        	// 
        	// trackBar1
        	// 
        	this.trackBar1.Location = new System.Drawing.Point(532, 70);
        	this.trackBar1.Name = "trackBar1";
        	this.trackBar1.Size = new System.Drawing.Size(236, 42);
        	this.trackBar1.TabIndex = 3;
        	this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
        	// 
        	// osG_Round1
        	// 
        	this.osG_Round1.BackColor = System.Drawing.Color.Transparent;
        	this.osG_Round1.CenterColor = System.Drawing.Color.RoyalBlue;
        	this.osG_Round1.DisplayTickText = true;
        	this.osG_Round1.Location = new System.Drawing.Point(75, 96);
        	this.osG_Round1.Maximum = 100;
        	this.osG_Round1.Minimum = 50;
        	this.osG_Round1.MinimumSize = new System.Drawing.Size(40, 40);
        	this.osG_Round1.Name = "osG_Round1";
        	this.osG_Round1.NeedleColor = System.Drawing.Color.Navy;
        	this.osG_Round1.NeedleLength = 100;
        	this.osG_Round1.ShowNeedle = true;
        	this.osG_Round1.Size = new System.Drawing.Size(294, 316);
        	this.osG_Round1.StrokeNeedle = true;
        	this.osG_Round1.SurroundColor = System.Drawing.Color.MidnightBlue;
        	this.osG_Round1.TabIndex = 4;
        	this.osG_Round1.TickAlternateLineLength = 15;
        	this.osG_Round1.TickFrequency = 10;
        	this.osG_Round1.TickLineLength = 10;
        	this.osG_Round1.TickLineOffset = 5;
        	this.osG_Round1.TicksArcEnd = 380F;
        	this.osG_Round1.TicksArcStart = 120F;
        	this.osG_Round1.TicksColor = System.Drawing.Color.Aqua;
        	this.osG_Round1.TickTextOffset = 10;
        	this.osG_Round1.TrimColor = System.Drawing.SystemColors.ControlDark;
        	this.osG_Round1.TrimWidth = 6;
        	this.osG_Round1.Value = 50;
        	// 
        	// Form1
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(1142, 529);
        	this.Controls.Add(this.osG_Round1);
        	this.Controls.Add(this.trackBar1);
        	this.Controls.Add(this.button1);
        	this.Name = "Form1";
        	this.Text = "Form1";
        	this.Load += new System.EventHandler(this.Form1_Load);
        	((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar1;
        private OSGaugesLib.OSG_Round osG_Round1;









    }
}

