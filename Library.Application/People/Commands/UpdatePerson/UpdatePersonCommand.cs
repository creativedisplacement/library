using Library.Common.Models.Person;

namespace Library.Application.People.Commands.UpdatePerson;

public class UpdatePersonCommand : BasePersonItem, IRequest<GetPersonModel>
{
}