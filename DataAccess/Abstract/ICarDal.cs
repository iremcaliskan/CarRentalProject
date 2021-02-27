using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        /*
        Car GetCarById(int carId); // carId'ye göre bir araba listelenir.
        List<Car> GetAll(); // Tüm arabaları listeleme.
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
        */
        List<CarDetailDto> GetCarDetails(); // Özel metodu
        CarDetailDto GetCarDetailsById(Expression<Func<CarDetailDto, bool>> filter); // Özel metodu

    }
}
