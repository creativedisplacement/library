using Library.Common;
using Library.Common.People.Queries.GetPerson;
using MediatR;

namespace Library.Application.People.Queries.GetPerson
{
    public class GetPersonQuery : BaseItem, IRequest<GetPersonModel>
    {
    }
}