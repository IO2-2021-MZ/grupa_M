SET NOCOUNT ON
GO

USE master

GO
if exists (select * from sysdatabases where name='IO2_Restaurants')
        drop database IO2_Restaurants
GO

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE IO2_Restaurants
  ON PRIMARY (NAME = N''IO2_Restaurants'', FILENAME = N''' + @device_directory + N'IO2_Restaurants.mdf'')
  LOG ON (NAME = N''IO2_Restaurants_log'',  FILENAME = N''' + @device_directory + N'IO2_Restaurants.ldf'')')

GO 

set quoted_identifier on
GO

SET DATEFORMAT mdy
GO

use "IO2_Restaurants"
GO


CREATE TABLE [Address] (
	id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	city varchar(50) NOT NULL,
	street varchar(50) NOT NULL,
	post_code varchar(6) NOT NULL,
)

GO


CREATE TABLE Restaurant (
	id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[name] varchar(100) NOT NULL,
	contact_information varchar(100) NOT NULL,
	rating decimal NOT NULL,
	[state] int NOT NULL,
	[owing] decimal NOT NULL,
	date_of_joining datetime NOT NULL,
	aggregate_payment decimal NOT NULL,
	address_id int NOT NULL,

	CONSTRAINT FK_Restaurant_Address FOREIGN KEY
	(
		address_id
	) REFERENCES [Address]
	(
		id
	) ON DELETE CASCADE
)

GO

CREATE TABLE Discount_Code(

    id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [percent] int NOT NULL,
    code varchar(50) NOT NULL,
    date_from datetime NOT NULL,
    date_to datetime NOT NULL,

		restaurant_id int NOT NULL,

	CONSTRAINT FK_Address_Restaurant FOREIGN KEY
	(
		restaurant_id
	) REFERENCES Restaurant
	(
		id
	) ON DELETE CASCADE
)

GO

CREATE TABLE [User] (
	id int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[name] varchar(50) NOT NULL,
	surname varchar(50) NOT NULL,
	email varchar(50) NOT NULL,
	is_restaurateur bit NOT NULL,
	is_administrator bit NOT NULL,
	creation_date datetime NOT NULL,
	password_hash varchar(100) NOT NULL,
	address_id int,
	restaurant_id int,

	CONSTRAINT FK_User_Address FOREIGN KEY
	(
		address_id
	) REFERENCES [Address],

	CONSTRAINT FK_User_Restaurant FOREIGN KEY
	(
		restaurant_id
	) REFERENCES Restaurant
)

GO

CREATE TABLE [Order] (
	id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	payment_method int NOT NULL,
	[state] int NOT NULL,
	[date] datetime NOT NULL,

	address_id int NOT NULL,
	discount_code_id int,
	customer_id int,
	restaurant_id int NOT NULL,
	employee_id int,

	--! positions_ids - realizowane przez Order_Dish

	CONSTRAINT FK_Orders_Users FOREIGN KEY
	(
		customer_id
	) REFERENCES [User],
	CONSTRAINT FK_Order_Address FOREIGN KEY
	(
		address_id
	) REFERENCES [Address],
	CONSTRAINT FK_Order_Discount_Code FOREIGN KEY
	(
		discount_code_id
	) REFERENCES Discount_Code,
	CONSTRAINT FK_Order_Restaurant FOREIGN KEY
	(
		restaurant_id
	) REFERENCES Restaurant,
	CONSTRAINT FK_Order_Employee FOREIGN KEY
	(
		employee_id
	) REFERENCES [User]
)

GO

CREATE TABLE Review (
	id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	content varchar(100),
	rating decimal NOT NULL,
	customer_id int NOT NULL,
	restaurant_id int NOT NULL,

	CONSTRAINT FK_Review_User FOREIGN KEY
	(
		customer_id
	) REFERENCES [User],
	CONSTRAINT FK_Review_Restaurant FOREIGN KEY
	(
		restaurant_id
	) REFERENCES Restaurant
)

GO

CREATE TABLE Section(
	id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[name] varchar(100),
	restaurant_id int NOT NULL

	CONSTRAINT FK_Section_Restaurant FOREIGN KEY
	(
		restaurant_id
	) REFERENCES Restaurant
	(
		id
	) ON DELETE CASCADE
)

GO

CREATE TABLE Dish(
	id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[name] varchar (20) NOT NULL,
	[description] varchar (MAX) NOT NULL,
	price decimal NOT NULL,
	section_id int NOT NULL,

	CONSTRAINT FK_Dish_Section FOREIGN KEY
	(
		section_id
	) REFERENCES Section
	(
		id
	) ON DELETE CASCADE,
)

GO


CREATE TABLE Order_Dish(
	id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	dish_id int NOT NULL,
	order_id int NOT NULL,

	CONSTRAINT FK_Order_Dish_Dish FOREIGN KEY
	(
		dish_id
	) REFERENCES Dish,
	CONSTRAINT FK_Order_Dish_Order FOREIGN KEY
	(
		order_id
	) REFERENCES [Order]
)

GO

CREATE TABLE Complaint(
    id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    content varchar (MAX) NOT NULL,
    response varchar (MAX),
    [open] bit NOT NULL,
    customer_id int NOT NULL,
    order_id int NOT NULL,
	employee_id int,

	CONSTRAINT FK_Order_Complaint_User2 FOREIGN KEY
	(
		employee_id
	) REFERENCES [User],
 
    CONSTRAINT FK_Complaint_User FOREIGN KEY
    (
        customer_id
    ) REFERENCES [User]
    (
        id
    ) ON DELETE CASCADE,
    CONSTRAINT FK_Complaint_Order FOREIGN KEY
    (
        order_id
    ) REFERENCES [Order]
    (
        id
    ) ON DELETE CASCADE
)

GO
--! Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=IO2_Restaurants;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
