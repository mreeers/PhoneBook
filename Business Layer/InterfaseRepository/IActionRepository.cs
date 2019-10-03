using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.InterfaseRepository
{
    public interface IActionRepository<T>
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetOAll();
        Task<T> Find(int? id);
    }
}
