using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RentALimo.Business.Test
{
    [TestClass]
    public class KlantFixture
    {
        [TestMethod]
        public void ReserveringenDitJaarGoedBijgehouden()
        {
            //test herschrijven adhv DateTime.Now.Addhours
            var kl = new Klant("Pol", new Adres("Holmerestraat 30", 9032, "WondelGem", "België"), new KlantCategorie());
            kl.Reserveringen.Add(new Reservering(Arrangement.NightLife,
                new Periode(new DateTime(2017, 12, 11, 12, 0, 0), new DateTime(2017, 12, 1, 17, 0, 0)),
                new Limo("Peugeot", "Snel", "Blauw", new WagenPrijs()), kl, DateTime.Now));
            kl.Reserveringen.Add(new Reservering(Arrangement.Wedding,
                new Periode(new DateTime(2017, 12, 11, 12, 0, 0), new DateTime(2017, 12, 1, 19,0, 0)),
                new Limo("Audi","lame", "Blauw", new WagenPrijs()), kl, DateTime.Now));
            var expected = 2;
            Assert.AreEqual(expected, kl.ReserveringenDitJaar());
        }
        
    }
}
