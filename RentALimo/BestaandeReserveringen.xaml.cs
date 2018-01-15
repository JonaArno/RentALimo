using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RentALimo.Business;
using RentALimo.EF;

namespace RentALimo
{
    /// <summary>
    /// Interaction logic for BestaandeReserveringen.xaml
    /// </summary>
    public partial class BestaandeReserveringen : Window
    {
        public Klant GeselecteerdeKlant { get; set; }
        public Limo GeselecteerdeLimo { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public Arrangement Arrangement { get; set; }


        public BestaandeReserveringen()
        {
            InitializeComponent();
        }

        private void OnKlantSelecterenClick(object sender, RoutedEventArgs e)
        {
            var klantenZoekenVenster = new Klanten(this);
            klantenZoekenVenster.ShowDialog();
        }

        private void OnBeschikbaarheidLimosControlerenClick(object sender, RoutedEventArgs e)
        {
            var limobeschikbaarheidVenster = new LimoBeschikbaarheid(this);
            limobeschikbaarheidVenster.ShowDialog();
        }

        private void OnAnnuleerClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnReserveringenOpzoekenClick(object sender, RoutedEventArgs e)
        {
            var repo = new ReserveringsRepo();

            var resResultaat = new ReserveringResultaat();

            if (GeselecteerdeKlant != null && GeselecteerdeLimo == null)
            {
                resResultaat.ReserveringenResultaatDataGrid.ItemsSource = repo.AlleReserveringenVoorKlant(GeselecteerdeKlant);
            }
            else if (GeselecteerdeKlant == null && GeselecteerdeLimo != null)
            {
                resResultaat.ReserveringenResultaatDataGrid.ItemsSource =
                    repo.ReserveringenVoorLimoTussenDataBinnenArrangement(GeselecteerdeLimo,StartDatum,EindDatum,Arrangement);
            }
            else if (GeselecteerdeKlant != null && GeselecteerdeLimo != null)
            {
                resResultaat.ReserveringenResultaatDataGrid.ItemsSource = repo.ReserveringenMetAlleGegevens(GeselecteerdeKlant,GeselecteerdeLimo,StartDatum,EindDatum,Arrangement);
            }
            else if (GeselecteerdeKlant == null && GeselecteerdeLimo == null)
            {
                resResultaat.ReserveringenResultaatDataGrid.ItemsSource = repo.OphalenAlleReserveringen();
            }

                resResultaat.ShowDialog();
        }
    }
}
