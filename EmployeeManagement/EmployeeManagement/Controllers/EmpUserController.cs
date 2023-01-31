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
using Microsoft.AspNetCore.Hosting;
using EmployeeManagement.Models.Employee;
using System.IO;
using EmployeeManagement.Entities;

namespace EmployeeManagement.Controllers
{
   
    public class EmpUserController : Controller
    {
        private static int officeId;
        private static string officeName;
        private readonly IOfficeService officeService;
        private readonly IEmployeeService employeeService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmpUserController(IOfficeService officeService,
                                  IEmployeeService employeeService,
                                  IWebHostEnvironment webHostEnvironment)
        {
            this.officeService = officeService;
            this.employeeService = employeeService;
            this.webHostEnvironment = webHostEnvironment;
        }
        [Route("/EmpUser/EmpIndex/{offiId=1}/{pageNumber=1}/{pageSize=10}/{keyword=''}")]
        public async Task<IActionResult> EmpIndex(int offiId, int? pageNumber, int? pageSize, string keyword)
        {
            officeId = offiId;
            var offices = await officeService.GetOfficesById(offiId);
            officeName = offices.OfficeName;
            var pagination = new Pagination(offices.Employees.Count, pageNumber, pageSize, keyword);
            keyword = keyword == "''" ? string.Empty : keyword;
            var employees = string.IsNullOrEmpty(keyword) ? offices.Employees : offices.Employees.Where(b => b.EmployeeName.Contains(keyword)).ToList();
            employees = employees.OrderByDescending(b => b.EmployeeId).ToList().Skip((pagination.CurrentPage - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
            offices.Employees = employees;
            var listemployee = new ListEmployee()
            {

                Offices = offices,
                Pagination = pagination
            };
            ViewBag.OfficeName = officeName;
            ViewBag.OfficeId = officeId;
            return View(listemployee);
        }

       
    }

    
}
