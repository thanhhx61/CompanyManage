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
    [Authorize(Roles = "SystemAdmin")]
    public class EmployeeController : Controller
    {
        private static int officeId;
        private static string officeName;
        private readonly IOfficeService officeService;
        private readonly IEmployeeService employeeService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeController(IOfficeService officeService,
                                  IEmployeeService employeeService,
                                  IWebHostEnvironment webHostEnvironment)
        {
            this.officeService = officeService;
            this.employeeService = employeeService;
            this.webHostEnvironment = webHostEnvironment;
        }
        [Route("/Employee/Index/{offiId=1}/{pageNumber=1}/{pageSize=5000}/{keyword=''}")]
        public async Task<IActionResult> Index(int offiId, int? pageNumber, int? pageSize, string keyword)
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


        public async Task<IActionResult> Create(CreateEmployee createEmployee)
        {

            if (ModelState.IsValid)
            {
                string filename = "No-Image.png";
                if (createEmployee.Photo != null)
                {
                    string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\Images");
                    filename = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{createEmployee.Photo.FileName}";
                    string fullPath = Path.Combine(folderPath, filename);
                    using (var file = new FileStream(fullPath, FileMode.Create))
                    {
                        createEmployee.Photo.CopyTo(file);
                    }
                }
                var newEmployee = new Employees()
                {
                    Photo = $"/Images/{filename}",
                    EmployeeName = createEmployee.EmployeeName,
                    Dob = createEmployee.Dob,
                    IsDeleted = false,
                    Street = createEmployee.Street,
                    Workingday = createEmployee.Workingday,
                    Skill = createEmployee.Skill,
                    Introduce = createEmployee.Introduce,
                    OfficeId = officeId
                };
                await employeeService.Create(newEmployee);
                return RedirectToAction("Index", "Employee", new { offiId = officeId });
            }
            ViewBag.OfficeName = officeName;
            ViewBag.OfficeId = officeId;
            return View();
        }


        [HttpGet("/Employee/View/{employeeId}")]
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
                Introduce = employee.Introduce,
                Skill = employee.Skill

            };
            ViewBag.OfficeId = officeId;
            return View(viewemployee);
        }
        [HttpGet("/Employee/Modify/{employeeId}")]
        public async Task<IActionResult> Modify(int employeeId)
        {
            ViewBag.OfficeName = officeName;
            ViewBag.OfficeId = officeId;
            var employee = await employeeService.GetEmployeeById(employeeId);
            var modifyEmployee = new ModifyEmployee()
            {
                EmployeeName = employee.EmployeeName,
                Dob = employee.Dob,
                Street = employee.Street,
                Workingday = employee.Workingday,
                ExistPhoto = employee.Photo,
                Skill = employee.Skill,
                Introduce = employee.Introduce,
                EmployeeId = employee.EmployeeId
            };
            
            return View(modifyEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> Modify(ModifyEmployee modifyEmployee)
        {
            if (ModelState.IsValid)
            {
                var employee = await employeeService.GetEmployeeById(modifyEmployee.EmployeeId);
                if (employee != null)
                {
                    string fileName = employee.Photo;
                    if (modifyEmployee.Photo != null)
                    {
                        //Deleted old photo
                        var oldFileName = fileName.Split("/")[2];
                        if (string.Compare(oldFileName, "No-Image.png") != 0)
                        {
                            System.IO.File.Delete(Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\Images\", oldFileName));

                        }
                        string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\Images\");
                        fileName = $"{DateTime.Now.ToString("ddMMyyyy")}_{modifyEmployee.Photo.FileName}";
                        string fullPath = Path.Combine(folderPath, fileName);
                        using (var file = new FileStream(fullPath, FileMode.Create))
                        {
                            modifyEmployee.Photo.CopyTo(file);
                        }
                    }
                    employee.Photo = modifyEmployee.Photo != null ? $"/Images/{fileName}" : fileName;
                    employee.EmployeeName = modifyEmployee.EmployeeName;
                    employee.Dob = modifyEmployee.Dob;
                    employee.Street = modifyEmployee.Street;
                    employee.Workingday = modifyEmployee.Workingday;
                    employee.Skill = modifyEmployee.Skill;
                    employee.Introduce = modifyEmployee.Introduce;
                 

                    await employeeService.Modify(employee);
                    return RedirectToAction("Index", new { offiId = officeId });
                }
            }
            ViewBag.OfficeName = officeName;
            ViewBag.OfficeId = officeId;
            return View();
        }
        [HttpGet("/Employee/Remove/{employeeId}")]
        public async Task<IActionResult> Remove(int employeeId)
        {
            await employeeService.Remove(employeeId);
            return RedirectToAction("Index", "Employee", new { offiId = officeId });
        }

        [HttpGet("/Employee/Restore/{employeeId}")]
        public async Task<IActionResult> Restore(int employeeId)
        {
            await employeeService.Restore(employeeId);
            return RedirectToAction("Index", "Employee", new { offiId = officeId });
        }
        protected void SetAlert(string message, int type)
        {
            TempData["AlertMessage"] = message;
            if (type == 1)
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == 2)
            {
                TempData["AlertType"] = "alert-warning";
            }

        }
    }

    
}
