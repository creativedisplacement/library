using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Models.Books;

namespace Library.Application.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, GetBooksModel>
    {
        private readonly LibraryDbContext _context;

        public GetBooksQueryHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<GetBooksModel> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.Book> books = _context.Books
                .Include(i => i.BookCategories)
                .ThenInclude(c => c.Category);

            if (!string.IsNullOrEmpty(request.Title))
            {
                books = books.Where(b => b.Title.Contains(request.Title));
            }

            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                books = books.Where(b =>
                    b.BookCategories.Any(bc => request.CategoryIds.Any(c => bc.CategoryId == c)));
            }

            if (request.LenderId != Guid.Empty)
            {
                books = books.Where(b => b.Lender.Id == request.LenderId);
            }

            if (request.IsAvailable.HasValue)
            {
                books = books.Where(bb => bb.IsAvailable == request.IsAvailable);
            }

            return new GetBooksModel
            {
                Books = await books
                    .Select(b => new GetBookModel {Id = b.Id, Title = b.Title, Categories = b.BookCategories.Select(c => new GetBookModelCategory{ Id = c.CategoryId, Name = c.Category.Name})})
                    .OrderBy(b => b.Title)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}