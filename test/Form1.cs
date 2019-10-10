using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OSGaugesTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            osG_Round1.Value = trackBar1.Value;
            osG_Round2.Value = trackBar1.Value;
            osG_Round3.Value = trackBar1.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Maximum = osG_Round2.Maximum;
            trackBar1.Minimum = osG_Round2.Minimum;
            

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            osG_NumericDisplay1.Value = (float)trackBar2.Value / 100;
        }
    }
}
