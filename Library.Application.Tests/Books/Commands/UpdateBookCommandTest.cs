﻿using FluentValidation.TestHelper;
using Library.Application.Books.Commands.UpdateBook;
using Library.Domain.Entities;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Book.Queries.GetBook;
using Xunit;

namespace Library.Application.Tests.Books.Commands
{

    public class UpdateBookCommandTest : TestBase, IDisposable
    {
        private readonly LibraryDbContext _context;
        private readonly UpdateBookCommandHandler _commandHandler;

        public UpdateBookCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new UpdateBookCommandHandler(_context);
        }

        [Fact]
        public async Task Update_Book()
        {
            var command = new UpdateBookCommand
            {
                Id = (await _context.Books.FirstOrDefaultAsync()).Id,
                Title = "Title2",
                Categories = new List<GetBookModelCategory> { new GetBookModelCategory { Id = Guid.NewGuid() } }
            };

            await _commandHandler.Handle(command, CancellationToken.None);

            var book = await _context.Books.FindAsync(command.Id);

            Assert.Equal(command.Title, book.Title);
            Assert.Equal(command.Categories.ToList(), book.BookCategories.Select(b => new GetBookModelCategory{ Id = b.BookId, Name = b.Category.Name}).ToList());
        }

        [Fact]
        public void Update_Book_With_No_Id_Throws_Exception()
        {
            var validator = new UpdateBookCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }

        [Fact]
        public void Update_Book_With_No_Title_Throws_Exception()
        {
            var validator = new UpdateBookCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Title, string.Empty);
        }

        [Fact]
        public void Update_Book_With_No_Categories_Throws_Exception()
        {
            var validator = new UpdateBookCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Categories, new List<GetBookModelCategory>());
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