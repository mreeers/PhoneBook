using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBookMVC.Controllers
{
    public class PositionsController : Controller
    {
        private readonly DataContext _context;
        public PositionsController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Position> positions = _context.Positions;
            return View(positions);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id", "Title", "Level")]Position position)
        {
            if (ModelState.IsValid)
            {
                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

    }
}