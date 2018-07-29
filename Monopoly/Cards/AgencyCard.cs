using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    [Serializable()]
    public class AgencyCard : Card
    {
        public string Description { get; private set; }

        /**
         * the "dice throw" modifiers for payment when stepping onto this Agency field.
         * ONE_BONUS - when the player owns only one of the agencies
         * TWO_BONUS - when the player owns both agencies
         */
        private const float ONE_BONUS = 0.4F;
        private const float TWO_BONUS = 1;

        /**
         * The constructor.
         * Arguments: data 
         */ 
        public AgencyCard(string data, Image img)
        {
            string[] info = data.Split(';');
            Name = info[0];
            Cost = float.Parse(info[2]);
            MortgageValue = float.Parse(info[3]);
            Group = 0;
            logo = img;
        }

        /**
         * Implemented from Card interface.
         */
        public new float GetPayment()
        {
            return 0F;
        }

        /**
         * AgencyCard's overload of the method.
         * Arguments: int dice - the sum of all values rolled on the dice 
         *                       in the beginning of turn
         *            int numberOfAgenciesOwned - used for the Bonus counting
         */
        public float GetPayment(int dice, int numberOfAgenciesOwned)
        {
            if (numberOfAgenciesOwned == 1)
            {
                return ONE_BONUS * dice;
            }
            return TWO_BONUS * dice;
        }

        /**
         * Returns the ONE_BONUS or TWO_BONUS
         * Arguments: int which - 1 for ONE_BONUS, 2 for TWO_BONUS
         */
        public float GetBonus(int which)
        {
            if (which == 1)
            {
                return ONE_BONUS;
            }
            return TWO_BONUS;
        }
    }
}
