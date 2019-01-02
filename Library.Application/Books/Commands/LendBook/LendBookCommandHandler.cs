using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Books.Commands.LendBook
{
    public class LendBookCommandHandler : BaseCommandHandler, IRequestHandler<LendBookCommand, Unit>
    {
        private readonly LibraryDbContext _context;

        public LendBookCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(LendBookCommand request, CancellationToken cancellationToken)
        {
            var bookTask = _context.Books.SingleAsync(c => c.Id == request.Id, cancellationToken);
            var lenderTask = _context.Persons.SingleAsync(c => c.Id == request.LenderId, cancellationToken);

            await Task.WhenAll(bookTask, lenderTask);

            var book = bookTask.Result;
            var lender = lenderTask.Result;

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            if (lender == null)
            {
                throw new NotFoundException(nameof(Person), request.LenderId);
            }

            book.LendBook(lenderTask.Result);
            SetDomainState(book);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}