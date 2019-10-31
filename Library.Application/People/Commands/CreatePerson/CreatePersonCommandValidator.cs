using FluentValidation;
using Library.Persistence;
using System.Linq;

namespace Library.Application.People.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator(LibraryDbContext context)
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

            RuleFor(x => x.Name).Must(name => context.Persons.Count(b => b.Name == name) == 0)
                .WithMessage("The person already exists in the database.");

            RuleFor(x => x.Email).Must(email => context.Persons.Count(b => b.Email == email) == 0)
                .WithMessage("The email address already exists in the database.");
        }
    }
}