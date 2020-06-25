using System;
using System.Windows.Forms;

namespace GameServer
{
    partial class Launcher

    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btStartServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btStartServer
            // 
            this.btStartServer.Location = new System.Drawing.Point(12, 15);
            this.btStartServer.Name = "btStartServer";
            this.btStartServer.Size = new System.Drawing.Size(257, 43);
            this.btStartServer.TabIndex = 1;
            this.btStartServer.Text = "Start Application";
            this.btStartServer.UseVisualStyleBackColor = true;
            this.btStartServer.Click += new System.EventHandler(this.OnBtStartServer);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 66);
            this.Controls.Add(this.btStartServer);
            this.Name = "Launcher";
            this.Text = "Launcher";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btStartServer;

        Server svc;

        public void OnBtStartServer(object sender, EventArgs eventArgs)
        {
            if (svc == null)
            {
                Console.WriteLine("Starting Client");
                svc = new Server();

                Form serverui = new ServerUI();
                ((ServerUI)serverui).SetServer(svc);
                serverui.Show();

                Console.WriteLine("Starting Client");
                ClientUI Client = new ClientUI();
                Client.SetServer(svc);
                Client.Show();
            }
        }
    }
}

