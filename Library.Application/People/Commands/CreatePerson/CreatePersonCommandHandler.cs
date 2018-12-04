using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.People.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Unit>
    {
        private readonly LibraryDbContext _context;

        public CreatePersonCommandHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person(request.Name, request.Email, request.IsAdmin);

            _context.Persons.Add(person);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}