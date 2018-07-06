using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public class AgencyCard : IPurchasable
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float Cost { get; private set; }
        public float MortgageValue { get; private set; }

        private const float ONE_BONUS = 0.4F;
        private const float TWO_BONUS = 1;

        public AgencyCard(string data)
        {

        }

        public float GetPayment()
        {
            throw new NotImplementedException();
        }

        /*
         * Gets the value thrown on the dice as the first argument, the amount of agencies owned as the second one (1 or 2).
         */
        public float GetPayment(int dice, int numberOfAgenciesOwned)
        {
            if (numberOfAgenciesOwned == 1)
            {
                return ONE_BONUS * dice;
            }
            return TWO_BONUS * dice;
        }
    }
}
