Select * FROM Orders o
JOIN ProductOrders po ON o.OrderId = po.OrderId
JOIN Products p ON p.ProductId = po.ProductId;

SELECT * FROM Products
SELECT * FROM Orders
SELECT * FROM ProductOrders WHERE OrderId = 1

SELECT o.OrderId, o.PaymentId, o.CustomerId, p.* FROM Orders o
JOIN ProductOrders po ON o.OrderId = po.OrderId
JOIN Products p ON p.ProductId = po.ProductId;