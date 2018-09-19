using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstSprint.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public ProductTypes ProductType { get; set; }
        public int ProductTypesId { get; set; }
        public Customers Customer { get; set; }
        public int CustomerId { get; set;}
    }
}
//author: Emily, this code is getting and setting all of the data from Products