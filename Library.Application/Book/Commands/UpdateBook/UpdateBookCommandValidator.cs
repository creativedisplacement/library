using FluentValidation;
using Library.Persistence;
using System.Linq;

namespace Library.Application.Book.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator(LibraryDbContext context)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(x => x.Categories).NotEmpty();

            RuleFor(x => x).Must(book => context.Books.Count(b => b.Title == book.Title && b.Id != book.Id) > 0)
                .WithMessage("The title for this book already exists in the database.");
        }
    }
}