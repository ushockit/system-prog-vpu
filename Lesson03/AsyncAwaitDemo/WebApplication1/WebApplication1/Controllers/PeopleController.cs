using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.People;
using WebApplication1.Services;
using WebApplication1.Services.Abstract;

namespace WebApplication1.Controllers
{
    public class PeopleController : Controller
    {
        readonly IWebPeopleService peopleService;

        public PeopleController(IWebPeopleService peopleService)
        {
            this.peopleService = peopleService;
        }

        // [HttpGet]
        public IActionResult Index()
        {
            var people = peopleService.GetAllPeople();
            return View(new PeopleIndexViewModel
            {
                People = peopleService.GetAllPeople(),
                MaxPerson = people.FirstOrDefault(p => p.Birth.Ticks == people.Min((p) => p.Birth.Ticks)),
                MinPerson = people.FirstOrDefault(p => p.Birth.Ticks == people.Max((p) => p.Birth.Ticks)),
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePersonModel person)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Model is not valid!");
                return View(person);
            }
            peopleService.CreateNewPerson(person);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid? id)
        {
            if (id is null || peopleService.GetPersonById((Guid)id) is null)
            {
                return BadRequest("Person was not found");
            }
            peopleService.RemovePersonById((Guid)id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid? id)
        {
            if (id is null || peopleService.GetPersonById((Guid)id) is null)
            {
                return BadRequest("Person was not found");
            }
            return View(peopleService.GetPersonById((Guid)id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PersonModel person)
        {
            if (peopleService.GetPersonById((Guid)person.Id) is null)
            {
                return BadRequest("Person was not found");
            }

            peopleService.UpdatePerson(person);
            return View(peopleService.GetPersonById(person.Id));
        }

        public IActionResult Detail(Guid? id)
        {
            if (id is null || peopleService.GetPersonById((Guid)id) is null)
            {
                return BadRequest("Person was not found");
            }
            return View(peopleService.GetPersonById((Guid)id));
        }
    }
}
