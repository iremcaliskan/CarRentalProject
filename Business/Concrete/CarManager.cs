using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _iCarDal; // ICarDal is the reference type can hold all the data access techniques
        public CarManager(ICarDal iCarDal)
        {
            _iCarDal = iCarDal;
        }

        [SecuredOperation("car.add")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Add(Car car)
        {
            //var context = new ValidationContext<Car>(car); // Gelen Car için Car türünde Doğrulama Context'i oluştur.
            //CarValidator carValidator = new CarValidator(); // Ne ile doğrulanacak
            //var result = carValidator.Validate(context); // CarValidator contexti doğrulayacak

            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}
            /* Fluent Validation da yazılan doğrulama Class'ının kurallarını kullanarak ilgili nesneyi 
             * doğrulamanın en kötü yolu(spaghetti) budur.
             */

            // Tool haline getirilerek tekrar tekrar yazılması engellenecek.
            //ValidationTool.Validate(new CarValidator(), car); // AOP yapıldı.

            /* Log, CacheRemove, Performans, Transaction, Yetkilendirme gibi yönetimler hepsi burada 
             * olacağı için AOP kullanılması gerekir.
             */

            // Business Codes

            _iCarDal.Add(car);

            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("car.update")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Update(Car car)
        {
            /*
            if (car.CarName.Length >= 2 && car.DailyPrice > 0)
            {
                _iCarDal.Update(car);
                return new SuccessResult(Messages.Updated);

            }
            else
            {
                return new ErrorResult(Messages.CarCanNotUpdated);
            }
            */
            _iCarDal.Update(car);

            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("car.delete")]
        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(Car car)
        {
            try
            {
                _iCarDal.Delete(car);
                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception)
            {
                throw new Exception("An error occurs on deletion!");
            }
        }

        [CacheAspect]
        [PerformanceAspect(15)] // For Performance issue, if process time takes time longer than 15 seconds, warn me
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_iCarDal.GetAll(), Messages.GetAll);
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_iCarDal.Get(c => c.CarId == carId), Messages.GetCarByCarId);
        }

        [CacheAspect]
        [PerformanceAspect(15)]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_iCarDal.GetAll(c => c.BrandId == brandId), Messages.GetCarsByBrandId);
        }

        [CacheAspect]
        [PerformanceAspect(15)]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_iCarDal.GetAll(c => c.ColorId == colorId), Messages.GetCarsByColorId);
        }

        [CacheAspect]
        [PerformanceAspect(15)]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_iCarDal.GetCarDetails(), Messages.GetCarsWithDetails);
        }

        [CacheAspect]
        public IDataResult<CarDetailDto> GetCarDetailsById(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(_iCarDal.GetCarDetailsById(c => c.CarId == carId), Messages.GetCarDetailsById);
        }

        [PerformanceAspect(5)]
        public IDataResult<List<CarImagesDto>> GetCarImageDetails()
        {
            return new SuccessDataResult<List<CarImagesDto>>(_iCarDal.GetCarImageDetails(), Messages.CarImageDetails);
        }

        [PerformanceAspect(15)]
        public IDataResult<List<CarImagesDto>> GetCarImageDetailsByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImagesDto>>(_iCarDal.GetCarImageDetails(c => c.CarId == carId), Messages.CarImageDetailsByCarId);
        }
    }
}
