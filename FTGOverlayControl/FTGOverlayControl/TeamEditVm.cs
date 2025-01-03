
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
                if (value == null) return;
                _currentTeam = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTeam)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPlayers)));
            }
        }

        public IEnumerable<PlayerModel> CurrentPlayers => _currentTeam.Players.Take(_rule.PlayerCount);

        public ObservableCollection<TeamModel> Teams { get; set; } = new ObservableCollection<TeamModel>()
        {
            new TeamModel()
            {
                Name = "Aチーム"
            },
            new TeamModel()
            {
                Name = "Bチーム"
            }
        };

        public RelayCommand AddTeamCommand { get; private set; }
        public RelayCommand RemoveTeamCommand { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public TeamEditVm(Rule ruleModel)
        {
            _rule = ruleModel;
            _currentTeam = Teams.First();

            AddTeamCommand = new RelayCommand(_ => AddTeam());
            RemoveTeamCommand = new RelayCommand(_ => RemoveTeam(), _ => Teams.Count > 0);
        }

        public void OnStart()
        {
            foreach(var t in Teams)
            {
                FillPlayers(t);
            }
        }

        private void FillPlayers(TeamModel team)
        {
            int toAdd = _rule.PlayerCount - team.Players.Count;
            for (int i = 0; i < toAdd; i++)
            {
                team.Players.Add(new PlayerModel(_rule.Positions[i].Name));
            }
        }

        private void AddTeam()
        {
            Teams.Add(new TeamModel() { Name = "新規チーム" });
            FillPlayers(Teams.Last());
            CurrentTeam = Teams.Last();
        }

        private void RemoveTeam()
        {
            Teams.Remove(CurrentTeam);
            CurrentTeam = Teams.Last();
        }
    }
}
