using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public class Paddle
    {
        public int Speed { get; set; }
        public List<PictureBox> thePaddle { get; set; }
        private Breaker win;
        public Paddle(Breaker win)
        {
            this.win = win;
            Speed = 6;
            thePaddle = new List<PictureBox>();
            Go();
        }
        private void Go()
        {
            //for (int i = 0; i < 3; i++)
            //{
            thePaddle.Add(new PictureBox()
            {
                BackColor = Color.Aqua,
                Height = 16,
                Visible = true,
                Width = 96,
                Top = win.ClientSize.Height - 20,
                Left = (win.ClientSize.Width - 96) / 2
            });
            //if (i == 0) thePaddle[i].Left -= 32;
            //if (i == 2) thePaddle[i].Left += 32;
            win.Controls.Add(thePaddle[0]);
            //}
        }
        public void rePos()
        {
            thePaddle[0].Top = win.ClientSize.Height - 20;
            thePaddle[0].Left = (win.ClientSize.Width - 96) / 2;
            Speed = 6;
        }
        public void MovePaddle(bool left, bool right, byte mod)
        {
            var a = mod & 2;
            if (a != 0) a = 3;
            else
            {
                a = mod & 9;
                if (a <= 8) a = -1;
            }
            //for (int i= 0; i < 3; i++)
            //{
            if (left) thePaddle[0].Left -= (Speed + a);
            if (right) thePaddle[0].Left += (Speed + a);
            //}
            PaddleEdge();
        }
        private void PaddleEdge()
        {
            if (thePaddle[0].Left < 1)
            {
                thePaddle[0].Left = 1;
                //thePaddle[1].Left = thePaddle[0].Width;
                //thePaddle[2].Left = thePaddle[0].Width + thePaddle[1].Width;
            }
            else if (thePaddle[0].Left > win.ClientSize.Width - thePaddle[0].Width - 1)
            {
                thePaddle[0].Left = win.ClientSize.Width - thePaddle[0].Width - 1;
                //thePaddle[1].Left = 0;
                //thePaddle[2].Left = 0;
            }
        }
    }
}
