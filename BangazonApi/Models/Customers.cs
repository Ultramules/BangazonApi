using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstSprint.Models
{
    public class Customers
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AccountCreationDate { get; set; }

        private DateTime lastLoginDate;

        public DateTime GetLastLoginDate()
        {
            return lastLoginDate;
        }

        public void SetLastLoginDate(DateTime value)
        {
            lastLoginDate = value;
        }
    }
}
