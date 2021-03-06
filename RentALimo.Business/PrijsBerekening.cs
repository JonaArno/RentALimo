﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RentALimo.Business
{
    public class PrijsBerekening
    {
        private const decimal BtwPercentage = 6;
        public PrijsInfo PrijsInfo = new PrijsInfo();
        public Limo Limo { get; set; }
        public Arrangement Arrangement { get; set; }
        public EventingKorting EventingKorting { get; set; }
        public int AantalReserveringen { get; set; }
        public Periode Periode { get; set; }
        Dictionary<int, decimal> PrijsPerUur = new Dictionary<int, decimal>();

        protected PrijsBerekening() { }

        public PrijsBerekening(Limo limo, Arrangement arr, EventingKorting evtKorting, int aantalReserveringen,
            DateTime startTijd, DateTime eindTijd)
        {
            Limo = limo;
            Arrangement = arr;
            EventingKorting = evtKorting;
            AantalReserveringen = aantalReserveringen;
            Periode = new Periode(startTijd, eindTijd);
            BerekenKostPrijs();
        }

        public void BerekenKostPrijs()
        {
            int teller = 0;

            if (Arrangement == Arrangement.NightLife)
            {
                PrijsPerUur.Add(teller, Limo.NightLifeArrangementPrijs);

                if (!Periode.BevatOverUren(Arrangement.NightLife))
                {
                    UurOntleder(Periode.Begin, Periode.Einde);
                }
                else
                {
                    UurOntleder(Periode.Begin, Periode.Begin.AddHours(7));
                }

                if (Periode.BevatOverUren(Arrangement))
                {
                    DateTime startTijd = Periode.Begin.AddHours(7);
                    while (startTijd < Periode.Einde)
                    {
                        PrijsPerUur.Add(teller + 1, NachtUurPrijs(Limo.EersteUurPrijs));
                        PrijsInfo.AantalOverUren += 1;
                        if (IsNachtUur(startTijd))
                        {
                            PrijsInfo.AantalNachtUren += 1;
                        }
                        else
                        {
                            PrijsInfo.AantalGewoneUren += 1;
                        }
                        teller += 1;
                        startTijd = Periode.Begin.AddHours(7 + teller);
                    }
                }
            }

            else if (Arrangement == Arrangement.Wedding)
            {
                PrijsPerUur.Add(teller, Limo.WeddingArrangementPrijs);

                if (!Periode.BevatOverUren(Arrangement.Wedding))
                {
                    UurOntleder(Periode.Begin, Periode.Einde);
                }
                else
                {
                    UurOntleder(Periode.Begin, Periode.Begin.AddHours(7));
                }

                if (Periode.BevatOverUren(Arrangement))
                {
                    DateTime startTijd = Periode.Begin.AddHours(7);

                    while (startTijd < Periode.Einde)
                    {
                        if (IsNachtUur(startTijd))
                        {
                            PrijsPerUur.Add(teller + 1, NachtUurPrijs(Limo.EersteUurPrijs));
                            PrijsInfo.AantalNachtUren += 1;
                        }
                        else
                        {
                            PrijsPerUur.Add(teller + 1, TweedeUurPrijs(Limo.EersteUurPrijs));
                        }

                        PrijsInfo.AantalOverUren += 1;
                        teller += 1;
                        startTijd = Periode.Begin.AddHours(7 + teller);

                    }
                }
            }

            else if (Arrangement == Arrangement.Airport || Arrangement == Arrangement.Business)
            {
                while (teller < Periode.Duur.Hours)
                {
                    DateTime huidigeTijd = Periode.Begin.AddHours(teller);

                    if (IsEersteUur(huidigeTijd))
                    {
                        PrijsPerUur.Add(teller, Limo.EersteUurPrijs);
                        if (IsNachtUur(huidigeTijd))
                        {
                            PrijsInfo.AantalNachtUren += 1;
                        }
                        else
                        {
                            PrijsInfo.AantalGewoneUren += 1;
                        }
                    }

                    else if (IsNachtUur(huidigeTijd))
                    {
                        PrijsPerUur.Add(teller, NachtUurPrijs(Limo.EersteUurPrijs));
                        PrijsInfo.AantalNachtUren += 1;
                    }

                    else
                    {
                        PrijsPerUur.Add(teller, TweedeUurPrijs(Limo.EersteUurPrijs));
                        PrijsInfo.AantalGewoneUren += 1;
                    }
                    teller += 1;
                }
            }
            //optellen van bedragen om prijs exclusief btw VOOR eventingkorting te bekomen
            foreach (KeyValuePair<int, decimal> prijsVoorUur in PrijsPerUur)
            {
                PrijsInfo.BedragExclusiefBtwVoorEventingKorting += prijsVoorUur.Value;
            }

            //eventingkorting berekenen en toekennen aan PrijsInfowaarde
            //controle geldige eventingKorting

            if (EventingKorting.IsGeldig())
            {
                PrijsInfo.AangerekendeEventingKorting = (PrijsInfo.BedragExclusiefBtwVoorEventingKorting / 100) * (decimal)EventingKorting.KortingVoorAantal(AantalReserveringen);
            }
            else
            {
                throw new InvalidOperationException("Eventingkorting niet geldig!");

            }

            //Berekening BedragExclBtwNaEventingKorting
            PrijsInfo.BedragExclusiefBtwNaEventingKorting =
                PrijsInfo.BedragExclusiefBtwVoorEventingKorting - PrijsInfo.AangerekendeEventingKorting;

            //Berekening BtwBedrag
            PrijsInfo.BtwBedrag = (PrijsInfo.BedragExclusiefBtwNaEventingKorting / 100) * BtwPercentage;

            //Berekening totaalbedrag
            PrijsInfo.TotaalTeBetalenBedrag = PrijsInfo.BedragExclusiefBtwNaEventingKorting + PrijsInfo.BtwBedrag;
        }

        private void UurOntleder(DateTime periodeBegin, DateTime eindTijd)
        {
            while (periodeBegin < eindTijd)
            {
                if (IsNachtUur(periodeBegin))
                    PrijsInfo.AantalNachtUren += 1;
                else
                    PrijsInfo.AantalGewoneUren += 1;

                periodeBegin = periodeBegin.AddHours(1);
            }
        }


        public decimal BerekenPrijsInclBtw(decimal prijsExclBtw)
        {
            return (prijsExclBtw / 100) * (100 + BtwPercentage);
        }


        public bool IsEersteUur(DateTime tijdStip)
        {
            if (tijdStip.Hour == Periode.Begin.Hour)
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
            bool returnWaarde = (tijdStip > Periode.Begin.AddHours(7));
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
