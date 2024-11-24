using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Bubbles : Form
    {
        enum Colors
        {
            Black,
            Red,
            Blue,
            Green,
            Yellow,
            Purple,
            Cyan,
            Orange,
            Pink,
            Lime
        };
        const int bcount = 16;
        const int bsize = 32;
        Colors[,] color;
        Random rand;
        int pts;
        bool[,] isSeld;
        int selectedBubs;
        HiScores score;
        public Bubbles()
        {
            InitializeComponent();
            rand = new Random();
            selectedBubs = 0;
            pts = 0;
            color = new Colors[bcount, bcount];
            isSeld = new bool[bcount, bcount];
            labelin.BackColor = Color.White;
        }

        private void Bubbles_Load(object sender, EventArgs e)
        {
            Boot();
        }
        private void Boot()
        {
            SetClientSizeCore(bcount * bsize, bcount * bsize);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.Black;
            DoubleBuffered = true;
            username.Visible = false;
            namebtn.Visible = false;
            rbtn.Visible = false;
            namebtn.Text = "Sign Your Name";
            rbtn.Text = "Replay";
            username.Width = ClientSize.Width < 100 ? ClientSize.Width : ClientSize.Width / 2;
            username.Height = 30;
            username.Location = new Point((ClientSize.Width - username.Width) / 2, username.Height);
            namebtn.Width = ClientSize.Width < 100 ? ClientSize.Width : ClientSize.Width / 2;
            namebtn.Height = 30;
            namebtn.Location = new Point((ClientSize.Width - namebtn.Width) / 2, namebtn.Height + bsize);
            rbtn.Width = username.Width;
            rbtn.Height = 32;
            rbtn.Location = username.Location;
            Begin();
        }
        private void EndGame()
        {
            this.Text = "Bubble Poppers (" + pts + ") - Game Over";
            MessageBox.Show("No More Moves!");
            score = new HiScores(pts, bcount);
            StringBuilder sb = new StringBuilder();
            sb.Append("Game Over!\n");
            sb.Append("Your Score: ");
            sb.Append(score.Score + "\n");
            sb.Append(score.GetScoreMsg());
            MessageBox.Show(sb.ToString());
            if (score.GetPlayerscPos() <= 16)
            {
                username.Visible = true;
                namebtn.Visible = true;
                while(namebtn.Visible == true)
                {
                    Application.DoEvents();
                }
            } 
            sb.Clear();
            sb.Append("High Scores:\n\n");
            sb.Append(score.GetHiScores());
            MessageBox.Show(sb.ToString());
            sb.Clear();
            rbtn.Visible = true;
        }
        private void Begin()
        {
            for(int y = 0; y < bcount; y++)
            {
                for(int x = 0;x < bcount; x++)
                {
                    color[y, x] = (Colors)rand.Next(1, 4);
                }
            }
            this.Text = "Bubble Poppers";
        }

        private void Bmake(object sender, PaintEventArgs e)
        {
            for (int y = 0; y < bcount; y++)
            {
                for (int x = 0; x < bcount; x++)
                {
                    Color bcolor = Color.Empty;
                    var isBubble = true;
                    switch (color[y, x])
                    {
                        case Colors.Red:
                            bcolor = Color.Red;
                            break;
                        case Colors.Blue:
                            bcolor = Color.Blue;
                            break;
                        case Colors.Green:
                            bcolor = Color.Green;
                            break;
                        case Colors.Yellow:
                            bcolor = Color.Yellow;
                            break;
                        case Colors.Purple:
                            bcolor = Color.Purple;
                            break;
                        case Colors.Cyan:
                            bcolor = Color.Cyan;
                            break;
                        case Colors.Orange:
                            bcolor = Color.Orange;
                            break;
                        case Colors.Pink:
                            bcolor = Color.Pink;
                            break;
                        case Colors.Lime:
                            bcolor = Color.Lime;
                            break;
                        default:
                            e.Graphics.FillRectangle(Brushes.Black, x * bsize, y * bsize, bsize, bsize);
                            isBubble = false;
                            break;
                    }
                    if (isBubble)
                    {
                        e.Graphics.FillEllipse(new LinearGradientBrush(new Point(y * bsize, x * bsize), new Point(y * bsize + bsize, x * bsize + bsize), Color.White, bcolor), x * bsize, y * bsize, bsize, bsize);
                        if (isSeld[y, x])
                        {// left, right, top, bottom
                            if (x > 0 && color[y, x] != color[y, x - 1]) e.Graphics.DrawLine(Pens.White, x * bsize, y * bsize, x * bsize, y * bsize + bsize);
                            if (x < bcount - 1 && color[y, x] != color[y, x + 1]) e.Graphics.DrawLine(Pens.White, x * bsize + bsize, y * bsize, x * bsize + bsize, y * bsize + bsize);
                            if (y > 0 && color[y, x] != color[y - 1, x]) e.Graphics.DrawLine(Pens.White, x * bsize, y * bsize, x * bsize + bsize, y * bsize);
                            if (y < bcount - 1 && color[y, x] != color[y + 1, x]) e.Graphics.DrawLine(Pens.White, x * bsize, y * bsize + bsize, x * bsize + bsize, y * bsize + bsize);
                        }
                    }
                }
            }
        }

        private void MouseDn(object sender, MouseEventArgs e)
        {
            var x = Convert.ToInt32(e.X/bsize);
            var y = Convert.ToInt32(e.Y/bsize);
            if (isSeld[y, x] && selectedBubs > 1)
            {
                pts += Convert.ToInt32(labelin.Text);
                this.Text = "Bubble Poppers (" + pts + ")";
                PopBubbles();
                ClearSeld();
                PushDown();
                PushLeft();
                if (!HasMoves())
                {
                    EndGame();
                }
            }
            else
            {
                ClearSeld();
                if (color[y, x] > Colors.Black)
                {
                    SelOthers(y, x);
                    this.Invalidate();
                    Application.DoEvents();
                    if (selectedBubs > 1)
                    {
                        SetLabel(selectedBubs, x, y);
                        MouseDn(sender, e);
                    }
                }
            }
        }
        private void PopBubbles()
        {
            for (int y = 0; y < bcount; y++)
            {
                for (int x = 0; x < bcount; x++)
                {
                    if (isSeld[y, x]) color[y, x] = Colors.Black;
                }
            }
            this.Invalidate();
            Application.DoEvents();
        }
        private void ClearSeld()
        {
            for (int y = 0; y < bcount; y++)
            {
                for (int x = 0; x < bcount; x++)
                {
                    isSeld[y, x] = false;
                }
            }
            selectedBubs = 0;
            labelin.Visible = false;
        }
        private bool HasMoves()
        {
            for (int y = 0; y < bcount; y++)
            {
                for (int x = 0; x < bcount; x++)
                {
                    if (color[y, x] > Colors.Black)
                    {
                        if (x < bcount - 1 && color[y, x] == color[y, x + 1]) return true;
                        if (y < bcount - 1 && color[y, x] == color[y + 1, x]) return true;
                    }
                }
            }
            return false;
        }
        private void SetLabel(int bubbles, int x, int y)
        {
            var value = Math.Floor(Math.Log(bubbles, 2) * bubbles);
            value = Convert.ToInt32(value);
            labelin.Text = value.ToString();
            labelin.Left = x * bsize + bsize;
            labelin.Top = y * bsize + bsize;
            if (labelin.Left > this.ClientSize.Width / 2)
            {
                labelin.Left -= bsize;
            }
            if (labelin.Top > this.ClientSize.Height / 2)
            {
                labelin.Top -= bsize;
            }
            labelin.Visible = true;
        }

        private void PushDown()
        {
            for (int x = 0; x < bcount; x++)
            {
                var blankBubblePos = bcount - 1;
                var foundBlank = false;
                for (int y = bcount - 1; y >= 0; y--)
                {
                    if (color[y, x] == Colors.Black) foundBlank = true;
                    if (color[y, x] != Colors.Black && !foundBlank) blankBubblePos--;
                    if (color[y, x] != Colors.Black && foundBlank)
                    {
                        color[blankBubblePos, x] = color[y, x];
                        blankBubblePos--;
                    }
                }
                for (int y = blankBubblePos; y >= 0; y--)
                {
                    color[y, x] = Colors.Black;
                }
            }
            this.Invalidate();
            Application.DoEvents();
        }

        private void PushLeft()
        {
            for (int y = 0; y < bcount; y++)
            {
                var blankBubblePos = 0;
                var foundBlank = false;
                for (int x = 0; x < bcount; x++)
                {
                    if (color[y, x] == Colors.Black) foundBlank = true;
                    if (color[y, x] != Colors.Black && !foundBlank) blankBubblePos++;
                    if (color[y, x] != Colors.Black && foundBlank)
                    {
                        color[y, blankBubblePos] = color[y, x];
                        blankBubblePos++;
                    }
                }
                for (int x = blankBubblePos; x < bcount; x++)
                {
                    color[y, x] = Colors.Black;
                }
            }
            this.Invalidate();
            Application.DoEvents();
            MoreBubbles();
        }

        private void MoreBubbles()
        {
            if (color[bcount - 1, bcount - 1] == Colors.Black)
            {
                for(int y = bcount - 1; y >= 0; y--)
                {
                    if (pts < 500)
                    {
                        color[y, bcount - 1] = (Colors)rand.Next(1, 4);
                    }
                    else if (pts < 1000)
                    {
                        color[y, bcount - 1] = (Colors)rand.Next(1, 5);
                    }
                    else if (pts < 2000)
                    {
                        color[y, bcount - 1] = (Colors)rand.Next(1, 6);
                    }
                    else if (pts < 3000)
                    {
                        color[y, bcount - 1] = (Colors)rand.Next(1, 7);
                    }
                    else if (pts < 4000)
                    {
                        color[y, bcount - 1] = (Colors)rand.Next(1, 8);
                    }
                    else if (pts < 5000)
                    {
                        color[y, bcount - 1] = (Colors)rand.Next(1, 9);
                    }
                    else
                    {
                        color[y, bcount - 1] = (Colors)rand.Next(1, 10);
                    }
                }
                this.Invalidate();
                Application.DoEvents();
                PushLeft();
            }
        }

        private void SelOthers(int y, int x)
        {
            isSeld[y, x] = true;
            selectedBubs++;

            /*Loopion
            int[,] posTrack = new int[bcount, bcount];
            var posCount = 1;
            posTrack[y, x] = posCount;
            var xi = x;
            var yi = y;
            while (posCount > 0)
            {
                //go up
                if (yi > 0 && color[yi, xi] == color[yi - 1, xi] && !isSeld[yi - 1, xi])
                {
                    isSeld[yi - 1, xi] = true;
                    selectedBubs++;
                    posCount++;
                    posTrack[yi - 1, xi] = posCount;
                    yi--;
                }//go down
                else if (yi < bcount - 1 && color[yi, xi] == color[yi + 1, xi] && !isSeld[yi + 1, xi])
                {
                    isSeld[yi + 1, xi] = true;
                    selectedBubs++;
                    posCount++;
                    posTrack[yi + 1, xi] = posCount;
                    yi++;
                }//go left
                else if (xi > 0 && color[yi, xi] == color[yi, xi - 1] && !isSeld[yi, xi - 1])
                {
                    isSeld[yi, xi - 1] = true;
                    selectedBubs++;
                    posCount++;
                    posTrack[yi, xi - 1] = posCount;
                    xi--;
                }//go right
                else if (yi < bcount - 1 && color[yi, xi] == color[yi, xi + 1] && !isSeld[yi, xi + 1])
                {
                    isSeld[yi, xi + 1] = true;
                    selectedBubs++;
                    posCount++;
                    posTrack[yi, xi + 1] = posCount;
                    xi++;
                }
                else
                {
                    posCount--;
                    for (int b = 0; b < bcount; b++)
                    {
                        for (int a = 0; a < bcount; a++)
                        {
                            if (posTrack[b, a] == posCount + 1) posTrack[b, a] = 0;
                            if (posTrack[b, a] == posCount)
                            {
                                xi = a;
                                yi = b;
                            }
                        }
                    }
                }
            }*/

            //Recursion
            //go up
            if (y > 0 && color[y, x] == color[y - 1, x] && !isSeld[y - 1, x])
            {
                SelOthers(y - 1, x);
            }//go down
            if (y < bcount - 1 && color[y, x] == color[y + 1, x] && !isSeld[y + 1, x])
            {
                SelOthers(y + 1, x);
            }//go left
            if (x > 0 && color[y, x] == color[y, x - 1] && !isSeld[y, x - 1])
            {
                SelOthers(y, x - 1);
            }//go right
            if (x < bcount - 1 && color[y, x] == color[y, x + 1] && !isSeld[y, x + 1])
            {
                SelOthers(y, x + 1);
            }
        }

        private void ebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
            {
                username.Text = "No Name";
            }
            score.WriteScore(username.Text);
            username.Visible = false;
            namebtn.Visible = false;
        }

        private void Reboot(object sender, EventArgs e)
        {
            rbtn.Visible = false;
            selectedBubs = 0;
            username.Text = "";
            pts = 0;
            Boot();
        }
    }
}
