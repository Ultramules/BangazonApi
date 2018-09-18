using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAPI.Models
{

    public class Employees
    {

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsSupervisor { get; set; }

        public int DepartmentId { get; set; }

        public Departments Department { get; set; }

        public Computers Computer { get; set; }


    }
}
