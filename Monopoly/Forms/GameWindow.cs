using Monopoly.Cards;
using Monopoly.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Monopoly.Main.Game;

namespace Monopoly.Main
{
    /**
     * THe most important grapical class. This is where the game actually takes place.
     */
    [Serializable]
    public partial class Monopoly : Form
    {
        PictureBox[] player;
        int numberOfPlayers;

        /**
         * The constructor. Takes one argument - an array of players. These players need to be drawn
         * onto the start field.
         */
        public Monopoly(Player[] players)
        {
            numberOfPlayers = players.Length;

            InitializeComponent();
            this.Height = 800;
            this.Width = 1200;
            playerDetail.Image = Resource1.playerHub;
            drawArea.Image = Resource1.image_drawn;
        }

        /**
         * Draws the player characters onto the gameplan.
         */
        private void MainWindow_Load(object sender, EventArgs e)
        {
            player = new PictureBox[] { player1, player2, player3, player4, player5, player6 };
            player1.BackColor = Color.Green;
            player2.BackColor = Color.Blue;
            player3.BackColor = Color.Yellow;
            player4.BackColor = Color.Tomato;
            player5.BackColor = Color.Purple;
            player6.BackColor = Color.Peru;
            for (int i = 5; i >= numberOfPlayers; i--)
            {
                player[i].Hide();
            }
            //textBox.Location = new Point(20, 200);
            PrepareForDice(Color.Green);
        }

        /**
         * Prepares the displayed data for the 'dice-roll' stage if turn.
         */
        public void PrepareForDice(Color color)
        {
            HideElements();
            gameButton1.Text = "Roll";
            gameButton2.Text = "Save game";
            playerNameLabel.BackColor = color;
            textBox.Text = "Click on the button to start your turn.";
            gameButton1.Show();
            gameButton2.Show();
        }

        /**
         * Updates the number displayed on the dice.
         */
        public void ShowDiceNumber(int i)
        {
            diceLabel.Text = i.ToString();
            Update();
        }

        /**
         * Helper method.
         * Displays part of Card - the common things of PropertyCard, BonusCard and AgencyCard.
         */
        private void DisplayCard(Card card, Color foreColor, Color backColor)
        {
            logoBox.Width = 298;
            logoBox.Height = 140;
            CardTitle.Text = card.Name;
            CardTitle.BackColor = Color.White;
            CardTitle.ForeColor = Color.Black;
            Image logo = card.logo;
            logoBox.Padding = new Padding((296 - logo.Width) / 2, (136 - logo.Height) / 2, 0, 0);
            logoBox.Image = logo;
            priceLabel.Text = "Price: " + card.Cost + "m";
            ShowCard();
        }

        /**
         * Overloaded method. 
         * Displays the data unique for PropertyCard.
         */
        public void DisplayCard(PropertyCard card, Color color)
        {
            paymentLabel1.Text = "Payment 1: " + card.GetPayment(0) + "m";
            paymentLabel2.Text = "Payment 2: " + card.GetPayment(1) + "m";
            paymentLabel3.Text = "Payment 3: " + card.GetPayment(2) + "m";
            paymentLabel4.Text = "Payment 4: " + card.GetPayment(3) + "m";
            apartmentCostLabel.Text = "Improvement cost: " + card.ApartmentCost + "m";
            mortgageLabel.Text = "Mortgage: " + card.MortgageValue + "m";
            DisplayCard(card, Color.WhiteSmoke, color);
        }

        /**
         * Overloaded method. 
         * Displays the data unique for AgencyCard.
         */
        public void DisplayCard(AgencyCard card)
        {
            paymentLabel1.Text = "One agency owned payment: ";
            paymentLabel2.Text = card.GetBonus(1) + "m * dice roll value";
            paymentLabel3.Text = "Both agencies owned payment:";
            paymentLabel4.Text = card.GetBonus(2) + "m * dice roll value";
            apartmentCostLabel.Text = "";
            mortgageLabel.Text = "Mortgage: " + card.MortgageValue + "m";
            DisplayCard(card, Color.Black, Color.White);
        }

        /**
         * Overloaded method. 
         * Displays the data unique for BonusCard.
         */
        public void DisplayCard(BonusCard card)
        {
            paymentLabel1.Text = "Payment per bonu cards owned:";
            paymentLabel2.Text = "One card:" + card.GetPayment(1) + "m";
            paymentLabel3.Text = "Two cards:" + card.GetPayment(2) + "m";
            paymentLabel4.Text = "Three cards:" + card.GetPayment(3) + "m";
            apartmentCostLabel.Text = "Four cards:" + card.GetPayment(4) + "m";
            mortgageLabel.Text = "Mortgage: " + card.MortgageValue + "m";
            DisplayCard(card, Color.Black, Color.White);
        }

