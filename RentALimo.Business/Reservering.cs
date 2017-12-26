using System;

namespace RentALimo.Business
{
    public class Reservering
    {
        public int ReserveringsId { get; set; }
        public Arrangement Arrangement { get; set; }
        public Periode Periode { get; set; }
        public Limo Limo { get; set; }
        public Klant Klant { get; set; }
        public DateTime ReserveringsDatum { get; set; }

        protected Reservering() { }

        public Reservering(Arrangement arrangement, Periode periode, Limo limo, Klant klant, DateTime datum)
        {
            Arrangement = arrangement;
            Periode = periode;
            Limo = limo;
            Klant = klant;
            ReserveringsDatum = datum;
        }

    }
}