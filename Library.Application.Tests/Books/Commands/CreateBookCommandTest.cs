﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Library.Application.Book.Commands.CreateBook;
using Library.Common.Models.Book;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Library.Application.Tests.Books.Commands
{
    public class CreateBookCommandTest : TestBase
    {
        [Fact]
        public async Task Create_New_Book()
        {
            await using var context = GetContextWithData();
            var handler = new CreateBookCommandHandler(context);
            var categoryId = Guid.NewGuid();

            await AddCategory(context, categoryId, "nothing");

            var command = new CreateBookCommand
            {
                Title = "Title",
                Categories = new List<CreateBookModelCategory> {new CreateBookModelCategory {Id = categoryId}}
            };

            await handler.Handle(command, CancellationToken.None);

            var book = await context.Books.SingleOrDefaultAsync(c => c.Title == command.Title);

            Assert.Equal(command.Title, book.Title);
            Assert.Equal(command.Categories.ToList().Count, book.BookCategories.ToList().Count);
            Assert.Equal(command.Categories.FirstOrDefault()?.Id, book.BookCategories.FirstOrDefault()?.CategoryId);
        }

        [Fact]
        public void Create_Book_With_No_Title_Throws_Exception()
        {
            using var context = GetContextWithData();
            var model = new CreateBookCommand {Title = null};
            var validator = new CreateBookCommandValidator(context);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Create_Book_With_No_Categories_Throws_Exception()
        {
            using var context = GetContextWithData();
            var model = new CreateBookCommand {Categories = new List<CreateBookModelCategory>()};
            var validator = new CreateBookCommandValidator(context);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Categories);
        }

        [Fact]
        public void Create_Book_With_Title_That_Already_Exists_Throws_Exception()
        {
            using var context = GetContextWithData();
            var validator = new CreateBookCommandValidator(context);
            var result = validator.TestValidate(new CreateBookCommand
            {
                Id = new Guid(),
                Title = context.Books.FirstOrDefault()?.Title,
                Categories = context.BookCategories.Select(c => new CreateBookModelCategory{ Id = c.Category.Id, Name = c.Category.Name}).ToList()
            });
            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Create_Book_With_Title_That_Does_Not_Already_Exists()
        {
            using var context = GetContextWithData();
            var model = new CreateBookCommand {Title = "Title9"};
            var validator = new CreateBookCommandValidator(context);
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        }
    }
}