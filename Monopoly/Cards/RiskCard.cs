using Monopoly.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Main
{
    /**
     * The data container for one RiskCard.
     */
    [Serializable()]
    public class RiskCard : SpecialCard
    {
        public string MoneyPlusMinus { get; private set; }
        public int TurnsStop { get; private set; }

        /**
         * The constructor.
         */
        public RiskCard(string data)
        {
            try
            {
                string[] content = data.Split(';');
                Description = content[0];
                MoneyPlusMinus = content[1];
                MoneyChange = float.Parse(content[2]);
                TurnsStop = int.Parse(content[3]);
            } catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new FormatException();
            }
        }
    }
}
