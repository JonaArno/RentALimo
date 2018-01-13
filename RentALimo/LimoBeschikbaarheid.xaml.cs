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
        IOphaalRepo _repo = new OphaalRepo();

        public BestaandeReserveringen BestaandeReserveringen { get; set; }

        public LimoBeschikbaarheid(BestaandeReserveringen bestaandeReserveringen)
        {
            InitializeComponent();
            BestaandeReserveringen = bestaandeReserveringen;


            ArrangementComboBox.ItemsSource = Enum.GetValues(typeof(Arrangement));
            StartLocatieComboBox.ItemsSource = Enum.GetValues(typeof(Locatie));

            //ophalen adhv OphalenLimosMetFilters 
            //met defaultgeselecteerde waarden


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
