using Library.Common;
using MediatR;

namespace Library.Application.People.Commands.CreatePerson
{
    public class CreatePersonCommand : BasePersonItem, IRequest
    {
    }
}