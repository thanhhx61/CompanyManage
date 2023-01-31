using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Service
{
   public interface IEmployeeService
    {
        Task<Employees> Create(Employees createEmployee);
        Task<Employees> GetEmployeeById(int employeeId);
        Task<Employees> Modify(Employees employee);
        Task<Employees> Remove(int employeeId);
        Task<Employees> Restore(int employeeId);
    }
}
