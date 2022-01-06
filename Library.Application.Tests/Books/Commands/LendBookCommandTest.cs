using FluentValidation.TestHelper;
using Library.Application.Book.Commands.LendBook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Commands
{
    public class LendBookCommandTest : TestBase
    {
        [Fact]
        public async Task Lend_Book()
        {
            await using var context = GetContextWithData();
            var handler = new LendBookCommandHandler(context);
            var command = new LendBookCommand
            {
                Id = (await context.Books.FirstOrDefaultAsync()).Id,
                LenderId = (await context.Persons.FirstOrDefaultAsync()).Id
            };

            await handler.Handle(command, CancellationToken.None);

            var book = await context.Books.FindAsync(command.Id);

            Assert.NotNull(book.Lender);
        }

        [Fact]
        public void Lend_Book_With_No_Id_Throws_Exception()
        {
            var model = new LendBookCommand {Id = Guid.Empty};
            var validator = new LendBookCommandValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Lend_Book_With_No_Lender_Id_Throws_Exception()
        {
            var model = new LendBookCommand {LenderId = Guid.Empty};
            var validator = new LendBookCommandValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.LenderId);
        }
    }
}