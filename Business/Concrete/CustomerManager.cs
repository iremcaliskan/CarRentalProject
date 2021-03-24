using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects;
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
        { // Newlenme anında, oluşturulma anında bir veri erişim yöntemi alacak
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
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

        public IResult Delete(Customer customer)
        {
            try
            {
                _customerDal.Delete(customer);
                return new SuccessResult(Messages.Deleted);
            }
            catch (Exception)
            {
                throw new Exception("A system error occurs on deletion!");
            }
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.GetAll);
        }

        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.CustomerId == customerId), Messages.GetCustomerByUserId);
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails(), "Customer details are listed!");
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails(string key)
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails(c => c.LastName.Contains(key)), "Customer is found by keyword!");
        }

        public IDataResult<CustomerDetailDto> GetCustomerDetailsById(int customerId)
        {
            return new SuccessDataResult<CustomerDetailDto>(_customerDal.GetCustomerDetailsById(c => c.CustomerId == customerId), "Customer is found!");
        }
    }
}
