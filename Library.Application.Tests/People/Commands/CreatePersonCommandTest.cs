using FluentValidation.TestHelper;
using Library.Application.People.Commands.CreatePerson;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Application.Tests.People.Commands
{
    public class CreatePersonCommandTest : TestBase
    {
        [Fact]
        public async Task Create_New_Person()
        {
            using (var context = GetContextWithData())
            {
                var handler = new CreatePersonCommandHandler(context);
                var command = new CreatePersonCommand
                {
                    Name = "Name",
                    Email = "email@mail.com",
                    IsAdmin = false
                };

                await handler.Handle(command, CancellationToken.None);
                var person = await context.Persons.SingleOrDefaultAsync(c => c.Name == command.Name);

                Assert.Equal(command.Name, person.Name);
                Assert.Equal(command.Email, person.Email);
                Assert.Equal(command.IsAdmin, person.IsAdmin);
            }
        }

        [Fact]
        public void Create_New_Person_With_No_Email_Throws_Exception()
        {
            var validator = new CreatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Email, string.Empty);
        }


        [Fact]
        public void Create_New_Person_With_Invalid_Email_Throws_Exception()
        {
            var validator = new CreatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Email, "111");
        }

        [Fact]
        public void Create_New_Person_With_No_Name_Throws_Exception()
        {
            var validator = new CreatePersonCommandValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }
    }
}