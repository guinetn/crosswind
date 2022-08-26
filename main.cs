using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Reflection;
using System.Text.RegularExpressions;   // Assembly 
using System.Resources;                 // ResourceManager
using System.Globalization;             // CultureInfo
using System.Threading;                 // Thread

namespace CrossWind
{
    public partial class main : Form
    {
        // Dimensions du dessin:
        private int Diametre = 180;
        private PointF CentrePt = new Point(200, 234);
        private Size Taille_Avion = new Size(66, 79);
        bool VentArrierePositif = false;

        // Tmp pour dessin:
        private Point TranslatePt;
        private PointF RotationPt;
        private PointF VentPt;

        // Pour le clic long
        private int Cap_MouseDown_Delta;    // variation du cap par mouse_down
        private Point Wind_MouseDown_Delta; // Variation du vent par mouse_down
        private int Attente_MouseDown = 500; // 500 ms


        // MousePos
        private Point LastPtClic = new Point(0, 0);
        private double prec_Angle;
        private int ModeCap = 0; // indicateur si "MouseMove" dans la rose des cap  OU  dans les vents
        // 0 = null  1 = caps  2 = vents




        private int Cap = 0;
        private double WindFrom = 0;
        private double WindSpeed = 0;

        double VentMax = 20;
        double Vent_Intervalle = 5;

        static ResourceManager rm = new ResourceManager("CrossWind.main", Assembly.GetExecutingAssembly());
        public CultureInfo cultInfo = new CultureInfo("");

        public main()
        {
            InitializeComponent();


            // Activates double buffering
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            VentPt = new PointF(CentrePt.X, CentrePt.Y);            
        }
               
    

        private void main_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Gradient fenêtre
            base.OnPaintBackground(e);
            System.Drawing.Drawing2D.LinearGradientBrush brush
            = new System.Drawing.Drawing2D.LinearGradientBrush(this.ClientRectangle,
                                                                Color.MidnightBlue,
                                                                Color.DodgerBlue,
                                                                LinearGradientMode.BackwardDiagonal);

            e.Graphics.FillRectangle(brush, this.ClientRectangle);
            brush.Dispose();


            TranslatePt.X = (int)CentrePt.X;
            TranslatePt.Y = (int)CentrePt.Y;
            TranslatePt.Offset(0, (int)-Diametre);
            RotationPt = new PointF(0, Diametre);

            // Antialiasing (efficace sur cube à 45°, arêtes...)
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Fond
            //    Rectangle rect = this.ClientRectangle;
            //    LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.BlueViolet, Color.Blue, LinearGradientMode.BackwardDiagonal);
            //    g.FillRectangle(lBrush, rect);

            // TRACE COMPAS
            Trace_Compas(g, Point.Truncate(CentrePt), Cap);

            // ---------------------------------------------------------------------------
            // AXES ECHELLE DES VENTS
            int diametre = (int)Diametre - 28;
            g.DrawLine(Pens.White, CentrePt.X, CentrePt.Y - diametre,
                                   CentrePt.X, CentrePt.Y + diametre);
            g.DrawLine(Pens.White, CentrePt.X - diametre, CentrePt.Y,
                                   CentrePt.X + diametre, CentrePt.Y);

            Font EchelleFont = new Font("Arial", 9, FontStyle.Bold);
            Font EchelleFontKt = new Font("Arial", 6, FontStyle.Bold);
            int t;
            Color Col_Echelle = Color.White; // FromArgb(170,Color.Ivory);

