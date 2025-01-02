using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl.Model
{
    public  class Position
    {
        public string Index { get; set; }
        public string Name { get; set; }
        public int MatchCount { get; set; } = 3;
    }
}
