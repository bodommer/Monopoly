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
    [Serializable]
    public partial class Monopoly : Form
    {
        Graphics gamePlan;
        Graphics playerPart;
        PictureBox[] player;
        int numberOfPlayers;
        public int button;

        public Monopoly(Player[] players)
        {
            numberOfPlayers = players.Length;

            InitializeComponent();
            this.Height = 800;
            this.Width = 1200;
            gamePlan = drawArea.CreateGraphics();
            playerPart = playerDetail.CreateGraphics();
            DrawPlan();
            playerDetail.Image = Resource1.playerHub;
            drawArea.Image = Resource1.image_drawn;
            //playerDetail.Image = Image.FromFile("Resources/images/playerHub.png", true);
            //drawArea.Image = Image.FromFile("Resources/images/image-drawn.png", true);
            gamePlan.DrawEllipse(new Pen(Brushes.Black, 3), 20, 20, 20, 20);
            //Game g = new Game(players, this);
        }

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
            PrepareForDice(Color.Green);
            Console.WriteLine("start");
        }

        private void PaintWindowMethod(object sender, PaintEventArgs e)
        {
        }

        private void DrawPlan()
        {
            drawArea.CreateGraphics();
        }

        public void gameButton1_click(object sender, EventArgs e)
        {
        }

        private void gameButton2_Click(object sender, EventArgs e)
        {
        }

        private void gameButton3_Click(object sender, EventArgs e)
        {
        }

        private void saveGameButton_Click(object sender, EventArgs e)
        {
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
        }

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

        public void ShowDiceNumber(int i)
        {
            diceLabel.Text = i.ToString();
            this.Update();
        }

        public void DisplayPropertyCard(PropertyCard card, Color color, int field)
        {
            CardTitle.Text = card.Name;
            CardTitle.BackColor = color;
            CardTitle.ForeColor = Color.WhiteSmoke;
            string resName = field.ToString() + ".png";
            Image img = card.logo;
            //Image img = Image.FromFile("Resources/images/" + field.ToString());
            logoBox.Padding = new Padding((298 - img.Width) / 2, (140 - img.Height) / 2, 0, 0);
            logoBox.Image = img;
            priceLabel.Text = "Price: " + card.Cost + "m";
            paymentLabel1.Text = "Payment 1: " + card.GetPayment(0) + "m";
            paymentLabel2.Text = "Payment 2: " + card.GetPayment(1) + "m";
            paymentLabel3.Text = "Payment 3: " + card.GetPayment(2) + "m";
            paymentLabel4.Text = "Payment 4: " + card.GetPayment(3) + "m";
            apartmentCostLabel.Text = "Improvement cost: " + card.ApartmentCost + "m";
            mortgageLabel.Text = "Mortgage: " + card.MortgageValue + "m";
            ShowCard();
        }

        public void DisplayAgencyCard(AgencyCard card, int field)
        {
            CardTitle.Text = card.Name;
            CardTitle.BackColor = Color.White;
            CardTitle.ForeColor = Color.Black;
            string resName = $"_1";
            Image logo = card.logo;
            logoBox.Image = logo;
            //logoBox.Image = Resource1."_1"; ;
            logoBox.Padding = new Padding((298 - logo.Width) / 2, (140 - logo.Height) / 2, 0, 0);
            priceLabel.Text = "Price: " + card.Cost + "m";
            paymentLabel1.Text = "One agency owned payment: ";
            paymentLabel2.Text = card.GetBonus(1) + "m * dice roll value";
            paymentLabel3.Text = "Both agencies owned payment:";
            paymentLabel4.Text = card.GetBonus(2) + "m * dice roll value";
            apartmentCostLabel.Text = "";
            mortgageLabel.Text = "Mortgage: " + card.MortgageValue + "m";
            ShowCard();
        }

        public void DisplayBonusCard(BonusCard card, int field)
        {
            CardTitle.Text = card.Name;
            CardTitle.BackColor = Color.White;
            CardTitle.ForeColor = Color.Black;

            string resName = $"_" + field.ToString();
            Image logo = card.logo;
            logoBox.Image = logo;
            logoBox.Padding = new Padding((298 - logo.Width) / 2, (140 - logo.Height) / 2, 0, 0);
            priceLabel.Text = "Price: " + card.Cost + "m";
            paymentLabel1.Text = "Payment per bonu cards owned:";
            paymentLabel2.Text = "One card:" + card.GetPayment(1) + "m";
            paymentLabel3.Text = "Two cards:" + card.GetPayment(2) + "m";
            paymentLabel4.Text = "Three cards:" + card.GetPayment(3) + "m";
            apartmentCostLabel.Text = "Four cards:" + card.GetPayment(4) + "m";
            mortgageLabel.Text = "Mortgage: " + card.MortgageValue + "m";
            ShowCard();
        }

        public void DisplayRiskCard(RiskCard card)
        {
            HideElements();
            detailBox.Show();
            cardNameLabel.Text = "Risk Card";
            cardNameLabel.BorderStyle = BorderStyle.FixedSingle;
            cardContentLabel.BorderStyle = BorderStyle.FixedSingle;
            cardContentLabel.Text = card.Description;
            textBox.Text = "You played a risk card!";
            gameButton1.Text = "Continue";
            gameButton1.Show();
            cardNameLabel.Show();
            cardContentLabel.Show();
        }

        public void DisplayTreasureCard(TreasureCard card)
        {
            HideElements();
            detailBox.Show();
            cardNameLabel.BorderStyle = BorderStyle.FixedSingle;
            cardContentLabel.BorderStyle = BorderStyle.FixedSingle;
            cardNameLabel.Text = "Treasure Card";
            cardContentLabel.Text = card.Description;
            textBox.Text = "You played a trasure card!";
            gameButton1.Text = "Continue";
            gameButton1.Show();
            cardNameLabel.Show();
            cardContentLabel.Show();

        }

        public void ShowUpgradeOptions(string name, float price)
        {
            HideElements();
            textBox.Text = "Do you want to upgrade " + name + " for \u20AC" + price.ToString() + "m?";
            gameButton1.Text = "Yes";
            gameButton2.Text = "No";
            gameButton1.Show();
            gameButton2.Show();
        }

        public void PurchaseButtons(string name)
        {
            HideElements();
            textBox.Text = "Do you want to buy " + name + "?";
            gameButton1.Text = "Yes";
            gameButton2.Text = "No";
            gameButton1.Show();
            gameButton2.Show();
        }

        public void ShowPayment(string name, float payment)
        {
            HideElements();
            textBox.Text = "You must pay " + payment.ToString() + "m to " + name + ".";
            gameButton1.Text = "Ok";
            gameButton1.Show();
        }

        public void ShowNoMoneyPay()
        {
            HideElements();
            textBox.Text = "You have not got enough money to pay!";
            gameButton1.Text = "Declare bankrupcy";
            gameButton2.Text = "Take out a mortgage";
            gameButton1.Show();
            gameButton2.Show();
        }

        public void ShowMessage(string msg)
        {
            HideElements();
            textBox.Text = msg;
            gameButton1.Text = "Ok";
            gameButton1.Show();
            this.Update();
        }

        public void HideButton()
        {
            gameButton1.Hide();
            gameButton2.Hide();
        }

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

        public void ShowPlayerInfo(Player p)
        {
            playerNameLabel.Text = p.name;
            playerNameLabel.BackColor = p.Color;
            playerMoneyLabel.Text = "\u20AC " + p.Money.ToString() + "m";
        }

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

        public void MovePlayer(int player, Point position)
        {
            this.player[player].Location = position;
            this.Update();
        }

        public void ShowStartPassInfo()
        {
            cardContentLabel.Text = "";
        }

        public ListViewItem GetSelectedItem()
        {
            if (tradeViewer.SelectedItems.Count > 0)
            {
                return tradeViewer.SelectedItems[0];
            }
            return null;
        }

        public ListView.SelectedListViewItemCollection GetSelectedItems()
        {
            if (tradeViewer.SelectedItems.Count > 0)
            {
                return tradeViewer.SelectedItems;
            }
            return null;
        }

        public float GetOfferedMoney()
        {
            return moneyTrackBar.Value;
        }

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

        public void ShowMortgageMenu()
        {
            HideElements();
            textBox.Text = "What do you want to do?";
            gameButton1.Text = "Take Mortgage";
            gameButton2.Text = "Pay off a mortgage";
            gameButton3.Text = "back";
            gameButton1.Show();
            gameButton2.Show();
            gameButton3.Show();
        }

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
                gameButton1.Text = "Mortgage selected properties";
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

        public void ShowPlayerProperties(List<ListViewItem> items)
        {
            tradeViewer.Items.Clear();
            foreach (ListViewItem lvi in items)
            {
                tradeViewer.Items.Add(lvi);
            }
            tradeViewer.Show();
        }

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

        private void logoBox_Click(object sender, EventArgs e)
        {

        }

        private void drawArea_Click(object sender, EventArgs e)
        {

        }

        private void cardNameLabel_Click(object sender, EventArgs e)
        {
        }

        private void MoneyTrackBarChanged(object sender, EventArgs e)
        {
            trackBarShower.Text = moneyTrackBar.Value.ToString();
        }

        public void WindowKeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox_Click(object sender, EventArgs e)
        {

        }
    }
}
