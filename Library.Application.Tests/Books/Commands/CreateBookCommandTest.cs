using Library.Application.Books.Commands.CreateBook;
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Tests.Books.Commands
{
    [TestClass]
    public class CreateBookCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly CreateBookCommandHandler _commandHandler;

        public CreateBookCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new CreateBookCommandHandler(_context);
        }

        [TestMethod]
        public async Task Create_New_Book()
        {
            var command = new CreateBookCommand
            {
                Title = "Title",
                Categories = new List<BookCategory> { new BookCategory { CategoryId = Guid.NewGuid()} }
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            var book = await _context.Books.SingleOrDefaultAsync(c => c.Title == command.Title);

            Assert.AreEqual(command.Title, book.Title);
            CollectionAssert.AreEqual(command.Categories.ToList(), book.BookCategories.ToList());
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