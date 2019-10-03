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
    public class PositionRepository : IActionRepository<Position>
    {
        private readonly DataContext _context;
        public PositionRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Position> Find(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Position> Get(int id)
        {
            var position = await _context.Positions.SingleOrDefaultAsync(p => p.Id == id);
            return position;
        }

        public async Task<IEnumerable<Position>> GetOAll()
        {
            var departments = await _context.Positions.ToListAsync();
            return departments;
        }
    }
}
