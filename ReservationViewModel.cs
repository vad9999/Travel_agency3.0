using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public required string Name { get; set; }
        public required string Type { get; set; }
        public DateOnly ReservationDate {  get; set; }
        public bool IsConfirm { get; set; }
    }
}
