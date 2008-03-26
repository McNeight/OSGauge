using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;



namespace OSGaugesLib
{
    public partial class OS_Led : OS_DoubleBuffered
    {


        public OS_Led()
        {

            InitializeComponent();
            this.BackColor = Color.Transparent;
        }

        public OS_Led(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.BackColor = Color.Transparent;
        }


        private int _radius=0;

        #region property LedSize : int
        private int _LedSize = 16;
        public int LedSize
        {
            get { return _LedSize; }
            set
            {
                
                _LedSize = value;
                if(_LedSize<3)
                    _LedSize=16;

                OS_Led_Resize(null, null);
                _radius=(_LedSize/2)-2;
                Invalidate();
            }
        }
        #endregion

        #region property LedBorderWidth : int
        private int _LedBorderWidth=1;
        public int LedBorderWidth
        {
            get { return _LedBorderWidth; }
            set { _LedBorderWidth = value; }
        }
        #endregion













        private void OS_Led_Resize(object sender, EventArgs e)
        {

            this.Height = this.Width;
            this._LedSize = this.Width;
            Invalidate();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            DrawLed(e.Graphics);


        }


        private void DrawLed(Graphics g)
        {


            //g.DrawEllipse(Pens.Red, Width / 2 - _radius, Width / 2 - _radius, 2*_radius, 2*_radius);



            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(-15, -15, _LedSize + 30, _LedSize + 30);

            PathGradientBrush pgb = new PathGradientBrush(gp);
            ColorBlend _ColorBlend = new ColorBlend(3);
            _ColorBlend.Colors = new Color[] { Color.Transparent, Color.Transparent, Color.Red };
            _ColorBlend.Positions = new float[] { 0f, .3f, 1f };
            
            pgb.CenterColor = Color.Red;
            pgb.InterpolationColors = _ColorBlend;



            //g.FillEllipse(Brushes.Red, 1, 1, _LedSize - 3, _LedSize - 3);
            //g.DrawEllipse(Pens.RosyBrown, 1, 1, _LedSize - 3, _LedSize - 3);

            //Parent.CreateGraphics().FillEllipse(Brushes.Red, Left-5, Top-5, _LedSize +10, _LedSize +10);
            g.FillPath(pgb, gp);

            GraphicsPath gp2 = gp;
            Matrix m = new Matrix();
            m.Translate(Left, Top);
            gp2.Transform(m);


            PathGradientBrush pgb2 = new PathGradientBrush(gp2);

            pgb2.CenterColor = Color.Red;
            pgb2.InterpolationColors = _ColorBlend;
            
            
            Parent.CreateGraphics().FillPath(pgb2, gp2);
            
            //Parent.CreateGraphics().FillPath(pgb, gp);

        }


    }
}
