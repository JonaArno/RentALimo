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
