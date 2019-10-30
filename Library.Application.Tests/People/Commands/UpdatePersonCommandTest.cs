using FluentValidation.TestHelper;
using Library.Application.People.Commands.UpdatePerson;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.People.Commands
{
    public class UpdatePeopleCommandTest : TestBase
    {
        [Fact]
        public async Task Update_Person()
        {
            using (var context = GetContextWithData())
            {
                var handler = new UpdatePersonCommandHandler(context);
                var command = new UpdatePersonCommand
                {
                    Id = (await context.Persons.FirstOrDefaultAsync()).Id,
                    Name = "Name2",
                    Email = "email2@mail.com",
                    IsAdmin = true
                };

                await handler.Handle(command, CancellationToken.None);

                var person = await context.Persons.FindAsync(command.Id);

                Assert.Equal(command.Name, person.Name);
                Assert.Equal(command.Email, person.Email);
                Assert.Equal(command.IsAdmin, person.IsAdmin);
            }
        }

        [Fact]
        public void Update_Person_With_No_Id_Throws_Exception()
        {
            var validator = new UpdatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }

        [Fact]
        public void Update_Person_With_No_Name_Throws_Exception()
        {
            var validator = new UpdatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [Fact]
        public void Update_Person_With_No_Email_Throws_Exception()
        {
            var validator = new UpdatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Email, string.Empty);
        }

        [Fact]
        public void Update_Person_With_Invalid_Email_Throws_Exception()
        {
            var validator = new UpdatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Email, "111");
        }
    }
}