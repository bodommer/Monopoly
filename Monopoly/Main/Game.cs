using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Monopoly.Forms;
using Monopoly.AI;

namespace Monopoly.Main
{
    [Serializable()]
    public class Game
    {
        public enum GameStage { DICE, NO_FUNDS, NO_ACTION, AWAITING_PURCHASE,
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

        private bool process = true;

        int[,] colorGroups;
        Dice dice = Dice.Instance;
        RiskCardManager riskCardManager;
        TreasureCardManager treasureCardManager;
        Cards.PropertyManager propertyManager;
        public Gameplan gameplan;
        int playerTurnPointer = 0;
        Player[] players;
        Player currentPlayer;
        IPurchasable selectedItem = null;
        private const string FileName = "serialized.bin";

        public Game(Player[] players, Monopoly window)
        {
            this.players = players;
            GameState = GameStage.DICE;
            this.window = window;
            colorGroups = new int[,] { { 0, 0, 0 }, { 255, 51, 51 }, { 0, 128, 255 }, { 152, 76, 0 },
                                             {255, 153, 51 }, {255, 102, 255 }, {0, 0, 204 },
                                             {0, 204, 102 }, {255, 255, 51 } };
            currentPlayer = players[playerTurnPointer];
            PrepareGame(new GameSettings());
            window.Load += OnLoad;
            window.Enabled = true;
        }

        public Game()
        {
            if (File.Exists(FileName))
            {
                Console.WriteLine("Reading saved file");
                Stream openFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                Game game = (Game)deserializer.Deserialize(openFileStream);
                openFileStream.Close();
            }
        }

        private void OnLoad(object s, EventArgs ea)
        {
            window.gameButton1.Click += B1Callback;
            window.gameButton2.Click += B2Callback;
            window.gameButton3.Click += B3Callback;
            window.KeyPress += KeyPressed;
            window.exitButton.Click += EBCallback;
        }

        private void ReloadCallBacks()
        {
            window.gameButton1.Click += B1Callback;
            window.gameButton2.Click += B2Callback;
            window.gameButton3.Click += B3Callback;
            window.KeyPress += KeyPressed;
            window.exitButton.Click += EBCallback;
        }

        private void RemoveCallBacks()
        {
            window.gameButton1.Click -= B1Callback;
            window.gameButton2.Click -= B2Callback;
            window.gameButton3.Click -= B3Callback;
            window.KeyPress -= KeyPressed;
            window.exitButton.Click -= EBCallback;
        }

        private void ExitProcedure()
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to quit? All unsaved progress will be lost!", "Quit", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void SaveProcedure()
        {
        }

        private void B1Callback (object sender, EventArgs ea)
        {
            if (process)
            {
                process = false;
                Application.DoEvents();
                Button1_action();
                
                process = true;
            }
        }

        private void B2Callback(object sender, EventArgs ea)
        {
            Button2_action();
        }

        private void B3Callback(object sender, EventArgs ea)
        {
            Button3_action();
        }

