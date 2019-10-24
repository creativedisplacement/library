using Library.Application.Exceptions;
using Library.Common.Book.Commands.UpdateBook;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Book.Queries.GetBook;

namespace Library.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : BaseCommandHandler, IRequestHandler<UpdateBookCommand, GetBookModel>
    {
        private readonly LibraryDbContext _context;

        public UpdateBookCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GetBookModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.SingleAsync(c => c.Id == request.Id, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            book.UpdateBook(request.Title, request.Categories.Select(c => new BookCategory { CategoryId = c.Id, Category = new Category(c.Name) }).ToList());
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