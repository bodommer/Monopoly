using Monopoly.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Main
{
    public class RiskCard : IField
    {
        public string Description { get; private set; }
        public string MoneyPlusMinus { get; private set; }
        public float MoneyChange { get; private set; }
        public int TurnsStop { get; private set; }

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
