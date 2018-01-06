using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using RentALimo.Business;

namespace RentALimo.EF.Mapping
{
    public class ReserveringConfiguration : EntityTypeConfiguration<Reservering>
    {
        public ReserveringConfiguration()
        {
            ToTable("Reservering");
            HasKey(i => i.ReserveringsId);
            Property(i => i.ReserveringsId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.Arrangement).IsRequired();
            Property(i => i.StartLocatie).IsRequired();
            Property(i => i.EindLocatie).IsRequired();
            Property(i => i.ReserveringsDatum).IsRequired();
            //HasRequired(i => i.Periode);
            HasRequired(i => i.Limo);
            HasRequired(i => i.Klant);
        }

    }
}
