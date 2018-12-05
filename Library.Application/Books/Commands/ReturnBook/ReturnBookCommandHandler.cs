using Library.Application.Books.Commands.LendBook;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Books.Commands.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Unit>
    {
        private readonly LibraryDbContext _context;

        public ReturnBookCommandHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.Include(i => i.Lender).SingleAsync(c => c.Id == request.Id, cancellationToken);
          
            if (book == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            book.ReturnBook();
            _context.Books.Update(book);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}