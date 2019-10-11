// <copyright file="Linear.cs" company="Neil McNeight">
// Copyright © 2008 fatmantwo.
// Copyright © 2019 Neil McNeight. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.
// </copyright>

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OSGauge
{
    public enum TickSide
    {
        Left,
        Right,
        Top,
        Down,
        Both
    }

    public enum IndicatorType
    {
        Line,
        Fill
    }

    /// <summary>
    /// Represents a <see cref="Linear"/> <see cref='System.Windows.Forms.Panel'/> control.
    /// </summary>
    [DefaultProperty("Value")]
    public partial class Linear : Panel
    {
        /// <summary>
        /// Gets or sets the upper limit of values of the displayed range.
        /// </summary>
        [Category("LinearGauge")]
        [DefaultValue(10)]
        public int Maximum { get; set; }

        /// <summary>
        /// Gets or sets the lower limit of values of the displayed range.
        /// </summary>
        [Category("LinearGauge")]
        [DefaultValue(0)]
        public int Minimum { get; set; }

        private float _value = 0f;

        [Category("LinearGauge")]

        public float Value
        {
            get => this._value;
            set
            {
                this._value = value;
                this.Invalidate();
            }
        }

        private int _tickFrequency = 1;

        [Category("LinearGauge")]
        public int TickFrequency
        {
            get => this._tickFrequency;
            set
            {
                this._tickFrequency = value;
                this.Invalidate();
            }
        }

        private int _tickAlternateFrequency = 5;

        [Category("LinearGauge")]
        public int TickAlternateFrequency
        {
            get => this._tickAlternateFrequency;
            set
            {
                this._tickAlternateFrequency = value;
                this.Invalidate();
            }
        }

        private int _tickLineLength = 5;

        [Category("LinearGauge")]
        public int TickLineLength
        {
            get => this._tickLineLength;
            set
            {
                this._tickLineLength = value;
                this.Invalidate();
            }
        }

        private int _tickAlternateLineLength = 10;

        [Category("LinearGauge")]
        public int TickAlternateLineLength
        {
            get => this._tickAlternateLineLength;
            set
            {
                this._tickAlternateLineLength = value;
                this.Invalidate();
            }
        }

        private int _tickStartBias = 10;

        [Category("LinearGauge")]
        public int TickStartBias
        {
            get => this._tickStartBias;
            set
            {
                this._tickStartBias = value;
                this.Invalidate();
            }
        }

        private int _tickEndBias = 10;

        [Category("LinearGauge")]
        public int TickEndBias
        {
            get => this._tickEndBias;
            set
            {
                this._tickEndBias = value;
                this.Invalidate();
            }
        }

        private bool _vertical = true;

        [Category("LinearGauge")]
        public bool Vertical
        {
            get => this._vertical;
            set
            {
                this._vertical = value;
                this.Invalidate();
            }
        }

        private Color _centerColor = Color.White;

        [Category("LinearGauge")]
        public Color CenterColor
        {
            get => this._centerColor;
            set
            {
                this._centerColor = value;
                this.Invalidate();
            }
        }

        private Color _surroundColor = Color.Silver;

        [Category("LinearGauge")]
        public Color SurroundColor
        {
            get => this._surroundColor;
            set
            {
                this._surroundColor = value;
                this.Invalidate();
            }
        }

        private bool _reverse;

        [Category("LinearGauge")]
        public bool Reverse
        {
            get => this._reverse;
            set
            {
                this._reverse = value;
                this.Invalidate();
            }
        }

        private TickSide _tickSide = TickSide.Both;

        [Category("LinearGauge")]
        public TickSide TickSide
        {
            get => this._tickSide;
            set
            {
                this._tickSide = value;
                this.Invalidate();
            }
        }

        private IndicatorType _indicatorType = IndicatorType.Line;

        [DefaultValue(IndicatorType.Line)]
        [Category("LinearGauge")]
        public IndicatorType IndicatorType
        {
            get => this._indicatorType;
            set
            {
                this._indicatorType = value;
                this.Invalidate();
            }
        }

        private Color _indicatorFillColor = Color.Black;

        [Category("LinearGauge")]
        public Color IndicatorFillColor
        {
            get => this._indicatorFillColor;
            set
            {
                this._indicatorFillColor = value;
                this.Invalidate();
            }
        }

        private byte _indicatorFillAlpha = 255;

        [Category("LinearGauge")]
        [DefaultValue((byte)255)]
        public byte IndicatorFillAlpha
        {
            get => this._indicatorFillAlpha;
            set
            {
                this._indicatorFillAlpha = value;
                this.Invalidate();
            }
        }

        private int _indicatorWidth = 10;

        [Category("LinearGauge")]
        [DefaultValue(10)]
        public int IndicatorWidth
        {
            get => this._indicatorWidth;
            set
            {
                this._indicatorWidth = value;
                this.Invalidate();
            }
        }

        [Category("LinearGauge")]

        public override Font Font
        {
            get => base.Font;

            set
            {
                try
                {
                    if (value == this.Font)
                    {
                        return;
                    }

                    base.Font = value;
                    this.Invalidate();
                }
                catch (Exception)
                {
                    // MessageBox.Show(ex.Message);
                }
            }
        }

        private void DrawOrnaments(Graphics g)
        {
            RectangleF rf = new Rectangle(0, 0, this.Width, this.Height);
            var rf2 = rf;
            rf2.Height /= 2;
            var lgb = new LinearGradientBrush(rf2, this._centerColor, this._surroundColor, 270f)
            {
                WrapMode = WrapMode.TileFlipXY
            };
            g.FillRectangle(lgb, rf);
        }

        private void DrawTicks(Graphics g)
        {
            // TickAlternateFrequency

            int fVal, sVal, inc;
            if (this.Reverse)
            {
                fVal = this.Maximum;
                sVal = this.Minimum;
                inc = -this._tickFrequency;
            }
            else
            {
                fVal = this.Minimum;
                sVal = this.Maximum;
                inc = this._tickFrequency;
            }

            // int i = fVal;
            // while (Math.Sign(i - (sVal + inc)) * inc < 0)
            // {

            for (var i = fVal; ; i += inc)
            {
                if (inc > 0 && i > sVal)
                {
                    break;
                }
                else if (inc < 0 && i < sVal)
                {
                    break;
                }
                int y;

                if (this.Reverse)
                {
                    y = this._tickStartBias + (((this.Height - (this._tickEndBias + this._tickStartBias)) * i / (this.Maximum - this.Minimum)));
                }
                else
                {
                    y = this.Height - (this._tickStartBias + ((this.Height - (this._tickEndBias + this._tickStartBias)) * i / (this.Maximum - this.Minimum)));
                }

                if (i % this.TickAlternateFrequency == 0)
                {
                    g.DrawLine(Pens.Blue, 0, y, this._tickAlternateLineLength, y);
                    var fs = g.MeasureString("0", this.Font);
                    g.DrawString(i.ToString(), this.Font, Brushes.Red, new Point(20, (int)(y - fs.Height / 2)));
                }
                else
                {
                    g.DrawLine(Pens.Blue, 0, y, this._tickLineLength, y);
                }
            }
        }

        private void DrawValue(Graphics g)
        {
            int y;
            if (this.Reverse)
            {
                y = (int)(this._tickStartBias + (((this.Height - (this._tickEndBias + this._tickStartBias)) * this._value / (this.Maximum - this.Minimum))));
            }
            else
            {
                y = (int)(this.Height - (this._tickStartBias + ((this.Height - (this._tickEndBias + this._tickStartBias)) * this._value / (this.Maximum - this.Minimum))));
            }

            if (this._indicatorType == IndicatorType.Fill)
            {
                Rectangle r;
                if (this.Reverse)
                {
                    r = new Rectangle(this.Width / 2 - this._indicatorWidth / 2, this._tickStartBias, this._indicatorWidth, y - this._tickStartBias);
                }
                else
                {
                    r = new Rectangle(this.Width / 2 - this._indicatorWidth / 2, y, this._indicatorWidth, this.Height - y - this._tickStartBias);
                }

                g.FillRectangle(new SolidBrush(Color.FromArgb(this._indicatorFillAlpha, this._indicatorFillColor)), r);
            }
            else
                if (this._indicatorType == IndicatorType.Line)
            {
                g.DrawLine(Pens.Red, this._tickAlternateLineLength + 5, y, this.Width, y);
            }
        }

        public Linear()
        {
            this.InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);
        }

        public Linear(IContainer container)
        {
            container.Add(this);

            this.InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // base.OnPaint(e);
            this.DrawOrnaments(e.Graphics);
            this.DrawTicks(e.Graphics);
            this.DrawValue(e.Graphics);
        }
    }
}
