using System.Collections.Generic;
using RentALimo.Business;

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
            var limoLijst = new List<Limo>
            {
                new Limo("Porsche", "Cayenne Limousine White", 300, 1500, 1000),
                new Limo("Porsche", "Cayenne Limousine Electric Blue", 350, 1600, 1100),
                new Limo("Mercedes", "SL 55 AMG", 175, 0, 500),
                new Limo("Mercedes", "SL 55 AMG", 175, 0, 500),
                new Limo("Lincoln", "White Limousine", 155, 490, 430),
                new Limo("Lincoln", "Pink Limousine", 175, 600, 500),
                new Limo("Lincoln", "Black Limousine", 165, 600, 500),
                new Limo("Hummer", "Limousine Yellow", 225, 1290, 790),
                new Limo("Hummer", "Limousine Black", 195, 990, 0),
                new Limo("Hummer", "Limousine White", 195, 990, 0),
                new Limo("Chrysler", "300C Sedan - White", 125, 0, 350),
                new Limo("Chrysler", "300C Sedan - White", 125, 0, 350),
                new Limo("Chrysler", "300C Sedan - Black", 125, 0, 350),
                new Limo("Chrysler", "300C Sedan - Black", 125, 0, 350),
                new Limo("Chrysler", "300C Limousine - White", 165, 600, 500),
                new Limo("Chrysler", "300C Limousine - Tuxedo Crème", 165, 600, 500)
            };
            Repo.NieuweLimos(limoLijst);


            var eventingKortingLijst = new List<EventingKorting>();
            var geenKorting= new EventingKorting("Geen Korting");
            eventingKortingLijst.Add(geenKorting);
            var groteOmzet = new EventingKorting("Grote Omzet");
            groteOmzet.Nieuw(2,5);
            groteOmzet.Nieuw(5,8);
            groteOmzet.Nieuw(8,12);
            eventingKortingLijst.Add(groteOmzet);
            var bedrijf = new EventingKorting("Bedrijf");
            bedrijf.Nieuw(2,4);
            bedrijf.Nieuw(4,6);
            bedrijf.Nieuw(6,9);
            eventingKortingLijst.Add(bedrijf);
            var belangrijkeParticulier = new EventingKorting("Belangrijke Particulier");
            belangrijkeParticulier.Nieuw(3,5);
            belangrijkeParticulier.Nieuw(5,7);
            belangrijkeParticulier.Nieuw(7,10);
            belangrijkeParticulier.Nieuw(10,15);
            eventingKortingLijst.Add(belangrijkeParticulier);
            Repo.NieuweKortingen(eventingKortingLijst);


            var klantenCategorieenLijst = new List<KlantCategorie>();

            var org = new KlantCategorie("Organisator", groteOmzet);
            klantenCategorieenLijst.Add(org);
            var vip = new KlantCategorie("Vip", belangrijkeParticulier);
            klantenCategorieenLijst.Add(vip);
            var part = new KlantCategorie("Particulier", geenKorting);
            klantenCategorieenLijst.Add(part);
            var occ = new KlantCategorie("Occasioneel", geenKorting);
            klantenCategorieenLijst.Add(occ);
            var wPlann = new KlantCategorie("WeddingPlanner", groteOmzet);
            klantenCategorieenLijst.Add(wPlann);
            var bed = new KlantCategorie("Bedrijf", bedrijf);
            klantenCategorieenLijst.Add(bed);
            Repo.NieuweKlantCategorieen(klantenCategorieenLijst);


            //nog paar extra klanten toevoegen
            var klantenLijst = new List<Klant>
            {
                new Klant("Jonathan Arnoys", new Adres("Fictievestraat 36", 9000, "Gent", "België"), vip),
                new Klant("KBC Group", new Adres("Brugsesteenweg 45", 8400, "Oostende", "België"), bed, "BE8745614615"),
                new Klant("Artevelde Hogeschool", new Adres("Suckstraat", 9000, "Gent", "België"), occ, "BE9846553227"),
                new Klant("Pol Desterke", new Adres("Teststraat 825", 9032, "Wondelgem", "België"), part)
            };
            Repo.NieuweKlanten(klantenLijst);

            // Categorieen
            // Klanten
            // ....


        }
              

    }
}
