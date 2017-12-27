using RentALimo.Business;
using System.Collections.Generic;

namespace RentALimo.Populeer
{
    public interface IPopuleerRepo //eventueel :IReserveringRepo
    {
        void NieuweKortingen(IEnumerable<EventingKorting> kortingen);

        // Categorieen
        // Klanten
        // ....
        
        void NieuweKlantCategorieen(IEnumerable<KlantCategorie> klantcategoriën);
        void NieuweKlanten(IEnumerable<Klant> klanten);
        void NieuweLimos(IEnumerable<Limo> limos);
        
    }
}
