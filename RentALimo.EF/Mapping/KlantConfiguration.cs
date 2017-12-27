using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using RentALimo.Business;

namespace RentALimo.EF.Mapping
{
    public class KlantConfiguration : EntityTypeConfiguration<Klant>
    {
        public KlantConfiguration()
        {
            HasKey(i => i.KlantId);
            Property(i => i.Naam).IsRequired();
            Property(i => i.Adres).IsRequired();
            Property(i => i.BtwNummer).IsOptional();
            Property(i => i.KlantCategorie).IsRequired();
            HasMany(i => i.Reserveringen)
                .WithOptional()
                .WillCascadeOnDelete(true);
        }
    }
}
