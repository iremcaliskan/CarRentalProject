using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        { // It needs a data access technique at creation time of class, newing
            _brandDal = brandDal;
        }

        [SecuredOperation("brand.add")]
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandValidator))] // Before the Addition process, intercepts the process and validate structure of Brand with BrandValidator
        [TransactionScopeAspect]
        public IResult Add(Brand brand)
        {
            /*
            if (brand.BrandName.Length < 2)
            {
                return new ErrorResult(Messages.BrandCanNotAdded);
            }
            */

            _brandDal.Add(brand);

            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("brand.update")]
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandValidator))]
        [TransactionScopeAspect]
        public IResult Update(Brand brand)
        {
            /*
            if (brand.BrandName.Length < 2)
            {
                return new ErrorResult(Messages.BrandCanNotUpdated);
            }
            */

            _brandDal.Update(brand);

            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("brand.delete")]
        [CacheRemoveAspect("IBrandService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(Brand brand)
        {
            try
            {
                _brandDal.Delete(brand);
                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception)
            {
                throw new Exception(Messages.ErrorOnDeleted);
            }
            
        }

        [SecuredOperation("brand.list")]
        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.GetAll);
        }

        [SecuredOperation("brand.getid")]
        [CacheAspect]
        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == brandId), Messages.GetBrandByBrandId);
        }

        [SecuredOperation("brand.getname")]
        public IDataResult<List<Brand>> GetByName(string name)
        {
            var result = _brandDal.GetAll(b=> b.BrandName.Contains(name));
            return new SuccessDataResult<List<Brand>>(result); // Search for a name
        }
    }
}
