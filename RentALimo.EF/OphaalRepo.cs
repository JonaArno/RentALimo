using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using RentALimo.Business;
using System.Data.Entity;

namespace RentALimo.EF
{
    public class OphaalRepo : IOphaalRepo
    {
        public RentALimoContext Context = new RentALimoContext();

        public IEnumerable<Klant> OphalenKlanten()
        {
            return Context.Set<Klant>()
                .Include(kl => kl.Adres)
                .Include(kl => kl.KlantCategorie)
                //noodzakelijk?
                .Include(kl => kl.KlantCategorie.EventingKorting)
                .Include(kl => kl.KlantCategorie.EventingKorting)
                .ToList();
        }

        public IEnumerable<Klant> OphalenKlantenMetFilter(string filter)
        {
            return Context.Set<Klant>()
                .Where(kl => kl.Naam.Contains(filter))
                .Include(kl => kl.Adres)
                .Include(kl => kl.KlantCategorie)
                .ToList();
        }

        public IEnumerable<Limo> OphalenAlleLimos()
        {
            return Context.Set<Limo>()
                .ToList();
        }

        public Reservering OphalenLaatsteReserveringVanLimo(Limo limo, DateTime startDatum)
        {
            return Context.Set<Reservering>()
                .Include(res => res.Limo)
                .Where(res => res.Limo.WagenId == limo.WagenId &&
                              res.Periode.Einde < startDatum)
                .ToList()
                    .LastOrDefault();
        }

        public IEnumerable<Limo> OphalenLimosMetFilters(DateTime startDateTime, DateTime eindDateTime, Locatie startLocatie,
            Arrangement arrangement)
        {
            List<Limo> returnWaarde = OphalenBeschikbareLimosInPeriode(startDateTime, eindDateTime);
            List<Limo> copyOfReturnWaarde = new List<Limo>(returnWaarde);



            foreach (var limo in copyOfReturnWaarde)
            {
                Reservering laatstReserveringVanLimo = OphalenLaatsteReserveringVanLimo(limo, startDateTime);
                DateTime aangepastStartUur = DateTime.Now;

                if (laatstReserveringVanLimo != null)
                {
                    if (laatstReserveringVanLimo.EindLocatie == startLocatie)
                        aangepastStartUur = startDateTime.AddHours(-4);
                    else if (laatstReserveringVanLimo.EindLocatie != startLocatie)
                        aangepastStartUur = startDateTime.AddHours(-6);

                    if (aangepastStartUur <= laatstReserveringVanLimo.Periode.Einde)
                        returnWaarde.Remove(limo);
                }
            }

            List<Limo> secondCopyOfReturnWaarde = new List<Limo>(copyOfReturnWaarde);
            
            foreach (var limo in secondCopyOfReturnWaarde)
            {
                if (!limo.MogelijkBinnenArrangement(arrangement))
                    returnWaarde.Remove(limo);
            }

            return returnWaarde;

        }


        public List<Limo> OphalenBeschikbareLimosInPeriode(DateTime begin, DateTime einde)
        {
            List<Limo> alleLimos = Context.Set<Limo>().ToList();
            List<Limo> gereserveerdeLimos = new List<Limo>();

            List<Reservering> reserveringenInPeriode = Context.Set<Reservering>()
                                                            .Include(res => res.Limo)
                                                            .Where(res => res.Periode.Begin >= begin && res.Periode.Einde <= einde)
                                                            .ToList();

            foreach (var res in reserveringenInPeriode)
            {
                gereserveerdeLimos.Add(res.Limo);
            }

            foreach (var limo in gereserveerdeLimos)
            {
                if (alleLimos.Contains(limo))
                {
                    alleLimos.Remove(limo);
                }
            }
            return alleLimos;
        }
    }
}
