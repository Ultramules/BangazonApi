INSERT INTO Departments (Name, ExpenseBudget) VALUES
('IT Department', 4000),
('Creative Department', 5000),
('Music Department', 2000);


INSERT INTO Computers (PurchaseDate, DecommisionDate, Make, Model) VALUES 
('2012-02-01', '2016-02-04', 'HP', 'EliteBook'),
('2017-02-01', '2018-02-04', 'HP', 'EliteBook'),
('2015-02-01', '2016-02-07', 'Mac', 'Air');


INSERT INTO TrainingPrograms (ProgramName, StartDate, EmdDate, MaxAttendees) VALUES 
('IT Training', '2018-02-14', '2018-10-12', 10),
('HR Training', '2018-02-14', '2018-02-25', 10),
('Design Training', '2018-02-16', '2018-02-20', 20);

INSERT INTO Employees (FirstName, LastName, Supervisor, DepartmentId) VALUES 
('Beth', 'Johnson', 0, 1),
('John', 'Sherby', 1, 2),
('Mary', 'Luke', 0, 3);



INSERT INTO EmployeeComputers (EmployeeId,ComputerId, AssignedDate) VALUES 
(2, 2, '2018-12-13'),
(2, 2,'2014-12-11'),
(3, 3,'2018-02-13');

INSERT INTO EmployeeTrainingRegiments (EmployeeId, EmployeeTrainingId) VALUES
(2,2),
(3, 1),
(2, 3);

SELECT * FROM EmployeeTrainingRegiments;
--External
INSERT INTO Customers (FirstName, LastName, AccountCreationDate, LastLoginDate) VALUES
('Bill','Jean','2015-02-12','2018-09-12'),
('George','Brown','2017-01-29','2018-10-12'),
('Betsy','Jones', '2016-02-16', '2018-10-11');

INSERT INTO ProductTypes (ProductType) VALUES
('Fruit'),
('Vegetables'),
('Shoes');

INSERT INTO Products
 (Title, Price, Description, Quantity, ProductTypeId, CustomerId) VALUES
 ('Apple', 1000,'Red Apple', 1200, 2, 2),
 ('Peach', 3000,'Georgia Peach', 1000, 3, 3),
 ('Grapes', 1500,'Red Grapes', 1400, 2, 3);

INSERT INTO Payments (TypeAccountNumber, Type, BillingAddress,CustomerId) VALUES
(12321, 'Mastercard', '123 Apple Ct', 1),
(12356, 'Visa', '242 Orange St', 2),
(13553, 'Chase', '234 Peach Circle',1);

INSERT INTO Orders (Completed, PaymentId, CustomerId) VALUES
(1, 1, 2),
(0, 2, 1),
(1, 3, 3);

INSERT INTO ProductOrders (OrderId, ProductId) VALUES
(1, 2),
(1, 3),
(2, 2),
(3, 3);


