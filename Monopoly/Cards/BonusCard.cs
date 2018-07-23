using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    [Serializable()]
    public class BonusCard : Card
    {
        private const float ONE_BONUS = 3;
        private const float TWO_BONUS = 7;
        private const float THREE_BONUS = 13;
        private const float FOUR_BONUS = 20;

        private float[] payments;

        public BonusCard(string data, Image img)
        {
            payments = new float[4] { ONE_BONUS, TWO_BONUS, THREE_BONUS, FOUR_BONUS};

            Group = -1;

            string[] content = data.Split(';');
            Name = content[0];
            Cost = float.Parse(content[2]);
            MortgageValue = float.Parse(content[3]);

            logo = img;
        }

        public new float GetPayment()
        {
            throw new NotImplementedException();
        }

        public float GetPayment(int numberOfBonusCards)
        {
            return payments[numberOfBonusCards-1];
        }
    }
}
