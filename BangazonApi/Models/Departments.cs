using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using firstSprint.Models;

namespace BangazonAPI.Models
{
    public class Departments
    {

        [Key]
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public float ExpenseBudget { get; set; }

        public List<Employees> EmployeeList { get; set; }
    }
}

/* 
 AUTHOR: Aaron Miller
 PURPOSE: Model reflects Departments Table 
*/