            int fin = 0; // Pour décaler le tracé de la valeur maximale de l'échelle

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

          
            for (double vent = 1; vent <= Math.Ceiling(VentMax / Vent_Intervalle); vent++)
            {
                if ((VentMax / Vent_Intervalle) < vent)
                { // Vent max au bord du cercle extérieur
                    vent = VentMax / Vent_Intervalle;
                    t = diametre;
                }
                else
                    t = (int)(vent * diametre / (VentMax / Vent_Intervalle));

                if (vent == Math.Ceiling(VentMax / Vent_Intervalle))
                    fin = 1;

                // Chiffre à droite du centre
                g.DrawString((vent * Vent_Intervalle).ToString(), EchelleFont, new SolidBrush(Col_Echelle),
                new RectangleF(new PointF(CentrePt.X + t - 14 - fin * 7, CentrePt.Y + 2),
                new Size(30, 15)), format);

                // Kt pour les chiffre à droite du centre
                if (fin == 1 )
                {
                    g.DrawString("Kt", EchelleFontKt, new SolidBrush(Col_Echelle),
                    new RectangleF(new PointF(CentrePt.X + t - 23 , CentrePt.Y + 15),
                    new Size(30, 15)), format);
                }

                // Chiffre à gauche du centre
                g.DrawString((vent * Vent_Intervalle).ToString(), EchelleFont, new SolidBrush(Col_Echelle),
                new RectangleF(new PointF(CentrePt.X - t - 14 + fin * 7, CentrePt.Y + 2),
                new Size(30, 15)), format);

                // Chiffre en bas du centre
                g.DrawString((vent * Vent_Intervalle).ToString(), EchelleFont, new SolidBrush(Col_Echelle),
                new RectangleF(new PointF(CentrePt.X - 2, CentrePt.Y + t - 8 - fin * 10),
                new Size(30, 15)), format);

                // Chiffre en haut du centre
                g.DrawString((vent * Vent_Intervalle).ToString(), EchelleFont, new SolidBrush(Col_Echelle),
                new RectangleF(new PointF(CentrePt.X - 2, CentrePt.Y - t - 8 + fin * 10),
                new Size(30, 15)), format);

                // Kt pour les chiffre à droite du centre
                if (fin == 1)
                {
                    g.DrawString("Kt", EchelleFontKt, new SolidBrush(Col_Echelle),
                 new RectangleF(new PointF(CentrePt.X + 11, CentrePt.Y - t + 12),
                 new Size(30, 15)), format);

                }
            }
            // ---------------------------------------------------------------------------


            g.TranslateTransform(CentrePt.X, CentrePt.Y - Diametre - 9);

            // INDEX DES CAPS
            GraphicsPath gp_CapIndex = new GraphicsPath();
            gp_CapIndex.AddLines(new Point[] { new Point(-8, 2) ,
                                                new Point( 8, 2) ,
                                                new Point( 0, 12),
                                                new Point(-8, 2)    });
            g.TranslateTransform(0, 2);
            g.FillPath(Brushes.Black, gp_CapIndex);
            Matrix sc = new Matrix(1, 0, 0, 1, 0, 0);
            sc.Scale(1.25f, 1.25f);
            gp_CapIndex.Transform(sc);
            g.TranslateTransform(0, -1);
            g.FillPath(new SolidBrush(Color.FromArgb(220, 255, 255, 55)), gp_CapIndex); 
            sc.Dispose();


            // AFFICHE LE CAP SUR L'INDEX
            RectangleF rectangle = new RectangleF(-20, -22, 50, 0);
            Font CapFont = new Font("Arial", 13, FontStyle.Bold);
            g.DrawString(Cap.ToString() + "°", CapFont, new SolidBrush(Color.White), rectangle, format);


            // TRACE L'AVION
            g.ResetTransform();
            g.TranslateTransform(CentrePt.X - Taille_Avion.Width / 2, CentrePt.Y - Taille_Avion.Height / 2);

            GraphicsPath gp_plane = new GraphicsPath();
            gp_plane.AddBezier(33, 0, 29, 7, 27, 16, 26, 28);
            Point[] Contour_avion = {
            new Point(0,49) , new Point(0,58) , new Point(26,49) , new Point(26,60),
            new Point(17,69), new Point(17,79), new Point(30,69), new Point(33,73) };
            gp_plane.AddLines(Contour_avion);

            Color plane_body = Color.FromArgb(30, Color.WhiteSmoke);
            Color plane_draw = Color.FromArgb(100, Color.White);
            Matrix mirror = new Matrix(1, 0, 0, 1, -33, -33);
            mirror.Scale(2, 2);
            gp_plane.Transform(mirror);
            g.DrawPath(new Pen(plane_draw), gp_plane);
            g.FillPath(new SolidBrush(plane_body), gp_plane);
            // Mirroir d'axe vertical
            mirror.Scale(0.5f, 0.5f);
            mirror.Multiply(new Matrix(-1, 0, 0, 1, 99, 33));
            gp_plane.Transform(mirror);

