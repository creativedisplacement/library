using Library.Application.People.Queries.GetPeople;
using Library.Application.Tests.Infrastructure;
using Library.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.People.Queries
{
    [TestClass]
    public class GetPeopleQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetPeopleQueryHandlerTests() : this(new QueryTestFixture())
        {

        }

        public GetPeopleQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [TestMethod]
        public async Task Get_All_People()
        {
            var queryHandler = new GetPeopleQueryHandler(_context);
            var result = await queryHandler.Handle(new GetPeopleQuery(), CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetPeopleModel));
            Assert.AreEqual(_context.Persons.Count(), result.People.Count());
        }

        [TestMethod]
        public async Task Get_People_By_Name()
        {
            const string name = "Victor";
            var queryHandler = new GetPeopleQueryHandler(_context);
            var result = await queryHandler.Handle(new GetPeopleQuery {Name = name }, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetPeopleModel));

            var person = _context.Persons.First(p => p.Name == name);
            Assert.AreEqual(person.Name, result.People.First().Name);
            Assert.AreEqual(person.Email, result.People.First().Email);
        }

        [TestMethod]
        public async Task Get_People_By_Email()
        {
            const string email = "victor@shodimeji.com";

            var queryHandler = new GetPeopleQueryHandler(_context);
            var result = await queryHandler.Handle(new GetPeopleQuery {Email = email },
                CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetPeopleModel));
            var person = _context.Persons.First(p => p.Email == email);
            Assert.AreEqual(person.Name, result.People.First().Name);
            Assert.AreEqual(person.Email, result.People.First().Email);
        }

        [TestMethod]
        public async Task Get_People_That_Are_Admins()
        {
            var queryHandler = new GetPeopleQueryHandler(_context);
            var result = await queryHandler.Handle(new GetPeopleQuery {IsAdmin = true}, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetPeopleModel));
            Assert.AreEqual(_context.Persons.Count(p => p.IsAdmin), result.People.Count());
            Assert.AreEqual(_context.Persons.First(p => p.IsAdmin).Name, result.People.First().Name);
        }

        [TestMethod]
        public async Task Get_People_That_Are_Not_Admins()
        {
            var queryHandler = new GetPeopleQueryHandler(_context);
            var result = await queryHandler.Handle(new GetPeopleQuery {IsAdmin = false}, CancellationToken.None);

            Assert.IsInstanceOfType(result, typeof(GetPeopleModel));
            Assert.AreEqual(_context.Persons.Count(p => !p.IsAdmin), result.People.Count());
            Assert.AreEqual(_context.Persons.First(p => !p.IsAdmin).Name, result.People.First().Name);
        }
    }
}