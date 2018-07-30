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
    public partial class PlayersChoosing : Form
    {
        public Player[] players;
        float money;
        int count;

        TextBox[] textboxes;
        CheckBox[] checkboxes;

        public PlayersChoosing(int count, float money)
        {
            players = new Player[count];
            this.money = money;
            this.count = count;

            InitializeComponent();

            textboxes = new TextBox[] { name1, name2, name3, name4, name5, name6 };
            checkboxes = new CheckBox[] { human1, human2, human3, human4, human5, human6 };

            for (int i=5; i >= count; i--)
            {
                textboxes[i].Enabled = false;
                checkboxes[i].Enabled = false;
            }
            button1.Enabled = false;
        }


        private void PlayersChoosing_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Color[] colors = { Color.Green, Color.Blue, Color.Yellow, Color.Tomato, Color.Purple, Color.Peru };

            for (int i=0; i < count; i++)
            {
                if (checkboxes[i].Checked)
                {
                    players[i] = new HumanPlayer(textboxes[i].Text, money, colors[i]);
                }
                else
                {
                    players[i] = new AIPlayer(textboxes[i].Text, money, colors[i]);
                }
            }
        }

        private void updateButton(object sender, EventArgs e)
        {
            if (AllFieldsOK())
            {
                button1.Enabled = true;
                return;
            }
            button1.Enabled = false;
        }

        private bool AllFieldsOK()
        {
            if (name1.Text.Trim() != "" && name2.Text.Trim() != "" && FieldOK(2) && FieldOK(3) && FieldOK(4) && FieldOK(5))
            {
                return true;
            }
            return false;
        }

        private bool FieldOK(int i)
        {
            if (textboxes[i].Text.Trim() != "" || textboxes[i].Enabled == false)
            {
                return true;
            }
            return false;
        }
    }
}
