using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RentALimo.Business.Test
{
    [TestClass]
    public class PrijsBerekeningFixture
    {
        private static readonly Limo TestLimo = new Limo("Testcar","ForDummies",100,500, 600);
        private static readonly EventingKorting LegeEventingKorting = new EventingKorting("Leeg");
        private static EventingKorting _gevuldeEventingKorting = new EventingKorting("Gevulde Eventingkorting");

        [TestMethod]
        public void AfronderRondtNaarBovenAf()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = 10;
            Assert.AreEqual(expected, target.Afronder(8));
            
        }

        [TestMethod]
        public void AfronderRondtNaarOnderAf()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = 10;
            Assert.AreEqual(expected, target.Afronder(11));
        }

        [TestMethod]
        public void VijfRondtNietAf()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = 5;
            Assert.AreEqual(expected, target.Afronder(5));
        }

        [TestMethod]
        public void NulBlijftZelfdeBijAfronding()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = 0;
            Assert.AreEqual(expected, target.Afronder(0));
        }

        [TestMethod]
        public void NachtUurPrijsBerekentCorrect()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = 120;
            Assert.AreEqual(expected, target.NachtUurPrijs(TestLimo.EersteUurPrijs));
        }

        [TestMethod]
        public void TweedeUurPrijsBerekentCorrect()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = 60;
            Assert.AreEqual(expected, target.TweedeUurPrijs(TestLimo.EersteUurPrijs));
        }

        [TestMethod]
        public void KortingBerekentCorrect()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = 75;
            Assert.AreEqual(expected, target.BerekenPrijsNaEventingKorting(TestLimo.EersteUurPrijs, 25));
        }

        [TestMethod]
        public void OverUurWordtAlsdusHerkend()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = true;
            Assert.AreEqual(expected, target.IsOverUur(target.Periode.Begin.AddHours(9)));
        }

        [TestMethod]
        public void GeenOverUurWordtAlsdusHerkend()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = false;
            Assert.AreEqual(expected, target.IsOverUur(target.Periode.Begin.AddHours(6)));
        }

        [TestMethod]
        public void EerstUurWordtHerkend()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = true;
            Assert.AreEqual(expected, target.IsEersteUur(target.Periode.Begin));
        }

        [TestMethod]
        public void AnderUurNietAlsEersteUurHerkend()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = false;
            Assert.AreEqual(expected, target.IsEersteUur(target.Periode.Begin.AddHours(2)));
        }

        [TestMethod]
        public void NachtuurwordtHerkend()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = true;
            Assert.AreEqual(expected, target.IsNachtUur(new DateTime(2018,01,01,22,0,0)));
        }

        [TestMethod]
        public void MidderNachtIsNachtuur()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, DateTime.Now,
                DateTime.Now.AddHours(5));
            var expected = true;
            Assert.AreEqual(expected, target.IsNachtUur(new DateTime(2018, 01, 01, 0, 0, 0)));
        }

        [TestMethod]
        public void PrijsVoorEventingKortingWordtBerekend()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, new DateTime(2017, 10, 10, 10, 0, 0),
                new DateTime(2017, 10, 10, 15, 0, 0));
            var expected = 340;
            Assert.AreEqual(expected, target.PrijsInfo.BedragExclusiefBtwVoorEventingKorting);
        }

        [TestMethod]
        public void BerekentEventingKortingCorrect()
        {
            _gevuldeEventingKorting.Nieuw(1,5);
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, _gevuldeEventingKorting,1, new DateTime(2017,10,10,10,0,0), 
                new DateTime(2017,10,10,15,0,0));
            var expected = 17;
            Assert.AreEqual(expected,target.PrijsInfo.AangerekendeEventingKorting);
        }

        [TestMethod]
        public void BerekentPrijsNaEventingKortingCorrect()
        {
            _gevuldeEventingKorting.Nieuw(1, 5);
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, _gevuldeEventingKorting, 1, new DateTime(2017, 10, 10, 10, 0, 0),
                new DateTime(2017, 10, 10, 15, 0, 0));
            var expected = 323;
            Assert.AreEqual(expected, target.PrijsInfo.BedragExclusiefBtwNaEventingKorting);
        }

        [TestMethod]
        public void BerekentBtwPrijsCorrect()
        {
            _gevuldeEventingKorting.Nieuw(1, 5);
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, _gevuldeEventingKorting, 1, new DateTime(2017, 10, 10, 10, 0, 0),
                new DateTime(2017, 10, 10, 15, 0, 0));
            var expected = (decimal)19.38;
            Assert.AreEqual(expected, target.PrijsInfo.BtwBedrag);
        }

        [TestMethod]
        public void BerekentTotaalPrijsCorrect()
        {
            _gevuldeEventingKorting.Nieuw(1, 5);
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, _gevuldeEventingKorting, 1, new DateTime(2017, 10, 10, 10, 0, 0),
                new DateTime(2017, 10, 10, 15, 0, 0));
            var expected = (decimal)342.38;
            Assert.AreEqual(expected, target.PrijsInfo.TotaalTeBetalenBedrag);
        }

        [TestMethod]
        public void AantalGewoneUrenBerekentCorrect()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, new DateTime(2017, 10, 10, 10, 0, 0),
                new DateTime(2017, 10, 10, 15, 0, 0));
            var expected = 5;
            Assert.AreEqual(expected,target.PrijsInfo.AantalGewoneUren);
        }

        [TestMethod]
        public void AantalNachtUrenBerekentCorrect()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Airport, LegeEventingKorting, 0, new DateTime(2017, 10, 10, 0, 0, 0),
                new DateTime(2017, 10, 10, 8, 0, 0));
            var expected = 7;
            Assert.AreEqual(expected, target.PrijsInfo.AantalNachtUren);
        }

        [TestMethod]
        public void AantalOverUrenBerekentCorrect()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.Wedding, LegeEventingKorting, 0, new DateTime(2017, 10, 10, 10, 0, 0),
                new DateTime(2017, 10, 10, 20, 0, 0));
            var expected = 3;
            Assert.AreEqual(expected, target.PrijsInfo.AantalOverUren);
        }

        [TestMethod]
        public void AantalNachtUrenBerekentCorrectBijNightlife()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.NightLife, LegeEventingKorting, 0, new DateTime(2017, 10, 10, 23, 0, 0),
                new DateTime(2017, 10, 11, 8, 0, 0));
            var expected = 8;
            Assert.AreEqual(expected, target.PrijsInfo.AantalNachtUren);
        }

        [TestMethod]
        public void AantalGewoneUrenBerekentCorrectBijNightlife()
        {
            var target = new PrijsBerekening(TestLimo, Arrangement.NightLife, LegeEventingKorting, 0, new DateTime(2017, 10, 10, 23, 0, 0),
                new DateTime(2017, 10, 11, 8, 0, 0));
            var expected = 1;
            Assert.AreEqual(expected, target.PrijsInfo.AantalGewoneUren);
        }
    }
}
