using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using Core.Aspects;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Business.BusinessAspect.Autofac;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _iCarDal; // Veri erişim yöntemlerinin her birini tutabilecek referans
        public CarManager(ICarDal iCarDal)
        { // Oluşturma anında bir veri erişim yöntemi istiyor.
            _iCarDal = iCarDal;
        }

        [ValidationAspect(typeof(CarValidator))] // Ekleme işleminden önce araya gir doğrulamak için, CarValidator kullanarak doğrula!
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

        [ValidationAspect(typeof(CarValidator))] // Güncelleme işlemi öncesinde araya gir, CarValidator ile doğrula
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

        [SecuredOperation("Car.List")]
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

        public IDataResult<List<CarImagesDto>> GetCarImageDetails()
        {
            return new SuccessDataResult<List<CarImagesDto>>(_iCarDal.GetCarImageDetails(), Messages.CarImageDetails);
        }

        public IDataResult<List<CarImagesDto>> GetCarImageDetailsByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImagesDto>>(_iCarDal.GetCarImageDetails(c => c.CarId == carId), Messages.CarImageDetailsByCarId);
        }
    }
}
