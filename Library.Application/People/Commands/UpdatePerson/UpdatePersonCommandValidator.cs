using System;
using FluentValidation;
using Library.Persistence;
using System.Linq;

namespace Library.Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        private readonly LibraryDbContext _context;

        public UpdatePersonCommandValidator(LibraryDbContext context, Guid personId)
        {
            _context = context;
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(20)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email address cannot be empty")
                .EmailAddress()
                .WithMessage("Email address must be valid");

            RuleFor(x => x.Name).Must(name => NameExists(name, personId))
                .WithMessage("The name for this person already exists in the database.");

            RuleFor(x => x.Email).Must(name => EmailAddressExists(name, personId))
                .WithMessage("The email address for this person already exists in the database.");
        }

        private bool EmailAddressExists(string email, Guid personId)
        {
            var result = _context.Persons.Count(c => c.Email == email && c.Id != personId);
            return result <= 0;
        }

        private bool NameExists(string name, Guid personId)
        {
            var result = _context.Persons.Count(c => c.Name == name && c.Id != personId);
            return result <= 0;
        }
    }
}