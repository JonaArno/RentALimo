using RentALimo.Business;
using System.Data.Entity.ModelConfiguration;

namespace RentALimo.EF.Mapping
{
    public class EventingKortingItemConfiguration: EntityTypeConfiguration<EventingKortingItem>
    {
        public EventingKortingItemConfiguration()
        {
            HasKey(i => i.Id);
            Property(i => i.Aantal).IsRequired();
            Property(i => i.Korting).IsRequired();
        }
    }
}
