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

        public override string ToString()
        {
            return $"Naam: {Naam}";
        }

        public void Nieuw(int aantal, double korting)
        {
            Items.Add(new EventingKortingItem(aantal, korting));
        }
    }


}
