﻿using FluentValidation.TestHelper;
using Library.Application.Book.Commands.ReturnBook;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.Books.Commands
{
    public class ReturnBookCommandTest : TestBase
    {
        [Fact]
        public async Task Return_Book()
        {
            await using var context = GetContextWithData();
            var handler = new ReturnBookCommandHandler(context);
            var command = new ReturnBookCommand
            {
                Id = context.Books.First().Id
            };

            var returnedBook = await handler.Handle(command, CancellationToken.None);

            Assert.Null(returnedBook.Lender);
        }

        [Fact]
        public void Return_Book_With_No_Id_Throws_Exception()
        {
            var model = new ReturnBookCommand {Id = Guid.Empty};
            var validator = new ReturnBookCommandValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}