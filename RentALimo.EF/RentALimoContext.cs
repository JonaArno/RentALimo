using System.Data.Entity;
using System.Diagnostics;
using System.Data.Entity.ModelConfiguration.Conventions;
using RentALimo.Business;

namespace RentALimo.EF
{
    public class RentALimoContext : DbContext
    {
        private static readonly string ConnectionString =
            @"Data Source=(localDb)\mssqllocaldb; Database=rentalimo.mdf;" +
            "Initial Catalog=rentalimo;Integrated Security=true";

        public RentALimoContext() : base(ConnectionString)
        {
            if (!Database.Exists()) Database.Create();
            Database.Log = s => Debug.WriteLine(s);
            
            //wat doet dit?
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            
            // ...
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            //wat doen de twee hieronder?
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            
            
            //modelBuilder.Entity<EventingKorting>();
            // alle mappings toevoegen in deze assembly

            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
        }
    }
}
