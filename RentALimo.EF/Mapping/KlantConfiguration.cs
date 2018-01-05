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
            HasRequired(i => i.Adres);
            Property(i => i.BtwNummer).IsOptional();
            HasRequired(i => i.KlantCategorie);

            //als dit al gebeurt bij Reserveringen (Klant mandatory bij Reservering), dan is dit niet meer nodig
            //HasMany(i => i.Reserveringen)
            //    .WithOptional()
            //    .WillCascadeOnDelete(true);
        }
    }
}
