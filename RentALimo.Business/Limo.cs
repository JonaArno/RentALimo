using System;

namespace RentALimo.Business
{
    public class Limo
    {
        protected Limo() { }

        public Limo(string merk, string type, string kleur, WagenPrijs wagenPrijs)
        {
            Merk = merk;
            Type = type;
            Kleur = kleur;
            WagenPrijs = wagenPrijs;
        }

        private int WagenId { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Kleur { get; set; }
        public WagenPrijs WagenPrijs { get; set; }

        public bool MogelijkBinnenArrangement(Arrangement arr)
        {
            //controle inbouwen voor mocht arrangement niet bestaan voor wagen?
            //dan moet er niet met nullen gewerkt worden

            if (WagenPrijs.PrijsOverzicht[arr] != 0)
            {
                return true;
            }
            else return false;
        }
    }
}