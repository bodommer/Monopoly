using Monopoly.Forms;
using Monopoly.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly.Main
{
    public partial class MainMenu : Form
    {
        Player[] players; 

        public MainMenu()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            using (NewGameDialog dlg = new NewGameDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    int playersCount = dlg.playersCount;
                    float money = dlg.millions;

                    using(PlayersChoosing dlg2 = new PlayersChoosing(playersCount, money))
                    {
                        if (dlg2.ShowDialog() == DialogResult.OK)
                        {
                            players = dlg2.players;
                            Hide();
                            using (Monopoly gw = new Monopoly(players)) 
                            {
                                Game g = new Game(players, gw);
                                if (gw.ShowDialog() == DialogResult.OK) Show();
                            };
                        }
                    }
                }
            }
            this.Enabled = true;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void loadGameButton_Click(object sender, EventArgs e)
        {
            if (File.Exists("Serialized.bin"))
            {
                Console.WriteLine("Reading saved file");
                Stream openFileStream = File.OpenRead("Serialized.bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                Game g = (Game)deserializer.Deserialize(openFileStream);
                openFileStream.Close();
                Hide();
                using (Monopoly gw = new Monopoly(g.GetPlayers()))
                {
                    gw.Show();
                    g.SetWindow(gw);
                    g.UpdateWindow();
                    gw.Hide();
                    if (gw.ShowDialog() == DialogResult.OK) Show();
                };
            }
        }
    }
}
