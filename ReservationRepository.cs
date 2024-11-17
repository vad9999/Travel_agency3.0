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

        public List<Reservation> GetAllReservarions()
        {
            return _context.Reservations.ToList();
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

        public List<Reservation> UserReservation(string userEmail)
        {
            List<Reservation> reservations = GetAllReservarions();
            List<Reservation> userReservations = new List<Reservation>();
            for(int i = 0;  i < reservations.Count; i++)
            {
                if (reservations[i].UserEmail == userEmail)
                {
                    userReservations.Add(reservations[i]);
                }
            }
            return userReservations;
        }
    }
}
