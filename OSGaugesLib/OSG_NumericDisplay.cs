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
        private int _NrOfDigits;
        public int NrOfDigits
        {
            get { return _NrOfDigits; }
            set { _NrOfDigits = value; RecalculateSize(); }
        }
        #endregion

        #region property Decimals : int
        private int _Decimals;
        public int Decimals
        {
            get { return _Decimals; }
            set { _Decimals = value; RecalculateSize(); }
        }
        #endregion



        #region property Font : Font
        private Font _Font = new Font("serif", 10f);
        public new Font Font
        {
            get { return _Font; }
            set { try { _Font = value; RecalculateSize(); } catch (Exception) { } }
        }
        #endregion


        #region property Value : float
        private float _Value;
        public float Value
        {
            get { return _Value; }
            set { _Value = value; }
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

            Width = (int)_newWidth;
            Height = (int)_newHeight;


        }



        private Dictionary<string, Bitmap> _digits = new Dictionary<string, Bitmap>();


        private void PreDrawDigits()
        {
            RecalculateSize();
            string chr = "";
            Graphics g;
            for (int i = 0; i < 10; i++)
            {
                chr = i.ToString();

                _digits[chr] = new Bitmap((int)_digitSize.Width, (int)_digitSize.Height);
                g = Graphics.FromImage(_digits[chr]);
                g.DrawString(chr, Font, Brushes.Black, 0, 0);
            }

            chr = ".";
            _digits[chr] = new Bitmap((int)_digitSize.Width, (int)_digitSize.Height);
            g = Graphics.FromImage(_digits[chr]);
            g.DrawString(chr, Font, Brushes.Black, 0, 0);

            chr = "-";
            _digits[chr] = new Bitmap((int)_digitSize.Width, (int)_digitSize.Height);
            g = Graphics.FromImage(_digits[chr]);
            g.DrawString(chr, Font, Brushes.Black, 0, 0);

        }



        private void DrawValue(Graphics g)
        {
            RecalculateSize();


            int x = Convert.ToInt32(((_Value * (float)Math.Pow(10, _Decimals))) / Math.Pow(10, _Decimals));

            //char[] _digitsChars = ((Convert.ToInt32( _Value*(float) Math.Pow(10, _Decimals)) ) / Math.Pow(10, _Decimals)).ToString().ToCharArray();
            string _digitsString = ((Convert.ToInt32(_Value * (float)Math.Pow(10, _Decimals))) / Math.Pow(10, _Decimals)).ToString();


            _digitsString = String.Format("{0:0." + new string('0', _Decimals) + "}", 44.232344223f);
            Console.WriteLine("_digitsString :  {0} ", _digitsString);


            char[] _digitsChars = _digitsString.ToCharArray();

            int _pad = _Decimals;
            if (_Decimals > 0)
            {
                _pad += 1;
            }
            _pad += _NrOfDigits;
            _pad -= _digitsChars.Length;


            //Console.WriteLine(_pad);


            for (int i = _pad; i < _pad + _digitsChars.Length; i++)
            {
                int _left = (int)(i * _digitSize.Width);
                g.DrawImageUnscaled(_digits[_digitsChars[i - _pad].ToString()], _left, 0);
            }
        }









        public OSG_NumericDisplay()
        {
            InitializeComponent();
            PreDrawDigits();
        }

        public OSG_NumericDisplay(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            PreDrawDigits();
        }

        private void OSG_NumericDisplay_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawRectangle(Pens.Red, 0, 0, Width - 1, Height - 1);
            DrawValue(e.Graphics);

        }

        private void OSG_NumericDisplay_Resize(object sender, EventArgs e)
        {
            RecalculateSize();
            Invalidate();
        }
    }
}
