using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameServer
{
    partial class ClientUI
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
            this.pnRequestServices = new System.Windows.Forms.Panel();
            this.listBoxGamesToDownload = new System.Windows.Forms.ListBox();
            this.lbGamesToDownload = new System.Windows.Forms.Label();
            this.btRqGanesList = new System.Windows.Forms.Button();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pnRequestServices.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnRequestServices
            // 
            this.pnRequestServices.Controls.Add(this.listBoxGamesToDownload);
            this.pnRequestServices.Controls.Add(this.lbGamesToDownload);
            this.pnRequestServices.Location = new System.Drawing.Point(200, 12);
            this.pnRequestServices.Name = "pnRequestServices";
            this.pnRequestServices.Size = new System.Drawing.Size(167, 196);
            this.pnRequestServices.TabIndex = 0;
            // 
            // listBoxGamesToDownload
            // 
            this.listBoxGamesToDownload.FormattingEnabled = true;
            this.listBoxGamesToDownload.Location = new System.Drawing.Point(13, 24);
            this.listBoxGamesToDownload.Name = "listBoxGamesToDownload";
            this.listBoxGamesToDownload.Size = new System.Drawing.Size(140, 160);
            this.listBoxGamesToDownload.TabIndex = 4;
            this.listBoxGamesToDownload.SelectedIndexChanged += new System.EventHandler(this.OnSelectedGameToDownloadChanged);
            // 
            // lbGamesToDownload
            // 
            this.lbGamesToDownload.AutoSize = true;
            this.lbGamesToDownload.Location = new System.Drawing.Point(10, 8);
            this.lbGamesToDownload.Name = "lbGamesToDownload";
            this.lbGamesToDownload.Size = new System.Drawing.Size(96, 13);
            this.lbGamesToDownload.TabIndex = 3;
            this.lbGamesToDownload.Text = "Game to download";
            // 
            // btRqGanesList
            // 
            this.btRqGanesList.Location = new System.Drawing.Point(13, 9);
            this.btRqGanesList.Name = "btRqGanesList";
            this.btRqGanesList.Size = new System.Drawing.Size(140, 33);
            this.btRqGanesList.TabIndex = 2;
            this.btRqGanesList.Text = "Update Games List";
            this.btRqGanesList.UseVisualStyleBackColor = true;
            this.btRqGanesList.Click += new System.EventHandler(this.OnRequesGameList);
            // 
            // textOutput
            // 
            this.textOutput.Location = new System.Drawing.Point(11, 226);
            this.textOutput.Multiline = true;
            this.textOutput.Name = "textOutput";
            this.textOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textOutput.Size = new System.Drawing.Size(355, 165);
            this.textOutput.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.btRqGanesList);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(167, 196);
            this.panel1.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 33);
            this.button2.TabIndex = 1;
            this.button2.Text = "Enqueue Game Request";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnEnqueueGameRequest);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 38);
            this.button3.TabIndex = 0;
            this.button3.Text = "Get Game Synchronous";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnRequestGameSync);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 33);
            this.button1.TabIndex = 3;
            this.button1.Text = "Get Game ASync";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnExecuteAsynCall);
            // 
            // ClientUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 403);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.pnRequestServices);
            this.Name = "ClientUI";
            this.Text = "Cliente";
            this.pnRequestServices.ResumeLayout(false);
            this.pnRequestServices.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnRequestServices;
        private System.Windows.Forms.Button btRqGanesList;

        private Server svc;
        List<String> listaServidor = new List<string>();
        List<String> listaPorReproducir = new List<string>();
        List<String> listaReproducidos = new List<string>();
        private string gameName;
        List<string> pendingServices = new List<string>();
        List<IGameItem> frameToReproduce;
               
        public void SetServer(Server svc)
        {
            this.svc = svc;
        }
        
        /// <summary>
        /// Selecciona el juego a descargar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSelectedGameToDownloadChanged(object sender, EventArgs e)
        {
            if (listBoxGamesToDownload.SelectedIndex != -1)
            {
                gameName = listBoxGamesToDownload.SelectedValue.ToString();
                Console.WriteLine("Cl: game SElected: " + gameName);
                addLineToTextArea("Cl: game SElected: " + gameName);
            }
        }

        public void OnRequesGameList(object sender, EventArgs e)
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Cl: RqGameList");
            addLineToTextArea("--------------------------");
            addLineToTextArea("Cl: RqGameList");
            if (svc != null)
            {
                listaServidor = svc.GetGameList();
                listBoxGamesToDownload.DataSource = listaServidor;
                foreach (var i in listaServidor)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("Cl: RqGameList");
            Console.WriteLine("--------------------------");
            addLineToTextArea("Cl: RqGameList");
            addLineToTextArea("--------------------------");
        }

        public void OnRequestGameSync(object sender, EventArgs e)
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Client: RqGameSync: " + gameName);
            addLineToTextArea("--------------------------");
            addLineToTextArea("RqGameSync: " + gameName);
            if (gameName != null)
            {                              
                if (svc != null)
                {
                    List<List<IGameItem>> buf = svc.GetGame(gameName);
                    foreach(var i in buf)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach(var j in i)
                        {
                            sb.Append(j);                           
                        }
                        addLineToTextArea(sb.ToString());
                        Console.WriteLine(sb);
                    }
                }
            }
            else
            {
                Console.WriteLine("Client: RqGameSync: No game selected ");
                addLineToTextArea("RqGameSync: No game selected ");
            }
            Console.WriteLine("Client: RqGameSync: End");
            Console.WriteLine("--------------------------");
            addLineToTextArea("RqGameSync: End");
            addLineToTextArea("--------------------------");
        }

        /// <summary>
        /// Manejador de peticiones de servicio. Para usar en IssueGameRequest.
        /// </summary>
        /// <param name="request"></param>
        public void Handler(GameRequestTask request)
        {
            Console.WriteLine("ClientUI: handler: id ---> " + request.RequestId + " has been completed. ");
            List < List < IGameItem >> lista = request.getResult();
            ReproducerUI gui = new ReproducerUI();
            gui.SetBuffer(request.getResult());
            gui.ShowDialog();
        }

        public void OnEnqueueGameRequest(object sender, EventArgs e)
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Cl: Enqueing: " + gameName);
            addLineToTextArea("--------------------------");
            addLineToTextArea("Enqueing: " + gameName);

            // Encole peticiones de servicio (IssueGameRequest) y 
            // suscríba Handler a las mismas.

            // Añada el código aquí ...
            svc.IssueGameRequest(gameName, this.Handler);
            

            Console.WriteLine("Cl: Enqueing: End");
            Console.WriteLine("--------------------------");
            addLineToTextArea("Enqueing: End");
            addLineToTextArea("--------------------------");
        }

        public async void OnExecuteAsynCall(object sender, EventArgs e)
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Cl: RqGameASYNC: " + gameName);
            addLineToTextArea("--------------------------");
            addLineToTextArea("RqGameASYNC: " + gameName);

            // Obtenga un juego invocando GetGameAsync.
            // Cuando el juego este listo reprodúzcalo.

            // Añada el código aquí ...
            List<List<IGameItem>> game = await svc.GetGameAsync(gameName);
            ReproducerUI gui = new ReproducerUI();
            gui.SetBuffer(game);
            gui.ShowDialog();

            Console.WriteLine("Cl: RqGameASYNC: End");
            Console.WriteLine("--------------------------");
            addLineToTextArea("RqGameASYNC: End");
            addLineToTextArea("--------------------------");
        }












        private System.Windows.Forms.Label lbGamesToDownload;
        private System.Windows.Forms.TextBox textOutput;
        private System.Windows.Forms.ListBox listBoxGamesToDownload;

        private void addLineToTextArea(String s)
        {
            textOutput.AppendText(s + "\n");
        }
        
        public void updateView(List<IGameItem> frame)
        {
            frameToReproduce = frame;
            Refresh();
        }

        private Panel panel1;
        private Button button2;
        private Button button3;
        private Button button1;
    }
}
