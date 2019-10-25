using Library.Application.People.Queries.GetPeople;
using Library.Application.Tests.Infrastructure;
using Library.Common.People.Queries.GetPeople;
using Library.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.People.Queries
{

    public class GetPeopleQueryHandlerTests
    {
        private readonly LibraryDbContext _context;

        public GetPeopleQueryHandlerTests()
        {
            _context = new QueryTestFixture().Context;
        }

        [Fact]
        public async Task Get_All_People()
        {
            var queryHandler = new GetPeopleQueryHandler(_context);
            var result = await queryHandler.Handle(new GetPeopleQuery(), CancellationToken.None);

            Assert.IsType<GetPeopleModel>(result);
            Assert.Equal(_context.Persons.Count(), result.People.Count());
        }

        [Fact]
        public async Task Get_People_By_Name()
        {
            const string name = "Victor";
            var queryHandler = new GetPeopleQueryHandler(_context);
            var result = await queryHandler.Handle(new GetPeopleQuery {Name = name }, CancellationToken.None);

            Assert.IsType<GetPeopleModel>(result);

            var person = _context.Persons.First(p => p.Name == name);
            Assert.Equal(person.Name, result.People.First().Name);
            Assert.Equal(person.Email, result.People.First().Email);
        }

        [Fact]
        public async Task Get_People_By_Email()
        {
            const string email = "victor@shodimeji.com";

            var queryHandler = new GetPeopleQueryHandler(_context);
            var result = await queryHandler.Handle(new GetPeopleQuery {Email = email },
                CancellationToken.None);

            Assert.IsType<GetPeopleModel>(result);
            var person = _context.Persons.First(p => p.Email == email);
            Assert.Equal(person.Name, result.People.First().Name);
            Assert.Equal(person.Email, result.People.First().Email);
        }

        [Fact]
        public async Task Get_People_That_Are_Admins()
        {
            var queryHandler = new GetPeopleQueryHandler(_context);
            var result = await queryHandler.Handle(new GetPeopleQuery {IsAdmin = true}, CancellationToken.None);

            Assert.IsType<GetPeopleModel>(result);
            Assert.Equal(_context.Persons.Count(p => p.IsAdmin.Value), result.People.Count());
            Assert.Equal(_context.Persons.First(p => p.IsAdmin.Value).Name, result.People.First().Name);
        }

        [Fact]
        public async Task Get_People_That_Are_Not_Admins()
        {
            var queryHandler = new GetPeopleQueryHandler(_context);
            var result = await queryHandler.Handle(new GetPeopleQuery {IsAdmin = false}, CancellationToken.None);

            Assert.IsType<GetPeopleModel>(result);
            Assert.Equal(_context.Persons.Count(p => !p.IsAdmin.Value), result.People.Count());
            Assert.Equal(_context.Persons.First(p => !p.IsAdmin.Value).Name, result.People.First().Name);
        }
    }
}