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
    /// Interaction logic for LimoBeschikbaarheid.xaml
    /// </summary>
    public partial class LimoBeschikbaarheid : Window
    {
        private IOphaalRepo _repo = new OphaalRepo();
        private IReserveringRepo _repo2 = new ReserveringsRepo();

        public BestaandeReserveringen BestaandeReserveringen { get; set; }

        public LimoBeschikbaarheid(BestaandeReserveringen bestaandeReserveringen)
        {
            InitializeComponent();
            BestaandeReserveringen = bestaandeReserveringen;


            StartLocatieComboBox.ItemsSource = Enum.GetValues(typeof(Locatie));
            ArrangementComboBox.ItemsSource = Enum.GetValues(typeof(Arrangement));

        }

        private void OnZoekBeschikbareLimosClick(object sender, RoutedEventArgs e)
        {
            //DateTime? startDatum = StartDatumDatePicker.SelectedDate;
            //DateTime? eindDatum = EindDatumDatePicker.SelectedDate;

            DateTime startDatum;
            DateTime eindDatum;

            if (StartDatumDatePicker.SelectedDate == null)
            {
                startDatum = DateTime.MinValue;
            }
            else
            {
                startDatum = (DateTime)StartDatumDatePicker.SelectedDate;
            }

            if (EindDatumDatePicker.SelectedDate == null)
            {
                eindDatum = DateTime.MaxValue;
            }
            else
            {
                eindDatum = (DateTime)EindDatumDatePicker.SelectedDate;
            }
            
            Locatie startLocatie = (Locatie)StartLocatieComboBox.SelectionBoxItem;
            Arrangement arrangement = (Arrangement) ArrangementComboBox.SelectionBoxItem;

           BeschikbareLimosListView.ItemsSource = _repo.OphalenLimosMetFilters(startDatum, eindDatum, startLocatie, arrangement);






        }

        private void OnSelecteerButtonClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnAnnuleerButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      


        //rekening houden met pauze
        //auto verwijderen uit lijst beschikbaar als te kort na vorige

    }
}
