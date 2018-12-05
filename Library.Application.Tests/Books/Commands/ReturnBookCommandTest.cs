using Library.Application.Books.Commands.ReturnBook;
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.Books.Commands
{
    [TestClass]
    public class ReturnBookCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly ReturnBookCommandHandler _commandHandler;

        public ReturnBookCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new ReturnBookCommandHandler(_context);
        }

        [TestMethod]
        public async Task Return_Book()
        {
            var command = new ReturnBookCommand
            {
                Id =  _context.Books.First().Id
            };

            await _commandHandler.Handle(command, CancellationToken.None);

            var book = await _context.Books.FindAsync(command.Id);

            Assert.IsNull(book.Lender);
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            context.Books.Add(new Book("Title", new List<BookCategory> { new BookCategory { CategoryId = Guid.NewGuid() } }, new Person("Test", "test@test.com", false)));
            context.SaveChanges();
            return context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}