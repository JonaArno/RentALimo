using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentALimo.Business;

namespace RentALimo.EF.Mapping
{
    public class PrijsInfoConfiguration : ComplexTypeConfiguration<PrijsInfo>
    {
        public PrijsInfoConfiguration()
        {
            Property(p => p.AantalGewoneUren)
                .IsRequired();
            Property(p => p.AantalNachtUren)
                .IsRequired();
            Property(p => p.AantalOverUren)
                .IsRequired();
            Property(p => p.BedragExclusiefBtwVoorEventingKorting)
                .IsRequired();
            Property(p => p.AangerekendeEventingKorting)
                .IsRequired();
            Property(p => p.BedragExclusiefBtwNaEventingKorting)
                .IsRequired();
            Property(p => p.BtwBedrag)
                .IsRequired();
            Property(p => p.TotaalTeBetalenBedrag)
                .IsRequired();    
        }
    }
}
