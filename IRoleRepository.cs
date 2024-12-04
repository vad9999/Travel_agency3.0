using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public interface IRoleRepository
    {
        List<Role> GetAll();
        void AddRoles();
        int CustomerId();
    }
}
