using System;
using System.Collections.Generic;
using Monopoly.Players;
using Monopoly.Cards;
using static Monopoly.Main.Gameplan;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Diagnostics;
using Monopoly.AI;

namespace Monopoly.Main
{
    /**
     * The "controller" of the game - despite the architecture being done in a pretty low-life
     * style, this aspires to manage all the aspects of the game and the main game logics is here.
     */
    [Serializable()]
    public class Game
    {
        /**
         * Describes the states in which the game is. For more GameState description, see GameStates.pdf in Documentation folder.
         */
        public enum GameStage { DICE, NO_ACTION, AWAITING_PURCHASE,
            AWAITING_RENT, AWAITING_UPGRADE, NO_FUNDS_PAY, NO_FUNDS_BUY,
            CANNOT_UPGRADE, SPECIAL_CARD, SPECIAL_FIELD, TRADE_OFFER,
            TRADE_CONFIRM, WHAT_NEXT, MORTGAGE_MENU, MORTGAGE_TAKE, MORTGAGE_PAY,
            HOLIDAY}

        private const float TAX_PRICE = 2.5F;
        private const float TAX_FINE_COST = 5;
        private const float START_BONUS = 30;

        public GameStage GameState { get; private set; }
        [field:NonSerialized()]
        private Monopoly window;

        // each property group (1-8) has its own 'group colour'
        private int[,] colorGroups;
        private Dice dice = Dice.Instance;
        private RiskCardManager riskCardManager;
        private TreasureCardManager treasureCardManager;
        private Cards.PropertyManager propertyManager;
        private Gameplan gameplan;

        //points to players field and shows whose turn currently is
        private int playerTurnPointer = 0;
        // possesses references to all players in game
        private Player[] players;
        // easily accessible informationa bout current player - instead of players[playerTurnPointer]
        // improves readability of code
        private Player currentPlayer;
        // stores information about the traded property for when the player accepts/rejects trade offer
        private Card selectedItem = null;

        /**
         * The cosntructor. Prepares the game, assigns/creates its helping classes/data manipulators.
         */
        public Game(Player[] players, Monopoly window)
        {
            this.players = players;
            GameState = GameStage.DICE;
            this.window = window;
            colorGroups = new int[,] { { 0, 0, 0 }, { 255, 51, 51 }, { 0, 128, 255 }, { 152, 76, 0 },
                                             {255, 153, 51 }, {255, 102, 255 }, {0, 0, 204 },
                                             {0, 204, 102 }, {255, 255, 51 } };
            currentPlayer = players[playerTurnPointer];
            riskCardManager = new RiskCardManager();
            treasureCardManager = new TreasureCardManager();
            propertyManager = new Cards.PropertyManager();
            gameplan = new Gameplan(players, propertyManager.GetFieldTypes());
            window.ShowPlayerInfo(currentPlayer);
            window.Load += OnLoad;
            window.Enabled = true;
        }

        /**
         * When the main graphical window (GameWindow) is loaded, it assign
         */
        private void OnLoad(object s, EventArgs ea)
        {
            ReloadCallBacks();
        }

        /**
         * Assigns listeners to graphical elements of the game window.
         */
        private void ReloadCallBacks()
        {
            window.gameButton1.Click += B1Callback;
            window.gameButton2.Click += B2Callback;
            window.gameButton3.Click += B3Callback;
            window.KeyPress += KeyPressed;
            window.exitButton.Click += EBCallback;
        }

        /**
         * Removes listeners of graphical elements of the game window.
         */
        private void RemoveCallBacks()
        {
            window.gameButton1.Click -= B1Callback;
            window.gameButton2.Click -= B2Callback;
            window.gameButton3.Click -= B3Callback;
            window.KeyPress -= KeyPressed;
            window.exitButton.Click -= EBCallback;
        }

        /**
         * Shows a dialog to confirm before quiting the game.
         */
        private void ExitProcedure()
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to quit? All unsaved progress will be lost!", "Quit", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        /**
         * A callback method - listens to events of gameButton1 of graphical window.
         */
        private void B1Callback (object sender, EventArgs ea)
        {
                Button1_action();
        }

