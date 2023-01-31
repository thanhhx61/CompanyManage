using EmployeeManagement.DBContexts;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly CompanyDBContext context;
        public EmployeeService(CompanyDBContext context)
        {
            this.context = context;
        }
        public async Task<Employees> Create(Employees createEmployee)
        {
            try
            {
                context.Add(createEmployee);
                var employeeId = await context.SaveChangesAsync();
                createEmployee.EmployeeId = employeeId;
                return createEmployee;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Employees> GetEmployeeById(int employeeId)
        {
            return await context.Employees.Include(e => e.office).FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<Employees> Modify(Employees employee)
        {
            try
            {
                context.Attach(employee);
                context.Entry<Employees>(employee).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return employee;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Employees> Remove(int employeeId)
        {
            try
            {
                var employee = await GetEmployeeById(employeeId);
                employee.IsDeleted = true;
                context.Attach(employee);
                context.Entry<Employees>(employee).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return employee;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Employees> Restore(int employeeId)
        {
            try
            {
                var employee = await GetEmployeeById(employeeId);
                employee.IsDeleted = false;
                context.Attach(employee);
                context.Entry<Employees>(employee).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return employee;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
