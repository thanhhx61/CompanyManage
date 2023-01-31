using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Employee
{
    public class ListEmployee
    {
        public Entities.Offices Offices { get; set; }
        public Pagination Pagination { get; set; }
    }
}
