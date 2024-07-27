using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace FTGOverlayControl
{
    internal class MainVm : INotifyPropertyChanged
    {
        private static string PlayerFileName = "contents/players.json";
        private static string SettingFileName = "contents/score.json";

        public int _currentMatch { get; set; }
        public int CurrentMatch
        {
            get { return _currentMatch; }
            set
            {
                _currentMatch = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentMatch)));
                ApplyMatch();
            }
        }

        private static IList<Match> MatchOrder { get; } = new List<Match>()
        {
            new Match(0, 5, "先鋒戦", true),
            new Match(1, 6, "次鋒戦", false),
            new Match(2, 7, "中堅戦", false),
            new Match(3, 8, "副将戦", false),
            new Match(4, 9, "大将戦", false),
            new Match(10, 15, "先鋒戦", true),
            new Match(11, 16, "次鋒戦", false),
            new Match(12, 17, "中堅戦", false),
            new Match(13, 18, "副将戦", false),
            new Match(14, 19, "大将戦", false),
            new Match(0, 10, "先鋒戦", true),
            new Match(1, 11, "次鋒戦", false),
            new Match(2, 12, "中堅戦", false),
            new Match(3, 13, "副将戦", false),
            new Match(4, 14, "大将戦", false),
            new Match(5, 15, "先鋒戦", true),
            new Match(6, 16, "次鋒戦", false),
            new Match(7, 17, "中堅戦", false),
            new Match(8, 18, "副将戦", false),
            new Match(9, 19, "大将戦", false),
            new Match(10, 5, "先鋒戦", true),
            new Match(11, 6, "次鋒戦", false),
            new Match(12, 7, "中堅戦", false),
            new Match(13, 8, "副将戦", false),
            new Match(14, 9, "大将戦", false),
            new Match(15, 0, "先鋒戦", true),
            new Match(16, 1, "次鋒戦", false),
            new Match(17, 2, "中堅戦", false),
            new Match(18, 3, "副将戦", false),
            new Match(19, 4, "大将戦", false),
        };

        public IList<MatchViewModel> Matches { get; private set; }
        public PlayerViewModel Player1 { get; }
        public PlayerViewModel Player2 { get; }
        public RelayCommand ResetScore { get; private set; }
        public RelayCommand AddScore1 { get; private set; }
        public RelayCommand AddScore2 { get; private set; }
        public RelayCommand NextMatchCommand { get; private set; }
        public RelayCommand PrevMatchCommand { get; private set; }
        public RelayCommand UpdateScreenCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _centerTopText = string.Empty;
        public string CenterTopText
        {
            get { return _centerTopText; }
            set
            {
                _centerTopText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CenterTopText)));
            }
        }

        public string CenterBottomText { get; set; }

        private KeyboardHook hook = new KeyboardHook();

        public MainVm()
        {
            hook.KeyDownEvent += (sender, e) =>
            {
                if (e.KeyCode == 0x30)
                {
                    Player1.Score = 0;
                    Player2.Score = 0;
                    UpdateScreen();
                }
                if (e.KeyCode == 0x31)
                {
                    Player1.Score++;
                    UpdateScreen();
                }
                if (e.KeyCode == 0x32)
                {
                    Player2.Score++;
                    UpdateScreen();
                }
            };
            hook.Hook();

            ResetScore = new RelayCommand(_ =>
            {
                Player1.Score = 0;
                Player1.TeamScore = 0;
                Player2.Score = 0;
                Player2.TeamScore = 0;
                UpdateScreen();
            }, _ => true);

            AddScore1 = new RelayCommand(_ =>
            {
                Player1.Score++;
            }, _ => true);

            AddScore2 = new RelayCommand(_ =>
            {
                Player2.Score++;
            }, _ => true);

            NextMatchCommand = new RelayCommand(_ => NextMatch(), _ => true);
            PrevMatchCommand = new RelayCommand(_ => PrevMatch(), _ => true);
            UpdateScreenCommand = new RelayCommand(_ => UpdateScreen(), _ => true);

            var players = JsonSettingIO.Read<PlayerDatas>(PlayerFileName);
            var setting = JsonSettingIO.Read<OverlaySetting>(SettingFileName);
            CenterTopText = setting.centerTopText;
            Player1 = new PlayerViewModel(new PlayerSetting() { Score = setting.score1, TeamScore = setting.teamScore1 }, UpdateScreen, players.players.Select(x => new Model.PlayerModel() { Name = x.name }));
            Player2 = new PlayerViewModel(new PlayerSetting() { Score = setting.score2, TeamScore = setting.teamScore2 }, UpdateScreen, players.players.Select(x => new Model.PlayerModel() { Name = x.name }));

            Matches = MatchOrder.Select(x => new MatchViewModel(x, players)).ToList();

            ApplyMatch();
        }

        private void NextMatch()
        {
            if ((CurrentMatch + 1) >= MatchOrder.Count) return;
            CurrentMatch++;
            ApplyMatch();
        }

        private void PrevMatch()
        {
            if ((CurrentMatch - 1) < 0) return;
            CurrentMatch--;
            ApplyMatch();
        }

        private void ApplyMatch()
        {
            var currentMatch = MatchOrder[CurrentMatch];
            Player1.SelectedIndex = currentMatch.Player1Index;
            Player2.SelectedIndex = currentMatch.Player2Index;

            Player1.Score = 0;
            Player2.Score = 0;

            if (currentMatch.ResetTeamScore)
            {
                Player1.TeamScore = 0;
                Player2.TeamScore = 0;
            }

            CenterTopText = currentMatch.CenterText;
        }

        private void UpdateScreen()
        {
            var setting = new OverlaySetting();
            setting.centerTopText = CenterTopText;
            setting.player1 = Player1.SelectedIndex;
            setting.player2 = Player2.SelectedIndex;
            setting.score1 = Player1.Score;
            setting.score2 = Player2.Score;
            setting.teamScore1 = Player1.TeamScore;
            setting.teamScore2 = Player2.TeamScore;
            JsonSettingIO.ToJson(SettingFileName, setting);
        }
    }
}
