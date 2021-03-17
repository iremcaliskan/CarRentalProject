using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq; // Error veriyor sorgu ayrı eklenmeyince
using System.Linq.Expressions;


namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, CarRentalDBContext>, ICustomerDal
    {
        // Ef veri erişim yöntemlerini kendine uyarlayarak yapacak
        public List<CustomerDetailDto> GetCustomerDetails(Expression<Func<CustomerDetailDto, bool>> filter = null)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                // Customer User DTO
                var result =   from customer in context.Customers // using System.Linq'i ayrı ekle
                               join user in context.Users
                               on customer.CustomerId equals user.UserId
                               select new CustomerDetailDto()
                               {
                                   FirstName = user.FirstName,
                                   LastName = user.LastName,
                                   Email = user.Email
                               };

                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }

        public CustomerDetailDto GetCustomerDetailsById(Expression<Func<CustomerDetailDto, bool>> filter)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                var result = from customer in context.Customers
                             join user in context.Users
                             on customer.CustomerId equals user.UserId
                             select new CustomerDetailDto()
                             {
                                 CustomerId = customer.CustomerId,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email
                             };

                return result.SingleOrDefault(filter);
            }
        }
    }
}
