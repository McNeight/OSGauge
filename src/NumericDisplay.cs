// <copyright file="NumericDisplay.cs" company="Neil McNeight">
// Copyright © 2008 fatmantwo.
// Copyright © 2019 Neil McNeight. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OSGauge
{
    /// <summary>
    /// Represents a <see cref="NumericDisplay"/> <see cref='System.Windows.Forms.Panel'/> control.
    /// </summary>
    public partial class NumericDisplay : Panel
    {
        private int _nrOfDigits = 3;

        public int NrOfDigits
        {
            get => this._nrOfDigits;
            set
            {
                this._nrOfDigits = value;
                this.RecalculateSize();
            }
        }

        private int _decimals = 2;

        public int Decimals
        {
            get => this._decimals;
            set
            {
                this._decimals = value;
                this.RecalculateSize();
            }
        }

        /// <inheritdoc/>
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
                    this.RecalculateSize();
                    this.PreDrawDigits();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private float _value;

        public float Value
        {
            get => this._value;
            set
            {
                this._value = value;
                this.Invalidate();
            }
        }

        private int _borderWidth = 2;

        public int BorderWidth
        {
            get => this._borderWidth;
            set
            {
                this._borderWidth = value;
                this.PreDrawDigits();
                this.Invalidate();
            }
        }

        private Color _centerColor = Color.WhiteSmoke;

        public Color CenterColor
        {
            get => this._centerColor;
            set
            {
                this._centerColor = value;
                this.PreDrawDigits();
                this.Invalidate();
            }
        }

        private Color _surroundColor = Color.Silver;

        public Color SurroundColor
        {
            get => this._surroundColor;
            set
            {
                this._surroundColor = value;
                this.PreDrawDigits();
                this.Invalidate();
            }
        }

        private Color _borderColorOutside;

        public Color BorderColorOutside
        {
            get => this._borderColorOutside;
            set
            {
                this._borderColorOutside = value;
                this.PreDrawDigits();
                this.Invalidate();
            }
        }

        private Color _borderColorInside;

        public Color BorderColorInside
        {
            get => this._borderColorInside;
            set
            {
                this._borderColorInside = value;
                this.PreDrawDigits();
                this.Invalidate();
            }
        }

        private Color _textColor = Color.Black;

        public Color TextColor
        {
            get => this._textColor;
            set
            {
                this._textColor = value;
                this.PreDrawDigits();
                this.Invalidate();
            }
        }

        private Color _textShadowColor = Color.Gray;

        public Color TextShadowColor
        {
            get => this._textShadowColor;
            set
            {
                this._textShadowColor = value;
                this.PreDrawDigits();
                this.Invalidate();
            }
        }

        private int _shadowBias = 1;

        public int ShadowBias
        {
            get => this._shadowBias;
            set
            {
                this._shadowBias = value;
                this.PreDrawDigits();
                this.Invalidate();
            }
        }

        private SizeF _digitSize = new SizeF(1, 1);

        private void RecalculateSize()
        {
            var g = this.CreateGraphics();
            this._digitSize = g.MeasureString("0", this.Font);
            this._digitSize.Width = (float)Math.Ceiling(this._digitSize.Width);
            this._digitSize.Height = (float)Math.Ceiling(this._digitSize.Height);

            float _newWidth = 0;
            float _newHeight = 0;

            if (this._decimals > 0)
            {
                _newWidth += (this._decimals + 1) * this._digitSize.Width;
            }
            _newWidth += this._nrOfDigits * this._digitSize.Width;
            _newHeight = this._digitSize.Height;

            _newWidth += 2 * this._borderWidth;
            _newHeight += 2 * this._borderWidth;

            if ((int)_newWidth != this.Width || (int)_newHeight != this.Height)
            {
                this.Width = (int)_newWidth;
                this.Height = (int)_newHeight;
                this.dbBitmap = new Bitmap(this.Width, this.Height);
                this.dbGraphics = Graphics.FromImage(this.dbBitmap);
            }
        }

        private readonly Dictionary<string, Bitmap> _digits = new Dictionary<string, Bitmap>();

        private void PreDrawDigits()
        {
            this.RecalculateSize();
            var chr = string.Empty;
            Graphics g;

            RectangleF rf = new Rectangle(0, 0, (int)this._digitSize.Width, (int)this._digitSize.Height);
            var rf2 = rf;
            rf2.Height /= 2;
            var lgb = new LinearGradientBrush(rf2, this._centerColor, this._surroundColor, 270f)
            {
                WrapMode = WrapMode.TileFlipXY
            };

            var _shadowBrush = new SolidBrush(this._textShadowColor);
            var _textBrush = new SolidBrush(this._textColor);

            for (var i = 0; i < 10; i++)
            {
                chr = i.ToString();
                this._digits[chr] = new Bitmap((int)this._digitSize.Width, (int)this._digitSize.Height);
                g = Graphics.FromImage(this._digits[chr]);
                g.FillRectangle(lgb, rf);
                g.DrawLine(Pens.Black, 0, 0, 0, (int)this._digitSize.Height);
                g.DrawLine(Pens.White, 1, 0, 1, (int)this._digitSize.Height);
                g.DrawString(chr, this.Font, _shadowBrush, this._shadowBias, this._shadowBias);
                g.DrawString(chr, this.Font, _textBrush, 0, 0);
            }

            chr = ".";
            this._digits[chr] = new Bitmap((int)this._digitSize.Width, (int)this._digitSize.Height);
            g = Graphics.FromImage(this._digits[chr]);
            g.FillRectangle(lgb, rf);
            g.DrawLine(Pens.Black, 0, 0, 0, (int)this._digitSize.Height);
            g.DrawLine(Pens.White, 1, 0, 1, (int)this._digitSize.Height);
            g.DrawString(chr, this.Font, _shadowBrush, this._shadowBias, this._shadowBias);
            g.DrawString(chr, this.Font, _textBrush, 0, 0);

            chr = "-";
            this._digits[chr] = new Bitmap((int)this._digitSize.Width, (int)this._digitSize.Height);
            g = Graphics.FromImage(this._digits[chr]);
            g.FillRectangle(lgb, rf);
            g.DrawLine(Pens.Black, 0, 0, 0, (int)this._digitSize.Height);
            g.DrawLine(Pens.White, 1, 0, 1, (int)this._digitSize.Height);
            g.DrawString(chr, this.Font, _shadowBrush, this._shadowBias, this._shadowBias);
            g.DrawString(chr, this.Font, _textBrush, 0, 0);
        }

        private void DrawOrnament(Graphics g)
        {
            GraphicsPath gp;

            Rectangle r;
            LinearGradientBrush lgb;
            if (this._borderWidth < 2)
            {
                this._borderWidth = 2;
            }

            r = new Rectangle(0, 0, this.Width, this._borderWidth / 2);
            lgb = new LinearGradientBrush(r, this._borderColorOutside, this._borderColorInside, 90f)
            {
                WrapMode = WrapMode.TileFlipXY
            };
            gp = new GraphicsPath();
            gp.AddLine(0, 0, this.Width, 0);
            gp.AddLine(this.Width, 0, this.Width - this._borderWidth, this._borderWidth);
            gp.AddLine(this.Width - this._borderWidth, this._borderWidth, this._borderWidth, this._borderWidth);
            gp.AddLine(this._borderWidth, this._borderWidth, 0, 0);
            g.FillPath(lgb, gp);

            r = new Rectangle(0, this.Height - this._borderWidth, this.Width, this._borderWidth / 2);
            lgb = new LinearGradientBrush(r, this._borderColorOutside, this._borderColorInside, 90f)
            {
                WrapMode = WrapMode.TileFlipXY
            };
            gp = new GraphicsPath();
            gp.AddLine(this._borderWidth, this.Height - this._borderWidth, this.Width - this._borderWidth, this.Height - this._borderWidth);
            gp.AddLine(this.Width - this._borderWidth, this.Height - this._borderWidth, this.Width, this.Height);
            gp.AddLine(this.Width, this.Height, 0, this.Height);
            gp.AddLine(0, this.Height, this._borderWidth, this.Height - this._borderWidth);
            g.FillPath(lgb, gp);

            r = new Rectangle(0, 0, this._borderWidth / 2, this.Height);
            lgb = new LinearGradientBrush(r, this._borderColorOutside, this._borderColorInside, 0f)
            {
                WrapMode = WrapMode.TileFlipXY
            };
            gp = new GraphicsPath();
            gp.AddLine(0, 0, this._borderWidth, this._borderWidth);
            gp.AddLine(this._borderWidth, this._borderWidth, this._borderWidth, this.Height - this._borderWidth);
            gp.AddLine(this._borderWidth, this.Height - this._borderWidth, 0, this.Height);
            gp.AddLine(0, this.Height, 0, 0);
            g.FillPath(lgb, gp);

            r = new Rectangle(this.Width - this._borderWidth, 0, this._borderWidth / 2, this.Height);
            lgb = new LinearGradientBrush(r, this._borderColorOutside, this._borderColorInside, 0f)
            {
                WrapMode = WrapMode.TileFlipXY
            };
            gp = new GraphicsPath();
            gp.AddLine(this.Width, 0, this.Width, this.Height);
            gp.AddLine(this.Width, this.Height, this.Width - this._borderWidth, this.Height - this._borderWidth);
            gp.AddLine(this.Width - this._borderWidth, this.Height - this._borderWidth, this.Width - this._borderWidth, this._borderWidth);
            gp.AddLine(this.Width - this._borderWidth, this._borderWidth, this.Width, 0);
            g.FillPath(lgb, gp);
        }

        private void DrawValue(Graphics g)
        {
            // RecalculateSize();

            this.DrawOrnament(this.dbGraphics);

            var x = Convert.ToInt32(((this._value * (float)Math.Pow(10, this._decimals))) / Math.Pow(10, this._decimals));

            // char[] _digitsChars = ((Convert.ToInt32( _Value*(float) Math.Pow(10, _Decimals)) ) / Math.Pow(10, _Decimals)).ToString().ToCharArray();
            var _digitsString = ((Convert.ToInt32(this._value * (float)Math.Pow(10, this._decimals))) / Math.Pow(10, this._decimals)).ToString();

            _digitsString = string.Format("{0:0." + new string('0', this._decimals) + "}", this._value);
            // Console.WriteLine("_digitsString :  {0} ", _digitsString);

            var _digitsChars = _digitsString.ToCharArray();

            var _pad = this._decimals;
            if (this._decimals > 0)
            {
                _pad += 1;
            }
            _pad += this._nrOfDigits;
            _pad -= _digitsChars.Length;

            for (var i = 0; i < _pad; i++)
            {
                var _left = (int)(i * this._digitSize.Width);
                this.dbGraphics.DrawImage(this._digits["0"], _left + this._borderWidth, this._borderWidth);
            }

            for (var i = _pad; i < _pad + _digitsChars.Length; i++)
            {
                var _left = (int)(i * this._digitSize.Width);
                this.dbGraphics.DrawImage(this._digits[_digitsChars[i - _pad].ToString()], _left + this._borderWidth, this._borderWidth);
            }

            g.DrawImage(this.dbBitmap, 0, 0);

            // CreateGraphics().DrawImage(dbBitmap,0,0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericDisplay"/> class.
        /// </summary>
        public NumericDisplay()
        {
            this.InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);
            this.PreDrawDigits();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericDisplay"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> to add 'this' to.</param>
        public NumericDisplay(IContainer container)
            : this()
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);
        }

        private Bitmap dbBitmap;
        private Graphics dbGraphics;

        private void OSG_NumericDisplay_Paint(object sender, PaintEventArgs e)
        {
            /*
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            */

            // dbGraphics = Graphics.FromImage(dbBitmap);
            // Graphics g=e.Graphics;            DrawValue(ref g);
            this.DrawValue(e.Graphics);
            // e.Graphics.DrawImage(dbBitmap, 0, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // base.OnPaint(e);
            this.DrawValue(e.Graphics);
        }

        private void OSG_NumericDisplay_Resize(object sender, EventArgs e)
        {
            this.RecalculateSize();
            this.Invalidate();
        }
    }
}
