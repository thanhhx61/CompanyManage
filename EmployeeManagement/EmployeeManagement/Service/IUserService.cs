using EmployeeManagement.Entities;
using EmployeeManagement.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Service
{
   public interface IUserService
    {
        Task<LoginResult> Login(Login LoginUser);
        void Signout();
        Task<RegisterResult> Register(Register register);
        Task<List<AppIdentityUser>> GetUsers();
    }
}
