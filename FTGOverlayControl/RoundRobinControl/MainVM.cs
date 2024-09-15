using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundRobinControl
{
    public class MainVM
    {
        private static string FightOrderFileName = "matches.json";
        private ParallelMatches _matchModels;

        public MainVM()
        { 
            _matchModels = JsonSettingIO.Read<ParallelMatches>(FightOrderFileName);

            foreach(var matchModel in _matchModels.Items)
            {
                foreach(var match in matchModel.Items)
                {
                    Console.WriteLine($"{match.Player1Index} vs {match.Player2Index}");
                }
            }
        }
    }
}
