using Monopoly.Cards;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Main
{
    /**
     * A type of Card class. It represents a set of properties that can be upgraded (ApartmentCost attribute).
     * All properties of groups 1-8 are of this type.
     */
    [Serializable()]
    public class PropertyCard : Card
    {
        // number of apartments built on the field
        public int Apartments { get; private set; }
        // the array of payments for each of purchased apartments - 0 to 3
        private float[] payments;
        // the cost of upgrade (does not increase with more apartments)
        public float ApartmentCost { get; private set; }

        /**
         * The constructor.
         */
        public PropertyCard(string data, Image img)
        {
            string[] details = data.Split(';');
            // fixing interpreting & as an ecape character
            if (details[0] == "ATT")
            {
                Name = "AT&T";
            }
            else if (details[0] == "CA")
            {
                Name = "CA Technologies";
            }
            {
                Name = details[0];
            }
            Cost = float.Parse(details[2]);
            Apartments = 0;
            payments = new float[4];
            payments[0] = float.Parse(details[3], CultureInfo.InvariantCulture);
            payments[1] = float.Parse(details[4], CultureInfo.InvariantCulture);
            payments[2] = float.Parse(details[5], CultureInfo.InvariantCulture);
            payments[3] = float.Parse(details[6], CultureInfo.InvariantCulture);
            ApartmentCost = float.Parse(details[7], CultureInfo.InvariantCulture);
            MortgageValue = float.Parse(details[8], CultureInfo.InvariantCulture);
            Group = int.Parse(details[9]);

            logo = img;
        }

        public void AddApartment()
        {
            Apartments++;
        }

        /**
         * Returns current payment amount.
         */
        public override float GetPayment()
        {
            return payments[Apartments];
        }

        /**
         * Returns the payment amount for a specified apartment count.
         */
        public float GetPayment(int apt)
        {
            return payments[apt];
        }

    }
}
