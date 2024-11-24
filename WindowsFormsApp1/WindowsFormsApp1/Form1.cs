using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Snake : Form
    {
        Random rand;
        enum boardfields
        {
            Free,
            Snak,
            Food
        };
        enum controls
        {
            Up,
            Dn,
            Lt,
            Rt
        }
        struct SCoords
        {
            public int x, y;
        }
        boardfields[,] field;
        SCoords[] SnXY;
        int sLen;
        controls mov;
        Graphics gfx;
        public Snake()
        {
            InitializeComponent();
            field = new boardfields[16, 16];
            SnXY = new SCoords[196];
            rand = new Random();
        }
        private void Snake_Load(object sender, EventArgs e)
        {
            this.Text = "Classic Snake";
            pictureBoard.Image = new Bitmap(560, 560);
            gfx = Graphics.FromImage(pictureBoard.Image);
            gfx.Clear(Color.White);
            for(int i = 0; i <= 15; i++)
            {   // top & bottom walls
                gfx.DrawImage(imageList1.Images[0], i * 35, 0);
                gfx.DrawImage(imageList1.Images[0], i * 35, 525);
            }
            for (int i = 1; i <= 14; i++)
            {   // left & right walls
                gfx.DrawImage(imageList1.Images[0], 0, i * 35);
                gfx.DrawImage(imageList1.Images[0], 525, i * 35);
            }
            // Init the snake
            SnXY[0].x = 2;
            SnXY[0].y = 7;
            SnXY[1].x = 2;
            SnXY[1].y = 8;
            SnXY[2].x = 2;
            SnXY[2].y = 9;
            gfx.DrawImage(imageList1.Images[6], 70, 7 * 35);
            gfx.DrawImage(imageList1.Images[5], 70, 8 * 35);
            gfx.DrawImage(imageList1.Images[5], 70, 9 * 35);
            field[2, 7] = boardfields.Snak;
            field[2, 8] = boardfields.Snak;
            field[2, 9] = boardfields.Snak;
            mov = controls.Up;
            sLen = 3;
            for (int i = 0; i < 7; i++)
            {
                Foods();
            }
        }
        private void Foods()
        {
            int x, y;
            var p = rand.Next(1, 5);
            do
            {
                x = rand.Next(1, 14);
                y = rand.Next(1, 14);
            } while (field[x, y] != boardfields.Free);
            field[x, y] = boardfields.Food;
            gfx.DrawImage(imageList1.Images[p], x * 35, y * 35);
        }

        private void BtnDn(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (mov != controls.Dn)
                        mov = controls.Up;
                    break;
                case Keys.Down:
                    if (mov != controls.Up)
                        mov = controls.Dn;
                    break;
                case Keys.Left:
                    if (mov != controls.Rt)
                        mov = controls.Lt;
                    break;
                case Keys.Right:
                    if (mov != controls.Lt)
                        mov = controls.Rt;
                    break;
            }
        }
        private void EndGame()
        {
            timer1.Enabled = false;
            MessageBox.Show("Game Over!");
            this.Text = "Game Over";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            gfx.FillRectangle(Brushes.White, SnXY[sLen - 1].x * 35, SnXY[sLen - 1].y * 35, 35, 35);
            field[SnXY[sLen - 1].x, SnXY[sLen - 1].y] = boardfields.Free;
            pictureBoard.Refresh();
            for(int i = sLen; i >= 1; i--) // move the snake pieces
            {
                SnXY[i].x = SnXY[i - 1].x;
                SnXY[i].y = SnXY[i - 1].y;
            }
            gfx.DrawImage(imageList1.Images[5], SnXY[0].x * 35, SnXY[0].y * 35);
            //pictureBoard.Refresh();
            switch (mov) // direction change
            {
                case controls.Up:
                    SnXY[0].y--;
                    break;
                case controls.Dn:
                    SnXY[0].y++;
                    break;
                case controls.Lt:
                    SnXY[0].x--;
                    break;
                case controls.Rt:
                    SnXY[0].x++;
                    break;
            }
            if (SnXY[0].x < 1 || SnXY[0].x > 14 || SnXY[0].y < 1 || SnXY[0].y > 14) // wall collision
            {
                EndGame();
                pictureBoard.Refresh();
                return;
            }
            if (field[SnXY[0].x, SnXY[0].y] == boardfields.Snak) // self collision
            {
                EndGame();
                pictureBoard.Refresh();
                return;
            }
            if (field[SnXY[0].x, SnXY[0].y] == boardfields.Food) // eat food
            {
                gfx.DrawImage(imageList1.Images[5], SnXY[sLen].x * 35, SnXY[sLen].y * 35);
                field[SnXY[sLen].x, SnXY[sLen].y] = boardfields.Snak;
                sLen++;
                if (sLen < 190) Foods();
                else
                {
                    timer1.Enabled = false;
                    this.Text = "Game Complete";
                    return;
                }
                this.Text = "Classic Snake (" + (sLen - 3) + ")";
            }
            gfx.DrawImage(imageList1.Images[6], SnXY[0].x * 35, SnXY[0].y * 35);
            field[SnXY[0].x, SnXY[0].y] = boardfields.Snak;
            pictureBoard.Refresh();
        }
    }
}
