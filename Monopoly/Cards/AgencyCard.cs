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

        private const float ONE_BONUS = 0.4F;
        private const float TWO_BONUS = 1;

        public AgencyCard(string data, Image img)
        {
            string[] info = data.Split(';');
            Name = info[0];
            Cost = float.Parse(info[2]);
            MortgageValue = float.Parse(info[3]);
            Payment = 0F;
            Group = 0;
            logo = img;
        }

        public new float GetPayment()
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
