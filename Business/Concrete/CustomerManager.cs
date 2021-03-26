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
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        { // It requires a data access technique at the new time
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect("ICustomerService.Get")]
        [TransactionScopeAspect]
        public IResult Add(Customer customer)
        {
            /*
            if (customer.CompanyName.Length < 3)
            {
                return new ErrorResult(Messages.CustomerCanNotAdded);
            }
            */

            _customerDal.Add(customer);
            return new SuccessResult(Messages.Added);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect("ICustomerService.Get")]
        [TransactionScopeAspect]
        public IResult Update(Customer customer)
        {
            /*
            if (customer.CompanyName.Length < 3)
            {
                return new ErrorResult(Messages.CustomerCanNotUpdated);
            }
            */

            _customerDal.Update(customer);
            return new SuccessResult(Messages.Updated);
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(Customer customer)
        {
            try
            {
                _customerDal.Delete(customer);
                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [SecuredOperation("customer.list")]
        [CacheAspect]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.GetAll);
        }

        [SecuredOperation("customer.getid")]
        [CacheAspect]
        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.CustomerId == customerId), Messages.GetCustomerByUserId);
        }

        [SecuredOperation("customer.details")]
        [CacheAspect]
        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails(), Messages.GetCustomerDetails);
        }

        [SecuredOperation("customer.detailskey")]
        [CacheAspect]
        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails(string key)
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails(c => c.LastName.Contains(key)), Messages.GetCustomerDetailsByKey);
        }

        [SecuredOperation("customer.detailsid")]
        [CacheAspect]
        public IDataResult<CustomerDetailDto> GetCustomerDetailsById(int customerId)
        {
            return new SuccessDataResult<CustomerDetailDto>(_customerDal.GetCustomerDetailsById(c => c.CustomerId == customerId), Messages.GetCustomerDetailsById);
        }
    }
}
