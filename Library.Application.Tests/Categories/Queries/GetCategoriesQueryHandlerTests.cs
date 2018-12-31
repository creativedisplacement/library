using Library.Application.Categories.Queries.GetCategories;
using Library.Application.Tests.Infrastructure;
using Library.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Categories.Queries
{

    public class GetCategoriesQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetCategoriesQueryHandlerTests()
        {
            _context = new QueryTestFixture().Context;
        }

        [Fact]
        public async Task Get_Categories()
        {
            var queryHandler = new GetCategoriesQueryHandler(_context);

            var result = await queryHandler.Handle(new GetCategoriesQuery(), CancellationToken.None);

            Assert.IsType<GetCategoriesModel>(result);
            Assert.Equal(_context.Categories.Count(), result.Categories.Count());
        }
    }
}