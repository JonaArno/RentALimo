using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using RentALimo.Business;

namespace RentALimo.EF.Mapping
{
    public class KlantCategorieConfiguration : EntityTypeConfiguration<KlantCategorie>
    {
        public KlantCategorieConfiguration()
        {
            HasKey(i => i.CategorieId);
            Property(i => i.Naam).IsRequired();
            //klopt dit?
            //HasMany(i => i.Kl)
            //    .WithOptional()
            //    .Map(m => m.MapKey("KlantCategorie"))
            //    //true?? - als klantcategorie verwijdert wordt, moet de FK ook leeg komen te staan bij de klanten
            //    .WillCascadeOnDelete(true);
        }
    }
}
