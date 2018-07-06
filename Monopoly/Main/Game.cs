using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Players;
using Monopoly.Cards;
using static Monopoly.Main.Gameplan;
using System.Drawing;

namespace Monopoly.Main
{
    public class Game
    {
        public enum GameStage {MAIN_MENU, NEW_GAME, LOAD_GAME, SETTINGS, EXIT }

        private const float TAX_PRICE = 2.5F;
        private const float TAX_FINE_COST = 5;
        // when passing the start field
        private const float START_BONUS = 10;

        private GameStage gameStage = GameStage.MAIN_MENU;
        private Monopoly window;

        Dice dice = Dice.Instance;
        RiskCardManager riskCardManager;
        TreasureCardManager treasureCardManager;
        PropertyManager propertyManager;
        Gameplan gameplan;
        int playerTurnPointer = 0;
        Player[] players;

        public Game()
        {

            window = new Monopoly
            {
                Width = 1000,
                Height = 1000,
                BackColor = Color.White
            };

            while (gameStage != GameStage.EXIT)
            {
                switch (gameStage)
                {
                    case GameStage.MAIN_MENU:
                        MainMenu();
                        break;

                    case GameStage.NEW_GAME:
                        LaunchGame();
                        gameStage = GameStage.MAIN_MENU;
                        break;

                    case GameStage.LOAD_GAME:
                        LoadGame();
                        gameStage = GameStage.MAIN_MENU;
                        break;

                    case GameStage.SETTINGS:
                        Settings();
                        gameStage = GameStage.MAIN_MENU;
                        break;

                    default:
                        Environment.Exit(1);
                        break;
                }
            }
            Environment.Exit(0);



            // launch the menu to select new game (and then assign number of players and characters) and load game


        }

        private void MainMenu()
        {
            window.Update();
            Console.ReadLine();

            gameStage = GameStage.NEW_GAME;
            
        }

        private void LaunchGame() {
                   
        }

        private void LoadGame() { }

        private void Settings() { }


        private void Play()
        {
            Console.WriteLine("Enter number of players");
            players = new Player[3];
            // dialog to choose game settings

            string resultSource = "";
            riskCardManager = new RiskCardManager(resultSource + "riskCards.txt");
            treasureCardManager = new TreasureCardManager(resultSource + "treasureCards.txt");
            propertyManager = new PropertyManager(resultSource + "properties.txt");
            gameplan = new Gameplan(players, propertyManager.GetFieldTypes());

            bool play = true;
            while(play)
            {
                Player player = players[playerTurnPointer];
                playerTurnPointer = playerTurnPointer++ % players.Length;
                if (player.Blocked == 0)
                {
                    player.Prison = false;
                }
                //window.drawStats(player);
                //gameplan.DrawPlan()
                int roll = dice.Roll(window);
                FieldType fieldType = gameplan.Move(roll, player);
                Action(fieldType, roll, player);
                if (player.Prison)
                {
                    return;
                }
                //wanna trade/mortage/bankrupt - if there is negative money, cannot end turn
                //end turn
            }
        }

