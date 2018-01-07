using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace RentALimo.Business.Test
{
    [TestClass]
    public class ReserveringBouwerFixture
    {
        [TestMethod]
        public void TestEensIets()
        {
            var target = new ReserveringBouwer(new NepRepo());
        }
    }

    internal class NepRepo : IReserveringRepo
    {
        public List<Reservering> Reserveringen { get; set; } = new List<Reservering>();

        public int AantalReserveringenVoorKlantInJaar(Klant klant, int jaar)
        {
            var ditjaar = DateTime.Today.Year;
            if (klant.Naam == "Dirk Andries") return 100;
            if (klant.Naam == "Sven") return 2;
            if (klant.Naam == "Tom" && jaar == ditjaar) return 3;
            return Reserveringen.Count(
                r => r.Klant.Equals(klant) && r.ReserveringsDatum.Year == jaar);
        }

        public void Nieuw(Reservering reservering)
        {
            Reserveringen.Add(reservering);
        }

        public IEnumerable<Klant> OphalenKlanten()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Limo> ReserveringenVoorLimoInPeriode(Limo limo, DateTime begin, DateTime einde)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Reservering> IReserveringRepo.ReserveringenVoorLimoInPeriode(Limo limo, DateTime begin, DateTime einde)
        {
            throw new NotImplementedException();
        }
    }
}
