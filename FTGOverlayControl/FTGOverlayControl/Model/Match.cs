using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl.Model
{
    internal class Match
    {
        public int Player1Index { get; }
        public int Player2Index { get; }
        public string CenterText { get; } = string.Empty;

        public Match(int player1, int player2, string centerText) 
        {
            Player1Index = player1;
            Player2Index = player2;
            CenterText = centerText;
        }
    }
}
