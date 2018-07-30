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

        private static readonly HashSet<Game.GameStage> defaultActions = new HashSet<Game.GameStage> {
            Game.GameStage.NO_ACTION, Game.GameStage.AWAITING_RENT,
            Game.GameStage.NO_FUNDS_BUY, Game.GameStage.SPECIAL_CARD, Game.GameStage.HOLIDAY,
            Game.GameStage.CANNOT_UPGRADE, Game.GameStage.SPECIAL_FIELD };

        public static void PlayTurn(AIPlayer player, Main.Monopoly window, Game game, Card card)
        {
            if (defaultActions.Contains(game.GameState))
            {
                PerformButtonClick(window.gameButton1);
                game.ClickButton(1);
                return;
            } 
            switch (game.GameState)
            {
                case Game.GameStage.AWAITING_PURCHASE:
                    if (ShallBuy(card, player))
                    {
                        PerformButtonClick(window.gameButton1);
                        game.ClickButton(1);
                    }
                    else
                    {
                        PerformButtonClick(window.gameButton2);
                        game.ClickButton(2);
                    }
                    break;

                case Game.GameStage.AWAITING_UPGRADE:
                    if (ShallUpgrade(card, player))
                    {
                        PerformButtonClick(window.gameButton1);
                        game.ClickButton(1);
                    }
                    else
                    {
                        PerformButtonClick(window.gameButton2);
                        game.ClickButton(2);
                    }
                    break;


                case Game.GameStage.SPECIAL_FIELD:
                    PerformButtonClick(window.gameButton1);
                    game.ClickButton(1);
                    break;
                    
                case Game.GameStage.WHAT_NEXT:
                    Console.Write("WHAT NEXT");
                    Card c = game.GetTradeCard();
                    if (player.Trade)
                    {
                        if (c != null)
                        {
                            float offeredMoney;
                            if (player.offers.ContainsKey(c))
                            {
                                offeredMoney = player.offers[c] * (1 + player.dangerFactor / 4);
                            }
                            else
                            {
                                offeredMoney = (float)Math.Round(((Card)c).Cost * (1.4 + player.dangerFactor / 2), 2);
                            }
                            player.Trade = false;
                            game.SendTradeOffer(c, offeredMoney);
                            break;
                        }
                    }
                    List<ListViewItem> properties = game.HasMortgagedProperties();
                    if (properties.Count > 0)
                    {
                        foreach (ListViewItem lvi in properties)
                        {
                            Card p = (Card) lvi.Tag;
                            Card ca = (Card)p;
                            if (ca.Mortgaged && ca.Cost + 20 > player.Money)
                            {
                                game.UnMortgage(p);
                            }
                        }
                        game.ShowWhatNext(player);
                    }
                    if (player.Money < 20)
                    {
                        game.MortgageProperty();
                    }
                    player.Trade = true;
                    PerformButtonClick(window.gameButton3);
                    game.ClickButton(3);
                    break;

                case Game.GameStage.NO_FUNDS_PAY:
                    PerformButtonClick(window.gameButton3);
                    game.ClickButton(3);
                    break;
                    
                default:
                    break;
            }
        }
        
        public static bool TradeOfferDecide(Card item, float money, Cards.PropertyManager pm, AIPlayer player)
        {
            if (item is PropertyCard)
            {
                PropertyCard propCard = (PropertyCard) item;
                float requiredMoney = (3 + player.dangerFactor) * propCard.Cost;
                if (!(pm.OwnsWholeGroup(item.Group, player)))
                {
                    if (money > requiredMoney)
                    {
                        return true;
                    }
                }
            }
            else if (item is BonusCard)
            {
                if (money > (2 +  player.dangerFactor) * item.Cost)
                {
                    return true;
                }
            }
            else
            {
                if (money > (1.5 + 2 * player.dangerFactor) * item.Cost )
                {
                    return true;
                }
            }
            return false;
        }

        public static void PerformButtonClick(Button b)
        {
            b.Update();
            Thread.Sleep(500);
            Color c = b.BackColor;
            b.BackColor = Color.Red;
            b.Update();
            Thread.Sleep(700);
            b.BackColor = c;
            b.Update();
            Thread.Sleep(300);
        }

        public static bool ShallBuy(Card card, AIPlayer player) 
        {
            Card c = (Card)card;
            float coeffitient = (float) 3.2 - 3 * player.dangerFactor;
             if (coeffitient * c.Cost <= player.Money)
            {
                return true;
            }
            return false;
        }

        public static bool ShallUpgrade(Card card, AIPlayer player)
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

        public static Card TakeMortgage()
        {
            return null;
        }

        public static Card PayOffMortgage()
        {
            return null;
        }



    }
}
