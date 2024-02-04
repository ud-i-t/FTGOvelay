using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void Save() 
        {
            SettingFileReader.Save(_file);
        }
    }
}
