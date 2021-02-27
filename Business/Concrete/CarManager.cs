using Business.Abstract;
using Business.Constants;
using Core.Results;
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

        public IResult Add(Car car)
        {
            if (car.CarName.Length >= 2 && car.DailyPrice > 0)
            {
                _iCarDal.Add(car);
                return new SuccessResult(Messages.Added);
            }
            else
            {
                return new ErrorResult(Messages.CarCanNotAdded);
            }
        }

        public IResult Update(Car car)
        {
            if (car.CarName.Length >= 2 && car.DailyPrice > 0)
            {
                _iCarDal.Update(car);
                return new SuccessResult(Messages.Updated);

            }
            else
            {
                return new ErrorResult(Messages.CarCanNotUpdated);
            }
        }

        public IResult Delete(Car car)
        {
            try
            {
                _iCarDal.Delete(car);
                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception)
            {
                throw new Exception("A system error occurs on deletion!");
            }
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_iCarDal.GetAll(), Messages.GetAll);
        }

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_iCarDal.Get(c => c.CarId == carId), Messages.GetCarByCarId);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_iCarDal.GetAll(c => c.BrandId == brandId), Messages.GetCarsByBrandId);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_iCarDal.GetAll(c => c.ColorId == colorId), Messages.GetCarsByColorId);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_iCarDal.GetCarDetails(), Messages.GetCarsWithDetails);
        }

        public IDataResult<CarDetailDto> GetCarDetailsById(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(_iCarDal.GetCarDetailsById(c => c.CarId == carId), Messages.GetCarDetailsById);
        }
    }
}
