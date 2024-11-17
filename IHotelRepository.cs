using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_agency
{
    public interface IHotelRepository
    {
        List<Hotels> GetAllHotels();
        void AddHotel(Hotels hotel);
        Hotels GetHotelById(int id);
        void DeleteHotel(int id);
        List<Hotels> GetAllHotelsNonArchive();
        List<Hotels> GetAllHotelsArchive();
        void UpdateHotel(Hotels hotel);
        bool DoubleName(string name);
    }
}
