using System.Collections.Generic;

namespace RentALimo.Business
{
    public class WagenPrijs
    {
        public int Id { get; set; }

        public WagenPrijs() { }

        public Dictionary<Arrangement, decimal> PrijsOverzicht = new Dictionary<Arrangement, decimal>();


        //dit opsplitsen met items
        public void Toevoegen(Arrangement arr, decimal prijs)
        {
            PrijsOverzicht.Add(arr, prijs);
        }
    }


}