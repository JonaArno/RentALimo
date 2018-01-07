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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RentALimo.Business;
using RentALimo.EF;

namespace RentALimo
{
    /// <summary>
    /// Interaction logic for NieuweReservering.xaml
    /// </summary>
    public partial class NieuweReservering : Window
    {
        public NieuweReservering()
        {
            InitializeComponent();

            //dit moet ergens anders
            //var rr = new ReserveringsRepo();
            //var rb = new ReserveringBouwer(rr);

            //using (var rr = new ReserveringsRepo())
            //{
            //    using (var rb = new ReserveringBouwer(rr))
            //    {
            //        foreach (var kl in rr.OphalenKlanten())
            //        {
            //            KlantComboBox.Items.Add(kl);
            //        }
            //    }

            //}

            var repo = new OphaalRepo();

            foreach (var kl in repo.OphalenKlanten())
            {
                KlantComboBox.Items.Add(kl);
            }

            foreach (var li in repo.OphalenAlleLimos())
            {
                BeschikbareWagensListView.Items.Add(li);
            }


        }

        private void AnnuleerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaakButton_Click(object sender, RoutedEventArgs e)
        {

            //aanmaken dateTimes
            //moet hieronder niet met (default)DateTime gewerkt worden?
            if (StartDatePicker.Text == "" || EndDatePicker.Text == "")
            {
                throw new InvalidOperationException("Gelieve alle gegevens in te vullen");
            }
            else
            {
                DateTime invoerStartDatum = (DateTime)StartDatePicker.SelectedDate;
                DateTime invoerEindDatum = (DateTime)EndDatePicker.SelectedDate;

                var start = new DateTime(invoerStartDatum.Year, invoerStartDatum.Month, invoerStartDatum.Day,
                    (int.Parse(StartUurComboBox.SelectionBoxItemStringFormat)), 0, 0);
                var eind = new DateTime(invoerEindDatum.Year, invoerEindDatum.Month, invoerEindDatum.Day,
                    (int.Parse(EindUurComboBox.SelectionBoxItemStringFormat)), 0, 0);

                //klant 
                Klant kl = (Klant)KlantComboBox.SelectionBoxItem;


                //arrangement toewijzen
                Arrangement arr = Arrangement.Airport;
                if (ArrangementComboBox.Text == "Business")
                    arr = Arrangement.Business;
                else if (ArrangementComboBox.Text == "Airport")
                    arr = Arrangement.Airport;
                else if (ArrangementComboBox.Text == "Wedding")
                    arr = Arrangement.Wedding;
                else if (ArrangementComboBox.Text == "Nightlife")
                    arr = Arrangement.NightLife;

                Locatie startLocatie = Locatie.Antwerpen;
                if (StartLocatieComboBox.Text != "Antwerpen")
                {
                    if (StartLocatieComboBox.Text == "Gent")
                        startLocatie = Locatie.Gent;
                    else if (StartLocatieComboBox.Text == "Brussel")
                        startLocatie = Locatie.Brussel;
                    else if (StartLocatieComboBox.Text == "Hasselt")
                        startLocatie = Locatie.Hasselt;
                    else if (StartLocatieComboBox.Text == "Charleroi")
                        startLocatie = Locatie.Hasselt;
                }

                Locatie eindLocatie = Locatie.Antwerpen;
                if (EindLocatieComboBox.Text != "Antwerpen")
                {
                    if (EindLocatieComboBox.Text == "Gent")
                        eindLocatie = Locatie.Gent;
                    else if (EindLocatieComboBox.Text == "Brussel")
                        eindLocatie = Locatie.Brussel;
                    else if (EindLocatieComboBox.Text == "Hasselt")
                        eindLocatie = Locatie.Hasselt;
                    else if (EindLocatieComboBox.Text == "Charleroi")
                        eindLocatie = Locatie.Hasselt;
                }



                var rb = new ReserveringBouwer(new ReserveringsRepo());

                rb.Klant = kl;
                rb.Periode = new Periode(start, eind);
                rb.Arrangement = arr;
                rb.StartLocatie = startLocatie;
                rb.EindLocatie = eindLocatie;
                rb.Limo = (Limo)BeschikbareWagensListView.SelectedItem;


                rb.Maak();



            }


        }

        private void BerekenPrijsInfoButton_Click(object sender, RoutedEventArgs e)
        {
            //REPO
            ReserveringsRepo repo = new ReserveringsRepo();


            if (BeschikbareWagensListView.SelectedItem != null && StartDatePicker.SelectedDate != (default(DateTime)) &&
                StartUurComboBox.SelectionBoxItem != null && EndDatePicker.SelectedDate != (default(DateTime)) &&
                EindUurComboBox.SelectionBoxItem != null && StartLocatieComboBox.SelectionBoxItem != null &&
                EindLocatieComboBox.SelectionBoxItem != null && ArrangementComboBox.SelectionBoxItem != null)
            {
                Arrangement arr = Arrangement.Airport;
                if (ArrangementComboBox.Text != "Airport")
                {
                    if (ArrangementComboBox.Text == "Business") arr = Arrangement.Business;
                    else if (ArrangementComboBox.Text == "Nightlife")
                        arr = Arrangement.NightLife;
                    else if (ArrangementComboBox.Text == "Wedding")
                        arr = Arrangement.Wedding;
                }

                Klant kl = (Klant) KlantComboBox.SelectionBoxItem;
                EventingKorting evtKorting = kl.KlantCategorie.EventingKorting;

                DateTime invoerStartDatum = (DateTime)StartDatePicker.SelectedDate;
                DateTime invoerEindDatum = (DateTime)EndDatePicker.SelectedDate;

                var start = new DateTime(invoerStartDatum.Year, invoerStartDatum.Month, invoerStartDatum.Day,
                    (int.Parse(StartUurComboBox.SelectionBoxItemStringFormat)), 0, 0);
                var eind = new DateTime(invoerEindDatum.Year, invoerEindDatum.Month, invoerEindDatum.Day,
                    (int.Parse(EindUurComboBox.SelectionBoxItemStringFormat)), 0, 0);
                

                var prb = new PrijsBerekening((Limo)BeschikbareWagensListView.SelectedItem, arr, evtKorting,repo.AantalReserveringenVoorKlantInJaar(kl,DateTime.Now.Year),start,eind);

                

            }


            else
            {
                var foutTekst = new InvalidOperationException("Gelieve alle velden in te vullen");
                MessageBox.Show(foutTekst.ToString());
            }

        }

    }
}
