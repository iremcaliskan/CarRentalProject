﻿using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, CarRentalDBContext>, IUserDal
    {
        // Ef veri erişim yöntemlerini kendine uyarlayarak yapacak

    }
}
