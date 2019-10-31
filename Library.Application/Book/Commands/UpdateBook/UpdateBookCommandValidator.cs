using FluentValidation;
using Library.Persistence;
using System;
using System.Linq;

namespace Library.Application.Book.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator(LibraryDbContext context, Guid bookId)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(x => x.Categories).NotEmpty();

            RuleFor(x => x.Title).Must(title => context.Books.Count(b => b.Title == title && b.Id != bookId) > 0)
                .WithMessage("The title for this book already exists in the database.");
        }
    }
}