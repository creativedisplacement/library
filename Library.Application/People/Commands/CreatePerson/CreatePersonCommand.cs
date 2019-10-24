using Library.Common;
using Library.Common.People.Queries.GetPerson;
using MediatR;

namespace Library.Application.People.Commands.CreatePerson
{
    public class CreatePersonCommand : BasePersonItem, IRequest<GetPersonModel>
    {
    }
}