using Library.Application.Categories.Queries.GetCategory;
using Library.Application.Tests.Infrastructure;
using Library.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Categories.Queries
{

    public class GetCategoryQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetCategoryQueryHandlerTests()
        {
            _context = new QueryTestFixture().Context;
        }

        [Fact]
        public async Task Get_Category()
        {
            var queryHandler = new GetCategoryQueryHandler(_context);
            var category = _context.Categories.First();

            var result = await queryHandler.Handle(new GetCategoryQuery{ Id = category.Id}, CancellationToken.None);

            Assert.IsType<GetCategoryModel>(result);
            Assert.Equal(result.Id, category.Id);
            Assert.Equal(result.Name, category.Name);
        }
    }
}