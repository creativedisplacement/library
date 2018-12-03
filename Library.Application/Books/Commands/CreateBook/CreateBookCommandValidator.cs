using FluentValidation;

namespace Library.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title).Length(50).NotEmpty();
            RuleFor(x => x.Categories).NotEmpty();
        }
    }
}