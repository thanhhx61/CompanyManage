using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagement.Models;
using EmployeeManagement.Service;
using EmployeeManagement.Models.Offices;
using EmployeeManagement.Models.Employee;
using Microsoft.AspNetCore.Hosting;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private static int officeId;
        private static string officeName;
        private readonly IUserService userService;
        private readonly IOfficeService officeService;
        private readonly IEmployeeService employeeService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IUserService userService, IOfficeService officeService, IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment)
        {
            this.userService = userService;
            this.officeService = officeService;
            this.employeeService = employeeService;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {

            return View();

        }
        // show ofice for user
        public async Task<IActionResult> OffIndex(int? pageNumber, int? pageSize, string keyword)
        {
            var offices = await officeService.GetOffices();
            var pagination = new Pagination(offices.Count, pageNumber, pageSize, null);
            keyword = keyword == "''" ? string.Empty : keyword;
            var offi = string.IsNullOrEmpty(keyword) ? offices : offices.Where(b => b.OfficeName.Contains(keyword)).ToList();
            var offiView = new OfficeView()
            {
                Offices = offi.Skip((pagination.CurrentPage - 1) * pagination.PageSize).Take(pagination.PageSize).ToList(),
                Pagination = pagination
            };
            return View(offiView);
        }

        [HttpGet("/Home/View/{employeeId}")]
        public async Task<IActionResult> View(int employeeId)
        {
            var employee = await employeeService.GetEmployeeById(employeeId);
            var viewemployee = new ViewEmployee()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                ExistPhoto = employee.Photo,
                Dob = employee.Dob,
                Street = employee.Street,
                Workingday = employee.Workingday,
                Offices = employee.office,
                Skill = employee.Skill,
                Introduce = employee.Introduce,
                OfficeId = employee.OfficeId
                

            };
            ViewBag.OfficeId = officeId;
            return View(viewemployee);
        }

    }
}
