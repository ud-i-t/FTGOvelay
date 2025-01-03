using FTGOverlayControl.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl
{
    internal class MatchViewModel
    {
        private PlayerModel _player1;
        private PlayerModel _player2;

        public MatchViewModel(PlayerModel player1, PlayerModel player2) 
        { 
            _player1 = player1;
            _player2 = player2;
        }

        public override string ToString()
        {
            return $"{_player1.Name} vs {_player2.Name}";
        }
    }
}
