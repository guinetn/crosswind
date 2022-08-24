namespace CrossWind
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.lbl_WindFromLbl = new System.Windows.Forms.Label();
            this.lbl_WindSpeedLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer_Cap = new System.Windows.Forms.Timer(this.components);
            this.timer_Vent = new System.Windows.Forms.Timer(this.components);
            this.lbl_CrossWind = new System.Windows.Forms.Label();
            this.lbl_HeadWind = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_RunwayHeading = new System.Windows.Forms.Label();
            this.lbl_WindFrom = new System.Windows.Forms.Label();
            this.lbl_WindSpeed = new System.Windows.Forms.Label();
            this.lbl_WindInfos = new System.Windows.Forms.Label();
            this.gb_SelectLanguage = new Glass.GlassButton();
            this.gb_WindSpeedMinus = new Glass.GlassButton();
            this.gb_WindSpeedPlus = new Glass.GlassButton();
            this.gb_WindFromMinus = new Glass.GlassButton();
            this.gb_WindFromPlus = new Glass.GlassButton();
            this.sb_CompassScale = new ng.Controls.Bar.ScrollProgrBar();
            this.gb_HeadingMinus = new Glass.GlassButton();
            this.gb_HeadingPlus = new Glass.GlassButton();
            this.SuspendLayout();
            // 
            // lbl_WindFromLbl
            // 
            resources.ApplyResources(this.lbl_WindFromLbl, "lbl_WindFromLbl");
            this.lbl_WindFromLbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl_WindFromLbl.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_WindFromLbl.Name = "lbl_WindFromLbl";
            // 
            // lbl_WindSpeedLbl
            // 
            resources.ApplyResources(this.lbl_WindSpeedLbl, "lbl_WindSpeedLbl");
            this.lbl_WindSpeedLbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl_WindSpeedLbl.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_WindSpeedLbl.Name = "lbl_WindSpeedLbl";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Name = "label1";
            // 
            // timer_Cap
            // 
            this.timer_Cap.Interval = 20;
            this.timer_Cap.Tick += new System.EventHandler(this.timer_Cap_Tick);
            // 
            // timer_Vent
            // 
            this.timer_Vent.Interval = 20;
            this.timer_Vent.Tick += new System.EventHandler(this.timer_Vent_Tick);
            // 
            // lbl_CrossWind
            // 
            resources.ApplyResources(this.lbl_CrossWind, "lbl_CrossWind");
            this.lbl_CrossWind.BackColor = System.Drawing.Color.Transparent;
            this.lbl_CrossWind.ForeColor = System.Drawing.Color.Ivory;
            this.lbl_CrossWind.Name = "lbl_CrossWind";
            // 
            // lbl_HeadWind
            // 
            resources.ApplyResources(this.lbl_HeadWind, "lbl_HeadWind");
            this.lbl_HeadWind.BackColor = System.Drawing.Color.Transparent;
            this.lbl_HeadWind.ForeColor = System.Drawing.Color.Ivory;
            this.lbl_HeadWind.Name = "lbl_HeadWind";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Ivory;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Ivory;
            this.label3.Name = "label3";
            // 
            // lbl_RunwayHeading
            // 
            resources.ApplyResources(this.lbl_RunwayHeading, "lbl_RunwayHeading");
            this.lbl_RunwayHeading.BackColor = System.Drawing.Color.Transparent;
            this.lbl_RunwayHeading.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_RunwayHeading.Name = "lbl_RunwayHeading";
            // 
            // lbl_WindFrom
            // 
            resources.ApplyResources(this.lbl_WindFrom, "lbl_WindFrom");
            this.lbl_WindFrom.BackColor = System.Drawing.Color.Transparent;
            this.lbl_WindFrom.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_WindFrom.Name = "lbl_WindFrom";
            // 
            // lbl_WindSpeed
            // 
            resources.ApplyResources(this.lbl_WindSpeed, "lbl_WindSpeed");
            this.lbl_WindSpeed.BackColor = System.Drawing.Color.Transparent;
            this.lbl_WindSpeed.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_WindSpeed.Name = "lbl_WindSpeed";
            // 
            // lbl_WindInfos
            // 
            resources.ApplyResources(this.lbl_WindInfos, "lbl_WindInfos");
            this.lbl_WindInfos.BackColor = System.Drawing.Color.Transparent;
            this.lbl_WindInfos.ForeColor = System.Drawing.Color.LightGray;
            this.lbl_WindInfos.Name = "lbl_WindInfos";
            // 
            // gb_SelectLanguage
            // 
            this.gb_SelectLanguage.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.gb_SelectLanguage, "gb_SelectLanguage");
            this.gb_SelectLanguage.InnerBorderColor = System.Drawing.Color.Transparent;
            this.gb_SelectLanguage.Name = "gb_SelectLanguage";
            this.gb_SelectLanguage.OuterBorderColor = System.Drawing.Color.Transparent;
            this.gb_SelectLanguage.Click += new System.EventHandler(this.gb_SelectLanguage_Click);
            // 
            // gb_WindSpeedMinus
            // 
            this.gb_WindSpeedMinus.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.gb_WindSpeedMinus, "gb_WindSpeedMinus");
            this.gb_WindSpeedMinus.InnerBorderColor = System.Drawing.Color.Transparent;
            this.gb_WindSpeedMinus.Name = "gb_WindSpeedMinus";
            this.gb_WindSpeedMinus.OuterBorderColor = System.Drawing.Color.Transparent;
            this.gb_WindSpeedMinus.Click += new System.EventHandler(this.gb_WindSpeedMinus_Click);
            this.gb_WindSpeedMinus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gb_WindSpeedMinus_MouseDown);
            this.gb_WindSpeedMinus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gb_WindSpeedMinus_MouseUp);
            // 
            // gb_WindSpeedPlus
            // 
            this.gb_WindSpeedPlus.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.gb_WindSpeedPlus, "gb_WindSpeedPlus");
            this.gb_WindSpeedPlus.InnerBorderColor = System.Drawing.Color.Transparent;
            this.gb_WindSpeedPlus.Name = "gb_WindSpeedPlus";
            this.gb_WindSpeedPlus.OuterBorderColor = System.Drawing.Color.Transparent;
            this.gb_WindSpeedPlus.Click += new System.EventHandler(this.gb_WindSpeedPlus_Click);
            this.gb_WindSpeedPlus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gb_WindSpeedPlus_MouseDown);
            this.gb_WindSpeedPlus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gb_WindSpeedPlus_MouseUp);
            // 
            // gb_WindFromMinus
            // 
            this.gb_WindFromMinus.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.gb_WindFromMinus, "gb_WindFromMinus");
            this.gb_WindFromMinus.InnerBorderColor = System.Drawing.Color.Transparent;
            this.gb_WindFromMinus.Name = "gb_WindFromMinus";
            this.gb_WindFromMinus.OuterBorderColor = System.Drawing.Color.Transparent;
            this.gb_WindFromMinus.Click += new System.EventHandler(this.gb_WindFromMinus_Click);
            this.gb_WindFromMinus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gb_WindFromMinus_MouseDown);
            this.gb_WindFromMinus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gb_WindFromMinus_MouseUp);
            // 
            // gb_WindFromPlus
            // 
            this.gb_WindFromPlus.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.gb_WindFromPlus, "gb_WindFromPlus");
            this.gb_WindFromPlus.InnerBorderColor = System.Drawing.Color.Transparent;
            this.gb_WindFromPlus.Name = "gb_WindFromPlus";
            this.gb_WindFromPlus.OuterBorderColor = System.Drawing.Color.Transparent;
            this.gb_WindFromPlus.Click += new System.EventHandler(this.gb_WindFromPlus_Click);
            this.gb_WindFromPlus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gb_WindFromPlus_MouseDown);
            this.gb_WindFromPlus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gb_WindFromPlus_MouseUp);
            // 
            // sb_CompassScale
            // 
            this.sb_CompassScale.BarBorderColor = System.Drawing.Color.Black;
            this.sb_CompassScale.BarColor = System.Drawing.Color.White;
            this.sb_CompassScale.BarMode = ng.Controls.Bar.Mode.ScrollBar;
            this.sb_CompassScale.BarOrientation = ng.Controls.Bar.Orientations.Horizontal;
            this.sb_CompassScale.BorderWidth = 0;
            this.sb_CompassScale.ControlCornerStyle = ng.Controls.Bar.CornerStyles.Square;
            this.sb_CompassScale.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.sb_CompassScale, "sb_CompassScale");
            this.sb_CompassScale.Maximum = 6D;
            this.sb_CompassScale.MaximumValueSide = ng.Controls.Bar.Poles.Right;
            this.sb_CompassScale.Minimum = 0D;
            this.sb_CompassScale.Name = "sb_CompassScale";
            this.sb_CompassScale.TrackerBorderColor = System.Drawing.Color.Black;
            this.sb_CompassScale.TrackerColor = System.Drawing.Color.Yellow;
            this.sb_CompassScale.Value = 0D;
            this.sb_CompassScale.ValueChanged += new ng.Controls.Bar.ScrollProgrBar.ValueChangedEventHandler(this.sb_CompassScale_ValueChanged);
            // 
            // gb_HeadingMinus
            // 
            this.gb_HeadingMinus.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.gb_HeadingMinus, "gb_HeadingMinus");
            this.gb_HeadingMinus.InnerBorderColor = System.Drawing.Color.Transparent;
            this.gb_HeadingMinus.Name = "gb_HeadingMinus";
            this.gb_HeadingMinus.OuterBorderColor = System.Drawing.Color.Transparent;
            this.gb_HeadingMinus.Click += new System.EventHandler(this.gb_HeadingMinus_Click);
            this.gb_HeadingMinus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gb_HeadingMinus_MouseDown);
            this.gb_HeadingMinus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gb_HeadingMinus_MouseUp);
            // 
            // gb_HeadingPlus
            // 
            this.gb_HeadingPlus.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.gb_HeadingPlus, "gb_HeadingPlus");
            this.gb_HeadingPlus.InnerBorderColor = System.Drawing.Color.Transparent;
            this.gb_HeadingPlus.Name = "gb_HeadingPlus";
            this.gb_HeadingPlus.OuterBorderColor = System.Drawing.Color.Transparent;
            this.gb_HeadingPlus.Click += new System.EventHandler(this.gb_HeadingPlus_Click);
            this.gb_HeadingPlus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gb_HeadingPlus_MouseDown);
            this.gb_HeadingPlus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gb_HeadingPlus_MouseUp);
            // 
            // main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Controls.Add(this.gb_SelectLanguage);
            this.Controls.Add(this.lbl_WindInfos);
            this.Controls.Add(this.lbl_WindSpeed);
            this.Controls.Add(this.lbl_WindFrom);
            this.Controls.Add(this.lbl_RunwayHeading);
            this.Controls.Add(this.gb_WindSpeedMinus);
            this.Controls.Add(this.gb_WindSpeedPlus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_HeadWind);
            this.Controls.Add(this.lbl_CrossWind);
            this.Controls.Add(this.gb_WindFromMinus);
            this.Controls.Add(this.gb_WindFromPlus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sb_CompassScale);
            this.Controls.Add(this.lbl_WindSpeedLbl);
            this.Controls.Add(this.lbl_WindFromLbl);
            this.Controls.Add(this.gb_HeadingMinus);
            this.Controls.Add(this.gb_HeadingPlus);
            this.ForeColor = System.Drawing.Color.Yellow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "main";
            this.Deactivate += new System.EventHandler(this.main_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.Load += new System.EventHandler(this.main_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.main_Paint);
            this.DoubleClick += new System.EventHandler(this.main_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.main_MouseDown);
            this.MouseEnter += new System.EventHandler(this.main_MouseEnter);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.main_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Glass.GlassButton gb_HeadingPlus;
        private Glass.GlassButton gb_HeadingMinus;
        private System.Windows.Forms.Label lbl_WindFromLbl;
        private System.Windows.Forms.Label lbl_WindSpeedLbl;
        private System.Windows.Forms.Label label1;
        private ng.Controls.Bar.ScrollProgrBar sb_CompassScale;
        private Glass.GlassButton gb_WindFromMinus;
        private Glass.GlassButton gb_WindFromPlus;
        private System.Windows.Forms.Timer timer_Cap;
        private System.Windows.Forms.Timer timer_Vent;
        private System.Windows.Forms.Label lbl_CrossWind;
        private System.Windows.Forms.Label lbl_HeadWind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Glass.GlassButton gb_WindSpeedMinus;
        private Glass.GlassButton gb_WindSpeedPlus;
        private System.Windows.Forms.Label lbl_RunwayHeading;
        private System.Windows.Forms.Label lbl_WindFrom;
        private System.Windows.Forms.Label lbl_WindSpeed;
        private System.Windows.Forms.Label lbl_WindInfos;
        private Glass.GlassButton gb_SelectLanguage;
    }
}

