using Library.Common.Models.Person;

namespace Library.Application.People.Commands.CreatePerson;

public class CreatePersonCommand : BasePersonItem, IRequest<GetPersonModel>
{
}