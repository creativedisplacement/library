using Library.Application.Books.Commands.LendBook;
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Commands
{

    public class LendBookCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly LendBookCommandHandler _commandHandler;

        public LendBookCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new LendBookCommandHandler(_context);
        }

        [Fact]
        public async Task Lend_Book()
        {
            var command = new LendBookCommand
            {
                Id = (await _context.Books.FirstOrDefaultAsync()).Id,
                LenderId = (await _context.Persons.FirstOrDefaultAsync()).Id
            };

            await _commandHandler.Handle(command, CancellationToken.None);

            var book = await _context.Books.FindAsync(command.Id);

            Assert.NotNull(book.Lender);
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            context.Books.Add(new Book("Title", new List<BookCategory> { new BookCategory { CategoryId = Guid.NewGuid() } }));
            context.Persons.Add(new Person("Test", "test@test.com", false));
            context.SaveChanges();
            return context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}