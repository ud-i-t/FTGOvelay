using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl.Model
{
    public class PlayerModel
    {
        public string Position { get; } = "先鋒";
        public string Name { get; set; }
        public int Score { get; set; }

        public PlayerModel(string position) 
        {
            Position = position;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
