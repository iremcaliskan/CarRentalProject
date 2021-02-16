using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager1 = new CarManager(new InMemoryCarDal());
            foreach (var car in carManager1.GetAll())
            {
                Console.WriteLine("CarId:{0} Description:{1} ModelYear:{2} DailyPrice{3}", 
                    car.CarId, car.Description, car.ModelYear, car.DailyPrice);
            }

            Console.WriteLine("Result of filter: {0} {1}", carManager1.GetById(1).CarId, carManager1.GetById(1).DailyPrice);
        }

    }
}
