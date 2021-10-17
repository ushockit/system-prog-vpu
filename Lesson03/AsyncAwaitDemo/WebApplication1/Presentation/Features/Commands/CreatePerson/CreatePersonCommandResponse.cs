using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Features.Commands.CreatePerson
{
    public class CreatePersonCommandResponse
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
    }
}
