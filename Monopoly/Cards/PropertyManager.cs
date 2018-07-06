using Monopoly.Main;
using Monopoly.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    public class PropertyManager
    {
        private const int NUMBER_OF_PROPERTY_CARDS = 22;
        private const int NUMBER_OF_AGENCIES = 2;

        private Dictionary<int, IPurchasable> propertyCards;
        private Dictionary<IPurchasable, Player> ownership;


        private int[][] groups;



        // source is usually "properties.txt"
        public PropertyManager(string source)
        {
            groups = new int[][]
            {
                new int[] {0},
                new int[] {1, 3 },
                new int[] {6, 8, 9},
                new int[] {11, 13, 14},
                new int[] {16, 18, 19},
                new int[] {21, 23, 24},
                new int[] {26, 27, 29},
                new int[] {31, 32, 34},
                new int[] {37, 39},
            };

            // initialise all the property cards
            propertyCards = new Dictionary<int, IPurchasable>();
            string[] cardDetails = File.ReadAllLines(source);
            for (int i = 0; i < NUMBER_OF_PROPERTY_CARDS; i++)
            {
                string[] numberAndData = cardDetails[i].Split(' ');
                propertyCards.Add(int.Parse(numberAndData[0]), new PropertyCard(numberAndData[1]));
            }
            for (int i = NUMBER_OF_PROPERTY_CARDS; i < NUMBER_OF_PROPERTY_CARDS + NUMBER_OF_AGENCIES; i++)
            {
                string[] numberAndData = cardDetails[i].Split(' ');
                propertyCards.Add(int.Parse(numberAndData[0]), new AgencyCard(numberAndData[1]));
            }
        }

        public Dictionary<string, List<int>> GetFieldTypes()
        {
            Dictionary<string, List<int>> ret = new Dictionary<string, List<int>>();
            ret["property"] = new List<int>();
            ret["agency"] = new List<int>();
            ret["bonus"] = new List<int>();

            foreach (int field in propertyCards.Keys)
            {
                if (propertyCards[field] is PropertyCard)
                {
                    ret["property"].Add(field);
                }
                else if (propertyCards[field] is AgencyCard)
                {
                    ret["agency"].Add(field);
                }
                else
                {
                    ret["bonus"].Add(field);
                }
            }
            return ret;
        }

        public Player WhoOwns(IPurchasable card)
        {
            if (ownership.ContainsKey(card))
            {
                return ownership[card];
            }
            return null;
        }

        public IPurchasable CardAt(int position)
        {
            if (propertyCards.ContainsKey(position))
            {
                return propertyCards[position];
            }
            return null;
        }

        public void AddOwnership (IPurchasable card, Player player)
        {
            ownership[card] = player;
        }

        public bool OwnsWholeGroup(int groupNumber, Player player)
        {
            int size = groups[groupNumber].Length;
            int count = 0;
            foreach (int i in groups[groupNumber])
            {
                if (player == ownership[propertyCards[i]])
                {
                    count++;
                }
            }
            return size == count;
        }
    }
}
