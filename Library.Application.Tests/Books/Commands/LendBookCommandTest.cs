using FluentValidation.TestHelper;
using Library.Application.Book.Commands.LendBook;
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

        [Fact]
        public void Lend_Book_With_No_Id_Throws_Exception()
        {
            var validator = new LendBookCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }

        [Fact]
        public void Lend_Book_With_No_Lender_Id_Throws_Exception()
        {
            var validator = new LendBookCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.LenderId, Guid.Empty);
        }

        private LibraryDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            context.Books.Add(new Domain.Entities.Book("Title", new List<BookCategory> { new BookCategory { CategoryId = Guid.NewGuid() } }));
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