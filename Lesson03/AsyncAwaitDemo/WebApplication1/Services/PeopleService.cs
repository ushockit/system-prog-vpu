using AutoMapper;
using Domain.Entity;
using Domain.Repository;
using Services.Abstract;
using Services.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class PeopleService : IPeopleService
    {
        readonly IUnitOfWork unitOfWork;
        readonly IMapper mapper;

        public PeopleService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            unitOfWork = uow;
            this.mapper = mapper;
        }

        public async Task<PersonDto> CreateNewPersonAsync(PersonDto model, CancellationToken token = default)
        {
            var person = new Person
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birth = model.Birth
            };
            await unitOfWork.PeopleRepository.CreateAsync(person, token);
            await unitOfWork.SaveChangesAsync(token);
            return mapper.Map<PersonDto>(person);
        }

        public async Task<IEnumerable<PersonDto>> GetAllPeopleAsync(CancellationToken token = default)
        {
            var people = await unitOfWork.PeopleRepository.GetAllAsync(token);
            return mapper.Map<IEnumerable<PersonDto>>(people);
        }

        public async Task<PersonDto> GetPersonByFirstNameAndLastNameAsync(string firstName, string lastName, CancellationToken token = default)
        {
            var person = (await unitOfWork.PeopleRepository.GetAllAsync((person)
                => person.FirstName.Equals(firstName) &&
                person.LastName.Equals(lastName), token)).FirstOrDefault();
            return mapper.Map<PersonDto>(person);
        }

        public async Task<PersonDto> GetPersonByIdAsync(Guid id, CancellationToken token = default)
        {
            var person = await unitOfWork.PeopleRepository.GetAsync(id, token);
            return mapper.Map<PersonDto>(person);
        }

        public async Task RemovePersonByIdAsync(Guid id, CancellationToken token = default)
        {
            await unitOfWork.PeopleRepository.RemoveAsync(id, token);
            await unitOfWork.SaveChangesAsync(token);
        }

        public async Task<PersonDto> UpdatePersonAsync(PersonDto model, CancellationToken token = default)
        {
            var person = new Person
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birth = model.Birth,
                // CreatedAt = person.CreatedAt
            };
            await unitOfWork.PeopleRepository.UpdateAsync(person, token);
            await unitOfWork.SaveChangesAsync(token);

            return mapper.Map<PersonDto>(person);
        }
    }
}
