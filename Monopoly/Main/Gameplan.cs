using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Monopoly.Players;
using System.Threading;
using Monopoly.Cards;

namespace Monopoly.Main
{
    public class Gameplan
    {
        public enum FieldType { PROPERTY, TREASURE, RISK, PARKING, START, PRISON, VISIT, TAX, AGENCY, TAX_FINE, BONUS_PROPERTY }

        private const int GAME_TURN_WAIT = 200;

        public List<int> agencyFields;
        public List<int> bonusFields; 

        private Monopoly window;
        private FieldType[] fieldMap;

        Dictionary<Player, int> playerFields;

        public Gameplan(Player[] players, Dictionary<string, List<int>> fields)
        {
            playerFields = new Dictionary<Player, int>();
            foreach(Player p in players)
            {
                playerFields[p] = 0;
            }
            //this.window = window;

            fieldMap = new FieldType[40];

            AssignFields(fields);

        }

        private void AssignFields(Dictionary<string, List<int>> fields)
        {
            int[] risks = { 7, 22, 36 };
            int[] treasures = { 2, 17, 33 };
            
            
            foreach (int i in risks) {
                fieldMap[i] = FieldType.RISK;
            }

            foreach (int i in treasures)
            {
                fieldMap[i] = FieldType.TREASURE;
            }
            foreach (int i in fields["bonus"])
            {
                fieldMap[i] = FieldType.BONUS_PROPERTY;
            }
            foreach (int i in fields["agency"])
            {
                fieldMap[i] = FieldType.AGENCY;
            }
            foreach (int i in fields["property"])
            {
                fieldMap[i] = FieldType.PROPERTY;
            }
            
            fieldMap[10] = FieldType.VISIT;
            fieldMap[20] = FieldType.PARKING;
            fieldMap[30] = FieldType.PRISON;
            fieldMap[0] = FieldType.START;
            fieldMap[38] = FieldType.TAX_FINE;
            fieldMap[4] = FieldType.TAX;

            agencyFields = fields["agency"];
            bonusFields = fields["bonus"];
        }
        
        public void GoToPrison(Player player)
        {
            playerFields[player] = 10;
            //window.redraw(player);
        }

        private void Draw()
        {

        }

        private int AgencyBonus()
        {
            return 1;
        }

        private int BonusPropertyAmount(Player player, int field)
        {
            return 1;
        }

        public FieldType Move(int roll, Player player)
        {
            for (int i=0; i < roll; i++)
            {
                //move from one field to another
                playerFields[player]++;
            }
            return fieldMap[playerFields[player]];
        }

        public int PlayerPosition(Player player)
        {
            if (playerFields.ContainsKey(player))
            {
                return playerFields[player];
            }
            return 99;
        }
    }
}
