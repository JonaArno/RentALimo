using System;
using System.Text;

namespace RentALimo.Business
{

    //in frontend new ReserveringBouwer
    //laag tussen EF en businesslaag

    public class ReserveringBouwer
    {
        private IReserveringRepo _repo;

        public DateTime? Datum { get; set; }
        public Klant Klant { get; set; }
        public Periode Periode { get; set; }
        public Arrangement? Arrangement { get; set; }
        public Locatie? StartLocatie { get; set; }
        public Locatie? EindLocatie { get; set; }
        public Limo Limo { get; set; }


        //laag boven dbcontext
        public ReserveringBouwer(IReserveringRepo repo)
        {
            _repo = repo;
        }

        public Reservering Maak()
        {
            if (!IsGeldig())
            {
                throw new InvalidOperationException(Status());
            }

            var result = new Reservering
            {
                Arrangement = Arrangement.Value,
                Periode = Periode,
                Limo = Limo

                // enz ...
                // PrijsInfo = PrijsBerekenaarFabriek.Berekenaar(Arrangement).PrijsInfo()
            };
            return result;
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
                   Datum.HasValue &&
                   Arrangement.HasValue &&
                   StartLocatie.HasValue &&
                   EindLocatie.HasValue &&
                   Limo.MogelijkBinnenArrangement(Arrangement.Value) &&
                   Periode.Duur <= Periode.MaximaleDuur &&
                   // Arrangement.Value.IsGeldigStartUur(Periode.StartUur) &&
                   LimoIsVrij();

        }

        private bool LimoIsVrij()
        {
            // dit is slechts 1 mogelijke (uit vele) en 
            // een onvolledige implementatie
            var buffer = 6; // komt waarschijnlijk uit Locatie
            var reserveringen = _repo.ReserveringenVoorLimoInPeriode(Limo, Periode.Begin.AddHours(buffer), Periode.Einde.AddHours(buffer));
            return true;
        }
    }
}
