using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentALimo.Business;

namespace RentALimo.EF.Mapping
{
    public class PeriodeConfiguration : ComplexTypeConfiguration<Periode>
    {
        public PeriodeConfiguration()
        {
            Property(i => i.Begin)
                .HasColumnName("Begin")
                .IsRequired();
            Property(i => i.Einde)
                .HasColumnName("Einde")
                .IsRequired();
        }
    }
}
