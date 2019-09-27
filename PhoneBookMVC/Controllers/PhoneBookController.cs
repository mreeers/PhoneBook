using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Domain.Models;

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





    }
}