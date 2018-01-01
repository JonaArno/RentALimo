using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RentALimo.Business.Test
{
    [TestClass]
    public class PrijsBerekeningFixture
    {
        [TestMethod]
        public void AfronderRondtNaarBovenAf()
        {
            var res = new Reservering(Arrangement.Airport, new Periode(DateTime.Now, DateTime.Now.AddHours(5)),
                new Limo("Peugeot", "AX8", "Blauw", new WagenPrijs()),
                new Klant("Filip Nuyts", new Adres("Holstraat", 9000, "Gent", "België"),
                    new KlantCategorie("De beste"),"2839828BE"),DateTime.Now); 

            var ber = new PrijsBerekening(res);
            var expected = 10;
            Assert.AreEqual(expected,ber.Afronder(9));
        }

        [TestMethod]
        public void AfronderRondtNaarOnderAf()
        {
            var res = new Reservering(Arrangement.Airport, new Periode(DateTime.Now, DateTime.Now.AddHours(5)),
                new Limo("Peugeot", "AX8", "Blauw", new WagenPrijs()),
                new Klant("Filip Nuyts", new Adres("Holstraat", 9000, "Gent", "België"),
                    new KlantCategorie("De beste"), "2839828BE"), DateTime.Now);
            var ber = new PrijsBerekening(res);
            var expected = 10;
            Assert.AreEqual(expected, ber.Afronder(12));
        }

        //btwPercentage kan wijzigen, test daarnaar aanpassen?
        [TestMethod]
        public void BerekenPrijsInclBtw()
        {
            var res = new Reservering(Arrangement.Airport, new Periode(DateTime.Now, DateTime.Now.AddHours(5)),
                new Limo("Peugeot", "AX8", "Blauw", new WagenPrijs()),
                new Klant("Filip Nuyts", new Adres("Holstraat", 9000, "Gent", "België"),
                    new KlantCategorie("De beste"), "2839828BE"), DateTime.Now);
            var ber = new PrijsBerekening(res);

            var expected = 212;
            Assert.AreEqual(expected,ber.BerekenPrijsInclBtw(200));
        }
    }
}
