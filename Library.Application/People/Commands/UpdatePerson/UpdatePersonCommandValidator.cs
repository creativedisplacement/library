using FluentValidation;

namespace Library.Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).Length(20).NotEmpty();
            RuleFor(x => x.Email).Length(50).NotEmpty();
        }
    }
}