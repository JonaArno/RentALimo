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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RentALimo.EF;
using RentALimo.Populeer;

namespace RentALimo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //var pop = new Populator(new PopuleerRepo());
            //pop.Populeer();
            InitializeComponent();
            var context = new RentALimoContext();
        }

        private void NieuweReserveringButton_Click(object sender, RoutedEventArgs e)
        {
            var nr = new NieuweReservering();
            nr.ShowDialog();
        }

        private void BestaandeReserveringButton_Click(object sender, RoutedEventArgs e)
        {
            var br = new BestaandeReserveringen();
            br.ShowDialog();
        }

        private void BeschikbaarheidLimosButton_Click(object sender, RoutedEventArgs e)
        {
            var lb = new LimoBeschikbaarheid();
            lb.ShowDialog();
        }

        private void KlantenButton_Click(object sender, RoutedEventArgs e)
        {
            var kl = new Klanten();
            kl.ShowDialog();
        }
    }
}
