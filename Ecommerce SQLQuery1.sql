create database EcommerceDB

use EcommerceDB
-- Create Customers table

CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(300) NOT NULL,
    EmailAddress NVARCHAR(300) NOT NULL,
    Password NVARCHAR(300) NOT NULL,
    DeliveryAddress NVARCHAR(300) NOT NULL,
	PhoneNumber NVARCHAR(100) NOT NULL,
)

select * from Customers
select * from Categories
delete from Categories where CategoryId=3
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(300) NOT NULL,
    Description NVARCHAR(MAX) NULL,
	CategoryImage VARCHAR(MAX)NULL
)


select * from Products
-- Create Products table
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    CategoryId INT FOREIGN KEY REFERENCES Categories(CategoryId),
    ModelNumber NVARCHAR(100) NOT NULL,
    ModelName NVARCHAR(300) NOT NULL,
    UnitCost DECIMAL(10, 2) NOT NULL,
    Description NVARCHAR(MAX) NULL,
	ProductImage VARCHAR(MAX)
)



-- Create Orders table
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT FOREIGN KEY REFERENCES Customers(CustomerId),
    OrderDate DATETIME NOT NULL,
    ShipDate DATETIME NULL
)

-- Create OrderDetails table
CREATE TABLE OrderDetails (
    OrderDetailsID INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT,
    CustomerId INT FOREIGN KEY REFERENCES Customers(CustomerId),
    ProductId INT, 
    Quantity INT NOT NULL,
    UnitCost DECIMAL(10,2),
	FOREIGN KEY (OrderID) REFERENCES Orders(OrderId),
	FOREIGN KEY (ProductID) REFERENCES Products(ProductId)
	)


-- Create ShoppingCart table
CREATE TABLE ShoppingCart (
    CartId INT PRIMARY KEY IDENTITY(1,1),
	CustomerID INT,
    Quantity INT,
    ProductId INT,
    DateCreated DATE,
	FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
)
select*from ShoppingCart