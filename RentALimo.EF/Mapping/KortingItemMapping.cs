using RentALimo.Business;
using System.Data.Entity.ModelConfiguration;

namespace RentALimo.EF.Mapping
{
    public class KortingItemMapping: EntityTypeConfiguration<EventingKortingItem>
    {
        public KortingItemMapping()
        {
            HasKey(i => i.Id);
            Property(i => i.Aantal).IsRequired();
            Property(i => i.Korting).IsRequired();
        }
    }
}
