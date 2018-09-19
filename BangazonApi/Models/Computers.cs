using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstSprint.Models
{
    public class Computers
    {
        public int ComputerId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime DecommisionDate { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
    }
}

//purpose: the purpose of this model is to hold the info for Computers
// author: Adelaide
//methods: No methods
