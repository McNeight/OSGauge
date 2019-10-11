// <copyright file="Round.cs" company="Neil McNeight">
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
    public partial class Round : Panel, IRound
    {
        private int _radius;
        private Point _center;
        private PathGradientBrush _ellipseBrush;
        private PathGradientBrush _trimBrush;
        private Bitmap _ellipseBitmap;
        private Bitmap _needleBitmap;
        private Bitmap _needleBitmap2;
        private GraphicsPath _needlePath;
        private int _needleWidth;
        private int _needleHeight;

        private Pen _ticksPen = new Pen(Color.Black);
        private SolidBrush _needleBrush = new SolidBrush(Color.Black);

        private GraphicsPath _ellipsePath = null;
        private GraphicsPath _trimPath = null;
        private GraphicsPath _ticksPath = null;
        private float _ticksArcStart = 120;

        public float TicksArcStart
        {
            get => this._ticksArcStart;
            set
            {
                this._ticksArcStart = value;
                this.InitializeGDI();
            }
        }

        private float _ticksArcEnd = 380;

        public float TicksArcEnd
        {
            get => this._ticksArcEnd;
            set
            {
                this._ticksArcEnd = value;
                this.InitializeGDI();
            }
        }

        private int _tickFrequency = 10;

        public int TickFrequency
        {
            get => this._tickFrequency;
            set
            {
                this._tickFrequency = value;
                this.InitializeGDI();
            }
        }

        private int _tickLineLength = 10;

        public int TickLineLength
        {
            get => this._tickLineLength;

            set
            {
                this._tickLineLength = value;
                this.MinimumSize = new Size(2 * (this._tickLineOffset + Math.Max(this._tickAlternateLineLength, this._tickLineLength)), 2 * (this._tickLineOffset + Math.Max(this._tickAlternateLineLength, this._tickLineLength)));
                this.InitializeGDI();
            }
        }

        private int _tickAlternateLineLength = 15;

        public int TickAlternateLineLength
        {
            get => this._tickAlternateLineLength;

            set
            {
                this._tickAlternateLineLength = value;
                this.MinimumSize = new Size(2 * (this._tickLineOffset + Math.Max(this._tickAlternateLineLength, this._tickLineLength)), 2 * (this._tickLineOffset + Math.Max(this._tickAlternateLineLength, this._tickLineLength)));
                this.InitializeGDI();
            }
        }

        private int _tickLineOffset = 5;

        public int TickLineOffset
        {
            get => this._tickLineOffset;

            set
            {
                this._tickLineOffset = value;
                this.MinimumSize = new Size(2 * (this._tickLineOffset + Math.Max(this._tickAlternateLineLength, this._tickLineLength)), 2 * (this._tickLineOffset + Math.Max(this._tickAlternateLineLength, this._tickLineLength)));
                this.InitializeGDI();
            }
        }

        private Color _centerColor = Color.GhostWhite;

        public Color CenterColor
        {
            get => this._centerColor;
            set
            {
                this._centerColor = value;
                this.InitializeGDI();
            }
        }

        private Color _surroundColor = Color.Gainsboro;

        public Color SurroundColor
        {
            get => this._surroundColor;
            set
            {
                this._surroundColor = value;
                this.InitializeGDI();
            }
        }

        private Color _trimColor = Color.Black;

        public Color TrimColor
        {
            get => this._trimColor;
            set
            {
                this._trimColor = value;
                this.InitializeGDI();
            }
        }

        private int _trimWidth = 6;

        public int TrimWidth
        {
            get => this._trimWidth;
            set
            {
                this._trimWidth = value;
                this.InitializeGDI();
            }
        }

        private int _tickTextOffset = 10;

        public int TickTextOffset
        {
            get => this._tickTextOffset;
            set
            {
                this._tickTextOffset = value;
                this.InitializeGDI();
            }
        }

        private bool _displayTickText = true;

        public bool DisplayTickText
        {
            get => this._displayTickText;
            set
            {
                this._displayTickText = value;
                this.InitializeGDI();
            }
        }

        private Color _ticksColor = Color.Black;

        public Color TicksColor
        {
            get => this._ticksColor;
            set
            {
                this._ticksColor = value;
                this._ticksPen = new Pen(this._ticksColor, 1);
                this.InitializeGDI();
            }
        }

        private Color _needleColor = Color.Black;

        public Color NeedleColor
        {
            get => this._needleColor;

            set
            {
                this._needleColor = value;

                this._needleBrush = new SolidBrush(this._needleColor);
                this.InitializeGDI();
            }
        }

        private int _needleLength = 0;

        public int NeedleLength
        {
            get => this._needleLength;
            set
            {
                this._needleLength = value;
                this.InitializeGDI();
            }
        }

        private bool _showNeedle = true;

        public bool ShowNeedle
        {
            get => this._showNeedle;
            set
            {
                this._showNeedle = value;
                this.InitializeGDI();
            }
        }

        private bool _strokeNeedle = true;

        public bool StrokeNeedle
        {
            get => this._strokeNeedle;
            set
            {
                this._strokeNeedle = value;
                this.InitializeGDI();
            }
        }

        private void CreatePaths()
        {
            this._ellipsePath = new GraphicsPath();
            this._ellipsePath.AddEllipse(this._center.X - this._radius, this._center.Y - this._radius, 2 * this._radius, 2 * this._radius);

            this._trimPath = new GraphicsPath();
            this._trimPath.AddEllipse(this._center.X - this._radius + this._trimWidth / 2, this._center.Y - this._radius + this._trimWidth / 2, 2 * this._radius - this._trimWidth, 2 * this._radius - this._trimWidth);
            this._trimPath.StartFigure();
            this._trimPath.AddEllipse(this._center.X - this._radius - this._trimWidth / 2, this._center.Y - this._radius - this._trimWidth / 2, 2 * this._radius + this._trimWidth, 2 * this._radius + this._trimWidth);

            // create ticks
            this._ticksPath = new GraphicsPath();
            var _tickStep = (this._ticksArcEnd * (float)Math.PI / 180 - this._ticksArcStart * (float)Math.PI / 180) / (this._maximum - this._minimum);
            float _radius0 = this._radius - this._tickLineOffset;
            var _radius2 = _radius0 - this._tickLineLength;
            var _radius3 = _radius0 - this._tickAlternateLineLength;
            var tick = this._ticksArcStart * (float)Math.PI / 180;
            var _radiusFrom = _radius0;
            var _radiusTo = _radius2;

            for (var i = this._minimum; i <= this._maximum; i++)
            {
                if (i % this._tickFrequency == 0)
                {
                    _radiusTo = _radius3;
                    if (this._displayTickText)
                    {
                        var _ticksPath2 = new GraphicsPath();
                        _ticksPath2.AddString(i.ToString(), this.Font.FontFamily, 0, this.Font.Size, new Point(0, 0), StringFormat.GenericDefault);
                        _ticksPath2.Flatten();
                        var _radiusTextOffset = Math.Max(_ticksPath2.GetBounds().Width, _ticksPath2.GetBounds().Height);
                        var _textPosition = new PointF();
                        this._center = new Point(this.Width / 2, this.Height / 2);
                        _textPosition.X = this._center.X + (_radius3 - this._tickTextOffset) * (float)Math.Cos(tick) - _ticksPath2.GetBounds().Width + _ticksPath2.GetBounds().Left;
                        _textPosition.Y = this._center.Y + (_radius3 - this._tickTextOffset) * (float)Math.Sin(tick) - _ticksPath2.GetBounds().Height - 0.5f * _ticksPath2.GetBounds().Top;
                        var m = new Matrix();
                        m.Translate(_textPosition.X, _textPosition.Y);
                        _ticksPath2.Transform(m);
                        this._ticksPath.AddPath(_ticksPath2, false);
                    }
                }
                else
                {
                    _radiusTo = _radius2;
                }
                this._ticksPath.StartFigure();
                this._ticksPath.AddLine(this._center.X + _radiusFrom * (float)Math.Cos(tick), this._center.Y + _radiusFrom * (float)Math.Sin(tick), this._center.X + _radiusTo * (float)Math.Cos(tick), this._center.Y + _radiusTo * (float)Math.Sin(tick));
                // Console.WriteLine("{0} - {1} - {2} - {3}", _TicksArcStart, _TicksArcEnd, _tickStep, tick);
                tick += _tickStep;
            }
            this._ticksPath.Flatten();
        }

        private void CreateBrushes()
        {
            var _ColorBlend = new ColorBlend(3)
            {
                Colors = new Color[] { this._surroundColor, this._surroundColor, this._centerColor },
                Positions = new float[] { 0f, .3f, 1f }
            };
            this._ellipseBrush = new PathGradientBrush(this._ellipsePath)
            {
                CenterColor = this._centerColor,
                InterpolationColors = _ColorBlend
            };

            this._ellipseBitmap = new Bitmap(this.Width, this.Height);

            this._trimBrush = new PathGradientBrush(this._trimPath)
            {
                WrapMode = WrapMode.Clamp,
                CenterColor = Color.White
            };

            _ColorBlend.Colors = new Color[] { this._trimColor, Color.White, Color.White };
            _ColorBlend.Positions = new float[] { 0f, .05f, 1f };
            this._trimBrush.InterpolationColors = _ColorBlend;
        }

        public void InitializeGDI()
        {
            this._radius = -5 + Math.Min(this.Width, this.Height) / 2;
            this._center = new Point(this.Width / 2, this.Height / 2);

            this.CreatePaths();
            this.CreateBrushes();

            if (this._showNeedle)
            {
                this.CreateNeedle();
            }
            this.Invalidate();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        public Round()
        {
            this.InitializeComponent();
            this.TickAlternateLineLength = this._tickAlternateLineLength;
            this.TicksColor = this._ticksColor;
            this.NeedleColor = this._needleColor;
            this._showNeedle = (this._needleLength > 0);

            this.InitializeGDI();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> to add 'this' to.</param>
        public Round(IContainer container)
            : this()
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);
        }

        private void OSG_Round_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            // e.Graphics.DrawString(_radius + " " + _Minimum + " " + _Maximum + " " + _Value, new Font("Arial", 10f), Brushes.BlueViolet, 10, 10);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.PaintBackground(e.Graphics);
            this.PaintTicks(e.Graphics);
            if (this._showNeedle)
            {
                this.PaintNeedle(e.Graphics);
            }
        }

        private void PaintBackground(Graphics g)
        {
            // if (_ellipseBitmap == null)				return;
            var g2 = Graphics.FromImage(this._ellipseBitmap);
            g2.SmoothingMode = SmoothingMode.AntiAlias;
            g2.FillPath(this._ellipseBrush, this._ellipsePath);
            g2.FillPath(this._trimBrush, this._trimPath);
            g.DrawImage(this._ellipseBitmap, 0, 0);
        }

        private void PaintTicks(Graphics g)
        {
            var g2 = Graphics.FromImage(this._ellipseBitmap);
            g2.SmoothingMode = SmoothingMode.AntiAlias;

            g2.DrawPath(this._ticksPen, this._ticksPath);
            g.DrawImage(this._ellipseBitmap, 0, 0);
        }

        private void CreateNeedle()
        {
            // _needleWidth = 2 * _radius - 50;
            this._needleWidth = 2 * this._needleLength;
            this._needleHeight = 2 * (this._radius / 10); // has to be an even number!

            if (this._needleWidth == 0 || this._needleHeight == 0)
            {
                return;
            }

            this._needleBitmap = new Bitmap(this._needleWidth, this._needleHeight);

            var g2 = Graphics.FromImage(this._needleBitmap);
            g2.SmoothingMode = SmoothingMode.AntiAlias;

            this._needlePath = new GraphicsPath();
            // _needlePath.AddEllipse(_needleWidth / 2 - _needleHeight / 2, 0, _needleHeight, _needleHeight);

            // _needlePath.AddArc(_needleWidth / 2 - _needleHeight / 2, 0, _needleHeight, _needleHeight, 10, 340);
            this._needlePath.AddArc(this._needleBitmap.Width / 2 - this._needleBitmap.Height / 2 + 1, 1, this._needleBitmap.Height - 2, this._needleBitmap.Height - 2, 10, 340);

            // g2.FillEllipse(_needleBrush, _needleWidth / 2 - _needleHeight / 2, 0, _needleHeight, _needleHeight);
            // _needlePath.AddEllipse(_needleWidth / 2 - _needleHeight / 2, 0, _needleHeight, _needleHeight);

            this._needlePath.FillMode = FillMode.Winding;
            // _needlePath.StartFigure();
            this._needlePath.AddLine(this._needleBitmap.Height / 2 + this._needleBitmap.Width / 2 - 1, this._needleBitmap.Height / 2 - 3, this._needleBitmap.Width - 10, this._needleBitmap.Height / 2 - 3);
            this._needlePath.AddLine(this._needleBitmap.Width - 10, this._needleBitmap.Height / 2 - 3, this._needleBitmap.Width, this._needleBitmap.Height / 2);
            this._needlePath.AddLine(this._needleBitmap.Width, this._needleBitmap.Height / 2, this._needleBitmap.Width - 10, this._needleBitmap.Height / 2 + 3);
            this._needlePath.AddLine(this._needleBitmap.Width - 10, this._needleBitmap.Height / 2 + 3, this._needleBitmap.Height / 2 + this._needleBitmap.Width / 2 - 1, this._needleBitmap.Height / 2 + 3);
            this._needlePath.Flatten();
            // _needlePath.CloseFigure();

            g2.FillPath(this._needleBrush, this._needlePath);

            if (this._strokeNeedle)
            {
                g2.DrawPath(this._ticksPen, this._needlePath);
            }

            this._needlePath.Reset();

            this._needlePath.AddLine(6 * this._needleBitmap.Width / 10, this._needleBitmap.Height / 2, 9 * this._needleBitmap.Width / 10, this._needleBitmap.Height / 2);
            g2.DrawPath(this._ticksPen, this._needlePath);
            // _needlePath.AddLine(_needleWidth / 2, _needleHeight / 2, _needleWidth, _needleHeight / 2);
        }

        private void PaintNeedle(Graphics g)
        {
            Graphics g3 = null;
            this._needleBitmap2 = new Bitmap(this.Width, this.Height);

            g3 = Graphics.FromImage(this._needleBitmap2);
            g3.SmoothingMode = SmoothingMode.AntiAlias;
            var _angle = this._ticksArcStart + (this.Value - this.Minimum) * (this._ticksArcEnd - this._ticksArcStart) / (this._maximum - this._minimum);
            g3.TranslateTransform(this._center.X, this._center.Y);
            g3.RotateTransform(_angle);

            g3.DrawImage(this._needleBitmap, -this._needleBitmap.Width / 2, -this._needleBitmap.Height / 2);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var _sumBitmap = new Bitmap(this.Width, this.Height);
            var _sumGraphics = Graphics.FromImage(_sumBitmap);
            _sumGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            _sumGraphics.DrawImage(this._ellipseBitmap, 0, 0);
            _sumGraphics.DrawImage(this._needleBitmap2, 0, 0);
            g.DrawImage(_sumBitmap, 0, 0);
        }

        private int _minimum = 0;

        public int Minimum
        {
            get => this._minimum;

            set
            {
                this._minimum = value;
                this.InitializeGDI();
            }
        }

        private int _maximum = 100;

        public int Maximum
        {
            get => this._maximum;

            set
            {
                this._maximum = value;
                this.InitializeGDI();
            }
        }

        private int _value;

        public int Value
        {
            get => this._value;

            set
            {
                this._value = value;
                if (this._value < this._minimum)
                {
                    this._value = this._minimum;
                }

                if (this._value > this._maximum)
                {
                    this._value = this._maximum;
                }

                if (this._showNeedle)
                {
                    this.PaintNeedle(this.CreateGraphics());
                }
            }
        }

        private void OSG_Round_Resize(object sender, EventArgs e)
        {
            this.InitializeGDI();
        }
    }
}
