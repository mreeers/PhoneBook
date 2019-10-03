using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_Layer.InterfaseRepository;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBookMVC.Controllers
{
    public class PositionsController : Controller
    {
        private readonly DataContext _context;
        private readonly IActionRepository<Position> _positionRepository;

        public PositionsController(DataContext context, IActionRepository<Position> positionRepository)
        {
            _context = context;
            _positionRepository = positionRepository;
        }

        public async Task<IActionResult> Index()
        {   
            IEnumerable<Position> positions = await _positionRepository.GetOAll();
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