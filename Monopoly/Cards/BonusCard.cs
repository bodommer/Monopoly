using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    /**
     * The data structure containing data representing a Bonus Card of a gameplan.
     */
    [Serializable()]
    public class BonusCard : Card
    {
        /*
         * The bonus for payment according to the number of BOnus cards owned.
         */
        private const float ONE_BONUS = 3;
        private const float TWO_BONUS = 7;
        private const float THREE_BONUS = 13;
        private const float FOUR_BONUS = 20;

        private float[] payments;
        
        /**
         * The constructor.
         */
        public BonusCard(string data, Image img)
        {
            payments = new float[4] { ONE_BONUS, TWO_BONUS, THREE_BONUS, FOUR_BONUS};

            Group = -1;

            // self-assigning data accoridng to structure of properties.txt file.
            string[] content = data.Split(';');
            Name = content[0];
            Cost = float.Parse(content[2], CultureInfo.InvariantCulture);
            MortgageValue = float.Parse(content[3], CultureInfo.InvariantCulture);

            // company's logo
            logo = img;
        }

        /**
         * Card interface's implementation.
         */
        public new float GetPayment()
        {
            throw new NotImplementedException();
        }

        /**
         * Bonus card's overload.
         * Arguments: numberOfBonusCards - specifies number of bonus cards owned by the player 
         *                                 that is going to receive the money.
         */
        public float GetPayment(int numberOfBonusCards)
        {
            return payments[numberOfBonusCards-1];
        }
    }
}
