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

            //CoreAndDtoTest();

            //ColorWithResultAndDataResultTest();

            //BrandWithResultAndDataResultTest();

            //CarWithResultAndDataResultTest();

            //BrandManager brandManager = new BrandManager(new EfBrandDal());
            //brandManager.Add(new Brand { BrandName = "Volkswagen" });
            //brandManager.Add(new Brand { BrandName = "Volvo" });
            //brandManager.Add(new Brand { BrandName = "BMW" });
            //brandManager.Add(new Brand { BrandName = "Mercedes" });
            //brandManager.Add(new Brand { BrandName = "Jaguar" });
            //brandManager.Add(new Brand { BrandName = "Chrysler" });

            //var result = brandManager.GetAll();
            //if (result.Success)
            //{
            //    foreach (var brand in result.Data)
            //    {
            //        Console.WriteLine(brand.BrandName);
            //    }
            //}
            //ColorManager colorManager = new ColorManager(new EfColorDal());
            //colorManager.Add(new Color { ColorName = "Siyah" });
            //colorManager.Add(new Color { ColorName = "Beyaz" });
            //colorManager.Add(new Color { ColorName = "Gri" });
            //colorManager.Add(new Color { ColorName = "Mavi" });

            //var result2 = colorManager.GetAll();
            //if (result.Success)
            //{
            //    foreach (var color in result2.Data)
            //    {
            //        Console.WriteLine(color.ColorName);
            //    }
            //}

            //CarManager carManager = new CarManager(new EfCarDal());
            //carManager.Add(new Car { BrandId = 2, CarName = "Phaeton", ColorId = 4, DailyPrice = 120000, ModelYear = "2003", Description = "Volkswagen’in Amiral Gemisi" });
            //carManager.Add(new Car { BrandId = 3, CarName = "S80", ColorId = 2, DailyPrice = 80000, ModelYear = "2000", Description = "Volvo S80; çıkış motoru 2.0 litre olan, bakım ve vergi avantajı yüksek bir arabadır" });
            //carManager.Add(new Car { BrandId = 4, CarName = "BMW 7", ColorId = 2, DailyPrice = 82000, ModelYear = "2001", Description = "BMW’nin Amiral Gemisi" });
            //carManager.Add(new Car { BrandId = 5, CarName = "Mercedes S", ColorId = 4, DailyPrice = 64000, ModelYear = "1999", Description = "İlk gergili emniyet kemerinin kullanıldığı arabadır" });
            //carManager.Add(new Car { BrandId = 6, CarName = "X-TYPE", ColorId = 2, DailyPrice = 72000, ModelYear = "2004", Description = "2006 yılında üretim sonlandırılmasından sonra daha değerli olmuştur" });
            //carManager.Add(new Car { BrandId = 7, CarName = "300M", ColorId = 2, DailyPrice = 56000, ModelYear = "1999", Description = "İthalat Savaşçısı" });
            //carManager.Add(new Car { BrandId = 1, CarName = "Accord", ColorId = 1, DailyPrice = 57000, ModelYear = "2003", Description = "Japon işçiliğinin kalitesini hissettiriyor" });

            //var result3 = carManager.GetAll();
            //if (result3.Success)
            //{
            //    foreach (var car in result3.Data)
            //    {
            //        Console.WriteLine("CarId:{0} BrandId:{1} CarName:{2} ColorId:{3} DailyPrice:{4} ModelYear:{5}, Description:{6}",
            //            car.CarId, car.BrandId, car.CarName, car.ColorId, car.DailyPrice, car.ModelYear, car.Description);
            //    }
            //}

            //UserManager userManager = new UserManager(new EfUserDal());
            //userManager.Add(new User { FirstName = "İrem", LastName = "Çalışkan", Email = "iremcaliskan0@gmail.com", Password = "123123" });
            //userManager.Add(new User { FirstName = "Ceren", LastName = "Çalışkan", Email = "cerencaliskan@gmail.com", Password = "123123" });

            //var result4 = userManager.GetAll();
            //if (result4.Success)
            //{
            //    foreach (var user in result4.Data)
            //    {
            //        Console.WriteLine(user.FirstName);
            //    }
            //}

            //CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            //customerManager.Add(new Customer { UserId = 1, CompanyName = "Öğrenci" });
            //customerManager.Add(new Customer { UserId = 2, CompanyName = "Öğrenci" });
            //customerManager.Add(new Customer { UserId = 3, CompanyName = "Yok" });

            //var result = customerManager.GetAll();
            //if (result.Success)
            //{
            //    foreach (var customer in result.Data)
            //    {
            //        Console.WriteLine(customer.CompanyName);
            //    }
            //}

            //RentalManager rentalManager = new RentalManager(new EfRentalDal());
            //Console.WriteLine(rentalManager.Add(new Rental { CarId = 1, CustomerId = 1, RentDate = new DateTime(2021, 2, 25) }).Message);
            //rentalManager.Add(new Rental { CarId = 2, CustomerId = 1, RentDate = new DateTime(2021, 2, 20), ReturnDate = new DateTime(2021, 2, 25) });
            //rentalManager.Add(new Rental { CarId = 3, CustomerId = 2, RentDate = new DateTime(2021, 2, 20), ReturnDate = new DateTime(2021, 2, 25) });

            //var result = rentalManager.GetAll();
            //if (result.Success)
            //{
            //    foreach (var rental in result.Data)
            //    {
            //        Console.WriteLine(rental.RentDate);
            //    }
            //}


        }

        private static void CarWithResultAndDataResultTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();
            if (result.Success)
            {
                Console.WriteLine(result.Message);
                foreach (var car in result.Data)
                {
                    Console.WriteLine("{0} {1} {2} {3}", car.CarName, car.BrandName, car.ColorName, car.DailyPrice);

                }
            }

            var result2 = carManager.GetCarsByBrandId(2);
            if (result2.Success)
            {
                Console.WriteLine(result2.Message);
                foreach (var car in result2.Data)
                {
                    Console.WriteLine(car.CarName);
                }
            }

            var result3 = carManager.GetCarsByColorId(1);
            if (result3.Success)
            {
                Console.WriteLine(result3.Message);
                foreach (var car in result3.Data)
                {
                    Console.WriteLine(car.CarName);
                }
            }

            var result4 = carManager.GetById(1);
            if (result4.Success)
            {
                Console.WriteLine(result4.Message);
                Console.WriteLine(result4.Data.CarName);
            }
        }

        private static void BrandWithResultAndDataResultTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            Brand brand1 = new Brand { BrandId = 9, BrandName = "Volvo" };
            //Console.WriteLine(brandManager.Add(brand1).Message);
            brand1.BrandName = "Volvoo";
            //Console.WriteLine(brandManager.Update(brand1).Message);
            //Console.WriteLine(brandManager.Delete(brand1).Message);
            var result = brandManager.GetAll();
            if (result.Success)
            {
                foreach (var brand in result.Data)
                {
                    Console.WriteLine(brand.BrandName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            var result2 = brandManager.GetById(1);
            Console.WriteLine(result2.Data.BrandName);
            Brand brand2 = new Brand { BrandId = 9, BrandName = "V" };
            Console.WriteLine(brandManager.Add(brand2).Message);
        }

        private static void ColorWithResultAndDataResultTest()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            Color color1 = new Color() { ColorId = 8, ColorName = "Mor" };

            //Console.WriteLine(colorManager.Add(color1).Message);
            //color1.ColorName = "Purple";
            //Console.WriteLine(colorManager.Update(color1).Message);
            //Console.WriteLine(colorManager.Delete(color1).Message);
            //var result = colorManager.GetAll();
            var result = colorManager.GetById(6);

            if (result.Success == true)
            {
                /*
                foreach (var color in result.Data)
                {
                    Console.WriteLine(color.ColorName);
                }
                */
                Console.WriteLine(result.Data.ColorName);
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Color color2 = new Color() { ColorId = 8, ColorName = "Mo" };
            var result2 = colorManager.Add(color2);
            if (result2.Success == false)
            {
                Console.WriteLine(result2.Message);
            }
        }

        private static void CoreAndDtoTest()
        {
            // Evrensel kodların yazıldığı ve base sınıflardan oluşan Core katmanı yazıldı.
            // Dto örneği yapıldı.
            // Crud operasyonları tamamlandı.
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetCarDetails().Data)
            {
                Console.WriteLine("{0} {1} {2} {3}", car.CarName, car.BrandName, car.ColorName, car.DailyPrice);
            }
            ColorManager colorManager = new ColorManager(new EfColorDal());
            //colorManager.Add(new Color { ColorId = 6, ColorName = "Orange" });
            //colorManager.Update(new Color { ColorId = 6, ColorName = "Turuncu" });

            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine(color.ColorName);
            }
            Console.WriteLine(colorManager.GetById(6).Data.ColorName);

            BrandManager brandManager = new BrandManager(new EfBrandDal());
            //brandManager.Add(new Brand { BrandId = 8, BrandName = "TOYOTA" });
            //brandManager.Update(new Brand { BrandId = 8, BrandName = "Toyota" });

            foreach (var brand in brandManager.GetAll().Data)
            {
                Console.WriteLine(brand.BrandName);
            }
            Console.WriteLine(brandManager.GetById(8).Data.BrandName);
        }

        private static void EfCarDalTest()
        {
            // Test of EfCarDal
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetCarsByBrandId(5).Data) // BMW
            {
                Console.WriteLine(car.CarName);
            }

            foreach (var car in carManager.GetCarsByColorId(1).Data) // Siyah
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
            foreach (var car in carManager1.GetAll().Data)
            {
                Console.WriteLine("CarId:{0} Description:{1} ModelYear:{2} DailyPrice{3}",
                    car.CarId, car.Description, car.ModelYear, car.DailyPrice);
            }

            Console.WriteLine("Result of filter: {0} {1}", carManager1.GetById(1).Data.CarId, carManager1.GetById(1).Data.DailyPrice);
        }
    }
}
