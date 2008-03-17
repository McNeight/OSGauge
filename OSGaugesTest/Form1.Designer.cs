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
        	this.trackBar1 = new System.Windows.Forms.TrackBar();
        	this.osG_Round1 = new OSGaugesLib.OSG_Round(this.components);
        	this.osG_Round2 = new OSGaugesLib.OSG_Round(this.components);
        	this.osG_Round3 = new OSGaugesLib.OSG_Round(this.components);
        	((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// trackBar1
        	// 
        	this.trackBar1.Location = new System.Drawing.Point(32, 334);
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
        	this.osG_Round1.Location = new System.Drawing.Point(12, 12);
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
        	// osG_Round2
        	// 
        	this.osG_Round2.BackColor = System.Drawing.Color.Transparent;
        	this.osG_Round2.CenterColor = System.Drawing.Color.GhostWhite;
        	this.osG_Round2.DisplayTickText = true;
        	this.osG_Round2.Location = new System.Drawing.Point(312, 12);
        	this.osG_Round2.Maximum = 100;
        	this.osG_Round2.Minimum = 10;
        	this.osG_Round2.MinimumSize = new System.Drawing.Size(40, 40);
        	this.osG_Round2.Name = "osG_Round2";
        	this.osG_Round2.NeedleColor = System.Drawing.Color.Black;
        	this.osG_Round2.NeedleLength = 50;
        	this.osG_Round2.ShowNeedle = true;
        	this.osG_Round2.Size = new System.Drawing.Size(189, 185);
        	this.osG_Round2.StrokeNeedle = false;
        	this.osG_Round2.SurroundColor = System.Drawing.Color.Gainsboro;
        	this.osG_Round2.TabIndex = 5;
        	this.osG_Round2.TickAlternateLineLength = 15;
        	this.osG_Round2.TickFrequency = 13;
        	this.osG_Round2.TickLineLength = 10;
        	this.osG_Round2.TickLineOffset = 5;
        	this.osG_Round2.TicksArcEnd = 450F;
        	this.osG_Round2.TicksArcStart = 190F;
        	this.osG_Round2.TicksColor = System.Drawing.Color.Black;
        	this.osG_Round2.TickTextOffset = 10;
        	this.osG_Round2.TrimColor = System.Drawing.Color.Black;
        	this.osG_Round2.TrimWidth = 6;
        	this.osG_Round2.Value = 0;
        	// 
        	// osG_Round3
        	// 
        	this.osG_Round3.BackColor = System.Drawing.Color.Transparent;
        	this.osG_Round3.CenterColor = System.Drawing.Color.LightSteelBlue;
        	this.osG_Round3.DisplayTickText = true;
        	this.osG_Round3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.osG_Round3.Location = new System.Drawing.Point(312, 198);
        	this.osG_Round3.Maximum = 100;
        	this.osG_Round3.Minimum = 15;
        	this.osG_Round3.MinimumSize = new System.Drawing.Size(40, 40);
        	this.osG_Round3.Name = "osG_Round3";
        	this.osG_Round3.NeedleColor = System.Drawing.Color.Black;
        	this.osG_Round3.NeedleLength = 0;
        	this.osG_Round3.ShowNeedle = false;
        	this.osG_Round3.Size = new System.Drawing.Size(130, 130);
        	this.osG_Round3.StrokeNeedle = true;
        	this.osG_Round3.SurroundColor = System.Drawing.Color.SlateGray;
        	this.osG_Round3.TabIndex = 6;
        	this.osG_Round3.TickAlternateLineLength = 15;
        	this.osG_Round3.TickFrequency = 10;
        	this.osG_Round3.TickLineLength = 10;
        	this.osG_Round3.TickLineOffset = 5;
        	this.osG_Round3.TicksArcEnd = 380F;
        	this.osG_Round3.TicksArcStart = 120F;
        	this.osG_Round3.TicksColor = System.Drawing.Color.DarkSlateGray;
        	this.osG_Round3.TickTextOffset = 10;
        	this.osG_Round3.TrimColor = System.Drawing.Color.DarkGray;
        	this.osG_Round3.TrimWidth = 6;
        	this.osG_Round3.Value = 0;
        	// 
        	// Form1
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(636, 529);
        	this.Controls.Add(this.osG_Round3);
        	this.Controls.Add(this.osG_Round2);
        	this.Controls.Add(this.osG_Round1);
        	this.Controls.Add(this.trackBar1);
        	this.Name = "Form1";
        	this.Text = "Speed Gauge Test Form";
        	this.Load += new System.EventHandler(this.Form1_Load);
        	((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private OSGaugesLib.OSG_Round osG_Round3;
        private OSGaugesLib.OSG_Round osG_Round2;

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private OSGaugesLib.OSG_Round osG_Round1;









    }
}

