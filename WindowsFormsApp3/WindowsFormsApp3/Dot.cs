using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    internal class Dot
    {
        public int DotX { get; set; }
        public int DotY { get; set; }
        Pong pong;
        private Random rand;
        public readonly PictureBox O;
        public Dot(Pong pong, PictureBox pic)
        {
            this.pong = pong;
            rand = new Random();
            DotX = pong.paddlespd - 1;
            DotY = pong.paddlespd - 1;
            O = new PictureBox
            {
                Image = pic.Image,
                Size = pic.Size,
                Visible = true,
                Left = pong.ClientSize.Width / 2,
                Top = rand.Next(pong.ClientSize.Height)
            };
            pong.Controls.Add(O);
        }
    }
}
