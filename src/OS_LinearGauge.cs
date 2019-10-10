using System;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;



namespace OSGaugesLib
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

    [DefaultProperty("Value")]
    public partial class OS_LinearGauge : Panel
    {


        #region property Maximum : int
        private int _Maximum=10;
        [Category("LinearGauge")]
        public int Maximum
        {
            get { return _Maximum; }
            set { _Maximum = value; }
        }
        #endregion

        #region property Minimum : int
        private int _Minimum=0;
        [Category("LinearGauge")]
        public int Minimum
        {
            get { return _Minimum; }
            set { _Minimum = value; }
        }
        #endregion

        #region property Value : int
        private float _Value = 0f;
        [Category("LinearGauge")]
        
        public float Value
        {
            get { return _Value; }
            set { _Value = value; Invalidate(); }
        }
        #endregion


        #region property TickFrequency : int
        private int _TickFrequency=1;
        [Category("LinearGauge")]
        public int TickFrequency
        {
            get { return _TickFrequency; }
            set { _TickFrequency = value; Invalidate(); }
        }
        #endregion


        #region property TickAlternateFrequency : int
        private int _TickAlternateFrequency = 5;
        [Category("LinearGauge")]
        public int TickAlternateFrequency 
        {
            get { return _TickAlternateFrequency; }
            set { _TickAlternateFrequency = value; Invalidate(); }
        }
        #endregion

        #region property TickLineLength : int
        private int _TickLineLength=5;
        [Category("LinearGauge")]
        public int TickLineLength
        {
            get { return _TickLineLength; }
            set { _TickLineLength = value; Invalidate(); }
        }
        #endregion

        #region property TickAlternateLineLength : int
        private int _TickAlternateLineLength=10;
        [Category("LinearGauge")]
        public int TickAlternateLineLength
        {
            get { return _TickAlternateLineLength; }
            set { _TickAlternateLineLength = value; Invalidate(); }
        }
        #endregion


        #region property TickStartBias : int
        private int _TickStartBias=10;
        [Category("LinearGauge")]
        public int TickStartBias
        {
            get { return _TickStartBias; }
            set { _TickStartBias = value; Invalidate(); }
        }
        #endregion

        #region property TickEndBias : int
        private int _TickEndBias=10;
        [Category("LinearGauge")]
        public int TickEndBias
        {
            get { return _TickEndBias; }
            set { _TickEndBias = value; Invalidate(); }
        }
        #endregion


        #region property Vertical : bool
        private bool _Vertical=true;
        [Category("LinearGauge")]
        public bool Vertical
        {
            get { return _Vertical; }
            set { _Vertical = value; Invalidate(); }
        }
        #endregion


        #region property CenterColor : Color
        private Color _CenterColor=Color.White;
        [Category("LinearGauge")]
        public Color CenterColor
        {
            get { return _CenterColor; }
            set { _CenterColor = value; Invalidate(); }
        }
        #endregion

        #region property SurroundColor : Color
        private Color _SurroundColor=Color.Silver;
        [Category("LinearGauge")]
        public Color SurroundColor
        {
            get { return _SurroundColor; }
            set { _SurroundColor = value; Invalidate(); }
        }
        #endregion

        #region property Reverse : bool
        private bool _Reverse;
        [Category("LinearGauge")]
        public bool Reverse
        {
            get { return _Reverse; }
            set { _Reverse = value; Invalidate(); }
        }
        #endregion

        #region property TickSide : TickSide
        private TickSide _TickSide=TickSide.Both;
        [Category("LinearGauge")]
        public TickSide TickSide
        {
            get { return _TickSide; }
            set { _TickSide = value; Invalidate ( ); }
        }
        #endregion

        #region property IndicatorType : IndicatorType
        private IndicatorType _IndicatorType=IndicatorType.Line;
        [DefaultValue (IndicatorType.Line)]
        [Category("LinearGauge")]
        public IndicatorType IndicatorType
        {
            get { return _IndicatorType; }
            set { _IndicatorType = value; Invalidate ( ); }
        }
        #endregion

        #region property IndicatorFillColor : Color
        private Color _IndicatorFillColor=Color.Black;
        [Category("LinearGauge")]
        public Color IndicatorFillColor
        {
            get { return _IndicatorFillColor; }
            set { _IndicatorFillColor = value; Invalidate ( ); }
        }
        #endregion

        #region property IndicatorFillAlpha : byte
        private byte _IndicatorFillAlpha=255;
        [Category ("LinearGauge")]
        [DefaultValue((byte)255)]
        public byte IndicatorFillAlpha
        {
            get { return _IndicatorFillAlpha; }
            set { _IndicatorFillAlpha = value; Invalidate ( ); }
        }
        #endregion
        
        #region property IndicatorWidth : int
        private int _IndicatorWidth=10;
        [Category ("LinearGauge")]
        [DefaultValue(10)]
        public int IndicatorWidth
        {
            get { return _IndicatorWidth; }
            set { _IndicatorWidth = value; Invalidate ( ); }
        }
        #endregion





        #region property Font : Font
        private Font _Font = new Font("serif", 10f);
        [Category("LinearGauge")]
        

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
                    Invalidate(); 
                    
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        private void DrawOrnaments(Graphics g)
        {
            
            RectangleF rf = new Rectangle (0, 0, Width, Height);
            RectangleF rf2 = rf;
            rf2.Height /= 2;
            LinearGradientBrush lgb = new LinearGradientBrush (rf2, _CenterColor, _SurroundColor, 270f);
            lgb.WrapMode = WrapMode.TileFlipXY;
            g.FillRectangle (lgb, rf);



        }

        private void DrawTicks(Graphics g)
        {
            //TickAlternateFrequency 

            int fVal,sVal, inc;
            if (Reverse)
            {
                fVal = _Maximum;
                sVal = _Minimum;
                inc = -_TickFrequency;
            }
            else
            {
                fVal = _Minimum;
                sVal = _Maximum;
                inc = _TickFrequency;
                
            }


            //int i = fVal;
            //while (Math.Sign(i - (sVal + inc)) * inc < 0)
            //{

            for (int i = fVal; ; i+=inc)
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

                if (Reverse)
                {
                    y = _TickStartBias + (((Height - (_TickEndBias + _TickStartBias)) * i / (_Maximum - _Minimum)));
                }
                else
                {
                    y = Height - (_TickStartBias + ((Height - (_TickEndBias + _TickStartBias)) * i / (_Maximum - _Minimum)));
                }


                

                if (i % TickAlternateFrequency == 0)
                {
                    g.DrawLine(Pens.Blue, 0, y, _TickAlternateLineLength, y);
                    SizeF fs=g.MeasureString("0", Font);
                    g.DrawString(i.ToString(), Font, Brushes.Red, new Point(20, (int)(y-fs.Height/2)));
                }
                else
                {
                    g.DrawLine(Pens.Blue, 0, y, _TickLineLength, y);
                    
                }



                
            }
            
        }
        private void DrawValue(Graphics g)
        {

            
            int y;
            if (Reverse)
            {
                y = (int)(_TickStartBias + (((Height - (_TickEndBias + _TickStartBias)) * _Value / (_Maximum - _Minimum))));
            }
            else
            {
                y = (int)(Height - (_TickStartBias + ((Height - (_TickEndBias + _TickStartBias)) * _Value / (_Maximum - _Minimum))));
            }


            if ( _IndicatorType == IndicatorType.Fill )
            {
                Rectangle r;
                if ( Reverse )
                {
                    r = new Rectangle (Width / 2 - _IndicatorWidth / 2, _TickStartBias, _IndicatorWidth, y - _TickStartBias);
                }
                else
                {
                    r = new Rectangle (Width / 2 - _IndicatorWidth / 2, y, _IndicatorWidth, Height - y - _TickStartBias);
                }


                g.FillRectangle (new SolidBrush (Color.FromArgb (_IndicatorFillAlpha, _IndicatorFillColor)), r);

            }
            else
                if ( _IndicatorType == IndicatorType.Line )
                {
                    g.DrawLine (Pens.Red, _TickAlternateLineLength + 5, y, Width, y);
                }



        }







        public OS_LinearGauge()
        {

            InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            

        }

        public OS_LinearGauge(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);
            
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            DrawOrnaments (e.Graphics);
            DrawTicks(e.Graphics);
            DrawValue(e.Graphics);
            
        }
    }
}
