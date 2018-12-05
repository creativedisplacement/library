using Library.Application.People.Commands.DeletePerson;
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
    public class DeletePersonCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly DeletePersonCommandHandler _commandHandler;

        public DeletePersonCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new DeletePersonCommandHandler(_context);
        }

        [TestMethod]
        public async Task Delete_Person()
        {
            var command = new DeletePersonCommand
            {
                Id = (await _context.Persons.FirstOrDefaultAsync()).Id
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            Assert.IsNull(await _context.Persons.FindAsync(command.Id));
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