﻿using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [SecuredOperation("rental.add")]
        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        [TransactionScopeAspect]
        public IResult Add(Rental rental)
        {
            /*
            if (rental.ReturnDate != null)
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.Added);
            }
            return new ErrorResult("The car has not been returned, it can not be rented yet!");
            */

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("rental.update")]
        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        [TransactionScopeAspect]
        public IResult Update(Rental rental)
        {
            /*
            if (rental.ReturnDate != null)
            {
                _rentalDal.Update(rental);
                return new SuccessResult(Messages.Updated);
            }
            return new ErrorResult("The car has not been returned, it can not be updated yet!");
            */

            _rentalDal.Update(rental);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("rental.delete")]
        [CacheRemoveAspect("IRentalService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(Rental rental)
        {
            /*
            if (rental.ReturnDate != null)
            {
                _rentalDal.Delete(rental);
                return new SuccessResult(Messages.Deleted);
            }
            return new ErrorResult("The car has not been returned, it can not be deleted yet!");
            */

            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("rental.list")]
        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.GetAll);
        }

        [SecuredOperation("rental.getid")]
        [CacheAspect]
        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.RentalId == rentalId), Messages.GetRentalByRentalId);
        }

        [SecuredOperation("rental.details")]
        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.GetRentalDetails);
        }

        [SecuredOperation("rental.detailsid")]
        [CacheAspect]
        public IDataResult<RentalDetailDto> GetRentalDetailsById(int rentalId)
        {
            return new SuccessDataResult<RentalDetailDto>(_rentalDal.GetRentalDetailsById(r => r.RentalId == rentalId), Messages.GetRentalDetailsById);
        }
    }
}
