using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _iCarDal; // Veri erişim yöntemlerinin her birini tutabilecek referans
        public CarManager(ICarDal iCarDal)
        { // Oluşturma anında bir veri erişim yöntemi istiyor.
            _iCarDal = iCarDal;
        }

        public void Add(Car car)
        {
            if (car.CarName.Length >= 2 && car.DailyPrice > 0)
            {
                _iCarDal.Add(car);
            }
            else
            {
                Console.WriteLine("The car description must contain at least two characters!\n" +
                    "The daily price of the car must be greater than zero!");
            }
        }
        public void Update(Car car)
        {
            if (car.CarName.Length >= 2 && car.DailyPrice > 0)
            {
                _iCarDal.Update(car);
            }
            else
            {
                Console.WriteLine("The car description must contain at least two characters!/n" +
                    "The daily price of the car must be greater than zero!");
            }
        }

        public void Delete(Car car)
        {
            _iCarDal.Delete(car);
        }

        public List<Car> GetAll()
        {
            return _iCarDal.GetAll();
        }

        public Car GetById(int carId)
        {
            return _iCarDal.Get(c => c.CarId == carId);
        }

        public List<Car> GetCarsByBrandId(int brandId)
        {
            return _iCarDal.GetAll(c => c.BrandId == brandId);
        }

        public List<Car> GetCarsByColorId(int colorId)
        {
            return _iCarDal.GetAll(c => c.ColorId == colorId);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            return _iCarDal.GetCarDetails();
        }        
    }
}
