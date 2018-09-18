using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstSprint.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Supervisor { get; set; }
        public Departments Department { get; set; }
        public int DepartmentsId { get; set; }
        public Computers Computer { get; set; }
        public int ComputerId { get; set; }
    }
}

//purpose: the purpose of this model is to hold the info for Employees
// author: Adelaide
//methods: No methods


