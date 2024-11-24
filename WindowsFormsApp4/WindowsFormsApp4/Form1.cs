using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Breaker : Form
    {
        List<Block> blocks;
        Random rand;
        public int boxes = 100;
        public int cubes = 10;
        public int lv = 5;
        public int time = 0;
        int d = 100;
        public int p = 100;
        public bool n = false;
        byte s = 0;
        bool l = false;
        bool r = false;
        Paddle paddle;
        Dot dot;
        public Breaker()
        {
            InitializeComponent();
            rand = new Random();
            blocks = new List<Block>();
            paddle = new Paddle(this);
            dot = new Dot(this, smiley, paddle);
            DoubleBuffered = true;
            delta.Interval = 28;
            this.Text = "Block Breaker";
        }

        private void Breaker_Load(object sender, EventArgs e)
        {
            MakeBoxes();
        }
        private void MakeBoxes()
        {
            cubes = boxes / 4;
            for (int i = 0; i < boxes; i++)
            {
                blocks.Add(new Block(this));
                Thread.Sleep(8);
            }
            this.Text = "Block Breaker (" + dot.lives + ")";
        }
        private void restat()
        {
            boxes = rand.Next(lv * 16, (lv + 2) * 16);
            time = 0;
            d = 100;
            p = 100;
            n = false;
            s = 0;
            l = false;
            r = false;
            MakeBoxes();
            paddle.rePos();
            dot.init();
            dot.replay();
            Score.Reset();
            Score.EndGame = false;
            delta.Interval = 28;
            this.Text = "Block Breaker (" + dot.lives + ")";
        }
        private void clearBlocks()
        {
            foreach(var b in Controls.OfType<PictureBox>().Where(t => t.Tag == "Block"))
            {
                Controls.Remove(b);
            }
        }
        private void Move(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) l = true;
            if (e.KeyCode == Keys.Right) r = true;
            if (e.KeyCode == Keys.Up) s |= 1;
            if (e.KeyCode == Keys.Down) s |= 8;
            if (e.KeyCode == Keys.ShiftKey) s |= 2;
            if (e.KeyCode == Keys.Space)
            {
                if (Score.EndGame)
                {
                    while (Controls.OfType<PictureBox>().Where(t => t.Tag == "Block").Count() > 0) clearBlocks();
                    restat();
                    return;
                }
                else Pause();
                
            }
        }

        private void Frame(object sender, EventArgs e)
        {
            paddle.MovePaddle(l,r,s);
            if (Score.EndGame)
            {
                //delta.Enabled = false;
                if (!n)
                {
                    Score.Bonus(dot.lives * boxes, this);
                    if (dot.lives > 1) dot.lives = 1;
                    this.Text += " - Game Over";
                    //return;
                }
                n = true;
                if (dot.lives >= 1)
                {
                    Pass();
                }
            }
            else
            {
                Pass();
            }
        }
        private void Pass()
        {
            dot.Rollin();
            //this.Text = "Block Breaker (" + dot.lives + "," + Score.theScore + ") " + p + " [" + Controls.OfType<PictureBox>().Where(t => t.Tag == "Block" && t.BackColor != Color.Black).Count() + "-" + Controls.OfType<PictureBox>().Where(t => t.Tag == "Block" && t.BackColor == Color.Black).Count() + "] *" + cubes + "." + dot.hp;
            if (++time >= d)
            {
                if (delta.Interval > 16)
                {
                    delta.Interval--;
                    d = delta.Interval <= 16 ? d + 120 : d + 100;
                }
                else
                {
                    p++;
                    dot.Spd = (p * 5) / 100;
                    dot.Spd = dot.Spd - (3 - dot.lives) > 3 ? dot.Spd - (3 - dot.lives) : 3;
                    paddle.Speed = (p * 3 / 4) * 6 / 100;
                    paddle.Speed += 2;
                    d += 30;
                }
            }
        }
        private void Halt(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) l = false;
            if (e.KeyCode == Keys.Right) r = false;
            if (e.KeyCode == Keys.Up) s &= 254;
            if (e.KeyCode == Keys.Down) s &= 247;
            if (e.KeyCode == Keys.ShiftKey) s &= 253;
        }
        public void Pause()
        {
            if (delta.Enabled)
            {
                delta.Enabled = false;
                if (!Score.EndGame) this.Text += " - Paused";
            }
            else
            {
                delta.Enabled = true;
                if (!Score.EndGame) dot.resume();
            }
        }
    }
}
