using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public class Reservation
    {
        public int Id { get; set; }
        public bool IsConfirm { get; set; } = false;
        public int? HotelId { get; set; }
        public Hotels? Hotel { get; set; }
        public int? TourId { get; set; }
        public Tours? Tour { get; set; }
        public DateOnly ReservationDate { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
