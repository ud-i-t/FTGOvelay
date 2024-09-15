using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundRobinControl
{
    internal class PlayersScore
    {
        public PlayerScore[] Scores { get; set; }
    }

    internal class PlayerScore
    {
        public int[] Results { get; set; }
        public int Score { get; set; }
        public int Rank { get; set; }
    }
}
