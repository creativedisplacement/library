using Library.Application.Books.Queries.GetBook;
using Library.Application.Tests.Infrastructure;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.Books.Queries
{
    [TestClass]
    public class GetBookQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetBookQueryHandlerTests() : this(new QueryTestFixture())
        {

        }

        public GetBookQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [TestMethod]
        public async Task Get_Book()
        {
            var queryHandler = new GetBookQueryHandler(_context);
            var book = _context.Books.Include(i => i.BookCategories).First();

            var result = await queryHandler.Handle(new GetBookQuery {Id = book.Id}, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetBookModel));
            Assert.AreEqual(result.Id, book.Id);
            Assert.AreEqual(result.Title, book.Title);
            CollectionAssert.AreEqual(result.Categories.Select(c => c.Name).OrderBy(c => c).ToList(),
                book.BookCategories.Select(c => c.Category.Name).OrderBy(c => c).ToList());
        }
    }
}