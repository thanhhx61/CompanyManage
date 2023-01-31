using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Service
{
   public interface IOfficeService
    {
        Task<List<Offices>> GetOffices();
        Task<Offices> GetOfficesById(int officeId);

        Task<Offices> Remove(int officeId);
        Task<Offices> Restore(int officeId);
    }
}
