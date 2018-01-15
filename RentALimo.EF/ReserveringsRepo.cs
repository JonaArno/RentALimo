using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using RentALimo.Business;
using System.Data.Entity;

namespace RentALimo.EF
{
    public class ReserveringsRepo : IReserveringRepo
    {
        public RentALimoContext Context = new RentALimoContext();


        public int ReserveringenVoorLimoInPeriode(Limo limo, DateTime beginMetMarge, DateTime eindeMetMarge)
        {
            List<Reservering> lookup = Context.Set<Reservering>()
                                            .Where(r => r.Limo.WagenId == limo.WagenId &&
                                                        r.Periode.Begin >= beginMetMarge &&
                                                        r.Periode.Einde <= eindeMetMarge)
                                            .ToList();
            return lookup.Count;
        }

        public int AantalReserveringenVoorKlantInJaar(Klant klant, int jaar)
        {
            int returnWaarde = 0;
            List<Reservering> opzoekWerk = Context.Set<Reservering>()
                .Where(res => res.Klant.KlantId == klant.KlantId
                              && res.ReserveringsDatum.Year == jaar)
                .ToList();

            returnWaarde = opzoekWerk.Count;

            return returnWaarde;
        }

        public Reservering LaatsteReserveringVoorLimo(Limo limo, DateTime periodeBegin)
        {
            //ordent dit wel van klein naar groot?
            List<Reservering> res = Context.Set<Reservering>()
                                    .Include(r => r.Limo)
                                    .Where(r => r.Limo.WagenId == limo.WagenId)
                                    .Where(r => r.Periode.Einde <= periodeBegin)
                                    .OrderBy(r => r.Periode.Einde)
                                    .ToList();

            return res.FirstOrDefault();
        }

        public Reservering VolgendeReserveringVoorLimo(Limo limo, DateTime periodeEinde)
        {
            //ordening ok?
            List<Reservering> res = Context.Set<Reservering>()
                .Include(r => r.Limo)
                .Where(r => r.Limo.WagenId == limo.WagenId)
                .Where(r => r.Periode.Begin >= periodeEinde)
                .OrderBy(r => r.Periode.Begin)
                .ToList();

            return res.FirstOrDefault();
        }

        public IEnumerable<Reservering> AlleReserveringenVoorKlant(Klant klant)
        {
            return Context.Set<Reservering>()
                .Include(r => r.Limo)
                .Include(r => r.Klant)
                .Where(r => r.Klant.KlantId == klant.KlantId)
                .ToList();
        }

        public IEnumerable<Reservering> ReserveringenVoorLimoTussenDataBinnenArrangement(Limo limo, DateTime startDatum, DateTime eindDatum,
            Arrangement arrangement)
        {
            return Context.Set<Reservering>()
                .Include(r => r.Limo)
                .Include(r => r.Klant)
                .Where(r => r.Limo.WagenId == limo.WagenId &&
                            r.Periode.Begin >= startDatum &&
                            //AddDays nodig anders missen we alle uren na 0h die dag
                            r.Periode.Einde < eindDatum.AddDays(1) &&
                            r.Arrangement == arrangement)
                .OrderBy(res => res.Periode.Begin)
                .ToList();
        }

        public IEnumerable<Reservering> ReserveringenMetAlleGegevens(Klant klant, Limo limo, DateTime startDatum, DateTime eindDatum,
            Arrangement arrangement)
        {
            DateTime dt = eindDatum;

            if (eindDatum != DateTime.MaxValue)
            {
                dt = eindDatum.AddDays(1);
            }

            return Context.Set<Reservering>()
                .Include(r => r.Limo)
                .Include(r => r.Klant)
                .Where(r => r.Klant.KlantId == klant.KlantId &&
                            r.Limo.WagenId == limo.WagenId &&
                            r.Periode.Begin >= startDatum &&
                            //AddDays nodig anders missen we alle uren na 0h die dag
                            r.Periode.Einde < dt &&
                            r.Arrangement == arrangement)
                .OrderBy(res => res.Periode.Begin)
                .ToList();
        }

        public IEnumerable<Reservering> OphalenAlleReserveringen()
        {
            return Context.Set<Reservering>()
                .Include(res => res.Klant)
                .Include(res => res.Limo)
                .ToList();
        }

        public Reservering OphalenReservering(Reservering res)
        {
            return Context.Set<Reservering>()
                .Include(r => r.Limo)
                .Include(r => r.Klant)
                .Where(r => r.ReserveringsId == res.ReserveringsId)
                .ToList()
                .First();
        }


        public void Nieuw(Reservering res)
        {

            //dubbele records vermijden:
            Klant klant = Context
                .Set<Klant>()
                .Include(kl => kl.Adres)
                .Include(kl => kl.KlantCategorie)
                .First(k => k.KlantId == res.Klant.KlantId);


            Limo limo = Context
                .Set<Limo>()
                .First(l => l.WagenId == res.Limo.WagenId);

            res.Klant = klant;
            res.Limo = limo;

            Context.Set<Reservering>().Add(res);
            Context.SaveChanges();
        }


    }
}
