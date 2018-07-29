using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    [Serializable()]
    /**
     * Default class of Card, which is the base of: PropertyCard, BonusCard, AgencyCard
     * 
     * The class provides the basic common attributes of all successor cards.
     */
    public abstract class Card
    {
        public string Name { get; protected set; }
        //for how much can the card be bought/mortgage pay-off value
        public float Cost { get; protected set; }
        // how much money the player gets after mortgaging this card
        public float MortgageValue { get; protected set; }
        // group - 1-8 regular groups, -1 bonus cards, 0 agencies
        public int Group { get; protected set; }
        public bool Mortgaged { get; set; } = false;
        public Image logo;

        public virtual float GetPayment()
        {
            return 0F;
        }
    }
}
