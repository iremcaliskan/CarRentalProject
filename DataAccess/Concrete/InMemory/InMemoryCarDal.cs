using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>()
            {
                new Car() {CarId = 1, BrandId = 1, ColorId = 1, CarName = "A", Description = "Otomatik", ModelYear = "2010", DailyPrice = 89999.99m},
                new Car() {CarId = 2, BrandId = 1, ColorId = 2, CarName = "B", Description = "Otomatik", ModelYear = "2012", DailyPrice = 129999.99m},
                new Car() {CarId = 3, BrandId = 1, ColorId = 3, CarName = "C", Description = "Manuel", ModelYear = "2010", DailyPrice = 79999.99m},
                new Car() {CarId = 4, BrandId = 2, ColorId = 1, CarName = "D", Description = "Otomatik", ModelYear = "2010", DailyPrice = 259999.99m},
                new Car() {CarId = 5, BrandId = 2, ColorId = 2, CarName = "E", Description = "Otomatik ", ModelYear = "2010", DailyPrice = 281000}
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car); // Listeye ekleme
        }
        public void Update(Car car)
        { // Güncellenecek olan araba kullanıcıdan gelen araba ile aynı Id'ye sahip ise adreslerini eşitle.
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            // Gelen özellikleri güncellenecek arabaya atama:
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.CarName = car.CarName;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;
        }

        public void Delete(Car car)
        { // Silinecek araba kullanıcıdan gelen araba ile aynı Id'ye sahip ise adreslerini eşitle.
            Car carToDelete = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            // Ve listeden o arabayı sil.
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public Car GetCarById(int carId)
        {
            return _cars.SingleOrDefault(c => c.CarId == carId); // Tek bir eleman dönecek bir liste değil!
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException(); // EF'ye geçildi, sadece bozulmaması için gerekli implementasyonlar yapıldı.
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }
    }
}
