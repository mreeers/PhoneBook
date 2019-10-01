using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IPersonRepository
    {
        Task<Person> GetPerson(int id);
        Task<IEnumerable<Person>> GetPersons();
    }
}
