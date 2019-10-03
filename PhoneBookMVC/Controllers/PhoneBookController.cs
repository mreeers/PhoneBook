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
using Business_Layer.InterfaseRepository;

namespace PhoneBookMVC.Controllers
{
    public class PhoneBookController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IActionRepository<Person> _personRepository;
        private readonly IActionRepository<Department> _departmentRepository;

        public PhoneBookController(DataContext context, IMapper mapper, IActionRepository<Person> personRepository, IActionRepository<Department> departmentRepository)
        {
            _context = context;
            _mapper = mapper;
            _personRepository = personRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<Person> people = await _personRepository.GetOAll();

            var departmens = await _departmentRepository.GetOAll();

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
        public async Task<IActionResult> Create()
        {
            var departmens = await _departmentRepository.GetOAll();
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

                await _context.Phones.AddAsync(phone);
                await _context.SaveChangesAsync();
            }
            person.PhoneId = phone.Id;


            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);

        }

        /////
        ///
        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var departmens = await _departmentRepository.GetOAll();
            ViewBag.Departments = new SelectList(departmens, "Id", "Title");
            var positions = _context.Positions.ToList();
            ViewBag.Positions = new SelectList(positions, "Id", "Title");
            
            var person = await _context.People.FindAsync(id);
            
            if (person == null)
            {
                return NotFound();
            }
            var personForUpdate =  _mapper.Map<PersonForUpdateDto>(person);
            
            return View(personForUpdate);
        }

        //// POST: People/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, PersonForUpdateDto personForUpdateDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Некоректные данные");
        //    }
        //    var person = _mapper.Map<Person>(personForUpdateDto);

        //    var phone = await _context.Phones.SingleOrDefaultAsync(p => p.Id == personForUpdateDto.Id);
        //    if(phone == null)
        //    {
        //        phone = new Phone
        //        {
        //            PhoneNumber = personForUpdateDto.phoneNumber
        //        };
        //        await _context.Phones.AddAsync(phone);
        //        person.PhoneId = phone.Id;
        //    }
        //    if (id != person.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(person);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PersonExists(person.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(person);
        //}

        //private bool PersonExists(int id)
        //    {
        //        return _context.People.Any(e => e.Id == id);
        //    }

    }
    }