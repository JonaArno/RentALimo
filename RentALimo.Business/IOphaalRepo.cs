using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentALimo.Business
{
    public interface IOphaalRepo
    {
        IEnumerable<Klant> OphalenKlanten();
        IEnumerable<Limo> OphalenAlleLimos();
        IEnumerable<Limo> OphalenBeschikbareLimosInPeriode(DateTime begin, DateTime einde);
    }
}
