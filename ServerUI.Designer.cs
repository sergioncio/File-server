using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameServer
{
    partial class ServerUI 
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
            this.boardPanel = new System.Windows.Forms.Panel();
            this.pnActions = new System.Windows.Forms.Panel();
            this.btSave = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.btStart = new System.Windows.Forms.Button();
            this.pnAvailableGames = new System.Windows.Forms.Panel();
            this.listAvailableGames = new System.Windows.Forms.ListBox();
            this.lbAvailableGames = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pnActions.SuspendLayout();
            this.pnAvailableGames.SuspendLayout();
            this.SuspendLayout();
            // 
            // boardPanel
            // 
            this.boardPanel.BackColor = System.Drawing.Color.Black;
            this.boardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardPanel.Location = new System.Drawing.Point(8, 108);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(290, 180);
            this.boardPanel.TabIndex = 2;
            this.boardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.CatsAndMiceForm_Paint);
            // 
            // pnActions
            // 
            this.pnActions.Controls.Add(this.btSave);
            this.pnActions.Controls.Add(this.btStop);
            this.pnActions.Controls.Add(this.btStart);
            this.pnActions.Location = new System.Drawing.Point(8, 294);
            this.pnActions.Name = "pnActions";
            this.pnActions.Size = new System.Drawing.Size(289, 43);
            this.pnActions.TabIndex = 3;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(209, 11);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 4;
            this.btSave.Text = "SAVE";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.onBtnSave);
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(110, 11);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(75, 23);
            this.btStop.TabIndex = 1;
            this.btStop.Text = "STOP";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.onBtnStop);
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(9, 11);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "START";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.onBtnStart);
            // 
            // pnAvailableGames
            // 
            this.pnAvailableGames.Controls.Add(this.listAvailableGames);
            this.pnAvailableGames.Controls.Add(this.lbAvailableGames);
            this.pnAvailableGames.Location = new System.Drawing.Point(8, 12);
            this.pnAvailableGames.Name = "pnAvailableGames";
            this.pnAvailableGames.Size = new System.Drawing.Size(291, 90);
            this.pnAvailableGames.TabIndex = 4;
            // 
            // listAvailableGames
            // 
            this.listAvailableGames.FormattingEnabled = true;
            this.listAvailableGames.Location = new System.Drawing.Point(5, 29);
            this.listAvailableGames.Name = "listAvailableGames";
            this.listAvailableGames.Size = new System.Drawing.Size(281, 56);
            this.listAvailableGames.TabIndex = 1;
            // 
            // lbAvailableGames
            // 
            this.lbAvailableGames.AutoSize = true;
            this.lbAvailableGames.Location = new System.Drawing.Point(6, 10);
            this.lbAvailableGames.Name = "lbAvailableGames";
            this.lbAvailableGames.Size = new System.Drawing.Size(127, 13);
            this.lbAvailableGames.TabIndex = 0;
            this.lbAvailableGames.Text = "Available games in server";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(8, 343);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(289, 147);
            this.listBox1.TabIndex = 5;
            // 
            // ServerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 507);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.pnAvailableGames);
            this.Controls.Add(this.pnActions);
            this.Controls.Add(this.boardPanel);
            this.Name = "ServerUI";
            this.Text = "ServeUI";
            this.Load += new System.EventHandler(this.ServerUI_Load);
            this.pnActions.ResumeLayout(false);
            this.pnAvailableGames.ResumeLayout(false);
            this.pnAvailableGames.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel boardPanel;
        private System.Windows.Forms.Panel pnActions;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Panel pnAvailableGames;
        private System.Windows.Forms.ListBox listAvailableGames;
        private System.Windows.Forms.Label lbAvailableGames;


        private Timer MyTimer = new Timer();
        private IGameLogic game;
        private int lastGameId = 0;
        private Server provider;
        private List<List<IGameItem>> gameBuffer;
        private List<string> storedGames = new List<string>();

        private void ServerUI_Load(object sender, System.EventArgs e)
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
        }



        private void Timer_Tick(object sender, System.EventArgs e)
        {
            executeGameStep();
        }

        private void onBtnStart(object sender, EventArgs e)
        {
            Console.WriteLine("ServerUI:onBtnStart: ... ");
            StartGame();
        }

        private void onBtnStop(object sender, EventArgs e)
        {
            if (game != null)
            {
                game.GameOver();
            }
        }

        private void onBtnSave(object sender, EventArgs e)
        {
            //printCurrentGame();
            provider.LoadGame(gameBuffer, ("Game_" + lastGameId));            
            storedGames = provider.GetGameList();
            listAvailableGames.DataSource = storedGames;           
        }

        public void printCurrentGame()
        {
            int counter = 0;

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("ServerUI: CurrentGame ---> Game_" + lastGameId);
            Console.WriteLine();
            foreach (var frame in gameBuffer)
            {
                foreach (var i in frame)
                {
                    Console.Write("Frame " + counter + " ---> " + i);
                }
                Console.WriteLine();
                counter++;
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
        }


        public void SetServer(Server svc)
        {
            this.provider = svc;
        }

        private void StartGame()
        {
            Console.WriteLine("ServerUI:StartGame: Entering");
            if (game == null)
            {
                Console.WriteLine("ServerUI:StartGame: Creating new game");
                game = new GameLogic(18, 29);
            }
            else if (!game.IsOver())
            {
                Console.WriteLine("ServerUI:StartGame: Failed: There's a game running currenlty.");
                return;
            }
            gameBuffer = new List<List<IGameItem>>();
            lastGameId++;
            game.StartGame();
            MyTimer.Start();           
        }

        private void executeGameStep() {

            if (game == null)
                return;

            if (!game.IsOver())
            {
                game.ExecuteStep();
                storeFrame();
            }
            else
            {
                System.Console.WriteLine("Game is over");
                MyTimer.Stop();
            }
            Refresh();
        }

        private void storeFrame()
        {
            List<IGameItem> items = new List<IGameItem>();
            foreach (var item in game)
            {
                items.Add(item.GetDeepCopy());
            }
            gameBuffer.Add(items);
        }

        private void CatsAndMiceForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            if (game == null)
            {
                return;
            }

            IEnumerator<IGameItem> enumerator = game.GetEnumerator();

            foreach (var item in game)
            {
                if (item != null)
                {
                    ViewFactory.GetView(item, 10).Draw(e);
                }
            }
        }
        private ListBox listBox1;
    }
}