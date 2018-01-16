using System;
using System.Collections.Generic;

namespace RentALimo.Business
{
    public class Limo
    {
        public int WagenId { get; set; }
        public string Merk { get; set; }

        public string Type { get; set; }

        public decimal EersteUurPrijs { get; set; }
        public decimal NightLifeArrangementPrijs { get; set; }
        public decimal WeddingArrangementPrijs { get; set; }


        protected Limo() {}


        public Limo(string merk, string type, decimal eersteUurPrijs, decimal nightLifeArrangementPrijs,
            decimal weddingArrangementPrijs)
        {
            Merk = merk;
            Type = type;
            EersteUurPrijs = eersteUurPrijs;
            NightLifeArrangementPrijs = nightLifeArrangementPrijs;
            WeddingArrangementPrijs = weddingArrangementPrijs;

        }


        public bool MogelijkBinnenArrangement(Arrangement arr)
        {
            bool isMogelijk = false;

            if (arr == Arrangement.Airport || arr == Arrangement.Business)
            {
                isMogelijk = EersteUurPrijs != 0;
            }

            else if (arr == Arrangement.Wedding)
            {
                isMogelijk = WeddingArrangementPrijs != 0;
            }

            else if (arr == Arrangement.NightLife)
            {
                isMogelijk = NightLifeArrangementPrijs != 0;
            }
            return isMogelijk;
        }


        public override string ToString()
        {
            return $"{Merk} {Type}";
        }
    }
}
