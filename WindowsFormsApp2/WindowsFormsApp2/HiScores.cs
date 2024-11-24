using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    internal class HiScores
    {
        private string _fileName;
        string thePlayer = "Anonymous";
        public int Score { get; set; }
        public int BNum { get; set; }
        public Dictionary<string, int> AllScores { get; set; }
        public HiScores(int points, int bubbles)
        {
            Score = points;
            BNum = bubbles;
            AllScores = new Dictionary<string, int>();
            SetScores();
        }
        private void SetScores()
        {
            _fileName = "BP-" + BNum.ToString() + ".txt";
            if (File.Exists(_fileName))
            {
                try
                {
                    string[] allLines = File.ReadAllLines(_fileName);
                    foreach(var line in allLines)
                    {
                        AllScores.Add(line.Split(',')[0], Convert.ToInt32(line.Split(',')[1]));
                    }
                }
                catch(Exception no)
                {
                    MessageBox.Show("Error reading file\n" + no.Message);
                }
            }
            else
            {
                var file = File.Create(_fileName);
                file.Close();
            }
        }
        public string GetHiScores()
        {
            AllScores.Add(thePlayer, Score);
            var items = (from pair in AllScores orderby pair.Value descending select pair).Take(10);
            var points = "";
            foreach(var i in items)
            {
                points += i.Key + ", " + i.Value + " pts\n";
            }
            return points;
        }
        public int GetPlayerscPos()
        {
            var items = from a in AllScores orderby a.Value descending select a.Value;
            var pos = 1;
            foreach(var i in items)
            {
                if (i <= Score) return pos;
                pos++;
            }
            return pos;
        }
        public string GetScoreMsg()
        {
            var rank = GetPlayerscPos();
            if (rank <= 16) return "Rank In: " + GetPlayerscPos() + "\n" + BNum + "x" + BNum;
            else return "\n" + BNum + "x" + BNum;
        }
        public void WriteScore(string user)
        {
            if(GetPlayerscPos() > 16) return;
            AllScores.Remove(thePlayer);
            var name = user;
            var noDuplicates = false;
            var count = 0;
            while (!noDuplicates)
            {
                if (AllScores.ContainsKey(name))
                {
                    count++;
                    name = user;
                    name += " (" + count.ToString() + ")";
                }
                else noDuplicates = true;
            }
            user = name;
            using (StreamWriter file = new StreamWriter(_fileName, true))
            {
                file.Write(name + "," + Score + Environment.NewLine);
            }
            thePlayer = name;
        }
    }
}
