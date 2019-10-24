using Library.Common;
using MediatR;

namespace Library.Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommand : BasePersonItem, IRequest
    {
    }
}