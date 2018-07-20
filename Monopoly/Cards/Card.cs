using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public abstract class Card : IPurchasable
    {
        public string Name { get; protected set; }
        public float Payment { get; protected set;  }
        public float Cost { get; protected set; }
        public float MortgageValue { get; protected set; }
        public int Group { get; protected set; }
        public bool Mortgaged { get; set; } = false;

        public virtual float GetPayment()
        {
            return 0F;
        }
    }
}
