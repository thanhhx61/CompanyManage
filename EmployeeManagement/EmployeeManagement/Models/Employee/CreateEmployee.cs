using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Employee
{
    public class CreateEmployee
    {
        [Required(ErrorMessage = "The Employee name is required")]
        [MaxLength(500)]
        public string EmployeeName { get; set; }
        
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "The Day Of Birth is required")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1980", "1/1/2001")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "The Street is required")]
        [MaxLength(500)]
        public string Street { get; set; }
        [Required(ErrorMessage = "The Day Of Birth is required")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "3/1/2001", "1/1/2050")]
        public DateTime Workingday { get; set; }
        [Required(ErrorMessage = "The Skill is required")]
        [MaxLength(500)]
        public string Skill { get; set; }
        [Required(ErrorMessage = "The Introduce is required")]
        [MaxLength(500)]
        public string Introduce { get; set; }
        public int OfficeId { get; set; }
    }
}
