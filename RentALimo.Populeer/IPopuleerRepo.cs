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
    }
}
