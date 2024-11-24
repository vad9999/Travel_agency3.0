using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Travel_agency
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Role> GetAll()
        {
            return _context.Role.ToList();
        }

        public void AddRoles()
        {
            _context.Role.AddRange(new Role { Name = "Administrator" },
                    new Role { Name = "Customer" });
            _context.SaveChanges();
        }

        public int CustomerId()
        {
            return _context.Role.First(r => r.Name == "Customer").Id;
        }

        public int AdminId()
        {
            return _context.Role.First(r => r.Name == "Administrator").Id;
        }
    }
}
