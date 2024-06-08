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

        private SettingFile _file;
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

            //_file = new SettingFile();
            //_file.Players.Add(new PlayerSetting());
            //_file.Players.Add(new PlayerSetting());

            //var players = new PlayerListReader("players.csv").Read();

            //Player1 = new PlayerViewModel(_file.Players[0], Save, players);
            //Player2 = new PlayerViewModel(_file.Players[1], Save, players);

            //ResetScore = new RelayCommand(_ =>
            //{
            //    Player1.Score = 0;
            //    Player2.Score = 0;
            //}, _ => true);

            //AddScore1 = new RelayCommand(_ =>
            //{
            //    Player1.Score++;
            //}, _ => true);

            //AddScore2 = new RelayCommand(_ =>
            //{
            //    Player2.Score++;
            //}, _ => true);

            UpdateScreenCommand = new RelayCommand(_ => UpdateScreen(), _ => true);

            var players = JsonSample.JsonUtilSample.Read<PlayerDatas>("players.json");

            var setting = JsonSample.JsonUtilSample.Read<OverlaySetting>("score.json");
            setting.player2 = 1;
            JsonSample.JsonUtilSample.ToJson("score2.json", setting);
        }

        private void Save()
        {
            // SettingFileReader.Save(_file);
        }

        private void UpdateScreen()
        {
            using (StreamReader sr = new StreamReader(@"template/overlay.html"))
            using (StreamWriter sw = new StreamWriter(@"output/overlay.html", false))
            {
                var line = sr.ReadLine();

                while (line != null)
                {
                    var outString = line.Replace(@"{player1Name}", Player1.Name);
                    outString = outString.Replace(@"{player2Name}", Player2.Name);
                    outString = outString.Replace(@"{player1Score}", Player1.Score.ToString());
                    outString = outString.Replace(@"{player2Score}", Player2.Score.ToString());
                    outString = outString.Replace(@"{CenterTopText}", CenterTopText);
                    outString = outString.Replace(@"{CenterBottomText}", CenterBottomText);
                    outString = outString.Replace(@"{player1Image}", Player1.File);
                    outString = outString.Replace(@"{player2Image}", Player2.File);
                    outString = outString.Replace(@"{player1Attr1}", Player1.Character);
                    outString = outString.Replace(@"{player1Attr2}", Player1.Rank);
                    outString = outString.Replace(@"{player1Attr3}", Player1.ControlType);
                    outString = outString.Replace(@"{player2Attr1}", Player2.Character);
                    outString = outString.Replace(@"{player2Attr2}", Player2.Rank);
                    outString = outString.Replace(@"{player2Attr3}", Player2.ControlType);
                    sw.WriteLine(outString);
                    line = sr.ReadLine();
                }
                sr.Close();
                sw.Close();
            }

            using (StreamReader sr = new StreamReader(@"template/matchup.html"))
            using (StreamWriter sw = new StreamWriter(@"output/matchup.html", false))
            {
                var line = sr.ReadLine();

                while (line != null)
                {
                    var outString = line.Replace(@"{player1Name}", Player1.Name);
                    outString = outString.Replace(@"{player2Name}", Player2.Name);
                    outString = outString.Replace(@"{player1Copy}", Player1.Copy);
                    outString = outString.Replace(@"{player2Copy}", Player2.Copy);
                    outString = outString.Replace(@"{player1Image}", Player1.File);
                    outString = outString.Replace(@"{player2Image}", Player2.File);
                    outString = outString.Replace(@"{player1Attr1}", Player1.Character);
                    outString = outString.Replace(@"{player1Attr2}", Player1.Rank);
                    outString = outString.Replace(@"{player1Attr3}", Player1.ControlType);
                    outString = outString.Replace(@"{player2Attr1}", Player2.Character);
                    outString = outString.Replace(@"{player2Attr2}", Player2.Rank);
                    outString = outString.Replace(@"{player2Attr3}", Player2.ControlType);
                    sw.WriteLine(outString);
                    line = sr.ReadLine();
                }
                sr.Close();
                sw.Close();
            }
        }
    }
}
