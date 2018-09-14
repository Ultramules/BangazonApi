

DELETE FROM Departments;
DELETE FROM Employees;
DELETE FROM Computers;
DELETE FROM TrainingPrograms;
DELETE FROM EmployeeComputers;
DELETE FROM EmployeeTrainingRegiments;
DELETE FROM Customers;
DELETE FROM ProductTypes;
DELETE FROM Products;
DELETE FROM Payments;
DELETE FROM Orders;
DELETE FROM ProductOrders;




ALTER TABLE Employees DROP CONSTRAINT [FK_Department_Employees];
ALTER TABLE EmployeeComputers DROP CONSTRAINT [FK_EmployeeComputer];
ALTER TABLE EmployeeComputers DROP CONSTRAINT [FK_ComputerEmployee];
ALTER TABLE EmployeeTrainingRegiments DROP CONSTRAINT [FK_EmployeeTraining];
ALTER TABLE EmployeeTrainingRegiments DROP CONSTRAINT [FK_TrainingEmployees];
ALTER TABLE Products DROP CONSTRAINT [FK_ProductTypeId];
ALTER TABLE Products DROP CONSTRAINT [FK_CustomerId];
ALTER TABLE Payments DROP CONSTRAINT [FK_CustomerPayment];
ALTER TABLE Orders DROP CONSTRAINT [FK_PaymentId];
ALTER TABLE Orders DROP CONSTRAINT [FK_CustomerOrders];
ALTER TABLE ProductOrders DROP CONSTRAINT [FK_OrderId];
ALTER TABLE ProductOrders DROP CONSTRAINT [FK_ProductId];



DROP TABLE IF EXISTS Departments;
DROP TABLE IF EXISTS Employees;
DROP TABLE IF EXISTS Computers;
DROP TABLE IF EXISTS TrainingPrograms;
DROP TABLE IF EXISTS EmployeeComputers;
DROP TABLE IF EXISTS EmployeeTrainingRegiments;
DROP TABLE IF EXISTS Customers;
DROP TABLE IF EXISTS ProductTypes;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Payments;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS ProductOrders;

