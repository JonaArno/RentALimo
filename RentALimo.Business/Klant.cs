﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace RentALimo.Business
{
    public class Klant
    {
        public int KlantId { get; set; }
        public string Naam { get; set; }
        public Adres Adres { get; set; }
        public string BtwNummer { get; set; }

        public KlantCategorie KlantCategorie { get; set; }
        //public ICollection<Reservering> Reserveringen = new List<Reservering>();

        protected Klant() { }

        public Klant(string naam, Adres adres, KlantCategorie klantCategorie)
        {
            Naam = naam;
            Adres = adres;
            KlantCategorie = klantCategorie;
        }

        public Klant(string naam, Adres adres, KlantCategorie klantCategorie, string btwNummer) : this(naam, adres, klantCategorie)
        {
            BtwNummer = btwNummer;
        }

        //dit moet in de repo gebeuren
        public int ReserveringenInJaar(int jaarTal)
        {
            //int returnWaarde = 0;
            //foreach (Reservering res in Reserveringen)
            //{
            //    if (res.ReserveringsDatum.Year == jaarTal)
            //    {
            //        returnWaarde += 1;
            //    }
            //}
            //return returnWaarde;

            return 0;
        }
    }

}