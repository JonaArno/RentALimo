using System;
using System.Collections.Generic;

namespace RentALimo.Business
{
    public interface IReserveringRepo
    {
        int ReserveringenVoorLimoInPeriode(Limo limo, DateTime beginMetMarge, DateTime eindeMetMarge);
        int AantalReserveringenVoorKlantInJaar(Klant klant, int jaar);
        void Nieuw(Reservering reservering);
        Reservering LaatsteReserveringVoorLimo(Limo limo, DateTime periodeBegin);
        Reservering VolgendeReserveringVoorLimo(Limo limo, DateTime periodeEinde);
        IEnumerable<Reservering> AlleReserveringenVoorKlant(Klant klant);
        IEnumerable<Reservering> ReserveringenVoorLimoTussenDataBinnenArrangement(Limo limo, DateTime startDatum, DateTime eindDatum,
             Arrangement arrangement);
        IEnumerable<Reservering> ReserveringenMetAlleGegevens(Klant klant, Limo limo, DateTime startDatum, DateTime eindDatum,
             Arrangement arrangement);

        IEnumerable<Reservering> OphalenAlleReserveringen();
        Reservering OphalenReservering(Reservering res);
    }
}
