using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl.Model
{
    internal class PlayerModel
    {
        public string Name { get; set; }
        public string Copy { get; internal set; }
        public int Score { get; set; }
        public string ImagePath { get; set; }
        public List<string> Attributes { get; set; } = new List<string>();

        public override string ToString()
        {
            return Name;
        }
    }
}
