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

        [TestMethod]
        public void KleinerAantalNooitGrotereKorting()
        {
            var target = new EventingKorting("KleinerAantalNooitGrotereKorting");
            target.Nieuw(1,20);
            target.Nieuw(2, 10);

            var expected = false;
            var result =target.IsGeldig();
            
            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void MoetenGroterDanNulZijn()
        {
            var target = new EventingKorting("MoetenGroterDan0Zijn");
            target.Nieuw(-2,-3);
            const int expected = 0;
            var result = target.Items.Count;
            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void MaxKortingIs100()
        {
            var target = new EventingKorting("MaxKortingIs100");
            target.Nieuw(3,101);
            const int expected = 0;
            var result = target.Items.Count;
            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void AantalIsUniek()
        {
            var target = new EventingKorting("AantalIsUniek");
            target.Nieuw(1,5);
            target.Nieuw(1,6);
            const int expected = 1;
            var result = target.Items.Count;
            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void KortingIsUniek()
        {
            var target = new EventingKorting("KortingIsUniek");
            target.Nieuw(1, 5);
            target.Nieuw(2, 5);
            const int expected = 1;
            var result = target.Items.Count;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GroterIsKleinerKanNiet()
        {
            var target = new EventingKorting("GroterIsKleinerKanNiet");
            target.Nieuw(1,10);
            target.Nieuw(3,3);
            var expected = false;
            Assert.AreEqual(expected,target.IsGeldig());
        }
    }
}
