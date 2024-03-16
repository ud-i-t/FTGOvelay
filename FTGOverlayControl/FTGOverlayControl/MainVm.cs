using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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

        private SettingFile _file;

        public MainVm() 
        {
            _file = SettingFileReader.Load();
            var players = new PlayerListReader("players.txt").Read();

            Player1 = new PlayerViewModel(_file.Players[0], Save, players);
            Player2 = new PlayerViewModel(_file.Players[1], Save, players);

            ResetScore = new RelayCommand(_ =>
            {
                Player1.Score = 0;
                Player2.Score = 0;
                Save();
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
        }

        private void Save() 
        {
            SettingFileReader.Save(_file);
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
                    outString = outString.Replace(@"{player1Score}", Player1.Score.ToString());
                    sw.WriteLine(outString);
                    line = sr.ReadLine();
                }
                sr.Close();
                sw.Close();
            }
        }
    }
}
