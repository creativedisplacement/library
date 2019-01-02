using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Library.Domain.Enums;

namespace Library.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : BaseCommandHandler, IRequestHandler<CreateBookCommand, Unit>
    {
        private readonly LibraryDbContext _context;

        public CreateBookCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book(request.Title, request.Categories);
            SetDomainState(book);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}