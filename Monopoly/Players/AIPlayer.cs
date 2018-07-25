using Monopoly.Cards;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Players
{
    [Serializable()]
    public class AIPlayer : Player
    {
        public float dangerFactor;
        public bool Trade;
        public Dictionary<IPurchasable, float> offers;

        public AIPlayer(string name, float money, Color color)
        {
            offers = new Dictionary<IPurchasable, float>();
            Trade = true;
            dangerFactor = (float) new Random().NextDouble(); 
            Money = money;
            this.name = name;
            Blocked = 0;
            Prison = false;
            this.Color = color;
        }
    }
}
