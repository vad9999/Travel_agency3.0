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
        public DbSet<Role> Role => Set<Role>();
        public AppDbContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = myDb.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role) // Один пользователь связан с одной ролью
                .WithMany(r => r.Users) // Одна роль может иметь много пользователей
                .HasForeignKey(u => u.RoleId) // Связь через внешний ключ RoleId
                .OnDelete(DeleteBehavior.Restrict); // При удалении роли, связь с пользователями не удаляется

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Hotel)
            .WithMany()
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Tour)
                .WithMany()
                .HasForeignKey(r => r.TourId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
