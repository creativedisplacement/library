using Library.Application.Book.Queries.GetBook;
using Library.Application.Tests.Infrastructure;
using Library.Common.Book.Queries.GetBook;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Queries
{

    public class GetBookQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetBookQueryHandlerTests()
        {
            _context = new QueryTestFixture().Context;
        }

        [Fact]
        public async Task Get_Book()
        {
            var queryHandler = new GetBookQueryHandler(_context);
            var book = _context.Books.Include(i => i.BookCategories).First();

            var result = await queryHandler.Handle(new GetBookQuery {Id = book.Id}, CancellationToken.None);

            Assert.IsType<GetBookModel>(result);
            Assert.Equal(result.Id, book.Id);
            Assert.Equal(result.Title, book.Title);
            Assert.Equal(result.Categories.Select(c => c.Name).OrderBy(c => c).ToList(),
                book.BookCategories.Select(c => c.Category.Name).OrderBy(c => c).ToList());
        }
    }
}