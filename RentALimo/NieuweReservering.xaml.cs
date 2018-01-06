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
    /// Interaction logic for NieuweReservering.xaml
    /// </summary>
    public partial class NieuweReservering : Window
    {
        public NieuweReservering()
        {
            InitializeComponent();
            var rr = new ReserveringsRepo();
            var rb = new ReserveringBouwer(rr);

            foreach (var kl in rr.OphalenKlanten())
            {
                KlantComboBox.Items.Add(kl);
            }

        }

        private void AnnuleerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
