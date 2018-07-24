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
        public static void PlayTurn(Player player, Main.Monopoly window, Game game)
        {
            switch (game.GameState)
            {
                case Game.GameStage.DICE:
                    AIDecider.PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.NO_FUNDS:
                    AIDecider.PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.NO_ACTION:
                    AIDecider.PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.AWAITING_PURCHASE:

                    break;

                case Game.GameStage.AWAITING_RENT:
                    AIDecider.PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.AWAITING_UPGRADE:

                    break;

                case Game.GameStage.NO_FUNDS_PAY:
                    AIDecider.PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.NO_FUNDS_BUY:
                    AIDecider.PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.CANNOT_UPGRADE:
                    AIDecider.PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.SPECIAL_CARD:
                    AIDecider.PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.SPECIAL_FIELD:
                    AIDecider.PerformButtonClick(window.gameButton1);
                    break;

                case Game.GameStage.TRADE_OFFER:

                    break;

                case Game.GameStage.TRADE_CONFIRM:

                    break;

                case Game.GameStage.WHAT_NEXT:

                    break;

                case Game.GameStage.MORTGAGE_MENU:

                    break;

                case Game.GameStage.MORTGAGE_TAKE:

                    break;
                case Game.GameStage.MORTGAGE_PAY:

                    break;

                case Game.GameStage.HOLIDAY:

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

        public static bool ShallBuy()
        {
            return true;
        }

        public static bool Upgrade()
        {
            return true;
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
