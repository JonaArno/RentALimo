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
    /// Interaction logic for Klanten.xaml
    /// </summary>
    public partial class Klanten : Window
    {
        public BestaandeReserveringen BestaandeReserveringen { get; set; }

        public Klanten(BestaandeReserveringen bestres)
        {
            InitializeComponent();
            BestaandeReserveringen = bestres;
            var repo = new OphaalRepo();
            KlantenOverzicht.ItemsSource = repo.OphalenKlantenMetFilter("");          
        }

        private void ZoekKlantTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var repo = new OphaalRepo();
            KlantenOverzicht.ItemsSource = repo.OphalenKlantenMetFilter(ZoekKlantTextBox.Text);
        }

        private void OnBevestigGeselecteerdeKlantClick(object sender, RoutedEventArgs e)
        {
            BestaandeReserveringen.GeselecteerdeKlant = (Klant) KlantenOverzicht.SelectedItem;
            BestaandeReserveringen.KlantLabel.Foreground = Brushes.Black;
            BestaandeReserveringen.KlantValueLabel.Content = BestaandeReserveringen.GeselecteerdeKlant;
            this.Close();
        }

        private void OnAnnuleerKlantVensterClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
