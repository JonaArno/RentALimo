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
            //var kl = new Klant("Pol", new Adres("Holmerestraat 30", 9032, "WondelGem", "België"), new KlantCategorie("Pol",new EventingKorting("PolOok")));
            //kl.Reserveringen.Add(new Reservering(Arrangement.NightLife,
            //    new Periode(new DateTime(2017, 12, 11, 12, 0, 0), new DateTime(2017, 12, 1, 17, 0, 0)),
            //    new Limo("Peugeot", "Snel", new WagenPrijs(15,300,300)), kl, DateTime.Now));
            //kl.Reserveringen.Add(new Reservering(Arrangement.Wedding,
            //    new Periode(new DateTime(2017, 12, 11, 12, 0, 0), new DateTime(2017, 12, 1, 19,0, 0)),
            //    new Limo("Audi","lame",  new WagenPrijs(100,500,600)), kl, DateTime.Now));
            //var expected = 2;
            //Assert.AreEqual(expected, kl.ReserveringenInJaar(2017));
        }
        
    }
}