        /**
         * A callback method - listens to events of gameButton2 of graphical window.
         */
        private void B2Callback(object sender, EventArgs ea)
        {
            Button2_action();
        }

        /**
         * A callback method - listens to events of gameButton3 of graphical window.
         */
        private void B3Callback(object sender, EventArgs ea)
        {
            Button3_action();
        }

        /**
         * A callback method - listens to events of keyboard of graphical window.
         * Then, analyses which key was pressed and handles the event.
         */
        private void KeyPressed(object sender, KeyPressEventArgs ea)
        {
           switch (ea.KeyChar)
            {
                case '1':
                    Button1_action();
                    break;

                case '2':
                    Button2_action();
                    break;

                case '3':
                    Button3_action();
                    break;

                case 'l':
                    ExitProcedure();
                    break;

                case 'm':
                    window.mainMenuButton.PerformClick();
                    break;

                default:
                    break;
            }
        }

        /**
         * A callback method - listens to events of exitButton of graphical window.
         */
        private void EBCallback(object sender, EventArgs ea)
        {
            ExitProcedure();
        }

        /**
         * The way how to 'perform click' from AIDecider or other classes.
         */
        public void ClickButton(int number)
        {
            if (number == 1)
            {
                Button1_action();
            }
            else if (number == 2)
            {
                Button2_action();
            }
            else
            {
                Button3_action();
            }
        }

        /**
         * Called when gameButton1 was clicked, it analyses the game situation (using GameState attribute)
         * and then handles the click button accordingly.
         */
        private void Button1_action()
        {
            if (GameState == GameStage.NO_ACTION) { return; }

            window.Enabled = false;

            Card card = propertyManager
                  .CardAt(gameplan.PlayerPosition(currentPlayer));
            Player owner = propertyManager.WhoOwns(card);

            GameStage state = GameState;
            GameState = GameStage.NO_ACTION;

            switch (state)
            {
                case GameStage.DICE:
                    window.HideButton();
                    RollDice();
                    break;

                case GameStage.AWAITING_PURCHASE:
                    propertyManager.AddOwnership(card, currentPlayer);
                    currentPlayer.Money -= card.Cost;
                    break;

                case GameStage.AWAITING_RENT:
                    float money = card.GetPayment();
                    currentPlayer.Money -= money;
                    owner.Money += money;
                    break;

                case GameStage.AWAITING_UPGRADE:
                    PropertyCard c2 = (PropertyCard)card;
                    window.DrawApartment(
                        gameplan.GetCoordsOfNextApartment(
                            gameplan.PlayerPosition(
                                currentPlayer), c2.Apartments));
                    c2.AddApartment();
                    currentPlayer.Money -= c2.ApartmentCost;
                    break;

                case GameStage.NO_FUNDS_PAY:
                    RemovePlayer();
                    NextPlayer();
                    break;

                case GameStage.WHAT_NEXT:
                    window.ShowTradeOptions(propertyManager
                        .GetTradeOptions(currentPlayer),
                        currentPlayer);
                    GameState = GameStage.TRADE_OFFER;
                    break;

                case GameStage.TRADE_OFFER:
                    MakeTradeOffer();
                    break;

                case GameStage.TRADE_CONFIRM:
                    propertyManager.ChangeOwner(currentPlayer, 
                        selectedItem, window.GetOfferedMoney());
                    break;

                case GameStage.MORTGAGE_MENU:
                    window.ShowMortgagedProperties(propertyManager.GetMortgagedProperties(currentPlayer, false), true);
                    GameState = GameStage.MORTGAGE_TAKE;
                    break;

                case GameStage.MORTGAGE_TAKE:
                    TakeMortgage();
                    break;

                case GameStage.MORTGAGE_PAY:
                    PayMortgageOff();
                    break;
                    
                case GameStage.HOLIDAY:
                    NextPlayer();
                    break;

                default:
                    Console.Write("An unexpected error occurred! " + GameState);
                    break;
            }
            if (GameState == GameStage.NO_ACTION) ShowWhatNext(currentPlayer);
            window.Update();
            if (currentPlayer is AIPlayer) AIDecider.PlayTurn((AIPlayer) currentPlayer, window, this, propertyManager
                  .CardAt(gameplan.PlayerPosition(currentPlayer)));
            window.Enabled = true;
        }

