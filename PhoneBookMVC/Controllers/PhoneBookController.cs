using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Domain.Models;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneBookMVC.ViewModel;
using System.Collections;
using AutoMapper;

namespace PhoneBookMVC.Controllers
{
    public class PhoneBookController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PhoneBookController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index(string searchString)
        {
            IEnumerable<Person> people = _context.People
                 .Include(p => p.Department)
                 .Include(p => p.Phone)
                 .Include(p => p.Position);

            var departmens = _context.Departments.ToList();

            var searchPeople = from m in _context.People select m;

            List<PeopleInTheDepartment> peopleInTheDepartments = new List<PeopleInTheDepartment>();
            foreach (var n in departmens)
            {
                PeopleInTheDepartment departmentWhichPeople = new PeopleInTheDepartment();

                if (!String.IsNullOrEmpty(searchString))
                {
                    departmentWhichPeople.People = people.Where(p => p.SecondName.Contains(searchString)).ToList();
                }
                else
                {
                    departmentWhichPeople.People = people.Where(p => p.DepartmentId == n.Id).ToList();
                    departmentWhichPeople.DepartmentId = n.Id;
                    departmentWhichPeople.Department = n;
                }


                peopleInTheDepartments.Add(departmentWhichPeople);
            }

            return View(peopleInTheDepartments);
        }


        //GET: /Create
        public IActionResult Create()
        {
            var departmens = _context.Departments.ToList();
            ViewBag.Departments = new SelectList(departmens, "Id", "Title");
            var positions = _context.Positions.ToList();
            ViewBag.Positions = new SelectList(positions, "Id", "Title");
            
            return View();
        }

        //POST: Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind("Id,FirstName,SecondName,MiddleName,Email,DepartmentId,PositionId, phoneNumber")]PersonForCreateDto personForCreateDto)
        {
            var person = _mapper.Map<Person>(personForCreateDto);
            var phone = await _context.Phones.SingleOrDefaultAsync(p => p.PhoneNumber == personForCreateDto.phoneNumber);

            if (phone == null)
            {
                phone = new Phone()
                {
                    PhoneNumber = personForCreateDto.phoneNumber
                };
            
                //phone.PhoneNumber = personForCreateDto.phoneNumber;
                await _context.Phones.AddAsync(phone);
                await _context.SaveChangesAsync();
            }
            person.PhoneId = phone.Id;

            /*if(phone != null)
            {
                person.PhoneId = phone.Id;
            }
            else
            {
                phone.PhoneNumber = personForCreateDto.phoneNumber;
                await _context.Phones.AddAsync(phone);
                await _context.SaveChangesAsync();
                person.PhoneId = phone.Id;

            }*/

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