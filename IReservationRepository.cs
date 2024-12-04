using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public interface IReservationRepository
    {
        void AddReservarion(Reservation reserv);
        void UpdateReservation(Reservation reserv);
        List<ReservationViewModel> UserReservationViewModel(int userId);
        List<ReservationViewModel> AdminReservationViewModel();
        Reservation GetReservationById(int id);
    }
}
