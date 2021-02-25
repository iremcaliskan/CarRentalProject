using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, CarRentalDBContext>, ICustomerDal
    {
        // Ef veri erişim yöntemlerini kendine uyarlayarak yapacak

    }
}