            g.DrawPath(new Pen(plane_draw), gp_plane);
            g.FillPath(new SolidBrush(plane_body), gp_plane);


            // TRACE LE VENT

            Pen ventpen = new Pen(Color.SpringGreen, 3);
            ventpen.StartCap = LineCap.Triangle;
            ventpen.EndCap = LineCap.RoundAnchor;
            //           g.DrawLine(ventpen, CentrePt, VentPt);

            Pen pDash = new Pen(Color.FromArgb(150, Color.Ivory), 1);
            pDash.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            pDash.StartCap = LineCap.Triangle;
            pDash.EndCap = LineCap.Triangle;

            Compute_Wind();
            drawVector(g, Color.Yellow, WindFrom, WindSpeed);

            // Trace les projections du vent
            g.ResetTransform();
            pDash.Color = Color.Yellow;
            g.DrawLine(pDash, VentPt, new PointF(VentPt.X, CentrePt.Y));
            g.DrawLine(pDash, VentPt, new PointF(CentrePt.X, VentPt.Y));
        }


       
        private bool Compute_Wind()
        {
            //  User change wind

            #region CALCUL ANGLE = ARCTG (Y/X) IN  DEGREES
            
            if (!IsInsideCircle(new Point((int)VentPt.X, (int)VentPt.Y), 1, Diametre - 28))
                return false;

            double dx = VentPt.X - CentrePt.X;
            double dy = CentrePt.Y - VentPt.Y;

            if (dy == 0) // Evite la divison par zéro
            {
                WindFrom = Math.PI / 2.0;
                if (dx < 0) WindFrom = 3.0 * Math.PI / 2;
            }
            else if (dx == 0) WindFrom = 0;
            else WindFrom = Math.Atan(dx / dy); // !! on fait dx/dy car angle en sens horaire !!

            WindFrom *= 180.0 / Math.PI;
            WindFrom = (int)WindFrom;

            if (dy < 0) WindFrom += 180.0;
            else if (dx < 0 && dy > 0) WindFrom += 360.0;

            WindFrom += Cap;
            if (WindFrom >= 360) WindFrom -= 360.0;
            lbl_WindFrom.Text = String.Format("{0:000}°", WindFrom);
            
            #endregion


            #region  COMPUTE WIND SPEED (MAX = VENT_MAX)

                double d = Math.Sqrt(dx * dx + dy * dy);
                WindSpeed = 0;
                if (Diametre != 0) WindSpeed = d * VentMax / (Diametre - 28);
                WindSpeed = Math.Round(WindSpeed, 0);
                lbl_WindSpeed.Text = WindSpeed.ToString() + " Kt";

            #endregion


            #region COMPUTE CROSSWIND & HEADWIND

            double Crosswind = WindSpeed * Math.Sin((WindFrom - Cap) * Math.PI / 180);

                // Crosswind
                Crosswind = Math.Round(Crosswind, 1);
                lbl_CrossWind.Text = String.Format("{0:##0.0 Kt}", Crosswind);

                // HeadWind
                double HeadWind = WindSpeed * Math.Cos((WindFrom - Cap) * Math.PI / 180);
                HeadWind = Math.Round(HeadWind, 1);
                if (VentArrierePositif)
                    HeadWind = -HeadWind; // Conformité avec le CX2...
                lbl_HeadWind.Text = String.Format("{0:##0.0 Kt}", HeadWind); 

            #endregion


            #region WIND DETAILS INFOS
            
                StringBuilder infos_vent = new StringBuilder();
                //if (0 < HeadWind)
                //    infos_vent.Append(VentArrierePositif ? rm.GetString("WArriere", cultInfo) : rm.GetString("WFace", cultInfo));
                //else if ((int)HeadWind != 0)
                //    infos_vent.Append(VentArrierePositif ? rm.GetString("WFace", cultInfo) : rm.GetString("WArriere", cultInfo));

                //if (0 < Crosswind)
                //    infos_vent.Append(" " + rm.GetString("WDroite", cultInfo));
                //else if ((int)Crosswind != 0)
                //    infos_vent.Append(" " + rm.GetString("WGauche", cultInfo));

                lbl_WindInfos.Text = infos_vent.ToString(); 
            
            #endregion

            return true;
        }