        /**
         * Helper method.
         * Does steps common for displaying SpecialCard - TreasureCard or RiskCard.
         */
        private void DisplaySpecialCard(SpecialCard card)
        {
            HideElements();
            detailBox.Show();
            textBox.Text = "You played a special card!";
            cardNameLabel.BorderStyle = BorderStyle.FixedSingle;
            cardContentLabel.BorderStyle = BorderStyle.FixedSingle;
            cardContentLabel.Text = card.Description;
            gameButton1.Text = "Continue";
            gameButton1.Show();
            cardNameLabel.Show();
            cardContentLabel.Show();
        }

        /**
         * Procedure for displaying the data of the RiskCard properly.
         */
        public void DisplayRiskCard(RiskCard card)
        {
            cardNameLabel.Text = "Risk Card";
            textBox.Text = "You played a risk card!";
            DisplaySpecialCard(card);
        }

        /**
         * Procedure for displaying the data of the TreasureCard properly.
         */
        public void DisplayTreasureCard(TreasureCard card)
        {
            cardNameLabel.Text = "Treasure Card";
            textBox.Text = "You played a trasure card!";
            DisplaySpecialCard(card);
        }

        /**
         * Prompts player to upgrade a PropertyCard if it is possible.
         */
        public void ShowUpgradeOptions(string name, float price)
        {
            HideElements();
            textBox.Text = "Do you want to upgrade " + name + " for \u20AC" + price.ToString() + "m?";
            gameButton1.Text = "Yes";
            gameButton2.Text = "No";
            gameButton1.Show();
            gameButton2.Show();
        }

        /**
         * Prompts player to either buy, or not buy a property.
         */
        public void PurchaseButtons(string name)
        {
            HideElements();
            textBox.Text = "Do you want to buy " + name + "?";
            gameButton1.Text = "Yes";
            gameButton2.Text = "No";
            gameButton1.Show();
            gameButton2.Show();
        }

        /**
         * Show information that the players has to pay "rent" to other player.
         */
        public void ShowPayment(string name, float payment)
        {
            HideElements();
            textBox.Text = "You must pay " + payment.ToString() + "m to " + name + ".";
            gameButton1.Text = "Ok";
            gameButton1.Show();
        }

        /**
         * When player has not got enough money to make a compulsory payment 
         * (a tax, rent or risk card), it prompts the player to either 
         * declare bankruptcy or take a mortgage to have sufficient funds
         * for payments.
         */
        public void ShowNoMoneyPay()
        {
            HideElements();
            textBox.Text = "You have not got enough money to pay!";
            gameButton1.Text = "Declare bankrupcy";
            gameButton2.Text = "Take out a mortgage";
            gameButton1.Show();
            gameButton2.Show();
        }

        /**
         * Shows a provided text message with only one, ok button.
         */
        public void ShowMessage(string msg)
        {
            HideElements();
            textBox.Text = msg;
            gameButton1.Text = "OK";
            gameButton1.Show();
            this.Update();
        }

        /**
         * Helper method.
         * Hides gameButtons.
         */
        public void HideButton()
        {
            gameButton1.Hide();
            gameButton2.Hide();
            gameButton3.Hide();
        }

        /**
         * Helper method.
         * Hides all the elements of the graphical window that don't need 
         * to be shown constantly (buttons, card, frames, ...). Erases texts
         * of textboxes.
         */
        private void HideElements()
        {
            textBox.Text = "";
            trackBarShower.Hide();
            gameButton1.Hide();
            gameButton2.Hide();
            gameButton3.Hide();
            HideCard();
            cardNameLabel.BorderStyle = BorderStyle.None;
            cardContentLabel.BorderStyle = BorderStyle.None;
            cardNameLabel.Text = "";
            cardContentLabel.Text = "";
            moneyTrackBar.Hide();
            tradeViewer.Hide();
        }

        /**
         * Helper method.
         * Hides a Card.
         */
        private void HideCard()
        {
            Card.Hide();
            CardTitle.Hide();
            priceLabel.Hide();
            paymentLabel1.Hide();
            paymentLabel2.Hide();
            paymentLabel3.Hide();
            paymentLabel4.Hide();
            apartmentCostLabel.Hide();
            mortgageLabel.Hide();
            logoBox.Hide();
        }

        /**
         * Helper method.
         * Shows a Card.
         */
        private void ShowCard()
        {
            Card.Show();
            CardTitle.Show();
            priceLabel.Show();
            paymentLabel1.Show();
            paymentLabel2.Show();
            paymentLabel3.Show();
            paymentLabel4.Show();
            apartmentCostLabel.Show();
            mortgageLabel.Show();
            logoBox.Show();
        }

        /**
         * Displays player info - player name and money in the top left corner.
         */
        public void ShowPlayerInfo(Player p)
        {
            playerNameLabel.Text = p.name;
            playerNameLabel.BackColor = p.Color;
            playerMoneyLabel.Text = "\u20AC " + p.Money.ToString() + "m";
        }

