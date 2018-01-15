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

        [TestMethod]
        public void GeldigWeddingStartUurAccepted()
        {
            var target = new ReserveringBouwer(new NepRepo());

            var expected = true;
            var result = target.IsGeldigStartUur(Arrangement.Wedding, new DateTime(2018, 01, 4, 7, 0, 0));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OngeldigWeddingStartUurNietAccepted()
        {
            var target = new ReserveringBouwer(new NepRepo());

            var expected = false;
            var result = target.IsGeldigStartUur(Arrangement.Wedding, new DateTime(2018, 01, 4, 6, 0, 0));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GeldigNightlifeStartUurAccepted()
        {
            var target = new ReserveringBouwer(new NepRepo());

            var expected = true;
            var result = target.IsGeldigStartUur(Arrangement.NightLife, new DateTime(2018, 01, 4, 20, 0, 0));

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void MiddernachtNightlifeStartUurAccepted()
        {
            var target = new ReserveringBouwer(new NepRepo());

            var expected = true;
            var result = target.IsGeldigStartUur(Arrangement.NightLife, new DateTime(2018, 01, 4, 0, 0, 0));

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void OngeldigNightlifeStartUurNietAccepted()
        {
            var target = new ReserveringBouwer(new NepRepo());

            var expected = false;
            var result = target.IsGeldigStartUur(Arrangement.NightLife, new DateTime(2018, 01, 4, 6, 0, 0));

            Assert.AreEqual(expected, result);
        }
    }

    internal class NepRepo : IReserveringRepo
    {
        public List<Reservering> Reserveringen { get; set; } = new List<Reservering>();

        public int ReserveringenVoorLimoInPeriode(Limo limo, DateTime beginMetMarge, DateTime eindeMetMarge)
        {
            throw new NotImplementedException();
        }

        public int AantalReserveringenVoorKlantInJaar(Klant klant, int jaar)
        {
            var ditjaar = DateTime.Today.Year;
            if (klant.Naam == "Dirk Andries") return 100;
            if (klant.Naam == "Sven") return 2;
            if (klant.Naam == "Tom" && jaar == ditjaar) return 3;
            return Reserveringen.Count(
                r => r.Klant.Equals(klant) && r.ReserveringsDatum.Year == jaar);
        }

        public Reservering LaatsteReserveringVoorStartuurHuidige(Limo limo, DateTime periodeBegin)
        {
            throw new NotImplementedException();
        }

        public void Nieuw(Reservering reservering)
        {
            Reserveringen.Add(reservering);
        }

        public Reservering LaatsteReserveringVoorLimo(Limo limo, DateTime periodeBegin)
        {
            throw new NotImplementedException();
        }

        public Reservering VolgendeReserveringVoorLimo(Limo limo, DateTime periodeEinde)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservering> AlleReserveringenVoorKlant(Klant klant)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservering> ReserveringenVoorLimoTussenDataBinnenArrangement(Limo limo, DateTime startDatum, DateTime eindDatum,
            Arrangement arrangement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservering> ReserveringenMetAlleGegevens(Klant klant, Limo limo, DateTime startDatum, DateTime eindDatum,
            Arrangement arrangement)
        {
            throw new NotImplementedException();
        }

        public Reservering OphalenReservering(Reservering res)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Klant> OphalenKlanten()
        {
            throw new NotImplementedException();
        }



    }
}
