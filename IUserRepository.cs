using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        void AddAdmin();
        void UpdateUser(User user);
        bool GetBlockUser(string email);
        User GetUserByEmail(string email);
        string GetHash(string rawData);
        bool CheckUser(string email);
    }
}
