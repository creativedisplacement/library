using Library.Application.People.Commands.CreatePerson;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.People.Commands
{
    [TestClass]
    public class CreatePersonCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly CreatePersonCommandHandler _commandHandler;

        public CreatePersonCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new CreatePersonCommandHandler(_context);
        }

        [TestMethod]
        public async Task Create_New_Person()
        {
            var command = new CreatePersonCommand
            {
                Name = "Name",
                Email = "email@mail.com",
                IsAdmin = false
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            var person = await _context.Persons.SingleOrDefaultAsync(c => c.Name == command.Name);

            Assert.AreEqual(command.Name, person.Name);
            Assert.AreEqual(command.Email, person.Email);
            Assert.AreEqual(command.IsAdmin, person.IsAdmin);
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            return context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}