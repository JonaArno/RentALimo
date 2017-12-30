using System;

namespace RentALimo.Business
{
    public class Periode
    {
        public static readonly int MaximaleDuur = 11;

        public int Id { get; set; }
        public TimeSpan Duur { get; internal set; }
        public DateTime Begin { get; internal set; }
        public DateTime Einde { get; internal set; }

        //denkpiste
        public int NormaleUren { get; set; }
        public int AantalOverUren { get; set; }
        public int AantalNachtUren { get; set; }

        protected Periode() { }

        public Periode(DateTime begin, DateTime einde)
        {
            Begin = begin;
            Einde = einde;
            Duur = einde - begin;
            IndelingInUren();
        }

        private void IndelingInUren()
        {
            //#nachturen berekenen
            var copy = Begin;
            do
            {
                copy = copy.AddHours(1);
                if (copy.Hour >= 22 || copy.Hour >= 0 && copy.Hour <= 6)
                {
                    AantalNachtUren += 1;
                }

            } while (copy < Einde);
        }

        public bool RespecteertMaxDuur()
        {
            if (Duur > TimeSpan.FromHours(MaximaleDuur))
            {
                return false;
            }
            else return true;
        }

        //public bool IsGeldig(Arrangement arr)
        //{
        //    switch (arr)
        //    {
        //        case Arrangement.Airport:
        //            {

        //            }
        //            break;
        //        case Arrangement.Business:
        //            {

        //            }
        //            break;
        //        case Arrangement.NightLife:
        //            {

        //            }
        //            break;
        //        case Arrangement.Wedding:
        //            {
        ////                werkt dit??
        ////                dit houdt nog geen rekening met het tijden OP het uur
        ////                testen of startuur tussen 07y en 15u is &of totaletijd onder het maximum blijft
        //                if (DateTime.Compare(Begin, new DateTime(Begin.Year, Begin.Month, Begin.Day, 7, 0, 0)) > 0
        //                    && DateTime.Compare(Begin, new DateTime(Begin.Year, Begin.Month, Begin.Day, 15, 0, 0)) < 0
        //                    && Duur.Hours < MaximaleDuur)
        //                {
        //                    return true;
        //                }
        //                else return false;
        //            }
        //    }


        //schrappen?
        public bool BevatOverUren(Arrangement arr)
        {
            bool bevatOveruren = false;

            if (arr == Arrangement.NightLife || arr == Arrangement.Wedding)
            {
                bevatOveruren = this.Duur > TimeSpan.FromHours(7);
            }
            return bevatOveruren;
        }

        //schrappen?
        //public bool BevatNachtUren()
        //{

        //}

        
    }
}