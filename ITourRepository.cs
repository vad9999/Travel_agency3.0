using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public interface ITourRepository
    {
        List<Tours> GetAllTours();
        void AddTour(Tours tour);
        Tours GetTourById(int id);
        List<Tours> GetAllToursNonArchive();
        List<Tours> GetAllToursArchive();
        void UpdateTour(Tours tour);
        bool DoubleName(string name);
        List<object> DateCheckAndGetList();
    }
}
