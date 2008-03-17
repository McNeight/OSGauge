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

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            osG_Round1.Value = trackBar1.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Maximum = osG_Round1.Maximum;
            trackBar1.Minimum = osG_Round1.Minimum;
        }
    }
}
