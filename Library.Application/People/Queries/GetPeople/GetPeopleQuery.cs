using MediatR;
using System;

namespace Library.Application.People.Queries.GetPeople
{
    public class GetPeopleQuery : IRequest<GetPeopleModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
    }
}