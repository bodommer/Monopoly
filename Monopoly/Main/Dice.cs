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
        private int lastThrow;
        private const int diceSleep = 300;

        private Dice()
        {
            randomizer = new Random();
            lastThrow = 4;
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

            lastThrow = randomizer.Next(1, 7);
            Thread.Sleep(diceSleep);
            return lastThrow;
        }

    }
}
