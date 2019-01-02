using FluentValidation;

namespace Library.Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).Length(20).NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email address cannot be empty")
                .EmailAddress()
                .WithMessage("Email address must be valid");
        }
    }
}