using MediatR;

namespace Library.Application.People.Queries.GetPeople
{
    public class GetPeopleQuery : IRequest<GetPeopleModel>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
    }
}