﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl.Model
{
    internal class PlayerModel
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
