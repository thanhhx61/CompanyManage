using EmployeeManagement.DBContexts;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Service
{
    public class OfficeService : IOfficeService
    {
        private readonly CompanyDBContext context;
        public OfficeService(CompanyDBContext context)
        {
            this.context = context;
        }
        public async Task<List<Offices>> GetOffices()
        {
            return await context.Offices.Include(e => e.Employees).ToListAsync();
        }

        public async Task<Offices> GetOfficesById(int officeId)
        {
            return await context.Offices.Include(e => e.Employees).FirstOrDefaultAsync(o => o.OfficeId == officeId);
        }
        public async Task<Offices> Remove(int officeId)
        {
            try
            {
                var off = await GetOfficesById(officeId);
                off.IsDeleted = true;
                context.Attach(off);
                context.Entry<Offices>(off).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return off;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Offices> Restore(int officeId)
        {
            try
            {
                var off = await GetOfficesById(officeId);
                off.IsDeleted = false;
                context.Attach(off);
                context.Entry<Offices>(off).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return off;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
