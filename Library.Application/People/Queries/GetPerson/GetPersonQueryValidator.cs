using FluentValidation;

namespace Library.Application.People.Queries.GetPerson
{
    public class GetPersonQueryValidator : AbstractValidator<GetPersonModel>
    {
        public GetPersonQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}