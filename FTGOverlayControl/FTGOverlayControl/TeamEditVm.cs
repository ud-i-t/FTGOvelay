
using FTGOverlayControl.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;

namespace FTGOverlayControl
{
    public class TeamEditVm : INotifyPropertyChanged
    {
        private TeamModel _currentTeam;
        public TeamModel CurrentTeam
        {
            get { return _currentTeam; }
            set 
            { 
                _currentTeam = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTeam)));
            }
        }

        public ObservableCollection<TeamModel> Teams { get; set; } = new ObservableCollection<TeamModel>() {
            new TeamModel()
            {
                Name = "Aチーム",
                Players = new List<PlayerModel>()
                {
                    new PlayerModel(){ Name = "aaa" },
                    new PlayerModel(){ Name = "bbb" },
                }
            },
            new TeamModel()
            {
                Name = "Bチーム",
                Players = new List<PlayerModel>()
                {
                    new PlayerModel(){ Name = "ccc" },
                    new PlayerModel(){ Name = "ddd" },
                }
            }
        };

        public event PropertyChangedEventHandler? PropertyChanged;

        public TeamEditVm()
        {
            _currentTeam = Teams.First();
        }
    }
}
