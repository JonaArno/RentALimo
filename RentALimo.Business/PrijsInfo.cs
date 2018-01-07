using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentALimo.Business
{
    public class PrijsInfo
    {
        //uitwerking bedenken
        public decimal BedragExclusiefBtwVoorEventingKorting { get; set; }
        public decimal AangerekendeEventingKorting { get; set; }
        public decimal BedragExclusiefBtwNaEventingKorting { get; set; }
        public decimal BtwBedrag { get; set; }
        public decimal TotaalTeBetalenBedrag { get; set; }

        public int AantalGewoneUren = 0;
        public int AantalNachtUren = 0;
        public int AantalOverUren = 0;
    }
}