        /**
        * Called when gameButton2 was clicked, it analyses the game situation (using GameState attribute)
        * and then handles the click button accordingly.
        */
        private void Button2_action()
        {
            if (GameState == GameStage.NO_ACTION) { return;  }

            Card card = (Card)propertyManager
                 .CardAt(gameplan.PlayerPosition(currentPlayer));

            GameState = GameStage.NO_ACTION;

            switch (GameState)
            {
                case GameStage.DICE:
                    SaveButton_action();
                    GameState = GameStage.DICE;
                    break;

                case GameStage.NO_FUNDS_PAY:
                    window.ShowMortgagedProperties(propertyManager.GetMortgagedProperties(currentPlayer, false), true);
                    GameState = GameStage.MORTGAGE_TAKE;
                    break;

                case GameStage.WHAT_NEXT:
                    window.ShowMortgageMenu();
                    GameState = GameStage.MORTGAGE_MENU;
                    break;

                case GameStage.TRADE_CONFIRM:
                    window.ShowPlayerInfo(currentPlayer);
                    window.ShowMessage("Your offer was declined!");
                    Thread.Sleep(2000);
                    break;

                case GameStage.MORTGAGE_MENU:
                    window.ShowMortgagedProperties(propertyManager.GetMortgagedProperties(currentPlayer, true), false);
                    GameState = GameStage.MORTGAGE_PAY;
                    break;

                default:
                    break;
            }
            if (GameState == GameStage.NO_ACTION) ShowWhatNext(currentPlayer);

            if (currentPlayer is AIPlayer) AIDecider.PlayTurn((AIPlayer)currentPlayer, window, this, propertyManager
                  .CardAt(gameplan.PlayerPosition(currentPlayer)));
        }

        /**
         * Called when gameButton3 was clicked, it analyses the game situation (using GameState attribute)
         * and then handles the click button accordingly.
         */
        private void Button3_action()
        {
            GameState = GameStage.NO_ACTION;

            switch (GameState)
            {
                case GameStage.WHAT_NEXT:
                    NextPlayer();
                    break;

                case GameStage.MORTGAGE_MENU:
                    window.ShowPlayerInfo(currentPlayer);
                    window.ShowNextOptions();
                    GameState = GameStage.WHAT_NEXT;
                    break;

                default:
                    ShowWhatNext(currentPlayer);
                    break;
            }
        }

