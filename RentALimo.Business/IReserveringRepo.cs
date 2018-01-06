using System;
using System.Collections.Generic;

namespace RentALimo.Business
{
    public interface IReserveringRepo
    {
        //IEnumerable<Limo> ReserveringenVoorLimoInPeriode(Limo limo, DateTime begin, DateTime einde);
        IEnumerable<Klant> OphalenKlanten(); 
        IEnumerable<Reservering> ReserveringenVoorLimoInPeriode(Limo limo, DateTime begin, DateTime einde);
        int AantalReserveringenVoorKlantInJaar(Klant klant, int jaar);
        void Nieuw(Reservering reservering);
    }
}