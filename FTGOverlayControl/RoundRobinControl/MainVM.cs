using FTGOverlayControl;
using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        private PlayerDatas _players;

        public RelayCommand OnNext { get; private set; }
        public RelayCommand OnPreview { get; private set; }
        public RelayCommand OnSave { get; private set; }

        public MainVM()
        { 
            _players = JsonSettingIO.Read<PlayerDatas>(PlayerFileName);
            _matchModels = JsonSettingIO.Read<ParallelMatches>(FightOrderFileName);
            _rounds = _matchModels.Items.Select((x, index) => new RoundViewModel(index + 1, x.Items, _players)).ToList();
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
            // 全試合結果
            var results = new RoundRobinMatchResults();
            results.Results = _rounds.Select(x => new RoundRobinRoundResult() { Results = x.MatchViewModels.Select(x => x.ToResult()).ToArray() }).ToArray();
            JsonSettingIO.ToJson("results.json", results);
            
            // プレイヤー成績
            var playerResults = new PlayersScore();
            playerResults.Scores = _players.players.Select((player, index) => new PlayerScore() { 
                Results = _rounds.Select(x => x.IsWinner(index) ? 1 : 0).ToArray(),
                Score = _rounds.Count(x => x.IsWinner(index))
            }).ToArray();
            var ranking = Enumerable.Range(0, _players.players.Count())
                .GroupBy(x => playerResults.Scores[x].Score)
                .OrderBy(x => -x.Key)
                .ToList();

            int rank = 1;
            foreach(var group in ranking)
            {
                foreach (var item in group)
                {
                    playerResults.Scores[item].Rank = rank;
                }
                rank += group.Count();
            }

            JsonSettingIO.ToJson("playersScore.json", playerResults);
        }
    }
}
