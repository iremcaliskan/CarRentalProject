using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll(); // Tüm arabaları listele
        Car GetById(int carId);
        List<Car> GetCarsByBrandId(int brandId);
        List<Car> GetCarsByColorId(int colorId);

        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);

        List<CarDetailDto> GetCarDetails();
    }
}
