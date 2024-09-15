using FTGOverlayControl;
using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundRobinControl
{
    internal class RoundViewModel
    {
        private int _round;
        public string Title => $"第{_round}試合";

        public IList<MatchViewModel> MatchViewModels { get; }

        public RoundViewModel(int round, IList<Match> matchViewModels, PlayerDatas players)
        {
            _round = round;
            MatchViewModels = matchViewModels.Select(x => new MatchViewModel(x, players)).ToList();
        }
    }
}
