// <copyright file="Form1.cs" company="Neil McNeight">
// Copyright © 2008 fatmantwo.
// Copyright © 2019 Neil McNeight. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.
// </copyright>

using System;
using System.Windows.Forms;

namespace OSGaugeTest
{
    /// <summary>
    /// The main form for the application.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.trackBar1.Maximum = this.osG_Round2.Maximum;
            this.trackBar1.Minimum = this.osG_Round2.Minimum;
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            this.osG_Round1.Value = this.trackBar1.Value;
            this.osG_Round2.Value = this.trackBar1.Value;
            this.osG_Round3.Value = this.trackBar1.Value;
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            this.osG_NumericDisplay1.Value = (float)this.trackBar2.Value / 100;
        }
    }
}
