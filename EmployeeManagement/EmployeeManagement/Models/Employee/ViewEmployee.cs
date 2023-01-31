using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.Employee
{
    public class ViewEmployee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "The Employee name is required")]
        [MaxLength(500)]
        public string EmployeeName { get; set; }

        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "The Day Of Birth is required")]
        [MaxLength(500)]
        [Range(minimum: 1990, maximum: 2000)]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "The Street is required")]
        [MaxLength(500)]
        public string Street { get; set; }
        [Required(ErrorMessage = "The Day Of Birth is required")]
        [MaxLength(500)]
        public DateTime Workingday { get; set; }
        [Required(ErrorMessage = "The Skill is required")]
        [MaxLength(500)]
        public string Skill { get; set; }
        [Required(ErrorMessage = "The Introduce is required")]
        [MaxLength(500)]
        public string Introduce { get; set; }
        public string ExistPhoto { get; set; }
        public int OfficeId { get; set; }

        public Entities.Offices Offices { get; set; }
    }
}
