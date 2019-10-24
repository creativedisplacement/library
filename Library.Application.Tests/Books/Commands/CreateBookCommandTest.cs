using FluentValidation.TestHelper;
using Library.Application.Books.Commands.CreateBook;
using Library.Common.Book.Commands.CreateBook;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Commands
{

    public class CreateBookCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly CreateBookCommandHandler _commandHandler;

        public CreateBookCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new CreateBookCommandHandler(_context);
        }

        [Fact]
        public async Task Create_New_Book()
        {
            var command = new CreateBookCommand
            {
                Title = "Title",
                Categories = new List<CreateBookModelCategory> { new CreateBookModelCategory { Id = Guid.NewGuid()} }
            };

            await _commandHandler.Handle(command, CancellationToken.None);
            var book = await _context.Books.SingleOrDefaultAsync(c => c.Title == command.Title);

            Assert.Equal(command.Title, book.Title);
            Assert.Equal(command.Categories.ToList().Count, book.BookCategories.ToList().Count);
            Assert.Equal(command.Categories.FirstOrDefault()?.Id, book.BookCategories.FirstOrDefault()?.CategoryId);
        }

        [Fact]
        public void Create_Book_With_No_Title_Throws_Exception()
        {
            var validator = new CreateBookCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Title, string.Empty);
        }

        [Fact]
        public void Create_Book_With_No_Categories_Throws_Exception()
        {
            var validator = new CreateBookCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Categories, new List<CreateBookModelCategory>());
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