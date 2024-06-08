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

            //var players = new PlayerListReader("players.csv").Read();
            //Player1 = new PlayerViewModel(_file.Players[0], Save, players);
            //Player2 = new PlayerViewModel(_file.Players[1], Save, players);

            ResetScore = new RelayCommand(_ =>
            {
                Player1.Score = 0;
                Player2.Score = 0;
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

            var players = JsonSample.JsonUtilSample.Read<PlayerDatas>("players.json");

            var setting = JsonSample.JsonUtilSample.Read<OverlaySetting>("score.json");
            setting.player2 = 1;
            JsonSample.JsonUtilSample.ToJson("score2.json", setting);
        }

        private void UpdateScreen()
        {
            
        }
    }
}
