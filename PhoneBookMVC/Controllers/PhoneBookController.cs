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
using Domain.Repository;

namespace PhoneBookMVC.Controllers
{
    public class PhoneBookController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public PhoneBookController(DataContext context, IMapper mapper, IPersonRepository personRepository, IDepartmentRepository departmentRepository)
        {
            _context = context;
            _mapper = mapper;
            _personRepository = personRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<Person> people = await _personRepository.GetPersons();

            var departmens = await _departmentRepository.GetDepartments();

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
        public async Task<IActionResult> CreateAsync()
        {
            var departmens = await _departmentRepository.GetDepartments();
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