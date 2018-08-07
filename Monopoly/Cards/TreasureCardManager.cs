using Monopoly.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    /**
     * The RiskCard manipulator, creator and overlord.
     */
    [Serializable]
    public class TreasureCardManager
    {
        private TreasureCard[] TreasureCards;
        private int cardPointer = 0;

        /**
         * The cosntructor.
         */
        public TreasureCardManager()
        {
            HashSet<TreasureCard> TreasureCardSet = new HashSet<TreasureCard>();

            // initialise all the Treasure cards
            // like ReadAllLines() in typical text file
            string details = Resource1.treasureCards;

            Regex regex = new Regex(@"(?<description>[A-Za-z .!0-9\-'?]+);(?<money>[0-9\.]+)");
            MatchCollection cards = regex.Matches(details);

            foreach (Match m in cards)
            {
                TreasureCardSet.Add(new TreasureCard(m.ToString()));
            }

            // if there is no treasure card, somebody edited the source files severely!
            if (TreasureCardSet.Count < 1)
            {
                throw new IOException("Source files are wrong (treasure cards)! Re-install the game and try again!");
            }

            TreasureCards = TreasureCardSet.ToArray();
        }

        /**
         * Returns the card "on top of current RiskCard stack" and puts it to last spot.
         */
        public TreasureCard GetTreasureCard()
        {
            cardPointer++;
            cardPointer = cardPointer % TreasureCards.Length;
            return TreasureCards[cardPointer];
        }
    }
}
