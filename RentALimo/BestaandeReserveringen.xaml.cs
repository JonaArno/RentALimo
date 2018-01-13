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

namespace RentALimo
{
    /// <summary>
    /// Interaction logic for BestaandeReserveringen.xaml
    /// </summary>
    public partial class BestaandeReserveringen : Window
    {
        public Klant GeselecteerdeKlant { get; set; }


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
    }
}
