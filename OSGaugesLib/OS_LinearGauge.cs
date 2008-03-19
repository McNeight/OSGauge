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
    public partial class OS_LinearGauge : Panel
    {


        #region property Maximum : int
        private int _Maximum=10;
        public int Maximum
        {
            get { return _Maximum; }
            set { _Maximum = value; }
        }
        #endregion

        #region property Minimum : int
        private int _Minimum=0;
        public int Minimum
        {
            get { return _Minimum; }
            set { _Minimum = value; }
        }
        #endregion

        #region property Value : int
        private int _Value=0;
        public int Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        #endregion


        #region property TickFrequency : int
        private int _TickFrequency=1;
        public int TickFrequency
        {
            get { return _TickFrequency; }
            set { _TickFrequency = value; }
        }
        #endregion


        #region property TickAlternateFrequency : int
        private int _TickAlternateFrequency = 4;
        public int TickAlternateFrequency 
        {
            get { return _TickAlternateFrequency; }
            set { _TickAlternateFrequency = value; }
        }
        #endregion

        #region property TickLineLength : int
        private int _TickLineLength=5;
        public int TickLineLength
        {
            get { return _TickLineLength; }
            set { _TickLineLength = value; }
        }
        #endregion

        #region property TickAlternateLineLength : int
        private int _TickAlternateLineLength=10;
        public int TickAlternateLineLength
        {
            get { return _TickAlternateLineLength; }
            set { _TickAlternateLineLength = value; }
        }
        #endregion


        #region property TickStartBias : int
        private int _TickStartBias=10;
        public int TickStartBias
        {
            get { return _TickStartBias; }
            set { _TickStartBias = value; }
        }
        #endregion

        #region property TickEndBias : int
        private int _TickEndBias=10;
        public int TickEndBias
        {
            get { return _TickEndBias; }
            set { _TickEndBias = value; }
        }
        #endregion


        #region property Vertical : bool
        private bool _Vertical=true;
        public bool Vertical
        {
            get { return _Vertical; }
            set { _Vertical = value; }
        }
        #endregion



        private void DrawTicks(Graphics g)
        {
            //TickAlternateFrequency 
            
            for (int i = _Minimum; i < _Maximum; i++)
            {
                
                int y = _TickStartBias + ((Height - (_TickEndBias + _TickStartBias)) * i / (_Maximum - _Minimum));
                if (i % TickAlternateFrequency == 0)
                {
                    g.DrawLine(Pens.Blue, 0, y, _TickAlternateLineLength, y);
                }
                else
                {

                    g.DrawLine(Pens.Blue, 0, y, _TickLineLength, y);
                }

            }
            
        }
        private void DrawValue(Graphics g)
        {

            int _value = Height-1-(Height-1) * _Value / (_Maximum - _Minimum);
            g.DrawLine(Pens.Red, 0, _value, Width, _value);

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
            DrawTicks(e.Graphics);
            DrawValue(e.Graphics);
            
        }
    }
}
