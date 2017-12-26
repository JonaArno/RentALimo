using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RentALimo.Business.Test
{
    [TestClass]
    public class EventingKortingFixture
    {
        [TestMethod]
        public void NieuwKortingIsNul()
        {
            var target = new EventingKorting("Leeg");
            const double expected = 0;
            var actual = target.KortingVoorAantal(42);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void KortingVoorExactAantal()
        {
            var target = new EventingKorting("Simpel");
            const int aantal = 2;
            const double korting = 7.5;
            target.Nieuw(aantal, korting);
            Assert.AreEqual(korting, target.KortingVoorAantal(aantal));
        }

        [TestMethod]
        public void UitVolgorde()
        {
            var target = new EventingKorting("Uitvolgorde");
            target.Nieuw(15, 10);
            target.Nieuw(2, 5);
            Assert.AreEqual(target.KortingVoorAantal(15), 10.0);
            Assert.AreEqual(target.KortingVoorAantal(2), 5.0);
        }
    }
}
