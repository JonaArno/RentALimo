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
			List<Reservering>() lookup = Context.Set<Reservering>()
											.Where(r => r.Limo.WagenId == limo.WagenId &&
														r.Periode.Begin >= beginMetMarge &&
														r.Periode.Einde <= eindeMetMarge)
											.ToList();
			return lookup.Count;
		}

        //dit zou best een int retourneren
        //public IEnumerable<Reservering> ReserveringenVoorLimoInPeriode(Limo limo, DateTime begin, DateTime einde)
        //{
           // //  bool overlap = a.start < b.end && b.start < a.end;
            //return Context.Set<Reservering>()
              //  .Include(r => r.Limo)
                //.Where(r => r.Limo.WagenId == limo.WagenId &&
                  //          r.Periode.Begin <= begin &&
                    //        r.Periode.Einde >= einde);
							
							
			//hier nakijken of er limo's binnen een bepaalde periode vallen dmv de overlap-bool			
				
        //}

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
                                    .Include(r => r.Periode)
                                    .Include(r => r.EindLocatie)
                                    .Where(r => r.Limo == limo)
                                    .Where(r => r.Periode.Einde <= periodeBegin)
                                    .OrderBy(r => r.Periode.Einde)
                                    .ToList();

            //return res.Last();
            return res.FirstOrDefault();
        }

        public Reservering VolgendeReserveringVoorLimo(Limo limo, DateTime periodeEinde)
        {
            //ordening ok?
            List<Reservering> res = Context.Set<Reservering>()
                .Include(r => r.Limo)
                .Include(r => r.Periode)
                .Include(r => r.StartLocatie)
                .Where(r => r.Limo == limo)
                .Where(r => r.Periode.Begin >= periodeEinde)
                .OrderBy(r => r.Periode.Begin)
                .ToList();

            //return res.First();
            return res.FirstOrDefault();
        }


        public void Nieuw(Reservering res)
        {
            Context.Set<Reservering>().Add(res);
            Context.SaveChanges();
        }


    }
}
