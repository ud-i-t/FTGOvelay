using FTGOverlayControl;
using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundRobinControl
{
    internal class MainVM : INotifyPropertyChanged
    {
        private static string PlayerFileName = "contents/players.json";
        private static string FightOrderFileName = "matches.json";
        private ParallelMatches _matchModels;

        private int _roundIndex = 0;
        private IList<RoundViewModel> _rounds;

        public event PropertyChangedEventHandler? PropertyChanged;

        private RoundViewModel _matches;
        public RoundViewModel Matches
        {
            get { return _matches; }
            private set
            {
                _matches = value;
                OnPropertyChanged(nameof(Matches));
            }
        }

        public RelayCommand OnNext { get; private set; }
        public RelayCommand OnPreview { get; private set; }
        public RelayCommand OnSave { get; private set; }

        public MainVM()
        { 
            var players = JsonSettingIO.Read<PlayerDatas>(PlayerFileName);
            _matchModels = JsonSettingIO.Read<ParallelMatches>(FightOrderFileName);
            _rounds = _matchModels.Items.Select((x, index) => new RoundViewModel(index + 1, x.Items, players)).ToList();
            Matches = _rounds.First();

            OnNext = new RelayCommand(_ =>
            {
                _roundIndex++;
                Matches = _rounds[_roundIndex];
            }, _ => (_roundIndex + 1) < _rounds.Count);

            OnPreview = new RelayCommand(_ =>
            {
                _roundIndex--;
                Matches = _rounds[_roundIndex];
            }, _ => _roundIndex > 0 );

            OnSave = new RelayCommand(_ =>
            {
                Save();    
            }, _ => true);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Save()
        {
            var results = new RoundRobinMatchResults();
            results.Results = _rounds.Select(x => new RoundRobinRoundResult() { Results = x.MatchViewModels.Select(x => x.ToResult()).ToArray()}).ToArray();
            JsonSettingIO.ToJson("results.json", results);
        }
    }
}
