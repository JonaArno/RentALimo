using System.Collections.Generic;

namespace RentALimo.Business
{
    public class WagenPrijs
    {
        public int Id { get; set; }
        public decimal EersteUurPrijs { get; set; }
        public decimal WeddingArrangementPrijs { get; set; }
        public decimal NightLifeArrangementPrijs { get; set; }

        protected WagenPrijs() { }

        public WagenPrijs(decimal eersteUurPrijs, decimal weddingArrangementPrijs, decimal nightLifeArrangementPrijs)
        {
            EersteUurPrijs = eersteUurPrijs;
            WeddingArrangementPrijs = weddingArrangementPrijs;
            NightLifeArrangementPrijs = nightLifeArrangementPrijs;
        }
        

        //public Dictionary<Arrangement, decimal> ArrangementPrijzen = new Dictionary<Arrangement, decimal>();

        //public void Toevoegen(Arrangement arr, decimal prijs)
        //{
        //    ArrangementPrijzen.Add(arr, prijs);
        //}
    }


}