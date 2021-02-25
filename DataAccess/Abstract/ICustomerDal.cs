using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {
        //ICustomerDal operasyonlarını IEntityRepository<Customer>'dan alır, Customer göndererek
    }
}