        #region Trace vecteur vent

        public void drawVector(Graphics g, Color couleur, double from, double longueur)
        {
            g.ResetTransform();
            g.TranslateTransform(VentPt.X, VentPt.Y);
            g.RotateTransform(-(float)Cap + (float)from + 180);
            // Flêche
            longueur = (int)((double)Diametre - 28) * (longueur / VentMax);
            GraphicsPath gp_Arrow = getArrowGraphic((int)(longueur));
            g.FillPath(new SolidBrush(Color.FromArgb(180, couleur)), gp_Arrow);
            g.DrawPath(Pens.Black, gp_Arrow);
        }

        public static GraphicsPath getArrowGraphic(int longueur)
        {
            // Renvoi les points pour dessiner une flêche
            longueur -= 7;
            GraphicsPath Arrow = new GraphicsPath();
            int d = 2;
            Point[] Fleche = { new Point(-6-d, 0) ,
                                   new Point( 0, -7-d) ,
                                   new Point( 6+d, 0) ,
                                   new Point( 2+d, 0),
                                   new Point( 1, longueur),
                                   new Point( -1, longueur),
                                   new Point( -2-d, 0),
                                   new Point( -6-d, 0)
                                       };
            Arrow.AddLines(Fleche);

            Matrix m = new Matrix(1, 0, 0, 1, 0, -longueur);
            Arrow.Transform(m);
            return Arrow;
        }

        #endregion

        #region Trace_Compas

