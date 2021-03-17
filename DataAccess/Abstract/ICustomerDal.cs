using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {
        //ICustomerDal operasyonlarını IEntityRepository<Customer>'dan alır, Customer göndererek
        List<CustomerDetailDto> GetCustomerDetails(Expression<Func<CustomerDetailDto, bool>> filter = null);
        CustomerDetailDto GetCustomerDetailsById(Expression<Func<CustomerDetailDto, bool>> filter);

    }
}
