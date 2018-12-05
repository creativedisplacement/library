using Library.Application.People.Queries.GetPerson;
using Library.Application.Tests.Infrastructure;
using Library.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.People.Queries
{
    [TestClass]
    public class GetPersonQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetPersonQueryHandlerTests() : this(new QueryTestFixture())
        {

        }

        public GetPersonQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [TestMethod]
        public async Task Get_Person()
        {
            var queryHandler = new GetPersonQueryHandler(_context);
            var person = _context.Persons.First();

            var result = await queryHandler.Handle(new GetPersonQuery { Id = person.Id }, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetPersonModel));
            Assert.AreEqual(result.Id, person.Id);
            Assert.AreEqual(result.Name, person.Name);
            Assert.AreEqual(result.Email, person.Email);
            Assert.AreEqual(result.IsAdmin, person.IsAdmin);
        }
    }
}