﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal
    {
        Car GetCarById(int carId); // carId'ye göre bir araba listelenir.
        List<Car> GetAll(); // Tüm arabaları listeleme.
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);


    }
}
