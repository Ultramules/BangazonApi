

-- Internal Table

CREATE TABLE Departments (
    Id	INTEGER NOT NULL PRIMARY KEY IDENTITY,
    Name varchar(80) NOT NULL UNIQUE,
	ExpenseBudget FLOAT NOT NULL

);

CREATE TABLE Employees (
    Id	INTEGER NOT NULL PRIMARY KEY IDENTITY,
    FirstName	VARCHAR(80) NOT NULL,
    LastName	VARCHAR(80) NOT NULL,
    Supervisor BIT NOT NULL, 
    DepartmentId	INTEGER NOT NULL,
    CONSTRAINT FK_Department_Employees FOREIGN KEY(DepartmentId) REFERENCES Departments(Id)
);

CREATE TABLE Computers (
    Id	INTEGER NOT NULL PRIMARY KEY IDENTITY,
    PurchaseDate	DATE NOT NULL,
    DecommisionDate DATE,
	Make VARCHAR(80) NOT NULL,
	Model VARCHAR(80) NOT NULL
);


CREATE TABLE TrainingPrograms (
    Id	integer NOT NULL PRIMARY KEY IDENTITY,
    ProgramName	varchar(80) NOT NULL,
    StartDate	DATE NOT NULL,
    EmdDate	DATE,
    MaxAttendees integer NOT NULL
);

CREATE TABLE EmployeeComputers (
    Id	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    EmployeeId	INTEGER NOT NULL,
    ComputerId 	INTEGER NOT NULL,
    AssignedDate DATE NOT NULL,
    CONSTRAINT FK_EmployeeComputer FOREIGN KEY(EmployeeId) REFERENCES Employees(Id),
    CONSTRAINT FK_ComputerEmployee FOREIGN KEY(ComputerId) REFERENCES Computers(Id)

);

CREATE TABLE EmployeeTrainingRegiments (
    Id	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    EmployeeId	INTEGER NOT NULL,
    EmployeeTrainingId 	INTEGER NOT NULL,
    CONSTRAINT FK_EmployeeTraining FOREIGN KEY(EmployeeId) REFERENCES Employees(Id),
    CONSTRAINT FK_TrainingEmployees FOREIGN KEY(EmployeeTrainingId) REFERENCES TrainingPrograms(Id)

);

-- External Table

CREATE TABLE Customers (
    Id	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    FirstName	VARCHAR(80) NOT NULL,
    LastName 	VARCHAR(80) NOT NULL,
    AccountCreationDate DATE NOT NULL,
	LastLoginDate DATE NOT NULL

);

CREATE TABLE ProductTypes (
    Id	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    ProductType	VARCHAR(80) NOT NULL,
   
);

CREATE TABLE Products (
    Id	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    Title	VARCHAR(80) NOT NULL,
    Price 	FLOAT NOT NULL,
    Description VARCHAR(80) NOT NULL,
	Quantity INTEGER NOT NULL,
	ProductTypeId INTEGER NOT NULL,
	CustomerId INTEGER NOT NULL,
    CONSTRAINT FK_ProductTypeId FOREIGN KEY(ProductTypeId) REFERENCES ProductTypes(Id),
    CONSTRAINT FK_CustomerProduct FOREIGN KEY(CustomerId) REFERENCES Customers(Id)

);

CREATE TABLE Payments (
    Id	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    TypeAccountNumber	INTEGER NOT NULL,
    Type 	   VARCHAR(80) NOT NULL,
    BillingAddress VARCHAR(100) NOT NULL,
	CustomerId INTEGER NOT NULL,
	CONSTRAINT FK_CustomerPayment FOREIGN KEY(CustomerId) REFERENCES Customers(Id),
);

CREATE TABLE Orders (
    Id	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    PaymentId	INTEGER NOT NULL,
    CustomerId 	INTEGER NOT NULL,
	CONSTRAINT FK_PaymentId FOREIGN KEY(PaymentId) REFERENCES Payments(Id),
	CONSTRAINT FK_CustomerOrders FOREIGN KEY(CustomerId) REFERENCES Customers(Id)
);

CREATE TABLE ProductOrders (
    Id	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    OrderId	    INTEGER NOT NULL,
    ProductId 	INTEGER NOT NULL,
	CONSTRAINT FK_OrderId FOREIGN KEY(OrderId) REFERENCES Orders(Id),
	CONSTRAINT FK_ProductId FOREIGN KEY(ProductId) REFERENCES Products(Id)
	
);
