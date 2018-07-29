﻿using Monopoly.Main;
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
        private const int NUMBER_OF_CARDS = 10;

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
            string[] cardDetails = Regex.Split(details, @"\r?\n|\r");

            TreasureCards = new TreasureCard[cardDetails.Count()];
            for (int i = 0; i < NUMBER_OF_CARDS; i++)
            {
                TreasureCardSet.Add(new TreasureCard(cardDetails[i]));
            }

            TreasureCards = TreasureCardSet.ToArray<TreasureCard>();
        }

        /**
         * Returns the card "on top of current RiskCard stack" and puts it to last spot.
         */
        public TreasureCard GetTreasureCard()
        {
            cardPointer++;
            cardPointer = cardPointer % NUMBER_OF_CARDS;
            return TreasureCards[cardPointer];
        }
    }
}
