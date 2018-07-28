using Monopoly.Main;
using Monopoly.Players;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly.Cards
{
    [Serializable()]
    public class PropertyManager
    {
        private const int NUMBER_OF_PROPERTY_CARDS = 22;
        private const int NUMBER_OF_AGENCIES = 2;
        private const int NUMBER_OF_BONUS_CARDS = 4;

        private Dictionary<int, IPurchasable> propertyCards;
        private Dictionary<IPurchasable, Player> ownership;

        private Image[] logos = {null, Resource1._1, null, Resource1._3, null, Resource1._5,
                                Resource1._6, null, Resource1._8, Resource1._9, null, Resource1._11,
                                Resource1._12, Resource1._13, Resource1._14, Resource1._15,
                                Resource1._16, null, Resource1._18, Resource1._19, null, Resource1._21,
                                null, Resource1._23, Resource1._24, Resource1._25, Resource1._26,
                                Resource1._27, Resource1._28, Resource1._29, null, Resource1._31,
                                Resource1._32, null, Resource1._34, Resource1._35, null, Resource1._37,
                                null, Resource1._39 };

        private int[][] groups;
        
        // source is usually "properties.txt"
        public PropertyManager(string source)
        {
            groups = new int[][]
            {
                new int[] {}, // placeholder, no item has group 0
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
            ownership = new Dictionary<IPurchasable, Player>();

            string details = Resource1.properties;
            string[] cardDetails = Regex.Split(details, @"\r?\n|\r");

            //string[] cardDetails = File.ReadAllLines(source);
            for (int i = 0; i < NUMBER_OF_PROPERTY_CARDS; i++)
            {
                string[] numberAndData = cardDetails[i].Split(' ');
                propertyCards.Add(int.Parse(numberAndData[0]), new PropertyCard(numberAndData[1], logos[int.Parse(numberAndData[0])]));
            }
            for (int i = NUMBER_OF_PROPERTY_CARDS; i < NUMBER_OF_PROPERTY_CARDS + NUMBER_OF_AGENCIES; i++)
            {
                string[] numberAndData = cardDetails[i].Split(' ');
                propertyCards.Add(int.Parse(numberAndData[0]), new AgencyCard(numberAndData[1], logos[int.Parse(numberAndData[0])]));
            }
            for (int i = NUMBER_OF_PROPERTY_CARDS + NUMBER_OF_AGENCIES; i < NUMBER_OF_PROPERTY_CARDS + NUMBER_OF_AGENCIES + NUMBER_OF_BONUS_CARDS; i++)
            {
                string[] numberAndData = cardDetails[i].Split(' ');
                propertyCards.Add(int.Parse(numberAndData[0]), new BonusCard(numberAndData[1], logos[int.Parse(numberAndData[0])]));
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
            if (card != null)
            {
                if (ownership.ContainsKey(card))
                {
                    return ownership[card];
                }
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
                if (ownership.ContainsKey(propertyCards[i]))
                {
                    if (player == ownership[propertyCards[i]])
                    {
                        count++;
                    }
                }
            }
            return size == count;
        }

        public IPurchasable GetTradeCard(AIPlayer player)
        {
            AgencyCard agency = WantsAgency(player);
            if (agency != null && agency.Cost < player.Money)
            {
                return agency;
            }
            BonusCard bonus = WantsBonus(player);
            if (bonus != null && bonus.Cost < player.Money)
            {
                return bonus;
            }
            PropertyCard property = WantsProperty(player);
            if (property != null && property.Cost < player.Money)
            {
                return (IPurchasable) property;
            }
            return null;
        }

        private AgencyCard WantsAgency(AIPlayer player)
        {
            if (ownership.ContainsKey(propertyCards[12]) && ownership[propertyCards[12]] == player)
            {
                if (ownership.ContainsKey(propertyCards[28]) && !(ownership[propertyCards[28]] == player))
                {
                    return (AgencyCard) propertyCards[28];
                }
                return null;
            }
            if (ownership.ContainsKey(propertyCards[28]) && ownership[propertyCards[28]] == player)
            {
                return (AgencyCard)propertyCards[12];
            }
            return null;
        }

        private BonusCard WantsBonus(AIPlayer player)
        {
            int[] bonusFields = { 5, 15, 25, 35 };
            int owned = CountGroup(bonusFields, player);
            if (owned > 2)
            {
                foreach (int j in bonusFields)
                {
                    if (ownership.ContainsKey(propertyCards[j]))
                    {
                        if (!(ownership[propertyCards[j]] == player && ((BonusCard)propertyCards[j]).Cost < player.Money))
                            return (BonusCard)propertyCards[j];
                    }
                }
            }
            return null;
        }

        private PropertyCard WantsProperty(AIPlayer player)
        {
            int[] owned = new int[9];
            int pointer = -1;
            foreach (int[] group in groups)
            {
                pointer++;
                owned[pointer] = CountGroup(group, player);
                
            }
            for (; pointer > 0; pointer--)
            {
                if (owned[pointer] + 1 == groups[pointer].Length)
                {
                    return GetNotOwned(groups[pointer], player);
                }
            }
            return null;
        }

        private int CountGroup(int[] group, AIPlayer player)
        {
            int ret = 0;
            foreach(int i in group)
            {
                if (ownership.ContainsKey(propertyCards[i]))
                {
                    if (ownership[propertyCards[i]] == player)
                    {
                        ret++;
                    }
                }
            }
            return ret;
        }

        private PropertyCard GetNotOwned(int[] group, AIPlayer player)
        {
            foreach (int i in group)
            {
                if (!(ownership.ContainsKey(propertyCards[i])))
                {
                    return (PropertyCard) propertyCards[i];
                }
                if (!(ownership[propertyCards[i]] == player))
                    return (PropertyCard) propertyCards[i];
            }
            return null;
        }

        public List<ListViewItem> GetTradeOptions(Player player)
        {
            List<ListViewItem> ret = new List<ListViewItem>();
            foreach (IPurchasable card in ownership.Keys)
            {
                if (ownership[card] != player)
                {
                    Card card2 = (Card)card;
                    ret.Add(CreateListItem(card2, card));
                }
            }
            return ret;
        }

        public List<ListViewItem> GetPlayerProperties(Player player)
        {
            List<ListViewItem> ret = new List<ListViewItem>();
            foreach (IPurchasable card in ownership.Keys)
            {
                if (ownership[card] == player)
                {
                    Card card2 = (Card)card;
                    ret.Add(CreateListItem(card2, card));
                }
            }
            return ret;
        }

        public void ChangeOwner(Player newOwner, IPurchasable property, float money)
        {
            newOwner.Money -= money;
            ownership[property].Money += money;
            ownership[property] = newOwner;
        }

        /*
         * This method returns the player's (un)mortgaged properties - mortgaged parameter
         * says whether the method shall return the list of mortgaged or unmortgaged properties.
         * true - mortgaged
         * false - unmortgaged
         * 
         */
        public List<ListViewItem> GetMortgagedProperties(Player player, bool mortgaged)
        {
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (IPurchasable ip in ownership.Keys)
            {
                if (ownership[ip] == player)
                {
                    Card card = (Card) ip;
                    if (card.Mortgaged == mortgaged)
                    {
                        list.Add(CreateListItem(card, ip));
                    }
                }
            }
            return list;
        }

        private ListViewItem CreateListItem(Card card, IPurchasable ip)
        {
            ListViewItem item = new ListViewItem(card.Name);
            item.SubItems.Add(card.Cost.ToString());
            item.SubItems.Add(card.MortgageValue.ToString());
            if (card.Mortgaged)
            {
                item.SubItems.Add("Yes");
            }
            else
            {
                item.SubItems.Add("No");
            }
            item.Tag = ip;
            int gr = card.Group;
            if (gr > 0)
            {
                item.SubItems.Add(gr.ToString());
            }
            else if (gr == 0)
            {
                item.SubItems.Add("A");
            }
            else
            {
                item.SubItems.Add("B");
            }
            item.SubItems.Add(ownership[card].name);
            return item;
        }

        public Dictionary<int, IPurchasable>.KeyCollection GetKeyCollection()
        {
            return propertyCards.Keys;
        }
    }
}
