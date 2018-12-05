using Library.Application.Books.Commands.DeleteBook;
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.Books.Commands
{
    [TestClass]
    public class DeleteBookCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly DeleteBookCommandHandler _commandHandler;

        public DeleteBookCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new DeleteBookCommandHandler(_context);
        }

        [TestMethod]
        public async Task Delete_Book()
        {
            var command = new DeleteBookCommand
            {
                Id = (await _context.Books.FirstOrDefaultAsync()).Id
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            Assert.IsNull(await _context.Books.FindAsync(command.Id));
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            context.Books.Add(new Book("Title", new List<BookCategory> { new BookCategory { CategoryId = Guid.NewGuid() } }));
            context.SaveChanges();
            return context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}