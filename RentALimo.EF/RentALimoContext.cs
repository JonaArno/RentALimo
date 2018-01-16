using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Data.Entity.ModelConfiguration.Conventions;
using RentALimo.Business;
using RentALimo.Populeer;

namespace RentALimo.EF
{
    public class RentALimoContext : DbContext
    {
        private static readonly string ConnectionString =
            @"Data Source=(localDb)\mssqllocaldb; Database=rentalimo.mdf;" +
            "Initial Catalog=rentalimo;Integrated Security=true";

        public RentALimoContext() : base(ConnectionString)
        {
            if (!Database.Exists())
            {
                Database.Create();

                var pop = new Populator(new PopuleerRepo());
                pop.Populeer();
            }
            Database.Log = s => Debug.WriteLine(s);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnName("datetime2"));
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
        }

    
    }


}
