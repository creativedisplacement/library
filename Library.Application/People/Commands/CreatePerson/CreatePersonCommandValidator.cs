using FluentValidation;

namespace Library.Application.People.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(20)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email address cannot be empty")
                .EmailAddress()
                .WithMessage("Email address must be valid");
        }
    }
}