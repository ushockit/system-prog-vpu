using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Features.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommandRequest>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotNull()
                .WithMessage("First name can`t be null")
                .MaximumLength(20)
                .MinimumLength(3)
                .WithMessage("'First name' must be 3 - 20 symbols");
            
        }
    }
}
