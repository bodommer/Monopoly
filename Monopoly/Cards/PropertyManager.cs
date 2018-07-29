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
    /**
     * A very important class when it comes to handling Card class' successors. All Card processes
     * are executed by this class. It is 'the right hand' of the controller - Game class.
     * 
     * It's main tasks are:
     *  - keep the relation between the field number (0-39) and the Card associated to the field
     *  - keep the ownership data (which card is owned by whom)
     *  - create datasets of the Card successors - for ListViews,...
     *  - provides the decision logic for trades of AIPlayer
     */
    [Serializable()]
    public class PropertyManager
    {
        private const int NUMBER_OF_PROPERTY_CARDS = 22;
        private const int NUMBER_OF_AGENCIES = 2;
        private const int NUMBER_OF_BONUS_CARDS = 4;

        // links the gameplan field's number to a certain Card.
        private Dictionary<int, Card> propertyCards;
        // keeps the data of ownership of each Card.
        private Dictionary<Card, Player> ownership;

        // information about logos 
        private Image[] logos = {null, Resource1._1, null, Resource1._3, null, Resource1._5,
                                Resource1._6, null, Resource1._8, Resource1._9, null, Resource1._11,
                                Resource1._12, Resource1._13, Resource1._14, Resource1._15,
                                Resource1._16, null, Resource1._18, Resource1._19, null, Resource1._21,
                                null, Resource1._23, Resource1._24, Resource1._25, Resource1._26,
                                Resource1._27, Resource1._28, Resource1._29, null, Resource1._31,
                                Resource1._32, null, Resource1._34, Resource1._35, null, Resource1._37,
                                null, Resource1._39 };

        private int[][] groups;
        
        public PropertyManager()
        {
            // The field numbers of cards of the ame group - for PropertyCard groups only (1-8)
            groups = new int[][]
            {
                new int[] {}, // placeholder, no PropertyCard item has group 0
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
            propertyCards = new Dictionary<int, Card>();
            ownership = new Dictionary<Card, Player>();

            // Something like "ReadAllLines(), but with Resources
            string details = Resource1.properties;
            string[] cardDetails = Regex.Split(details, @"\r?\n|\r");

            // Initialise PropertyCards
            for (int i = 0; i < NUMBER_OF_PROPERTY_CARDS; i++)
            {
                string[] numberAndData = cardDetails[i].Split(' ');
                propertyCards.Add(int.Parse(numberAndData[0]), new PropertyCard(numberAndData[1], logos[int.Parse(numberAndData[0])]));
            }
            // Initialise AgencyCards
            for (int i = NUMBER_OF_PROPERTY_CARDS; i < NUMBER_OF_PROPERTY_CARDS + NUMBER_OF_AGENCIES; i++)
            {
                string[] numberAndData = cardDetails[i].Split(' ');
                propertyCards.Add(int.Parse(numberAndData[0]), new AgencyCard(numberAndData[1], logos[int.Parse(numberAndData[0])]));
            }
            // Initialise BonusCards
            for (int i = NUMBER_OF_PROPERTY_CARDS + NUMBER_OF_AGENCIES; i < NUMBER_OF_PROPERTY_CARDS + NUMBER_OF_AGENCIES + NUMBER_OF_BONUS_CARDS; i++)
            {
                string[] numberAndData = cardDetails[i].Split(' ');
                propertyCards.Add(int.Parse(numberAndData[0]), new BonusCard(numberAndData[1], logos[int.Parse(numberAndData[0])]));
            }
        }

        /**
         * Returns a Dictionary of Fieldtypes - for Gameplan to initialise the types of each gamefield.
         */
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

        /**
         * Returns the owner of a Card.
         * Returns null if there is no owner.
         */
        public Player WhoOwns(Card card)
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

        /**
         * Returns the card that is located at specified field.
         * Returns null if the field is not a Card field.
         */
        public Card CardAt(int position)
        {
            if (propertyCards.ContainsKey(position))
            {
                return propertyCards[position];
            }
            return null;
        }

        /**
         * Assigns or re-assigns the ownership of the Card in the ownership dictionary.
         */
        public void AddOwnership (Card card, Player player)
        {
            ownership[card] = player;
        }

        /**
         * Returns the answer to question "Does player own all the PropertyCards of groupNumber"?
         */
        public bool OwnsWholeGroup(int groupNumber, Player player)
        {
            int count = 0;
            foreach (int i in groups[groupNumber])
            {
                if (WhoOwns(propertyCards[i]) == player) count++;
            }
            return groups[groupNumber].Length == count;
        }

        /**
         * AIPlayer deciding logics. It evaluates which card is the AIPlayer most likely to want to buy. 
         * It takes into consideration, that:
         *  - Agency Cards are the most wanted
         *  - Bonus Cards have 2nd highest priority
         *  - PropertyCards of Highest (most expensive group) have higher priority than the cheaper ones
         *            (unless they cannot afford the expensive ones)
         */
        public Card GetTradeCard(AIPlayer player)
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
                return (Card) property;
            }
            return null;
        }

        /**
         * Helper method.
         * Returns AgencyCard, if a player owns exactly one AgencyCard. Returns the NOT owned one. 
         * That is the card that the player would eventually want to buy to complete the duo.
         * It does not make sense to buy AgencyCard if the player doesn't own at least one. 
         */
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

        /**
         * Helper method.
         * Returns BonusCard, if a player owns at least 2 BonusCards, but not all. 
         * Returns the NOT owned one - it picks the one with lowest field number that can be bught.
         * 
         * It does not make sense to buy BonusCard if the player doesn't own at least two. 
         */
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

        /**
         * Helper method.
         * Returns a PropertyCard if the player owns all but one PropertyCards of a group.
         * Therefore the AIPlayer, given he/she has sufficient funds, would want to complete
         * the group ownership that'd let him upgrade PropertyCard once he steps on it.
         */
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

        /**
         * Helper method.
         * Counts the number of properties owned of a given group by the player.
         */
        public int CountGroup(int[] group, Player player)
        {
            int ret = 0;
            foreach(int i in group)
            {
                if (WhoOwns(propertyCards[i]) == player) ret++;
            }
            return ret;
        }

        /**
         * Helper method.
         * Returns the first card of the group that is not owned by the given player.
         * If the player owns all the properties, returns null.
         */
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

        /**
         * This method prepares data for the gameWindow's TradeViewer.
         * It lists all the items not owned by the player, but someone else.
         * Therefore the list contains the trade options for the player.
         */
        public List<ListViewItem> GetTradeOptions(Player player)
        {
            List<ListViewItem> ret = new List<ListViewItem>();
            foreach (Card card in ownership.Keys)
            {
                if (WhoOwns(card) != player)
                {
                    ret.Add(CreateListItem(card));
                }
            }
            return ret;
        }

        /**
         * Prepares data for gameWindow's TradeViewer. Lists all the Cards
         * that are in the ownership of the given player.
         */
        public List<ListViewItem> GetPlayerProperties(Player player)
        {
            List<ListViewItem> ret = new List<ListViewItem>();
            foreach (Card card in ownership.Keys)
            {
                if (WhoOwns(card) == player)
                {
                    ret.Add(CreateListItem(card));
                }
            }
            return ret;
        }

        /**
         * Effectively performs 'trade' between two players. The old owner gets
         * the agreed money and the newOwner is assigned ownership 
         * of the traded Card.
         */
        public void ChangeOwner(Player newOwner, Card property, float money)
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
            foreach (Card card in ownership.Keys)
            {
                if (ownership[card] == player)
                {
                    if (card.Mortgaged == mortgaged)
                    {
                        list.Add(CreateListItem(card));
                    }
                }
            }
            return list;
        }

        /**
         * Creates a GameWindow's TradeViewer list item.
         * Subitems:
         *  - name
         *  - cost
         *  - mortgaged (bool to string yes/no)
         *  - group (Agencies as "A", BonusCard as "B")
         *  - owner
         *  Tag: Listed card (type Card)
         */
        private ListViewItem CreateListItem(Card card)
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
            item.Tag = card;
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
            item.SubItems.Add(WhoOwns(card).name);
            return item;
        }

        /**
         * Returns the Collection of keys of propertyCards dictionary.
         * It displays allt he numbers of Card fields.
         */
        public Dictionary<int, Card>.KeyCollection GetKeyCollection()
        {
            return propertyCards.Keys;
        }
    }
}
