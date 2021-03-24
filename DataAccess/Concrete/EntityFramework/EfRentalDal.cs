using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{

    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalDBContext>, IRentalDal
    {
        // EfRentalDal inherits operations from EfEntityRepositoryBase<Rental, CarRentalDBContext> by passing own type and context
        // Also, if EfRentalDal has special methods, it gets method from IRentalDal so that implements method on it.

        public List<RentalDetailDto> GetRentalDetails(Expression<Func<RentalDetailDto, bool>> filter = null)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                // Customer User DTO
                var customer = from c in context.Customers
                               join u in context.Users
                               on c.CustomerId equals u.UserId
                               select new CustomerDetailDto()
                               {
                                   CustomerId = c.CustomerId,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName
                               };

                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarId equals c.CarId
                             join cstmr in customer
                             on r.CustomerId equals cstmr.CustomerId
                             select new RentalDetailDto()
                             {
                                 RentalId = r.RentalId,
                                 CarName = c.CarName,
                                 CustomerFirstName = cstmr.FirstName,
                                 CustomerLastName = cstmr.LastName,
                                 RentDate = r.RentDate
                             };

                return filter == null
                       ? result.ToList() // if filter is null return the all rentals list
                       : result.Where(filter).ToList(); // if not, return the all rentals list that is specified by the filter/predicate/lamda
                // Where, chooses elements by the given filter and gather all chosed elements in a list
            }
        }

        public RentalDetailDto GetRentalDetailsById(Expression<Func<RentalDetailDto, bool>> filter)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                var customer = from c in context.Customers
                               join u in context.Users
                               on c.CustomerId equals u.UserId
                               select new CustomerDetailDto()
                               {
                                   CustomerId = c.CustomerId,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName
                               };

                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarId equals c.CarId
                             join cstmr in customer
                             on r.CustomerId equals cstmr.CustomerId
                             select new RentalDetailDto()
                             {
                                 RentalId = r.RentalId,
                                 CarName = c.CarName,
                                 CustomerFirstName = cstmr.FirstName,
                                 CustomerLastName = cstmr.LastName,
                                 RentDate = r.RentDate
                             };

                return result.SingleOrDefault(filter);
            }
        }
    }
}
