using Library.Application.People.Commands.UpdatePerson;
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.People.Commands
{
    [TestClass]
    public class UpdatePeopleCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly UpdatePersonCommandHandler _commandHandler;

        public UpdatePeopleCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new UpdatePersonCommandHandler(_context);
        }

        [TestMethod]
        public async Task Update_Person()
        {
            var command = new UpdatePersonCommand
            {
                Id = (await _context.Persons.FirstOrDefaultAsync()).Id,
                Name = "Name2",
                Email = "email2@mail.com",
                IsAdmin = true
            };

            await _commandHandler.Handle(command, CancellationToken.None);

            var person = await _context.Persons.FindAsync(command.Id);

            Assert.AreEqual(command.Name, person.Name);
            Assert.AreEqual(command.Email, person.Email);
            Assert.AreEqual(command.IsAdmin, person.IsAdmin);
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            context.Persons.Add(new Person("Name", "email@mail.com", false));
            context.SaveChanges();
            return context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}