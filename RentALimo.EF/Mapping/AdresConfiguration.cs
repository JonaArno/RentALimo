﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using RentALimo.Business;

namespace RentALimo.EF.Mapping
{
    public class AdresConfiguration : EntityTypeConfiguration<Adres>
    {
        public AdresConfiguration()
        {
            ToTable("Adres");
            HasKey(i => i.Id);
            Property(i => i.Straat).IsRequired();
            Property(i => i.PostCode).IsRequired();
            Property(i => i.Stad).IsRequired();
            Property(i => i.Land).IsRequired();
        }
    }
}
