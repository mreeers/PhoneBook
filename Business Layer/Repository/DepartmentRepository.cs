using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Business_Layer.InterfaseRepository;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Business_Layer.Repository
{
    public class DepartmentRepository : IActionRepository<Department>
    {
        private readonly DataContext _context;
        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Department> Find(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Department> Get(int id)
        {
            var department = await _context.Departments.SingleOrDefaultAsync(p => p.Id == id);
            return department;
        }

        public async Task<IEnumerable<Department>> GetOAll()
        {
            var departments = await _context.Departments.ToListAsync();
            return departments;
        }
    }
}
