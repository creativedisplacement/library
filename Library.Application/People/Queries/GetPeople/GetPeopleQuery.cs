using Library.Common;
using Library.Common.People.Queries.GetPeople;
using MediatR;

namespace Library.Application.People.Queries.GetPeople
{
    public class GetPeopleQuery : BasePersonItem, IRequest<GetPeopleModel>
    {
    }
}