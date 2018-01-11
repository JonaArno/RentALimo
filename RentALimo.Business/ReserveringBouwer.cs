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

            //DEZE CONSTRUCTOR MOET ZEKER HERBEKEKEN WORDEN
            var result = new Reservering(Arrangement, Periode, StartLocatie, EindLocatie,
                Limo, Klant,new PrijsBerekening(Limo,Arrangement,Klant.KlantCategorie.EventingKorting,_repo.AantalReserveringenVoorKlantInJaar(Klant,DateTime.Now.Year),Periode.Begin,Periode.Einde).PrijsInfo);
            //{
            //    Arrangement = Arrangement.Value,
            //    Periode = Periode,
            //    Limo = Limo

            //    // enz ...
            //    // PrijsInfo = PrijsBerekenaarFabriek.Berekenaar(Arrangement).PrijsInfo()
            //};
            _repo.Nieuw(result);
            //return result;
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


        //DIT MOET NOG EENS GOED UITGETEKEND WORDEN
        public bool LimoIsVrij()
        {
            var laatste = _repo.LaatsteReserveringVoorLimo(Limo, Periode.Begin);
            var volgende = _repo.VolgendeReserveringVoorLimo(Limo, Periode.Einde);

            DateTime gecorrigeerdHuidigeReservatieStart = new DateTime();
            DateTime gecorrigeerdHuidigeReservatieEinde = new DateTime();         
            
            //aanpassen huidige startTijd
            if (laatste.EindLocatie == this.StartLocatie)
            {
                gecorrigeerdHuidigeReservatieStart = this.Periode.Begin.Addhours(-4);
            }
            else if (laatste.EindLocatie != this.StartLocatie)
            {
                gecorrigeerdHuidigeReservatieStart = this.Periode.Begin.Addhours(-6);
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
            
            int overlappendeReserveringen = _repo.ReserveringenVoorLimoInPeriode(Limo, gecorrigeerdHuidigeReservatieStart, gecorrigeerdHuidigeReservatieEinde);
            
            
            return overlappendeReserveringen == 0
            
            
            
            //oude foute implementatie
            //bool overlapMetLaatste =
                //this.Periode.Begin < gecorrigeerdLaatsteReservatieEinde && laatste.Periode.Begin < this.Periode.Einde;

            //bool overlapMetVolgende =
                //this.Periode.Begin < volgende.Periode.Einde && volgende.Periode.Begin < gecorrigeerdHuidigeReservatieEinde;

            //if (!overlapMetLaatste && !overlapMetVolgende)
                //return true;
            //else return false;





            // dit is slechts 1 mogelijke (uit vele) en 
            // een onvolledige implementatie
            //var buffer = 6; // komt waarschijnlijk uit Locatie
            //var reserveringen = _repo.ReserveringenVoorLimoInPeriode(Limo, Periode.Begin.AddHours(buffer), Periode.Einde.AddHours(buffer));
            //return true;
        }

    }
}
