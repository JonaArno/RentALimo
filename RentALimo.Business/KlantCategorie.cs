using System.Collections;
using System.Collections.Generic;

namespace RentALimo.Business
{
    public class KlantCategorie
    {
        public int CategorieId { get; set; }
        public string Naam { get; set; }
        public EventingKorting EventingKorting { get; set; }
        
        //public ICollection<Klant> Klanten = new List<Klant>();

        protected KlantCategorie() { }

        public KlantCategorie(string naam,EventingKorting eventingKorting)
        {
            Naam = naam;
            EventingKorting = eventingKorting;
        }
    }
}
