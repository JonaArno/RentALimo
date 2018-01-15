using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RentALimo.Business.Test
{
    [TestClass]
    public class PeriodeFixture
    {
        [TestMethod]
        public void PeriodeMaxDuurIs11Uur()
        {
            var startTijd = new DateTime(2017, 12, 15, 05, 0, 0);
            var eindTijd = new DateTime(2017, 12, 15, 17, 0, 0);
            var periode = new Periode(startTijd, eindTijd);

            var expected = false;
            Assert.AreEqual(expected, periode.RespecteertMaxDuur());
        }

        [TestMethod]
        public void GeldigePeriodeDuurWerkt()
        {
            var startTijd = new DateTime(2017, 12, 15, 05, 0, 0);
            var eindTijd = new DateTime(2017, 12, 15, 12, 0, 0);
            var periode = new Periode(startTijd, eindTijd);

            var expected = true;
            Assert.AreEqual(expected, periode.IsGeldig());

        }

        [TestMethod]
        public void NightlifeBevatOveruren()
        {
            var startUur = DateTime.Now;
            var eindUur = DateTime.Now.AddHours(9);
            Arrangement arr = Arrangement.NightLife;

            var per = new Periode(startUur, eindUur);

            var expected = true;

            Assert.AreEqual(expected, per.BevatOverUren(arr));
        }

        [TestMethod]
        public void NightlifeBevatGeenOveruren()
        {
            var startUur = DateTime.Now;
            var eindUur = DateTime.Now.AddHours(6);
            Arrangement arr = Arrangement.NightLife;

            var per = new Periode(startUur, eindUur);

            var expected = false;

            Assert.AreEqual(expected, per.BevatOverUren(arr));
        }

  

    }
}
