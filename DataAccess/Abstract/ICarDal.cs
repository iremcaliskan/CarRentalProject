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
        Car GetCarById(int carId);
        List<Car> GetAll();
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
        */

        //List<CarDetailDto> GetCarDetails();

        // Special Methods:
        List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null);

        CarDetailDto GetCarDetailsById(Expression<Func<CarDetailDto, bool>> filter);
        List<CarImagesDto> GetCarImageDetails(Expression<Func<CarImagesDto, bool>> filter = null);
    }
}
