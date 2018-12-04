using MediatR;
using System;

namespace Library.Application.People.Queries.GetPerson
{
    public class GetPersonQuery : IRequest<GetPersonModel>
    {
        public Guid Id { get; set; }
    }
}