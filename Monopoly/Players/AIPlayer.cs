﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Players
{
    public class AIPlayer : Player
    {
        public AIPlayer(string name, float money, Color color)
        {
            Money = money;
            this.name = name;
            Blocked = 0;
            Prison = false;
            this.Color = color;
        }
    }
}
