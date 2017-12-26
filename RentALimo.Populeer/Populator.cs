using System.Collections.Generic;

namespace RentALimo.Populeer
{
    public class Populator
    {
        public IPopuleerRepo Repo { get; private set; }

        public Populator(IPopuleerRepo repo)
        {
            Repo = repo;
        }

        public void Populeer()
        {
            var lijst = new List<Business.EventingKorting>();
            lijst.Add(new Business.EventingKorting("Leeg"));
            // ...
            Repo.NieuweKortingen(lijst);

            // Categorieen
            // Klanten
            // ....


        }
              

    }
}
