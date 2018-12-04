using FluentValidation;

namespace Library.Application.People.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.Name).Length(20).NotEmpty();
            RuleFor(x => x.Email).Length(50).NotEmpty();
        }
    }
}