using RentALimo.Business;
using RentALimo.Populeer;
using System.Collections.Generic;

namespace RentALimo.EF
{
    public class PopuleerRepo: IPopuleerRepo
    {

        public RentALimoContext Context { get; private set; }

        public PopuleerRepo(RentALimoContext context)
        {
            Context = context;
        }

        public void NieuweKortingen(IEnumerable<EventingKorting> kortingen)
        {
            Context.Set<EventingKorting>().AddRange(kortingen);
            Context.SaveChanges();
        }
        
        
    }
}
