using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class CarRentalDBContext : DbContext
    {
        // override On...
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CarRentalDB;Trusted_Connection=true");
        }

        public DbSet<Car> Cars { get; set; } // Projenin Car sınıfı, Cars Tablosu ile bağlıdır.
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }

        /* EntityFrameworkCore.SqlServer Nuget paketi indirildi,
         * Context sınıfı oluşturulup, projenin kullanacağı Db belirtildi.
         * Projedeki sınıflar, entityler ile Db'deki tablolar bağlandı.
         */
    }
}
