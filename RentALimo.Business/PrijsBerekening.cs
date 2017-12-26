using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentALimo.Business
{
    public class PrijsBerekening
    {
        private const decimal BtwPercentage = 6;
        public decimal AangerekendeEventingKorting { get; set; }
        public decimal BedragExclusiefBtw { get; set; }
        public decimal BtwBedrag { get; set; }
        public decimal TotaalTeBetalenBedrag { get; set; }

        protected PrijsBerekening() { }

        //limo, arrangement, klant, eventingkorting en aantalreservaties kunnen we uit reservering halen
        //opmerking: is reservering wel al geïnstantieerd op dit punt?
        public PrijsBerekening(Reservering res)
        {

        }

        public decimal NachtUurPrijs(decimal prijs)
        {
            //prijs * 120%
            var returnWaarde = prijs * (decimal)1.2;
            returnWaarde = Afronder(returnWaarde);
            return returnWaarde;
        }

        public decimal TweedeUurPrijs(decimal prijs)
        {
            //prijs * 60%
            var returnWaarde = prijs * (decimal)0.6;
            returnWaarde = Afronder(returnWaarde);
            return returnWaarde;
        }

        public decimal Afronder(decimal prijs)
        {
            var returnWaarde = Math.Round(prijs / (decimal)5.0) * 5;
            return returnWaarde;
        }
    }
}
