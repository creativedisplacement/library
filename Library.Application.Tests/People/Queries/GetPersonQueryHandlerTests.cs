using Library.Application.People.Queries.GetPerson;
using Library.Application.Tests.Infrastructure;
using Library.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.People.Queries
{

    public class GetPersonQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetPersonQueryHandlerTests()
        {
            _context = new QueryTestFixture().Context;
        }

        [Fact]
        public async Task Get_Person()
        {
            var queryHandler = new GetPersonQueryHandler(_context);
            var person = _context.Persons.First();

            var result = await queryHandler.Handle(new GetPersonQuery { Id = person.Id }, CancellationToken.None);

            Assert.IsType<GetPersonModel>(result);
            Assert.Equal(result.Id, person.Id);
            Assert.Equal(result.Name, person.Name);
            Assert.Equal(result.Email, person.Email);
            Assert.Equal(result.IsAdmin, person.IsAdmin);
        }
    }
}