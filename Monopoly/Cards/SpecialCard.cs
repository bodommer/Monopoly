using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    /**
     * An ancestor card of Treasure Card and Risk Card.
     */
    [Serializable()]
    public abstract class SpecialCard
    {
        public string Description { get; protected set; }
        public float MoneyChange { get; protected set; }
    }
}
