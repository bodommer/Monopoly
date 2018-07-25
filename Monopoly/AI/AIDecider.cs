using Monopoly.Cards;
using Monopoly.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Main;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Monopoly.AI
{
    static class AIDecider
    {
        public static void PlayTurn(AIPlayer player, Main.Monopoly window, Game game, IPurchasable card)
        {
            switch (game.GameState)
            {
                case Game.GameStage.DICE:
                    PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.NO_FUNDS:
                    PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.NO_ACTION:
                    PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.AWAITING_PURCHASE:
                    if (ShallBuy(card, player))
                    {
                        PerformButtonClick(window.gameButton1);
                    }
                    else
                    {
                        PerformButtonClick(window.gameButton2);
                    }
                    break;

                case Game.GameStage.AWAITING_RENT:
                    PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.AWAITING_UPGRADE:
                    if (ShallUpgrade(card, player))
                    {
                        PerformButtonClick(window.gameButton1);
                    }
                    else
                    {
                        PerformButtonClick(window.gameButton2);
                    }
                    break;

                case Game.GameStage.NO_FUNDS_PAY:
                    PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.NO_FUNDS_BUY:
                    PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.CANNOT_UPGRADE:
                    PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.SPECIAL_CARD:
                    PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.SPECIAL_FIELD:
                    PerformButtonClick(window.gameButton1);
                    break;
                    
                case Game.GameStage.WHAT_NEXT:
                    if (player.Trade)
                    {
                        player.Trade = false;
                        IPurchasable c = game.GetTradeCard();
                        if (c != null)
                        {
                            float offeredMoney;
                            if (player.offers.ContainsKey(c))
                            {
                                offeredMoney = player.offers[c] * (1 + player.dangerFactor / 4);
                            }
                            else {
                                offeredMoney = (float)Math.Round(((Card)c).Cost * (1.4 + player.dangerFactor / 2), 2);
                            }
                            game.SendTradeOffer(c, offeredMoney);
                            break;
                        }
                    }
                    else
                    {
                        if (player.Money < 20)
                        {
                            game.MortgageProperty(player.Money);
                            break;
                        }
                        List<ListViewItem> properties = game.HasMortgagedProperties();
                        if (properties.Count > 0)
                        {
                            foreach (ListViewItem lvi in properties)
                            {
                                IPurchasable p = (IPurchasable) lvi.Tag;
                                Card c = (Card)p;
                                if (c.Mortgaged && c.Cost + 20 > player.Money)
                                {
                                    game.UnMortgage(p);
                                }
                            }
                            break;
                        }
                        PerformButtonClick(window.gameButton3);
                    }
                    break;

                case Game.GameStage.TRADE_OFFER:

                    break;

                case Game.GameStage.TRADE_CONFIRM:

                    break;
                    
                case Game.GameStage.MORTGAGE_MENU:

                    break;

                case Game.GameStage.MORTGAGE_TAKE:

                    break;

                case Game.GameStage.MORTGAGE_PAY:

                    break;

                case Game.GameStage.HOLIDAY:
                    PerformButtonClick(window.gameButton1);
                    break;
                    
                default:
                    break;
            }
        }
        
        private static void PerformButtonClick(Button b)
        {
            Thread.Sleep(500);
            Color c = b.BackColor;
            b.BackColor = Color.Red;
            Thread.Sleep(1000);
            b.BackColor = c;
            b.PerformClick();
        }

        public static bool ShallBuy(IPurchasable card, AIPlayer player) 
        {
            Card c = (Card)card;
            float coeffitient = (float) 3.2 - 3 * player.dangerFactor;
             if (coeffitient * c.Cost <= player.Money)
            {
                return true;
            }
            return false;
        }

        public static bool ShallUpgrade(IPurchasable card, AIPlayer player)
        {
            Card c = (Card)card;
            float coeffitient = (float) 2.1 - player.dangerFactor;
            if (coeffitient * c.Cost <= player.Money)
            {
                return true;
            }
            return false;
        }

        public static Card OfferTrade()
        {
            return null;
        }

        public static bool AcceptTradeOffer()
        {
            return false;
        }

        public static IPurchasable TakeMortgage()
        {
            return null;
        }

        public static IPurchasable PayOffMortgage()
        {
            return null;
        }



    }
}
