using FluentValidation;

namespace Library.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
    {
        public GetCategoryQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}