using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using RentALimo.Business;

namespace RentALimo.EF
{
    public class ReserveringsRepo : IReserveringRepo
    {
        public RentALimoContext Context = new RentALimoContext();
        
        //dit moet herbekeken worden
        public IEnumerable<Reservering> ReserveringenVoorLimoInPeriode(Limo limo, DateTime begin, DateTime einde)
        {
            List<Reservering> returnWaarde = new List<Reservering>();

            foreach (var res in limo.Reserveringen)
            {
                if (res.Periode.Begin > begin && res.Periode.Einde<einde)
                    returnWaarde.Add(res);
            }

            return returnWaarde;
        }

        public int AantalReserveringenVoorKlantInJaar(Klant klant, int jaar)
        {
           return klant.ReserveringenInJaar(jaar);
        }

        //??
        public void Nieuw(Reservering res)
        {
            Context.Set<Reservering>().Add(res);
            Context.SaveChanges();
        }
    }
}
