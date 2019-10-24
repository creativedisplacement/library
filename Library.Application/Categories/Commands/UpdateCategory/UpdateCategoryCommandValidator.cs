using FluentValidation;

namespace Library.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}