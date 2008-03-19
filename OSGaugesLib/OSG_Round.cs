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
	public partial class OSG_Round : Panel
	{




		private int _radius;
		private Point _center;
		private PathGradientBrush _ellipseBrush;
		private PathGradientBrush _trimBrush;
		private Bitmap _ellipseBitmap;
		private	Bitmap _needleBitmap;
		private	Bitmap _needleBitmap2;
		private GraphicsPath _needlePath;
		private int _needleWidth;
		private int _needleHeight;

		private Pen _ticksPen = new Pen(Color.Black);
		private SolidBrush _needleBrush = new SolidBrush(Color.Black);

		private GraphicsPath _ellipsePath = null;
		private GraphicsPath _trimPath = null;
		private GraphicsPath _ticksPath = null;



		#region property TicksArcStart : float
		private float _TicksArcStart = 120;
		public float TicksArcStart
		{
			get { return _TicksArcStart; }
			set { _TicksArcStart = value; InitializeGDI(); }
		}
		#endregion

		#region property TicksArcEnd : float
		private float _TicksArcEnd = 380;
		public float TicksArcEnd
		{
			get { return _TicksArcEnd; }
			set { _TicksArcEnd = value; InitializeGDI(); }
		}
		#endregion

		#region property TickFrequency : int
		private int _TickFrequency = 10;
		public int TickFrequency
		{
			get { return _TickFrequency; }
			set { _TickFrequency = value; InitializeGDI(); }
		}
		#endregion

		#region property TickLineLength : int
		private int _TickLineLength = 10;
		public int TickLineLength
		{
			get { return _TickLineLength; }
			set
			{
				_TickLineLength = value;
				MinimumSize = new Size(2 * (_TickLineOffset + Math.Max(_TickAlternateLineLength, _TickLineLength)), 2 * (_TickLineOffset + Math.Max(_TickAlternateLineLength, _TickLineLength)));
				InitializeGDI();
			}
		}
		#endregion

		#region property TickAlternateLineLength : int
		private int _TickAlternateLineLength = 15;
		public int TickAlternateLineLength
		{
			get { return _TickAlternateLineLength; }
			set
			{
				_TickAlternateLineLength = value;
				MinimumSize = new Size(2 * (_TickLineOffset + Math.Max(_TickAlternateLineLength, _TickLineLength)), 2 * (_TickLineOffset + Math.Max(_TickAlternateLineLength, _TickLineLength)));
				InitializeGDI();
			}
		}
		#endregion
		
		#region property TickLineOffset : int
		private int _TickLineOffset = 5;
		public int TickLineOffset
		{
			get { return _TickLineOffset; }
			set
			{
				_TickLineOffset = value;
				MinimumSize = new Size(2 * (_TickLineOffset + Math.Max(_TickAlternateLineLength, _TickLineLength)), 2 * (_TickLineOffset + Math.Max(_TickAlternateLineLength, _TickLineLength)));
				InitializeGDI();

			}
		}
		#endregion

		#region property CenterColor : Color
		private Color _CenterColor = Color.GhostWhite;
		public Color CenterColor
		{
			get { return _CenterColor; }
			set { _CenterColor = value; InitializeGDI(); }
		}
		#endregion

		#region property SurroundColor : Color
		private Color _SurroundColor = Color.Gainsboro;
		public Color SurroundColor
		{
			get { return _SurroundColor; }
			set { _SurroundColor = value; InitializeGDI(); }
		}
		#endregion

		#region property TrimColor : Color
		private Color _TrimColor = Color.Black;
		public Color TrimColor
		{
			get { return _TrimColor; }
			set { _TrimColor = value; InitializeGDI(); }
		}
		#endregion

		#region property TrimWidth : int
		private int _TrimWidth = 6;
		public int TrimWidth
		{
			get { return _TrimWidth; }
			set { _TrimWidth = value; InitializeGDI(); }
		}
		#endregion

		#region property TickTextOffset : int
		private int _TickTextOffset = 10;
		public int TickTextOffset
		{
			get { return _TickTextOffset; }
			set { _TickTextOffset = value; InitializeGDI(); }
		}
		#endregion

		#region property DisplayTickText : bool
		private bool _DisplayTickText = true;
		public bool DisplayTickText
		{
			get { return _DisplayTickText; }
			set { _DisplayTickText = value; InitializeGDI(); }
		}
		#endregion

		#region property TicksColor : Color
		private Color _TicksColor = Color.Black;
		public Color TicksColor
		{
			get { return _TicksColor; }
			set { _TicksColor = value; _ticksPen = new Pen(_TicksColor, 1); InitializeGDI(); }
		}
		#endregion

		#region property NeedleColor : Color
		private Color _NeedleColor = Color.Black;
		public Color NeedleColor
		{
			get { return _NeedleColor; }
			set
			{
				_NeedleColor = value;

				_needleBrush = new SolidBrush(_NeedleColor);
				InitializeGDI();
			}
		}
		#endregion

		#region property NeedleLength : int
		private int _NeedleLength=0;
		public int NeedleLength
		{
			get { return _NeedleLength; }
			set { _NeedleLength = value; InitializeGDI(); }
		}
		#endregion

		#region property ShowNeedle : bool
		private bool _ShowNeedle = true;
		public bool ShowNeedle
		{
			get { return _ShowNeedle; }
			set { _ShowNeedle = value; InitializeGDI(); }
		}
		#endregion

		#region property StrokeNeedle : bool
		private bool _StrokeNeedle=true;
		public bool StrokeNeedle
		{
			get { return _StrokeNeedle; }
			set { _StrokeNeedle = value; InitializeGDI(); }
		}
		#endregion



		private void CreatePaths()
		{
			_ellipsePath = new GraphicsPath();
			_ellipsePath.AddEllipse(_center.X - _radius, _center.Y - _radius, 2 * _radius, 2 * _radius);

			_trimPath = new GraphicsPath();
			_trimPath.AddEllipse(_center.X - _radius + _TrimWidth / 2, _center.Y - _radius + _TrimWidth / 2, 2 * _radius - _TrimWidth, 2 * _radius - _TrimWidth);
			_trimPath.StartFigure();
			_trimPath.AddEllipse(_center.X - _radius - _TrimWidth / 2, _center.Y - _radius - _TrimWidth / 2, 2 * _radius + _TrimWidth, 2 * _radius + _TrimWidth);

			//create ticks
			_ticksPath = new GraphicsPath();
			float _tickStep = (_TicksArcEnd * (float)Math.PI / 180 - _TicksArcStart * (float)Math.PI / 180) / (_Maximum - _Minimum);
			float _radius0 = _radius - _TickLineOffset;
			float _radius2 = _radius0 - _TickLineLength;
			float _radius3 = _radius0 - _TickAlternateLineLength;
			float tick = _TicksArcStart * (float)Math.PI / 180;
			float _radiusFrom = _radius0;
			float _radiusTo = _radius2;

			for (int i = _Minimum; i <= _Maximum; i++)
			{
				if (i % _TickFrequency == 0)
				{
					_radiusTo = _radius3;
					if (_DisplayTickText)
					{
						GraphicsPath _ticksPath2 = new GraphicsPath();
						_ticksPath2.AddString(i.ToString(), this.Font.FontFamily, 0, this.Font.Size, new Point(0, 0), StringFormat.GenericDefault);
						_ticksPath2.Flatten();
						float _radiusTextOffset = Math.Max(_ticksPath2.GetBounds().Width, _ticksPath2.GetBounds().Height);
						PointF _textPosition = new PointF();
						_center = new Point(Width / 2, Height / 2);
						_textPosition.X = _center.X + (_radius3 - _TickTextOffset) * (float)Math.Cos(tick) - _ticksPath2.GetBounds().Width + _ticksPath2.GetBounds().Left;
						_textPosition.Y = _center.Y + (_radius3 - _TickTextOffset) * (float)Math.Sin(tick) - _ticksPath2.GetBounds().Height - 0.5f * _ticksPath2.GetBounds().Top;
						Matrix m = new Matrix();
						m.Translate(_textPosition.X, _textPosition.Y);
						_ticksPath2.Transform(m);
						_ticksPath.AddPath(_ticksPath2, false);
					}
				}
				else
				{
					_radiusTo = _radius2;
				}
				_ticksPath.StartFigure();
				_ticksPath.AddLine(_center.X + _radiusFrom * (float)Math.Cos(tick), _center.Y + _radiusFrom * (float)Math.Sin(tick), _center.X + _radiusTo * (float)Math.Cos(tick), _center.Y + _radiusTo * (float)Math.Sin(tick));
				//Console.WriteLine("{0} - {1} - {2} - {3}", _TicksArcStart, _TicksArcEnd, _tickStep, tick);
				tick += _tickStep;
			}
			_ticksPath.Flatten();
		}

		private void CreateBrushes()
		{
			ColorBlend _ColorBlend = new ColorBlend(3);
			_ColorBlend.Colors = new Color[] { _SurroundColor, _SurroundColor, _CenterColor };
			_ColorBlend.Positions = new float[] { 0f, .3f, 1f };
			_ellipseBrush = new PathGradientBrush(_ellipsePath);
			_ellipseBrush.CenterColor = _CenterColor;
			_ellipseBrush.InterpolationColors = _ColorBlend;


			_ellipseBitmap = new Bitmap(Width, Height);

			_trimBrush = new PathGradientBrush(_trimPath);
			_trimBrush.WrapMode = WrapMode.Clamp;
			_trimBrush.CenterColor = Color.White;


			_ColorBlend.Colors = new Color[] { _TrimColor, Color.White, Color.White };
			_ColorBlend.Positions = new float[] { 0f, .05f, 1f };
			_trimBrush.InterpolationColors = _ColorBlend;

		}






		public void InitializeGDI()
		{
			_radius = -5 + Math.Min(Width, Height) / 2;
			_center = new Point(Width / 2, Height / 2);
			


			CreatePaths();
			CreateBrushes();

			if (_ShowNeedle)
			{
				CreateNeedle();
			}
			Invalidate();
		}


		public OSG_Round()
		{

			InitializeComponent();
			TickAlternateLineLength = _TickAlternateLineLength;
			TicksColor = _TicksColor;
			NeedleColor = _NeedleColor;
			_ShowNeedle = (_NeedleLength > 0);

			InitializeGDI();
		}

		public OSG_Round(IContainer container):this()
		{
			container.Add(this);
			/*
			InitializeComponent();
			TickAlternateLineLength = _TickAlternateLineLength;
			TicksColor = _TicksColor;
			NeedleColor = _NeedleColor;
			_ShowNeedle = !(_NeedleLength > 0);
			InitializeGDI();*/
		}





		private void OSG_Round_Paint(object sender, PaintEventArgs e)
		{


			e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			//e.Graphics.DrawString(_radius + " " + _Minimum + " " + _Maximum + " " + _Value, new Font("Arial", 10f), Brushes.BlueViolet, 10, 10);

			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			PaintBackground(e.Graphics);
			PaintTicks(e.Graphics);
			if (_ShowNeedle)
			{
				PaintNeedle(e.Graphics);
			}
		}



		private void PaintBackground(Graphics g)
		{
			//if (_ellipseBitmap == null)				return;
			Graphics g2 = Graphics.FromImage(_ellipseBitmap);
			g2.SmoothingMode = SmoothingMode.AntiAlias;
			g2.FillPath(_ellipseBrush, _ellipsePath);
			g2.FillPath(_trimBrush, _trimPath);
			g.DrawImage(_ellipseBitmap, 0, 0);
		}





		private void PaintTicks(Graphics g)
		{
			Graphics g2 = Graphics.FromImage(_ellipseBitmap);
			g2.SmoothingMode = SmoothingMode.AntiAlias;



			g2.DrawPath(_ticksPen, _ticksPath);
			g.DrawImage(_ellipseBitmap, 0, 0);
		}



		private void CreateNeedle()
		{
			//_needleWidth = 2 * _radius - 50;
			_needleWidth = 2*_NeedleLength;
			_needleHeight = 2*((int)_radius/10); //has to be an even number!


			if(_needleWidth==0 || _needleHeight==0)
				return;
			_needleBitmap = new Bitmap(_needleWidth, _needleHeight );

			Graphics g2 = Graphics.FromImage(_needleBitmap);
			g2.SmoothingMode = SmoothingMode.AntiAlias;

			_needlePath = new GraphicsPath();
			//_needlePath.AddEllipse(_needleWidth / 2 - _needleHeight / 2, 0, _needleHeight, _needleHeight);

			//_needlePath.AddArc(_needleWidth / 2 - _needleHeight / 2, 0, _needleHeight, _needleHeight, 10, 340);
			_needlePath.AddArc(_needleBitmap.Width / 2 - _needleBitmap.Height / 2+1,1, _needleBitmap.Height-2, _needleBitmap.Height-2, 10, 340);

			//g2.FillEllipse(_needleBrush, _needleWidth / 2 - _needleHeight / 2, 0, _needleHeight, _needleHeight);
			//_needlePath.AddEllipse(_needleWidth / 2 - _needleHeight / 2, 0, _needleHeight, _needleHeight);


			_needlePath.FillMode = FillMode.Winding;
			//_needlePath.StartFigure();
			_needlePath.AddLine(_needleBitmap.Height/2+_needleBitmap.Width / 2-1, _needleBitmap.Height / 2 - 3, _needleBitmap.Width - 10, _needleBitmap.Height / 2 - 3);
			_needlePath.AddLine(_needleBitmap.Width - 10, _needleBitmap.Height / 2 - 3, _needleBitmap.Width, _needleBitmap.Height / 2);
			_needlePath.AddLine(_needleBitmap.Width, _needleBitmap.Height / 2, _needleBitmap.Width - 10, _needleBitmap.Height / 2 + 3);
			_needlePath.AddLine(_needleBitmap.Width - 10, _needleBitmap.Height / 2 + 3, _needleBitmap.Height / 2 + _needleBitmap.Width / 2-1, _needleBitmap.Height / 2 + 3);
			_needlePath.Flatten();
			//_needlePath.CloseFigure();

			g2.FillPath(_needleBrush, _needlePath);

			if(_StrokeNeedle)
				g2.DrawPath(_ticksPen, _needlePath);
			_needlePath.Reset();

			_needlePath.AddLine(6 * _needleBitmap.Width / 10, _needleBitmap.Height / 2, 9 * _needleBitmap.Width / 10, _needleBitmap.Height / 2);
			g2.DrawPath(_ticksPen, _needlePath);
			// _needlePath.AddLine(_needleWidth / 2, _needleHeight / 2, _needleWidth, _needleHeight / 2);


		}


		private void PaintNeedle(Graphics g)
		{

			Graphics g3=null;
			_needleBitmap2 = new Bitmap(Width, Height);

			g3= Graphics.FromImage(_needleBitmap2);
			g3.SmoothingMode = SmoothingMode.AntiAlias;
			float _angle = _TicksArcStart + (Value-Minimum) * (_TicksArcEnd - _TicksArcStart) / (_Maximum - _Minimum);
			g3.TranslateTransform(_center.X, _center.Y);
			g3.RotateTransform(_angle);
			
			g3.DrawImage(_needleBitmap, -_needleBitmap.Width/ 2, -_needleBitmap.Height/ 2);
			g.SmoothingMode = SmoothingMode.AntiAlias;

			Bitmap _sumBitmap = new Bitmap(Width, Height);
			Graphics _sumGraphics=Graphics.FromImage(_sumBitmap);
			_sumGraphics.SmoothingMode = SmoothingMode.AntiAlias;
			_sumGraphics.DrawImage(_ellipseBitmap, 0, 0);
			_sumGraphics.DrawImage(_needleBitmap2, 0, 0);
			g.DrawImage(_sumBitmap,0,0);




		}



		
		
		
		
		



		private int _Minimum = 0;
		public int Minimum
		{
			get
			{
				return _Minimum;
			}
			set
			{
				_Minimum = value;
				InitializeGDI();
			}
		}

		private int _Maximum = 100;
		public int Maximum
		{
			get
			{
				return _Maximum;
			}
			set
			{
				_Maximum = value;
				InitializeGDI();
			}
		}

		private int _Value;
		public int Value
		{
			get
			{
				return _Value;
			}
			set
			{
				_Value = value;
				if (_Value < _Minimum)
					_Value = _Minimum;
				if (_Value > _Maximum)
					_Value = _Maximum;
				if(_ShowNeedle)
				PaintNeedle(this.CreateGraphics());
			}
		}



		private void OSG_Round_Resize(object sender, EventArgs e)
		{
			InitializeGDI();
		}

	}
}
