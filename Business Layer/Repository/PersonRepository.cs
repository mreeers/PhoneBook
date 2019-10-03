using Business_Layer.InterfaseRepository;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repository
{
    public class PersonRepository : IActionRepository<Person>
    {
        private readonly DataContext _context;
        public PersonRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Person> Find(int? id)
        {
            var person = await _context.People.FindAsync(id);
            return person;
        }

        public async Task<Person> Get(int id)
        {
            var person = await _context.People.SingleOrDefaultAsync(p => p.Id == id);
            return person;
        }

        public async Task<IEnumerable<Person>> GetOAll()
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
