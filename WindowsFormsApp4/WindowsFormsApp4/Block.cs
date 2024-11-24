using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public class Block
    {
        private Random rand;
        private PictureBox block;
        private Breaker win;
        private List<PictureBox> blocks;
        public Block(Breaker win)
        {
            rand = new Random();
            this.win = win;
            blocks = new List<PictureBox>();
            block = new PictureBox()
            {
                BackColor = win.cubes > 0 ? colorPick(rand.Next(0, 5)) : colorPick(rand.Next(1, 5)),
                Width = 32,
                Height = 16,
                Tag = "Block",
                Visible = true
            };
            make();
        }
        private Color colorPick(int a)
        {
            Color c;
            switch (a)
            {
                case 1:
                    c = Color.Red;
                    break;
                case 2:
                    c = Color.Green;
                    break;
                case 3:
                    c = Color.Blue;
                    break;
                case 4:
                    c = Color.Yellow;
                    break;
                default:
                    c = Color.Black;
                    --win.cubes;
                    break;
            }
            return c;
        }
        private void make()
        {
            CreateBlocks();
            SetBlockPos();
            win.Controls.Add(block);
        }
        private void CreateBlocks()
        {
            foreach (var box in win.Controls.OfType<PictureBox>().Where(t => t.Tag == "Block"))
            {
                blocks.Add(box);
            }
        }
        private void SetBlockPos()
        {
            do
            {
                block.Left = rand.Next(0, 20) * 32;
                block.Top = (rand.Next(0, 16) * 16) + 8;
            } while (!CheckHitbox());
            blocks.Add(block);
        }
        private bool CheckHitbox()
        {
            for (int i = 0; i < blocks.Count; i++) 
            {
                if (this.block.Bounds.IntersectsWith(blocks[i].Bounds)) return false;
            }
            return true;
        }
    }
}
