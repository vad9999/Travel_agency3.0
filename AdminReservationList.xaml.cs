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

namespace Travel_agency
{
    public partial class AdminReservationList : Window
    {
        public AdminReservationList()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            IReservationRepository reservationRepository = new ReservationRepository(new AppDbContext());

            List<ReservationViewModel> reservations = reservationRepository.AdminReservationViewModel();
            ReservationsListView.ItemsSource = reservations;
        }

        private void ReservationTrueButton_Click(object sender, RoutedEventArgs e)
        {
            IReservationRepository reservationRepository = new ReservationRepository(new AppDbContext());

            var reservation = (ReservationViewModel)ReservationsListView.SelectedItem;
            if (reservation != null)
            {
                Reservation res = reservationRepository.GetReservationById(reservation.Id);
                res.IsConfirm = true;
                reservationRepository.UpdateReservation(res);
            }
            LoadData();
        }
    }
}
