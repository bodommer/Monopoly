using Monopoly.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class RiskCardManager
    {
        private const int NUMBER_OF_CARDS = 10;

        private RiskCard[] riskCards;
        private int cardPointer = 0;

        public RiskCardManager(string source)
        {
            HashSet<RiskCard> riskCardSet = new HashSet<RiskCard>();
            string[] cardDetails;
            // initialise all the risk cards
            try
            {
                cardDetails = File.ReadAllLines(source);
            } catch
            {
                throw new InvalidDataException("Wrong source file address!");
            }
            riskCards = new RiskCard[cardDetails.Count()];
            for (int i = 0; i < NUMBER_OF_CARDS; i++)
            {
                riskCardSet.Add(new RiskCard(cardDetails[i]));
            }

            riskCards = riskCardSet.ToArray<RiskCard>();
        }

        public RiskCard GetRiskCard()
        {
            cardPointer++;
            cardPointer = cardPointer % NUMBER_OF_CARDS;
            return riskCards[cardPointer];
        }
    }
}
