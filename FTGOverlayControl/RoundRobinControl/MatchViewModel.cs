using RoundRobinControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Formats.Asn1.AsnWriter;

namespace FTGOverlayControl
{
    internal class MatchViewModel : INotifyPropertyChanged
    {
        private static SolidColorBrush DefaultColor = new SolidColorBrush(Colors.LightGray);
        private static SolidColorBrush WinnerColor = new SolidColorBrush(Colors.Orange);
        private static SolidColorBrush LoserColor = new SolidColorBrush(Colors.Gray);

        public string Player1Name => _players.players[_match.Player1Index].name;
        public string Player2Name => _players.players[_match.Player2Index].name;

        private SolidColorBrush _player1Color;
        public SolidColorBrush Player1Color
        {
            get { return _player1Color; }
            set
            {
                _player1Color = value;
                OnPropertyChanged(nameof(Player1Color));
            }
        }
        private SolidColorBrush _player2Color;
        public SolidColorBrush Player2Color
        {
            get { return _player2Color; }
            set
            {
                _player2Color = value;
                OnPropertyChanged(nameof(Player2Color));
            }
        }

        public RelayCommand OnPlayer1Win { get; private set; }
        public RelayCommand OnPlayer2Win { get; private set; }

        private int WinnerIndex = -1;

        private Model.Match _match;
        private PlayerDatas _players;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MatchViewModel(Model.Match match, PlayerDatas player)
        {
            _match = match;
            _players = player;
            Player1Color = DefaultColor;
            Player2Color = DefaultColor;

            OnPlayer1Win = new RelayCommand(_ =>
            {
                WinnerIndex = 0;
                Player1Color = WinnerColor;
                Player2Color = LoserColor;
            }, _ => true);

            OnPlayer2Win = new RelayCommand(_ =>
            {
                WinnerIndex = 1;
                Player1Color = LoserColor;
                Player2Color = WinnerColor;
            }, _ => true);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{_match.CenterText} {_players.players[_match.Player1Index].name} vs {_players.players[_match.Player2Index].name}";
        }

        public RoundRobinMatchResult ToResult()
        {
            return new RoundRobinMatchResult() { Player1 = _match.Player1Index, Player2 = _match.Player2Index, Winner = WinnerIndex };
        }

        public bool IsDone()
        {
            return WinnerIndex != -1;
        }

        public bool IsWinner(int playerIndex) 
        {
            if (WinnerIndex == 0 &&_match.Player1Index == playerIndex)
            {
                return true;
            }
            if (WinnerIndex == 1 && _match.Player2Index == playerIndex)
            {
                return true;
            }
            return false;
        }
    }
}
