using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;
        public PersonRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Person> GetPerson(int id)
        {
            var person = await _context.People.SingleOrDefaultAsync(p => p.Id == id);
            return person;
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {               
            var people = await _context.People
                .Include(p => p.Department)
                .Include(p => p.Phone)
                .Include(p => p.Position)
                .ToListAsync();
            return people;
        }
    }
}
