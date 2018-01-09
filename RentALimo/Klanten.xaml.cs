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
using RentALimo.EF;

namespace RentALimo
{
    /// <summary>
    /// Interaction logic for Klanten.xaml
    /// </summary>
    public partial class Klanten : Window
    {
        public Klanten()
        {
            InitializeComponent();
            var repo = new OphaalRepo();

            KlantenOverzicht.ItemsSource = repo.OphalenKlanten();
            
        }

        private void ZoekKlantTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var repo = new OphaalRepo();
            KlantenOverzicht.ItemsSource = repo.OphalenKlantenMetFilter(ZoekKlantTextBox.Text);
        }
    }
}
