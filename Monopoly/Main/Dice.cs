using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monopoly.Main
{
    /**
     * This is the class that generates the dice rolls.
     * It is a singleton and can be obtained by Dice.Instance call.
     */
    [Serializable()]
    public class Dice
    {
        private static Dice dice;
        private Random randomizer;
        private const int DICE_SLEEP = 150;
        private const int ROLL_OVER_WAIT = 1000;

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

        /**
         * The private constructor.
         */
        private Dice()
        {
            randomizer = new Random();
        }

        /**
         * The only function of the dice is to simulate dice roll and redraw the "rolled"
         * number in the graphic interface.
         */
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
            Thread.Sleep(ROLL_OVER_WAIT);
            // roll again
            if (result == 6)
            {
                result += Roll(window);
            }
            return result;
            //return 7; //for testing only!
        }
    }
}
