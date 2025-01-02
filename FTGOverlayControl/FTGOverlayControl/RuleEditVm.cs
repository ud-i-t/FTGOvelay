using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl
{
    class RuleEditVm : INotifyPropertyChanged
    {
        public ObservableCollection<Position> Positions { get; } = new ObservableCollection<Position>();
        private List<Position> _positions { get; } = new List<Position>
        {
            new Position(){ Index = "#1", Name = "先鋒" },
            new Position(){ Index = "#2", Name = "次鋒" },
            new Position(){ Index = "#3", Name = "中堅" },
            new Position(){ Index = "#4", Name = "副将" },
            new Position(){ Index = "#5", Name = "大将" },
        };
        public RelayCommand ChangePlayerCount { get; private set; }

        private int _playerCount = 5;
        public int PlayerCount 
        { 
            get
            {
                return _playerCount;
            }
            set
            {
                _playerCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerCount)));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public RuleEditVm()
        {
            ChangePlayerCount = new RelayCommand(_ =>
            {
                int toAdd = PlayerCount - _positions.Count;
                for (int i = 0; i < toAdd; i++)
                {
                    AddPlayer();
                }

                Positions.Clear();
                foreach (var p in _positions.Take(PlayerCount))
                {
                    Positions.Add(p);
                }
            }, _ => true);

            ChangePlayerCount.Execute(null);
        }

        private void AddPlayer()
        {
            _positions.Add(new Position() { Index = $"#{_positions.Count + 1}" });
        }
    }
}
