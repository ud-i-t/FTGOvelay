using FTGOverlayControl;
using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundRobinControl
{
    internal class MainVM
    {
        private static string PlayerFileName = "contents/players.json";
        private static string FightOrderFileName = "matches.json";
        private ParallelMatches _matchModels;
        public IList<MatchViewModel> Matches { get; private set; }

        public MainVM()
        { 
            var players = JsonSettingIO.Read<PlayerDatas>(PlayerFileName);
            _matchModels = JsonSettingIO.Read<ParallelMatches>(FightOrderFileName);
            Matches = _matchModels.Items.First().Items.Select(x => new MatchViewModel(x, players)).ToList();
        }
    }
}
