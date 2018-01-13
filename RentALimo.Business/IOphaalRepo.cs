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
        //IEnumerable<Klant> OphalenKlantenGrid();
        IEnumerable<Klant> OphalenKlantenMetFilter(string filter);
        IEnumerable<Limo> OphalenAlleLimos();
        Reservering OphalenLaatsteReserveringVanLimo(Limo limo, DateTime startDatum);
        IEnumerable<Limo> OphalenLimosMetFilters(DateTime startDateTime, DateTime eindDateTime, Locatie startLocatie,
            Arrangement arrangement);
        IEnumerable<Limo> OphalenBeschikbareLimosInPeriode(DateTime begin, DateTime einde);
    }
}
