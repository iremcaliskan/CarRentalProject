CREATE TABLE Brands (
    BrandId int IDENTITY(1,1) NOT NULL,
    BrandName nvarchar(50) NOT NULL,
    CONSTRAINT PK_Brands PRIMARY KEY (BrandId)
);

CREATE TABLE Colors (
    ColorId int IDENTITY(1,1) NOT NULL,
    ColorName nvarchar(50) NOT NULL,
    CONSTRAINT PK_Colors PRIMARY KEY (ColorId)
);

CREATE TABLE Cars (
    CarId int IDENTITY(1,1) NOT NULL,
    BrandId int NOT NULL,
    ColorId int NOT NULL,
    CarName nvarchar(50) NOT NULL,
    ModelYear nvarchar(20) NOT NULL,
    DailyPrice decimal(18) NOT NULL,
    Description nvarchar(255) NOT NULL,
    CONSTRAINT PK_Cars PRIMARY KEY (CarId),
    FOREIGN KEY (BrandId) REFERENCES Brands(BrandId),
    FOREIGN KEY (ColorId) REFERENCES Colors(ColorId)
);

CREATE TABLE Users (
    UserId int IDENTITY(1,1) NOT NULL,
    FirstName nvarchar(50) NOT NULL,
    LastName nvarchar(50) NOT NULL,
    Email nvarchar(50) NOT NULL,
    Password nvarchar(50) NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY (UserId)
);

CREATE TABLE Customers (
    UserId int NOT NULL,
    CompanyName nvarchar(50) NOT NULL,
    CONSTRAINT PK_Customers PRIMARY KEY (UserId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE Rentals (
    RentalId int IDENTITY(1,1) NOT NULL,
    CarId int NOT NULL,
    CustomerId int NOT NULL,
    RentDate nvarchar(255) NOT NULL,
    ReturnDate nvarchar(255) DEFAULT NULL,
    CONSTRAINT PK_Rentals PRIMARY KEY (RentalId),
    FOREIGN KEY (CarId) REFERENCES Cars(CarId),
    FOREIGN KEY (CustomerId) REFERENCES Customers(UserId)
);