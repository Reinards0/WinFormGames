namespace WindowsFormsApp4
{
    partial class Breaker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Breaker));
            this.smiley = new System.Windows.Forms.PictureBox();
            this.delta = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.smiley)).BeginInit();
            this.SuspendLayout();
            // 
            // smiley
            // 
            this.smiley.BackColor = System.Drawing.Color.Transparent;
            this.smiley.Image = ((System.Drawing.Image)(resources.GetObject("smiley.Image")));
            this.smiley.Location = new System.Drawing.Point(600, 480);
            this.smiley.Name = "smiley";
            this.smiley.Size = new System.Drawing.Size(15, 15);
            this.smiley.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.smiley.TabIndex = 0;
            this.smiley.TabStop = false;
            this.smiley.Visible = false;
            // 
            // delta
            // 
            this.delta.Enabled = true;
            this.delta.Interval = 50;
            this.delta.Tick += new System.EventHandler(this.Frame);
            // 
            // Breaker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(638, 568);
            this.Controls.Add(this.smiley);
            this.DoubleBuffered = true;
            this.Name = "Breaker";
            this.Text = "0";
            this.Load += new System.EventHandler(this.Breaker_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Move);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Halt);
            ((System.ComponentModel.ISupportInitialize)(this.smiley)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox smiley;
        private System.Windows.Forms.Timer delta;
    }
}

