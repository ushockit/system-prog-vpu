using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Features.Commands.CreatePerson
{
    public class CreatePersonCommandRequest : IRequest<CreatePersonCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birth { get; set; }
    }
}
