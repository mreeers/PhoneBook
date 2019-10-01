using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;
        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Department> GetDepartment(int id)
        {
            var department = await _context.Departments.SingleOrDefaultAsync(p => p.Id == id);
            return department;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            return departments;
        }
    }
}
