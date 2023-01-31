using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Service;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    public class DashboardController : Controller
    {
        private readonly IUserService userService;
        private readonly IOfficeService officeService;
        private readonly IEmployeeService employeeService;

        public DashboardController(IUserService userService, IOfficeService officeService, IEmployeeService employeeService)
        {
            this.userService = userService;
            this.officeService = officeService;
            this.employeeService = employeeService;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
