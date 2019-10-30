using FluentValidation.TestHelper;
using Library.Application.Book.Commands.CreateBook;
using Library.Common.Models.Book;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Commands
{
    public class CreateBookCommandTest : TestBase
    {
        [Fact]
        public async Task Create_New_Book()
        {
            using (var context = GetContextWithData())
            {
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
    }
}