using FluentValidation;

namespace Library.Application.Books.Commands.ReturnBook
{
    public class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}