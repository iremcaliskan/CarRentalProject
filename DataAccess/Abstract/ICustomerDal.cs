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
        // ICustomerDal gets its operation from IEntityRepository<Customer> with passing parameter as Customer, own type
        List<CustomerDetailDto> GetCustomerDetails(Expression<Func<CustomerDetailDto, bool>> filter = null);
        CustomerDetailDto GetCustomerDetailsById(Expression<Func<CustomerDetailDto, bool>> filter);

    }
}
