using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public class Dot
    {
        public int Spd { get; set; }
        public int sX { get; set; }
        public int sY { get; set; }
        public int SpdOffset { get; set; }
        private Random rand = new Random();
        private PictureBox dot;
        private Breaker win;
        private Paddle pad;
        private Block b;
        public int lives;
        public int hp;
        bool bs = false;
        int str = 0;
        byte ls = 0;
        public Dot(Breaker win, PictureBox box, Paddle pad)
        {
            this.win = win;
            this.pad = pad;
            init();
            this.dot = new PictureBox()
            {
                Width = box.Width,
                Height = box.Height,
                Visible = true,
                Image = box.Image,
                Left = (win.ClientSize.Width - box.Width) / 2,
                Top = (win.ClientSize.Height - box.Height) / 2,
                BackColor = Color.Transparent
            };
            win.Controls.Add(dot);
        }
        public void init()
        {
            SpdOffset = 0;
            Spd = 5;
            sX = rand.Next(-4, 5);
            sY = 5;
            lives = 3;
            hp = 0;
        }
        public void replay()
        {
            dot.Left = (win.ClientSize.Width - dot.Width) / 2;
            dot.Top = (win.ClientSize.Height - dot.Height) / 2;
            dot.Visible = true;
        }
        public void Rollin()
        {
            if (!dot.Visible) return;
            if (win.Controls.OfType<PictureBox>().Where(t => t.Tag == "Block" && t.BackColor != Color.Black).Count() == 0 && !Score.EndGame)
            {
                Score.EndGame = true;
                ls = 0;
                if (win.lv < 16) win.lv++;
                win.Pause();
                MessageBox.Show("Game Complete!");
                win.Pause();
                //return;
            }
            if (win.Controls.OfType<PictureBox>().Where(t => t.Tag == "Block").Count() == 0) bs = true;
            if (sX < 0) dot.Left += sX - SpdOffset;
            else if (sX > 0) dot.Left += sX + SpdOffset;
            if (sY < 0) dot.Top += sY - SpdOffset;
            else if (sY > 0) dot.Top += sY + SpdOffset;
            foreach (var item in win.Controls.OfType<PictureBox>().Where(t => t.Tag == "Block"))
            {
                if (dot.Bounds.IntersectsWith(item.Bounds))
                {
                    if (!Score.EndGame) Score.CalcScore(lives, item, win);
                    if (Math.Abs(sX) < 3)
                    {
                        if (dot.Left + (dot.Width / 2) < item.Left + (item.Width / 2)) sX = -Math.Abs(sX) - 1;
                        else sX = Math.Abs(sX) + 1;
                    }
                    else
                    {
                        if (Math.Abs(item.Left - dot.Right) <= Spd) sX = -Math.Abs(sX);
                        if (Math.Abs(item.Right - dot.Left) <= Spd) sX = Math.Abs(sX);
                    }
                    if (Math.Abs(item.Top - dot.Bottom) <= Spd) sY = -Math.Abs(sY);
                    if (Math.Abs(item.Bottom - dot.Top) <= Spd) sY = Math.Abs(sY);
                    if (item.BackColor != Color.Black)
                    {
                        win.Controls.Remove(item);
                        hp = hp > 1 ? --hp : 0;
                    }
                    else if (!Score.EndGame)
                    {
                        hp += (win.p / 100);
                        if (hp >= 10)
                        {
                            Score.BlackScore(lives, win);
                            win.Controls.Remove(item);
                            hp /= 5;
                        }
                    }
                    else
                    {
                        Score.BlackScore(lives, win);
                        win.Controls.Remove(item);
                    } 
                }
            }
            if (dot.Bounds.IntersectsWith(pad.thePaddle[0].Bounds))
            {
                sY = -Spd;
                sX = dot.Left + (dot.Width / 2) - pad.thePaddle[0].Left;
                if (sX < 1) sX = -Spd;
                else
                {
                    sX = sX * 20;
                    sX = sX % 10 < 5 ? sX / pad.thePaddle[0].Width : (sX / pad.thePaddle[0].Width) + 1;
                    sX = sX * Spd;
                    sX = sX % 10 < 5 ? sX / 10 : (sX / 10) + 1;
                    sX -= Spd;
                }
                if (bs)
                {
                    str++;
                    Score.Bonus(1, win);
                    win.Text += " - Game Over";
                }
            }
            else if (dot.Bottom > win.ClientSize.Height)
            {
                lives--;
                hp /= 2;
                if (!Score.EndGame) Score.CalcScore(lives, dot, win);
                if (lives < 1)
                {
                    dot.Visible = false;
                    Score.EndGame = true;
                    if (++ls > 2)
                    {
                        win.lv = win.lv > 1 ? win.lv - 1 : 1;
                        ls = 1;
                    }
                    win.Pause();
                    if (!win.n) MessageBox.Show("Game Over!");
                    win.Pause();
                    if (bs)
                    {
                        bs = false;
                        str = Convert.ToInt32((Math.Log10(10 + str) - 1) * Math.Floor((double)win.p / 10));
                        Score.Bonus(str, win);
                        str = 0;
                        win.Text += " - Game Over";
                    }
                    return;
                }
                win.p = win.p / 10 > 50 ? win.p - (win.p * 50 / 100) : win.p - (win.p * (win.p / 10) / 100);
                if (win.p < 50) win.p = 50;
                Spd = (win.p * 5) / 100;
                Spd = Spd - (3 - lives) > 3 ? Spd - (3 - lives) : 3;
                pad.Speed = (win.p * 3 / 4) * 6 / 100;
                pad.Speed = pad.Speed + 2 > 5 ? pad.Speed + 2 : 5;
                win.time -= (200 + ((win.p - 100) / 5));
                if (win.time < 0) win.time = 0;
                //dot.Left = (win.ClientSize.Width - dot.Width) / 2;
                dot.Left = pad.thePaddle[0].Left + (pad.thePaddle[0].Width / 2);
                dot.Top = (win.ClientSize.Height - dot.Height) / 2;
                sX = rand.Next(-4, 4);
                sY = Spd;
            }
            else if (dot.Top < 1)
            {
                if (Math.Abs(sX) < Spd)
                {
                    if (sX == 0)
                    {
                        //rand = new Random();
                        sX = rand.Next(-1, 2);
                    }
                    else sX = sX < 0 ? --sX : ++sX;
                }
                sY = Spd;
            }
            else if (dot.Left < 1)
            {
                if (Math.Abs(sX) < Spd) sX = Math.Abs(sX) + 1;
                else sX = Spd;
            }
            else if (dot.Right >= win.ClientSize.Width - 1)
            {
                if (Math.Abs(sX) < Spd) sX = -Math.Abs(sX) - 1;
                else sX = -Spd;
            }
            //var b = 0;
            //foreach(var item in win.Controls.OfType<PictureBox>())
            //{
            //    if (item.Tag == "Block") b++;
            //}
            //if (b == 0)
            //{
            //    Score.EndGame = true;
            //    MessageBox.Show("Game Complete!");
            //    return;
            //}
        }
        public void resume()
        {
            Score.CalcScore(lives, dot, win);
        }
    }
}
