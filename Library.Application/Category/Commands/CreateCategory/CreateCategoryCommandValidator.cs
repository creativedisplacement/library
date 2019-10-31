using FluentValidation;
using Library.Persistence;
using System.Linq;

namespace Library.Application.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(LibraryDbContext context)
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(x => x.Name).Must(name => context.Categories.Count(b => b.Name == name) == 0)
                .WithMessage("The category already exists in the database.");
        }
    }
}