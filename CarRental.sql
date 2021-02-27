CREATE TABLE Brands (
    BrandId int IDENTITY(1,1) NOT NULL,
    BrandName nvarchar(50) NOT NULL,
    CONSTRAINT "PK_Brands" PRIMARY KEY CLUSTERED ("BrandId")
);

CREATE TABLE Colors (
    ColorId int IDENTITY(1,1) NOT NULL,
    ColorName nvarchar(50) NOT NULL,
    CONSTRAINT "PK_Colors" PRIMARY KEY CLUSTERED ("ColorId")
);

CREATE TABLE Cars (
    CarId int IDENTITY(1,1) NOT NULL,
    BrandId int NOT NULL,
    ColorId int NOT NULL,
    CarName nvarchar(50) NOT NULL,
    ModelYear nvarchar(20) NOT NULL,
    DailyPrice decimal(18) NOT NULL,
    Description nvarchar(255) NOT NULL,
    CONSTRAINT "PK_Cars" PRIMARY KEY  CLUSTERED ("CarId"),
    CONSTRAINT "FK_Cars_Brands" FOREIGN KEY ("BrandId") REFERENCES "dbo"."Brands" ("BrandId"),
    CONSTRAINT "FK_Cars_Colors" FOREIGN KEY ("ColorId") REFERENCES "dbo"."Colors" ("ColorId")
);

CREATE TABLE Users (
    UserId int IDENTITY(1,1) NOT NULL,
    FirstName nvarchar(50) NOT NULL,
    LastName nvarchar(50) NOT NULL,
    Email nvarchar(50) NOT NULL,
    Password nvarchar(50) NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY CLUSTERED ("UserId"),
);

CREATE TABLE Customers (
    UserId int NOT NULL,
    CompanyName nvarchar(255) NULL,
    CONSTRAINT "PK_Customers" PRIMARY KEY CLUSTERED ("UserId"),
    CONSTRAINT "FK_Customers_Users" FOREIGN KEY ("UserId") REFERENCES "dbo"."Users" ("UserId")
);

CREATE TABLE Rentals (
    RentalId int IDENTITY(1,1) NOT NULL,
    CarId int NOT NULL,
    CustomerId int NOT NULL,
    RentDate datetime NOT NULL,
    ReturnDate datetime DEFAULT NULL,
    CONSTRAINT "PK_Rentals" PRIMARY KEY CLUSTERED ("RentalId"),
    CONSTRAINT "FK_Rentals_Cars" FOREIGN KEY ("CarId") REFERENCES "dbo"."Cars" ("CarId"),
    CONSTRAINT "FK_Rentals_Customers" FOREIGN KEY ("CustomerId") REFERENCES "dbo"."Customers" ("UserId")
);