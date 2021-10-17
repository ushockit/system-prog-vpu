using MediatR;
using Services.Abstract;
using Services.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Features.Commands.CreatePerson
{
    public class CreatePersonCommandHandler 
        : IRequestHandler<CreatePersonCommandRequest, CreatePersonCommandResponse>
    {
        readonly IServiceManager _serviceManager;
        public CreatePersonCommandHandler(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        public async Task<CreatePersonCommandResponse> Handle(CreatePersonCommandRequest request, CancellationToken cancellationToken)
        {
            PersonDto person = new PersonDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Birth = request.Birth
            };

            var newPerson = await _serviceManager.PeopleService.CreateNewPersonAsync(person, cancellationToken);

            return new CreatePersonCommandResponse
            {
                Succeed = newPerson is null ? false : true,
                Message = "Success added"
            };
        }
    }
}
