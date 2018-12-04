using MediatR;

namespace Library.Application.People.Commands.CreatePerson
{
    public class CreatePersonCommand : IRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}