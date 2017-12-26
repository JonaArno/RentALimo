using System;

namespace RentALimo.Business
{
    public class EventingKortingItem : IComparable<EventingKortingItem>
    {
      
        public int Id { get; set; }
        public int Aantal { get; protected set; }
        public double Korting { get; protected set; }

        protected EventingKortingItem()
        {
        }

        internal  EventingKortingItem(int aantal, double korting)
        {
            Aantal = aantal;
            Korting = korting;
        }

        public override string ToString()
        {
            return $"{nameof(Aantal)}:{Aantal}, {nameof(Korting)}:{Korting }";
        }

        public int CompareTo(EventingKortingItem other)
        {
            return Aantal - other.Aantal;
        }
    }


}