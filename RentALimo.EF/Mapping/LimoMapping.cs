using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentALimo.Business;
using System.Data.Entity.ModelConfiguration;

namespace RentALimo.EF.Mapping
{
    public class LimoMapping : EntityTypeConfiguration<Limo>
    {
        public LimoMapping()
        {
            HasKey(i => i.WagenId);
            Property(i => i.Merk).IsRequired();
            Property(i => i.Type).IsRequired();
            Property(i => i.Kleur).IsRequired();
            Property(i => i.WagenPrijs).IsRequired();
        }
    }
}

