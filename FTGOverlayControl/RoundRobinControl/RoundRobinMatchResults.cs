using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundRobinControl
{
    internal class RoundRobinMatchResults
    {
        public RoundRobinMatchResult[] Results { get; set; }
    }

    internal class RoundRobinMatchResult
    {
        public int Player1 { get; set; }
        public int Player2 { get; set; }
        public int Winner { get; set; }
        public bool IsLast { get; set; }
        public bool IsNext { get; set; }
    }
}
