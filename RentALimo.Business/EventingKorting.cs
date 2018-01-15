using System.Collections.Generic;
using System.Linq;

namespace RentALimo.Business
{
    public class EventingKorting
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public ICollection<EventingKortingItem> Items { get; protected set; } = new SortedSet<EventingKortingItem>();
        
        // default ctor nodig voor persistentie
        protected EventingKorting() { }

        public EventingKorting(string naam)
        {
            Naam = naam;
        }
       

        public double KortingVoorAantal(int aantal)
        {
            return Items
                .LastOrDefault(i => i.Aantal <= aantal)?.Korting??0.0;
        }

        public bool IsGeldig()
        {
            double grootste = 0;

            foreach (var kortingItem in Items)
            {
                if (kortingItem.Korting > grootste)
                {
                    grootste = kortingItem.Korting;
                }
                else return false;
            }
            return true;
        }


        public override string ToString()
        {
            return $"Naam: {Naam}";
        }

        public void Nieuw(int aantal, double korting)
        {
            if (aantal > 0 && korting > 0 && korting <= 100 && !AantalBestaatAl(aantal) && !KortingBestaatAl(korting))
            {
                Items.Add(new EventingKortingItem(aantal, korting));
            }
        }

        private bool KortingBestaatAl(double korting)
        {
            bool returnWaarde = false;

            foreach (var item in Items)
            {
                if (item.Korting == korting)
                {
                    return returnWaarde = true;
                }
            }

            return returnWaarde;
        }

        private bool AantalBestaatAl(int aantal)
        {
            bool returnWaarde = false;

            foreach (var item in Items)
            {
                if (item.Aantal == aantal)
                {
                    returnWaarde = true;
                }
            }

            return returnWaarde;
        }
    }


}
