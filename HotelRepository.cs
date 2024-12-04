using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Travel_agency
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _context;
        public HotelRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Hotels> GetAllHotels()
        {
            return _context.Hotels.ToList();
        }

        public void AddHotel(Hotels hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public Hotels GetHotelById(int id)
        {
            return _context.Hotels.Find(id);
        }

        public List<Hotels> GetAllHotelsNonArchive()
        {
            List<Hotels> hotel = GetAllHotels();
            List<Hotels> nonarchive = new List<Hotels>();
            if(hotel.Count > 0)
            {
                for (int i = 1; i <= hotel[hotel.Count - 1].Id; i++)
                {
                    if (GetHotelById(i) != null)
                    {
                        if (GetHotelById(i).IsArchive == false)
                        {
                            nonarchive.Add(GetHotelById(i));
                        }
                    }
                }
            }
            return nonarchive;
        }
        public List<Hotels> GetAllHotelsArchive()
        {
            List<Hotels> hotel = GetAllHotels();
            List<Hotels> archive = new List<Hotels>();
            if(hotel.Count > 0)
            {
                for (int i = 1; i <= hotel[hotel.Count - 1].Id; i++)
                {
                    if (GetHotelById(i) != null)
                    {
                        if (GetHotelById(i).IsArchive == true)
                        {
                            archive.Add(GetHotelById(i));
                        }
                    }
                }
            }
            return archive;
        }

        public void UpdateHotel(Hotels hotel)
        {
            _context.Hotels.Update(hotel);
            _context.SaveChanges();
        }

        public bool DoubleName(string name)
        {
            List<Hotels> hotel = GetAllHotels();
            if (hotel.Count > 0)
            {
                for (int i = 1; i <= hotel[hotel.Count - 1].Id; i++)
                {
                    if (GetHotelById(i) != null)
                    {
                        if (GetHotelById(i).Name == name)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
