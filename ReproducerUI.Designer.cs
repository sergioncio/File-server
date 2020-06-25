using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameServer
{
    partial class ReproducerUI
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 180);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Reproducer_Paint);
            // 
            // ReproducerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 205);
            this.Controls.Add(this.panel1);
            this.Name = "ReproducerUI";
            this.Text = "Reproducer";
            this.Load += new System.EventHandler(this.ReproducerUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        List<List<IGameItem>> frames;
        int frameCounter = 0;

        string myName = "No Name";

        public void setId(string gameId)
        {
            myName = gameId;
        }

        public void SetBuffer(List<List<IGameItem>> frames)
        {
            Console.WriteLine("Reproducer: SetBuffer: Frames ---> " + frames.Count);

            this.frames = frames;
            MyTimer.Start();
        }


        private Timer MyTimer = new Timer();

        private void ReproducerUI_Load(object sender, System.EventArgs e)
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);

            this.UpdateStyles();

            MyTimer.Interval = 100;
            MyTimer.Enabled = true;
            MyTimer.Tick += new EventHandler(Timer_Tick);
            //MyTimer.Start();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            Console.WriteLine("Reproducer: TimerTick Handler ");
            if (frames == null)
            {
                Console.WriteLine("Reproducer: Disposing from TimerTick Handler ");
                Dispose();
            }
            Refresh();
        }

        private void Reproducer_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (frames != null)
            {
                if (frameCounter < frames.Count)
                {
                    List<IGameItem> frame = frames[frameCounter];
                    foreach (var item in frame)
                    {
                        if (item != null)
                        {
                            ViewFactory.GetView(item, 10).Draw(e);
                        }
                    }
                    frameCounter++;
                }
                else
                {
                    MyTimer.Stop();
                    Console.WriteLine("Reproducer: Disposing " + frameCounter);
                    Dispose();
                }
            }
            else
            {
                Console.WriteLine("Reproducer: Disposing " + frameCounter);
                Dispose();
            }
        }
    }
}