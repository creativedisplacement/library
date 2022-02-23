using System.Threading;
using System.Threading.Tasks;
using Library.Common.Models.Person;
using Library.Domain.Entities;
using Library.Persistence;

namespace Library.Application.People.Commands.CreatePerson;

public class CreatePersonCommandHandler : BaseCommandHandler, IRequestHandler<CreatePersonCommand, GetPersonModel>
{
    private readonly LibraryDbContext _context;

    public CreatePersonCommandHandler(LibraryDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<GetPersonModel> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Person(request.Name, request.Email, request.IsAdmin);
        SetDomainState(person);
        await _context.SaveChangesAsync(cancellationToken);

        return new GetPersonModel
        {
            Id = person.Id,
            Name = person.Name,
            Email = person.Email,
            IsAdmin = person.IsAdmin
        };
    }
}