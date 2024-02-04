using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl
{
    public class PlayerListReader
    {
        private string _filePath;

        public PlayerListReader(string filePath)
        { 
            _filePath = filePath; 
        }

        internal IEnumerable<PlayerModel> Read()
        {
            var players = File.ReadAllLines(_filePath);
            return players.Select(x => new PlayerModel() { Name = x });
        }
    }
}
