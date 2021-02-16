using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new Car() {CarId = 1, BrandId = 1, ColorId = 1, Description = "Otomatik", ModelYear = "2010", DailyPrice = 89999.99m},
                new Car() {CarId = 2, BrandId = 1, ColorId = 2, Description = "Otomatik", ModelYear = "2012", DailyPrice = 129999.99m},
                new Car() {CarId = 3, BrandId = 1, ColorId = 3, Description = "Manuel", ModelYear = "2010", DailyPrice = 79999.99m},
                new Car() {CarId = 4, BrandId = 2, ColorId = 1, Description = "Otomatik", ModelYear = "2010", DailyPrice = 259999.99m},
                new Car() {CarId = 5, BrandId = 2, ColorId = 2, Description = "Otomatik ", ModelYear = "2010", DailyPrice = 281000}
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
    }
}
