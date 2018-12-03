using FluentValidation;

namespace Library.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).Length(50).NotEmpty();
            RuleFor(x => x.Categories).NotEmpty();
        }
    }
}