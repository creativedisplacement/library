using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Unit>
    {
        private readonly LibraryDbContext _context;

        public UpdatePersonCommandHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.SingleAsync(c => c.Id == request.Id, cancellationToken);

            if (person == null)
            {
                throw new NotFoundException(nameof(Person), request.Id);
            }

            person.UpdatePerson(request.Name, request.Email, request.IsAdmin);
            _context.Persons.Update(person);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}