using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentALimo.Business
{
    public class PrijsInfo
    {
        public decimal BedragExclusiefBtwVoorEventingKorting { get; set; }
        public decimal AangerekendeEventingKorting { get; set; }
        public decimal BedragExclusiefBtwNaEventingKorting { get; set; }
        public decimal BtwBedrag { get; set; }
        public decimal TotaalTeBetalenBedrag { get; set; }

        public int AantalGewoneUren { get; set; }
        public int AantalNachtUren {get; set; }
        public int AantalOverUren { get; set; }
    }
}
