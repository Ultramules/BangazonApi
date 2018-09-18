Select o.*, po.*, p.* FROM Orders o
JOIN ProductOrders po ON o.OrderId = po.OrderId
JOIN Products p ON p.Id = po.ProductId;

SELECT * FROM Products
SELECT * FROM Orders
SELECT * FROM ProductOrders WHERE OrderId = 1