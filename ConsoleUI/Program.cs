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

            //EfCarDalTest();

            // Evrensel kodların yazıldığı ve base sınıflardan oluşan Core katmanı yazıldı.
            // Dto örneği yapıldı.
            // Crud operasyonları tamamlandı.
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetCarDetails())
            {
                Console.WriteLine("{0} {1} {2} {3}", car.CarName, car.BrandName, car.ColorName, car.DailyPrice);
            }
            ColorManager colorManager = new ColorManager(new EfColorDal());
            //colorManager.Add(new Color { ColorId = 6, ColorName = "Orange" });
            //colorManager.Update(new Color { ColorId = 6, ColorName = "Turuncu" });

            foreach (var color in colorManager.GetAll())
            {
                Console.WriteLine(color.ColorName);
            }
            Console.WriteLine(colorManager.GetById(6).ColorName);
           
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            //brandManager.Add(new Brand { BrandId = 8, BrandName = "TOYOTA" });
            //brandManager.Update(new Brand { BrandId = 8, BrandName = "Toyota" });

            foreach (var brand in brandManager.GetAll())
            {
                Console.WriteLine(brand.BrandName);
            }
            Console.WriteLine(brandManager.GetById(8).BrandName);
        }

        private static void EfCarDalTest()
        {
            // Test of EfCarDal
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetCarsByBrandId(5)) // BMW
            {
                Console.WriteLine(car.CarName);
            }

            foreach (var car in carManager.GetCarsByColorId(1)) // Siyah
            {
                Console.WriteLine(car.CarName);
            }

            Car demoCar = new Car()
            {
                CarId = 8,
                BrandId = 7,
                ColorId = 1,
                CarName = "Car Demo",
                DailyPrice = 10,
                Description = "Car Demo",
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
