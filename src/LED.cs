// <copyright file="LED.cs" company="Neil McNeight">
// Copyright © 2008 fatmantwo.
// Copyright © 2019 Neil McNeight. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.
// </copyright>

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace OSGauge
{
    /// <summary>
    /// Represents an <see cref="LED"/> <see cref="DoubleBuffered"/> <see cref='System.Windows.Forms.Panel'/> control.
    /// </summary>
    public partial class LED : DoubleBuffered
    {
        private int radius = 0;
        private int _ledSize = 16;

        /// <summary>
        /// Initializes a new instance of the <see cref="LED"/> class.
        /// </summary>
        public LED()
        : base()
        {
            this.InitializeComponent();
            this.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LED"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> to add 'this' to.</param>
        public LED(IContainer container)
            : this()
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);
        }

        public int LedSize
        {
            get => this._ledSize;
            set
            {
                this._ledSize = value;
                if (this._ledSize < 3)
                {
                    this._ledSize = 16;
                }

                this.OS_Led_Resize(null, null);
                this.radius = (this._ledSize / 2) - 2;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets border width.
        /// </summary>
        public int BorderWidth { get; set; } = 1;

        /// <inheritdoc/>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            this.DrawLed(e.Graphics);
        }

        private void OS_Led_Resize(object sender, EventArgs e)
        {
            this.Height = this.Width;
            this._ledSize = this.Width;
            this.Invalidate();
        }

        private void DrawLed(Graphics g)
        {
            // g.DrawEllipse(Pens.Red, Width / 2 - radius, Width / 2 - radius, 2*radius, 2*radius);

            var gp = new GraphicsPath();
            gp.AddEllipse(-15, -15, this._ledSize + 30, this._ledSize + 30);

            var pgb = new PathGradientBrush(gp);
            var _ColorBlend = new ColorBlend(3)
            {
                Colors = new Color[] { Color.Transparent, Color.Transparent, Color.Red },
                Positions = new float[] { 0f, .3f, 1f }
            };

            pgb.CenterColor = Color.Red;
            pgb.InterpolationColors = _ColorBlend;

            // g.FillEllipse(Brushes.Red, 1, 1, _LedSize - 3, _LedSize - 3);
            // g.DrawEllipse(Pens.RosyBrown, 1, 1, _LedSize - 3, _LedSize - 3);

            // Parent.CreateGraphics().FillEllipse(Brushes.Red, Left-5, Top-5, _LedSize +10, _LedSize +10);
            g.FillPath(pgb, gp);

            var gp2 = gp;
            var m = new Matrix();
            m.Translate(this.Left, this.Top);
            gp2.Transform(m);

            var pgb2 = new PathGradientBrush(gp2)
            {
                CenterColor = Color.Red,
                InterpolationColors = _ColorBlend
            };

            this.Parent.CreateGraphics().FillPath(pgb2, gp2);

            // Parent.CreateGraphics().FillPath(pgb, gp);
        }
    }
}
