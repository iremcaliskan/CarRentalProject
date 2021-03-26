using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [SecuredOperation("carimage.add")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            // Rule Engine
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(carImage.CarId),CheckIfImageExtensionValid(file));

            if (result != null)
            {
                return result;
            } // If it does not return logic, it is ok

            carImage.ImagePath = FileHelper.Add(file);
            carImage.CreatedDate = DateTime.Now;

            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("carimage.update")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            // Rule Engine
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(carImage.CarId), CheckIfImageExtensionValid(file), CheckIfImageExists(carImage.CarImageId));

            if (result != null)
            {
                return result;
            } // If it does not return logic, it is ok

            CarImage oldCarImage = GetById(carImage.CarImageId).Data;

            carImage.ImagePath = FileHelper.Update(file, oldCarImage.ImagePath);
            carImage.CreatedDate = DateTime.Now;
            carImage.CarId = oldCarImage.CarId;

            _carImageDal.Update(carImage);

            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("carimage.delete")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfImageExists(carImage.CarImageId));

            if (result != null)
            {
                return result;
            }

            string path = GetById(carImage.CarImageId).Data.ImagePath;

            FileHelper.Delete(path);

            _carImageDal.Delete(carImage);

            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.CarImageId == id));
        }

        [SecuredOperation("carimage.getid")]
        [CacheAspect]
        public IDataResult<CarImage> GetById(int carImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.CarImageId == carImageId), "Car Image is found by its id");
        }

        [SecuredOperation("carimage.list")]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        [SecuredOperation("carimage.carid")]
        [CacheAspect]
        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageNull(carId));

            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(result.Message);
            }

            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(carId).Data);
        }
        
         /**
         *** Business Rules - Yapısal olmayan sisteme konması istenen kurallar (Not structural rules, Rules from Management)
        **/

        private IResult CheckImageLimitExceeded(int carId)
        {
            var carImageCount = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (carImageCount > 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }

            return new SuccessResult();
        }

        private IDataResult<List<CarImage>> CheckIfCarImageNull(int carId)
        {
            try
            {
                string defaultPath = @"\wwwroot\images\logo.jpg";
                var result = _carImageDal.GetAll(c => c.CarId == carId).Any();
                if (!result)
                {
                    var carImage = new List<CarImage>();
                    carImage.Add(new CarImage { CarId = carId, ImagePath = defaultPath, CreatedDate = DateTime.Now });
                    return new SuccessDataResult<List<CarImage>>(carImage);
                }
                
            }
            catch (Exception exception)
            {

                return new ErrorDataResult<List<CarImage>>(exception.Message);
            }   

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
        }

        private IResult CheckIfImageExists(int id)
        {
            if (_carImageDal.IsExist(id))
                return new SuccessResult();
            return new ErrorResult("Car image must be exist!");
        }

        private List<CarImage> CheckIfCarHaveNoImage(int carId)
        {
            string path = @"\images\logo.jpg";
            var result = _carImageDal.GetAll(c => c.CarId == carId);
            if (!result.Any())
            {
                return new List<CarImage> { new CarImage { CarId = carId, ImagePath = path } };
            }
            return result;
        }

        private IResult CheckIfImageExtensionValid(IFormFile file)
        {
            bool isValidFileExtension = Messages.ValidImageFileTypes.Any(t => t == Path.GetExtension(file.FileName).ToUpper());
            if (!isValidFileExtension)
            {
                return new ErrorResult(Messages.InvalidImageExtension);
            }
            return new SuccessResult();
        }
    }
}
