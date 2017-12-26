using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentALimo.Business
{
    public class Adres
    {
        public int Id { get; set; }
        public string Straat { get; set; }
        public int PostCode { get; set; }
        public string Stad { get; set; }
        public string Land { get; set; }

        protected Adres() { }

        public Adres(string straat, int postCode, string stad, string land)
        {
            Straat = straat;
            PostCode = postCode;
            Stad = stad;
            Land = land;
        }
    }
}
