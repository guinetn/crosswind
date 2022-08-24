using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace ng.Controls.Bar
{

    #region Enums
	public enum CornerStyles
	{
		Rounded,
		Square
	}
	public enum Orientations
	{
		Vertical,
        Horizontal  
	}
    public enum Mode
    {
        ScrollBar,
        ProgressBar
    }
	public enum Poles
	{
		Left,
		Right,
		Top,
		Bottom
	}

	#endregion

	/// <summary>
	/// Description Résumé de ngScrollBar.
	/// </summary>
       // [Serializable]
       [Serializable]
	public class ScrollProgrBar : UserControl
	{
		/// <summary>
		/// Variable requise par le concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

                private double barMinimum = 0.0;
		        private double barMaximum = 100.0;
                private double plageValeurs = 100.0;
           
                private double value = 0.0; // de  barMinimum  à  barMaximum

                private Color barborderColor = Color.Black;
                private int borderWidth = 0;
                private Color barColor = Color.White;

                // Width trackBar (scrollbar mode)
                private int trackSize=10;
                private Color trackColor = Color.Red;
                private Color trackborderColor= Color.Black;
                           
                private Rectangle trackRect = Rectangle.Empty;

                private CornerStyles cornerStyle=CornerStyles.Square;
		        private Orientations barOrientation = Orientations.Horizontal;
                private Mode barMode = Mode.ScrollBar;
                private Poles maxSide=Poles.Right;

                public double grow; // positive if increase, negative if decrease
                private double previousValue;

                #region PTES

                 /// <summary>
		/// Valeur du scrollbar
		/// </summary>
		[Description( "Bar Value")]
		[Category( "ColorTrackBar" )]
		public double Value
		{
			set
                        {
                          if (value < barMinimum)
                             this.value = barMinimum;
                          else if (Maximum < value)
                             this.value = barMaximum ;
                          else
                            this.value = value;

                          // Compute if increase or decrease
                          grow = value - previousValue;
                          previousValue = value;

                          OnValueChanged();
                        }
                        
			get
                        {
                         /* double z = Minimum;
                          z += this.value * plageValeurs/100.0;
                          z = Math.Round(z);*/
                          return value;
                        }
		}

                public void setValue(double valeur)
                {
                  this.value = valeur; //100.0*(valeur-Minimum)/plageValeurs;
                  this.Invalidate();
                }

                /// <summary>
		/// Background Bar Border Color
		/// </summary>
		[Description( "Bar borderWidth")]
		[Category( "ColorTrackBar" )]
		public int BorderWidth
		{
			set{this.borderWidth = value; this.Invalidate(); }
			get{return this.borderWidth; }
		}

                /// <summary>
		/// Background Bar Border Color
		/// </summary>
		[Description( "Bar border color")]
		[Category( "ColorTrackBar" )]
		public Color BarBorderColor
		{
			set{this.barborderColor=value;this.Invalidate();}
			get{return this.barborderColor;}
		}

                /// <summary>
		/// Background bar Color
		/// </summary>
		[Description( "Bar color")]
		[Category( "ColorTrackBar" )]
		public Color BarColor
		{
			set{this.barColor=value;this.Invalidate();}
			get{return this.barColor;}
		}

                /// <summary>
		/// Tracker bar COlor
		/// </summary>
		[Description( "Tracker color")]
		[Category( "ColorTrackBar" )]
		public Color TrackerColor
		{
			set{this.trackColor=value;this.Invalidate();}
			get{return this.trackColor;}
		}

                /// <summary>
		/// Set Trakcer border color
		/// </summary>
		[Description( "Tracker border color")]
		[Category( "ColorTrackBar" )]
		public Color TrackerBorderColor
		{
			set{this.trackborderColor=value;this.Invalidate();}
			get{return this.trackborderColor;}
		}

                /// <summary>
		/// Set Trackerbar minimum value
		/// </summary>
		[Description( "Set Minimum value of the Track bar")]
		[Category( "ColorTrackBar" )]
	   	[RefreshProperties(RefreshProperties.All)]
		public double Minimum
		{
			set
			{
				if (barMaximum < value)
				{
					throw new ArgumentException("'"+value+"' is not a valid value for 'Mimimum'.\n"+
						"'Minimum' must be less than 'Maximum'.");
				}
				this.barMinimum = value;
				if(this.value < this.barMinimum)
					this.value = value;
                                plageValeurs = Maximum - Minimum;
			      	this.Invalidate();
			}
			get{return this.barMinimum;}
		}

		/// <summary>
		/// Set TrackerBar maximum value
		/// </summary>
		[Description( "Set Maximum value of the Track bar")]
		[Category( "ColorTrackBar" )]
	 	[RefreshProperties(RefreshProperties.All)]
		public double Maximum
		{
			set
			{
				if (value < barMinimum)
				{
					throw new ArgumentException("'"+value+"' is not a valid value for 'Maximum'.\n"+
						"'Maximum' must be greater than 'Minimum'.");
				}
                                this.barMaximum = value;

				if(value < this.barMaximum)
					this.value = value;

                                plageValeurs = Maximum - Minimum;
			      	this.Invalidate();
			}
			get{return this.barMaximum;}
		}

                /// <summary>
		/// Set the poles of the trackbar
		/// </summary>
		[Description( "Select the side of the control to represent the maximum range value")]
		[Category( "ColorTrackBar" )]
		public Poles MaximumValueSide
		{
			get
			{
				return maxSide;
			}
			set
			{
				switch(barOrientation)
				{
					case Orientations.Horizontal:
						if(value==Poles.Top || value==Poles.Bottom)
						{
							throw new ArgumentException ("Since your Orientation is set to Horizontal, you can only select"+
								" Left or Right for this property");
						}
						break;
					case Orientations.Vertical:
						if(value==Poles.Left || value==Poles.Right)
						{
							throw new ArgumentException ("Since your Orientation is set to Vertical, you can only select"+
								" Top or Bottom for this property");
						}
						break;
				}
				maxSide=value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Set tracker bar orientation
		/// </summary>
		[Description( "Set whether the bar will be Veirtically or Horizontally oriented")]
		[Category( "ColorTrackBar" )]
		[RefreshProperties(RefreshProperties.All)]
		public Orientations BarOrientation
		{
			set
			{
				//check old value//set new size
				if(this.barOrientation==Orientations.Horizontal)
					base.Size=new Size(base.Size.Height,base.Size.Width);
				else
					base.Size=new Size(base.Size.Height,base.Size.Width);
				this.barOrientation = value;
				if(value==Orientations.Vertical &&
					(this.maxSide!=Poles.Bottom && this.maxSide!=Poles.Top))
					this.MaximumValueSide=Poles.Bottom;
				if(value==Orientations.Horizontal &&
					(this.maxSide!=Poles.Left && this.maxSide!=Poles.Right))
					this.MaximumValueSide=Poles.Right;
				this.Invalidate();
			}
			get{return this.barOrientation;}
		}

        /// <summary>
        /// Set tracker bar orientation
        /// </summary>
        [Description("Scrollbar or ProgressBar")]
        [Category("ColorTrackBar")]
        [RefreshProperties(RefreshProperties.All)]
        public Mode BarMode
        {
            set
            {
                barMode = value;
                this.Invalidate();
            }
            get { return this.barMode; }
        }


                /// <summary>
		/// Set control corner style
		/// </summary>
		[Description( "Set the shape of the control's corners")]
		[Category( "ColorTrackBar" )]
		[RefreshProperties(RefreshProperties.All)]
		public CornerStyles ControlCornerStyle
		{
			set
			{
				switch(barOrientation)
				{
					case Orientations.Horizontal:
						if(value==CornerStyles.Rounded)
						{
							if(this.Width<this.Height)
								this.Width=this.Height;
							this.trackSize=this.Height;
						}
						break;
					case Orientations.Vertical:
						if(value==CornerStyles.Rounded)
						{
							if(this.Height<this.Width)
								this.Height=this.Width;
							this.trackSize=this.Width;
						}
						break;
					default:
						break;
				}
				this.cornerStyle=value;
				this.Invalidate();
			}
			get{return this.cornerStyle;}
		}


                #endregion

		public ScrollProgrBar() : base()
		{
		    // Cet appel est requis par le concepteur Windows.Forms.
		    InitializeComponent();

                    //set initla size
		    // base.Size = new Size(150,25);
	            // Activates double buffering
                    SetStyle(ControlStyles.UserPaint, true);
                    SetStyle(ControlStyles.AllPaintingInWmPaint, true);                   
                    SetStyle(ControlStyles.ResizeRedraw, true);
                    SetStyle(ControlStyles.SupportsTransparentBackColor,true);
                    plageValeurs = Maximum - Minimum;
		   //set cursor
		   this.Cursor=Cursors.Hand;
           previousValue = value;
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

       #region Event Delegates

	   public delegate void ValueChangedEventHandler(object sender);

                /// <summary>
		/// Fires when track bar posotion has changed
		/// </summary>
		[Description("Event fires when the Value property changes")]
		[Category("Action")]
		public event ValueChangedEventHandler ValueChanged;

        protected virtual void OnValueChanged()
		{
			try
			{
				if(ValueChanged!=null)
					ValueChanged(this);
			}
			catch(Exception Err)
			{
				MessageBox.Show("OnValueChanged Exception: " + Err.Message);
			}

                        this.Invalidate();
		}


        #endregion

		#region Code généré par le concepteur de composant
		/// <summary>
		/// Méthode requise pour la gestion du concepteur  - ne pas modifier
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
                {
                    this.SuspendLayout();
                    // 
                    // ScrollProgrBar
                    // 
                    this.DoubleBuffered = true;
                    this.Name = "ScrollProgrBar";
                    this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ngScrollBar_MouseDown);
                    this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ngScrollBar_MouseMove);
                    this.Paint += new System.Windows.Forms.PaintEventHandler(this.ngScrollBar_Paint);
                    this.ResumeLayout(false);

                }
		#endregion

        #region Actions Souris

         private void ngScrollBar_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
         {
           mouseAction(e);
         }

         private void ngScrollBar_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
         {
           mouseAction(e);
         }

         private void mouseAction( MouseEventArgs e )
         {
          if (BarMode == Mode.ProgressBar) 
             return;

          RectangleF r = ClientRectangle;

          if (barOrientation == Orientations.Horizontal)
          {
              int xPos = e.X;

              if (e.X < 0)
                  xPos = 0;
              else if (Width < e.X)
                  xPos = ClientRectangle.Width - 1;
              else if (e.Button != MouseButtons.Left)
                  return;

              value = xPos / (r.Width - 1.0);
          }
          else 
          {
              int yPos = e.Y;

              if (e.Y < 0)
                  yPos = 0;
              else if (Height < e.Y)
                  yPos = ClientRectangle.Height - 1;
              else if (e.Button != MouseButtons.Left)
                  return;

              value = yPos / (r.Height - 1.0);
          }

          Value = barMinimum + value*plageValeurs;
         }

        #endregion

       #region Drawing GDI+

        private void ngScrollBar_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Buffer to avoid flicker !
            BufferedGraphics bufferG = BufferedGraphicsManager.Current.Allocate(e.Graphics, this.DisplayRectangle);
            Graphics g = bufferG.Graphics;

               try
                {
                        //get parent color
                        Color ParentColor = Color.FromName("Control");
                        if(this.Parent!=null)
                                ParentColor = this.Parent.BackColor;

                        //double v = Minimum + this.value*plageValeurs/100.0;
                        double v = value; // Math.Round(value);

                        switch (barOrientation)
                        {
                                case Orientations.Horizontal:

                                    int TrackX = 0;
                                    if (barMode==Mode.ScrollBar)
                                    {
                                                // for scrollbar mode 
                                    
                                                    TrackX = (int)(((v-Minimum)*(ClientRectangle.Width-trackSize)) / (Maximum-Minimum));
                                                    //don't go past the borders
                                                    if(TrackX==0)
                                                            TrackX++;
                                                    if(TrackX+trackSize==ClientRectangle.Width)
                                                            TrackX--;
                                                    if(maxSide!=Poles.Right)
                                                            TrackX = (TrackX-ClientRectangle.Width+trackSize)*-1;                                                                     
                                                     trackRect = new Rectangle(TrackX, 1, trackSize, ClientRectangle.Height-2);
                                                     break;
                                    }
                                    else
                                    {
                                        // For progressbar mode
                                        TrackX = (int)(((v - Minimum) * (ClientRectangle.Width)) / (Maximum - Minimum));
                                        //don't go past the borders
                                                    trackRect = new Rectangle(0, 1, TrackX, ClientRectangle.Height - 2);
                                                break;
                                    }
                                default:  // Orientations.Vertical:
                                     int TrackY = 0;
                                     if (barMode == Mode.ScrollBar)
                                     {
                                         // for scrollbar mode 
                                         TrackY = (int)(((v - Minimum) * (ClientRectangle.Height - trackSize)) / (Maximum - Minimum));
                                         //don't go past the borders
                                         if (TrackY == 0)
                                             TrackY++;
                                         if (TrackY + ClientRectangle.Width == ClientRectangle.Height)
                                             TrackY--;
                                         if (maxSide != Poles.Bottom)
                                             TrackY = (TrackY - ClientRectangle.Height + trackSize) * -1;
                                         trackRect = new Rectangle(1, TrackY, ClientRectangle.Width - 2, trackSize);
                                         break;
                                     }
                                     else 
                                     {
                                         // For progressbar mode
                                         TrackY = (TrackY - ClientRectangle.Height) * -1;
                                         //don't go past the borders
                                         trackRect = new Rectangle(0, 1, TrackY, ClientRectangle.Height - 2);
                                         break;
                                     }

                        }

                        Region TrackRegion=null;
                        Region BarRegion=null;
                        switch (cornerStyle)
                        {
                                case CornerStyles.Square:
                                        PaintRectangle(ClientRectangle,barColor,barborderColor,g);
                                        BarRegion=new Region(ClientRectangle);
                                        TrackRegion = new Region(trackRect);
                                        PaintRectangle(trackRect, trackColor, trackborderColor, g);
                                        break;

                                case CornerStyles.Rounded:
                                        //first draw bar
                                        GraphicsPath BarPath=DrawRoundedCorners(ClientRectangle,barborderColor,g);
                                        BarRegion = new Region(BarPath);
                                        PaintPath(BarPath,barColor,g);

                                        {
                                                if(barOrientation==Orientations.Horizontal)
                                                {
                                                        trackRect = new Rectangle(trackRect.Location,
                                                                    new Size(ClientRectangle.Height,ClientRectangle.Height-2));
                                                }
                                                else
                                                {
                                                        trackRect = new Rectangle(trackRect.Location,
                                                                    new Size(ClientRectangle.Width-2,ClientRectangle.Width));
                                                }

                                        }
                                    
                                        // Draw Tracker
                                        GraphicsPath TrackPath = DrawRoundedCorners(trackRect,trackborderColor,g);
                                        TrackRegion = new Region(TrackPath);
                                        PaintPath(TrackPath,trackColor,g);
                                        break;
                                default:
                                        break;
                        }

                }
                catch(Exception Err)
                {
                        throw new Exception("ngbar DrawBackGround Error: "+Err.Message);
                }
                finally
                {
                }

                // Render control
                bufferG.Render();
        }

        protected void PaintRectangle(Rectangle Rect,Color RectColor,Color RectBorderColor,Graphics g)
        {
                //draw rectangle
                Pen LinePen = new Pen(RectBorderColor, borderWidth);
                g.DrawRectangle(LinePen,new Rectangle(Rect.X,Rect.Y,Rect.Width-1,Rect.Height-1));
                LinePen.Dispose();
                Rect = new Rectangle(Rect.X+1,Rect.Y+1,Rect.Width-2,Rect.Height-2);
                //
                // Fill background
                //
                SolidBrush bgBrush = new SolidBrush(ControlPaint.Dark(RectColor));
                g.FillRectangle(bgBrush, Rect);
                bgBrush.Dispose();

                //
                // The gradient brush
                //
                LinearGradientBrush brush;
                Rectangle FirstRect, SecondRect;
                switch(barOrientation)
                {
                        case Orientations.Horizontal:
                                FirstRect= new Rectangle(Rect.X,Rect.Y, Rect.Width, Rect.Height / 2);
                                SecondRect=new Rectangle(Rect.X, Rect.Height / 2, Rect.Width, Rect.Height / 2);
                                // Paint upper half
                                brush = new LinearGradientBrush(
                                        new Point(FirstRect.Width/2,FirstRect.Top),
                                        new Point(FirstRect.Width/2,FirstRect.Bottom),
                                        ControlPaint.Dark(RectColor),
                                        RectColor);
                                g.FillRectangle(brush, FirstRect);
                                brush.Dispose();
                                // Paint lower half
                                // (SecondRect.Y - 1 because there would be a dark line in the middle of the bar)
                                brush = new LinearGradientBrush(
                                        new Point(SecondRect.Width/2, SecondRect.Top-1),
                                        new Point(SecondRect.Width/2, SecondRect.Bottom), 
                                        RectColor,
                                        ControlPaint.Dark(RectColor));
                                g.FillRectangle(brush, SecondRect);
                                brush.Dispose();

                                break;
                        case Orientations.Vertical:
                                FirstRect= new Rectangle(Rect.X,Rect.Y, Rect.Width/2, Rect.Height);
                                SecondRect=new Rectangle(Rect.Width / 2,Rect.Y, Rect.Width/2, Rect.Height);
                                // Paint left half
                                brush = new LinearGradientBrush(
                                        new Point(FirstRect.Left, FirstRect.Height/2),
                                        new Point(FirstRect.Right,FirstRect.Height/2),
                                        ControlPaint.Dark(RectColor),
                                        RectColor);
                                g.FillRectangle(brush, FirstRect);
                                brush.Dispose();
                                // Paint right half
                                // (SecondRect.X - 1 because there would be a dark line in the middle of the bar)
                                brush = new LinearGradientBrush(
                                        new Point(SecondRect.Left - 1,SecondRect.Height/2),
                                        new Point(SecondRect.Right,SecondRect.Height/2),
                                        RectColor, 
                                        ControlPaint.Dark(RectColor));
                                g.FillRectangle(brush, SecondRect);
                                brush.Dispose();
                                break;
                        default:
                                break;
                }
        }


        protected void PaintPath(GraphicsPath PaintPath,Color PathColor,Graphics g)
        {
                Region FirstRegion,SecondRegion;
                FirstRegion = new Region(PaintPath);
                SecondRegion= new Region(PaintPath);
                //
                // Fill background
                //
                SolidBrush bgBrush = new SolidBrush(ControlPaint.Dark(PathColor));
                g.FillRegion(bgBrush, new Region(PaintPath));
                bgBrush.Dispose();

                //
                // The gradient brush
                //
                LinearGradientBrush brush;
                Rectangle FirstRect,SecondRect;
                Rectangle RegionRect = Rectangle.Truncate(PaintPath.GetBounds());
                switch(barOrientation)
                {
                        case Orientations.Horizontal:
                                FirstRect= new Rectangle(RegionRect.X,RegionRect.Y, RegionRect.Width, RegionRect.Height / 2);
                                SecondRect=new Rectangle(RegionRect.X, RegionRect.Height / 2, RegionRect.Width, RegionRect.Height / 2);
                                //only get the bar region
                                FirstRegion.Intersect(FirstRect);
                                SecondRegion.Intersect(SecondRect);
                                // Paint upper half
                                brush = new LinearGradientBrush(
                                        new Point(FirstRect.Width/2,FirstRect.Top),
                                        new Point(FirstRect.Width/2,FirstRect.Bottom),
                                        ControlPaint.Dark(PathColor),
                                        PathColor);
                                g.FillRegion(brush, FirstRegion);
                                
                                brush.Dispose();
                                // Paint lower half
                                // (SecondRect.Y - 1 because there would be a dark line in the middle of the bar)
                                brush = new LinearGradientBrush(
                                        new Point(SecondRect.Width/2, SecondRect.Top-1),
                                        new Point(SecondRect.Width/2, SecondRect.Bottom), 
                                        PathColor, 
                                        ControlPaint.Dark(PathColor));
                                g.FillRegion(brush, SecondRegion);
                                brush.Dispose();
					
                                break;
                        case Orientations.Vertical:
                                FirstRect= new Rectangle(RegionRect.X,RegionRect.Y, RegionRect.Width/2, RegionRect.Height);
                                SecondRect=new Rectangle(RegionRect.Width / 2,RegionRect.Y, RegionRect.Width/2, RegionRect.Height);
                                //only get the bar region
                                FirstRegion.Intersect(FirstRect);
                                SecondRegion.Intersect(SecondRect);
                                // Paint left half
                                brush = new LinearGradientBrush(
                                        new Point(FirstRect.Left, FirstRect.Height/2),
                                        new Point(FirstRect.Right,FirstRect.Height/2),
                                        ControlPaint.Dark(PathColor),
                                        PathColor);
                                g.FillRegion(brush, FirstRegion);
                                brush.Dispose();
                                // Paint right half
                                // (SecondRect.X - 1 because there would be a dark line in the middle of the bar)
                                brush = new LinearGradientBrush(
                                        new Point(SecondRect.Left - 1,SecondRect.Height/2),
                                        new Point(SecondRect.Right,SecondRect.Height/2),
                                        PathColor, 
                                        ControlPaint.Dark(PathColor));
                                g.FillRegion(brush, SecondRegion);
                                brush.Dispose();
                                break;
                        default:
                                break;
                }
        }


        protected GraphicsPath DrawRoundedCorners(Rectangle Rect,Color BorderColor,Graphics g)
        {
                GraphicsPath gPath = new GraphicsPath();
                try
                {
                        Pen LinePen = new Pen(BorderColor,borderWidth+1);
                        switch(barOrientation)
                        {
                                case Orientations.Horizontal:
                                        Rectangle LeftRect,RightRect;
                                        LeftRect=new Rectangle(Rect.X,Rect.Y+1,Rect.Height-1,Rect.Height-2);
                                        RightRect = new Rectangle(Rect.X+(Rect.Width-Rect.Height),Rect.Y+1,Rect.Height-1,Rect.Height-2);
                                        //build shape

                                        gPath.AddArc(LeftRect,90,180);
                                        gPath.AddLine(
                                                LeftRect.X+LeftRect.Width/2+2,LeftRect.Top+1,
                                                RightRect.X+(RightRect.Width/2)-1,RightRect.Top+1);
                                        gPath.AddArc(RightRect,270,180);
                                        gPath.AddLine(RightRect.X+(RightRect.Width/2),RightRect.Bottom,LeftRect.X+(LeftRect.Width/2),LeftRect.Bottom);
						
                                        gPath.CloseFigure();
                                        g.DrawPath(LinePen,gPath);
                                        break;
                                case Orientations.Vertical:
                                        Rectangle TopRect,BotRect;
                                        TopRect=new Rectangle(Rect.X+1,Rect.Y,Rect.Width-2,Rect.Width-1);
                                        BotRect = new Rectangle(Rect.X+1,Rect.Y+(Rect.Height-Rect.Width),Rect.Width-2,Rect.Width-1);
                                        //build shape
                                        gPath.AddArc(TopRect,180,180);
                                        gPath.AddLine(TopRect.Right,TopRect.Y+TopRect.Height/2,BotRect.Right,BotRect.Y+BotRect.Height/2+1);
                                        gPath.AddArc(BotRect,0,180);
                                        gPath.AddLine(BotRect.Left+1,BotRect.Y+BotRect.Height/2-1,
                                                TopRect.Left+1,TopRect.Y+TopRect.Height/2+2);
                                        gPath.CloseFigure();
                                        g.DrawPath(LinePen,gPath);
                                        break;
                                default:
                                        break;
                        }
                }
                catch(Exception Err)
                {
                        throw new Exception("DrawRoundedCornersException: "+Err.Message);
                }
                return gPath;

        }
        #endregion
                  
	}
}

