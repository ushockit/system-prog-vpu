using Services;
using Services.Abstract;
using Services.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.People;
using WebApplication1.Services.Abstract;

namespace WebApplication1.Services.Impl
{
    public class WebPeopleService : IWebPeopleService
    {
        readonly IServiceManager serviceManager;
        public WebPeopleService(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        public void CreateNewPerson(CreatePersonModel person)
        {
            serviceManager.PeopleService.CreateNewPerson(new PersonDto
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birth = person.Birth
            });
        }

        public List<PersonModel> GetAllPeople()
        {
            var people = serviceManager.PeopleService.GetAllPeople();
            return people.Select((p) => new PersonModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birth = p.Birth
            }).ToList();
        }

        public PersonModel GetPersonById(Guid id)
        {
            var person = serviceManager.PeopleService.GetPersonById(id);
            return new PersonModel
            {
                Id = person.Id,
                Birth = person.Birth,
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }

        public void RemovePersonById(Guid id)
        {
            serviceManager.PeopleService.RemovePersonById(id);
        }

        public void UpdatePerson(PersonModel person)
        {
            serviceManager.PeopleService.UpdatePerson(new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birth = person.Birth
            });
        }
    }
}