        /**
         * Shows the menu after the first action of turn.
         */
        public void ShowNextOptions()
        {
            HideElements();
            textBox.Text = "What do you want to do next?";
            gameButton1.Text = "Trade";
            gameButton2.Text = "Manage mortgages";
            gameButton3.Text = "End turn";
            gameButton1.Show();
            gameButton2.Show();
            gameButton3.Show();
        }

        /**
         * Diplays the list of properties available for purchase from other players.
         */
        public void ShowTradeOptions(List<ListViewItem> items, Player player)
        {
            tradeViewer.Items.Clear();
            HideElements();
            tradeViewer.Show();
            foreach (ListViewItem li in items)
            {
                tradeViewer.Items.Add(li);
            }
            tradeViewer.Select();
            moneyTrackBar.Minimum = 0;
            moneyTrackBar.Value = moneyTrackBar.Minimum;
            moneyTrackBar.Maximum = (int) Math.Floor(player.Money);
            trackBarShower.Text = moneyTrackBar.Value.ToString();
            textBox.Text = "Choose property and money offer";
            gameButton1.Text = "Send offer";
            gameButton2.Text = "Back";
            tradeViewer.Show();
            moneyTrackBar.Show();
            trackBarShower.Show();
            gameButton1.Show();
            gameButton2.Show();
        }

        /**
         * Moves a player by one field on graphical window.
         */
        public void MovePlayer(int player, Point position)
        {
            this.player[player].Location = position;
            this.Update();
        }

        /**
         * Public method - returns the selected ONE item from the tradeViewer. 
         */
        public ListViewItem GetSelectedItem()
        {
            if (tradeViewer.SelectedItems.Count > 0)
            {
                return tradeViewer.SelectedItems[0];
            }
            return null;
        }

        /**
         * Public method - returns the selected itemS from the tradeViewer.
         */
        public ListView.SelectedListViewItemCollection GetSelectedItems()
        {
            if (tradeViewer.SelectedItems.Count > 0)
            {
                return tradeViewer.SelectedItems;
            }
            return null;
        }

        /**
         * Public method - returns the value of the moneyTrackBar.
         */
        public float GetOfferedMoney()
        {
            return moneyTrackBar.Value;
        }

        /**
         * Displays the offer that was made to the player that owns the property
         * to either accept or reject offer.
         */
        public void ShowTradeOffer(string name, float offeredMoney, Player owner)
        {
            HideElements();
            ShowPlayerInfo(owner);
            textBox.Text = "Do you want to sell " + name + " for " + offeredMoney + "m?";
            gameButton1.Text = "Yes";
            gameButton2.Text = "No";
            gameButton1.Show();
            gameButton2.Show();
        }

        /**
         * Shows mortgage menu where player can choose to either pay off a mortgage,
         * or take a mortgage.
         */
        public void ShowMortgageMenu()
        {
            HideElements();
            textBox.Text = "What do you want to do?";
            gameButton1.Text = "Take Mortgage";
            gameButton2.Text = "Pay off a mortgage";
            gameButton3.Text = "Back";
            gameButton1.Show();
            gameButton2.Show();
            gameButton3.Show();
        }

        /**
         * Displays a list of mortgaged properties in the tradeViewer.
         */
        public void ShowMortgagedProperties(List<ListViewItem> items, bool multiselect)
        {
            tradeViewer.Items.Clear();
            HideElements();
            tradeViewer.Show();
            foreach (ListViewItem li in items)
            {
                tradeViewer.Items.Add(li);
            }
            tradeViewer.Select();
            tradeViewer.MultiSelect = multiselect;
            tradeViewer.Show();
            if (multiselect)
            {
                textBox.Text = "Which property do you want to mortgage?";
                gameButton1.Text = "Mortgage selected";
            }
            else
            {
                textBox.Text = "Which mortgages do you want to pay off?";
                gameButton1.Text = "Pay off mortgage";
            }
            gameButton2.Text = "Back";
            gameButton1.Show();
            gameButton2.Show();
        }

        /**
         * Displays properties owned by the player.
         */
        public void ShowPlayerProperties(List<ListViewItem> items)
        {
            tradeViewer.Items.Clear();
            foreach (ListViewItem lvi in items)
            {
                tradeViewer.Items.Add(lvi);
            }
            tradeViewer.Show();
        }

        /**
         * Draws an apartment (a square) onto the given coords. 
         */
        public void DrawApartment(Point coords)
        {
            PictureBox picture = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(10, 10),
                Location = coords,
                BackColor = Color.White
            };
            this.Controls.Add(picture);
            picture.Show();
            picture.BringToFront();
            Update();
        }

        /**
         * Listener - updates whenever there is a moneyTrackBar change, which then
         * updates the trackBarShower label that displays the moneyTrackBar value.
         */
        private void MoneyTrackBarChanged(object sender, EventArgs e)
        {
            trackBarShower.Text = moneyTrackBar.Value.ToString();
        }

        /**
         * Public method - allows other classes to modify the textBox content.
         * This method hides every other element.
         */
        public void SetTextBox(string text)
        {
            HideElements();
            textBox.Text = text;
            textBox.Update();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