        private void Action(FieldType fieldType, int diceRoll, Player player)
        {


            switch (fieldType)
            {
                case FieldType.PROPERTY:
                    Player owner;
                    PropertyCard card = (PropertyCard)propertyManager.CardAt(gameplan.PlayerPosition(player));
                    if (card != null)
                    {
                        owner = propertyManager.WhoOwns(card);
                        if (owner == null)
                        {
                            // wanna buy?
                            //yes:
                            propertyManager.AddOwnership(card, player);
                            //no:
                            return;
                        }
                        else if (owner == player)
                        {
                            //check if he has all other from group
                            if (propertyManager.OwnsWholeGroup(card.Group, player))
                            {
                                // want to buy improvements? 
                                //if yes
                                card.AddApartment();
                                player.Money -= card.ApartmentCost;
                                //window.drawCard(card); //redraw with apartment
                            }
                            return;
                        }
                        else
                        {
                            if (owner.Prison)
                            {
                                //draw info that you cannot pay when owner is in prison
                                return;
                            }
                            float money = card.GetPayment();
                            player.Money -= money;
                            owner.Money += money;
                        }
                    }
                    // else error - this card is not purchasable!!
                    break;

                case FieldType.TREASURE:
                    TreasureCard treasureCard = treasureCardManager.GetTreasureCard();
                    //window.showTreasureCard(card);
                    player.Money += treasureCard.MoneyChange;
                    break;

                case FieldType.RISK:
                    RiskCard riskCard = riskCardManager.GetRiskCard();
                    //window.showRiskCard(card);
                    player.Blocked += riskCard.TurnsStop;
                    break;

                case FieldType.START:
                    player.Money += START_BONUS;
                    break;

                case FieldType.PRISON:
                    player.Blocked += 3;
                    player.Prison = true;
                    gameplan.GoToPrison(player);
                    return;

                case FieldType.PARKING:
                    player.Blocked += 1;
                    break;

                case FieldType.TAX:
                    player.Money -= TAX_PRICE;
                    break;

                case FieldType.AGENCY:
                    Player agencyOwner = GetOwner(player);
                    AgencyCard agencyCard = (AgencyCard)propertyManager.CardAt(gameplan.PlayerPosition(player));

                    if (agencyOwner == null)
                    {
                        // wanna buy?
                        //yes:
                        propertyManager.AddOwnership(agencyCard, player);
                        //no:
                        return;
                    }
                    else if (agencyOwner == player)
                    {
                        return;
                    }
                    else
                    {
                        if (agencyOwner.Prison)
                        {
                            //draw info that you cannot pay when owner is in prison
                            return;
                        }
                        int agenciesOwned = PropsOwned(agencyOwner, "agency");
                        float money = agencyCard.GetPayment(diceRoll, agenciesOwned);
                        player.Money -= money;
                        agencyOwner.Money += money;
                    }
                    // else error - this card is not purchasable!!
                    break;

                case FieldType.TAX_FINE:
                    player.Money -= TAX_FINE_COST;
                    break;

                case FieldType.BONUS_PROPERTY:
                    Player bonusOwner = GetOwner(player);
                    AgencyCard bonusCard = (AgencyCard)propertyManager.CardAt(gameplan.PlayerPosition(player));

                    if (bonusOwner == null)
                    {
                        // wanna buy?
                        //yes:
                        propertyManager.AddOwnership(bonusCard, player);
                        //no:
                        return;
                    }
                    else if (bonusOwner == player)
                    {
                        return;
                    }
                    else
                    {
                        if (bonusOwner.Prison)
                        {
                            //draw info that you cannot pay when owner is in prison
                            return;
                        }
                        int bonusesOwned = PropsOwned(bonusOwner, "bonus");
                        float money = bonusCard.GetPayment(diceRoll, bonusesOwned);
                        player.Money -= money;
                        bonusOwner.Money += money;
                    }
                    // else error - this card is not purchasable!!
                    break;

                default:
                    break;
            }
        }

        private Player GetOwner(Player player)
        {
            IPurchasable agencyCard = propertyManager.CardAt(gameplan.PlayerPosition(player));
            if (agencyCard != null)
            {
                return propertyManager.WhoOwns(agencyCard);
            }
            return null;
        }

        private int PropsOwned(Player player, string type)
        {
            int ret = 0;
            List<int> typeFields;
            if (type == "agency")
            {
                typeFields = gameplan.agencyFields;
            } else
            {
                typeFields = gameplan.bonusFields;
            }
            foreach (int i in typeFields)
            {
                if (player == propertyManager.WhoOwns(propertyManager.CardAt(i)))
                {
                    ret++;
                }
            }
            return ret;
        }
    }
}
