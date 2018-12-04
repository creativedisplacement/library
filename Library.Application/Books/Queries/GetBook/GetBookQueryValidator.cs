using FluentValidation;

namespace Library.Application.Books.Queries.GetBook
{
    public class GetBookQueryValidator : AbstractValidator<GetBookQuery>
    {
        public GetBookQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}