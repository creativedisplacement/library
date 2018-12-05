using Library.Application.Categories.Queries.GetCategories;
using Library.Application.Tests.Infrastructure;
using Library.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.Categories.Queries
{
    [TestClass]
    public class GetCategoriesQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetCategoriesQueryHandlerTests() : this(new QueryTestFixture())
        {
            
        }

        public GetCategoriesQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [TestMethod]
        public async Task Get_Categories()
        {
            var queryHandler = new GetCategoriesQueryHandler(_context);

            var result = await queryHandler.Handle(new GetCategoriesQuery(), CancellationToken.None);

            Assert.IsInstanceOfType(result,typeof(GetCategoriesModel));
            Assert.AreEqual(_context.Categories.Count(), result.Categories.Count());
        }
    }
}
