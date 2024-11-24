using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Blocking { get; set; } = false;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public int? RoleId { get; set; } 
        public Role? Role{ get; set; } 
    }
}
