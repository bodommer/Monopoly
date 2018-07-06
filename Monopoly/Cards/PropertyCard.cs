using Monopoly.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Main
{
    public class PropertyCard : IField, IPurchasable
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float Cost { get; private set; }
        public int Apartments { get; private set; }
        private float[] payments;
        public float ApartmentCost { get; private set; }
        public float MortgageValue { get; private set; }
        public int Group { get; private set; }
        // public Image image { get; private set; }

        public PropertyCard(string data)
        {
            string[] details = data.Split(';');
            Name = details[0];
            Description = details[1];
            Cost = float.Parse(details[2]);
            Apartments = 0;
            payments = new float[4];
            payments[0] = float.Parse(details[3]);
            payments[1] = float.Parse(details[4]);
            payments[2] = float.Parse(details[5]);
            payments[3] = float.Parse(details[6]);
            ApartmentCost = float.Parse(details[7]);
            MortgageValue = float.Parse(details[8]);
            Group = int.Parse(details[9]);
        }

        public void AddApartment()
        {
            Apartments++;
        }

        public float GetPayment()
        {
            return payments[Apartments];
        }
    }
}
