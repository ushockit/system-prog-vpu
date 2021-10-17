using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.People;

namespace WebApplication1.Services.Abstract
{
    public interface IWebPeopleService
    {
        List<PersonModel> GetAllPeople();
        PersonModel GetPersonById(Guid id);
        void UpdatePerson(PersonModel person);
        void CreateNewPerson(CreatePersonModel person);
        void RemovePersonById(Guid id);
    }
}
