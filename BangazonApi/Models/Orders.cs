using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstSprint.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        //public PaymentTypes PaymentType { get; set; }
        public Customers Customer { get; set; }
        public List<Products> Products { get; set; }
    }
}
//CREATE TABLE Orders(
//    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
//    PaymentId INTEGER NOT NULL,
//    CustomerId INTEGER NOT NULL,
//    CONSTRAINT FK_PaymentId FOREIGN KEY(PaymentId) REFERENCES Payments(Id),
//    CONSTRAINT FK_CustomerOrders FOREIGN KEY(CustomerId) REFERENCES Customers(Id)
//);