        /**
         * Handles the event of saving the game - opens SaveFileDialog and then saves the game.
         */
        private void SaveButton_action()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "mon",
                CheckPathExists = true,
                FileName = "game.mon",
                Filter = "Monopoly saved games (*.mon)|*.mon|All files (*.*)|*.*",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                OverwritePrompt = true,
                ShowHelp = true,
                Title = "Choose save destination",
                ValidateNames = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    object someObject = Process.GetCurrentProcess();
                    //Dump(someObject);
                    using (Stream SaveFileStream = File.Create(sfd.FileName))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(SaveFileStream, this);
                        SaveFileStream.Close();
                    }
                }
                catch (IOException e)
                {
                    MessageBox.Show("Failed to save the game. Try again!");
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        /**
         * Simulates the Dice roll. Then calls a method to perform 
         * a start-of-turn action.
         */
        public void RollDice()
        {
            int roll = Dice.Instance.Roll(window);
            FieldType field = FieldType.VISIT;
            for (int i=0; i < roll; i++)
            {
                field = gameplan.Move(currentPlayer);
                window.MovePlayer(playerTurnPointer, 
                    gameplan.GetCoordsOfNextField(currentPlayer, playerTurnPointer));
                if (gameplan.PlayerPosition(currentPlayer) == 0)
                {
                    currentPlayer.Money += START_BONUS;
                    window.ShowPlayerInfo(currentPlayer);
                    if (currentPlayer is HumanPlayer)
                    {
                        MessageBox.Show("You have passed the start and 30m have been added to your account");
                    }
                }
                Thread.Sleep(200);
            }
            Action(field, roll);
        }
        
        /**
         * Performs the start-of-turn action according to where the currentPlayer
         * is located and the type of field.
         */
        private void Action(FieldType fieldType, int diceRoll)
        {
            switch (fieldType)
            {
                case FieldType.PROPERTY:
                    DefaultCardAction(
                        (PropertyCard)propertyManager
                        .CardAt(gameplan.PlayerPosition(currentPlayer)), -1);
                    return;

                case FieldType.TREASURE:
                    TreasureCard treasureCard = treasureCardManager.GetTreasureCard();
                    window.DisplayTreasureCard(treasureCard);
                    currentPlayer.Money += treasureCard.MoneyChange;
                    GameState = GameStage.SPECIAL_CARD;
                    return;

                case FieldType.RISK:
                    RiskCard riskCard = riskCardManager.GetRiskCard();
                    window.DisplayRiskCard(riskCard);
                    PlayRiskCard(riskCard);
                    GameState = GameStage.SPECIAL_CARD;
                    return;

                case FieldType.START:
                    currentPlayer.Money += START_BONUS;
                    NextPlayer();
                    return;

                case FieldType.PRISON:
                    GameState = GameStage.NO_ACTION;
                    currentPlayer.Blocked += 3;
                    currentPlayer.Prison = true;
                    window.MovePlayer(playerTurnPointer, gameplan.GoToPrison(currentPlayer, playerTurnPointer));
                    window.ShowMessage("You are imprisoned for 3 turns!");
                    Thread.Sleep(2000);
                    NextPlayer();
                    return;

                case FieldType.PARKING:
                    window.ShowMessage("You are parking for 1 turn");
                    currentPlayer.Blocked += 2;
                    GameState = GameStage.SPECIAL_FIELD;
                    return;

                case FieldType.TAX:
                    window.ShowMessage("Each of us has to pay taxes!");
                    currentPlayer.Money -= TAX_PRICE;
                    GameState = GameStage.SPECIAL_FIELD;
                    break;

                case FieldType.AGENCY:
                    Player agencyOwner = GetOwner(currentPlayer);
                    AgencyCard agencyCard = (AgencyCard)propertyManager
                            .CardAt(gameplan.PlayerPosition(currentPlayer));

                    DefaultCardAction(agencyCard, diceRoll);
                    break;

                case FieldType.TAX_FINE:
                    window.ShowMessage("You have to pay taxes!");
                    currentPlayer.Money -= TAX_FINE_COST;
                    GameState = GameStage.SPECIAL_FIELD;
                    break;

                case FieldType.BONUS_PROPERTY:
                    Player bonusOwner = GetOwner(currentPlayer);
                    BonusCard bonusCard = (BonusCard)propertyManager
                        .CardAt(gameplan.PlayerPosition(currentPlayer));
                    DefaultCardAction(bonusCard, 0);
                    break;

                case FieldType.VISIT:
                    window.ShowMessage("You are on a holiday and skip your turn.");
                    GameState = GameStage.HOLIDAY;
                    return;

                default:
                    break;
            }
        }

        /**
         * The action when the player stepped onto a Card field.
         */
        private void DefaultCardAction(Card card, int diceRoll)
        {
            Player cardOwner = propertyManager.WhoOwns(card);
            if (cardOwner == null)
            {
                if (currentPlayer.Money < card.Cost)
                {
                    window.ShowMessage("You have not got enough money to buy this property");
                    ShowCard(card);
                    GameState = GameStage.NO_FUNDS_BUY;
                    return;
                }
                window.PurchaseButtons(card.Name);
                ShowCard(card);
                GameState = GameStage.AWAITING_PURCHASE;
                return;
            }
            else if (cardOwner == currentPlayer)
            {
                if (card is PropertyCard)
                {
                    if (propertyManager.OwnsWholeGroup(card.Group, currentPlayer))
                    {
                        if (((PropertyCard)card).ApartmentCost > currentPlayer.Money)
                        {
                            window.ShowMessage("You have not got enough money to upgrade this property");
                            GameState = GameStage.NO_FUNDS_BUY;
                        }
                        else
                        {
                            window.ShowUpgradeOptions(card.Name, ((PropertyCard)card).ApartmentCost);
                            ShowCard(card);
                            GameState = GameStage.AWAITING_UPGRADE;
                        }
                    }
                }
                else
                {
                    ShowWhatNext(currentPlayer);
                }
                return;
            }
            else
            {
                if (cardOwner.Prison)
                {
                    window.ShowMessage("The owner of this property is in prison. Lucky you!");
                    GameState = GameStage.NO_FUNDS_BUY;
                    return;
                }
                else if (card.Mortgaged)
                {
                    window.ShowMessage("This property is mortgaged and you don't have to pay!");
                    GameState = GameStage.NO_FUNDS_BUY;
                    return;
                }
                else
                {
                    float money;
                    int propsOwned;
                    if (diceRoll > 0)
                    {
                        propsOwned = PropsOwned(cardOwner, "agency");
                        money = ((AgencyCard)card).GetPayment(diceRoll, propsOwned);
                    }
                    else if (diceRoll < 0)
                    {
                        money = card.GetPayment();
                    }
                    else
                    {
                        propsOwned = PropsOwned(cardOwner, "bonus");
                        money = ((BonusCard)card).GetPayment(propsOwned);

                    }
                    if (money > currentPlayer.Money)
                    {
                        window.ShowNoMoneyPay();
                        GameState = GameStage.NO_FUNDS_PAY;
                        return;
                    }
                    currentPlayer.Money -= money;
                    cardOwner.Money += money;
                    window.ShowPayment(cardOwner.name, money);
                    ShowCard(card);
                    GameState = GameStage.AWAITING_RENT;
                }
                return;
            }
        }

        /**
         * Calls the correct DisplayCard of the gamewindow class, 
         * accoridng to the Card type.
         */
        private void ShowCard(Card card)
        {
            if (card is AgencyCard)
            {
                window.DisplayCard((AgencyCard)card);
            }
            else if (card is BonusCard)
            {
                window.DisplayCard((BonusCard)card);
            }
            else if (card is PropertyCard)
            {
                window.DisplayCard((PropertyCard)card, Color.FromArgb(
                colorGroups[card.Group, 0],
                colorGroups[card.Group, 1],
                colorGroups[card.Group, 2]));
            }
        }

        /**
         * Performs playing a risk card - showing the card and its action.
         */
        private void PlayRiskCard(RiskCard riskCard)
        {
            if (riskCard.MoneyPlusMinus == "plus")
            {
                currentPlayer.Money += riskCard.MoneyChange;
            }
            else
            {
                currentPlayer.Money -= riskCard.MoneyChange;
            }
            currentPlayer.Blocked += riskCard.TurnsStop;
            if (riskCard.TurnsStop > 0)
            {
                NextPlayer();
            }
        }

        /**
         * When a player makes a trade offer, it displays it and prompts
         * another player to accept or reject the offer.
         */
        private void MakeTradeOffer()
        {
            if (window.GetSelectedItem() != null)
            {
                ListViewItem lvi = window.GetSelectedItem();
                selectedItem = (Card)lvi.Tag;
                float offeredMoney = window.GetOfferedMoney();
                window.ShowTradeOffer(((Card)selectedItem).Name, offeredMoney,
                    propertyManager.WhoOwns(selectedItem));
                GameState = GameStage.TRADE_CONFIRM;
                if (propertyManager.WhoOwns(selectedItem) is AIPlayer)
                {
                    window.SetTextBox("");
                    Thread.Sleep(500);
                    window.Update();
                    if (AIDecider.TradeOfferDecide((Card)selectedItem, offeredMoney, propertyManager, (AIPlayer)propertyManager.WhoOwns(selectedItem)))
                    {
                        window.SetTextBox("Your trade offer was accepted.");
                        Thread.Sleep(700);
                        propertyManager.ChangeOwner(currentPlayer,
                            selectedItem, window.GetOfferedMoney());
                    }
                    else
                    {
                        window.SetTextBox("Your trade offer was declined.");
                        Thread.Sleep(700);
                    }
                    ShowWhatNext(currentPlayer);
                    return;
                }
            }
        }

        /**
         * Performs all te steps that are needed when taking a mortgage:
         * - mark property as mortgaged
         * - add the money to player 
         */
        private void TakeMortgage()
        {
            if (window.GetSelectedItem() != null)
            {
                GameState = GameStage.NO_ACTION;
                foreach (ListViewItem lvi in window.GetSelectedItems())
                {
                    Card mortgagedCard = (Card)lvi.Tag;
                    currentPlayer.Money += mortgagedCard.MortgageValue;
                    mortgagedCard.Mortgaged = true;
                }
            }
        }

        /**
         * The inverse method to TakeMortgage() - removes the card cost from 
         * player's money and marks the card as NOT mortgaged.
         */
        private void PayMortgageOff()
        {
            if (window.GetSelectedItem() != null)
            {
                GameState = GameStage.NO_ACTION;
                Card mortgagedCard = (Card)window.GetSelectedItem().Tag;
                currentPlayer.Money -= mortgagedCard.Cost;
                mortgagedCard.Mortgaged = false;
            }
        }

        /**
         * When the player goes bankrupt, it removes him from the array of players
         */
        private void RemovePlayer()
        {
            Player[] newArray = new Player[players.Length - 1];
            int i = 0;
            foreach (Player player in players)
            {
                if (player != currentPlayer)
                {
                    newArray[i] = player;
                    i++;
                }
            }
            players = newArray;
            if (players.Length == 1)
            {
                MessageBox.Show(players[0].name + " has won the game! CONGRATULATIONS!");
                window.mainMenuButton.PerformClick();
            }
        }

        /**
         * Returns the player who owns the Card, where the player from arguments is
         * on the gameplan.
         * Returns null if the Card is not owned by anyone.
         */
        private Player GetOwner(Player player)
        {
            Card agencyCard = propertyManager.CardAt(gameplan.PlayerPosition(player));
            if (agencyCard != null)
            {
                return propertyManager.WhoOwns(agencyCard);
            }
            return null;
        }

        /**
         * Returns the amount of agencies/bonus cards owned by the given player.
         * The type attribute can be either "agency" or "bonus" given which 
         * count we want to get.
         */
        private int PropsOwned(Player player, string type)
        {
            if (type == "agency")
            {
                return propertyManager.CountGroup(gameplan.agencyFields.ToArray(), player);
            } else
            {
                return propertyManager.CountGroup(gameplan.bonusFields.ToArray(), player);
            }
        }

        /**
         * Procedure that ends the currentPlayer's turna nd sets up the gameplan
         * for another player's turn.
         */
        private void NextPlayer()
        {
            if (currentPlayer is AIPlayer) ((AIPlayer)currentPlayer).Trade = true; 
            do
            {
                if (players[playerTurnPointer].Blocked > 0)
                {
                    players[playerTurnPointer].Blocked -= 1;
                }
                playerTurnPointer++;
                playerTurnPointer = playerTurnPointer % players.Length;
            } while (players[playerTurnPointer].Blocked > 0);

            currentPlayer = players[playerTurnPointer];
            window.ShowPlayerInfo(currentPlayer);
            window.PrepareForDice(currentPlayer.Color);

            if (currentPlayer is AIPlayer) window.gameButton2.Hide();
            window.Update();
            GameState = GameStage.DICE;
            if (currentPlayer is AIPlayer)
            {
                AIDecider.PerformButtonClick(window.gameButton1);
                ClickButton(1);
            }
        }

        /**
         * Draws a newly built apartment onto the gameplan
         */
        private void DrawApartment(Point coords)
        {
            window.DrawApartment(coords);
        }

        /*
         * Shows the menu after the start-of-turn action.
         */
        public void ShowWhatNext(Player p)
        {
            window.ShowPlayerInfo(p);
            window.ShowNextOptions();
            window.ShowPlayerProperties(propertyManager.GetPlayerProperties(p));
            GameState = GameStage.WHAT_NEXT;
        }

        /**
         * Assigns the gamewindow to the property. This is used after Serialization,
         * because the graphical window is not Serialized and after loading game, 
         * the newly created game window is not assigned to the Game's attribute window.
         */
        public void SetWindow(Monopoly gw)
        {
            window = gw;
        }

        /**
         * Returns the array of players.
         */         
        public Player[] GetPlayers()
        {
            return players;
        }

        /**
         * Called after laoding game to position the player characters onto
         * correct field of the gameplan.
         */
        public void UpdateWindow()
        {
            window.ShowPlayerInfo(currentPlayer);
            int i = 0;
            foreach (Player p in players)
            {
                window.MovePlayer(i, gameplan.GetCoordsOfNextField(p, i));
                i+=1;
            }
            ReloadCallBacks();
        }

        /**
         * Used for logics of the AI - returns the card which is wanted 
         * the most by the AIPlayer. Returns null if no card matches
         * the parameters.
         */
        public Card GetTradeCard()
        {
            return propertyManager.GetTradeCard((AIPlayer) currentPlayer);
        }

        /**
         * Updates game window to display trade offer confirmed by the currentPlayer.
         */
        public void SendTradeOffer(Card card, float money)
        {
            selectedItem = card;
            float offeredMoney = window.GetOfferedMoney();
            window.ShowTradeOffer((selectedItem).Name, offeredMoney,
                propertyManager.WhoOwns(selectedItem));
            GameState = GameStage.TRADE_CONFIRM;
        }

        /**
         * Returns the list of Cards which are owned by currentPlayer
         * and ARE mortgaged.
         */
        public List<ListViewItem> HasMortgagedProperties()
        {
            return propertyManager.GetMortgagedProperties(currentPlayer, true);
        }

        /**
         * Mortgages selected properties in the tradeViewer listbox.
         */
        public void MortgageProperty()
        {
            List<ListViewItem> items = propertyManager.GetMortgagedProperties(currentPlayer, false);
            foreach (ListViewItem lvi in items)
            {
                if (currentPlayer.Money > 20)
                {
                    break;
                }
                Card c = (Card)lvi.Tag;
                currentPlayer.Money += c.MortgageValue;
                c.Mortgaged = true;
                window.SetTextBox(currentPlayer.name + " mortgaged " + c.Name);
                Thread.Sleep(1000);
            }
            
        }

        /**
         * Unmortgages the given card.
         */
        public void UnMortgage(Card p)
        {
            currentPlayer.Money -= p.Cost;
            p.Mortgaged = false;
            window.SetTextBox(currentPlayer.name + " unmortgaged " + p.Name);
            Thread.Sleep(1000);
        }

        /*
         * For debugging Serialisation issues only.
        static void Dump(object x)
        {
            Dump(x, 0, new HashSet<object>());
        }

        static void Dump(object x, int indent, HashSet<object> seen)
        {
            if (seen.Contains(x)) // stop cycles
                Console.WriteLine("(saw this already)");
            else
            {
                seen.Add(x);
                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                foreach (var f in x.GetType().GetFields(bindingFlags))
                {
                    var value = f.GetValue(x);
                    var valueTypeStr = value == null ? "null" : value.GetType().Name;
                    Console.WriteLine("{0}{1} {2} = [{3}]", new string(' ', indent), f.FieldType, f.Name, valueTypeStr);
                    if (value != null && !value.GetType().IsPrimitive && !(value is string))
                        if (value is IEnumerable<object>)
                        {
                            int index = 0;
                            foreach (var item in (IEnumerable<object>)value)
                            {
                                Console.WriteLine("{0}[{1}]", new string(' ', indent + 2), index++);
                                Game.Dump(item, indent + 4, seen);
                            }
                        }
                        else
                            Dump(value, indent + 2, seen);
                }
            }
        }
        */
    }
}
