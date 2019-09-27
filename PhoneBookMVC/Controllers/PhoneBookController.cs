using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Domain.Models;
using System.Threading.Tasks;

namespace PhoneBookMVC.Controllers
{
    public class PhoneBookController : Controller
    {
        private readonly DataContext _context;

        public PhoneBookController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Person> people = _context.People
                .Include(p => p.Department)
                .Include(p => p.Phone)
                .Include(p => p.Position)
                .ToList();

            return View(people);
        }

        //GET: /Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }





    }
}