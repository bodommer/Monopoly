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
        private RiskCard[] riskCards;
        private int cardPointer = 0;

        /**
         * The constructor.
         */
        public RiskCardManager()
        {
            HashSet<RiskCard> riskCardSet = new HashSet<RiskCard>();
            string details;
            // initialise all the risk cards
            try
            {
                // like ReadAllLines() in typical text file
                details = Resource1.riskCards;
            } catch
            {
                throw new InvalidDataException("Wrong source file address!");
            }

            Regex regex = new Regex(@"(?<description>[A-Za-z .!0-9\-'?]+);(?<plusMinus>[A-Za-z]+);(?<money>[0-9\.]+);(?<turns>[0-9\.]+)");
            MatchCollection cards= regex.Matches(details);

            foreach (Match m in cards)
            {
                riskCardSet.Add(new RiskCard(m.ToString()));
            }

            // if there is no risk card, somebody edited the source files severely!
            if (riskCardSet.Count < 1)
            {
                throw new IOException("Source files are wrong (risk cards)! Re-install the game and try again!");
            }

            riskCards = riskCardSet.ToArray();
        }

        /**
         * Returns the card "on top of current RiskCard stack" and puts it to last spot.
         */
        public RiskCard GetRiskCard()
        {
            cardPointer++;
            cardPointer = cardPointer % riskCards.Length;
            return riskCards[cardPointer];
        }
    }
}
