using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentALimo.Business;
using System.Data.Entity.ModelConfiguration;

namespace RentALimo.EF.Mapping
{
    public class LimoConfiguration : EntityTypeConfiguration<Limo>
    {
        public LimoConfiguration()
        {
            HasKey(i => i.WagenId);
            Property(i => i.Merk)
                .IsRequired();
            Property(i => i.Type)
                .IsRequired();
            Property(i => i.EersteUurPrijs).IsRequired();
            Property(i => i.WeddingArrangementPrijs).IsRequired();
            Property(i => i.NightLifeArrangementPrijs).IsRequired();
        }
    }
}

