using System;
using FluentValidation;
using Library.Persistence;
using System.Linq;

namespace Library.Application.Book.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        private readonly LibraryDbContext _context;

        public UpdateBookCommandValidator(LibraryDbContext context)
        {
            _context = context;
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(x => x.Categories).NotEmpty();

            RuleFor(x => x).Must(book => TitleExists(book.Title, book.Id))
                .WithMessage("The title for this book already exists in the database.");
        }

        private bool TitleExists(string title, Guid bookId)
        {
            var result = _context.Books.Count(c => c.Title == title && c.Id != bookId);
            return result <= 0;
        }
    }
}