        private void KeyPressed(object sender, KeyPressEventArgs ea)
        {
           switch (ea.KeyChar)
            {
                case '1':
                    if (process)
                    {
                        process = false;
                        Application.DoEvents();
                        Button1_action();
                        process = true;
                        break;
                    }
                    break;

                case '2':
                    Button2_action();
                    break;

                case '3':
                    Button3_action();
                    break;

                case 's':
                    SaveButton_action();
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

        private void EBCallback(object sender, EventArgs ea)
        {
            ExitProcedure();
        }

        private void PrepareGame(GameSettings settings)
        {
            
            riskCardManager = new RiskCardManager("riskCards.txt");
            treasureCardManager = new TreasureCardManager("treasureCards.txt");
            propertyManager = new Cards.PropertyManager("properties.txt");
            gameplan = new Gameplan(players, propertyManager.GetFieldTypes());
            window.ShowPlayerInfo(currentPlayer);
        }

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


        private void Button1_action()
        {
            if (GameState == GameStage.NO_ACTION) { return; }

            window.Enabled = false;

            Card card = (Card)propertyManager
                  .CardAt(gameplan.PlayerPosition(currentPlayer));
            Player owner = propertyManager.WhoOwns(card);

            switch (GameState)
            {
                case GameStage.DICE:
                    window.HideButton();
                    GameState = GameStage.NO_ACTION;
                    RollDice();
                    break;

                case GameStage.AWAITING_PURCHASE:
                    GameState = GameStage.NO_ACTION;
                    propertyManager.AddOwnership(card, currentPlayer);
                    currentPlayer.Money -= card.Cost;
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.AWAITING_RENT:
                    GameState = GameStage.NO_ACTION;
                    float money = card.GetPayment();
                    currentPlayer.Money -= money;
                    owner.Money += money;
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.AWAITING_UPGRADE:
                    GameState = GameStage.NO_ACTION;
                    PropertyCard c2 = (PropertyCard)card;
                    window.DrawApartment(
                        gameplan.GetCoordsOfNextApartment(
                            gameplan.PlayerPosition(
                                currentPlayer), c2.Apartments));
                    c2.AddApartment();
                    currentPlayer.Money -= c2.ApartmentCost;
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.CANNOT_UPGRADE:
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.SPECIAL_CARD:
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.SPECIAL_FIELD:
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.NO_FUNDS_PAY:
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
                    NextPlayer();
                    break;

                case GameStage.NO_FUNDS_BUY:
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.WHAT_NEXT:
                    GameState = GameStage.NO_ACTION;
                    window.ShowTradeOptions(propertyManager
                        .GetTradeOptions(currentPlayer),
                        currentPlayer);
                    GameState = GameStage.TRADE_OFFER;
                    break;

                case GameStage.TRADE_OFFER:
                    if (window.GetSelectedItem() != null)
                    {
                        GameState = GameStage.NO_ACTION;
                        ListViewItem lvi = window.GetSelectedItem();
                        selectedItem = (IPurchasable)lvi.Tag;
                        float offeredMoney = window.GetOfferedMoney();
                        window.ShowTradeOffer(((Card)selectedItem).Name, offeredMoney, 
                            propertyManager.WhoOwns(selectedItem));
                        GameState = GameStage.TRADE_CONFIRM;
                        if (propertyManager.WhoOwns(selectedItem) is AIPlayer)
                        {
                            window.SetTextBox("");
                            Thread.Sleep(500);
                            window.Update();
                            if (AIDecider.TradeOfferDecide((Card) selectedItem, offeredMoney, propertyManager, (AIPlayer) propertyManager.WhoOwns(selectedItem)))
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
                            GameState = GameStage.WHAT_NEXT;
                            return;
                        }
                    }
                    break;

                case GameStage.TRADE_CONFIRM:
                    GameState = GameStage.NO_ACTION;
                    propertyManager.ChangeOwner(currentPlayer, 
                        selectedItem, window.GetOfferedMoney());
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.MORTGAGE_MENU:
                    GameState = GameStage.NO_ACTION;
                    window.ShowMortgagedProperties(propertyManager.GetMortgagedProperties(currentPlayer, false), true);
                    GameState = GameStage.MORTGAGE_TAKE;
                    break;

                case GameStage.MORTGAGE_TAKE:
                    if (window.GetSelectedItem() != null)
                    {
                        GameState = GameStage.NO_ACTION;
                        foreach (ListViewItem lvi in window.GetSelectedItems())
                        {
                            Card mortgagedCard = (Card)lvi.Tag;
                            currentPlayer.Money += mortgagedCard.MortgageValue;
                            mortgagedCard.Mortgaged = true;
                        }
                        ShowWhatNext(currentPlayer);
                    }
                    break;

                case GameStage.MORTGAGE_PAY:
                    if (window.GetSelectedItem() != null)
                    {
                        GameState = GameStage.NO_ACTION;
                        Card mortgagedCard = (Card)window.GetSelectedItem().Tag;
                        currentPlayer.Money -= mortgagedCard.Cost;
                        mortgagedCard.Mortgaged = false;
                        ShowWhatNext(currentPlayer);
                    }
                    break;
                    
                case GameStage.HOLIDAY:
                    NextPlayer();
                    break;

                default:
                    Console.Write("An unexpected error occurred! " + GameState);
                    break;
            }
            window.Update();
            if (currentPlayer is AIPlayer) AIDecider.PlayTurn((AIPlayer) currentPlayer, window, this, propertyManager
                  .CardAt(gameplan.PlayerPosition(currentPlayer)));
            window.Enabled = true;
        }

        private void Button2_action()
        {
            if (GameState == GameStage.NO_ACTION) { return;  }

            Card card = (Card)propertyManager
                 .CardAt(gameplan.PlayerPosition(currentPlayer));

            switch (GameState)
            {
                case GameStage.DICE:
                    SaveButton_action();
                    break;

                case GameStage.AWAITING_PURCHASE:
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.NO_FUNDS_PAY:
                    GameState = GameStage.NO_ACTION;
                    window.ShowMortgagedProperties(propertyManager.GetMortgagedProperties(currentPlayer, false), true);
                    GameState = GameStage.MORTGAGE_TAKE;
                    break;

                case GameStage.AWAITING_UPGRADE:
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.WHAT_NEXT:
                    GameState = GameStage.NO_ACTION;
                    window.ShowMortgageMenu();
                    GameState = GameStage.MORTGAGE_MENU;
                    break;

                case GameStage.TRADE_OFFER:
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.TRADE_CONFIRM:
                    GameState = GameStage.NO_ACTION;
                    window.ShowPlayerInfo(currentPlayer);
                    window.ShowMessage("Your offer was declined!");
                    Thread.Sleep(2500);
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.MORTGAGE_MENU:
                    GameState = GameStage.NO_ACTION;
                    window.ShowMortgagedProperties(propertyManager.GetMortgagedProperties(currentPlayer, true), false);
                    GameState = GameStage.MORTGAGE_PAY;
                    break;

                case GameStage.MORTGAGE_TAKE:
                    ShowWhatNext(currentPlayer);
                    break;

                case GameStage.MORTGAGE_PAY:
                    ShowWhatNext(currentPlayer);
                    break;

                default:
                    break;
            }

            if (currentPlayer is AIPlayer) AIDecider.PlayTurn((AIPlayer)currentPlayer, window, this, propertyManager
                  .CardAt(gameplan.PlayerPosition(currentPlayer)));
        }

        private void Button3_action()
        {
            switch (GameState)
            {
                case GameStage.WHAT_NEXT:
                    NextPlayer();
                    break;

                case GameStage.MORTGAGE_MENU:
                    GameState = GameStage.NO_ACTION;
                    window.ShowPlayerInfo(currentPlayer);
                    window.ShowNextOptions();
                    GameState = GameStage.WHAT_NEXT;
                    break;

                default:
                    break;
            }
        }

        private void SaveButton_action()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "mon";
            sfd.CheckPathExists = true;
            sfd.FileName = "game.mon";
            sfd.Filter = "Monopoly saved games (*.mon)|*.mon|All files (*.*)|*.*";
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            sfd.OverwritePrompt = true;
            sfd.ShowHelp = true;
            sfd.Title = "Choose save destination";
            sfd.ValidateNames = true;

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
         
        private void Action(FieldType fieldType, int diceRoll)
        {
            switch (fieldType)
            {
                case FieldType.PROPERTY:
                    PropertyCard card = (PropertyCard)propertyManager
                        .CardAt(gameplan.PlayerPosition(currentPlayer));
                    if (card != null)
                    {
                        Player owner = propertyManager.WhoOwns(card);
                        if (owner == null)
                        {
                            if (currentPlayer.Money < card.Cost)
                            {
                                window.ShowMessage("You have not got enough money to buy this property");
                                window.DisplayPropertyCard(card, Color.FromArgb(
                                colorGroups[card.Group, 0],
                                colorGroups[card.Group, 1],
                                colorGroups[card.Group, 2]),
                                gameplan.PlayerPosition(currentPlayer));
                                GameState = GameStage.NO_FUNDS_BUY;
                                return;
                            }
                            window.PurchaseButtons(card.Name);
                            window.DisplayPropertyCard(card, Color.FromArgb(
                                colorGroups[card.Group, 0], 
                                colorGroups[card.Group, 1], 
                                colorGroups[card.Group, 2]), 
                                gameplan.PlayerPosition(currentPlayer));
                            GameState = GameStage.AWAITING_PURCHASE;
                            return;
                        }
                        else if (owner == currentPlayer)
                        {
                            //check if he has all other from group
                            if (propertyManager.OwnsWholeGroup(card.Group, currentPlayer))
                            {
                                if (card.ApartmentCost > currentPlayer.Money)
                                {
                                    window.ShowMessage("You have not got enough money to upgrade this property");
                                    GameState = GameStage.NO_FUNDS_BUY;
                                }
                                else
                                {
                                    window.ShowUpgradeOptions(card.Name, card.ApartmentCost);
                                    window.DisplayPropertyCard(card, Color.FromArgb(
                                            colorGroups[card.Group, 0],
                                            colorGroups[card.Group, 1],
                                            colorGroups[card.Group, 2]),
                                            gameplan.PlayerPosition(currentPlayer));
                                    GameState = GameStage.AWAITING_UPGRADE;
                                }
                            }
                            return;
                        }
                        else
                        {
                            if (owner.Prison)
                            {
                                window.ShowMessage("The owner of this property is in blocked. Lucky you!");
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
                                if (card.GetPayment() > currentPlayer.Money)
                                {
                                    window.ShowNoMoneyPay();
                                    GameState = GameStage.NO_FUNDS_PAY;
                                    return;
                                }
                                window.ShowPayment(owner.name, card.GetPayment());
                                window.DisplayPropertyCard(card, Color.FromArgb(
                                colorGroups[card.Group, 0],
                                colorGroups[card.Group, 1],
                                colorGroups[card.Group, 2]), 
                                gameplan.PlayerPosition(currentPlayer));
                                GameState = GameStage.AWAITING_RENT;
                                return;
                            }
                        }
                    }
                    // else error - this card is not purchasable!!
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
                    gameplan.GoToPrison(currentPlayer);
                    window.ShowMessage("You are imprisoned for 3 turns!");
                    Thread.Sleep(3000);
                    NextPlayer();
                    return;

                case FieldType.PARKING:
                    window.ShowMessage("You are parking for 1 turn");
                    currentPlayer.Blocked += 1;
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

                    if (agencyOwner == null)
                    {
                        if (currentPlayer.Money < agencyCard.Cost)
                        {
                            window.ShowMessage("You have not got enough money to buy this property");
                            GameState = GameStage.NO_FUNDS_BUY;
                            return;
                        }
                        window.PurchaseButtons(agencyCard.Name);
                        window.DisplayAgencyCard(agencyCard,
                            gameplan.PlayerPosition(currentPlayer));
                        GameState = GameStage.AWAITING_PURCHASE;
                        return;
                    }
                    else if (agencyOwner == currentPlayer)
                    {
                        return;
                    }
                    else
                    {
                        if (agencyOwner.Prison)
                        {
                            window.ShowMessage("The owner of this property is in blocked. Lucky you!");
                            GameState = GameStage.NO_FUNDS_BUY;
                            return;
                        }
                        else if (agencyCard.Mortgaged)
                        {
                            window.ShowMessage("This property is mortgaged and you don't have to pay!");
                            GameState = GameStage.NO_FUNDS_BUY;
                            return;
                        }
                        else
                        {
                            int agenciesOwned = PropsOwned(agencyOwner, "agency");
                            float money = agencyCard.GetPayment(diceRoll, agenciesOwned);
                            if (money > currentPlayer.Money)
                            {
                                window.ShowNoMoneyPay();
                                GameState = GameStage.NO_FUNDS_PAY;
                                return;
                            }
                            currentPlayer.Money -= money;
                            agencyOwner.Money += money;
                            window.ShowPayment(agencyOwner.name, money);
                            GameState = GameStage.AWAITING_RENT;
                        }
                        return;
                    }

                case FieldType.TAX_FINE:
                    window.ShowMessage("You have to pay taxes!");
                    currentPlayer.Money -= TAX_FINE_COST;
                    GameState = GameStage.SPECIAL_FIELD;
                    break;

                case FieldType.BONUS_PROPERTY:
                    Player bonusOwner = GetOwner(currentPlayer);
                    BonusCard bonusCard = (BonusCard)propertyManager
                        .CardAt(gameplan.PlayerPosition(currentPlayer));

                    if (bonusOwner == null)
                    {
                        if (currentPlayer.Money < bonusCard.Cost)
                        {
                            window.ShowMessage("You have not got enough money to buy this property");
                            GameState = GameStage.NO_FUNDS_BUY;
                            return;
                        }
                        window.PurchaseButtons(bonusCard.Name);
                        window.DisplayBonusCard(bonusCard,
                            gameplan.PlayerPosition(currentPlayer));
                        GameState = GameStage.AWAITING_PURCHASE;
                        return;
                    }
                    else if (bonusOwner == currentPlayer)
                    {
                        return;
                    }
                    else
                    {
                        if (bonusOwner.Prison)
                        {
                            window.ShowMessage("The owner of this property is in prison. Lucky you!");
                            GameState = GameStage.NO_FUNDS_BUY;
                            return;
                        }
                        else if (bonusCard.Mortgaged)
                        {
                            window.ShowMessage("This property is mortgaged and you don't have to pay!");
                            GameState = GameStage.NO_FUNDS_BUY;
                            return;
                        }
                        else
                        {
                            int bonusesOwned = PropsOwned(bonusOwner, "bonus");
                            float money = bonusCard.GetPayment(bonusesOwned);
                            if (money > currentPlayer.Money)
                            {
                                window.ShowNoMoneyPay();
                                GameState = GameStage.NO_FUNDS_PAY;
                                return;
                            }
                            currentPlayer.Money -= money;
                            bonusOwner.Money += money;
                            window.ShowPayment(bonusOwner.name, money);
                            GameState = GameStage.AWAITING_RENT;
                        }
                        return;
                    }

                case FieldType.VISIT:
                    window.ShowMessage("You are on a holiday and skip your turn.");
                    GameState = GameStage.HOLIDAY;
                    return;

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

        public string GetPlayerName()
        {
            return currentPlayer.name;
        }

        public float GetPlayerMoney()
        {
            return currentPlayer.Money;
        }

        private void DrawApartment(Point coords)
        {
            window.DrawApartment(coords);
        }

        public void ShowWhatNext(Player p)
        {
            GameState = GameStage.NO_ACTION;
            window.ShowPlayerInfo(p);
            window.ShowNextOptions();
            window.ShowPlayerProperties(propertyManager.GetPlayerProperties(p));
            GameState = GameStage.WHAT_NEXT;
        }

        public void SetWindow(Monopoly gw)
        {
            window = gw;
        }

        public Player[] GetPlayers()
        {
            return players;
        }

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

        public IPurchasable GetTradeCard()
        {
            return propertyManager.GetTradeCard((AIPlayer) currentPlayer);
        }

        public void SendTradeOffer(IPurchasable card, float money)
        {
            selectedItem = card;
            float offeredMoney = window.GetOfferedMoney();
            window.ShowTradeOffer(((Card)selectedItem).Name, offeredMoney,
                propertyManager.WhoOwns(selectedItem));
            GameState = GameStage.TRADE_CONFIRM;

        }

        public List<ListViewItem> HasMortgagedProperties()
        {
            return propertyManager.GetMortgagedProperties(currentPlayer, true);
        }

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

        public void UnMortgage(IPurchasable p)
        {
            Card c = (Card)p;
            currentPlayer.Money -= c.Cost;
            c.Mortgaged = false;
            window.SetTextBox(currentPlayer.name + " unmortgaged " + c.Name);
            Thread.Sleep(1000);
        }

        static void Dump(object x)
        {
            Game.Dump(x, 0, new HashSet<object>());
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
    }
}
