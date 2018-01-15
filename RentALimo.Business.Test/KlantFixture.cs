using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RentALimo.Business.Test
{
    [TestClass]
    public class KlantFixture
    {
        [TestMethod]
        public void ToStringGeeftNaamWeer()
        {
            var target = new Klant("Pol",new Adres("polstraat 34", 8999,"Polgem","Polland"),new KlantCategorie("Pol",new EventingKorting("Pol")) );
            var expected = "Pol";
            Assert.AreEqual(expected,target.ToString());
        }

    }
}
