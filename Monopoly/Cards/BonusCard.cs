using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public class BonusCard : IPurchasable
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float Cost { get; private set; }
        public float MortgageValue { get; private set; }

        private const float ONE_BONUS = 3;
        private const float TWO_BONUS = 7;
        private const float THREE_BONUS = 13;
        private const float FOUR_BONUS = 20;

        private float[] payments;

        public BonusCard(string data)
        {
            payments = new float[4] { ONE_BONUS, TWO_BONUS, THREE_BONUS, FOUR_BONUS};

            string[] content = data.Split(';');
            Name = content[0];
            Description = content[1];
            Cost = float.Parse(content[2]);
            MortgageValue = float.Parse(content[3]);
        }

        public float GetPayment()
        {
            throw new NotImplementedException();
        }

        public float GetPayment(int numberOfBonusCards)
        {
            return payments[numberOfBonusCards-1];
        }
    }
}
