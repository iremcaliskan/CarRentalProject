using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //InMemoryCarDalTest();

            // Test of EfCarDal
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetCarsByBrandId(5)) // BMW
            {
                Console.WriteLine(car.Description);
            }

            foreach (var car in carManager.GetCarsByColorId(1)) // Siyah
            {
                Console.WriteLine(car.Description);
            }

            Car demoCar = new Car() {
                CarId = 8,
                BrandId = 7,
                ColorId = 1,
                DailyPrice = 10,
                Description = "Demo Car",
                ModelYear = "2010"
            };

            //carManager.Add(demoCar);
            demoCar.Description = "Updated Demo Car";
            carManager.Update(demoCar);
        }

        private static void InMemoryCarDalTest()
        {
            // Test of InMemoryCarDal 
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
