using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarImageDal : EfEntityRepositoryBase<CarImage, CarRentalDBContext>, ICarImageDal
    {
        public bool IsExist(int carImageId)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                return context.CarImages.Any(c => c.CarImageId == carImageId); // Any returns type of boolean
            }
        }
    }
}
