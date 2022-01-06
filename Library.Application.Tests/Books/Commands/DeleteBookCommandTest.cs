using FluentValidation.TestHelper;
using Library.Application.Book.Commands.DeleteBook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Commands
{
    public class DeleteBookCommandTest : TestBase
    {
        [Fact]
        public async Task Delete_Book()
        {
            await using var context = GetContextWithData();
            var handler = new DeleteBookCommandHandler(context);
            var command = new DeleteBookCommand
            {
                Id = (await context.Books.FirstOrDefaultAsync()).Id
            };

            await handler.Handle(command, CancellationToken.None);
            Assert.Null(await context.Books.FindAsync(command.Id));
        }

        [Fact]
        public void Delete_Book_With_No_Id_Throws_Exception()
        {
            var model = new DeleteBookCommand {Id = Guid.Empty};
            var validator = new DeleteBookCommandValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}