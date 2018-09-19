using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstSprint.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int TypeAccountNumber { get; set; }
        public string Type { get; set; }
        public string BillingAddress { get; set; }
        public Customers Customer { get; set; }
        public int CustomerId { get; set; }


    }
}

