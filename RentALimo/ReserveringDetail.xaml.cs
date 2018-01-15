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
    /// Interaction logic for ReserveringDetail.xaml
    /// </summary>
    public partial class ReserveringDetail : Window
    {
        public Reservering Reservering { get; set; }

        public ReserveringDetail(Reservering res)
        {
            InitializeComponent();
            Reservering = res;
            KlantValueLabel.Content = Reservering.Klant;
            StartDatumValueLabel.Content = Reservering.Periode.Begin;
            EindDatumValueLabel.Content = Reservering.Periode.Einde;
            TotaleDuurValueLabel.Content = Reservering.Periode.Duur;
            StartLocatieValueLabel.Content = Reservering.StartLocatie;
            EindLocatieValueLabel.Content = Reservering.EindLocatie;
            LimoValueLabel.Content = Reservering.Limo;
            ArrangementValueLabel.Content = Reservering.Arrangement;
            BedragVoorEventingKortingValueLabel.Content = Reservering.PrijsInfo.BedragExclusiefBtwVoorEventingKorting;
            AangerekendeEventingKortingValueLabel.Content = Reservering.PrijsInfo.AangerekendeEventingKorting;
            BedragVoorBtwValueLabel.Content = Reservering.PrijsInfo.BedragExclusiefBtwNaEventingKorting;
            BtwBedragValueLabel.Content = Reservering.PrijsInfo.BtwBedrag;
            TotaleBedragValueLabel.Content = Reservering.PrijsInfo.TotaalTeBetalenBedrag;
            DagUrenValueLabel.Content = Reservering.PrijsInfo.AantalGewoneUren;
            NachtUrenValueLabel.Content = Reservering.PrijsInfo.AantalNachtUren;
            OverUrenValueLabel.Content = Reservering.PrijsInfo.AantalOverUren;
        }

        private void OnCloseWindowButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
