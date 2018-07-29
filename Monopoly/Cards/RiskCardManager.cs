using Monopoly.Main;
using Monopoly.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Monopoly
{
    /**
     * The RiskCard manipulator, creator and overlord.
     */
    [Serializable()]
    public class RiskCardManager
    {
        private const int NUMBER_OF_CARDS = 10;

        private RiskCard[] riskCards;
        private int cardPointer = 0;

        /**
         * The constructor.
         */
        public RiskCardManager()
        {
            HashSet<RiskCard> riskCardSet = new HashSet<RiskCard>();
            string[] cardDetails;
            // initialise all the risk cards
            try
            {
                // like ReadAllLines() in typical text file
                string details = Resource1.riskCards;
                cardDetails = Regex.Split(details, @"\r?\n|\r");
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

        /**
         * Returns the card "on top of current RiskCard stack" and puts it to last spot.
         */
        public RiskCard GetRiskCard()
        {
            cardPointer++;
            cardPointer = cardPointer % NUMBER_OF_CARDS;
            return riskCards[cardPointer];
        }
    }
}
