using Monopoly.Cards;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Players
{
    /**
     * This class represents the AIPlayer - some additional attributes it could need to remember.
     */
    [Serializable()]
    public class AIPlayer : Player
    {
        public float dangerFactor;
        public bool Trade;
        public Dictionary<Card, float> offers;

        public AIPlayer(string name, float money, Color color)
        {
            offers = new Dictionary<Card, float>();
            Trade = true;
            dangerFactor = (float) new Random().NextDouble(); 
            Money = money;
            this.name = name + " (AI)";
            Blocked = 0;
            Prison = false;
            Color = color;
        }
    }
}
