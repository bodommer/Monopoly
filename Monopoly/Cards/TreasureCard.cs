using Monopoly.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Main
{
    /**
     * The data container for a Treasure Card.
     */
    [Serializable]
    public class TreasureCard : SpecialCard
    {


        /**
         * The constructor.
         */
        public TreasureCard(string data)
        {
            try
            {
                string[] content = data.Split(';');
                Description = content[0];
                MoneyChange = float.Parse(content[1]);
            } catch (FormatException e)
            {
                Console.WriteLine(e.StackTrace);
                throw new FormatException();
            }
        }

    }
}
