using System;
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
                new Limo("Mercedes", "SL 55 AMG Silver", 175, 0, 500),
                new Limo("Mercedes", "SL 55 AMG Silver", 175, 0, 500),
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
            belangrijkeParticulier.Nieuw(2,5);
            belangrijkeParticulier.Nieuw(4,7);
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


            var reserveringLijst = new List<Reservering>
            {
                new Reservering(Arrangement.Wedding,
                    new Periode(new DateTime(2018, 01, 13, 9, 0, 0), new DateTime(2018, 01, 13, 17, 0, 0)),
                    Locatie.Antwerpen, Locatie.Gent, limoLijst[0], klantenLijst[0],
                    new PrijsBerekening(limoLijst[0], Arrangement.Wedding,
                        klantenLijst[0].KlantCategorie.EventingKorting, 0, new DateTime(2018, 01, 13, 9, 0, 0),
                        new DateTime(2018, 01, 13, 17, 0, 0)).PrijsInfo),
                new Reservering(Arrangement.NightLife,
                    new Periode(new DateTime(2018, 01, 18, 23, 0, 0), new DateTime(2018, 01, 19, 06, 0, 0)),
                    Locatie.Gent, Locatie.Gent, limoLijst[4], klantenLijst[0],
                    new PrijsBerekening(limoLijst[0], Arrangement.NightLife,
                        klantenLijst[0].KlantCategorie.EventingKorting, 1, new DateTime(2018, 01, 18, 23, 0, 0),
                        new DateTime(2018, 01, 19, 06, 0, 0)).PrijsInfo),
                new Reservering(Arrangement.Business,
                    new Periode(new DateTime(2018, 01, 08, 23, 0, 0), new DateTime(2018, 01, 09, 06, 0, 0)),
                    Locatie.Hasselt, Locatie.Gent, limoLijst[6], klantenLijst[0],
                    new PrijsBerekening(limoLijst[0], Arrangement.Business,
                        klantenLijst[0].KlantCategorie.EventingKorting, 2, new DateTime(2018, 01, 08, 23, 0, 0),
                        new DateTime(2018, 01, 09, 06, 0, 0)).PrijsInfo),
            };
            Repo.NieuweReserveringen(reserveringLijst);
            

        }
              

    }
}
