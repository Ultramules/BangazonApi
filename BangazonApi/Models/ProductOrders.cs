using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstSprint.Models
{
    public class ProductOrders
    {
        public int Id { get; set; }
        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
//CREATE TABLE ProductOrders(
//    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
//    OrderId INTEGER NOT NULL,
//    ProductId INTEGER NOT NULL,
//    CONSTRAINT FK_OrderId FOREIGN KEY(OrderId) REFERENCES Orders(Id),
//    CONSTRAINT FK_ProductId FOREIGN KEY(ProductId) REFERENCES Products(Id)


//);
