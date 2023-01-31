using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Entities
{
    public class Offices
    {
        [Key]
        public int OfficeId { get; set; }
        [Required]
        [MaxLength(300)]
        public string OfficeName { get; set; }
        [Required]
        [MaxLength(300)]
        public string Street { get; set; }
        [Required]
        [MaxLength(300)]
        public int Amount { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public ICollection<Employees> Employees { get; set; }
    }
}
