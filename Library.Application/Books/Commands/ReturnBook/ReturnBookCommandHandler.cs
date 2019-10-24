using Library.Application.Exceptions;
using Library.Common.Book.Queries.GetBook;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Books.Commands.ReturnBook
{
    public class ReturnBookCommandHandler : BaseCommandHandler, IRequestHandler<ReturnBookCommand, GetBookModel>
    {
        private readonly LibraryDbContext _context;

        public ReturnBookCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GetBookModel> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.Include(i => i.Lender).SingleAsync(c => c.Id == request.Id, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            book.ReturnBook();
            SetDomainState(book);
            await _context.SaveChangesAsync(cancellationToken);

            return new GetBookModel
            {
                Id = book.Id,
                Title = book.Title,
                Categories = book.BookCategories.Select(c => new GetBookModelCategory { Id = c.CategoryId, Name = c.Category.Name }).ToList()
            };
        }
    }
}