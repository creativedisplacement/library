using Library.Application.Books.Commands.UpdateBook;
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
    public class UpdateBookCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly UpdateBookCommandHandler _commandHandler;

        public UpdateBookCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new UpdateBookCommandHandler(_context);
        }

        [TestMethod]
        public async Task Update_Book()
        {
            var command = new UpdateBookCommand
            {
                Id = (await _context.Books.FirstOrDefaultAsync()).Id,
                Title = "Title2",
                Categories = new List<BookCategory> { new BookCategory { CategoryId = Guid.NewGuid() } }
            };

            await _commandHandler.Handle(command, CancellationToken.None);

            var book = await _context.Books.FindAsync(command.Id);

            Assert.AreEqual(command.Title, book.Title);
            CollectionAssert.AreEqual(command.Categories.ToList(), book.BookCategories.ToList());
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