using System;

namespace RentALimo.Business
{
    public class Reservering
    {
        public int ReserveringsId { get; set; }
        public Arrangement Arrangement { get; set; }
        public Periode Periode { get; set; }
        public Limo Limo { get; set; }
        public Locatie StartLocatie { get; set; }
        public Locatie EindLocatie { get; set; }
        public Klant Klant { get; set; }
        public PrijsInfo PrijsInfo { get; set;}
        public DateTime ReserveringsDatum { get; set; }


        protected Reservering() { }

        public Reservering(Arrangement arrangement, Periode periode, Locatie startLocatie, Locatie eindLocatie, Limo limo, Klant klant,PrijsInfo prijsInfo)
        {
            Arrangement = arrangement;
            Periode = periode;
            StartLocatie = startLocatie;
            EindLocatie = eindLocatie;
            Limo = limo;
            Klant = klant;
            PrijsInfo = prijsInfo;
            ReserveringsDatum = DateTime.Now;
        }

    }
}