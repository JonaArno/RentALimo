using RentALimo.Business;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentALimo.EF.Mapping
{
    public class KortingMapping: EntityTypeConfiguration<EventingKorting>
    {
        public KortingMapping()
        {
            HasKey(k => k.Id);
            Property(k => k.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(k => k.Naam)
                .IsRequired();
            HasMany(k => k.Items)
                .WithRequired()
                .Map(m => m.MapKey("EventingKorting"))
                .WillCascadeOnDelete(true);
        }
    }
}
