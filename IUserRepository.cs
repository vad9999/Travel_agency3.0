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
        User GetUserById(int id);
        void AddUser(User user);
        void AddAdmin();
        void UpdateUser(User user);
        void DeleteUser(int id);
        bool GetBlockUser(string email);
        User GetUserByEmail(string email);
        string GetHash(string rawData);
        bool CheckUser(string email);
    }
}