        private void Trace_Compas(Graphics g, Point centerPoint, int Heading)
        {
            // Trace une rose des caps
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int rayonInt = Diametre - 20;
            int LineWidth = 2;
            int Letters_Size = 16;
            bool capsIn = true;       // Caps à l'interieur ou à l'exterieur

            // Fond
            Rectangle rect = this.ClientRectangle;
            SolidBrush sb = new SolidBrush(Color.FromArgb(66, 67, 75)); // Fond gris

            // Fond3D
            LinearGradientBrush b = new LinearGradientBrush(rect, Color.Black, Color.Ivory, LinearGradientMode.ForwardDiagonal);
            g.FillEllipse(b, centerPoint.X - Diametre - 9, centerPoint.Y - Diametre - 9, Diametre * 2 + 16, Diametre * 2 + 16);
            // Fond du compas
            g.FillEllipse(sb, centerPoint.X - Diametre - 5, centerPoint.Y - Diametre - 5, Diametre * 2 + 10, Diametre * 2 + 10);

            g.DrawEllipse(new Pen(Color.Black, 3), centerPoint.X - Diametre - 5, centerPoint.Y - Diametre - 5, Diametre * 2 + 10, Diametre * 2 + 10);


            // Verrière
            Color cc = Color.FromArgb(208, 206, 255);
            b = new LinearGradientBrush(rect, Color.Black, cc, LinearGradientMode.ForwardDiagonal);
            b.SetBlendTriangularShape(0.55f, 0.40F);
            g.FillEllipse(b, centerPoint.X - Diametre - 5, centerPoint.Y - Diametre - 5, Diametre * 2 + 10, Diametre * 2 + 10);
            b.Dispose();

            // Stylos et Fonte
            Pen styloCaps = new Pen(Color.White, LineWidth);

            FontFamily family = new FontFamily("Arial");
            int fontStyle = (int)FontStyle.Bold;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            int capToDraw;
            string strCap;
            string[] caps = { "N", "W", "S", "E" };
            int CapsInOut = (capsIn ? -20 : 0);
            GraphicsPath gp = new GraphicsPath();
            Rectangle MyRect;
            Pen pen_rayons = new Pen(Color.White, 1);

            // Parcourir les angles à tracer
            for (int angle = Heading; angle <= 350 + Heading; angle += 10)
            {
                g.ResetTransform();
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TranslateTransform(centerPoint.X, centerPoint.Y);
                g.RotateTransform(360 - angle);
                if ((angle - Heading) % 30 == 0)
                {
                    // Tirets
                    g.DrawLine(styloCaps, 0, -rayonInt - 2 * (capsIn ? 0 : 1) + CapsInOut, 0, 11 - rayonInt + CapsInOut); // tirets longs de 30° en 30°

                    // Rayon tous les 30°
                    g.DrawLine(pen_rayons, 0, 0, 0, -Diametre + 28);
                }
                else if ((angle - Heading) % 10 == 0)
                    g.DrawLine(styloCaps, 0, -rayonInt + CapsInOut, 0, 11 - rayonInt + CapsInOut); // tirets moyens de 10° en 10°

                if ((angle - Heading) % 30 == 0)
                {
                    // Ecrire le cap tous les 30°
                    if ((-angle + Heading) % 90 == 0)
                    {
                        strCap = caps[(angle - Heading) / 90]; //  Cap multiple de 90°: Ecrire la lettre NWSE

                        MyRect = new Rectangle(new Point(-20, -Diametre - 3 * (capsIn ? 3 : 1) - CapsInOut), new Size(40, Diametre + 12));
                        gp.Reset();
                        gp.AddString(strCap, family, fontStyle, Letters_Size, MyRect, format);
                        g.FillPath(Brushes.Ivory, gp);
                        //g.DrawString(strCap,capsFont,Brushes.Ivory,0, -Diametre-3*(capsIn?3:1)-CapsInOut, format);
                    }
                    else
                    {
                        // Cap multiple de 30° en chiffres
                        capToDraw = (int)Math.Round((360 - angle + (double)Heading) / 10, 0);
                        capToDraw = validHeading(capToDraw);
                        strCap = capToDraw.ToString();

                        MyRect = new Rectangle(new Point(-20, -Diametre - 1 * (capsIn ? 10 : 1) - CapsInOut), new Size(40, Diametre + 12));
                        gp.Reset();
                        gp.AddString(strCap, family, fontStyle, Letters_Size - 2, MyRect, format);
                        g.FillPath(Brushes.Ivory, gp);
                        // g.DrawString(strCap, anglesFont, Brushes.Ivory, 0, -Diametre-1*(capsIn?10:1)-CapsInOut, format);
                    }
                }

                g.RotateTransform(-5);  // tirets courts de 5° en 5°
                g.DrawLine(styloCaps, 0, -rayonInt + CapsInOut, 0, 6 - rayonInt + CapsInOut);
            }



            // ---------------------------------------------------------------------------
            // Cercles des vents
            int r;
            Pen pDash = new Pen(Color.FromArgb(150, Color.Ivory), 1);
            pDash.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            pDash.StartCap = LineCap.Triangle;
            pDash.EndCap = LineCap.Triangle;

            g.ResetTransform();
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Cercle intérieur
            int diametre = Diametre - 28;
            g.DrawEllipse(new Pen(Color.White, 1), centerPoint.X - diametre, centerPoint.Y - diametre, Diametre * 2 - 56, Diametre * 2 - 56);

            for (double i = 0; i < VentMax / Vent_Intervalle; i++)
            {
                r = (int)(i * ((double)diametre) / (VentMax / Vent_Intervalle));
                g.DrawEllipse(pDash, centerPoint.X - r, centerPoint.Y - r, r * 2, r * 2);
            }



        }

        public static int validHeading(int newCap)
        {
            if (newCap < 0)
                return 360 + newCap;
            else if (360 <= newCap)
                return newCap - 360;
            return newCap;
        }

        #endregion PaintRose

        private void main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Point pc = new Point(e.X, e.Y);

