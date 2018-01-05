using RentALimo.Business;
using System.Collections.Generic;

namespace RentALimo.Populeer
{
    public interface IPopuleerRepo //eventueel :IReserveringRepo
    {
        //Lector
        void NieuweKortingen(IEnumerable<EventingKorting> kortingen);
        //Zelf
        void NieuweKlantCategorieen(IEnumerable<KlantCategorie> klantcategoriën);
        void NieuweKlanten(IEnumerable<Klant> klanten);
        void NieuweLimos(IEnumerable<Limo> limos);
        
    }
}
