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
        public Reservering Reservering { get; set; }
        Dictionary<int,decimal> PrijsPerUur = new Dictionary<int, decimal>();

        protected PrijsBerekening() { }

        //limo, arrangement, klant, eventingkorting en aantalreservaties kunnen we uit reservering halen
        //opmerking: is reservering wel al geïnstantieerd op dit punt?
        public PrijsBerekening(Reservering res)
        {
            Reservering = res;
        }

        public decimal BerekenKostPrijs()
        {
            decimal returnWaarde =0;

            //wat als er extra minuten zijn?
            // 23 => 0 oplossing
            //how to iterate over time
            for (int i = 0; i < Reservering.Periode.Duur.Hours; i++)
            {
                DateTime huidigeTijd = Reservering.Periode.Begin.AddHours(i);

                if (IsEersteUur(huidigeTijd))
                {
                    PrijsPerUur.Add(i+1,Reservering.Limo.EersteUurPrijs);
                }

                else if (IsNachtUur(huidigeTijd))
                {
                    PrijsPerUur.Add(i+1,NachtUurPrijs(Reservering.Limo.EersteUurPrijs));
                }

                else
                {
                    PrijsPerUur.Add(i+1,TweedeUurPrijs(Reservering.Limo.EersteUurPrijs));
                }
                
            }


            //dit eigen method geven?
            foreach (KeyValuePair<int,decimal> prijsVoorUur in PrijsPerUur)
            {
                returnWaarde += prijsVoorUur.Value;
            }

            //toepassen eventingKorting
            double eventingKortingVoorAantal = Reservering.Klant.KlantCategorie.EventingKorting.KortingVoorAantal(Reservering.Klant.ReserveringenInJaar(DateTime.Now.Year));
            returnWaarde = BerekenPrijsNaEventingKorting(returnWaarde, eventingKortingVoorAantal);

            //berekenenPrijsInclBtw
            returnWaarde = BerekenPrijsInclBtw(returnWaarde);


            //return
            return returnWaarde;
        }

        public decimal BerekenPrijsInclBtw(decimal prijsExclBtw)
        {
            return (prijsExclBtw / 100) * (100 + BtwPercentage);
        }

        public bool IsEersteUur(DateTime tijdStip)
        {
            if (tijdStip <= Reservering.Periode.Begin.AddHours(1))
            {
                return true;
            }
            else return false;
        }

        public bool IsNachtUur(DateTime tijdStip)
        {
            bool returnWaarde = tijdStip.Hour >= 22 || tijdStip.Hour >= 0 && tijdStip.Hour <= 6;
            return returnWaarde;
        }

        public bool IsOverUur(DateTime tijdStip)
        {
            bool returnWaarde = (tijdStip > Reservering.Periode.Begin.AddHours(7));
            return returnWaarde;
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

        public decimal BerekenPrijsNaEventingKorting(decimal voorKorting, double kortingsPercentage)
        {
            decimal returnWaarde = (voorKorting / 100) * (100 - (decimal)kortingsPercentage);
            return returnWaarde;
        }

        public decimal Afronder(decimal prijs)
        {
            var returnWaarde = Math.Round(prijs / (decimal)5.0) * 5;
            return returnWaarde;
        }
    }
}
