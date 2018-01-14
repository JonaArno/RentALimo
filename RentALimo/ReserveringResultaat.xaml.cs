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
    /// Interaction logic for ReserveringResultaat.xaml
    /// </summary>
    public partial class ReserveringResultaat : Window
    {
        public ReserveringResultaat()
        {
            InitializeComponent();
        }

        private void OnAnnuleerButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnDetailReserveringClick(object sender, RoutedEventArgs e)
        {
            var repo = new ReserveringsRepo();
            Reservering geselecteerdeReservering =
                repo.OphalenReservering((Reservering) ReserveringenResultaatDataGrid.SelectedItem);

            var detail = new ReserveringDetail(geselecteerdeReservering);
            detail.ShowDialog();
        }
    }
}
