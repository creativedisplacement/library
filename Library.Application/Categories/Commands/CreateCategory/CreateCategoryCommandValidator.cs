using FluentValidation;

namespace Library.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}