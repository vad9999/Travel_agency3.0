using Microsoft.EntityFrameworkCore.Diagnostics;
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
    public partial class UserReservations : Window
    {
        public UserReservations()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            IReservationRepository ReservationRepository = new ReservationRepository(new AppDbContext());

            ReservationListView.ItemsSource = ReservationRepository.UserReservationViewModel(Session.CurrentUser.Id);
        }
    }
}
