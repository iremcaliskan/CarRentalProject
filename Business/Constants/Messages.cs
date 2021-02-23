using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    { // static: Newlenmeden kullanılır, bir instance olur sadece
        public static string Added = "Added";
        public static string BrandCanNotAdded = "An error occurs on addition about naming that must contain at least two characters";
        public static string ColorCanNotAdded = "An error occurs on addition about naming that must contain at least three characters";
        public static string CarCanNotAdded = "An error occurs on addition about naming that must contain at least two characters or The daily price of the car must be greater than zero";

        public static string Updated = "Updated";
        public static string BrandCanNotUpdated = "An error occurs on modification about naming that must contain at least two characters";
        public static string ColorCanNotUpdated = "An error occurs on modification about naming that must contain at least three characters";
        public static string CarCanNotUpdated = "An error occurs on modification about naming that must contain at least two characters or The daily price of the car must be greater than zero";

        public static string Deleted = "Deleted";

        public static string GetAll = "All is listed";
        public static string GetBrandByBrandId = "Brand is got by BrandId";
        public static string GetColorByColorId = "Color is got by ColorId";
        public static string GetCarByCarId = "Car is got by CarId";

        public static string GetCarsByColorId = "Cars are listed by ColorId";
        public static string GetCarsByBrandId = "Cars are listed by BrandId";
        public static string GetCarsWithDetails = "Cars are listed with own details";
        public static string GetCarWithDetails = "Car is got with own details by CarId.";

    }
}
