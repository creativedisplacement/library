using MediatR;
using System;
using Library.Common.People.Queries.GetPerson;

namespace Library.Application.People.Queries.GetPerson
{
    public class GetPersonQuery : IRequest<GetPersonModel>
    {
        public Guid Id { get; set; }
    }
}