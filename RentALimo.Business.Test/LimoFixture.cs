using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RentALimo.Business.Test
{
    [TestClass]
    public class LimoFixture
    {
        [TestMethod]
        public void ToStringGeeftMerkEnTypeWeer()
        {
            var target = new Limo("Audi", "A4",20,200,200);
            var expected = "Audi A4";
            Assert.AreEqual(expected,target.ToString());
        }

        [TestMethod]
        public void WeddingMogelijk()
        {
            var target = new Limo("Audi", "A4", 20, 200,200);
            var expected = true;
            Assert.AreEqual(expected,target.MogelijkBinnenArrangement(Arrangement.Wedding));
        }

        [TestMethod]
        public void WeddingNietMogelijk()
        {
            var target = new Limo("Audi", "A4", 20, 200, 0);
            var expected = false;
            Assert.AreEqual(expected, target.MogelijkBinnenArrangement(Arrangement.Wedding));
        }

        [TestMethod]
        public void NightlifeMogelijk()
        {
            var target = new Limo("Audi", "A4", 20, 200, 200);
            var expected = true;
            Assert.AreEqual(expected, target.MogelijkBinnenArrangement(Arrangement.NightLife));
        }

        [TestMethod]
        public void NightlifeNietMogelijk()
        {
            var target = new Limo("Audi", "A4", 20, 0, 200);
            var expected = false;
            Assert.AreEqual(expected, target.MogelijkBinnenArrangement(Arrangement.NightLife));
        }

        public void AirportMogelijk()
        {
            var target = new Limo("Audi", "A4", 20, 0, 0);
            var expected = true;
            Assert.AreEqual(expected, target.MogelijkBinnenArrangement(Arrangement.Airport));
        }

        [TestMethod]
        public void AirportNietMogelijk()
        {
            var target = new Limo("Audi", "A4", 0, 0, 200);
            var expected = false;
            Assert.AreEqual(expected, target.MogelijkBinnenArrangement(Arrangement.Airport));
        }

        public void BusinessMogelijk()
        {
            var target = new Limo("Audi", "A4", 20, 0, 0);
            var expected = true;
            Assert.AreEqual(expected, target.MogelijkBinnenArrangement(Arrangement.Business));
        }

        [TestMethod]
        public void BusinessNietMogelijk()
        {
            var target = new Limo("Audi", "A4", 0, 0, 200);
            var expected = false;
            Assert.AreEqual(expected, target.MogelijkBinnenArrangement(Arrangement.Business));
        }
    }
}
