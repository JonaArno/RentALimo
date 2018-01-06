using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using RentALimo.Business;
using System.Data.Entity;

namespace RentALimo.EF
{
    public class ReserveringsRepo : IReserveringRepo
    {
        public RentALimoContext Context = new RentALimoContext();
        
        //dit moet herbekeken worden
        public IEnumerable<Reservering> ReserveringenVoorLimoInPeriode(Limo limo, DateTime begin, DateTime einde)
        {
          //  bool overlap = a.start < b.end && b.start < a.end;
            return Context.Set<Reservering>()
                .Include(r => r.Limo)
                .Where(r => r.Limo.WagenId == limo.WagenId &&
                            r.Periode.Begin <= begin &&
                            r.Periode.Einde >= einde);
        }

        public int AantalReserveringenVoorKlantInJaar(Klant klant, int jaar)
        {
           //return klant.ReserveringenInJaar(jaar);
            int returnwaarde = 0;

            //moet uit reserveringen komen
            List <Klant> rw =
            //Context.Set<Klant>()
            //    .Where(kl => kl.KlantId == klant.KlantId)
            //    .ToList();



        }

        //??
        public void Nieuw(Reservering res)
        {
            Context.Set<Reservering>().Add(res);
            Context.SaveChanges();
        }
    }
}
