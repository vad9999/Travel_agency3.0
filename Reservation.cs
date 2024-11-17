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
        public int TourOrHotelId { get; set; }
        public string Name { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string Type { get; set; } = null!;
        public bool IsConfirm { get; set; }
        public DateOnly ReservationDate { get; set; }
    }
}
