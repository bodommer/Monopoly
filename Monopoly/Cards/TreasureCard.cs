using Monopoly.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Main
{
    [Serializable]
    public class TreasureCard : IField
    {
        public string Description { get; private set; }
        public float MoneyChange { get; private set; }

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
