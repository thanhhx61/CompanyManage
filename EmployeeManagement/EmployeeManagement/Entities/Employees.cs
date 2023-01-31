using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Entities
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(300)]
        public string Photo { get; set; }
        [Required]
        [MaxLength(500)]
        public string EmployeeName { get; set; }
        [Required]
        [MaxLength(500)] 
        public DateTime Dob { get; set; }
        [Required]
        [MaxLength(500)]
        public string Street { get; set; }
        [Required]
        [MaxLength(500)]
        public DateTime Workingday { get; set; }
        [Required]
        [MaxLength(500)]
        public string Skill { get; set; }
        [Required]
        [MaxLength(500)]
        public string Introduce { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int OfficeId { get; set; }
        public Offices office { get; set; }
    }
}
