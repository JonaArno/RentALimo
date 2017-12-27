using System.Collections.Generic;

namespace RentALimo.Business
{
    public class WagenPrijs
    {
        public int Id { get; set; }
        public decimal EersteUurPrijs { get; set; }
        public decimal WeddingArrangementPrijs { get; set; }
        public decimal NightLifeArrangementPrijs { get; set; }

        public WagenPrijs() { }
        

        //public Dictionary<Arrangement, decimal> ArrangementPrijzen = new Dictionary<Arrangement, decimal>();

        //public void Toevoegen(Arrangement arr, decimal prijs)
        //{
        //    ArrangementPrijzen.Add(arr, prijs);
        //}
    }


}