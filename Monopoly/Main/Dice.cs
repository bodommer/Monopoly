using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly.Main
{
    public class Dice
    {
        private static Dice dice;
        private Random randomizer;
        private const int DICE_SLEEP = 150;

        private Dice()
        {
            randomizer = new Random();
        }

        public static Dice Instance
        {
            get
            {
                if (dice == null)
                {
                    dice = new Dice();
                }
                return dice;
            }
        }

        public int Roll(Monopoly window)
        {
            //draw the dice and randomising
            int rolls = randomizer.Next(5, 10);
            int result = 0;
            for(int i=0; i<rolls;i++)
            {
                result = randomizer.Next(1, 7);
                window.ShowDiceNumber(result);
                Thread.Sleep(DICE_SLEEP);
            }
            Thread.Sleep(1500);
            if (result == 6)
            {
                result += Roll(window);
            }
            return result;
            //return 8; //for testing!
        }
    }
}
