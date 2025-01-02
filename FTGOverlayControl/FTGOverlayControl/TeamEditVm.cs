
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
        private Rule _rule;

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

        public IEnumerable<PlayerModel> CurrentPlayers => _currentTeam.Players.Take(_rule.PlayerCount);

        public ObservableCollection<TeamModel> Teams { get; set; } = new ObservableCollection<TeamModel>()
        {
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

        public TeamEditVm(Rule ruleModel)
        {
            _rule = ruleModel;
            _currentTeam = Teams.First();
        }

        public void OnStart()
        {
            foreach(var t in Teams)
            {
                int toAdd = _rule.PlayerCount - t.Players.Count;
                for (int i = 0; i < toAdd; i++)
                {
                    t.Players.Add(new PlayerModel());
                }
            }
        }
    }
}
