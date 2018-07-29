using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Main;
using System.Drawing;

namespace Monopoly.Players
{
    /**
     * A base container for both HumanPlayer and AIPlayer. Contains basic attributes the two types
     * of players have in common.
     */
    [Serializable()]
    public abstract class Player
    {
        public int Blocked { get; set; }
        public float Money { get; set; }
        public bool Prison = false;
        public string name;
        public Color Color { get; set; } = Color.Goldenrod;

        public override string ToString()
        {
            return name;
        }

    }
}
