using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Travel_agency
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Tours> Tours => Set<Tours>();
        public DbSet<Hotels> Hotels => Set<Hotels>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public AppDbContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = myDb.db");
        }
    }
}
