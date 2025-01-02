using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl.Model
{
    public class TeamModel
    {
        public string Name { get; set; }
        public List<PlayerModel> Players { get; set; } = new List<PlayerModel>()
        {
            new PlayerModel(){ Name = "aaa" },
            new PlayerModel(){ Name = "bbb" },
        };
    }
}
