using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    { // static: There is no newing, At the app life there is only one instance
        public static string Added = "Added";
        public static string BrandCanNotAdded = "An error occurs on addition about naming that must contain at least two characters";
        public static string ColorCanNotAdded = "An error occurs on addition about naming that must contain at least three characters";
        public static string CarCanNotAdded = "An error occurs on addition about naming that must contain at least two characters or The daily price of the car must be greater than zero";
        public static string CustomerCanNotAdded = "An error occurs on addition about naming that must contain at least three characters";

        public static string Updated = "Updated";
        public static string BrandCanNotUpdated = "An error occurs on modification about naming that must contain at least two characters";
        public static string ColorCanNotUpdated = "An error occurs on modification about naming that must contain at least three characters";
        public static string CarCanNotUpdated = "An error occurs on modification about naming that must contain at least two characters or The daily price of the car must be greater than zero";
        public static string CustomerCanNotUpdated = "An error occurs on modification about naming that must contain at least three characters";

        public static string Deleted = "Deleted";
        public static string ErrorOnDeleted = "An error occurs on deletion!";

        public static string GetAll = "All is listed";
        public static string GetBrandByBrandId = "Brand is got by BrandId";
        public static string GetColorByColorId = "Color is got by ColorId";
        public static string GetCarByCarId = "Car is got by CarId";
        public static string GetUserByUserId = "User is got by UserId";

        public static string GetCustomerByUserId = "Customer is got by UserId";
        public static string GetRentalByRentalId = "Rental is got by RentalId";
        public static string GetCarDetailsById = "Car is detailed by Id";
        public static string GetRentalDetails = "Rentals Details are listed";
        public static string GetRentalDetailsById = "Rental Details are listed by Id";

        public static string GetCarsByColorId = "Cars are listed by ColorId";
        public static string GetCarsByBrandId = "Cars are listed by BrandId";
        public static string GetCarsWithDetails = "Cars are listed with own details";
        public static string GetCarWithDetails = "Car is got with own details by CarId.";

        public static string CarImageLimitExceeded = "More than 5 images can not be added to the system";

        public static string FileAdded = "File is added!";
        public static string FileUpdated = "File is added!";
        public static string FileDeleted = "File is added!";

        public static string[] ValidImageFileTypes = { ".JPG", ".JPEG", ".PNG", ".TIF", ".TIFF", ".GIF", ".BMP", ".ICO" };
        public static string InvalidImageExtension = "Geçersiz dosya uzantısı, fotoğraf için kabul edilen uzantılar" + string.Join(",", ValidImageFileTypes);

        public static string UserFoundByEmail = "User is found by email!";
        public static string UserClaimsListed = "User's claims are listed!";
        public static string UserAdd = "User is added";
        public static string UserGetByMail = "User is got by email";
        public static string UserNotFound = "User is not found";
        public static string PasswordError = "Password is not correct";
        public static string SuccessfulLogin = "Login is successful";
        public static string UserAlreadyExists = "User is already exist in the system";
        public static string UserRegistered = "User is registered";
        public static string AccessTokenCreated = "Access token is created";

        public static string AuthorizationDenied = "Authorization is denied";

        public static string CarImageDetailsByCarId = "Images are listed by car id!";
        public static string CarImageDetails = "Car Images are listed!";

        public static string ColorIsFoundByKey = "Color is found by search key!";
        
        public static string GetCustomerDetailsById = "Customer is found!";
        public static string GetCustomerDetailsByKey = "Customer is found by keyword!";
        public static string GetCustomerDetails = "Customer details are listed!";

        public static string GetById = "Car Image is found by own Id!";

        public static string aa = "";
    }
}
