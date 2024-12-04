using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Travel_agency
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddAdmin()
        {
            var adminRole = _context.Role.First(r => r.Name == "Administrator");
            _context.Users.Add(new User { Name = "admin", RoleId = adminRole.Id, Email = "admin", Password = GetHash("admin"), Blocking = false });
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool GetBlockUser(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email).Blocking;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public string GetHash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool CheckUser(string email)
        {
            var users = GetAllUsers();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
