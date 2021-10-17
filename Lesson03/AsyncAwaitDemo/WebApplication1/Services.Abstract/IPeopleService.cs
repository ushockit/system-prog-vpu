using Services.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IPeopleService
    {
        Task<IEnumerable<PersonDto>> GetAllPeopleAsync(CancellationToken token);
        Task<PersonDto> GetPersonByIdAsync(Guid id, CancellationToken token);
        Task<PersonDto> UpdatePersonAsync(PersonDto person, CancellationToken token);
        Task<PersonDto> CreateNewPersonAsync(PersonDto person, CancellationToken token);
        Task RemovePersonByIdAsync(Guid id, CancellationToken token);
        Task<PersonDto> GetPersonByFirstNameAndLastNameAsync(string firstName, string lastName, CancellationToken token = default);
    }
}
