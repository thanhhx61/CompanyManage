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
using EmployeeManagement.Models.Offices;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    public class OfficesController : Controller
    {
        private readonly IOfficeService officeService;

        public OfficesController(IOfficeService officeService)
        {
            this.officeService = officeService;
        }

        public async Task<IActionResult> Index(int? pageNumber, int? pageSize, string keyword)
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
        [HttpGet("/Offices/Remove/{officeId}")]
        public async Task<IActionResult> Remove(int officeId)
        {
            await officeService.Remove(officeId);
            return RedirectToAction("Index");
        }
        [HttpGet("/Offices/Restore/{officeId}")]
        public async Task<IActionResult> Restore(int officeId)
        {
            await officeService.Restore(officeId);
            return RedirectToAction("Index"); ;
        }
    }
}
