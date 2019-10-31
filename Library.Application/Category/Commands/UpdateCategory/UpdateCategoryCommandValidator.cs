using FluentValidation;
using Library.Persistence;
using System;
using System.Linq;

namespace Library.Application.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(LibraryDbContext context, Guid categoryId)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(x => x.Id).Must(id => context.Categories.Count(c => c.Id == id && c.Id == categoryId) == 0)
                .WithMessage("The name for this category already exists in the database.");
        }
    }
}