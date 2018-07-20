using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Monopoly.Players;
using System.Threading;
using Monopoly.Cards;
using System.Drawing;

namespace Monopoly.Main
{
    public class Gameplan
    {
        public enum FieldType { PROPERTY, TREASURE, RISK, PARKING, START, PRISON, VISIT, TAX, AGENCY, TAX_FINE, BONUS_PROPERTY }

        private const int GAME_TURN_WAIT = 200;
        private const int GAMEPLAN_WIDTH = 760;
        private const int GAMEPLAN_HEIGHT = 760;
        private const int GAMEFIELD_HEIGHT = 119;
        private const int GAMEFIELD_WIDTH = 58;

        public List<int> agencyFields;
        public List<int> bonusFields;

        private Random randomizer = new Random();
        private Monopoly window;
        private FieldType[] fieldMap;

        Dictionary<Player, int> playerFields;
        int[] cornerPositions = { 0, 10, 20, 30 };

        public Gameplan(Player[] players, Dictionary<string, List<int>> fields)
        {
            playerFields = new Dictionary<Player, int>();
            foreach (Player p in players)
            {
                playerFields[p] = 0;
            }

            fieldMap = new FieldType[40];

            AssignFields(fields);
        }

        private void AssignFields(Dictionary<string, List<int>> fields)
        {
            int[] risks = { 7, 22, 36 };
            int[] treasures = { 2, 17, 33 };


            foreach (int i in risks)
            {
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

        public FieldType Move(Player player)
        {
            //move from one field to another
            playerFields[player]++;
            playerFields[player] = playerFields[player] % 40;
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

        public Point GetCoordsOfNextField(Player player, decimal playerNumber)
        {
            int field = playerFields[player];
            Point ret = new Point(0, 0);
            // distance from left/top border to regular-field equivalent of corner field
            int baseFromCorner = GAMEFIELD_HEIGHT - GAMEFIELD_WIDTH;

            // distance from right/bottom border to regular-field equivalent of corner field
            int baseFromCorner2 = GAMEPLAN_HEIGHT - GAMEFIELD_HEIGHT;

            if (field < 10)
            {
                // distance from the left + n-th field * 58px width of 1 field, 
                // players organised in 3 rows of 2 columns - margin from field border is 4px and in between them it is 10px
                ret.X = baseFromCorner + field * GAMEFIELD_WIDTH + ((int)Math.Floor(playerNumber / 3) * 25) + 4;

                // distance from top + margin from top: 15px, from bottom: 14px, between 2 players: 15px
                ret.Y = ((int)playerNumber) % 3 * 25;
            }
            else if (field < 20)
            {
                // similar to the Y coord above but the base of X is 1 field-width from the right border
                ret.X = GAMEPLAN_WIDTH - GAMEFIELD_HEIGHT + (((int)playerNumber) % 3 * 28) + 20;

                // similar to the 0-9 X coord
                int fieldNum = field - 10;
                ret.Y = baseFromCorner + fieldNum * GAMEFIELD_WIDTH + ((int)Math.Floor(playerNumber / 3) * 25) + 4;
            }
            else if (field < 30)
            {
                int fieldNum = field - 20;
                ret.X = baseFromCorner2 - fieldNum * GAMEFIELD_WIDTH + ((int)Math.Floor(playerNumber / 3) * 25) + 4;

                ret.Y = baseFromCorner2 + (((int)playerNumber) % 3 * 25) + 20;
            }
            else
            {
                int fieldNum = field - 30;
                ret.X = (((int)playerNumber) % 3 * 25) + 5;

                ret.Y = baseFromCorner2 - fieldNum * GAMEFIELD_WIDTH + ((int)Math.Floor(playerNumber / 3) * 25) + 4;
            }
            ret.X += randomizer.Next(-3, 4);
            ret.X += 426;
            ret.Y += randomizer.Next(-3, 4);
            ret.Y += 5;

            return ret;
        }

        public Point GetCoordsOfNextApartment(int field, int apartmentCount)
        {
            Point ret = new Point(0, 0);

            if (field < 10)
            {
                // distance from the left + n-th field * 58px width of 1 field, 
                // players organised in 3 rows of 2 columns - margin from field border is 4px and in between them it is 10px
                ret.X = 424 + 61 + (field) * 58 + 4 + 20 * (apartmentCount);

                // distance from top + margin from top: 15px, from bottom: 14px, between 2 players: 15px
                ret.Y = 107;
            }
            else if (field < 20)
            {
                // similar to the Y coord above but the base of X is 1 field-width from the right border
                ret.X = 644 + 424;

                // similar to the 0-9 X coord
                int fieldNum = field - 10;
                ret.Y = 61 + (fieldNum) * 58 + 4 + 20 * (apartmentCount); ;
            }
            else if (field < 30)
            {
                int fieldNum = field - 20;
                ret.X = 1113 - ((fieldNum) * 58 + 4 + 20 * (apartmentCount));

                ret.Y = 644;
            }
            else
            {
                int fieldNum = field - 30;
                ret.X = 531;

                ret.Y = 751 - (61 + (fieldNum) * 58 + 4 + 20 * (apartmentCount));
            }
            return ret;
        }
    }
}
