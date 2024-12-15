
using FTGOverlayControl.Model;
using System.Collections.Generic;
using System.Windows.Documents;

namespace FTGOverlayControl
{
    public class TeamEditVm
    {
        public List<PlayerModel> Players { get; set; } = new List<PlayerModel>()
        {
            new PlayerModel(){ Name = "aaa" },
            new PlayerModel(){ Name = "bbb" },
        };
    }
}
