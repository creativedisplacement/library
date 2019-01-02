using FluentValidation.TestHelper;
using Library.Application.Books.Commands.DeleteBook;
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

    public class DeleteBookCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly DeleteBookCommandHandler _commandHandler;

        public DeleteBookCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new DeleteBookCommandHandler(_context);
        }

        [Fact]
        public async Task Delete_Book()
        {
            var command = new DeleteBookCommand
            {
                Id = (await _context.Books.FirstOrDefaultAsync()).Id
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            Assert.Null(await _context.Books.FindAsync(command.Id));
        }

        [Fact]
        public void Delete_Book_With_No_Id_Throws_Exception()
        {
            var validator = new DeleteBookCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
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