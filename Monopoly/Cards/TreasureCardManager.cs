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
    public class TreasureCardManager
    {
        private const int NUMBER_OF_CARDS = 10;

        private TreasureCard[] TreasureCards;
        private int cardPointer = 0;

        public TreasureCardManager(string source)
        {
            HashSet<TreasureCard> TreasureCardSet = new HashSet<TreasureCard>();

            // initialise all the Treasure cards
            //string[] cardDetails = File.ReadAllLines(source);
            string details = Resource1.treasureCards;
            string[] cardDetails = Regex.Split(details, @"\r?\n|\r");

            TreasureCards = new TreasureCard[cardDetails.Count()];
            for (int i = 0; i < NUMBER_OF_CARDS; i++)
            {
                TreasureCardSet.Add(new TreasureCard(cardDetails[i]));
            }

            TreasureCards = TreasureCardSet.ToArray<TreasureCard>();
        }

        public TreasureCard GetTreasureCard()
        {
            cardPointer++;
            cardPointer = cardPointer % NUMBER_OF_CARDS;
            return TreasureCards[cardPointer];
        }
    }
}
