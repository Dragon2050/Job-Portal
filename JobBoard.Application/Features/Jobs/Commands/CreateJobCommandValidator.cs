using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace JobBoard.Application.Features.Jobs.Commands
{
    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        public CreateJobCommandValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required");
            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0");
            RuleFor(x => x.CreatedById)
                .NotEmpty().WithMessage("CreatedById is required");
        }
    }
}
