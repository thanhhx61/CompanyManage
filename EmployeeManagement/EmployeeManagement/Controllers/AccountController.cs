﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagement.Models;
using EmployeeManagement.Service;
using EmployeeManagement.Models.Account;

namespace EmployeeManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.Login(login);
                if (result.Success && result.Roles.Length > 0)
                {
                    if (result.Roles.Contains("SystemAdmin"))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ViewBag.Error = result.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.Register(register);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Error = result.Message;
                return View();
            }
            return View();
        }
        public IActionResult SignOut()
        {
            userService.Signout();
            return RedirectToAction("Login", "Account");
        }
    }
}