            if (ModeCap == 1 && IsInsideCircle(pc, 1, Diametre))
            {
                // Mode "rotation des caps"
                double dx = pc.X - CentrePt.X;
                double dy = pc.Y - CentrePt.Y;
                double angle;
                try
                {
                    angle = Math.Atan2(dx, dy);
                    angle *= (180.0 / Math.PI);
                    angle += 180;
                    if (360 <= angle) angle -= 360;
                }
                catch (DivideByZeroException)
                {
                    angle = 0;
                }
                int coeff = 1; // Accélérateur de rotation
                if (45 < angle && angle < 315)
                    coeff = 3;
                SetCap(Cap + coeff * Math.Sign(angle - prec_Angle));
                prec_Angle = angle;
            }
            else if (ModeCap == 2 && IsInsideCircle(pc, 1, Diametre - 28))
            {
                // Mode "Modification du vent"
                if (VentPt == pc) return;
                VentPt = pc;
                Invalidate();   // Paint() + Calcul_Vent()
            }
        }



        // ***************************************************
        private bool IsInsideCircle(Point p, int Rint, int Rext)
        {
            double d = distance(p);
            return ((Rint) <= d && d <= Rext ? true : false);
        }

        // ***************************************************
        private double distance(Point p)
        {
            return Math.Sqrt(Math.Pow(CentrePt.X - p.X, 2) + Math.Pow(CentrePt.Y - p.Y, 2));
        }

        private void main_MouseEnter(object sender, EventArgs e)
        {

        }

        private void main_MouseDown(object sender, MouseEventArgs e)
        {
            LastPtClic.X = e.X;
            LastPtClic.Y = e.Y;

            if (IsInsideCircle(LastPtClic, Diametre - 28, Diametre))
                ModeCap = 1;   // Cap
            else if (IsInsideCircle(LastPtClic, 1, Diametre - 28))
            {
                VentPt = LastPtClic;
                ModeCap = 2; // vent
                Invalidate();
            }
            else
                ModeCap = 0; // null
        }

        private void main_DoubleClick(object sender, EventArgs e)
        {
            Point pc = LastPtClic;
            if (!IsInsideCircle(pc, Diametre - 28, Diametre)) return;

            double dx = pc.X - CentrePt.X;
            double dy = -pc.Y + CentrePt.Y;
            double angle;
            try
            {
                angle = Math.Atan2(dx, dy);
            }
            catch (DivideByZeroException)
            {
                if (dy < 0) angle = 3.0 * Math.PI / 2;
                else angle = Math.PI / 2;
            }
            angle *= (180.0 / Math.PI);
            if (angle < 0) angle += 360;
            angle += Cap;
            if (360 <= angle) angle -= 360;
            SetCap((int)angle);
            Invalidate();
        }

        private void SetCap(int newcap)
        {
            Cap = newcap;
            if (Cap < 0)
                Cap = 359;
            else if (Cap >= 360)
                Cap = 0;

            Invalidate();
        }

        private void gb_WindFromMinus_Click(object sender, EventArgs e)
        {
            Wind_MouseDown_Delta = new Point(-1, 0);
            Change_Vent();            
        }

        private void gb_WindFromPlus_Click(object sender, EventArgs e)
        {
            Wind_MouseDown_Delta = new Point(1, 0);
            Change_Vent();
        }






        // Trouver le couple (x,y) qui donne le vent indiqué par l'utilisateur
        // Plusieurs points sont possibles: ils sont situés sur 3 arcs de cercles de rayon r+1,r , r-1

        private void Wind_ByValues(int Wind_Direction_Offset, int Wind_Speed_Offset)
        {
            double angle_Voulu = Convert.ToDouble(   Regex.Match(lbl_WindFrom.Text, "(\\d*)°").Groups[1].Value);
            angle_Voulu += Wind_Direction_Offset;
            if (360 <= angle_Voulu) angle_Voulu -= 360.0;
            if (angle_Voulu < 0) angle_Voulu += 360.0;

            double Force_Voulue = Convert.ToDouble(Regex.Match(lbl_WindSpeed.Text, "(\\d*) Kt").Groups[1].Value);
            Force_Voulue += Wind_Speed_Offset;
            if (VentMax < Force_Voulue || Force_Voulue < 1)
                return;

            Rectangle r = this.ClientRectangle;
            float w = r.Width;
            float h = r.Height;
            int vx = (int)(w / VentMax);
            int vy = (int)(h / VentMax);
            double ANGLE_rad = Cap + 90 - angle_Voulu;
            ANGLE_rad *= Math.PI / 180;

            int Ymin = (int)(CentrePt.Y - (-28 + Diametre) * (Force_Voulue / VentMax) * Math.Sin(ANGLE_rad));
            int Xmin = (int)(CentrePt.X + (-28 + Diametre) * (Force_Voulue / VentMax) * Math.Cos(ANGLE_rad));

            // Parcourir tous les points autour de la solution
            double angle2, force2;
            bool Succes = false;
            bool Point_In_RoseCaps = false;

            int x, y;
            vx /= 2;
            vy /= 2;
            for (x = Xmin; x <= Xmin + vx; x++)
            { // <1>
                for (y = (int)Ymin; y <= (int)Ymin + vy; y++)
                {
                    VentPt = new Point(x, y);
                    Point_In_RoseCaps = Compute_Wind();

                    angle2 = Convert.ToDouble(Regex.Match(lbl_WindFrom.Text, "(\\d*)°").Groups[1].Value);
                    if (angle2 < 0) angle2 += 360;
                    force2 = Convert.ToDouble(Regex.Match(lbl_WindSpeed.Text, "(\\d*) Kt").Groups[1].Value);

                    if (Point_In_RoseCaps && angle2 == angle_Voulu && force2 == Force_Voulue)
                    {
                        // Point correspondant trouvé et est bien situé dans la rose des caps
                        Succes = true;
                        break;
                    }
                }
                if (Succes) break;
            } // </1>

            if (!Succes) return;
            ModeCap = 2; // simule un clic pour le vent
            Invalidate();
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save Datas in Registry
            Application.UserAppDataRegistry.SetValue("Cap", Cap);
            Application.UserAppDataRegistry.SetValue("Windx", (int)VentPt.X);
            Application.UserAppDataRegistry.SetValue("Windy", (int)VentPt.Y);
            Application.UserAppDataRegistry.SetValue("Zoom", (int)sb_CompassScale.Value);
            Application.UserAppDataRegistry.SetValue("Left", Left);
            Application.UserAppDataRegistry.SetValue("Top", Top);
            Application.UserAppDataRegistry.SetValue("Culture", cultInfo.Name);
        }

        private void main_Load(object sender, EventArgs e)
        {
            // Read Datas in Registry
            try
            {
                Cap = (int)Application.UserAppDataRegistry.GetValue("Cap",0);
                sb_CompassScale.Value = (double) ((int)Application.UserAppDataRegistry.GetValue("Zoom",0));
                VentPt.X = (float)((int)Application.UserAppDataRegistry.GetValue("Windx", 0));
                VentPt.Y = (float)((int)Application.UserAppDataRegistry.GetValue("Windy", 0));

                Left = (int)Application.UserAppDataRegistry.GetValue("Left", 0);
                Top = (int)Application.UserAppDataRegistry.GetValue("Top", 0);
                cultInfo = new CultureInfo((string)Application.UserAppDataRegistry.GetValue("Culture", ""));

                ChangeLanguage();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void sb_CompassScale_ValueChanged(object sender)
        {
            int[][] Zooms = { new int[]{10,2}  , new int[]{20,5},new int[]{30,10},
                            new int[]{50,10} , new int[]{75,15},new int[]{100,25},
                            new int[]{200,50}, new int[]{300,100},new int[]{400,100},
                            new int[]{500,100} };

            VentMax = Zooms[(int)sb_CompassScale.Value][0];
            Vent_Intervalle = Zooms[(int)sb_CompassScale.Value][1];
            Invalidate();
        }


        private void gb_HeadingPlus_MouseUp(object sender, MouseEventArgs e)
        {
            timer_Cap.Enabled = false;
            timer_Cap.Interval = Attente_MouseDown;
        }

        private void gb_HeadingPlus_MouseDown(object sender, MouseEventArgs e)
        {
            Cap_MouseDown_Delta = 1;
            timer_Cap.Enabled = true;
        }

        
        private void gb_HeadingMinus_MouseDown(object sender, MouseEventArgs e)
        {
            Cap_MouseDown_Delta = -1;
            timer_Cap.Enabled = true;
        }

        private void Change_Cap()
        {
            timer_Cap.Interval = 10;
            SetCap(Cap + Cap_MouseDown_Delta);
            // if (timer_Cap.Interval > 10) timer_Cap.Interval += 1;
        }

        private void Change_Vent()
        {
            timer_Vent.Interval = 10;
            Wind_ByValues(Wind_MouseDown_Delta.X, Wind_MouseDown_Delta.Y);           
        }

        private void main_Deactivate(object sender, EventArgs e)
        {
            // Cas ou on passe sur une autre application (Alt+Tab) en laissant le clic sur un bouton +/-
            // le compteur continuerai à décompter...
            timer_Cap.Enabled = false;
            timer_Vent.Enabled = false;
        }

        private void timer_Cap_Tick(object sender, System.EventArgs e)
        {
            // Timer gérant l'affichage quand on appui sur le bouton +/-
            Change_Cap();
        }

        private void timer_Vent_Tick(object sender, System.EventArgs e)
        {
            timer_Vent.Interval = 10;
            Wind_ByValues(Wind_MouseDown_Delta.X, Wind_MouseDown_Delta.Y);
        }

        private void gb_WindFromPlus_MouseUp(object sender, MouseEventArgs e)
        {
            timer_Vent.Enabled = false;
            timer_Vent.Interval = Attente_MouseDown;
        }

        private void gb_WindFromPlus_MouseDown(object sender, MouseEventArgs e)
        {
            Wind_MouseDown_Delta = new Point(1, 0);
            timer_Vent.Enabled = true;
        }

        private void gb_WindFromMinus_MouseDown(object sender, MouseEventArgs e)
        {
            Wind_MouseDown_Delta = new Point(-1, 0);
            timer_Vent.Enabled = true;
        }

        private void gb_HeadingPlus_Click(object sender, EventArgs e)
        {
            Cap_MouseDown_Delta = 1;
            Change_Cap();
        }

        private void gb_HeadingMinus_Click(object sender, EventArgs e)
        {
            Cap_MouseDown_Delta = -1;
            Change_Cap();
        }

        private void gb_HeadingMinus_MouseUp(object sender, MouseEventArgs e)
        {
            timer_Cap.Enabled = false;
            timer_Cap.Interval = Attente_MouseDown;
        }

        private void gb_WindFromMinus_MouseUp(object sender, MouseEventArgs e)
        {
            timer_Vent.Enabled = false;
            timer_Vent.Interval = Attente_MouseDown;
        }


        #region WindSpeed Increase/Decrease

        // Decrease

        private void gb_WindSpeedMinus_Click(object sender, EventArgs e)
        {
            Wind_MouseDown_Delta = new Point(1, -1);
            Change_Vent();
        }

        private void gb_WindSpeedMinus_MouseDown(object sender, MouseEventArgs e)
        {
            Wind_MouseDown_Delta = new Point(0, -1);
            timer_Vent.Enabled = true;
        }
    
        // Increase

        private void gb_WindSpeedPlus_Click(object sender, EventArgs e)
        {
            Wind_MouseDown_Delta = new Point(0, 1);
            Change_Vent();
        }

        private void gb_WindSpeedPlus_MouseDown(object sender, MouseEventArgs e)
        {
            Wind_MouseDown_Delta = new Point(0, 1);
            timer_Vent.Enabled = true;
        }
              
        #endregion

        private void gb_WindSpeedMinus_MouseUp(object sender, MouseEventArgs e)
        {
            timer_Vent.Enabled = false;
            timer_Vent.Interval = Attente_MouseDown;
        }

        private void gb_WindSpeedPlus_MouseUp(object sender, MouseEventArgs e)
        {
            timer_Vent.Enabled = false;
            timer_Vent.Interval = Attente_MouseDown;
        }

        private void gb_SelectLanguage_Click(object sender, EventArgs e)
        {
            ChangeLanguage();
        }

        private void ChangeLanguage()
        {
            // optionnel :             
            if (cultInfo.Name == "")
            {
                cultInfo = new CultureInfo("en-GB");
                Thread.CurrentThread.CurrentCulture = cultInfo;
                Thread.CurrentThread.CurrentUICulture = cultInfo;
                gb_SelectLanguage.Text = "FR";
            }
            else
            {
                cultInfo = new CultureInfo("");
                Thread.CurrentThread.CurrentCulture = cultInfo;
                Thread.CurrentThread.CurrentUICulture = cultInfo;
                gb_SelectLanguage.Text = "EN";
            }

            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(main));
                resources.ApplyResources(c, c.Name);
            }
        }

       
        

    }
}