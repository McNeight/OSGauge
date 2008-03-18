using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace OSGaugesLib
{
    public partial class OSG_NumericDisplay : Panel
    {


        #region property NrOfDigits : int
        private int _NrOfDigits = 3;
        public int NrOfDigits
        {
            get { return _NrOfDigits; }
            set { _NrOfDigits = value; RecalculateSize(); }
        }
        #endregion

        #region property Decimals : int
        private int _Decimals = 2;
        public int Decimals
        {
            get { return _Decimals; }
            set { _Decimals = value; RecalculateSize(); }
        }
        #endregion



        #region property Font : Font
        private Font _Font = new Font("serif", 10f);
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                try
                {
                    if (value == Font)
                        return;
                    base.Font = value;
                    RecalculateSize(); PreDrawDigits();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion


        #region property Value : float
        private float _Value;
        public float Value
        {
            get { return _Value; }
            set { _Value = value; Invalidate(); }
        }
        #endregion




        #region property BorderWidth : int
        private int _BorderWidth = 2;
        public int BorderWidth
        {
            get { return _BorderWidth; }
            set { _BorderWidth = value; PreDrawDigits(); Invalidate(); }
        }
        #endregion


        #region property CenterColor : Color
        private Color _CenterColor = Color.WhiteSmoke;
        public Color CenterColor
        {
            get { return _CenterColor; }
            set { _CenterColor = value; PreDrawDigits(); Invalidate(); }
        }
        #endregion

        #region property SurroundColor : Color
        private Color _SurroundColor = Color.Silver;
        public Color SurroundColor
        {
            get { return _SurroundColor; }
            set { _SurroundColor = value; PreDrawDigits(); Invalidate(); }
        }
        #endregion

        #region property BorderColorOutside : Color
        private Color _BorderColorOutside;
        public Color BorderColorOutside
        {
            get { return _BorderColorOutside; }
            set { _BorderColorOutside = value; PreDrawDigits(); Invalidate(); }
        }
        #endregion

        #region property BorderColorInside : Color
        private Color _BorderColorInside;
        public Color BorderColorInside
        {
            get { return _BorderColorInside; }
            set { _BorderColorInside = value; PreDrawDigits(); Invalidate(); }
        }
        #endregion

        #region property TextColor : Color
        private Color _TextColor = Color.Black;
        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; PreDrawDigits(); Invalidate(); }
        }
        #endregion

        #region property TextShadowColor : Color
        private Color _TextShadowColor = Color.Gray;
        public Color TextShadowColor
        {
            get { return _TextShadowColor; }
            set { _TextShadowColor = value; PreDrawDigits(); Invalidate(); }
        }
        #endregion

        #region property ShadowBias : int
        private int _ShadowBias = 1;
        public int ShadowBias
        {
            get { return _ShadowBias; }
            set { _ShadowBias = value; PreDrawDigits(); Invalidate(); }
        }
        #endregion



        SizeF _digitSize = new SizeF(1, 1);
        private void RecalculateSize()
        {

            Graphics g = CreateGraphics();
            _digitSize = g.MeasureString("0", Font);
            _digitSize.Width = (float)Math.Ceiling((double)_digitSize.Width);
            _digitSize.Height = (float)Math.Ceiling((double)_digitSize.Height);

            float _newWidth = 0;
            float _newHeight = 0;

            if (_Decimals > 0)
            {
                _newWidth += (_Decimals + 1) * _digitSize.Width;
            }
            _newWidth += _NrOfDigits * _digitSize.Width;
            _newHeight = _digitSize.Height;

            _newWidth += 2 * _BorderWidth;
            _newHeight += 2 * _BorderWidth;

            if ((int)_newWidth != Width || (int)_newHeight != Height)
            {
                Width = (int)_newWidth;
                Height = (int)_newHeight;
                dbBitmap = new Bitmap(Width, Height);
                dbGraphics = Graphics.FromImage(dbBitmap);
            }
        }



        private Dictionary<string, Bitmap> _digits = new Dictionary<string, Bitmap>();


        private void PreDrawDigits()
        {
            RecalculateSize();
            string chr = "";
            Graphics g;

            RectangleF rf = new Rectangle(0, 0, (int)_digitSize.Width, (int)_digitSize.Height);
            RectangleF rf2 = rf;
            rf2.Height /= 2;
            LinearGradientBrush lgb = new LinearGradientBrush(rf2, _CenterColor, _SurroundColor, 270f);
            lgb.WrapMode = WrapMode.TileFlipXY;

            SolidBrush _shadowBrush = new SolidBrush(_TextShadowColor);
            SolidBrush _textBrush = new SolidBrush(_TextColor);

            for (int i = 0; i < 10; i++)
            {
                chr = i.ToString();
                _digits[chr] = new Bitmap((int)_digitSize.Width, (int)_digitSize.Height);
                g = Graphics.FromImage(_digits[chr]);
                g.FillRectangle(lgb, rf);
                g.DrawLine(Pens.Black, 0, 0, 0, (int)_digitSize.Height);
                g.DrawLine(Pens.White, 1, 0, 1, (int)_digitSize.Height);
                g.DrawString(chr, Font, _shadowBrush, _ShadowBias, _ShadowBias);
                g.DrawString(chr, Font, _textBrush, 0, 0);
            }


            chr = ".";
            _digits[chr] = new Bitmap((int)_digitSize.Width, (int)_digitSize.Height);
            g = Graphics.FromImage(_digits[chr]);
            g.FillRectangle(lgb, rf);
            g.DrawLine(Pens.Black, 0, 0, 0, (int)_digitSize.Height);
            g.DrawLine(Pens.White, 1, 0, 1, (int)_digitSize.Height);
            g.DrawString(chr, Font, _shadowBrush, _ShadowBias, _ShadowBias);
            g.DrawString(chr, Font, _textBrush, 0, 0);

            chr = "-";
            _digits[chr] = new Bitmap((int)_digitSize.Width, (int)_digitSize.Height);
            g = Graphics.FromImage(_digits[chr]);
            g.FillRectangle(lgb, rf);
            g.DrawLine(Pens.Black, 0, 0, 0, (int)_digitSize.Height);
            g.DrawLine(Pens.White, 1, 0, 1, (int)_digitSize.Height);
            g.DrawString(chr, Font, _shadowBrush, _ShadowBias, _ShadowBias);
            g.DrawString(chr, Font, _textBrush, 0, 0);
            
        }





        private void DrawOrnament(Graphics g)
        {
            GraphicsPath gp;

            Rectangle r;
            LinearGradientBrush lgb;
            if (_BorderWidth < 2) _BorderWidth = 2;
            r = new Rectangle(0, 0, Width, _BorderWidth / 2);
            lgb = new LinearGradientBrush(r, _BorderColorOutside, _BorderColorInside, 90f);
            lgb.WrapMode = WrapMode.TileFlipXY;
            gp = new GraphicsPath();
            gp.AddLine(0, 0, Width, 0);
            gp.AddLine(Width, 0, Width - _BorderWidth, _BorderWidth);
            gp.AddLine(Width - _BorderWidth, _BorderWidth, _BorderWidth, _BorderWidth);
            gp.AddLine(_BorderWidth, _BorderWidth, 0, 0);
            g.FillPath(lgb, gp);


            r = new Rectangle(0, Height - _BorderWidth, Width, _BorderWidth / 2);
            lgb = new LinearGradientBrush(r, _BorderColorOutside, _BorderColorInside, 90f);
            lgb.WrapMode = WrapMode.TileFlipXY;
            gp = new GraphicsPath();
            gp.AddLine(_BorderWidth, Height - _BorderWidth, Width - _BorderWidth, Height - _BorderWidth);
            gp.AddLine(Width - _BorderWidth, Height - _BorderWidth, Width, Height);
            gp.AddLine(Width, Height, 0, Height);
            gp.AddLine(0, Height, _BorderWidth, Height - _BorderWidth);
            g.FillPath(lgb, gp);

            r = new Rectangle(0, 0, _BorderWidth / 2, Height);
            lgb = new LinearGradientBrush(r, _BorderColorOutside, _BorderColorInside, 0f);
            lgb.WrapMode = WrapMode.TileFlipXY;
            gp = new GraphicsPath();
            gp.AddLine(0, 0, _BorderWidth, _BorderWidth);
            gp.AddLine(_BorderWidth, _BorderWidth, _BorderWidth, Height - _BorderWidth);
            gp.AddLine(_BorderWidth, Height - _BorderWidth, 0, Height);
            gp.AddLine(0, Height, 0, 0);
            g.FillPath(lgb, gp);

            r = new Rectangle(Width - _BorderWidth, 0, _BorderWidth / 2, Height);
            lgb = new LinearGradientBrush(r, _BorderColorOutside, _BorderColorInside, 0f);
            lgb.WrapMode = WrapMode.TileFlipXY;
            gp = new GraphicsPath();
            gp.AddLine(Width, 0, Width, Height);
            gp.AddLine(Width, Height, Width - _BorderWidth, Height - _BorderWidth);
            gp.AddLine(Width - _BorderWidth, Height - _BorderWidth, Width - _BorderWidth, _BorderWidth);
            gp.AddLine(Width - _BorderWidth, _BorderWidth, Width, 0);
            g.FillPath(lgb, gp);

        }






        private void DrawValue(Graphics g)
        {
            //RecalculateSize();


            DrawOrnament(dbGraphics);
            
            

            int x = Convert.ToInt32(((_Value * (float)Math.Pow(10, _Decimals))) / Math.Pow(10, _Decimals));

            //char[] _digitsChars = ((Convert.ToInt32( _Value*(float) Math.Pow(10, _Decimals)) ) / Math.Pow(10, _Decimals)).ToString().ToCharArray();
            string _digitsString = ((Convert.ToInt32(_Value * (float)Math.Pow(10, _Decimals))) / Math.Pow(10, _Decimals)).ToString();


            _digitsString = String.Format("{0:0." + new string('0', _Decimals) + "}", _Value);
            //Console.WriteLine("_digitsString :  {0} ", _digitsString);


            char[] _digitsChars = _digitsString.ToCharArray();

            int _pad = _Decimals;
            if (_Decimals > 0)
            {
                _pad += 1;
            }
            _pad += _NrOfDigits;
            _pad -= _digitsChars.Length;

            
            for (int i = 0; i < _pad; i++)
            {
                int _left = (int)(i * _digitSize.Width);
                dbGraphics.DrawImage(_digits["0"], _left + _BorderWidth, _BorderWidth);

            }

            
            for (int i = _pad; i < _pad + _digitsChars.Length; i++)
            {
                int _left = (int)(i * _digitSize.Width);
                dbGraphics.DrawImage(_digits[_digitsChars[i - _pad].ToString()], _left + _BorderWidth, _BorderWidth);
            }


            
            g.DrawImage(dbBitmap, 0, 0);


            //CreateGraphics().DrawImage(dbBitmap,0,0);

        }









        public OSG_NumericDisplay()
        {
            InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);
            PreDrawDigits();
        }

        public OSG_NumericDisplay(IContainer container)
        {
            container.Add(this);


            InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);
            PreDrawDigits();
        }

        Bitmap dbBitmap;
        Graphics dbGraphics;
        private void OSG_NumericDisplay_Paint(object sender, PaintEventArgs e)
        {
            /*
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            */

            //dbGraphics = Graphics.FromImage(dbBitmap);
            //Graphics g=e.Graphics;            DrawValue(ref g);
            DrawValue(e.Graphics);
            //e.Graphics.DrawImage(dbBitmap, 0, 0);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            DrawValue(e.Graphics);
        }




        private void OSG_NumericDisplay_Resize(object sender, EventArgs e)
        {
            RecalculateSize();
            Invalidate();
        }
    }
}
