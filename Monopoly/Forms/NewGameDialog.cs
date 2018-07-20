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

namespace Monopoly.Forms
{
    public partial class NewGameDialog : Form
    {
        public int playersCount;
        public float millions;

        public NewGameDialog()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            playersCount = Decimal.ToInt32(playerCount.Value);
            millions = moneyTrackBar.Value;
        }

        private void trackBarChanged(object sender, EventArgs e)
        {
            startingMoneyLabel.Text = moneyTrackBar.Value.ToString() + " m";
            Update();
        }
    }
}
