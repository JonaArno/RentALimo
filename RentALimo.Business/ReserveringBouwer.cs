using System;
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


        //laag boven dbcontext
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
                Limo, Klant,new PrijsBerekening(Limo,Arrangement,Klant.KlantCategorie.EventingKorting,_repo.AantalReserveringenVoorKlantInJaar(Klant,DateTime.Now.Year),Periode.Begin,Periode.Einde).PrijsInfo);         
            _repo.Nieuw(result);
        }

        private string Status()
        {
            var sb = new StringBuilder();
            if (Klant == null) sb.AppendLine("Geen klant voor reservering");
            // if (Limo == null) ...
            // alle 
            return sb.ToString();
        }

        private bool IsGeldig()
        {
            return Klant != null &&
                   Periode != null &&
                   Limo != null &&
                   //Arrangement != null &&
                   //StartLocatie.HasValue &&
                   //EindLocatie.HasValue &&
                   Limo.MogelijkBinnenArrangement(Arrangement) &&
                   Periode.Duur <= Periode.MaximaleDuur &&
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
            var vorige = _repo.LaatsteReserveringVoorLimo(Limo, Periode.Begin);
            var volgende = _repo.VolgendeReserveringVoorLimo(Limo, Periode.Einde);

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
                    gecorrigeerdHuidigeReservatieEinde = this.Periode.Einde.AddHours(4);
                }
                else if (this.EindLocatie != volgende.StartLocatie)
                {
                    gecorrigeerdHuidigeReservatieEinde = this.Periode.Einde.AddHours(6);
                }
            }

            else if (vorige != null && volgende == null)
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
            }

            else if (vorige == null && volgende == null)
            {
                gecorrigeerdHuidigeReservatieStart = this.Periode.Begin;
                gecorrigeerdHuidigeReservatieEinde = this.Periode.Einde;
            }
            
            int overlappendeReserveringen = _repo.ReserveringenVoorLimoInPeriode(Limo, gecorrigeerdHuidigeReservatieStart, gecorrigeerdHuidigeReservatieEinde);
            return overlappendeReserveringen == 0;
        }
        
    }
}
