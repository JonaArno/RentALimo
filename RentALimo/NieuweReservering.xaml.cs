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

            var repo = new OphaalRepo();

            foreach (var kl in repo.OphalenKlanten())
            {
                KlantComboBox.Items.Add(kl);
            }

            foreach (var li in repo.OphalenAlleLimos())
            {
                BeschikbareWagensListView.Items.Add(li);
            }

            ArrangementComboBox.ItemsSource = Enum.GetValues(typeof(Arrangement));
            StartLocatieComboBox.ItemsSource = Enum.GetValues(typeof(Locatie));
            EindLocatieComboBox.ItemsSource = Enum.GetValues(typeof(Locatie));


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
                var gegevens = new InvalidOperationException("Gelieve alle gegevens in te vullen");
                MessageBox.Show(gegevens.Message);
            }
            else
            {
                DateTime invoerStartDatum = (DateTime)StartDatePicker.SelectedDate;
                DateTime invoerEindDatum = (DateTime)EndDatePicker.SelectedDate;

                var start = new DateTime(invoerStartDatum.Year, invoerStartDatum.Month, invoerStartDatum.Day,
                    Convert.ToInt32(StartUurComboBox.SelectionBoxItem), 0, 0);
                var eind = new DateTime(invoerEindDatum.Year, invoerEindDatum.Month, invoerEindDatum.Day,
                    Convert.ToInt32(EindUurComboBox.SelectionBoxItem), 0, 0);

                // Parameters ophalen uit selecties
                Klant kl = (Klant)KlantComboBox.SelectionBoxItem;
                Arrangement arr = (Arrangement)ArrangementComboBox.SelectedItem;
                Locatie startLocatie = (Locatie)StartLocatieComboBox.SelectedItem;
                Locatie eindLocatie = (Locatie)EindLocatieComboBox.SelectedItem;

                var rb = new ReserveringBouwer(new ReserveringsRepo());

                rb.Klant = kl;
                rb.Periode = new Periode(start, eind);
                rb.Arrangement = arr;
                rb.StartLocatie = startLocatie;
                rb.EindLocatie = eindLocatie;
                rb.Limo = (Limo)BeschikbareWagensListView.SelectedItem;

                try
                {
                    rb.Maak();
                    MessageBox.Show("Reservatie succesvol aangemaakt");
                }
                catch (InvalidOperationException exception)
                {
                    MessageBox.Show(exception.Message);
                }

            }


        }

        private void BerekenPrijsInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Limo li = (Limo)BeschikbareWagensListView.SelectedItem;

            if (li == null)
            {
                var invOp = new InvalidOperationException("Gelieve een limo mee te geven");
                MessageBox.Show(invOp.Message);

            }
            else
            {
                if (!li.MogelijkBinnenArrangement((Arrangement)ArrangementComboBox.SelectionBoxItem))
                {
                    var foutTekst = new InvalidOperationException("Arrangement niet mogelijk voor de geselecteerde wagen");
                    MessageBox.Show(foutTekst.Message);
                }

                else
                {
                    //REPO
                    ReserveringsRepo repo = new ReserveringsRepo();


                    if (BeschikbareWagensListView.SelectedItem != null &&
                        StartDatePicker.SelectedDate != (default(DateTime)) &&
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

                        //hier ophalen klant
                        Klant kl = (Klant)KlantComboBox.SelectionBoxItem;
                        EventingKorting evtKorting = kl.KlantCategorie.EventingKorting;

                        if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null)
                        {
                            var dataLeeg = new InvalidOperationException("Gelieve geldige data mee te geven.");
                            MessageBox.Show(dataLeeg.Message);
                        }

                        else
                        {
                            DateTime invoerStartDatum = (DateTime)StartDatePicker.SelectedDate;
                            DateTime invoerEindDatum = (DateTime)EndDatePicker.SelectedDate;

                            var start = new DateTime(invoerStartDatum.Year, invoerStartDatum.Month, invoerStartDatum.Day,
                                Convert.ToInt32(StartUurComboBox.SelectionBoxItem), 0, 0);
                            var eind = new DateTime(invoerEindDatum.Year, invoerEindDatum.Month, invoerEindDatum.Day,
                                Convert.ToInt32(EindUurComboBox.SelectionBoxItem), 0, 0);


                            TimeSpan ts = eind - start;
                            if (ts.TotalHours > 11 || ts.TotalHours < 0)
                            {
                                var tsFoutCode = new InvalidOperationException("De einddatum van een reservatie moet steeds later vallen dan de startdatum en een reservatie kan niet langer dan 11 uur duren.");
                                MessageBox.Show(tsFoutCode.Message);
                            }

                            else
                            {
                                try
                                {
                                    //volgende twee lijnen ergens anders steken?
                                    var prb = new PrijsBerekening((Limo)BeschikbareWagensListView.SelectedItem, arr, evtKorting,
                                        repo.AantalReserveringenVoorKlantInJaar(kl, DateTime.Now.Year), start, eind);

                                    BedrExclBtwVoorEvtKrtValueLabel.Content = prb.PrijsInfo.BedragExclusiefBtwVoorEventingKorting;
                                    AangerekendeEvtKrtValueLabel.Content = prb.PrijsInfo.AangerekendeEventingKorting;
                                    BedrExclBtwNaEvtKrtValueLabel.Content = prb.PrijsInfo.BedragExclusiefBtwNaEventingKorting;
                                    BtwBedragValueLabel.Content = prb.PrijsInfo.BtwBedrag;
                                    TotaalbedragInclBtwValueLabel.Content = prb.PrijsInfo.TotaalTeBetalenBedrag;
                                }
                                catch (InvalidOperationException exception)
                                {
                                    MessageBox.Show(exception.Message);
                                }
                                
                            }
                        }
                    }

                    else
                    {
                        var foutTekst = new InvalidOperationException("Gelieve alle velden in te vullen");
                        MessageBox.Show(foutTekst.Message);
                    }
                }
            }
        }

    }
}
