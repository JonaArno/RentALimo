using RentALimo.Business;
using RentALimo.Populeer;
using System.Collections.Generic;

namespace RentALimo.EF
{
    public class PopuleerRepo: IPopuleerRepo
    {

        public RentALimoContext Context { get; private set; }

        public PopuleerRepo()
        {
            Context = new RentALimoContext();
        }

        public void NieuweKortingen(IEnumerable<EventingKorting> kortingen)
        {
            Context.Set<EventingKorting>().AddRange(kortingen);
            Context.SaveChanges();
        }
        
        public void NieuweKlantCategorieen(IEnumerable<KlantCategorie> klantcategoriën)
        {
            Context.Set<KlantCategorie>().AddRange(klantcategoriën);
            Context.SaveChanges();      
        }

        //hieronder ook WagenPrijsItems
        public void NieuweKlanten(IEnumerable<Klant> klanten)
        {
            Context.Set<Klant>().AddRange(klanten);
            Context.SaveChanges();
        }

        public void NieuweLimos(IEnumerable<Limo> limos)
        {
            Context.Set<Limo>().AddRange(limos);
            Context.SaveChanges();
        }
        
        //nog dingen toe te voegen? 
       
    }
}
