﻿using System;

namespace RentALimo.Business
{
    public class Periode
    {
        public static readonly TimeSpan MaximaleDuur = new TimeSpan(11,0,0);

        public TimeSpan Duur { get; internal set; }
        public DateTime Begin { get; internal set; }
        public DateTime Einde { get; internal set; }

        protected Periode() { }

        public Periode(DateTime begin, DateTime einde)
        {
            Begin = begin;
            Einde = einde;
            Duur = einde - begin;
        }

        public bool IsGeldig()
        {
            if (RespecteertMaxDuur() && Einde > Begin)
                return true;
            else return false;
        }

        public bool RespecteertMaxDuur()
        {
            if (Duur > TimeSpan.FromHours(MaximaleDuur.Hours))
            {
                return false;
            }
            else return true;
        }

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
        
    }
}
