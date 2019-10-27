using FluentValidation;

namespace Library.Application.Category.Commands.CreateCategory
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