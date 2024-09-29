using FTGOverlayControl;
using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

                Announce = "@everyone\n" +
                    $"第{_roundIndex + 1}試合は以下の組み合わせです\n" + 
                    _matches.MatchViewModels.Aggregate("", (acc, x) => (acc + $"1P側: {x.Player1Name} VS 2P側: {x.Player2Name}\n")) +
                    "準備ができ次第対戦を始めてください！";
            }
        }

        private PlayerDatas _players;

        public RelayCommand OnNext { get; private set; }
        public RelayCommand OnPreview { get; private set; }
        public RelayCommand OnSave { get; private set; }
        public RelayCommand OnExportMatchList { get; private set; }

        public string _announce = "";
        public string Announce 
        {
            get{ return _announce; }
            private set
            {
                _announce = value;
                OnPropertyChanged(nameof(Announce));
            }
        }

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
            }, _ => _roundIndex > 0);

            OnSave = new RelayCommand(_ =>
            {
                Save();
            }, _ => true);

            OnExportMatchList = new RelayCommand(_ => ExportMatchList(CalcScore()));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private PlayersScore CalcScore()
        {
            // プレイヤー成績
            var playerResults = new PlayersScore();

            {
                playerResults.Scores = _players.players.Select((player, index) => new PlayerScore()
                {
                    Name = _players.players[index].name,
                    Results = _rounds.Select(x => {
                        if (!x.IsDone())
                        {
                            return -1;
                        }
                        return x.IsWinner(index) ? 1 : 0;
                    }).ToArray(),
                    Score = _rounds.Count(x => x.IsWinner(index))
                }).ToArray();
                var ranking = Enumerable.Range(0, _players.players.Count())
                    .GroupBy(x => playerResults.Scores[x].Score)
                    .OrderBy(x => -x.Key)
                    .ToList();

                int rank = 1;
                foreach (var group in ranking)
                {
                    foreach (var item in group)
                    {
                        playerResults.Scores[item].Rank = rank;
                    }
                    rank += group.Count();
                }

            }

            return playerResults;
        }

        private void Save()
        {
            // 全試合結果
            var results = new RoundRobinMatchResults();
            results.Results = _rounds.SelectMany(x => x.MatchViewModels.Select(match => match.ToResult())).ToArray();
            int lastmatch = results.Results.Count(x => x.Winner != -1);

            int matchCountInRound = _rounds.First().MatchViewModels.Count();
            if (lastmatch > 0)
            {
                foreach (var match in results.Results.Skip(lastmatch - matchCountInRound).Take(matchCountInRound))
                {
                    match.IsLast = true;
                }
            }
            foreach (var match in results.Results.Skip(lastmatch).Take(matchCountInRound))
            {
                match.IsNext = true;
            }

            JsonSettingIO.ToJson("results.json", results);

            var playerResults = CalcScore();
            JsonSettingIO.ToJson("playersScore.json", playerResults);

            // サマリー
            {
                var summary = playerResults.Scores
                    .OrderBy(x => x.Rank)
                    .Select(x => $"{x.Rank}位: {x.Name} ({x.Results.Count(x => x == 1)}勝)\n")
                    .Aggregate((acc, x) => acc + x);
                File.WriteAllText("summary.txt", summary);
            }
        }

        private void ExportMatchList(PlayersScore score)
        {
            var round = _rounds[_roundIndex];
            string matchListString = round.MatchViewModels.Aggregate("", (string s, MatchViewModel x) => s + x.ToMatchListRow(_players.players, score));
            File.WriteAllText("contents/players.txt", matchListString);

            var app = new ProcessStartInfo();
            app.FileName = "python";
            app.Arguments = "makeMatchList.py";
            app.WorkingDirectory = "contents";
            app.CreateNoWindow = false;

            Process.Start(app);
        }
    }

    static class MatchViewModelExtentions
    {
        public static string ToMatchListRow(this MatchViewModel match, PlayerData[] players, PlayersScore score)
        {
            int p1ID = match.Player1Index;
            int p2ID = match.Player2Index;
            var p1 = players[p1ID];
            var p2 = players[p2ID];
            string p1Win = "";
            string p2Win = "";
            if (match.IsDone())
            {
                p1Win = match.IsWinner(0) ? "W" : "L";
                p2Win = match.IsWinner(1) ? "L" : "W";
            }
            string retval = $"{p1Win},{p1.name},{p1.team},{p1.character},{p1.rank},{score.Scores[p1ID].Rank},{score.Scores[p1ID].Score}\n";
            retval += $"{p2Win},{p2.name},{p2.team},{p2.character},{p2.rank},{score.Scores[p2ID].Rank},{score.Scores[p2ID].Score}\n";
            return retval;
        }
    }
}
