using Library.Application.Categories.Queries.GetCategory;
using Library.Application.Tests.Infrastructure;
using Library.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.Categories.Queries
{
    [TestClass]
    public class GetCategoryQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetCategoryQueryHandlerTests() : this(new QueryTestFixture())
        {
            
        }

        public GetCategoryQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [TestMethod]
        public async Task Get_Category()
        {
            var queryHandler = new GetCategoryQueryHandler(_context);
            var category = _context.Categories.First();

            var result = await queryHandler.Handle(new GetCategoryQuery{ Id = category.Id}, CancellationToken.None);

            Assert.IsInstanceOfType(result,typeof(GetCategoryModel));
            Assert.AreEqual(result.Id, category.Id);
            Assert.AreEqual(result.Name, category.Name);
        }
    }
}