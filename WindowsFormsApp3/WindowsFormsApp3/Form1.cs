using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Pong : Form
    {
        bool up;
        bool down;
        public int paddlespd;
        int aimove;
        int score;
        int lives;
        int time;
        int target;
        int plv;
        int wStr;
        int newball;
        int exlife;
        int escore;
        Random rand;
        List<Dot> dot;
        public Pong()
        {
            InitializeComponent();
            paddlespd = 5;
            aimove = paddlespd;
            score = 0;
            lives = 3;
            time = 600;
            target = 50;
            plv = 50;
            wStr = 0;
            newball = 600;
            exlife = 500;
            escore = exlife;
            rand = new Random();
            dot = new List<Dot>();
        }

        private void Pong_Load(object sender, EventArgs e)
        {
            this.Text = "Ping Pong";
            player1.Left = 1;
            player1.Top = (ClientSize.Height - player1.Height) / 2;
            player2.Left = (ClientSize.Width - player2.Width) - 1;
            player2.Top = player1.Top;
            theLine.Left = (ClientSize.Width - theLine.Width) / 2;
            theLine.Top = 0;
            theLine.Height = ClientSize.Height;
            dot.Add(new Dot(this, aBall));
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void KeyOn(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) up = true;
            if (e.KeyCode == Keys.Down) down = true;
        }

        private void KeyOff(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) up = false;
            if (e.KeyCode == Keys.Down) down = false;
        }

        private void tick_Tick(object sender, EventArgs e)
        {
            this.Text = "Ping Pong (" + lives + "," + score + ")";

            for (int i = 0; i < dot.Count; i++)
            {
                dot[i].O.Top -= dot[i].DotY;
                dot[i].O.Left -= dot[i].DotX;
                if (dot[i].O.Left + dot[i].O.Width > ClientSize.Width)
                {
                    //dot[i].O.Visible = false;
                    //dot.RemoveAt(i);
                    dot[i].DotX = (paddlespd - 1);
                    dot[i].O.Left = ClientSize.Width - dot[i].O.Width - player2.Width - rand.Next(4, 9);
                    ++wStr;
                    score += Convert.ToInt32(Math.Log(1 + wStr, 2));
                }
                if (dot[i].O.Left <= 0)
                {
                    dot[i].O.Visible = false;
                    dot.RemoveAt(i);
                    wStr = 0;
                }
            }
            if (dot.Count == 0)
            {
                if (--lives <= 0)
                {
                    tick.Stop();
                    this.Text = "Ping Pong (" + score + ") - Game Over";
                    MessageBox.Show("Game Over");
                }
                else
                {
                    paddlespd *= -1;
                    dot.Add(new Dot(this, aBall));
                    paddlespd *= -1;
                    time = 600;
                }
            }

            player2.Top += aimove;
            if (player2.Top <= 0 || player2.Top > ClientSize.Height - player2.Height) aimove *= -1;

            for (int i = 0; i < dot.Count; i++)
            {
                if (dot[i].O.Top < 0) dot[i].DotY = -(paddlespd - 1);
                if (dot[i].O.Top + dot[i].O.Height > ClientSize.Height) dot[i].DotY = (paddlespd - 1);
                if (dot[i].O.Bounds.IntersectsWith(player1.Bounds))
                {
                    score++;
                    dot[i].DotX = -(paddlespd - 1);
                }
                if (dot[i].O.Bounds.IntersectsWith(player2.Bounds)) dot[i].DotX = (paddlespd - 1);
            }
            if (--time < 0)
            {
                dot.Add(new Dot(this, aBall));
                time = newball;
                newball += 6;
            }

            if (up && player1.Top > 0) player1.Top -= paddlespd;
            if (down && player1.Top < ClientSize.Height - player1.Height) player1.Top += paddlespd;
            if (score >= target)
            {
                target += plv;
                if (tick.Interval > 1)
                {
                    tick.Interval--;
                    if (tick.Interval % 2 == 0) plv += 10;
                }
                else
                {
                    paddlespd++;
                    aimove /= Math.Abs(aimove);
                    aimove *= paddlespd;
                }
            }
            if (score >= escore)
            {
                escore += exlife;
                exlife += 100;
            }
        }
    }
}
