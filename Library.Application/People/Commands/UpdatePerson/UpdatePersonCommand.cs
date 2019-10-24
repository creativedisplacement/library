using Library.Common;
using Library.Common.People.Queries.GetPerson;
using MediatR;

namespace Library.Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommand : BasePersonItem, IRequest<GetPersonModel>
    {
    }
}