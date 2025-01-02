using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl
{
    class RuleEditVm
    {
        public List<Position> Positions { get; } = new List<Position>
        {
            new Position(){ Index = "#1", Name = "先鋒" },
            new Position(){ Index = "#2", Name = "次鋒" },
            new Position(){ Index = "#3", Name = "中堅" },
            new Position(){ Index = "#4", Name = "副将" },
            new Position(){ Index = "#5", Name = "大将" },
        };
    }
}
