namespace WindowsFormsApp2
{
    partial class Bubbles
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
            this.labelin = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.namebtn = new System.Windows.Forms.Button();
            this.rbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelin
            // 
            this.labelin.AutoSize = true;
            this.labelin.Location = new System.Drawing.Point(18, 20);
            this.labelin.Name = "labelin";
            this.labelin.Size = new System.Drawing.Size(0, 16);
            this.labelin.TabIndex = 0;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(67, 383);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(100, 22);
            this.username.TabIndex = 1;
            // 
            // namebtn
            // 
            this.namebtn.Location = new System.Drawing.Point(92, 278);
            this.namebtn.Name = "namebtn";
            this.namebtn.Size = new System.Drawing.Size(75, 23);
            this.namebtn.TabIndex = 2;
            this.namebtn.Text = "button1";
            this.namebtn.UseVisualStyleBackColor = true;
            this.namebtn.Click += new System.EventHandler(this.ebtn_Click);
            // 
            // rbtn
            // 
            this.rbtn.Location = new System.Drawing.Point(193, 315);
            this.rbtn.Name = "rbtn";
            this.rbtn.Size = new System.Drawing.Size(75, 23);
            this.rbtn.TabIndex = 3;
            this.rbtn.Text = "button1";
            this.rbtn.UseVisualStyleBackColor = true;
            this.rbtn.Click += new System.EventHandler(this.Reboot);
            // 
            // Bubbles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rbtn);
            this.Controls.Add(this.namebtn);
            this.Controls.Add(this.username);
            this.Controls.Add(this.labelin);
            this.Name = "Bubbles";
            this.Text = "Bubble Poppers";
            this.Load += new System.EventHandler(this.Bubbles_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Bmake);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDn);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelin;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Button namebtn;
        private System.Windows.Forms.Button rbtn;
    }
}

