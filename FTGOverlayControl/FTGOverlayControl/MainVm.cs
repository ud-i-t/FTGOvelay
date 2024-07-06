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
    internal class MainVm
    {
        private static string PlayerFileName = "contents/players.json";
        private static string SettingFileName = "contents/score.json";

        public PlayerViewModel Player1 { get; }
        public PlayerViewModel Player2 { get; }
        public RelayCommand ResetScore { get; private set; }
        public RelayCommand AddScore1 { get; private set; }
        public RelayCommand AddScore2 { get; private set; }
        public RelayCommand UpdateScreenCommand { get; private set; }

        public string CenterTopText { get; set; }
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

            UpdateScreenCommand = new RelayCommand(_ => UpdateScreen(), _ => true);

            var players = JsonSettingIO.Read<PlayerDatas>(PlayerFileName);
            var setting = JsonSettingIO.Read<OverlaySetting>(SettingFileName);
            CenterTopText = setting.centerTopText;
            Player1 = new PlayerViewModel(new PlayerSetting() { Score = setting.score1, TeamScore = setting.teamScore1 }, UpdateScreen, players.players.Select(x => new Model.PlayerModel() { Name = x.name }));
            Player2 = new PlayerViewModel(new PlayerSetting() { Score = setting.score2, TeamScore = setting.teamScore2 }, UpdateScreen, players.players.Select(x => new Model.PlayerModel() { Name = x.name }));
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
