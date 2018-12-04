using MediatR;
using System;

namespace Library.Application.People.Commands.DeletePerson
{
    public class DeletePersonCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}