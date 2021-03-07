﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects;
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
        { // Oluşturulma anında bir veri erişim yöntemi ister
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))] // Ekleme işleminden önce araya gir doğrulamak için, BrandValidator kullanarak doğrula!
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

        [ValidationAspect(typeof(BrandValidator))] // Güncelleme işlemi öncesinde araya gir, BrandValidator ile doğrula
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

        public IResult Delete(Brand brand)
        {
            try
            {
                _brandDal.Delete(brand);
                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception)
            {
                throw new Exception("An error occurs on deletion!");
            }
            
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.GetAll);
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == brandId), Messages.GetBrandByBrandId);
        }
    }
}
