using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Offices
{
    public class OfficeView
    {
        public List<Entities.Offices> Offices { get; set; }
        public Pagination Pagination { get; set; }
    }
}
