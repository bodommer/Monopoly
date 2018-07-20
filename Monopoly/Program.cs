using Monopoly.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Main.Monopoly window = new Main.Monopoly();
            //Console.ReadLine();
            //Game g = new Game();

            Application.Run(new Main.MainMenu());
            //ImageManipulator.CreateThumbnails();
        }
    }
}
