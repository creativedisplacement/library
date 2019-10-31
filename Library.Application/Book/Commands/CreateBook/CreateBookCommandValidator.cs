using FluentValidation;
using Library.Persistence;
using System.Linq;

namespace Library.Application.Book.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator(LibraryDbContext context)
        {
            RuleFor(x => x.Title)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(x => x.Categories).NotEmpty();

            RuleFor(x => x.Title).Must(title => context.Books.Count(b => b.Title == title) == 0)
                .WithMessage("The title for this book already exists in the database.");
        }
    }
}