namespace WindowsFormsApp3
{
    partial class Pong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pong));
            this.player1 = new System.Windows.Forms.PictureBox();
            this.player2 = new System.Windows.Forms.PictureBox();
            this.theLine = new System.Windows.Forms.PictureBox();
            this.aBall = new System.Windows.Forms.PictureBox();
            this.tick = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.player1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.theLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aBall)).BeginInit();
            this.SuspendLayout();
            // 
            // player1
            // 
            this.player1.BackColor = System.Drawing.Color.Cyan;
            this.player1.Location = new System.Drawing.Point(20, 80);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(10, 80);
            this.player1.TabIndex = 0;
            this.player1.TabStop = false;
            // 
            // player2
            // 
            this.player2.BackColor = System.Drawing.Color.Fuchsia;
            this.player2.Location = new System.Drawing.Point(420, 80);
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(10, 80);
            this.player2.TabIndex = 1;
            this.player2.TabStop = false;
            // 
            // theLine
            // 
            this.theLine.BackColor = System.Drawing.Color.White;
            this.theLine.Location = new System.Drawing.Point(222, 0);
            this.theLine.Name = "theLine";
            this.theLine.Size = new System.Drawing.Size(6, 240);
            this.theLine.TabIndex = 2;
            this.theLine.TabStop = false;
            // 
            // aBall
            // 
            this.aBall.Image = ((System.Drawing.Image)(resources.GetObject("aBall.Image")));
            this.aBall.Location = new System.Drawing.Point(206, 292);
            this.aBall.Name = "aBall";
            this.aBall.Size = new System.Drawing.Size(30, 30);
            this.aBall.TabIndex = 3;
            this.aBall.TabStop = false;
            this.aBall.Visible = false;
            // 
            // tick
            // 
            this.tick.Enabled = true;
            this.tick.Interval = 16;
            this.tick.Tick += new System.EventHandler(this.tick_Tick);
            // 
            // Pong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(622, 433);
            this.Controls.Add(this.aBall);
            this.Controls.Add(this.player2);
            this.Controls.Add(this.player1);
            this.Controls.Add(this.theLine);
            this.Name = "Pong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pong";
            this.Load += new System.EventHandler(this.Pong_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyOn);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyOff);
            ((System.ComponentModel.ISupportInitialize)(this.player1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.theLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aBall)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox player1;
        private System.Windows.Forms.PictureBox player2;
        private System.Windows.Forms.PictureBox theLine;
        private System.Windows.Forms.PictureBox aBall;
        private System.Windows.Forms.Timer tick;
    }
}

