using Library.Common.Book.Queries.GetBook;
using Library.Domain.Entities;
using Library.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : BaseCommandHandler, IRequestHandler<CreateBookCommand, GetBookModel>
    {
        private readonly LibraryDbContext _context;

        public CreateBookCommandHandler(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GetBookModel> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book(request.Title, request.Categories.Select(c => new BookCategory{ CategoryId = c.Id, Category = new Category(c.Name)}).ToList());
            SetDomainState(book);
            await _context.SaveChangesAsync(cancellationToken);

            return new GetBookModel
            {
                Id = book.Id,
                Title = book.Title,
                Categories = book.BookCategories.Select(c => new GetBookModelCategory { Id = c.CategoryId, Name = c.Category.Name}).ToList()
            };
        }
    }
}