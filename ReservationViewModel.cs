using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public class ReservationViewModel
    {
        public required string Name { get; set; }
        public required string Type { get; set; }
        public bool Confirm { get; set; }
        public DateOnly ReservationDate {  get; set; }  
    }
}
