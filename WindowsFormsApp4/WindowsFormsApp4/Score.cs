using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public static class Score
    {
        public static int total {  get; set; }
        public static int theScore { get { return total; } }
        public static bool EndGame { get; set; }
        public static void CalcScore(int left, PictureBox block, Breaker win)
        {
            if (block.BackColor == Color.Red) total = win.p / 100 < 1 ? total + 1 : total + (win.p / 100);
            else if (block.BackColor == Color.Green) total = win.p / 100 < 1 ? total + 1 : total + (win.p / 100);
            else if (block.BackColor == Color.Blue) total = win.p / 100 < 1 ? total + 1 : total + (win.p / 100);
            else if (block.BackColor == Color.Yellow) total = win.p / 100 < 1 ? total + 1 : total + (win.p / 100);
            else total += 0;
            win.Text = "Block Breaker (" + left + "," + total + ")";
        }
        public static void BlackScore(int left, Breaker win)
        {
            if (!EndGame)
            {
                total += (win.p / 20);
                win.Text = "Block Breaker (" + left + "," + total + ")";
            }
            else
            {
                total += (win.p / 50);
                win.Text = "Block Breaker (" + total + ") - Game Over";
            }
        }
        public static void Bonus(int points, Breaker win)
        {
            total += points;
            win.Text = "Block Breaker (" + total + ")";
        }
        public static void Reset()
        {
            total = 0;
        }
    }
}
