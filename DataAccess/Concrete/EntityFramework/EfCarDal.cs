using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalDBContext>, ICarDal
    {
        /* Her sınıf için tekrarlanan aynı veri erişim yöntemine sahip  bu metotlar Base Class'tan inherit edilerek halledilir.
        public void Add(Car entity)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                var addedCar = context.Entry(entity); // Referansı bul
                addedCar.State = EntityState.Added; // İşlemi ayarla
                context.SaveChanges(); // Kaydet
            }
        }
        public void Update(Car entity)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                var updatedCar = context.Entry(entity); // Referansı bul
                updatedCar.State = EntityState.Modified; // İşlemi ayarla
                context.SaveChanges(); // Kaydet
            }
        }

        public void Delete(Car entity)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                var deletedCar = context.Entry(entity);
                deletedCar.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                return context.Set<Car>().SingleOrDefault(filter); 
                //DbSet Car sınıfını yani Cars Tablosunu seç, predicate ifadeyi uygula gelen kaydı(record) döndür.
            }
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                return filter == null // filtre null mı?
                    ? context.Set<Car>().ToList() // null ise DbSet Car sınıfını yani Cars Tablosunu seç, liste olarak döndür
                    : context.Set<Car>().Where(filter).ToList(); // null değil ise DbSet Car sınıfını yani Cars Tablosunu seç,
                // filtreyi yani predicate, lamda ifadesini koşulunu sağlayanları seç(Where), listede topla ve döndür

            }
        }
        */
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                var result = from c in context.Cars // Cars tablosunu seç
                             join b in context.Brands // Brands tablosu ile birleştir, ilişkisel verileri varsa
                             on c.BrandId equals b.BrandId
                             join clr in context.Colors // Colors tablosu ile birleştir, ilişkisel verileri varsa
                             on c.ColorId equals clr.ColorId
                             select new CarDetailDto() // Aracın özelliklerini göstermek için oluşturularn veri aktarım objesine hangi tablonun hangi kısmı kullanılacaksa ata
                             {
                                 CarName = c.CarName, BrandName = b.BrandName, ColorName = clr.ColorName, DailyPrice = c.DailyPrice
                             };

                return result.ToList(); // IQueryable.ToList()
            }
        }

        public CarDetailDto GetCarDetailsById(Expression<Func<CarDetailDto, bool>> filter)//int id
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join clr in context.Colors
                             on c.ColorId equals clr.ColorId
                             select new CarDetailDto()
                             {  
                                 CarId = c.CarId, CarName = c.CarName, BrandName = b.BrandName, ColorName = clr.ColorName, DailyPrice = c.DailyPrice
                             };

                return result.SingleOrDefault(filter);
            }
        }
    }
}
