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
        private Rule _model;

        public ObservableCollection<Position> Positions { get; } = new ObservableCollection<Position>();
        
        public RelayCommand ChangePlayerCount { get; private set; }

        public int PlayerCount 
        { 
            get
            {
                return _model.PlayerCount;
            }
            set
            {
                _model.PlayerCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerCount)));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public RuleEditVm(Rule model)
        {
            _model = model;

            ChangePlayerCount = new RelayCommand(_ =>
            {
                int toAdd = PlayerCount - _model.Positions.Count;
                for (int i = 0; i < toAdd; i++)
                {
                    AddPlayer();
                }

                Positions.Clear();
                foreach (var p in _model.Positions.Take(PlayerCount))
                {
                    Positions.Add(p);
                }
            }, _ => true);

            ChangePlayerCount.Execute(null);
        }

        private void AddPlayer()
        {
            _model.Positions.Add(new Position() { Index = $"#{_model.Positions.Count + 1}" });
        }
    }
}
