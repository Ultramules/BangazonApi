

-- Internal Table

CREATE TABLE Departments (
    DepartmentId	INTEGER NOT NULL PRIMARY KEY IDENTITY,
    Name varchar(80) NOT NULL UNIQUE,
	ExpenseBudget FLOAT NOT NULL

);

CREATE TABLE Employees (
    EmployeeId	INTEGER NOT NULL PRIMARY KEY IDENTITY,
    FirstName	VARCHAR(80) NOT NULL,
    LastName	VARCHAR(80) NOT NULL,
    Supervisor BIT NOT NULL, 
    DepartmentId	INTEGER NOT NULL,
    CONSTRAINT FK_Department_Employees FOREIGN KEY(DepartmentId) REFERENCES Departments(DepartmentId)
);

CREATE TABLE Computers (
    ComputerId	INTEGER NOT NULL PRIMARY KEY IDENTITY,
    PurchaseDate	DATE NOT NULL,
    DecommisionDate DATE,
	Make VARCHAR(80) NOT NULL,
	Model VARCHAR(80) NOT NULL
);


CREATE TABLE TrainingPrograms (
    TrainingProgramId	integer NOT NULL PRIMARY KEY IDENTITY,
    ProgramName	varchar(80) NOT NULL,
    StartDate	DATE NOT NULL,
    EmdDate	DATE,
    MaxAttendees integer NOT NULL
);

CREATE TABLE EmployeeComputers (
    EmployeeComputerId	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    EmployeeId	INTEGER NOT NULL,
    ComputerId 	INTEGER NOT NULL,
    AssignedDate DATE NOT NULL,
    CONSTRAINT FK_EmployeeComputer FOREIGN KEY(EmployeeId) REFERENCES Employees(EmployeeId),
    CONSTRAINT FK_ComputerEmployee FOREIGN KEY(ComputerId) REFERENCES Computers(ComputerId)

);

CREATE TABLE EmployeeTrainingRegiments (
    EmployeeTrainingRegimentId	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    EmployeeId	INTEGER NOT NULL,
    EmployeeTrainingId 	INTEGER NOT NULL,
    CONSTRAINT FK_EmployeeTraining FOREIGN KEY(EmployeeId) REFERENCES Employees(EmployeeId),
    CONSTRAINT FK_TrainingEmployees FOREIGN KEY(EmployeeTrainingId) REFERENCES TrainingPrograms(TrainingProgramId)

);

-- External Table

CREATE TABLE Customers (
    CustomerId	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    FirstName	VARCHAR(80) NOT NULL,
    LastName 	VARCHAR(80) NOT NULL,
    AccountCreationDate DATE NOT NULL,
	LastLoginDate DATE NOT NULL

);

CREATE TABLE ProductTypes (
    ProductTypeId	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    ProductType	VARCHAR(80) NOT NULL,
   
);

CREATE TABLE Products (
    ProductId	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    Title	VARCHAR(80) NOT NULL,
    Price 	FLOAT NOT NULL,
    Description VARCHAR(80) NOT NULL,
	Quantity INTEGER NOT NULL,
	ProductTypeId INTEGER NOT NULL,
	CustomerId INTEGER NOT NULL,
    CONSTRAINT FK_ProductTypeId FOREIGN KEY(ProductTypeId) REFERENCES ProductTypes(ProductTypeId),
    CONSTRAINT FK_CustomerId FOREIGN KEY(CustomerId) REFERENCES Customers(CustomerId)

);

CREATE TABLE Payments (
    PaymentId	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    TypeAccountNumber	INTEGER NOT NULL,
    Type 	   VARCHAR(80) NOT NULL,
    BillingAddress VARCHAR(100) NOT NULL,
	CustomerId INTEGER NOT NULL,
	CONSTRAINT FK_CustomerPayment FOREIGN KEY(CustomerId) REFERENCES Customers(CustomerId),
);

CREATE TABLE Orders (
    OrderId	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    PaymentId	INTEGER NOT NULL,
    CustomerId 	INTEGER NOT NULL,
	CONSTRAINT FK_PaymentId FOREIGN KEY(PaymentId) REFERENCES Payments(PaymentId),
	CONSTRAINT FK_CustomerOrders FOREIGN KEY(CustomerId) REFERENCES Customers(CustomerId)
);

CREATE TABLE ProductOrders (
    ProductOrderId	        INTEGER NOT NULL PRIMARY KEY IDENTITY,
    OrderId	    INTEGER NOT NULL,
    ProductId 	INTEGER NOT NULL,
	CONSTRAINT FK_OrderId FOREIGN KEY(OrderId) REFERENCES Orders(OrderId),
	CONSTRAINT FK_ProductId FOREIGN KEY(ProductId) REFERENCES Products(ProductId)
	
);
