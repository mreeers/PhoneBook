using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartment(int id);
        Task<IEnumerable<Department>> GetDepartments();
    }
}
