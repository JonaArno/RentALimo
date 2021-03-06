﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RentALimo.Business
{

    //in frontend new ReserveringBouwer
    //laag tussen EF en businesslaag

    public class ReserveringBouwer
    {
        private IReserveringRepo _repo;

        //public DateTime? Datum { get; set; }
        public Klant Klant { get; set; }
        public Periode Periode { get; set; }
        public Arrangement Arrangement { get; set; }
        public Locatie StartLocatie { get; set; }
        public Locatie EindLocatie { get; set; }
        public Limo Limo { get; set; }


        public ReserveringBouwer(IReserveringRepo repo)
        {
            _repo = repo;
        }

        public void Maak()
        {
            if (!IsGeldig())
            {
                throw new InvalidOperationException(Status());
            }

            var result = new Reservering(Arrangement, Periode, StartLocatie, EindLocatie,
                Limo, Klant, new PrijsBerekening(Limo, Arrangement, Klant.KlantCategorie.EventingKorting, _repo.AantalReserveringenVoorKlantInJaar(Klant, DateTime.Now.Year), Periode.Begin, Periode.Einde).PrijsInfo);
            _repo.Nieuw(result);
        }

        private string Status()
        {
            //extra nodig?
            var sb = new StringBuilder();
            if (Klant == null) sb.AppendLine("Geen klant voor reservering");
            if (Periode == null) sb.AppendLine("Geen geldige data voor reservering");
            else if (Periode != null)
            {
                if (Periode.Duur > Periode.MaximaleDuur)
                    sb.AppendLine($"De gekozen periode overschrijdt de maximale duur voor een reservering ({Periode.MaximaleDuur})");
                else if (Periode.Duur.Hours < 0)
                    sb.AppendLine("De einddatum moet later dan de begindatum vallen");
            }
            if (!IsGeldigStartUur(Arrangement, Periode.Begin))
                sb.AppendLine("Het gekozen startuur is niet mogelijk binnen het geselecteerde arrangement");
            if (Limo == null) sb.AppendLine("Geen limo voor reservering");
            else if (Limo != null)
            {
                if (!Limo.MogelijkBinnenArrangement(Arrangement))
                    sb.AppendLine("De gekozen limo kan niet binnen het gekozen arrangement gebruikt worden");
            }

            if (!LimoIsVrij()) sb.AppendLine("De gekozen limo is niet beschikbaar op de gekozen data");
            return sb.ToString();
        }

        private bool IsGeldig()
        {
            return Klant != null &&
                   Periode != null &&
                   Limo != null &&
                   Limo.MogelijkBinnenArrangement(Arrangement) &&
                   Periode.Duur <= Periode.MaximaleDuur &&
                   Periode.Duur.Hours > 0 &&
                   IsGeldigStartUur(Arrangement, Periode.Begin) &&
                   LimoIsVrij();
        }

        public bool IsGeldigStartUur(Arrangement arrangement, DateTime periodeBegin)
        {
            bool returnWaarde = true;

            if (arrangement == Arrangement.Wedding)
            {
                if (periodeBegin.Hour < 7 || periodeBegin.Hour > 15)
                {
                    returnWaarde = false;
                }
            }

            else if (arrangement == Arrangement.NightLife)
            {
                if (periodeBegin.Hour > 0 && periodeBegin.Hour < 20)
                {
                    returnWaarde = false;
                }
            }
            return returnWaarde;
        }

        //van aanpassen tijd kunnen twee methods gemaakt worden (refactoring)
        public bool LimoIsVrij()
        {
            var returnWaarde = false;

            var vorige = _repo.LaatsteReserveringVoorLimo(Limo, Periode.Begin);
            var volgende = _repo.VolgendeReserveringVoorLimo(Limo, Periode.Begin);

            DateTime gecorrigeerdHuidigeReservatieStart = new DateTime();
            DateTime gecorrigeerdHuidigeReservatieEinde = new DateTime();

            if (vorige != null && volgende != null)
            {
                //aanpassen huidige startTijd
                if (vorige.EindLocatie == this.StartLocatie)
                {
                    gecorrigeerdHuidigeReservatieStart = this.Periode.Begin.AddHours(-4);
                }
                else if (vorige.EindLocatie != this.StartLocatie)
                {
                    gecorrigeerdHuidigeReservatieStart = this.Periode.Begin.AddHours(-6);
                }

                //aanpassen huidige eindTijd
                if (this.EindLocatie == volgende.StartLocatie)
                {
                    gecorrigeerdHuidigeReservatieEinde = this.Periode.Einde.AddHours(4);
                }
                else if (this.EindLocatie != volgende.StartLocatie)
                {
                    gecorrigeerdHuidigeReservatieEinde = this.Periode.Einde.AddHours(6);
                }
            }

            else if (vorige == null && volgende != null)
            {
                //aanpassen huidige eindTijd
                if (this.EindLocatie == volgende.StartLocatie)
                {
                    gecorrigeerdHuidigeReservatieStart = this.Periode.Begin;
                    gecorrigeerdHuidigeReservatieEinde = this.Periode.Einde.AddHours(4);
                }
                else if (this.EindLocatie != volgende.StartLocatie)
                {
                    gecorrigeerdHuidigeReservatieStart = this.Periode.Begin;
                    gecorrigeerdHuidigeReservatieEinde = this.Periode.Einde.AddHours(6);
                }
            }

            else if (vorige != null && volgende == null)
            {
                //aanpassen huidige startTijd
                if (vorige.EindLocatie == this.StartLocatie)
                {
                    gecorrigeerdHuidigeReservatieStart = this.Periode.Begin.AddHours(-4);
                    gecorrigeerdHuidigeReservatieEinde = this.Periode.Einde;
                }
                else if (vorige.EindLocatie != this.StartLocatie)
                {
                    gecorrigeerdHuidigeReservatieStart = this.Periode.Begin.AddHours(-6);
                    gecorrigeerdHuidigeReservatieEinde = this.Periode.Einde;
                }
            }

            else if (vorige == null && volgende == null)
            {
                gecorrigeerdHuidigeReservatieStart = this.Periode.Begin;
                gecorrigeerdHuidigeReservatieEinde = this.Periode.Einde;
            }

            int overlappendeReserveringen = _repo.ReserveringenVoorLimoInPeriode(Limo, gecorrigeerdHuidigeReservatieStart, gecorrigeerdHuidigeReservatieEinde);

            if (overlappendeReserveringen == 0)
            {
                returnWaarde = true;
            }

            return returnWaarde;
        }

    }
}
