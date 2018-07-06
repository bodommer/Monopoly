using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Main;

namespace Monopoly.Players
{
    public abstract class Player
    {
        public int Blocked { get; set; }
        protected HashSet<PropertyCard> properties;
        public int Position { get; set; }
        public float Money { get; set; }
        public bool Prison = false;
    }
}
