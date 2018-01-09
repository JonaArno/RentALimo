using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using RentALimo.Business;
using System.Data.Entity;

namespace RentALimo.EF
{
    public class OphaalRepo : IOphaalRepo
    {
        public RentALimoContext Context = new RentALimoContext();

        public IEnumerable<Klant> OphalenKlanten()
        {
            return Context.Set<Klant>()
                .ToList();
        }

        public IEnumerable<Klant> OphalenKlantenGrid()
        {
            return Context.Set<Klant>()
                .Include(kl => kl.Adres)
                .Include(kl => kl.KlantCategorie)
                .ToList();
        }


        public IEnumerable<Klant> OphalenKlantenMetFilter(string filter)
        {
            return Context.Set<Klant>()
                .Where(kl => kl.Naam.Contains(filter))
                .Include(kl => kl.Adres)
                .Include(kl => kl.KlantCategorie)
                .ToList();
        }

        public IEnumerable<Limo> OphalenAlleLimos()
        {
            return Context.Set<Limo>()
                .ToList();
        }
        


        public IEnumerable<Limo> OphalenBeschikbareLimosInPeriode(DateTime begin, DateTime einde)
        {
            throw new NotImplementedException();
        }
    }
}
