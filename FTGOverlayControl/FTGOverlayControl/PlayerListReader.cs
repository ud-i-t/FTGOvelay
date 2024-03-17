using FTGOverlayControl.Model;
using RLF3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
            var recoards = CSVReader.Parse(_filePath, Encoding.UTF8);
            return recoards.Select(x => new PlayerModel() { 
                Name = x["name"], 
                ImagePath = x["imagepath"],
                Copy = x["copy"],
                Attributes = new List<string>() { x["attr1"], x["attr2"], x["attr3"] }
            }).ToList();
        }
    }
}
