using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddReservarion(Reservation reserv)
        {
            _context.Reservations.Add(reserv);
            _context.SaveChanges();
        }

        public void UpdateReservation(Reservation reserv)
        {
            _context.Reservations.Update(reserv);
            _context.SaveChanges();
        }
    
        public Reservation GetReservationById(int id)
        {
            return _context.Reservations.Find(id);
        }

        public List<ReservationViewModel> UserReservationViewModel(int userId)
        {
            var bookings = _context.Reservations
                .Include(b => b.User)
                .Include(b => b.Hotel)
                .Include(b => b.Tour)
                .Where(b => b.UserId == userId)
                .Select(b => new ReservationViewModel
                {
                    Name = b.Hotel != null ? b.Hotel.Name : b.Tour.Name,
                    Type = b.Hotel != null ? b.Hotel.Type : b.Tour.Type,
                    ReservationDate = b.ReservationDate,
                    IsConfirm = b.IsConfirm
                })
                .ToList();

            return bookings; 
        }

        public List<ReservationViewModel> AdminReservationViewModel()
        {
            var bookings = _context.Reservations
                .Include(b => b.User)
                .Include(b => b.Hotel)
                .Include(b => b.Tour)
                .Select(b => new ReservationViewModel
                {
                    Id = b.Id,
                    UserEmail = b.User.Email,
                    Name = b.Hotel != null ? b.Hotel.Name : b.Tour.Name,
                    Type = b.Hotel != null ? b.Hotel.Type : b.Tour.Type,
                    ReservationDate = b.ReservationDate,
                    IsConfirm = b.IsConfirm
                })
                .ToList();

            return bookings;
        }
    }
}
